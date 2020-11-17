using Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    // This Class using AbstractFactory 
    class QustionFactory : AbstractFactory
    {
        // this function override function from AbstractFactory to get object from type of Question  
        public override Qustions GetQustion(string Qus_Type)
        {
            try
            {
                if (Qus_Type.Equals(Constant.SliderString))
                {
                    return new Slider();
                }
                else if (Qus_Type.Equals(Constant.SmilyString))
                {
                    return new Smiles();
                }
                else
                {
                    return new Stars();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
                return null; 
            }
        }
    }
}
