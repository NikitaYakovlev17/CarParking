use car_parking_db;
CREATE TABLE Users
(
    UserId INT PRIMARY KEY identity(1,1),
    UserName NVARCHAR(20) unique,
    Password NVARCHAR(100),
	IsAdmin bit, 
)

CREATE TABLE UserCar
(
	IdUser INT constraint  User_FK FOREIGN KEY  REFERENCES Users(UserId) ,
	CarNumber INT , 
	CarRegion INT ,
	CarSeries NVARCHAR(2) ,
	LeaseTime DATETIME PRIMARY KEY,
	PhoneNumber INT , 
	Comment NVARCHAR(100)
)

CREATE TABLE LeaseTime
(
LeaseTime DATETIME constraint LeaseTime_FK FOREIGN KEY REFERENCES UserCar(LeaseTime) ,
Time_In DATETIME,
Time_Out AS DATEADD(hour,DatePaRT(hour,LeaseTime),(DATEADD(MINUTE,DATEPART(minute,LeaseTime),Time_In)))
)