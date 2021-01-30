CREATE TABLE [dbo].[Vehicle]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ParkingSpotId] INT NOT NULL, 
    [RegPlate] NVARCHAR(50) NULL, 
    [Size] INT NULL, 
    [ArriveTime] DATETIME NULL, 
    CONSTRAINT [FK_Vehicle_ToParkingSpot] FOREIGN KEY ([ParkingSpotId])
    REFERENCES [ParkingSpot]([Id])
)
