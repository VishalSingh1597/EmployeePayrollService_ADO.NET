using NUnit.Framework;
using EmployeePayrollProblem;

namespace EmployeePayrollProblemTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
            // UC1:- Ability to create a payroll service database and have C# program connect to database.

            [Test]
            public void CheckConnection() //Create CheckConnection method to Check Connection
            {
                EmployeeRepository employeeRepository = new EmployeeRepository(); //create object EmployeeRepository class
                bool actual = employeeRepository.DataBaseConnection(); //call method
                bool expected = true; //expected true 
                Assert.AreEqual(expected, actual); // Check equal or not
            }
        
  //TC3:- Ability to update the salary i.e. the base pay for Employee 


        [Test]
        public void UpdateRecord() //Create UpdateRecord method to Update value on the table
        {
            string EmployeeName = "Terisa";
            double BasicPay = 3000000;
            EmployeeRepository employeeRepository = new EmployeeRepository(); //create object EmployeeRepository class
            bool actual = employeeRepository.UpdateBasicPay(EmployeeName, BasicPay); //call method and pass parameter
            bool expected = true; //expected true 
            Assert.AreEqual(expected, actual); // Check equal or not
        }

    }
}
    
