using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollProblem
{
    public class EmployeeRepository
    {  // UC1:- Ability to create a payroll service database and have C# program connect to database.


        public static string connectionString = @"Data Source=LAPTOP-FNHEQJH9;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;"; //Specifying the connection string from the sql server connection.

        SqlConnection connection = new SqlConnection(connectionString); // Establishing the connection using the Sqlconnection.  

        public bool DataBaseConnection()
        {
            try
            {
                connection.Open(); // open connection
                using (connection)  //using SqlConnection
                {
                    Console.WriteLine($"Connection is created Successful "); //print msg

                }
                connection.Close(); //close connection
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
        // UC2:- Ability for Employee Payroll Service to retrieve the Employee Payroll from the Database.

        public void GetAllEmployeeData()
        {

            EmployeeModel employeemodel = new EmployeeModel(); //Creating Employee model class object
            try
            {
                using (connection)
                {

                    string query = @"select * from dbo.payroll_servicess"; // Query to get all the data from table./*TableName:-dbo.payroll_service*/

                    this.connection.Open(); //open connection

                    SqlCommand command = new SqlCommand(query, connection); //accept query and connection

                    SqlDataReader reader = command.ExecuteReader(); // Execute sqlDataReader to fetching all records

                    if (reader.HasRows)     // Checking datareader has rows or not.               
                    {
                        while (reader.Read()) //using while loop for read multiple rows.
                        {
                            employeemodel.EmployeeId = reader.GetInt32(0);
                            employeemodel.EmployeeName = reader.GetString(1);
                            employeemodel.PhoneNumber = reader.GetString(2);
                            employeemodel.Address = reader.GetString(3);
                            employeemodel.Department = reader.GetString(4);
                            employeemodel.Gender = reader.GetString(5);
                            employeemodel.BasicPay = reader.GetDouble(6);
                            employeemodel.Deductions = reader.GetDouble(7);
                            employeemodel.TaxablePay = reader.GetDouble(8);
                            employeemodel.Tax = reader.GetDouble(9);
                            employeemodel.NetPay = reader.GetDouble(10);
                            employeemodel.StartDate = reader.GetDateTime(11);
                            employeemodel.City = reader.GetString(12);
                            employeemodel.Country = reader.GetString(13);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", employeemodel.EmployeeId, employeemodel.EmployeeName, employeemodel.PhoneNumber,
                            employeemodel.Address, employeemodel.Department, employeemodel.Gender, employeemodel.BasicPay, employeemodel.Deductions, employeemodel.TaxablePay, employeemodel.Tax, employeemodel.NetPay, employeemodel.StartDate, employeemodel.City, employeemodel.Country);

                        }
                    }
                    else
                    {
                        Console.WriteLine(" Record Not found on Table ");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close(); //Always ensuring the closing of the connection
            }
        }
            public bool AddEmployee(EmployeeModel model) //insert record to the table
            {

                try
                {
                    using (this.connection)
                    {
                        SqlCommand command = new SqlCommand("dbo.SqlProcedureName", this.connection);   //Creating a stored Procedure for adding employees into database

                        command.CommandType = CommandType.StoredProcedure; //Command type is a class to set as stored procedure
                                                                           // Adding values from employeemodel to stored procedure 

                        command.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                        command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", model.Address);
                        command.Parameters.AddWithValue("@Department", model.Department);
                        command.Parameters.AddWithValue("@Gender", model.Gender);
                        command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                        command.Parameters.AddWithValue("@Deductions", model.Deductions);
                        command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                        command.Parameters.AddWithValue("@Tax", model.Tax);
                        command.Parameters.AddWithValue("@NetPay", model.NetPay);
                        command.Parameters.AddWithValue("@StartDate", model.StartDate);
                        command.Parameters.AddWithValue("@City", model.City);
                        command.Parameters.AddWithValue("@Country", model.Country);
                        connection.Open();
                        var result = command.ExecuteNonQuery();
                        connection.Close();

                        if (result != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        // UC3:- Ability to update the salary i.e. the base pay for Employee 


        public bool UpdateBasicPay(string EmployeeName, double BasicPay)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = @"update dbo.payroll_servicess set BasicPay=@inputBasicPay where EmployeeName=@inputEmployeeName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@inputBasicPay", BasicPay); //parameters transact SQl stament or store procedure
                    command.Parameters.AddWithValue("@inputEmployeeName", EmployeeName);
                    var result = command.ExecuteNonQuery(); //ExecuteNonQuery and store result
                    Console.WriteLine("Record Update Successfully");
                    connection.Close();
                    GetAllEmployeeData(); // call method and show record
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return true;
        }
        //UC4:- Ability to update the salary i.e. the base pay for Employee.

        public double UpdatedSalaryFromDatabase(string EmployeeName)
        {

            string cconnectionString = @"Data Source=LAPTOP-FNHEQJH9;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; //Specifying the connection string from the sql server connection.

            SqlConnection connection = new SqlConnection(cconnectionString);
            try
            {
                using (connection)
                {
                    string query = @"select BasicPay from dbo.payroll_servicess where EmployeeName=@inputEmployeeName";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@inputEmployeeName", EmployeeName);
                    return (double)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        //UC5:- Ability to retrieve all employees who have joined in a particular data range from the  payroll service database
      

        public void EmployeesFromForDateRange(string Date)
        {
            EmployeeModel employeemodel = new EmployeeModel(); //Creating Employee model class object

            try
            {
                using (connection)
                {
                    connection.Open(); //open connection
                    string query = $@"select * from dbo.payroll_servicess where StartDate between cast('{Date}' as date) and cast(getdate() as date)";
                    SqlCommand command = new SqlCommand(query, connection); //accept query and connection

                    SqlDataReader reader = command.ExecuteReader(); // Execute sqlDataReader to fetching all records

                    if (reader.HasRows)     // Checking datareader has rows or not.               
                    {
                        // Console.WriteLine("EmployeeId, EmployeeName, PhoneNumber, Address, Department, Gender, BasicPay, Deductions, TaxablePay, TaxablePay, Tax, NetPay, StartDate, City, Country");                                            
                        while (reader.Read()) //using while loop for read multiple rows.
                        {
                            employeemodel.EmployeeId = reader.GetInt32(0);
                            employeemodel.EmployeeName = reader.GetString(1);
                            employeemodel.PhoneNumber = reader.GetString(2);
                            employeemodel.Address = reader.GetString(3);
                            employeemodel.Department = reader.GetString(4);
                            employeemodel.Gender = reader.GetString(5);
                            employeemodel.BasicPay = reader.GetDouble(6);
                            employeemodel.Deductions = reader.GetDouble(7);
                            employeemodel.TaxablePay = reader.GetDouble(8);
                            employeemodel.Tax = reader.GetDouble(9);
                            employeemodel.NetPay = reader.GetDouble(10);
                            employeemodel.StartDate = reader.GetDateTime(11);
                            employeemodel.City = reader.GetString(12);
                            employeemodel.Country = reader.GetString(13);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", employeemodel.EmployeeId, employeemodel.EmployeeName, employeemodel.PhoneNumber,
                            employeemodel.Address, employeemodel.Department, employeemodel.Gender, employeemodel.BasicPay, employeemodel.Deductions, employeemodel.TaxablePay, employeemodel.Tax, employeemodel.NetPay, employeemodel.StartDate, employeemodel.City, employeemodel.Country);

                        }

                    }
                    else
                    {
                        Console.WriteLine($"{Date} Record Not found on The Table "); //print 
                    }
                    reader.Close(); //close                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close(); //Always ensuring the closing of the connection
            }

        }
        //UC6:- Ability to find sum, average, min, max and number of male and female employees.


        public bool FindGroupedByGenderRecord(string Gender) //create method to find gender BasicPay min, max ...
        {
            try
            {
                using (connection)
                {
                    string query = @"select Gender,COUNT(BasicPay) as EmpCount, MIN(BasicPay) as MinBasicPay, MAX(BasicPay) 
                                   as MaxBasicPay, SUM(BasicPay) as SumBasicPay,avg(BasicPay) as AvgBasicPay from dbo.payroll_servicess
                                   where Gender=@inputGender group by Gender";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@inputGender", Gender);//parameters transact SQl statement or store procedure
                    connection.Open();  //open connection
                    SqlDataReader reader = command.ExecuteReader();  // Execute sqlDataReader to fetching all records
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int EmpCount = reader.GetInt32(1);  //Read EmpCount
                            double MinBasicPay = reader.GetDouble(2); //Read MinBasicPay
                            double MaxBasicPay = reader.GetDouble(3);
                            double SumBasicPay = reader.GetDouble(4);
                            double AvgBasicPay = reader.GetDouble(5);
                            Console.WriteLine($"Gender:- {Gender}\nEmployee Count:- {EmpCount}\nMinimum BasicPay:-{MinBasicPay}\nMaximum BasicPay:- {MaxBasicPay}\n" +
                                $"Total Salary for {Gender} :- {SumBasicPay}\n" + $"Average BasicPay:- {AvgBasicPay}");

                        }
                        connection.Close();
                    }
                    else
                    {
                        Console.WriteLine($"{Gender} Gender Record Not found From the Table");
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return true;
        }
        //UC7:- Ensure UC 2 – UC 7 works with the new ER Diagram implemented into Payroll Service DB

        public void InsertIntoMultipleTablesWithTransactions()
        {

            Console.Write("Enter EmployeeID:- ");
            int empID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee Name:- ");
            string empName = Console.ReadLine();

            DateTime startDate = DateTime.Now;

            Console.Write("Enter Employee Address:- ");
            string address = Console.ReadLine();

            Console.Write("Enter Employee Gender:- ");
            string gender = Console.ReadLine();

            Console.Write("Enter Employee PhoneNumber:- ");
            double phonenumber = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Employee City:- ");
            string city = Console.ReadLine();

            Console.Write("Enter Employee Country:- ");
            string country = Console.ReadLine();

            Console.Write("Enter Employee BasicPay:- ");
            int basicPay = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee Deductions:- ");
            int deductions = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee TaxablePay:- ");
            int taxablePay = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee IncomeTax:- ");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee NetPay:- ");
            int netPay = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee CompanyID:- ");
            int companyId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee CompanyName:- ");
            string companyName = Console.ReadLine();

            Console.Write("Enter Employee DepartmentID:- ");
            int deptId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee Department Name:- ");
            string deptName = Console.ReadLine();

            using (connection)
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction(); // Start a local transaction.

                SqlCommand command = connection.CreateCommand();  // Enlist a command in the current transaction.
                command.Transaction = sqlTran;

                try
                {
                    //company Table insert Record // Execute command
                    command.CommandText = "insert into company values(@CompanyID,@CompanyName)";
                    command.Parameters.AddWithValue("@CompanyID", companyId);
                    command.Parameters.AddWithValue("@CompanyName", companyName);
                    command.ExecuteScalar();

                    // employee Table insert Record // Execute command
                    command.CommandText = "insert into employee values(@EmployeeId,@EmployeeName,@Gender,@PhoneNumber,@Address,@StartDate,@City,@Country,@CompanyID)";
                    command.Parameters.AddWithValue("@EmployeeId", empID);
                    command.Parameters.AddWithValue("@EmployeeName", empName);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@Country", country);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                    command.Parameters.AddWithValue("@Address", address);
                    command.ExecuteScalar();

                    // payroll Table insert Record // Execute command
                    command.CommandText = "insert into payroll values(@EmployeeId,@BasicPay,@Deductions,@TaxablePay,@IncomeTax,@NetPay)";
                    command.Parameters.AddWithValue("@BasicPay", basicPay);
                    command.Parameters.AddWithValue("@Deductions", deductions);
                    command.Parameters.AddWithValue("@TaxablePay", taxablePay);
                    command.Parameters.AddWithValue("@IncomeTax", tax);
                    command.Parameters.AddWithValue("@NetPay", netPay);
                    command.ExecuteScalar();

                    // department Table insert Record // Execute command
                    command.CommandText = "insert into department values(@DepartmentID,@DepartmentName)";
                    command.Parameters.AddWithValue("@DepartmentID", deptId);
                    command.Parameters.AddWithValue("@DepartmentName", deptName);
                    command.ExecuteScalar();

                    // employee_dept  Table insert Record // Execute command
                    command.CommandText = "insert into employee_dept values(@EmployeeId,@DepartmentID)";
                    command.ExecuteNonQuery();

                    sqlTran.Commit(); // Commit the transaction after all commands.
                    Console.WriteLine("All Records Added into The Database.");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message); // Handle the exception if the transaction fails to commit.
                    try
                    {

                        sqlTran.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message); // Throws an InvalidOperationException if the connection
                                                               // is closed or the transaction has already been rolled
                                                               // back on the server.
                    }
                }
            }

        }
        // UC8:- Implement the complete ER Diagram in the Database.

        public void RetrieveEmployeeDetailsFromMultipleTables()
        {

            EmployeeModel employee = new EmployeeModel();


            string query = @"select emp.EmployeeId,emp. EmployeeName, emp.BasicPay, emp.StartDate, emp.PhoneNumber, emp.Address, 
                                    dept.Department, emp.Gender, pay.Deductions, pay.TaxablePay, pay.IncomeTax, pay.NetPay
                                    from employee emp, department dept, payroll pay
                                    where emp.EmployeeId = dept.EmployeeId and dept.BasicPay = pay.BasicPay;";

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employee.EmployeeId = reader.GetInt32(0);
                            employee.EmployeeName = reader.GetString(1);
                            employee.BasicPay = reader.GetInt32(2);
                            employee.StartDate = reader.GetDateTime(3);
                            employee.PhoneNumber = reader.GetString(4);
                            employee.Address = reader.GetString(5);
                            employee.Department = reader.GetString(6);
                            employee.Gender = reader.GetString(7);
                            employee.Deductions = reader.GetDouble(8);
                            employee.TaxablePay = reader.GetDouble(9);
                            employee.Tax = reader.GetDouble(10);
                            employee.NetPay = reader.GetDouble(11);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", employee.EmployeeId, employee.EmployeeName,
                                employee.Gender, employee.Address, employee.BasicPay, employee.StartDate, employee.PhoneNumber, employee.Address,
                                employee.Department, employee.Deductions, employee.TaxablePay, employee.Tax, employee.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Recod found");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }
    }
}




