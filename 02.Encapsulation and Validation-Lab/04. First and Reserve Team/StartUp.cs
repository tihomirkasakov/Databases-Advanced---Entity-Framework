using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main()
    {
        int input = int.Parse(Console.ReadLine());
        Team team = new Team("Chereshka");

        for (int i = 0; i < input; i++)
        {
            var tokens = Console.ReadLine().Split();

            string firstName = tokens[0];
            string lastName = tokens[1];
            int age = int.Parse(tokens[2]);
            double salary = double.Parse(tokens[3]);
            Person persons = new Person(firstName, lastName, age, salary);

            team.AddPlayer(persons);
        }

        Console.WriteLine($"First team have {team.FirstTeam.Count} players");
        Console.WriteLine($"Reserve team have {team.ReserveTeam.Count} players");
    }
}