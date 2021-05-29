--- create company Table
Create table company (CompanyID int,CompanyName varchar(50));

 select * from company

 --- create employee Table
Create table employee(EmployeeId int ,EmployeeName varchar(20),Gender char(1), PhoneNumber float , Address varchar(20) , StartDate DATETIME ,City varchar(10) ,Country varchar(10),CompanyID int);
 select * from employee
 
 --- create payroll Table
Create table payroll(EmployeeId int ,BasicPay float , Deductions float ,TaxablePay float ,IncomeTax float, NetPay float );
 select * from payroll
 
 --- create department Table
 Create table department (DepartmentID int,DepartmentName varchar(50));
 select * from department
 

 --- create employee_dept Table
 Create table employee_dept (EmployeeId int,DepartmentID int);

  select * from employee_dept