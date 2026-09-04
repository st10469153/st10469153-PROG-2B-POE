CREATE VIEW vw_RaceDayOverview
AS
SELECT
    -- Participant
    p.ParticipantID,
    p.Name AS ParticipantName,
    p.Surname AS ParticipantSurname,
    p.DateOfBirth,
    p.Email AS ParticipantEmail,

    -- Enrolment
    en.EnrolmentID,
    en.EnrolmentDate,
    en.EnrolmentStatus,

    -- Event
    e.EventID,
    e.EventName,
    e.EventType,
    e.Description AS EventDescription,
    e.Distance,
    e.Location,
    e.Date AS EventDate,

    -- Organiser
    o.OrganiserID,
    o.Name AS OrganiserName,
    o.Surname AS OrganiserSurname,
    o.Email AS OrganiserEmail,

    -- Category
    c.CategoryID,
    c.CategoryName,
    c.Description AS CategoryDescription,

    -- Result
    r.ResultID,
    r.FinishingTime,
    r.FinishingPosition

FROM Participant p

LEFT JOIN Enrolment en
    ON p.ParticipantID = en.ParticipantID

LEFT JOIN Event e
    ON en.EventID = e.EventID

LEFT JOIN Organiser o
    ON e.OrganiserID = o.OrganiserID

LEFT JOIN Category c
    ON en.CategoryID = c.CategoryID

LEFT JOIN Result r
    ON en.EnrolmentID = r.EnrolmentID;
GO

SELECT * 
FROM vw_RaceDayOverview;