
CREATE TABLE dbo.Employees (
    Id INT PRIMARY KEY IDENTITY (1,1),
    Name NVARCHAR(100) NOT NULL,
    Position NVARCHAR(100),
    Salary DECIMAL(18,2)
);
GO


CREATE PROCEDURE spInsertEmployee
    @Name NVARCHAR(100),
    @Position NVARCHAR(100),
    @Salary DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Employees (Name, Position, Salary)
    VALUES (@Name, @Position, @Salary);
    SELECT SCOPE_IDENTITY() AS Id;
END;
GO


CREATE PROCEDURE spGetAllEmployees
AS
BEGIN
    SELECT * FROM Employees;
END;
GO


CREATE PROCEDURE spGetEmployeeById
    @Id INT
AS
BEGIN
    SELECT * FROM Employees WHERE Id = @Id;
END;
GO


CREATE PROCEDURE spUpdateEmployee
    @Id INT,
    @Name NVARCHAR(100),
    @Position NVARCHAR(100),
    @Salary DECIMAL(18, 2)
AS
BEGIN
    UPDATE Employees
    SET Name = @Name, Position = @Position, Salary = @Salary
    WHERE Id = @Id;
END;
GO
CREATE PROCEDURE spDeleteEmployee 
@Id INT
AS
BEGIN
DELETE from Employees
WHERE Id= @Id;
END

GO


EXEC sp_Help 'spDeleteEmployee'

EXEC spGetAllEmployees 
EXEC spInsertEmployee @Name='Shreeya Khanal', @Position='Developer', @Salary=1


SELECT * FROM DBO.Employees


