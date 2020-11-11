﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Survey;

namespace Task1
{
    public partial class QuestionsInformation : Form
    {
        public DataGridView ListOfQuestion;
        Slider NewSlider = null;
        Smiles NewSmile = null;
        Stars NewStars = null;
        string[] Attrubite = null;
        private void ShowForSlider()
        {
            try
            {
                InitHide();
                GroupOfSlider.Visible = true; 
            }catch (Exception ex)
            {

            }
        }
        private void ShowForSmiles()
        {
            try
            {
                InitHide();
                panel2.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        private void ShowForStars()
        {
            try
            {
                InitHide();
                   panel1.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        public QuestionsInformation(DataGridView ListOfQuestion, int Id, string Type, int AddOrEdit)
        {
            InitializeComponent();
            this.ListOfQuestion = ListOfQuestion;
            Attributes.Id = Id;
            Attributes.Type = Type;
            Attributes.AddOrEdit = AddOrEdit;
            try
            {
                if (AddOrEdit == 2)
                {
                    GroupOfTypes.Visible = false;
                    GetObject(); 

                }
                else
                {
                    GroupOfTypes.Visible = true;
                    InitHide(); 
                }
                if (Type.Equals(Attributes.Variables.Slider.ToString()))
                {
                    ShowForSlider(); 
                }
                else if (Type.Equals(Attributes.Variables.Smily.ToString()))
                {
                    ShowForSmiles(); 
                }
                else if (Type.Equals(Attributes.Variables.Stars.ToString()))
                {
                    ShowForStars(); 

                }
            }catch (Exception ex)
            {

            }
        }
        private void InitHide()
        {
            try
            {
                GroupOfSlider.Visible = false ;
                panel2.Visible = false;
                panel1.Visible = false; 
            }catch (Exception ex)
            {

            }

        }
        private bool IsNumber(string Number)
        {
            // to check the string is number or not ?
            
            return int.TryParse(Number, out int N);

        }
        private void QuestionsInformation_Load(object sender, EventArgs e)
        {

        }
        private void questionDetalis1_Load(object sender, EventArgs e)
        {

        }
        private void Slider_CheckedChange(object sender, EventArgs e)
        {
            // for radio Button cahnges 
            try
            {
                if (SliderRadio.Checked == true)
                {
                    ShowForSlider();
                }
            } catch (Exception ex)
            {

            }
        }
        private void Smily_CheckedChange(object sender, EventArgs e)
        {
            // for radio Button cahnges 
            try
            {
                if (SmilyRadio.Checked == true)
                {
                    ShowForSmiles(); 
                }
            } catch (Exception ex)
            {

            }
        }
        private void Stars_CheckedChange(object sender, EventArgs e)
        {
            // for radio Button cahnges
            try
            {
                if (StarsRadio.Checked == true)
                {
                    ShowForStars(); 

                }
            } catch (Exception ex)
            {

            }
        }
        private void GetObject()
        {
            try
            {
                for (int i = 0; i < Attributes.ListOfAllQuestion.Count; ++i)
                {
                    if (Attributes.ListOfAllQuestion.ElementAt(i).Id == Attributes.Id)
                    {
                        if (Attributes.ListOfAllQuestion.ElementAt(i) is Slider)
                        {
                            NewSlider = (Slider)Attributes.ListOfAllQuestion.ElementAt(i);
                            NewText.Text = NewSlider.NewText;
                            NewOrder.Value = NewSlider.Order;
                            NewStartValue.Value = NewSlider.StartValue;
                            NewEndValue.Value = NewSlider.EndValue;
                            NewStartValueCaption.Text = NewSlider.StartCaption;
                            NewEndValueCaption.Text = NewSlider.EndCaption;
                        }
                        else if (Attributes.ListOfAllQuestion.ElementAt(i) is Smiles)
                        {
                            NewSmile = (Smiles)Attributes.ListOfAllQuestion.ElementAt(i);
                            NewText.Text = NewSmile.NewText;
                            NewOrder.Value = NewSmile.Order;
                            NewNumberOfSmiles.Value = NewSmile.NumberOfSmiles;
                        }else
                        {
                            NewStars = (Stars)Attributes.ListOfAllQuestion.ElementAt(i);
                            NewText.Text = NewStars.NewText;
                            NewOrder.Value = NewStars.Order;
                            NewNumberOfStars.Value = NewStars.NumberOfStars;
                        }
                    }
                }
            }catch (Exception ex)
            {
            }
        }
        private void DataEnter()
        {
            MessageBox.Show("Your Data is Enterd");
            Qustions.GetData(ListOfQuestion);
            this.Close();

        }
        private bool CheckTheData( Qustions TypeQuestion)
        {
            try
            {
                if (NewText.Text == "")
                {
                    MessageBox.Show("Your Question is Empty", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    return false;
                }
                else if (IsNumber(NewText.Text))
                {
                    MessageBox.Show("Your Question just numbers  ", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                else if (NewOrder.Value <= 0)
                {
                    MessageBox.Show("The order sholud be grater than 0", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (TypeQuestion is Slider)
                {
                    if (NewStartValue.Value <= 0)
                    {
                        MessageBox.Show("Your start value must be greater than 0", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValue.Value <= 0)
                    {
                        MessageBox.Show("Your end value must be greater than 0", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValue.Value > 100)
                    {
                        MessageBox.Show("The start value should be less than 100", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValue.Value > 100)
                    {
                        MessageBox.Show("The end value should be less than 100", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValue.Value >= NewEndValue.Value)
                    {
                        MessageBox.Show("The end value should be grater than start value", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValueCaption.Text == "")
                    {
                        MessageBox.Show("The start caption is empty", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (IsNumber(NewStartValueCaption.Text))
                    {
                        MessageBox.Show("The start caption should not contain only numbers", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValueCaption.Text == "")
                    {
                        MessageBox.Show("The end caption is empty", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (IsNumber(NewEndValueCaption.Text))
                    {
                        MessageBox.Show("The end caption should not contain only numbers", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (TypeQuestion is Smiles)
                {
                    if (NewNumberOfSmiles.Value <= 1 || NewNumberOfSmiles.Value > 5)
                    {
                        MessageBox.Show("The number of smile face should be greater than 1 and less than 6", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (TypeQuestion is Stars)
                {
                    if (NewNumberOfStars.Value <= 0 || NewNumberOfStars.Value > 10)
                    {
                        MessageBox.Show("The number of stars face should be greater than 0 and less than or equal 10", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }catch (Exception ex)
            {

            }
            return true; 
        }
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (Attributes.AddOrEdit == 1)
                {
                    Qustions NewQuestion = null;
                    
                    bool f = false;
                    if (SliderRadio.Checked)
                    {
                        NewQuestion = Attributes.QusFac.GetQustion(Attributes.Variables.Slider.ToString());
                        f = CheckTheData(NewQuestion);
                        if (f)
                        {
                            Attrubite = new string[6];
                            Attrubite[0] = NewText.Text;
                            Attrubite[1] = NewOrder.Text;
                            Attrubite[2] = NewStartValue.Text;
                            Attrubite[3] = NewEndValue.Text;
                            Attrubite[4] = NewStartValueCaption.Text;
                            Attrubite[5] = NewEndValueCaption.Text;
                        }
                    }
                    else if (SmilyRadio.Checked)
                    {

                        NewQuestion = Attributes.QusFac.GetQustion(Attributes.Variables.Smily.ToString());
                        f = CheckTheData(NewQuestion);
                        if (f)
                        {
                            Attrubite = new string[3];
                            Attrubite[0] = NewText.Text;
                            Attrubite[1] = NewOrder.Text;
                            Attrubite[2] = NewNumberOfSmiles.Text;


                        }
                    }
                    else if (StarsRadio.Checked)
                    {
                        NewQuestion = Attributes.QusFac.GetQustion(Attributes.Variables.Stars.ToString());
                        f = CheckTheData(NewQuestion);
                        if (f)
                        {
                            Attrubite = new string[3];
                            Attrubite[0] = NewText.Text;
                            Attrubite[1] = NewOrder.Text;
                            Attrubite[2] = NewNumberOfStars.Text;

                        }
                    }
                    if (f)
                    {
                        NewQuestion.AddQuestion(Attrubite);
                        DataEnter(); 
                    }
                }
            }catch (Exception ex)
            {

            }
            try { 
                 if (Attributes.AddOrEdit == 2)
                 {
                    string[] Attrubite = null;
                    if (NewSlider != null)
                    {
                        if (CheckTheData(NewSlider))
                        {
                            Attrubite[0] = NewText.Text;
                            Attrubite[1] = NewOrder.Value + "";
                            Attrubite[2] = NewStartValue.Value + "";
                            Attrubite[3] = NewEndValue.Value + "";
                            Attrubite[4] = NewStartValueCaption + "";
                            Attrubite[5] = NewEndValueCaption + "";
                            Attrubite[6] = NewSlider.IdForType + "";
                            Attrubite[7] = NewSlider.Id + "";
                            NewSlider.NewText = Attrubite[0];
                            NewSlider.Order = Convert.ToInt32(Attrubite[1]);
                            NewSlider.StartValue = Convert.ToInt32(Attrubite[2]);
                            NewSlider.EndValue = Convert.ToInt32(Attrubite[3]);
                            NewSlider.StartCaption = Attrubite[4];
                            NewSlider.EndCaption = Attrubite[5];
                            NewSlider.EditQuestion(Attrubite);
                            DataEnter();
                        } 

                    }
                    else if (NewSmile != null)
                    {
                        if (CheckTheData(NewSmile))
                        {
                            Attrubite = new string[5];
                            Attrubite[0] = NewText.Text;
                            Attrubite[1] = NewOrder.Value + "";
                            Attrubite[2] = NewNumberOfSmiles.Value + "";
                            Attrubite[3] = NewSmile.IdForType + "";
                            Attrubite[4] = NewSmile.Id + "";
                            NewSmile.NewText = Attrubite[0];
                            NewSmile.Order = Convert.ToInt32(Attrubite[1]);
                            NewSmile.NumberOfSmiles = Convert.ToInt32(Attrubite[2]);
                            NewSmile.EditQuestion(Attrubite);
                            DataEnter();

                        }
                    }
                    else if (NewStars != null)
                    {
                        if (CheckTheData(NewStars))
                        {
                            Attrubite = new string[5];
                            Attrubite[0] = NewText.Text;
                            Attrubite[1] = NewOrder.Value + "";
                            Attrubite[2] = NewNumberOfStars.Value + "";
                            Attrubite[3] = NewStars.IdForType + "";
                            Attrubite[4] = NewStars.Id + "";
                            NewStars.NewText = Attrubite[0];
                            NewStars.Order = Convert.ToInt32(Attrubite[1]);
                            NewStars.NumberOfStars = Convert.ToInt32(Attrubite[2]);
                            NewStars.EditQuestion(Attrubite);
                            DataEnter();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Your Data not updated something went wrong "); 
            }
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NewNumberOfSmiles_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
