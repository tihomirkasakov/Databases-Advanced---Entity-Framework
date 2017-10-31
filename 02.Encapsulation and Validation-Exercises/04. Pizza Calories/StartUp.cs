//using System;

//public class StartUp
//{
//    public static void Main()
//    {
//        try
//        {
//            var pizzaNameInput = Console.ReadLine().Split();
//            var pizzaName = pizzaNameInput[1];
//            Pizza pizza = new Pizza(pizzaName);

//            var doughInput = Console.ReadLine().Split();
//            string flourType = doughInput[1];
//            string bakingTechnique = doughInput[2];
//            double weightDough = double.Parse(doughInput[3]);
//            Dough dough = new Dough(flourType, bakingTechnique, weightDough);
//            pizza.Dough = dough;

//            string command = Console.ReadLine();
//            while (command != "END")
//            {
//                var input = command.Split();
//                string types = input[1];
//                double weightTopping = double.Parse(input[2]);
//                Topping topping = new Topping(types, weightTopping);
//                pizza.AddTopping(topping);

//                command = Console.ReadLine();
//            }

//            Console.WriteLine(pizza);

//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.Message);
//        }
//    }
//}

using System;

public class StartUp
{
    public static void Main()
    {
        try
        {
            var pizzaNameInput = Console.ReadLine().Split();
            var pizzaName = pizzaNameInput[1];

            var doughInput = Console.ReadLine().Split();
            string flourType = doughInput[1];
            string bakingTechnique = doughInput[2];
            double weightDough = double.Parse(doughInput[3]);
            Dough dough = new Dough(flourType, bakingTechnique, weightDough);

            Pizza pizza = new Pizza(pizzaName, dough);

            var input = Console.ReadLine().Split();
            while (input[0] != "END")
            {
                string classes = input[1];
                double weightTopping = double.Parse(input[2]);
                Topping topping = new Topping(classes, weightTopping);
                pizza.AddTopping(topping);

                input = Console.ReadLine().Split();
            }

            Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories():f2} Calories.");

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}