using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
           //var prop = typeof(Personel).GetProperties();
           // foreach (var personel in prop)
           // {
           //     Console.WriteLine(personel.Name);
            //}
            
        var met = typeof(Personel).GetMethods();
        foreach (var pers in met)
        {
            Console.WriteLine(pers.Name);
        }
        Console.ReadLine();




        }
    }
}
