CREATE DATABASE rentmotorcycle;

\c rentmotorcycle;

CREATE TABLE Motorcycle
(
    Id UUID NOT NULL PRIMARY KEY,
    Year INT NOT NULL,
    Model VARCHAR(50) NOT NULL,
    Plate VARCHAR(10) NOT NULL UNIQUE
);

CREATE TABLE MotorcycleNotify
(
    MotorcycleId UUID NOT NULL PRIMARY KEY,
    CreatedAt DATE NOT NULL
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


/* DATA USED TO TEST */

INSERT INTO Motorcycle VALUES ('d75f49f1-1ab5-49e7-ac7d-2b6b2af23809'::uuid, 2020, 'Test', 'AAA9999');

INSERT INTO Customer VALUES ('fd95f0a4-54f5-492e-8eb6-32a871527ef7'::uuid, 'Teste', '11111111', CURRENT_DATE, 22222222, 'AB');

INSERT INTO Rent VALUES ('0d6c2e28-e6fa-4468-9f0b-0eaf1c3e9a60'::uuid, 'fd95f0a4-54f5-492e-8eb6-32a871527ef7'::uuid, 'd75f49f1-1ab5-49e7-ac7d-2b6b2af23809'::uuid, 
    7, 210, CURRENT_DATE, CURRENT_DATE + 7, NULL, NULL);