using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

namespace NumberGame
{
    public partial class Form1 : Form
    {
        int Help = 0;
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
            InitializeLabels();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (!(sender is Label)) return;
            Label label = sender as Label;
            if (label.BorderStyle != BorderStyle.FixedSingle)
            {
                if (SelectItems == -114514)
                {
                    SelectItems = 0;
                }
                SelectItems += int.Parse(label.Text.Replace("(", string.Empty).Replace(")", string.Empty));
                SelectItemsList.Add(label);
                label.BorderStyle = BorderStyle.FixedSingle;
                return;
            }
            SelectItems -= int.Parse(label.Text.Replace("(", string.Empty).Replace(")", string.Empty));
            SelectItemsList.Remove(label);
            label.BorderStyle = BorderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectItems == -114514)
            {
                if (GetOKOffsetLabels().Count != 0)
                {
                    MessageBox.Show("还有可以抵消的数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Score--;
                    return;
                }
                InitializeLabels();
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
            if (SelectItems == -114514) button1.Text = "检测是否死局";
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

        private void InitializeLabels()
        {
            foreach (Label label in Labels)
            {
                label.BorderStyle = BorderStyle.None;
                label.Text = RandomLabelText();
                Thread.Sleep(10);
            }
        }

        private List<List<Label>> GetOKOffsetLabels()
        {
            List<List<Label>> result = new List<List<Label>>();
            Label[] labels = Labels;
            for (int i = 1; i < labels.Length; i++) //选择数
            {
                List<List<int>> list = new List<List<int>>();
                list.AddRange(PermutationCombinations(Enumerable.Range(0,labels.Length).ToList(),i));
                foreach (List<int> ls in list)
                {
                    List<Label> tmp = new List<Label>();
                    int integer = 0;
                    foreach (int inte in ls)
                    {
                        integer += int.Parse(labels[inte].Text.Replace("(", string.Empty).Replace(")", string.Empty));
                    }
                    if (integer == 0)
                    {
                        foreach (int inte in ls)
                        {
                            tmp.Add(labels[inte]);
                        }
                    }
                    if (tmp.Count != 0) result.Add(tmp);
                }
            }
            return result;
        }

        private void Form1_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (MessageBox.Show("玩法：\n" +
                "打开软件后可以看到左上方的横式，右上方的分数，下方有两个按钮\n" +
                "选择一些加起来等于0的数字（单独一个0也可以，或者-3，-2，+5这三个数也可以，不限项数）\n" +
                "选择好后，下方的“检测是否死局”按钮会变成“抵消”\n" +
                "如果所有选择的数相加等于0，按下“抵消”按钮即可得1分\n" +
                "如果觉得陷入了死局，可以按下“检测是否死局”，如确实死局，可以重开一次，如没有，将会扣掉一分\n\n" +
                "陷入迷茫？点击“是”获取提示\n" +
                "已使用" + Help + "次提示", "帮助", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Help++;
                string text = "";
                List<List<Label>> l = GetOKOffsetLabels();
                foreach (List<Label> ls in l)
                {
                    foreach (Label label in ls)
                    {
                        if (ls.IndexOf(label) == ls.Count - 1)
                        {
                            text += label.Text + "\n";
                        }
                        else
                        {
                            text += label.Text + " + ";
                        }
                    }
                }
                if (text != "") MessageBox.Show("可行的选择方案: \n" + text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("无可行的选择方案，可以选择“检测是否死局”来重新生成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Label[] Labels
        {
            get
            {
                List<Label> labels = new List<Label>();
                foreach (Control control in groupBox1.Controls)
                {
                    if (!(control is Label) || control.Name == "label1") continue;
                    labels.Add(control as Label);
                }
                return labels.ToArray();
            }
        }

        private List<List<int>> PermutationCombinations(List<int> things, int countInOneGroup)
        {
            List<List<int>> result = new List<List<int>>();
            if (countInOneGroup == 1)
            {
                foreach (int i in things)
                {
                    result.Add(new List<int>() { things[i] });
                }
                return result;
            }
            for (int i = 0; i < things.Count - 1; i++)
            {
                for (int j = 1; j < things.Count; j++)
                {
                    if (i >= j) continue;
                    result.Add(new List<int>() { things[i], things[j] });
                }
            }
            if (countInOneGroup != 2)
            {
                List<List<int>> tmp = new List<List<int>>();
                tmp.AddRange(result);
                result.Clear();
                for (int i = 0; i < tmp.Count; i++)
                {
                    List<int> tmp2 = new List<int>() { -114514 };
                    for (int j = 2; j < things.Count; j++) tmp2.Add(things[j]);
                    List<List<int>> ls = PermutationCombinations(tmp2, countInOneGroup - 1);
                    for (int j = 0; j < ls.Count; j++)
                    {
                        if (!ls[j].Contains(-114514) || ls[j][1] <= tmp[i][1]) continue;
                        List<int> tmp3 = new List<int>()
                        {
                            tmp[i][0],
                            tmp[i][1]
                        };
                        foreach (int l in ls[j]) if (l != -114514) tmp3.Add(l);
                        result.Add(tmp3);
                    }
                }
            }
            return result;
        }
    }
}
