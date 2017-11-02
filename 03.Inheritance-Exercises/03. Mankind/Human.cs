using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Human
{
    private string firstName;
    private string lastName;

    public Human(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public virtual string FirstName
    {
        get { return this.firstName; }
        set
        {
            if (value[0].ToString() != value[0].ToString().ToUpper())
            {
                throw new ArgumentException("Expected upper case letter! Argument: firstName");
            }
            this.firstName = value;
        }
    }

    public virtual string LastName
    {
        get { return this.lastName; }
        set
        {
            if (value[0].ToString() != value[0].ToString().ToUpper())
            {
                throw new ArgumentException("Expected upper case letter! Argument: lastName");
            }
            this.lastName = value;
        }
    }

    public override string ToString()
    {
        return$"First Name: {this.FirstName}" + Environment.NewLine +
               $"Last Name: {this.LastName}" + Environment.NewLine;
    }
}