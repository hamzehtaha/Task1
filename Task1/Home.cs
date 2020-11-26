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
                StaticObjects.Erros.Log(ex);
            }
        }
        private void Add_Click(object sender, EventArgs e)
        {
            // to go to the Add page
            try
            {
               
                QuestionsInformation QuestionsInformationPage = new QuestionsInformation(QuestionWillDeleteOrEdit,TypeOfChoice.Add.ToString());
                QuestionsInformationPage.ShowDialog(); 
                ShowData(); 
            }catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        private void Edit_Click(object sender, EventArgs e)
        {
            // this function for go to the edit page and give me the id 
            try
            {
                if (QuestionWillDeleteOrEdit != null)
                {
                    QuestionsInformation QuestionsInformationPage = new QuestionsInformation(QuestionWillDeleteOrEdit,TypeOfChoice.Edit.ToString());
                    QuestionsInformationPage.ShowDialog();
                    ShowData();
                    QuestionWillDeleteOrEdit = null;
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
                        if (QuestionWillDeleteOrEdit.TypeOfQuestion == TypeOfQuestion.Slider.ToString())
                        {
                            slider = (Slider)QuestionWillDeleteOrEdit; 
                            DataBaseConnections.DeleteQuestion(slider,slider.IdForType);
                            StaticObjects.ListOfAllQuestion.Remove(slider);
                        }else if (QuestionWillDeleteOrEdit.TypeOfQuestion == TypeOfQuestion.Smily.ToString())
                        {
                            smiles = (Smiles)QuestionWillDeleteOrEdit;
                            DataBaseConnections.DeleteQuestion(smiles, smiles.IdForType);
                            StaticObjects.ListOfAllQuestion.Remove(smiles);
                        }else if (QuestionWillDeleteOrEdit.TypeOfQuestion == TypeOfQuestion.Stars.ToString())
                        {
                            stars = (Stars)QuestionWillDeleteOrEdit;
                            DataBaseConnections.DeleteQuestion(stars, stars.IdForType);
                            StaticObjects.ListOfAllQuestion.Remove(stars);
                        }
                        ShowData();


                    }
                        
                        
                    }
                
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
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
                StaticObjects.Erros.Log(ex);
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
                StaticObjects.Erros.Log(ex); 
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
