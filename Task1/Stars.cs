using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Survey;
using System.Windows.Forms;
using System.Diagnostics;
namespace Task1
{
    class Stars : Qustions
    {
        public int NumberOfStars { get; set; }
        public int IdForType { get; set;  }
        public Stars(int Id,int IdForType, string NewText, string TypeOfQuestion, int Order , int NumberOfStars) {
            try
            {
                this.NewText = NewText;
                this.NumberOfStars = NumberOfStars;
                this.Order = Order;
                this.TypeOfQuestion = TypeOfQuestion;
                this.Id = Id;
                this.IdForType = IdForType;
            }catch(Exception ex)
            {
                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
            }
        }
        public Stars(int IdForType, string NewText, string TypeOfQuestion, int Order, int NumberOfStars)
        {
            try
            {
                this.NewText = NewText;
                this.NumberOfStars = NumberOfStars;
                this.Order = Order;
                this.TypeOfQuestion = TypeOfQuestion;
                this.IdForType = IdForType;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
            }
        }
        public Stars()
        {

        }


    }
}
