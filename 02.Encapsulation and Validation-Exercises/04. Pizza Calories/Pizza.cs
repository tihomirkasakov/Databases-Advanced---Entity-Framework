//using System;
//using System.Collections.Generic;
//using System.Linq;

//public class Pizza
//{
//    private string name;
//    private Dough dough;
//    private List<Topping> toppings;

//    public Pizza(string name)
//    {
//        this.Name = name;
//        this.toppings = new List<Topping>();
//    }

//    public string Name
//    {
//        get { return this.name; }
//        private set
//        {
//            if (string.IsNullOrEmpty(value) || value.Length > 15)
//            {
//                throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
//            }
//            this.name = value;
//        }
//    }
//    public Dough Dough
//    {
//        private get { return this.dough; }
//        set
//        {
//            this.dough = value;
//        }
//    }

//    public void AddTopping(Topping topping)
//    {
//        if (this.toppings.Count > 10)
//        {
//            throw new ArgumentException("Number of toppings should be in range [0..10].");
//        }
//        this.toppings.Add(topping);
//    }

//    public double TotalCalories()
//    {
//        double totalCalories = this.Dough.Calories;
//        foreach (var item in this.toppings)
//        {
//            totalCalories += item.Calories;
//        }

//        return totalCalories;
//    }

//    public override string ToString()
//    {
//        return $"{this.Name} - {this.TotalCalories():f2} Calories.";
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;

public class Pizza
{
    private string name;
    private Dough dough;
    private List<Topping> toppings;

    public Pizza(string name, Dough dough)
    {
        this.Name = name;
        this.Dough = dough;
        this.toppings = new List<Topping>();
    }

    public string Name
    {
        get { return this.name; }
        set
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length > 15)
            {
                throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
            }
            this.name = value;
        }
    }
    public Dough Dough
    {
        private get { return this.dough; }
        set
        {
            this.dough = value;
        }
    }

    public void AddTopping(Topping topping)
    {
        if (this.toppings.Count > 10)
        {
            throw new ArgumentException("Number of toppings should be in range [0..10].");
        }
        this.toppings.Add(topping);
    }

    public double TotalCalories()
    {
        double totalCalories = this.Dough.Calories();
        foreach (var item in this.toppings)
        {
            totalCalories += item.Calories();
        }

        return totalCalories;
    }
}