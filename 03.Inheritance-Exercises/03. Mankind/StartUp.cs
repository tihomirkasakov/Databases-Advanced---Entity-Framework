using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main()
    {
        try
        {
            var studentInput = Console.ReadLine().Split();
            Student student = new Student(studentInput[0], studentInput[1], studentInput[2]);

            var workerInput = Console.ReadLine().Split();
            Worker worker = new Worker(workerInput[0], workerInput[1], decimal.Parse(workerInput[2]), decimal.Parse(workerInput[3]));

            Console.WriteLine(student);
            Console.WriteLine();
            Console.WriteLine(worker);

        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
        }
    }
}