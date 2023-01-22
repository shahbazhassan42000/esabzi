-- Create the database E-Sabzi if it does not exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ESABZI')
CREATE DATABASE ESABZI;
GO

-- Switch to the E-Sabzi database
USE ESABZI;
GO

-- Create the user table if it does not exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'user' AND type = 'U')
CREATE TABLE [user] (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    contact_no NVARCHAR(255) NOT NULL,
    username NVARCHAR(255) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    picture NVARCHAR(255) NOT NULL default 'https://i.ibb.co/cT5mM2Z/profile-img.png',
)
GO

-- Insert the admin user
INSERT INTO [user] (name, email, contact_no, username, password, address)
VALUES ('Shahbaz','shahbazhassan42000@gmail.com','+923354058294','shahbaz','$2a$11$Z1z0YAwuK/Iln4i2c/o93.vvBfvvx6wicVkcS9a1XFDMM4w/Inw9e','https://i.ibb.co/x3nt3dc/Shahbaz.jpg');
GO
