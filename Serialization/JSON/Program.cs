using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JSON
{
    public class Employee
    {
        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }
    }

    public class Department
    {
        [JsonProperty("department_name")]
        public string DepartmentName { get; set; }

        [JsonProperty("employees")]
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

            // Serialize to JSON format
            var jsonString = JsonConvert.SerializeObject(department, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("department.json", jsonString);

            // Deserialize from JSON format
            var deserializedDepartment = JsonConvert.DeserializeObject<Department>(File.ReadAllText("department.json"));

            // Output the deserialized object to verify
            Console.WriteLine("Deserialized from JSON:");
            Console.WriteLine($"Department Name: {deserializedDepartment.DepartmentName}");
            foreach (var employee in deserializedDepartment.Employees)
            {
                Console.WriteLine($"Employee Name: {employee.EmployeeName}");
            }
        }
    }
}

