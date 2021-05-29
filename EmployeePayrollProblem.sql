-- Create Table
create table payroll_servicess(EmployeeId int ,EmployeeName varchar(20),PhoneNumber varchar(10) NOT NULL, Address varchar(20) NOT NULL, Department varchar(20) NOT NULL,Gender char(1) NOT NULL,BasicPay float NOT NULL , Deductions float NOT NULL,TaxablePay float NOT NULL ,Tax float  NOT NULL ,NetPay float NOT NULL , StartDate DATETIME DEFAULT GETDATE(),City varchar(10),Country varchar(10));
select * from payroll_servicess;


drop table payroll_services;