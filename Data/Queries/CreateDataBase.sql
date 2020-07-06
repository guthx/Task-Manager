DROP DATABASE IF EXISTS TaskManagerDB;
CREATE DATABASE TaskManagerDB;
USE TaskManagerDB;
CREATE TABLE Users (
	Id INT PRIMARY KEY IDENTITY,
	Email VARCHAR(100) NOT NULL UNIQUE,
	Password BINARY(60) NOT NULL,
	First_Name NVARCHAR(100) NOT NULL,
	Last_Name NVARCHAR(100) NOT NULL
)

CREATE TABLE Tasks (
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(255) NOT NULL,
	Description NVARCHAR(1000),
	Notes NVARCHAR(1000),
	Is_Active BIT NOT NULL DEFAULT 1,
	Parent_Id INT,
	CONSTRAINT fk_Tasks_Parent
		FOREIGN KEY (Parent_Id)
		REFERENCES Tasks(Id)
)

CREATE TABLE Privileges (
	Id INT PRIMARY KEY IDENTITY,
	Name VARCHAR(20) NOT NULL,
	Description NVARCHAR(100)
)

CREATE TABLE Users_Tasks (
	User_Id INT NOT NULL,
	Task_Id INT NOT NULL,
	Privilege_Id INT NOT NULL,
	CONSTRAINT pk_users_tasks
		PRIMARY KEY (User_Id, Task_Id),
	CONSTRAINT fk_Users_Tasks_Privilege
		FOREIGN KEY (Privilege_Id)
		REFERENCES Privileges(Id)
)

CREATE TABLE Requests (
	Id INT PRIMARY KEY IDENTITY,
	Sender_Id INT NOT NULL,
	Receiver_Id INT NOT NULL,
	Task_Id INT NOT NULL,
	CONSTRAINT fk_requests_sender_id
		FOREIGN KEY (Sender_Id)
		REFERENCES Users(Id),
	CONSTRAINT fk_requests_receiver_id
		FOREIGN KEY (Receiver_Id)
		REFERENCES Users(Id),
	CONSTRAINT fk_requests_task_id
		FOREIGN KEY (Task_Id)
		REFERENCES Tasks(Id)
)

CREATE TABLE Notifications (
	Id INT PRIMARY KEY IDENTITY,
	Task_Id INT,
	Request_Id INT,
	Is_Viewed BIT NOT NULL DEFAULT 0,
	CONSTRAINT Notifications_Mut_Ex CHECK (Task_Id is NULL or Request_Id is NULL),
	CONSTRAINT fk_notifications_task_id
		FOREIGN KEY (Task_Id)
		REFERENCES Tasks(Id),
	CONSTRAINT fk_notifications_request_id
		FOREIGN KEY (Request_Id)
		REFERENCES Requests(Id)
)

INSERT INTO Privileges
VALUES ('Full', 'Allows for all actions on a task, including deletion');
INSERT INTO Privileges
VALUES ('Moderator', 'Allows for all actions except deletion');
INSERT INTO Privileges
VALUES ('Limited', 'Allows only for marking tasks as done and adding notes');
INSERT INTO Privileges
VALUES ('View-only', 'Allows only view of the tasks');