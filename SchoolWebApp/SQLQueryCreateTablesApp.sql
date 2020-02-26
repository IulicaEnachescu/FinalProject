USE [SchoolApp]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- Create the User table.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[User]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User]([Id] [int] IDENTITY(1,1) NOT NULL,
[UserName] [varchar](50) NOT NULL,
[LastName] [varchar](50) NOT NULL,
[FirstName] [varchar](50) NOT NULL,
[Password] [varchar](10) NOT NULL,
[DateBirth][date] NOT NULL,
[Category][varchar](8) CHECK (Category IN('Admin', 'Student', 'Teacher')) NOT NULL,
[CreateDate][date] NOT NULL,
[City][varchar](20) NOT NULL,
[Adress][varchar](200) NOT NULL,
[Phone][varchar](20) NOT NULL,
[Email][varchar](30) NOT NULL,
CONSTRAINT 
User_unique UNIQUE ([UserName]),
CONSTRAINT 
[PK_User] PRIMARY KEY CLUSTERED ([Id])
)
END

GO


--Create Admin table.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Admin]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Admin]([Id] [int] IDENTITY(1,1) NOT NULL,
[UserId] [int] NOT NULL,
[Role] [nvarchar](20) NOT NULL,

CONSTRAINT 
[PK_Admin] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_Admin_User FOREIGN KEY (UserId)
        REFERENCES dbo.[User](Id)
)
END

GO
-- Create Contact table for referece Student to a person named for contact in some cases or for children.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[ContactPerson]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactPerson]([Id] [int] IDENTITY(1,1) NOT NULL,
[Name] [varchar](50) NOT NULL,
[Adress][varchar](200) NOT NULL,
[Phone][varchar](20) NOT NULL,
[Email][varchar](30) NOT NULL,
CONSTRAINT 
[PK_ContactPerson] PRIMARY KEY CLUSTERED ([Id])
)

END
GO
-- Create Student table.

IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Student]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Student]([Id] [int] IDENTITY(1,1) NOT NULL,
[UserId] [int] NOT NULL,
[StatusActive][bit] NOT NULL,
[ContactId][int],

CONSTRAINT 
[PK_Student] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_Student_User FOREIGN KEY (UserId)
        REFERENCES dbo.[User](Id)
,CONSTRAINT FK_Student_ContactPerson FOREIGN KEY (ContactId)
        REFERENCES dbo.[ContactPerson](Id)
)

END
GO


--Create Teacher table
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Teacher]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Teacher]([Id] [int] IDENTITY(1,1) NOT NULL,
[UserId] [int] NOT NULL,
[RoleDescription][varchar](50) NOT NULL,
[StatusActive][bit] NOT NULL,
CONSTRAINT 
[PK_Teacher] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_Teacher_User FOREIGN KEY (UserId)
        REFERENCES dbo.[User](Id)
)
END

--create Course Table
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Course]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Course]([Id] [int] IDENTITY(1,1) NOT NULL,

[NumberOfLessons][int] NOT NULL,
[Description][varchar](300) NOT NULL,
[Category][varchar](8) CHECK (Category IN('Children', 'Adults', 'Teens')) NOT NULL,
[Language][varchar](8) CHECK (Language IN ('English', 'French', 'German' )) NOT NULL,
[Level][varchar](20) CHECK 
(Level IN ('A1Beginer','A2Elementary','B1Intermediate','B2UpperIntermediate','B2PreAdvance','C1Advance','C2Profiency')) NOT NULL,
[StatusActive][bit] NOT NULL,

CONSTRAINT 
[PK_Course] PRIMARY KEY CLUSTERED ([Id])
)
END



--Create Class table
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Class]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Class]([Id] [int] IDENTITY(1,1) NOT NULL,
[CourseId] [int] NOT NULL,
[TeacherId][int] NOT NULL,
[Name][varchar](20) NOT NULL,
[ClassDescription][varchar](300) NOT NULL,
[StartDate][date] NOT NULL,
[EndDate] [date] NOT NULL,
[Price][decimal](5,2) NOT NULL,

CONSTRAINT 
[PK_Class] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_Class_Course FOREIGN KEY (CourseId)
        REFERENCES dbo.[Course](Id)
,CONSTRAINT FK_Class_Teacher FOREIGN KEY (TeacherId)
        REFERENCES dbo.[Teacher](Id)	
)
END
GO

--Create StudentClass table
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[ClassStudent]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ClassStudent]([Id] [int] IDENTITY(1,1) NOT NULL,

[ClassId][int] NOT NULL,
[StudentId][int] NOT NULL,
CONSTRAINT 
[PK_ClassStudent] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_ClassStudent_Class FOREIGN KEY (ClassId)
        REFERENCES dbo.[Class](Id)
,CONSTRAINT FK_ClassStudent_Student FOREIGN KEY (StudentId)
        REFERENCES dbo.[Student](Id)
)
END
GO
-- Create classtimetable table for each session of the class
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[ClassTimetable]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ClassTimetable]([Id] [int] IDENTITY(1,1) NOT NULL,

[ClassId][int] NOT NULL,
[LessonNumber][int] NOT NULL,
[ClassDate][date] NOT NULL,

CONSTRAINT 
[PK_ClassTimetable] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_ClassTimetable_Class FOREIGN KEY (ClassID)
        REFERENCES dbo.[Class](Id)	
)
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[ClassMessages]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ClassMessages]([Id] [int] IDENTITY(1,1) NOT NULL,

[ClassId][int] NOT NULL,
[Date][date] NOT NULL,
[Message][varchar](500) NOT NULL,

CONSTRAINT 
[PK_ClassMessages] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_ClassMessages_Class FOREIGN KEY (ClassID)
        REFERENCES dbo.[Class](Id)	
)
END
GO


--create ClassStudentPresence table
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[StudentPresence]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudentPresence]([Id] [int] IDENTITY(1,1) NOT NULL,

[ClassTimetableId][int] NOT NULL,
[StudentId][int] NOT NULL,
[Presence][bit] NOT NULL,
CONSTRAINT 
[PK_StudentPresence] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_StudentPresence_ClassSTimetable FOREIGN KEY (ClassTimetableId)
        REFERENCES dbo.[ClassTimetable](Id)
,CONSTRAINT FK_StudentPresence_Student FOREIGN KEY (StudentId)
        REFERENCES dbo.[Student](Id)	
)
END
GO


--create ClassStudentEvaluation table
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[StudentClassEvaluation]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudentClassEvaluation]([Id] [int] IDENTITY(1,1) NOT NULL,

[ClassId][int] NOT NULL,
[StudentId][int] NOT NULL,
[Description][varchar](500) NOT NULL,
[Grade][int] NOT NULL,
CONSTRAINT 
[PK_StudentClassEvaluation] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_StudentClassEvaluation_Class FOREIGN KEY (ClassId)
        REFERENCES dbo.[Class](Id)
,CONSTRAINT FK_StudentClassEvaluation_Student FOREIGN KEY (StudentId)
        REFERENCES dbo.[Student](Id)
)
END
GO

--create student payment class tabel
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[StudentPayment]')
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudentPayment]([Id] [int] IDENTITY(1,1) NOT NULL,

[StudentId][int] NOT NULL,
[ClassId] [int] NOT NULL,
[PaymentDate] [date] NOT NULL,
[Ammount][decimal](5,2) NOT NULL,

CONSTRAINT 
[PK_StudentPayment] PRIMARY KEY CLUSTERED ([Id])
,CONSTRAINT FK_SStudentPayment_Class FOREIGN KEY (ClassID)
        REFERENCES dbo.[Class](Id)
,CONSTRAINT FK_StudentPayment_Student FOREIGN KEY (StudentId)
        REFERENCES dbo.[Student](Id)
)
END
GO