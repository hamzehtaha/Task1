using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;

namespace Survey
{
    public static class Attributes
    {
        public static int Id = -1;
        public static string Type = "";
        public static string AddOrEdit = "";
        public static string SliderString = "Slider" ;
        public static string SmilyString = "Smily";
        public static string StarsString = "Stars";
        public static string Start_ValueString = "Start_Value";
        public static string End_ValueString = "End_Value";
        public static string Start_Value_CapString = "Start_Value_Cap";
        public static string End_Value_CapString = "End_Value_Cap"; 
        public static string Qustions_textString = "Qustions_text";
        public static string Type_Of_QustionString = "Type_Of_Qustion";
        public static string ErrorString = "Error";
        public static string IDString = "ID";
        public static string Qustion_orderString = "Qustion_order";
        public static string Qus_IDString = "Qus_ID";
        public static string Number_of_smilyString = "Number_of_smily";
        public static string Number_Of_StarsString = "Number_Of_Stars";
        public static string Languge = "English";
        public static string MessageError = "Smomething went wrong!";
        public static string AtsMark = "@";
        public static List<Qustions> ListOfAllQuestion = new List<Qustions>();
        public static AbstractFactory QusFac = new QustionFactory();
        public static AbstractLog Erros = new Logger(); 
    }
}
