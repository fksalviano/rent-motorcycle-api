CREATE DATABASE rentmotorcycle;

\c rentmotorcycle;

CREATE TABLE Motorcycle
(
    Id UUID NOT NULL PRIMARY KEY,
    Year INT NOT NULL,
    Model VARCHAR(50) NOT NULL,
    Plate VARCHAR(10) NOT NULL UNIQUE
);

CREATE TABLE Customer
(
    Id UUID NOT NULL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    TaxId VARCHAR(20) NOT NULL UNIQUE,
    BornDate DATE NOT NULL,
    DriverLicenseNumber INT NOT NULL UNIQUE,
    DriverLicenseType VARCHAR(2) NOT NULL
);

CREATE TABLE Rent
(
    Id UUID NOT NULL PRIMARY KEY,
    CustomerId UUID NOT NULL REFERENCES Customer(Id),
    MotorcycleId UUID NOT NULL REFERENCES Motorcycle(Id),
    RentDays INT NOT NULL,
    RentValue DECIMAL NOT NULL,
    StartDate DATE NOT NULL,
    ExpectedEnd DATE NOT NULL,
    EndDate DATE NULL,
    EndValue DECIMAL NULL
);