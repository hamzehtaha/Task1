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
        private static List<Qustions> ListOfAllQuestion = new List<Qustions>();
        private static string Languge = "English";
        public Home()
        {
            StartFunction(); 
        }
        /// <summary>
        /// The start function for open a home page and get data is already saved in database and show it in datagridview
        /// </summary>
        private void StartFunction()
        {
            InitializeComponent();
            ListOfAllQuestion.Clear();
            ListOfAllQuestion=DataBaseConnections.GetQuestionFromDataBase(); 
            ShowData(); 
        }
        /// <summary>
        /// Show data function get the question from MyList and show it in datagridview
        /// </summary>
        private void ShowData()
        {
            try
            {
                ListOfQuestion.Rows.Clear();
                foreach (Qustions Temp in ListOfAllQuestion)
                {
                    
                    int Index = ListOfQuestion.Rows.Add();
                    ListOfQuestion.Rows[Index].Cells[0].Value = Temp.NewText;
                    ListOfQuestion.Rows[Index].Cells[2].Value = Temp.Order;
                    ListOfQuestion.Rows[Index].Cells[1].Value = Temp.TypeOfQuestion;
                }
            }catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// This Listener for Add button when press add button 
        /// </summary>
        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
               
                QuestionsInformation QuestionsInformationPage = new QuestionsInformation(QuestionWillDeleteOrEdit,TypeOfChoice.Add.ToString());
                QuestionsInformationPage.ShowDialog();
                if (StaticObjects.SuccOfFail == 1)
                {
                    ListOfAllQuestion.Add(QuestionsInformationPage.ReturnNewQuestion);
                    ShowData();
                    StaticObjects.SuccOfFail = 0;
                }
            }catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// This Listener for Edit button when press add button
        /// </summary>
        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                QuestionWillDeleteOrEdit =  GetObjectSelect(); 
                if (QuestionWillDeleteOrEdit != null)
                {
                    
                    QuestionsInformation QuestionsInformationPage = new QuestionsInformation(QuestionWillDeleteOrEdit,TypeOfChoice.Edit.ToString());
                    QuestionsInformationPage.ShowDialog();
                    if (StaticObjects.SuccOfFail == 1)
                    {
                        ListOfAllQuestion.Remove(QuestionWillDeleteOrEdit);
                        ListOfAllQuestion.Add(QuestionsInformationPage.ReturnNewQuestion);
                        ShowData();
                        StaticObjects.SuccOfFail = 0; 
                    }
                }
                else
                {
                    MessageBox.Show(Survey.Properties.Resource1.NoSelectItem, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// This Listener for delete button when press add button
        /// </summary>
        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                Slider slider = null;
                Stars stars = null;
                Smiles smiles = null;
                QuestionWillDeleteOrEdit = GetObjectSelect();
                if (QuestionWillDeleteOrEdit == null)
                {
                    MessageBox.Show(Survey.Properties.Resource1.NoSelectItem, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(Survey.Properties.Resource1.SureToDeleteMessage, Constant.DELETE, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (QuestionWillDeleteOrEdit.TypeOfQuestion == TypeOfQuestion.Slider.ToString())
                        {
                            slider = (Slider)QuestionWillDeleteOrEdit;
                            DataBaseConnections.DeleteQuestion(slider, slider.IdForType);
                            ListOfAllQuestion.Remove(slider);
                        }
                        else if (QuestionWillDeleteOrEdit.TypeOfQuestion == TypeOfQuestion.Smily.ToString())
                        {
                            smiles = (Smiles)QuestionWillDeleteOrEdit;
                            DataBaseConnections.DeleteQuestion(smiles, smiles.IdForType);
                            ListOfAllQuestion.Remove(smiles);
                        }
                        else if (QuestionWillDeleteOrEdit.TypeOfQuestion == TypeOfQuestion.Stars.ToString())
                        {
                            stars = (Stars)QuestionWillDeleteOrEdit;
                            DataBaseConnections.DeleteQuestion(stars, stars.IdForType);
                            ListOfAllQuestion.Remove(stars);
                        }
                        ShowData();


                    }


                }

            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
                StaticObjects.SuccOfFail = 0;

            }
        }
        /// <summary>
        /// This function will return object is select in datagridview for edit and delete 
        /// </summary>
        /// <returns></returns>
        private Qustions GetObjectSelect()
        {
            try
            {
                foreach (Qustions Temp in ListOfAllQuestion)
                {
                    if (Temp.NewText.Equals(ListOfQuestion.SelectedCells[0].Value) && Temp.TypeOfQuestion.Equals(ListOfQuestion.SelectedCells[1].Value) && Temp.Order == Convert.ToInt32(ListOfQuestion.SelectedCells[2].Value))
                    {
                        
                        return Temp;

                    }

                }
                return null; 
            }
            catch (Exception ex) 
            {
                StaticObjects.Erros.Log(ex);
                return null;
            }
            
        }

        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// This for change language from arabic to english and english to arabic  
        /// /// </summary>
        private void changeToArabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Languge.Equals(Langugaes.English.ToString()))
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Constant.ArabicMark);
                    Languge = Langugaes.Arabic.ToString();
                    ListOfAllQuestion.Clear(); 
                }
                else
                {
                    Languge = Langugaes.English.ToString();
                    ListOfAllQuestion.Clear();
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Constant.EnglishMark);
                }
                this.Controls.Clear();
                StartFunction();
            }catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        private void ChooseTheQuestionClick(object sender, DataGridViewCellEventArgs e)
        {
          /*  try
            {
                DataBaseConnections.GetQuestionFromDataBase(); 
                foreach (Qustions Temp in ListOfAllQuestion)
                {
                    if (Temp.NewText.Equals(ListOfQuestion.CurrentRow.Cells[0].Value) && Temp.TypeOfQuestion.Equals(ListOfQuestion.CurrentRow.Cells[1].Value) && Temp.Order == Convert.ToInt32(ListOfQuestion.CurrentRow.Cells[2].Value))
                    {
                        QuestionWillDeleteOrEdit = Temp; 
                        
                    }
                }
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex); 
            }*/
        }
        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
