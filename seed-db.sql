-- Seed database with test users
-- This script should be run after EF migrations have completed

USE LoanHubBackend;
GO

-- Check if users already exist, if not insert seed data
IF NOT EXISTS (SELECT 1 FROM Users)
BEGIN
    SET IDENTITY_INSERT Users ON;
    
    INSERT INTO Users (Id, Email, FirstName, LastName, Role, Address, Phone, Job, Income, Costs, Age, Dependents, CreatedAt, DeletedAt, IsDeleted)
    VALUES
        (1, 'jan.kowalski@test.pl', 'Jan', 'Kowalski', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETUTCDATE(), NULL, 0),
        (2, 'maria.nowak@test.pl', 'Maria', 'Nowak', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETUTCDATE(), NULL, 0),
        (3, 'admin@bank.pl', 'Admin', 'Testowy', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETUTCDATE(), NULL, 0),
        (4, 'anna.wisniewski@test.pl', 'Anna', 'Wiśniewski', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETUTCDATE(), NULL, 0),
        (5, 'piotr.kaminski@test.pl', 'Piotr', 'Kamiński', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETUTCDATE(), NULL, 0);
    
    SET IDENTITY_INSERT Users OFF;
    
    PRINT 'Seeded 5 test users successfully';
END
ELSE
BEGIN
    PRINT 'Users table already contains data, skipping seed';
END
GO
