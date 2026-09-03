-- RaceDay System Database Creation & Schema Script
-- Matching provided ERD layout: Separate PARTICIPANT & ORGANISER tables
-- ============================================================================
CREATE DATABASE RaceDayDB;
GO

USE RaceDayDB;
GO

-- 1. ORGANISER Table
CREATE TABLE ORGANISER (
    OrganiserID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Surname VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE
);
GO

-- 2. PARTICIPANT Table
CREATE TABLE PARTICIPANT (
    ParticipantID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Surname VARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE
);
GO

-- 3. EVENT Table
CREATE TABLE EVENT (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    OrganiserID INT NOT NULL,
    EventName VARCHAR(100) NOT NULL,
    EventType VARCHAR(20) NOT NULL CHECK (EventType IN ('Run', 'Walk', 'Cycle')),
    Description VARCHAR(MAX) NOT NULL,
    Distance DECIMAL(5,2) NOT NULL,
    Location VARCHAR(150) NOT NULL,
    Date DATETIME NOT NULL,
    FOREIGN KEY (OrganiserID) REFERENCES ORGANISER(OrganiserID) ON DELETE CASCADE
);
GO

-- 4. CATEGORY Table
CREATE TABLE CATEGORY (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT NOT NULL,
    CategoryName VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NULL,
    FOREIGN KEY (EventID) REFERENCES EVENT(EventID) ON DELETE CASCADE
);
GO

-- 5. ENROLMENT Table
CREATE TABLE ENROLMENT (
    EnrolmentID INT IDENTITY(1,1) PRIMARY KEY,
    ParticipantID INT NOT NULL,
    EventID INT NOT NULL,
    CategoryID INT NOT NULL,
    EnrolmentDate DATETIME NOT NULL DEFAULT GETDATE(),
    EnrolmentStatus VARCHAR(20) NOT NULL DEFAULT 'Confirmed' CHECK (EnrolmentStatus IN ('Pending', 'Confirmed', 'Cancelled')),
    FOREIGN KEY (ParticipantID) REFERENCES PARTICIPANT(ParticipantID),
    FOREIGN KEY (EventID) REFERENCES EVENT(EventID),
    FOREIGN KEY (CategoryID) REFERENCES CATEGORY(CategoryID),
    CONSTRAINT UQ_Participant_Event UNIQUE (ParticipantID, EventID)
);
GO

-- 6. RESULT Table
CREATE TABLE RESULT (
    ResultID INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentID INT NOT NULL UNIQUE,
    FinishingTime TIME(0) NOT NULL,
    FinishingPosition INT NOT NULL,
    FOREIGN KEY (EnrolmentID) REFERENCES ENROLMENT(EnrolmentID) ON DELETE CASCADE
);
GO

-- ============================================================================
-- Seed Data Insertion (Mandatory minimum required by Part 1 Section C)
-- ============================================================================

-- Seed Organisers (Minimum 2)
INSERT INTO ORGANISER (Name, Surname, Email) VALUES
('Sipho', 'Nkosi', 'sipho.organiser@raceday.co.za'),
('Sarah', 'Van Der Merwe', 'sarah.organiser@raceday.co.za');

-- Seed Participants (Minimum 2)
INSERT INTO PARTICIPANT (Name, Surname, DateOfBirth, Email) VALUES
('Thabo', 'Mokoena', '1995-04-12', 'thabo.runner@gmail.com'),
('Lize', 'Botha', '1998-09-23', 'lize.cyclist@gmail.com');

-- Seed Events (Minimum 3)
INSERT INTO EVENT (OrganiserID, EventName, EventType, Description, Distance, Location, Date) VALUES
(1, 'Joburg City Marathon', 'Run', 'An annual road running challenge through Johannesburg city.', 42.20, 'Johannesburg', '2026-05-10 06:00:00'),
(1, 'Cape Coastal Walk', 'Walk', 'A scenic family-friendly 10km coastal walk.', 10.00, 'Cape Town', '2026-06-15 08:00:00'),
(2, 'Durban Beach Cycling Challenge', 'Cycle', 'Fast and flat coastal road cycling race.', 90.00, 'Durban', '2026-07-20 07:00:00');

-- Seed Categories for each event
INSERT INTO CATEGORY (EventID, CategoryName, Description) VALUES
(1, 'Open 42km Full Marathon', 'Full distance for senior runners'),
(1, 'Half Marathon 21km', 'Intermediate distance road race'),
(2, 'Open Walk 10km', 'General public 10km route'),
(3, 'Pro 90km Cycle', 'Advanced distance for elite cyclists'),
(3, 'Fun 45km Cycle', 'Half distance for social cyclists');

-- Seed Sample Enrolments
INSERT INTO ENROLMENT (ParticipantID, EventID, CategoryID, EnrolmentDate, EnrolmentStatus) VALUES
(1, 1, 1, '2026-03-01 10:30:00', 'Confirmed'),
(2, 3, 4, '2026-03-02 14:15:00', 'Confirmed');

-- Seed Sample Results
INSERT INTO RESULT (EnrolmentID, FinishingTime, FinishingPosition) VALUES
(1, '03:12:45', 47),
(2, '02:40:10', 12);
GO
