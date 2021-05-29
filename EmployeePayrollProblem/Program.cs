using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Services Using ADO.NET Problem");
            EmployeeRepository empRepo = new EmployeeRepository();//Creating a Object
            //empRepo.DataBaseConnection();
            //empRepo.GetAllEmployeeData();
            //AddRecordInput();
            //empRepo.UpdateBasicPay("Riya", 3000000);//UC3 update BasicPay where name is Terisa table 
            //empRepo.UpdatedSalaryFromDatabase("Terisa"); //UC4
            //empRepo.EmployeesFromForDateRange("2020-01-15"); //UC5
            //empRepo.FindGroupedByGenderRecord("F");
            empRepo. InsertIntoMultipleTablesWithTransactions(); //UC7

            empRepo.RetrieveEmployeeDetailsFromMultipleTables();

            Console.ReadLine();
        }
        public static void AddRecordInput() //UC2 Adding record inputs on the table
        {
            try
            {
                EmployeeRepository repository = new EmployeeRepository();//Creating a object of EmployeeRepository class.

                EmployeeModel model = new EmployeeModel();// Adding Employee To Database
                DateTime now = DateTime.Now;
                model.EmployeeId = 10;
                model.EmployeeName = "Ankita";
                model.PhoneNumber = "1237896541";
                model.Address = "Pune";
                model.Department = "Tester";
                model.Gender = "F";
                model.BasicPay = 45000;
                model.Deductions = 4000;
                model.TaxablePay = 1200;
                model.Tax = 1000;
                model.NetPay = 1500;
                model.StartDate = now;
                model.City = "Chennai";
                model.Country = "IN";

                repository.AddEmployee(model);  //call AddEmployee method and pass model values       
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}