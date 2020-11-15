using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;

namespace Survey
{
    // This Class Attributes for Variable use an many times in my project 
    public class Constant
    {
        public static int Id = -1;
        public static string Type = "";
        public static string AddOrEdit = "";
        public const string SliderString = "Slider" ;
        public const string SmilyString = "Smily";
        public const string StarsString = "Stars";
        public const string Start_ValueString = "Start_Value";
        public const string End_ValueString = "End_Value";
        public const string Start_Value_CapString = "Start_Value_Cap";
        public const string End_Value_CapString = "End_Value_Cap"; 
        public const string Qustions_textString = "Qustions_text";
        public const string Type_Of_QustionString = "Type_Of_Qustion";
        public const string ErrorString = "Error";
        public const string IDString = "ID";
        public const string Qustion_orderString = "Qustion_order";
        public const string Qus_IDString = "Qus_ID";
        public const string Number_of_smilyString = "Number_of_smily";
        public const string Number_Of_StarsString = "Number_Of_Stars";
        public static string Languge = "English";
        public const string MessageError = "Smomething went wrong!";
        public const string AtsMark = "@";
        public const string ADD = "Add";
        public const string EDIT = "Edit";
        public const string English = "English";
        public const string Arabic = "Arabic";
        public const string ProcdureQuestionDelete = "sp_Qustion_Delete";
        public const string ProcdureQuestionUpdate = "sp_Qustion_update7";
        public const string ProcdureQuestionInsert = "sp_Qustion_Insert3";
        public const string ProcdureQuestionSelectForMax = "select max(ID) as ID from Qustions";
        public const string ProcdureQuestionSelectAll = "sp_Qustions_SelectAll2";

        public const string ProcdureSliderDelete = "sp_Slider_Delete";
        public const string ProcdureSliderUpdate = "sp_Slider_Update10";
        public const string ProcdureSliderInsert = "sp_Slider_Insert1";
        public const string ProcdureSliderSelectAll = "sp_Slider_SelectAll2";


        public const string ProcdureSmilesDelete = "sp_Smily_Delete";
        public const string ProcdureSmilesUpdate = "sp_Smily_Update10";
        public const string ProcdureSmilesInsert = "sp_Smiles_Insert5";
        public const string ProcdureSmilesSelectAll = "sp_Smily_SelectAll2"; 

        public const string ProcdureStarsDelete = "sp_Stars_Delete";
        public const string ProcdureStarsUpdate = "sp_Stars_Update10";
        public const string ProcdureStarsInsert = "sp_Stars_Insert6";
        public const string ProcdureStarsSelectAll = "sp_Stars_SelectAll2";

        public static  List<Qustions> ListOfAllQuestion = new List<Qustions>();
        // This Object For Question using abstract factory 
        public static  AbstractFactory QuestionFactory = new QustionFactory();
        public static  AbstractLog Erros = new Logger();
    }
}
