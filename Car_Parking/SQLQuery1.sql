use car_parking_db
CREATE TABLE Users
(
    UserId INT PRIMARY KEY identity(1,1),
    PhoneNumberLog NVARCHAR(17) unique,
    Password NVARCHAR(100),
	IsAdmin bit, 
)

CREATE TABLE ParkingSpace
(
	SpaceType NVARCHAR(10) PRIMARY KEY,
	PayAmount NVARCHAR(5)
)

CREATE TABLE UserCar
(
	IdUser INT constraint  User_FK FOREIGN KEY  REFERENCES Users(UserId) ,
	CarNumber NVARCHAR(4) unique, 
	CarRegion INT ,
	CarSeries NVARCHAR(2) ,
	LeaseTime DATETIME PRIMARY KEY,
	PhoneNumber NVARCHAR(17) unique, 
	SpaceType NVARCHAR(10) constraint  SpaceType_FK FOREIGN KEY  REFERENCES ParkingSpace(SpaceType)
)

CREATE TABLE CarsHistory
(
	CarNumber NVARCHAR(4) unique, 
	CarRegion INT ,
	CarSeries NVARCHAR(2) ,
	LeaseTime DATETIME PRIMARY KEY,
	PhoneNumber NVARCHAR(17) unique, 
	SpaceType NVARCHAR(10) FOREIGN KEY  REFERENCES ParkingSpace(SpaceType),
	TimeOut DATETIME,
	PayAmount NVARCHAR(5) 
)