CREATE TABLE [dbo].[ParkingSpot]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ParkingHouseId] INT NOT NULL, 
    [ParkingSpotNumber] INT NULL, 
    [TotalSpace] INT NULL, 
    [AvailableSpace] INT NULL, 
    CONSTRAINT [FK_ParkingSpot_ToParkingHouse] FOREIGN KEY ([Id]) REFERENCES [ParkingHouse](Id)
)
