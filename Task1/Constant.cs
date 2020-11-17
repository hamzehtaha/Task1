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
        public const string DELETE = "Delete";
        public static int Id = -1;
        public static string Type = "";
        public static string AddOrEdit = "";
        public const string English = "English";
        public const string Arabic = "Arabic";
        public const string EnglishMark = "en-US";
        public const string ArabicMark = "ar-EG";
        public const string ProcdureQuestionDelete = "sp_Qustion_Delete";
        public const string ProcdureQuestionUpdate = "sp_Qustion_update7";
        public const string ProcdureQuestionInsert = "sp_Qustion_Insert3";
        public const string ProcdureQuestionSelectForMax = "select max(ID) as ID from Qustions";
        public const string ProcdureQuestionSelectAll = "sp_Qustions_SelectAll2";
        public const string SelectStarFromQuestion = "SELECT * FROM Qustions";
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
        public const string NoSelectItem = "This is not select any Question !";
        public const string SureToDeleteMessage = "Are you sure to Delete this answer ?";
        public const string DataIsEnterd = "Your Data is Enterd";
        public const string QuestionIsEmptyMessage = "Your Question is Empty";
        public const string QuestionIsJustANumberMessage = "Your Question just numbers";
        public const string NewOrderLessThanZeroMessage = "The order sholud be grater than 0";
        public const string StartValueLessThanZeroMessage = "Your start value must be greater than 0";
        public const string EndValueLessThanZeroMessage = "Your end value must be greater than 0";
        public const string StartValueGreaterThanOneHundredMessage = "The start value should be less than 100";
        public const string EndValueGreaterThanOneHundredMessage = "The end value should be less than 100";
        public const string TheEndValueSholudGreaterThanStartValueMessage = "The end value should be grater than start value";
        public const string StartCaptionEmptyMessage = "The start caption is empty";
        public const string StartCaptionJustNumberMessage = "The start caption should not contain only numbers";
        public const string EndCaptionEmptyMessage = "The end caption is empty";
        public const string EndCaptionJustNumberMessage = "The end caption should not contain only numbers";
        public const string NumberOfSmileBetweenFiveAndTow = "The number of smile face should be greater than 1 and less than 6";
        public const string NumberOfStrasBetweenTenAndOne = "The number of stars face should be greater than 0 and less than or equal 10";
        public const string TheOrderIsEmpty = "Your Order is Empty";
        public const string NotChooseTheType = "Your are not Choose the Type Of Question";
        public static string DoneOrNot = "";
        public const string Done = "Done";
        public const string Not = "Not";

        public const string Empty = "";
        public const string ConnectionString = "connection_string";
    }
}
