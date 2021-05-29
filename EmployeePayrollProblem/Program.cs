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

            empRepo.UpdatedSalaryFromDatabase("Terisa"); //UC4

            Console.ReadLine();
        }
        public static void AddRecordInput() //Adding record inputs on the table
        {
            try
            {
                EmployeeRepository repository = new EmployeeRepository();//Creating a object of EmployeeRepository class.

                EmployeeModel model = new EmployeeModel();// Adding Employee To Database
                DateTime now = DateTime.Now;
                model.EmployeeId = 4;
                model.EmployeeName = "Riya";
                model.PhoneNumber = "788967543";
                model.Address = "Mumbai";
                model.Department = "HR";
                model.Gender = "F";
                model.BasicPay = 20000;
                model.Deductions = 2000;
                model.TaxablePay = 100;
                model.Tax = 200;
                model.NetPay = 1800;
                model.StartDate = now;
                model.City = "Mumbai";
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
