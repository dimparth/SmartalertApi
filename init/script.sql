USE `smartalert`;
CREATE TABLE Users (
    UserId int NOT NULL AUTO_INCREMENT,
    Username VARCHAR(30) NOT NULL,
    Pass VARCHAR(50),
    Role VARCHAR(10),
    FcmToken VARCHAR(255),
    PRIMARY KEY (UserId),
    UNIQUE (Username)
);

CREATE TABLE DangerInformation (
    DangerId int NOT NULL AUTO_INCREMENT,
    Username VARCHAR(30) NOT NULL,
    Comments VARCHAR(100),
    Latitude DECIMAL(8,6),
    Longitude DECIMAL(9,6),
    DangerTimestamp DATETIME,
	ImagePath VARCHAR(255),
	DangerStatus VARCHAR(20),
	Category VARCHAR(20),
    Locality VARCHAR(50), 
    PRIMARY KEY (DangerId),
    UNIQUE (Username, Latitude, Longitude)
);