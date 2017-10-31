using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Car
{
    public string Model { get; set; }
    public double Fuel { get; set; }
    public double ConsumptionPer1km { get; set; }
    public double Distance { get; set; }

    public Car(string model, double fuel, double consumption)
    {
        this.Model = model;
        this.Fuel = fuel;
        this.ConsumptionPer1km = consumption;
        this.Distance = 0;
    }

    public bool CanCarMove(double distance)
    {
        var fuelNeeded = this.ConsumptionPer1km * distance;

        if (fuelNeeded>this.Fuel)
        {
            return false;
        }

        this.Distance += distance;
        this.Fuel -= fuelNeeded;

        return true;
    }
}