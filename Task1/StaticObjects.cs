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
        public static string Languge = "English";
        public static string AddOrEdit = "";
        public static string DoneOrNot = "";
    }
}
