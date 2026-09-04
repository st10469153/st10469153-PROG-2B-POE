<img width="1196" height="119" alt="successfulBuildScreenshot" src="https://github.com/user-attachments/assets/a5e84a8b-4eb8-4138-a8f2-fe7ee8d457a1" /># Race Day
# RaceDay – Part 1: System Planning and Database Design

## 📌 Project Overview

RaceDay is a planned full-stack web-based event management system designed for the South African road running, walking, and cycling community.

The system will allow **Organizers** to create and manage sporting events, define event categories, manage participant enrollments, and capture event results.

**Participants** will be able to register for an account, browse available events, enroll in events by selecting a category, and view their personal event results.

This repository currently contains the work completed for **Part 1: System Planning and Database Design**.

---

## 🎯 Part 1 Objectives

The purpose of Part 1 is to plan the RaceDay system before API development begins.

The following deliverables are included:

* Entity Relationship Diagram (ERD)
* API Endpoint Plan
* SQL Database Script
* GitHub repository structure
* GitHub Actions CI/CD workflow

No API code is required in Part 1.

---

## 👥 System Roles

The RaceDay system supports two main user roles.

### 🧑‍💼 Organiser

An Organiser is responsible for managing sporting events.

An Organiser can:

* Create events
* Edit events
* Delete events
* Create and manage event categories
* View participant enrolments for their events
* Capture participant results
* View events they have created

---

### 🏃 Participant

A Participant is a user who enters and participates in sporting events.

A Participant can:

* Create an account
* Browse available events
* View event details
* Select an event category
* Enrol in an event
* View their enrolments
* Track their personal event results
* Update their profile information

---

# 🗂️ Database Design

The RaceDay relational database consists of the following entities:

1. **Participant**
2. **Organizer**
3. **Event**
4. **Category**
5. **Enrolment**
6. **Result**

## Entity Relationships

The main relationships in the database are:

* One **Organizer** can create many **Events**.
* One **Event** can have many **Categories**.
* One **Participant** can have many **Enrollments**.
* One **Event** can have many **Enrollments**.
* One **Category** can be selected in many **Enrollments**.
* One **Enrolment** can have zero or one **Result**.

The **Enrolment** entity acts as a linking entity between a Participant, Event, and Category.

---

# 📁 Project Structure

```text
RaceDay/
│
├── docs/
│   ├── RaceDay_ERD.pdf
│   ├── API_Endpoint_Plan.pdf
│   └── RaceDay_Database.sql
│
├── .github/
│   └── workflows/
│       └── validation.yml
│
└── README.md
```

> Disclaimer: file names may change depending on the final names used in the repository.

---

# 📊 Entity Relationship Diagram

The ERD represents the complete relational database structure for the RaceDay system.

It includes:

* All database entities
* Entity attributes
* Primary Keys (PK)
* Foreign Keys (FK)
* Relationships between entities
* Relationship cardinalities

The ERD can be found in the `/docs` folder.

---

# 🌐 API Endpoint Plan

The API Endpoint Plan defines the endpoints that will be implemented in Part 2.

The plan includes endpoints for:

* Authentication
* User profiles
* Events
* Categories
* Event enrollments
* Results

Each endpoint includes:

* HTTP Method
* Route
* Description
* Role Required
* Request Body
* Expected Response

The API Endpoint Plan can be found in the `/docs` folder.

---

# 🗄️ SQL Database Script

The SQL script creates the RaceDay database structure using Microsoft SQL Server.

The script will include:

* `CREATE TABLE` statements
* Primary Keys
* Foreign Keys
* Database constraints
* Sample data for Organizers
* Sample data for Participants
* Sample Events
* Event Categories
* Sample Enrollments

The SQL script can be run using **SQL Server Management Studio (SSMS)** or any other related DBMS software.

The script is located in the `/docs` folder.

---

# 🛠️ Technologies and Tools

The following tools are used during Part 1:

* **Microsoft SQL Server**
* **SQL Server Management Studio (SSMS)**
* **Draw.io** for diagrams
* **GitHub**
* **GitHub Actions**
* **Markdown**

---

# 🔄 CI/CD

GitHub Actions is used to validate the repository structure.

The CI/CD workflow checks that the required project documentation is included in the repository.

The workflow is located in:

```text
.github/workflows/
```

A screenshot of a successful GitHub Actions build will be included below.

## CI/CD Build Screenshot

<img width="1196" height="119" alt="successfulBuildScreenshot" src="https://github.com/user-attachments/assets/fa240b1d-0ba5-4fe7-b47f-58bd1b0241bb" />
---

# 🚀 Future Development

The RaceDay project will be developed progressively across the remaining parts of the Portfolio of Evidence.
--- 

# 📹 Video Presentation

A video presentation demonstrating and explaining the Part 1 planning documents will be available at the following link:

**YouTube Link:**
[Insert unlisted YouTube video link here]

The video will cover:

* The RaceDay system concept
* The Entity Relationship Diagram
* Database design decisions
* API Endpoint Plan
* SQL Database Script
* A live demonstration of the SQL script running in SSMS
