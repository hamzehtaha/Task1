using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public abstract class AbstractFactory
    {
         public abstract Qustions GetQustion(string Qus_Type);
    }
}
