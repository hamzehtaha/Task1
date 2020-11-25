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
    class Slider : Qustions
    {
        public int IdForType{get; set;}
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public string StartCaption { get; set; }
        public string EndCaption { get; set; }
        public Slider(int Id, int IdForType, string NewText, string TypeOfQuestion, int Order, int StartValue, int EndValue, string StartCaption, string EndCaption) {
            try
            {
                this.NewText = NewText;
                this.Order = Order;
                this.StartValue = StartValue;
                this.EndValue = EndValue;
                this.StartCaption = StartCaption;
                this.EndCaption = EndCaption;
                this.Id = Id;
                this.TypeOfQuestion = TypeOfQuestion;
                this.IdForType = IdForType;
            }catch (Exception ex)
            {
                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
            }
        }
        public Slider(string NewText, string TypeOfQuestion, int IdForType,int Order, int StartValue, int EndValue, string StartCaption, string EndCaption)
        {
            try
            {
                this.NewText = NewText;
                this.Order = Order;
                this.StartValue = StartValue;
                this.EndValue = EndValue;
                this.StartCaption = StartCaption;
                this.EndCaption = EndCaption;
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
        public Slider()
        {

        }



    }
}
