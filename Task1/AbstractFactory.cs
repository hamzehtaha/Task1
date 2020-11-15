using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    // This abstract class for Abstract Factory Design Pattern
    public abstract class AbstractFactory
    {
        //This Abstract Function For get a Question for any type of Question 
         public abstract Qustions GetQustion(string Qus_Type);
    }
}
