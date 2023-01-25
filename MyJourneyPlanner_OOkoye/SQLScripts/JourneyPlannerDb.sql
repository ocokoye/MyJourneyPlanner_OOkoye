CREATE DATABASE journeyplannerdb;

USE journeyplannerdb;
CREATE TABLE Line (
LineId int NOT NULL PRIMARY KEY  IDENTITY(1,1),
LineName NVARCHAR(50) NOT NULL,
LineColor NVARCHAR(50)
);

INSERT INTO Line (LineName, LineColor) VALUES ('Central Line',  'Red'); 
INSERT INTO Line (LineName, LineColor) VALUES ('Circle Line', 'Yellow');
INSERT INTO Line (LineName, LineColor) VALUES('District Line', 'Green');
INSERT INTO Line (LineName,LineColor) VALUES ('Bakerloo Line', 'Brown');
INSERT INTO Line (LineName,LineColor) VALUES ('Northern Line', 'Black');
INSERT INTO Line (LineName, LineColor) VALUES ('Piccadilly Line', 'Red');
INSERT INTO Line (LineName, LineColor) VALUES ('Waterloo and City Line', '');
INSERT INTO Line (LineName, LineColor) VALUES ('Dockland Light Railway Line', '');

CREATE TABLE Station (
StationId int NOT NULL PRIMARY KEY  IDENTITY(1,1),
StationName NVARCHAR(50) NOT NULL
);

INSERT INTO Station (StationName) VALUES ('Chancery Lane'); 
INSERT INTO Station (StationName) VALUES ('St Paul s');
INSERT INTO Station (StationName) VALUES('Bank');
INSERT INTO Station (StationName) VALUES ('Monument');
INSERT INTO Station (StationName) VALUES ('Cannon Street');
INSERT INTO Station (StationName) VALUES ('Mansion House');
INSERT INTO Station (StationName) VALUES ('Blackfriars');
INSERT INTO Station (StationName) VALUES ('Temple');
INSERT INTO Station (StationName) VALUES ('Embankment');
INSERT INTO Station (StationName) VALUES ('Charing Cross');
INSERT INTO Station (StationName) VALUES ('Leicester Square');
INSERT INTO Station (StationName) VALUES ('Covent Garden');
INSERT INTO Station (StationName) VALUES ('Holborn');
INSERT INTO Station (StationName) VALUES ('Tottenham Court Road');

CREATE TABLE LineStation (
LineStationId int NOT NULL PRIMARY KEY  IDENTITY(1,1),
LineId int NOT NULL,
StationId int NOT NULL
);

INSERT INTO LineStation (LineId, StationId) VALUES (1, 1); 
INSERT INTO LineStation (LineId, StationId) VALUES (1, 2);
INSERT INTO LineStation (LineId, StationId) VALUES(1, 3);
INSERT INTO LineStation (LineId, StationId) VALUES (5, 3);
INSERT INTO LineStation (LineId, StationId) VALUES (7, 3);
INSERT INTO LineStation (LineId, StationId) VALUES (2, 4);
INSERT INTO LineStation (LineId, StationId) VALUES (3, 4);
INSERT INTO LineStation (LineId, StationId) VALUES (2, 5);
INSERT INTO LineStation (LineId, StationId) VALUES (3, 5);
INSERT INTO LineStation (LineId, StationId) VALUES (2, 6);
INSERT INTO LineStation (LineId, StationId) VALUES (3, 6);
INSERT INTO LineStation (LineId, StationId) VALUES (2, 7);
INSERT INTO LineStation (LineId, StationId) VALUES (3, 7);
INSERT INTO LineStation (LineId, StationId) VALUES (2, 8);
INSERT INTO LineStation (LineId, StationId) VALUES (3, 8);
INSERT INTO LineStation (LineId, StationId) VALUES (2, 9);
INSERT INTO LineStation (LineId, StationId) VALUES (3, 9);
INSERT INTO LineStation (LineId, StationId) VALUES (4, 9);
INSERT INTO LineStation (LineId, StationId) VALUES (5, 9);
INSERT INTO LineStation (LineId, StationId) VALUES (4, 10);
INSERT INTO LineStation (LineId, StationId) VALUES (5, 10);
INSERT INTO LineStation (LineId, StationId) VALUES (5, 11);
INSERT INTO LineStation (LineId, StationId) VALUES (6, 11);
INSERT INTO LineStation (LineId, StationId) VALUES (6, 12);
INSERT INTO LineStation (LineId, StationId) VALUES (1, 13);
INSERT INTO LineStation (LineId, StationId) VALUES (6, 13);
INSERT INTO LineStation (LineId, StationId) VALUES (1, 14);
INSERT INTO LineStation (LineId, StationId) VALUES (5, 14);

CREATE TABLE [dbo].[StationPair](
	[StationPairId] [int] IDENTITY(1,1) NOT NULL,
	[StartStationId] [int] NOT NULL,

	[DestinatioinStationId] [int] NOT NULL,
	[TimeSpent] Decimal(8,2) ,

	[Distance] Decimal(8,2) ,

PRIMARY KEY CLUSTERED 
(
	[StationPairId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [journeyplannerdb]
GO

INSERT INTO [dbo].[StationPair]
           ([StartStationId]
           ,[DestinatioinStationId]
          )
     VALUES
           (1,2),(2,3),(3,4),(4,5),(5,6),(6,7),(7,8),(8,9),
		     (9,10),(10,11),(11,12),(12,13),(13,1),(13,14),(11,14),(14,13),
		   (1,13),(13,12),(12,11),(14,11),(11,10),(10,9),(9,8),(8,7),
		   (7,6),(6,5),(5,4),(4,3),(3,2),(2,1)
GO