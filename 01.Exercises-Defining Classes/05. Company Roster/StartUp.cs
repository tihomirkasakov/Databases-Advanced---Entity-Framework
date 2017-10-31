using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StartUp
{
    static void Main()
    {
        int count = int.Parse(Console.ReadLine());

        var employees = new Dictionary<string, List<Employee>>();

        for (int i = 0; i < count; i++)
        {
            var input = Console.ReadLine().Split();
            string name = input[0];
            double salary = double.Parse(input[1]);
            string position = input[2];
            string department = input[3];

            var employee = new Employee(name, salary, position, department);

            if (input.Length==5)
            {
                bool isAge = int.TryParse(input[4], out int age);

                if (isAge)
                {
                    employee.Age = age;
                }
                else
                {
                    employee.Email = input[4];
                }
            }
            else if (input.Length == 6)
            {
                employee.Email = input[4];
                employee.Age = int.Parse(input[5]);
            }

            if (!employees.ContainsKey(department))
            {
                employees.Add(department, new List<Employee>());
            }

            employees[department].Add(employee);
        }

        double highestAvgSalary = 0;
        string highestAvgSalaryDepartment = "";

        foreach (var kvp in employees)
        {
            double depSalary = 0;
            foreach (var s in kvp.Value)
            {
                depSalary += s.Salary;

                var avgSalary = depSalary / kvp.Value.Count();

                if (avgSalary > highestAvgSalary)
                {
                    highestAvgSalary = avgSalary;
                    highestAvgSalaryDepartment = kvp.Key;
                }
            }
        }

        Console.WriteLine($"Highest Average Salary: {highestAvgSalaryDepartment}");

        foreach (var emp in employees[highestAvgSalaryDepartment].OrderByDescending(e=>e.Salary))
        {
            Console.WriteLine($"{emp.Name} {emp.Salary:f2} {emp.Email} {emp.Age}");
        }
    }
}

