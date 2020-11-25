using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Survey
{
    class StaticObjects
    {
        // List Of All My Question
        public static List<Qustions> ListOfAllQuestion = new List<Qustions>();
        public static AbstractLog Erros = new Logger();
        public static Slider NewSlider = null;
        public static Smiles NewSmile = null;
        public static Stars NewStars = null;
        public static string Languge = "English";
        public static int Id = -1;
        public static string Type = "";
        public static string AddOrEdit = "";
        public static string DoneOrNot = "";
    }
}
