using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

namespace NumberGame
{
    public partial class Form1 : Form
    {
        string SelectItemsString;
        int Help;
        int Score;
        int SelectItems;
        List<LinkLabel> SelectItemsList;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SelectItemsList = new List<LinkLabel>();
            Score = 0;
            Help = 0;
            SelectItemsString = "";
            SelectItems = -114514;
            timer1.Start();
            InitializeLabels();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (!(sender is LinkLabel)) return;
            LinkLabel label = sender as LinkLabel;
            SelectItemsString = "";
            if (label.BorderStyle != BorderStyle.FixedSingle)
            {
                if (SelectItems == -114514)
                {
                    SelectItems = 0;
                }
                SelectItems += int.Parse(label.Text.Replace("(", string.Empty).Replace(")", string.Empty));
                SelectItemsList.Add(label);
                label.BorderStyle = BorderStyle.FixedSingle;
                foreach (LinkLabel l in SelectItemsList)
                {
                    if (SelectItemsList.Last() == l) SelectItemsString += l.Text;
                    else SelectItemsString += l.Text + " + ";
                }
                return;
            }
            SelectItems -= int.Parse(label.Text.Replace("(", string.Empty).Replace(")", string.Empty));
            SelectItemsList.Remove(label);
            label.BorderStyle = BorderStyle.None;
            foreach (LinkLabel l in SelectItemsList)
            {
                if (SelectItemsList.Last() == l) SelectItemsString += l.Text;
                else SelectItemsString += l.Text + " + ";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button3.Visible == true)
            {
                MessageBox.Show("都输了还想作弊","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (SelectItems == -114514)
            {
                if (GetOKOffsetLabels().Count != 0)
                {
                    string text = "";
                    List<List<LinkLabel>> l = GetOKOffsetLabels();
                    foreach (List<LinkLabel> ls in l)
                    {
                        foreach (LinkLabel label in ls)
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
                    MessageBox.Show("还有可以抵消的数，可行的选择方案: \n" + text + "\n游戏结束", "你输了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button3.Visible = true;
                    button3.Focus();
                    return;
                }
                InitializeLabels();
                return;
            }
            if (SelectItems != 0)
            {
                MessageBox.Show(SelectItemsString + " = " + SelectItems + " ≠ 0" + "\n游戏结束", "你输了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button3.Visible = true;
                button3.Focus();
                return;
            }
            foreach (LinkLabel label in SelectItemsList)
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
            foreach (LinkLabel label in Labels)
            {
                label.BorderStyle = BorderStyle.None;
                label.Text = RandomLabelText();
                Thread.Sleep(10);
            }
        }

        private List<List<LinkLabel>> GetOKOffsetLabels()
        {
            List<List<LinkLabel>> result = new List<List<LinkLabel>>();
            LinkLabel[] labels = Labels;
            for (int i = 1; i < labels.Length; i++) //选择数
            {
                List<List<int>> list = new List<List<int>>();
                list.AddRange(PermutationCombinations(Enumerable.Range(0,labels.Length).ToList(),i));
                foreach (List<int> ls in list)
                {
                    List<LinkLabel> tmp = new List<LinkLabel>();
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

        int openHelpMenu = 0;
        private void Form1_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            openHelpMenu++;
            string HelpText = "玩法：\n" +
                    "打开软件后可以看到左上方的横式，右上方的分数，下方有两个按钮\n" +
                    "选择一些加起来等于0的数字（单独一个0也可以，或者-3，-2，+5这三个数也可以，不限项数）\n" +
                    "选择好后，下方的“检测是否死局”按钮会变成“抵消”\n" +
                    "如果所有选择的数相加等于0，按下“抵消”按钮即可得1分，如果选错了会结束游戏\n" +
                    "如果觉得陷入了死局，可以按下“检测是否死局”，如确实死局，可以重新生成一次，如没有，将会结束游戏" +
                    ((Help >= 3) ? ("\n\n已使用" + Help + "次提示，不可再次使用") : ("\n\n陷入迷茫？点击“是”获取提示\n" + "已使用" + Help + "次提示，为了游戏体验，只可使用3次提示，你还剩" + (3 - Help) + "次机会"));
            DialogResult dialogResult;
            switch (openHelpMenu)
            {
                case 5:
                    dialogResult = MessageBox.Show("这个菜单好看吗\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 6:
                    dialogResult = MessageBox.Show("别看了，有啥看头?\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 7:
                    dialogResult = MessageBox.Show("算了你看吧\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 8:
                    dialogResult = MessageBox.Show("...\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 9:
                    dialogResult = MessageBox.Show("真就继续看呗!\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 10:
                    dialogResult = MessageBox.Show("既然你这么喜欢这个菜单,就去GitHub给这个项目点个Star呗~\n点击“帮助”前往GitHub项目地址\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2,0,"https://github.com/WillamSun/NumberGame");
                    break;
                case 11:
                    dialogResult = MessageBox.Show("行行，我坦白，我确实sha人了\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    break;
                case 12:
                case 13:
                case 14:
                    dialogResult = MessageBox.Show("6\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 15:
                case 16:
                case 17:
                    dialogResult = MessageBox.Show("大哥佩服\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 18:
                case 19:
                    dialogResult = MessageBox.Show("爸爸\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                default:
                    if (openHelpMenu >= 20)
                    {
                        dialogResult = MessageBox.Show("傻逼别看了\n\n" + HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        break;
                    }
                    dialogResult = MessageBox.Show(HelpText, "帮助", (Help >= 3) ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
            }
            if (dialogResult == DialogResult.Yes)
            {
                Help++;
                string text = "";
                List<List<LinkLabel>> l = GetOKOffsetLabels();
                foreach (List<LinkLabel> ls in l)
                {
                    foreach (LinkLabel label in ls)
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

        private LinkLabel[] Labels
        {
            get
            {
                List<LinkLabel> labels = new List<LinkLabel>();
                foreach (Control control in groupBox1.Controls)
                {
                    if (!(control is LinkLabel)) continue;
                    labels.Add(control as LinkLabel);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
            Form1_Load(sender, e);
        }
    }
}
