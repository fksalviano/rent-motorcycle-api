CREATE DATABASE rentmotorcycle;

\c rentmotorcycle;

CREATE TABLE Motorcycle 
(
    Id UUID NOT NULL,
    Year INT NOT NULL,
    Model VARCHAR(50) NOT NULL,
    Plate VARCHAR(10) NOT NULL,
    CONSTRAINT PK_Motorcycle PRIMARY KEY (Id)
);
