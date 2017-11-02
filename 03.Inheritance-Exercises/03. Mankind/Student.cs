using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Student : Human
{
    private string facultyNumber;

    public Student(string firstName,string lastName,string facultyNumber) : base(firstName, lastName)
    {
        this.FacultyNumber = facultyNumber;
        base.FirstName = firstName;
        base.LastName = lastName;
    }

    public override string FirstName
    {
        get { return base.FirstName; }
        set
        {
            if (value.Length<4)
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
            if (value.Length<3)
            {
                throw new ArgumentException("Expected length at least 3 symbols! Argument: lastName");
            }
            base.LastName = value;
        }
    }

    public string FacultyNumber
    {
        get { return this.facultyNumber; }
        set
        {
            bool isLetterOrDigit = true;
            foreach (char item in value)
            {
                if (!char.IsLetterOrDigit(item))
                {
                    isLetterOrDigit = false;
                }
            }
            if (value.Length<5||value.Length>10||!isLetterOrDigit)
            {
                throw new ArgumentException("Invalid faculty number!");
            }
            this.facultyNumber = value;
        }
    }

    public override string ToString()
    {
        return base.ToString() + $"Faculty number: { this.FacultyNumber}";
    }
}