using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepCloning
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

            // Perform deep cloning
            var clonedDepartment = DeepClone(department);

            // Modify the cloned object to verify independence
            clonedDepartment.DepartmentName = "Marketing";
            clonedDepartment.Employees[0].EmployeeName = "Alice Johnson";

            // Output the original and cloned objects to verify
            Console.WriteLine("Original Department:");
            Console.WriteLine($"Department Name: {department.DepartmentName}");
            foreach (var employee in department.Employees)
            {
                Console.WriteLine($"Employee Name: {employee.EmployeeName}");
            }

            Console.WriteLine("\nCloned Department:");
            Console.WriteLine($"Department Name: {clonedDepartment.DepartmentName}");
            foreach (var employee in clonedDepartment.Employees)
            {
                Console.WriteLine($"Employee Name: {employee.EmployeeName}");
            }
        }

        public static T DeepClone<T>(T obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, obj);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}

