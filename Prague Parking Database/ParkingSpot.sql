CREATE TABLE [dbo].[ParkingSpot]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ParkingHouseId] INT NOT NULL, 
    [AvailableSpace] INT NULL, 
    [TotalSpace] INT NULL, 
    CONSTRAINT [FK_ParkingSpot_ToParkingHouse] FOREIGN KEY ([ParkingHouseId])
    REFERENCES [ParkingHouse]([Id]) 
)
