-- ============================================================================
-- RaceDay System - Database Creation & Schema Script (v2)
-- Two user tables kept (ORGANISER, PARTICIPANT) as per the v1 ERD.
--
-- CHANGES FROM v1 (to be listed in the README as deliberate differences):
--   1. ORGANISER.PasswordHash and PARTICIPANT.PasswordHash added (authentication).
--   2. PARTICIPANT.ProfilePictureUrl added (Part 3 - profile picture, Azure Blob).
--   3. EVENT.BannerImageUrl added (Part 3 - event banner, Azure Blob).
--   4. RESULT.IsPublished added (Organiser captures first, then publishes).
--   5. ENROLMENT.EnrolmentStatus now defaults to 'Pending' (Organiser confirms).
--   6. CATEGORY: UNIQUE (EventID, CategoryName) added (prevents duplicate categories).
--   7. CHECK constraints added: Distance > 0, FinishingPosition > 0.
--   8. VARCHAR changed to NVARCHAR so the schema matches EF Core Code-First defaults
--      (South African names often contain non-ASCII characters).
--   9. Script is re-runnable (drops RaceDayDB if it already exists).
--  10. Additional future-dated seed events so "upcoming events" has data.
--
-- Seed login for ALL seeded users:  password = Password123!
-- (hashes are ASP.NET Core Identity PasswordHasher<T> V3 hashes, so the API can
--  verify them with IPasswordHasher<T>.VerifyHashedPassword)
-- ============================================================================

USE master;
GO

IF DB_ID('RaceDayDB') IS NOT NULL
BEGIN
    ALTER DATABASE RaceDayDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE RaceDayDB;
END
GO

CREATE DATABASE RaceDayDB;
GO

USE RaceDayDB;
GO

-- 1. ORGANISER Table
CREATE TABLE ORGANISER (
    OrganiserID   INT IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(50)  NOT NULL,
    Surname       NVARCHAR(50)  NOT NULL,
    Email         NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash  NVARCHAR(255) NOT NULL
);
GO

-- 2. PARTICIPANT Table
CREATE TABLE PARTICIPANT (
    ParticipantID     INT IDENTITY(1,1) PRIMARY KEY,
    Name              NVARCHAR(50)  NOT NULL,
    Surname           NVARCHAR(50)  NOT NULL,
    DateOfBirth       DATE          NOT NULL,
    Email             NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash      NVARCHAR(255) NOT NULL,
    ProfilePictureUrl NVARCHAR(500) NULL
);
GO

-- 3. EVENT Table
CREATE TABLE EVENT (
    EventID        INT IDENTITY(1,1) PRIMARY KEY,
    OrganiserID    INT NOT NULL,
    EventName      NVARCHAR(100) NOT NULL,
    EventType      NVARCHAR(20)  NOT NULL CHECK (EventType IN ('Run', 'Walk', 'Cycle')),
    Description    NVARCHAR(MAX) NOT NULL,
    Distance       DECIMAL(5,2)  NOT NULL CHECK (Distance > 0),
    Location       NVARCHAR(150) NOT NULL,
    Date           DATETIME      NOT NULL,
    BannerImageUrl NVARCHAR(500) NULL,
    FOREIGN KEY (OrganiserID) REFERENCES ORGANISER(OrganiserID) ON DELETE CASCADE
);
GO

-- 4. CATEGORY Table
CREATE TABLE CATEGORY (
    CategoryID   INT IDENTITY(1,1) PRIMARY KEY,
    EventID      INT NOT NULL,
    CategoryName NVARCHAR(50)  NOT NULL,
    Description  NVARCHAR(255) NULL,
    FOREIGN KEY (EventID) REFERENCES EVENT(EventID) ON DELETE CASCADE,
    CONSTRAINT UQ_Category_Event_Name UNIQUE (EventID, CategoryName)
);
GO

-- 5. ENROLMENT Table
-- No cascade from EVENT/CATEGORY: an event or category that has enrolments
-- cannot be deleted (the API returns 409 Conflict).
CREATE TABLE ENROLMENT (
    EnrolmentID     INT IDENTITY(1,1) PRIMARY KEY,
    ParticipantID   INT NOT NULL,
    EventID         INT NOT NULL,
    CategoryID      INT NOT NULL,
    EnrolmentDate   DATETIME    NOT NULL DEFAULT GETDATE(),
    EnrolmentStatus NVARCHAR(20) NOT NULL DEFAULT 'Pending'
        CHECK (EnrolmentStatus IN ('Pending', 'Confirmed', 'Cancelled')),
    FOREIGN KEY (ParticipantID) REFERENCES PARTICIPANT(ParticipantID),
    FOREIGN KEY (EventID)       REFERENCES EVENT(EventID),
    FOREIGN KEY (CategoryID)    REFERENCES CATEGORY(CategoryID),
    CONSTRAINT UQ_Participant_Event UNIQUE (ParticipantID, EventID)
);
GO

-- 6. RESULT Table
CREATE TABLE RESULT (
    ResultID          INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentID       INT NOT NULL UNIQUE,
    FinishingTime     TIME(0) NOT NULL,
    FinishingPosition INT     NOT NULL CHECK (FinishingPosition > 0),
    IsPublished       BIT     NOT NULL DEFAULT 0,
    FOREIGN KEY (EnrolmentID) REFERENCES ENROLMENT(EnrolmentID) ON DELETE CASCADE
);
GO

-- ============================================================================
-- Seed Data  (all seeded accounts use the password: Password123!)

-- Organisers
INSERT INTO ORGANISER (Name, Surname, Email, PasswordHash) VALUES
(N'Sipho', N'Nkosi',         N'sipho.organiser@raceday.co.za', N'AQAAAAIAAYagAAAAEHteWz4ksP4txybT8iGIgb7tn2y0Sa+upK2cLKy+kaQK0TzfIVi8lUzgCVgNT4vYWA=='),
(N'Sarah', N'Van Der Merwe', N'sarah.organiser@raceday.co.za', N'AQAAAAIAAYagAAAAEEeyijaGQuInY0fRtHB/nDa+Mcm/BUHWdtSJ9pCQffUoyVDTYBsRntqlD3ysG3TV0Q==');

-- Participants
INSERT INTO PARTICIPANT (Name, Surname, DateOfBirth, Email, PasswordHash) VALUES
(N'Thabo',  N'Mokoena', '1995-04-12', N'thabo.runner@gmail.com',   N'AQAAAAIAAYagAAAAECnoqH122qetyQp2Tp7Ua8eEThmTZxHR67nySXpYHcUkW3E5Qctw4hJ9IOuqPVxa+w=='),
(N'Lize',   N'Botha',   '1998-09-23', N'lize.cyclist@gmail.com',   N'AQAAAAIAAYagAAAAECXg54V4NAZTWfhEcAoMofPsgRzp+Ab455BsJmlDdB+NAtUf6WAlKiB2/+CtLeSEtg=='),
(N'Naledi', N'Dlamini', '2007-02-14', N'naledi.walker@gmail.com',  N'AQAAAAIAAYagAAAAEAjP80U4HnhmGj6HT1QOPj+6IU8B58pCf4GE1KU0Ol81iGMT8codPdx7N3t3ANErFg==');

-- Events 
INSERT INTO EVENT (OrganiserID, EventName, EventType, Description, Distance, Location, Date) VALUES
(1, N'Joburg City Marathon',          'Run',   N'An annual road running challenge through Johannesburg city.', 42.20, N'Johannesburg', '2026-05-10 06:00:00'),
(1, N'Cape Coastal Walk',             'Walk',  N'A scenic family-friendly 10km coastal walk.',                 10.00, N'Cape Town',    '2026-06-15 08:00:00'),
(2, N'Durban Beach Cycling Challenge','Cycle', N'Fast and flat coastal road cycling race.',                    90.00, N'Durban',       '2026-07-20 07:00:00'),
(1, N'Soweto Spring Fun Run',         'Run',   N'A festive 10km run through the streets of Soweto.',           10.00, N'Soweto',       '2026-11-22 07:00:00'),
(2, N'Pretoria Park Walk',            'Walk',  N'A relaxed 5km community walk through the Pretoria parks.',     5.00, N'Pretoria',     '2026-12-05 08:00:00'),
(2, N'Midrand Road Cycle Classic',    'Cycle', N'A rolling 60km road cycling event on closed Midrand roads.',  60.00, N'Midrand',      '2027-02-14 06:30:00');

-- Categories for every event
INSERT INTO CATEGORY (EventID, CategoryName, Description) VALUES
(1, N'Open 42km Full Marathon', N'Full distance for senior runners'),          -- 1
(1, N'Half Marathon 21km',      N'Intermediate distance road race'),           -- 2
(2, N'Open Walk 10km',          N'General public 10km route'),                 -- 3
(2, N'Family Walk 5km',         N'Shorter family-friendly route'),             -- 4
(3, N'Pro 90km Cycle',          N'Advanced distance for elite cyclists'),      -- 5
(3, N'Fun 45km Cycle',          N'Half distance for social cyclists'),         -- 6
(4, N'Open 10km',               N'Open to all runners'),                       -- 7
(4, N'Under 20',                N'Runners aged 19 and younger'),               -- 8
(5, N'Open 5km Walk',           N'General public 5km route'),                  -- 9
(5, N'Seniors 60+',             N'Walkers aged 60 and older'),                 -- 10
(6, N'Elite 60km',              N'Competitive riders, full distance'),         -- 11
(6, N'Social 30km',             N'Half distance for social cyclists');         -- 12

-- Enrolments
INSERT INTO ENROLMENT (ParticipantID, EventID, CategoryID, EnrolmentDate, EnrolmentStatus) VALUES
(1, 1, 1,  '2026-03-01 10:30:00', 'Confirmed'),   -- 1 Thabo  - Joburg Marathon - Full
(2, 3, 5,  '2026-03-02 14:15:00', 'Confirmed'),   -- 2 Lize   - Durban Cycling  - Pro
(1, 2, 3,  '2026-04-10 09:00:00', 'Confirmed'),   -- 3 Thabo  - Cape Walk       - Open 10km
(3, 1, 2,  '2026-03-05 16:45:00', 'Confirmed'),   -- 4 Naledi - Joburg Marathon - Half
(1, 4, 7,  '2026-10-10 11:20:00', 'Pending'),     -- 5 Thabo  - Soweto Fun Run  - Open 10km
(3, 4, 8,  '2026-10-11 08:05:00', 'Confirmed'),   -- 6 Naledi - Soweto Fun Run  - Under 20
(2, 6, 12, '2026-10-12 19:30:00', 'Pending');     -- 7 Lize   - Midrand Classic - Social 30km

-- Results (for completed events only). Result 3 is captured but NOT yet published.
INSERT INTO RESULT (EnrolmentID, FinishingTime, FinishingPosition, IsPublished) VALUES
(1, '03:12:45', 47,  1),
(2, '02:40:10', 12,  1),
(3, '01:55:30', 88,  0),
(4, '01:38:20', 15,  1);
GO