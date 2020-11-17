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
        // This Object For Question using abstract factory 
        public static AbstractFactory QuestionFactory = new QustionFactory();
        public static AbstractLog Erros = new Logger();
        public static Slider NewSlider = null;
        public static Smiles NewSmile = null;
        public static Stars NewStars = null;
        public static string[] Attrubite = null;
    }
}
