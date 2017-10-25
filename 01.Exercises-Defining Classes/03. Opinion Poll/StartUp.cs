using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main()
    {
        var personCount = int.Parse(Console.ReadLine());
        List<Person> persons = new List<Person>();

        for (int i = 0; i < personCount; i++)
        {
            string [] tokens = Console.ReadLine().Split();
            string name = tokens[0];
            int age = int.Parse(tokens[1]);
            Person nameAge = new Person(name,age);
            persons.Add(nameAge);
        }

        List<Person> filter = persons.Where(p => p.Age > 30).OrderBy(p => p.Name).ToList();

        foreach (Person person in filter)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }
    }
}