use car_parking_db
CREATE TABLE Users
(
    UserId INT PRIMARY KEY identity(1,1),
    PhoneNumberLog NVARCHAR(17) unique,
    Password NVARCHAR(100),
	IsAdmin bit, 
)

CREATE TABLE UserCar
(
	IdUser INT constraint  User_FK FOREIGN KEY  REFERENCES Users(UserId) ,
	CarNumber NVARCHAR(4) unique, 
	CarRegion INT ,
	CarSeries NVARCHAR(2) ,
	LeaseTime DATETIME PRIMARY KEY,
	PhoneNumber NVARCHAR(17) unique, 
	Comment NVARCHAR(100) NULL
)

CREATE TABLE CarsHistory
(
	CarNumber NVARCHAR(4) unique, 
	CarRegion INT ,
	CarSeries NVARCHAR(2) ,
	LeaseTime DATETIME PRIMARY KEY,
	PhoneNumber NVARCHAR(17) unique, 
	Comment NVARCHAR(100) NULL, 
	TimeOut DATETIME,
	PayAmount NVARCHAR(5)
)