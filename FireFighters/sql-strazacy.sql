-- Created by Vertabelo (http://vertabelo.com)

-- tables
-- Table: Action
CREATE TABLE FireAction (
    IdFireAction int  NOT NULL IDENTITY,
    StartTime datetime  NOT NULL,
    EndTime datetime  NULL,
    NeedSpecialEquipment bit  NOT NULL,
    CONSTRAINT Action_pk PRIMARY KEY  (IdAction)
);

-- Table: FireTruck
CREATE TABLE FireTruck (
    IdFiretruck int  NOT NULL IDENTITY,
    OperationNumber nvarchar(10)  NOT NULL,
    SpecialEquipment bit  NOT NULL,
    CONSTRAINT FireTruck_pk PRIMARY KEY  (IdFiretruck)
);

-- Table: FireTruck_Action
CREATE TABLE FireTruck_Action (
    IdFiretruck int  NOT NULL,
    IdFireAction int  NOT NULL,
    AssignmentDate datetime  NOT NULL,
    CONSTRAINT FireTruck_Action_pk PRIMARY KEY  (IdFiretruck,IdAction)
);

-- Table: Firefighter
CREATE TABLE Firefighter (
    IdFirefighter int  NOT NULL IDENTITY,
    FirstName nvarchar(30)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    CONSTRAINT Firefighter_pk PRIMARY KEY  (IdFirefighter)
);

-- Table: Firefighter_Action
CREATE TABLE Firefighter_Action (
    IdAction int  NOT NULL,
    IdFirefighter int  NOT NULL,
    CONSTRAINT Firefighter_Action_pk PRIMARY KEY  (IdFirefighter,IdAction)
);

-- foreign keys
-- Reference: Firefighter_Action_Action (table: Firefighter_Action)
ALTER TABLE Firefighter_Action ADD CONSTRAINT Firefighter_Action_Action
    FOREIGN KEY (IdAction)
    REFERENCES Action (IdAction);

-- Reference: Firefighter_Action_Firefighter (table: Firefighter_Action)
ALTER TABLE Firefighter_Action ADD CONSTRAINT Firefighter_Action_Firefighter
    FOREIGN KEY (IdFirefighter)
    REFERENCES Firefighter (IdFirefighter);

-- Reference: Table_4_Action (table: FireTruck_Action)
ALTER TABLE FireTruck_Action ADD CONSTRAINT Table_4_Action
    FOREIGN KEY (IdAction)
    REFERENCES Action (IdAction);

-- Reference: Table_4_FireTruck (table: FireTruck_Action)
ALTER TABLE FireTruck_Action ADD CONSTRAINT Table_4_FireTruck
    FOREIGN KEY (IdFiretruck)
    REFERENCES FireTruck (IdFiretruck);

-- End of file.

INSERT INTO Firefighter VALUES ('Jan', 'Kowalski'),('Anna','Nowak');
INSERT INTO FireTruck VALUES ('AAA11221', 1), ('BB002', 0);
INSERT INTO FireAction VALUES (GETDATE(), GETDATE(), 0), (GETDATE(), null, 1);
INSERT INTO FireTruck_Action VALUES (1,2,GETDATE()), (2,1,GETDATE());
INSERT INTO Firefighter_Action VALUES (1,1),(1,2),(2,1);