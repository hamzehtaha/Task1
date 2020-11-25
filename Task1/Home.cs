using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;
namespace Survey
{
    public partial class Home : Form
    {
        private Qustions QuestionWillDeleteOrEdit = null; 
        public Home()
        {
            StartFunction(); 
        }
        private void StartFunction()
        {
            InitializeComponent();
            DataBaseConnections.GetQuestionFromDataBase(); 
            ShowData(); 
        }
        private void ShowData()
        {
            try
            {
                ListOfQuestion.Rows.Clear();
                foreach (Qustions Temp in StaticObjects.ListOfAllQuestion)
                {

                    int Index = ListOfQuestion.Rows.Add();
                    ListOfQuestion.Rows[Index].Cells[0].Value = Temp.NewText;
                    ListOfQuestion.Rows[Index].Cells[2].Value = Temp.Order;
                    ListOfQuestion.Rows[Index].Cells[1].Value = Temp.TypeOfQuestion;
                }
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
        private void Add_Click(object sender, EventArgs e)
        {
            // to go to the Add page
            try
            {
               
                QuestionsInformation f = new QuestionsInformation(ListOfQuestion, QuestionWillDeleteOrEdit,TypeOfChoice.Add.ToString());
                f.Show();
                ShowData(); 
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
        private void Edit_Click(object sender, EventArgs e)
        {
            // this function for go to the edit page and give me the id 
            try
            {
                if (QuestionWillDeleteOrEdit != null)
                {
                    QuestionsInformation f = new QuestionsInformation(ListOfQuestion,QuestionWillDeleteOrEdit,TypeOfChoice.Edit.ToString());
                    f.Show();
                    QuestionWillDeleteOrEdit = null;
                }
                else
                {
                    MessageBox.Show(Survey.Properties.Resource1.NoSelectItem, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

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
        private void Delete_Click(object sender, EventArgs e)
        {
            // this function for delete the answer 
            try
            {
                Slider slider = null;
                Stars stars = null;
                Smiles smiles = null;
                
                if (QuestionWillDeleteOrEdit == null)
                {
                    MessageBox.Show(Survey.Properties.Resource1.NoSelectItem, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(Survey.Properties.Resource1.SureToDeleteMessage, Constant.DELETE, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes )
                    {
                        if (QuestionWillDeleteOrEdit is Slider)
                        {
                            slider = (Slider)QuestionWillDeleteOrEdit; 
                            DataBaseConnections.DeleteQuestion(slider, slider.Id,slider.IdForType);
                            StaticObjects.ListOfAllQuestion.Remove(slider);
                        }else if (QuestionWillDeleteOrEdit is Smiles)
                        {
                            smiles = (Smiles)QuestionWillDeleteOrEdit;
                            DataBaseConnections.DeleteQuestion(smiles, smiles.Id, smiles.IdForType);
                            StaticObjects.ListOfAllQuestion.Remove(smiles);
                        }else if (QuestionWillDeleteOrEdit is Stars)
                        {
                            stars = (Stars)QuestionWillDeleteOrEdit;
                            DataBaseConnections.DeleteQuestion(stars, stars.Id, stars.IdForType);
                            StaticObjects.ListOfAllQuestion.Remove(stars);
                        }
                        ShowData();


                    }
                        
                        
                    }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
                StaticObjects.DoneOrNot = Constant.Not;

            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void changeToArabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (StaticObjects.Languge.Equals(Langugaes.English.ToString()))
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Constant.ArabicMark);
                    StaticObjects.Languge = Langugaes.Arabic.ToString();
                    StaticObjects.ListOfAllQuestion.Clear(); 
                }
                else
                {
                    StaticObjects.Languge = Langugaes.English.ToString();
                    StaticObjects.ListOfAllQuestion.Clear();
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Constant.EnglishMark);
                }
                this.Controls.Clear();
                StartFunction();
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
        private void ChooseTheQuestionClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataBaseConnections.GetQuestionFromDataBase(); 
                foreach (Qustions Temp in StaticObjects.ListOfAllQuestion)
                {
                    if (Temp.NewText.Equals(ListOfQuestion.CurrentRow.Cells[0].Value) && Temp.TypeOfQuestion.Equals(ListOfQuestion.CurrentRow.Cells[1].Value) && Temp.Order == Convert.ToInt32(ListOfQuestion.CurrentRow.Cells[2].Value))
                    {
                        QuestionWillDeleteOrEdit = Temp; 
                        
                    }
                }
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

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
