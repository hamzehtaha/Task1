using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;

namespace Survey
{
    public partial class Home : Form
    {
        public Qustions QuestionWillDeleteOrEdit = null; 
        public Home()
        {
            StartFunction(); 
        }
        private void StartFunction()
        {
            InitializeComponent();
            DataBaseConnections.GetQuestionFromDataBase(new Slider());
            DataBaseConnections.GetQuestionFromDataBase(new Smiles());
            DataBaseConnections.GetQuestionFromDataBase(new Stars());
            Qustions.GetData(ListOfQuestion);
        }
        private void Add_Click(object sender, EventArgs e)
        {
            // to go to the Add page
            try
            {
                
                QuestionsInformation f = new QuestionsInformation(ListOfQuestion, -1, Constant.Empty, Constant.ADD);
                f.Show();
            }catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void Edit_Click(object sender, EventArgs e)
        {
            // this function for go to the edit page and give me the id 
            try
            {
                if (QuestionWillDeleteOrEdit != null)
                {
                    QuestionsInformation f = new QuestionsInformation(ListOfQuestion, QuestionWillDeleteOrEdit.Id, QuestionWillDeleteOrEdit.TypeOfQuestion, Constant.EDIT);
                    f.Show();
                    QuestionWillDeleteOrEdit = null; 
                }
                else
                {
                    MessageBox.Show(Constant.NoSelectItem, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        /*
         foreach (DataGridViewRow row in ListOfQuestion.Rows)
                {
                    if ((bool)ListOfQuestion.Rows[row.Index].Cells[0].Value == true)
                    {
                        string s = ListOfQuestion.Rows[row.Index].Cells[1].Value.ToString();
                        for (int i = 0; i < StaticObjects.ListOfAllQuestion.Count; ++i)
                            if (s.Equals(StaticObjects.ListOfAllQuestion.ElementAt(i).NewText))
                                ListOfId.Add(StaticObjects.ListOfAllQuestion.ElementAt(i).Id);
                    }
                }
        
                            for (int i = 0; i < StaticObjects.ListOfAllQuestion.Count; ++i)
                            {
                                if (StaticObjects.ListOfAllQuestion.ElementAt(i).Id == ListOfId.ElementAt(j))
                                {
                                    if (StaticObjects.ListOfAllQuestion.ElementAt(i) is Slider)
                                    {
                                        slider = (Slider)StaticObjects.ListOfAllQuestion.ElementAt(i);
                                        DataBaseConnections.DeleteQuestion(slider,slider.Id, slider.IdForType);
                                        StaticObjects.ListOfAllQuestion.Remove(slider);
                                        break;
                                    }
                                    else if (StaticObjects.ListOfAllQuestion.ElementAt(i) is Smiles)
                                    {
                                        smiles = (Smiles)StaticObjects.ListOfAllQuestion.ElementAt(i);
                                        DataBaseConnections.DeleteQuestion(smiles, smiles.Id, smiles.IdForType);
                                        StaticObjects.ListOfAllQuestion.Remove(smiles);
                                        break;

                                    }
                                    else if (StaticObjects.ListOfAllQuestion.ElementAt(i) is Stars)
                                    {
                                        stars = (Stars)StaticObjects.ListOfAllQuestion.ElementAt(i);
                                        DataBaseConnections.DeleteQuestion(stars, stars.Id, stars.IdForType);
                                        StaticObjects.ListOfAllQuestion.RemoveAt(i);
                                        break;

                                    }
                                
                            }


         */
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
                    MessageBox.Show(Constant.NoSelectItem, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(Constant.SureToDeleteMessage, Constant.DELETE, MessageBoxButtons.YesNo);
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
                        Qustions.GetData(ListOfQuestion);


                    }
                        
                        
                    }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void changeToArabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Constant.Languge.Equals(Constant.English))
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Constant.ArabicMark);
                    Constant.Languge = Constant.Arabic;

                }
                else
                {
                    Constant.Languge = Constant.English;

                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Constant.EnglishMark);
                }
                this.Controls.Clear();
                StartFunction();
            }catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void ChooseTheQuestionClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (ListOfQuestion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ListOfQuestion.CurrentRow.Selected = true;
                    string Text = ListOfQuestion.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string Type = ListOfQuestion.Rows[e.RowIndex].Cells[1].Value.ToString();
                    int Order = Convert.ToInt32(ListOfQuestion.Rows[e.RowIndex].Cells[2].Value.ToString());
                    foreach (Qustions QustionTemp in StaticObjects.ListOfAllQuestion)
                    {
                        if (Text.Equals(QustionTemp.NewText) && Type.Equals(QustionTemp.TypeOfQuestion) && Order == QustionTemp.Order)
                        {
                            QuestionWillDeleteOrEdit = QustionTemp; 
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
    }
}
