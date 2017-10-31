using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var people = new List<Person>();
        var products = new List<Product>();

        string[] allPeople = Console.ReadLine().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        string[] allProducts = Console.ReadLine().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < allPeople.Length; i++)
        {
            try
            {
                var currentPersonArgs = allPeople[i].Split('=');
                string currentName = currentPersonArgs[0];
                decimal currentMoney = decimal.Parse(currentPersonArgs[1]);

                var person = new Person(currentName, currentMoney);
                people.Add(person);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        for (int i = 0; i < allProducts.Length; i++)
        {
            var currentProductArgs = allProducts[i].Split('=');
            string currentProduct = currentProductArgs[0];
            decimal currentPrice = decimal.Parse(currentProductArgs[1]);

            try
            {
                var product = new Product(currentProduct, currentPrice);
                products.Add(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        var command = Console.ReadLine().Split();

        while (command[0] != "END")
        {
            string currentName = command[0];
            var currentItem = command[1];
            Person buyer = people.FirstOrDefault(p => p.Name == currentName);
            Product item = products.FirstOrDefault(p => p.Name == currentItem);

            try
            {
                buyer.BuyProduct(item);
                Console.WriteLine($"{buyer.Name} bought {item.Name}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            command = Console.ReadLine().Split();
        }

        foreach (var person in people)
        {
            Console.WriteLine(person);
        }
    }
}