using Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class QustionFactory : AbstractFactory
    {
        public override Qustions GetQustion(string Qus_Type)
        {
            if (Qus_Type.Equals(Attributes.SliderString))
            {
                return new Slider(); 
            }else if (Qus_Type.Equals(Attributes.SmilyString))
            {
                return new Smiles(); 
            }else 
            {
                return new Stars(); 
            }
        }
    }
}
