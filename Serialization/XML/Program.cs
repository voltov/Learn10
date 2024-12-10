using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XML
{
    public class Employee
    {
        [XmlElement("EmployeeName")]
        public string EmployeeName { get; set; }
    }

    public class Department
    {
        [XmlElement("DepartmentName")]
        public string DepartmentName { get; set; }

        [XmlArray("Employees")]
        [XmlArrayItem("Employee")]
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

            // Serialize to XML format
            var xmlSerializer = new XmlSerializer(typeof(Department));
            using (var xmlStream = new FileStream("department.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(xmlStream, department);
            }

            // Deserialize from XML format
            Department deserializedDepartment;
            using (var xmlStream = new FileStream("department.xml", FileMode.Open))
            {
                deserializedDepartment = (Department)xmlSerializer.Deserialize(xmlStream);
            }

            // Output the deserialized object to verify
            Console.WriteLine("Deserialized from XML:");
            Console.WriteLine($"Department Name: {deserializedDepartment.DepartmentName}");
            foreach (var employee in deserializedDepartment.Employees)
            {
                Console.WriteLine($"Employee Name: {employee.EmployeeName}");
            }
        }
    }
}

