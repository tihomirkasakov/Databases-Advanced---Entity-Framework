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

        var cars = new List<Car>();

        for (int i = 0; i < count; i++)
        {
            var input = Console.ReadLine().Split();

            string model = input[0];
            double fuel = double.Parse(input[1]);
            double consumption = double.Parse(input[2]);

            if (!cars.Any(c=>c.Model==model))
            {
                Car car = new Car(model, fuel, consumption);
                cars.Add(car);
            }
        }

        string command;
        while ((command=Console.ReadLine())!="End")
        {
            var tokens = command.Split();
            string model = tokens[1];
            double distance = double.Parse(tokens[2]);

            Car check = cars.Find(c => c.Model == model);
            bool enoughFuel = check.CanCarMove(distance);

            if (!enoughFuel)
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }

        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Model} {car.Fuel:f2} {car.Distance}");
        }
    }
}