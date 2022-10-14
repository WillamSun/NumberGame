using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace NumberGame
{
    public partial class Form1 : Form
    {
        int Score = 0;
        int SelectItems = -114514;
        List<Label> SelectItemsList = new List<Label> { };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            foreach (Label label in new List<Label> {label2,label3,label4,label5,label6,label7,label8})
            {
                label.BorderStyle = BorderStyle.None;
                label.Text = RandomLabelText();
                Thread.Sleep(10);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.BorderStyle != BorderStyle.FixedSingle)
            {
                if (SelectItems == -114514)
                {
                    SelectItems = 0;
                }
                SelectItems += int.Parse((label).Text.Replace("(", string.Empty).Replace(")", string.Empty));
                SelectItemsList.Add(label);
                label.BorderStyle = BorderStyle.FixedSingle;
                return;
            }
            SelectItems -= int.Parse((label).Text.Replace("(", string.Empty).Replace(")", string.Empty));
            SelectItemsList.Remove(label);
            label.BorderStyle = BorderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectItems == -114514)
            {
                foreach (Label label in new List<Label> { label2, label3, label4, label5, label6, label7, label8 })
                {
                    label.BorderStyle = BorderStyle.None;
                    label.Text = RandomLabelText();
                    Thread.Sleep(10);
                }
                Score = 0;
                return;
            }
            foreach (Label label in SelectItemsList)
            {
                label.Text = RandomLabelText();
                label.BorderStyle = BorderStyle.None;
                Thread.Sleep(10);
            }
            Score++;
            SelectItemsList.Clear();
            SelectItems = -114514;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SelectItemsList.Count == 0) SelectItems = -114514;
            if (SelectItems == -114514) button1.Text = "重开一局";
            else button1.Text = "抵消";
            if (SelectItems == 0 || SelectItems == -114514) button1.Enabled = true;
            else button1.Enabled = false;
            label9.Text = Score.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private string RandomLabelText()
        {
            int integer = new Random().Next(-9, 9);
            if (integer < 0)
            {
                return "(" + integer + ")";
            }
            else if (integer == 0)
            {
                return "(0)";
            }
            else
            {
                return "(+" + integer + ")";
            }
        }
    }
}
