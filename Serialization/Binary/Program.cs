using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Binary
{
    [Serializable]
    public class Employee
    {
        public string EmployeeName { get; set; }
    }

    [Serializable]
    public class Department
    {
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new List<Employee>();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a Department object
            var department = new Department
            {
                DepartmentName = "Engineering",
                Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "John Doe" },
                    new Employee { EmployeeName = "Jane Smith" }
                }
            };

            // Serialize to binary format
            var binaryFormatter = new BinaryFormatter();
            using (var binaryStream = new FileStream("department.dat", FileMode.Create))
            {
                binaryFormatter.Serialize(binaryStream, department);
            }

            // Deserialize from binary format
            Department deserializedDepartment;
            using (var binaryStream = new FileStream("department.dat", FileMode.Open))
            {
                deserializedDepartment = (Department)binaryFormatter.Deserialize(binaryStream);
            }

            // Output the deserialized object to verify
            Console.WriteLine("Deserialized from binary:");
            Console.WriteLine($"Department Name: {deserializedDepartment.DepartmentName}");
            foreach (var employee in deserializedDepartment.Employees)
            {
                Console.WriteLine($"Employee Name: {employee.EmployeeName}");
            }
        }
    }
}
