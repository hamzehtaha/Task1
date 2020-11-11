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
        public static int AddOrEdit = 0;
        public static AbstractFactory QusFac = new QustionFactory();
        public static List<Qustions> ListOfAllQuestion = new List<Qustions>();
        public static string connectionString = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        public static SqlConnection GetConnection ()
        {
            try
            {
                return new SqlConnection(Attributes.connectionString);
            }catch (Exception ex)
            {
                return new SqlConnection(Attributes.connectionString);
            }
        }
        public enum Variables
        {
            Slider,
            Smily,
            Stars,
            Start_Value,
            End_Value,
            Start_Value_Cap,
            End_Value_Cap,
            ID,
            Qustions_text,
            Qustion_order,
            Type_Of_Qustion,
            Qus_ID,
            Number_of_smily,
            Number_Of_Stars,
            Error

        }

    }
}
