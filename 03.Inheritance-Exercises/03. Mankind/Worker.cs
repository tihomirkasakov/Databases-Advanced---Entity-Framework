using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Worker : Human
{
    private decimal weekSalary;
    private decimal workingHours;

    public Worker(string firstName, string lastName, decimal weekSalary, decimal workingHours) : base(firstName, lastName)
    {
        base.FirstName = firstName;
        base.LastName = lastName;
        this.WeekSalary = weekSalary;
        this.WorkingHours = workingHours;
    }

    public override string FirstName
    {
        get { return base.FirstName; }
        set
        {
            if (value.Length < 4)
            {
                throw new ArgumentException("Expected length at least 4 symbols! Argument: firstName");
            }
            base.FirstName = value;
        }
    }

    public override string LastName
    {
        get { return base.LastName; }
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Expected length at least 3 symbols! Argument: lastName");
            }
            base.LastName = value;
        }
    }
    public decimal WeekSalary
    {
        get { return this.weekSalary; }
        set
        {
            if (value<=10)
            {
                throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
            }
            this.weekSalary = value;
        }
    }

    public decimal WorkingHours
    {
        get { return this.workingHours; }
        set
        {
            if (value < 1 || value > 12)
            {
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
            }
            this.workingHours = value;
        }
    }
    public override string ToString()
    {
        return base.ToString() +
            $"Week Salary: {this.WeekSalary:f2}" + Environment.NewLine +
            $"Hours per day: {this.workingHours:f2}" + Environment.NewLine +
            $"Salary per hour: {(this.weekSalary / 5) / this.workingHours:f2}";
    }
}