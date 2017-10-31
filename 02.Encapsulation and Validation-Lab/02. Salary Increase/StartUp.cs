using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main()
    {
        var lines = int.Parse(Console.ReadLine());
        var persons = new List<Person>();
        for (int i = 0; i < lines; i++)
        {
            var cmdArgs = Console.ReadLine().Split();
            string firstName = cmdArgs[0];
            string lastName = cmdArgs[1];
            int age = int.Parse(cmdArgs[2]);
            double salary = double.Parse(cmdArgs[3]);
            var person = new Person(firstName, lastName, age,salary);

            persons.Add(person);
        }
        var bonus = double.Parse(Console.ReadLine());

        foreach (var p in persons)
        {
            p.IncreaseSalary(bonus);
        }

        persons.ForEach(p => Console.WriteLine(p.ToString()));
    }
}