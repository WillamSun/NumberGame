using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;

namespace NumberGame
{
    public partial class Form1 : Form
    {
        string SelectItemsString;
        int Help;
        int Score;
        int SelectItems;
        int MaxSecond = 15;
        int Second = 0;
        List<LinkLabel> SelectItemsList; 
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox4.Enabled = true; 
            Second = MaxSecond;
            SelectItemsList = new List<LinkLabel>();
            label11.Visible = false;
            label10.Visible = false;
            Score = 0;
            Help = 0;
            SelectItemsString = "";
            SelectItems = -114514;
            timer1.Start();
            timer2.Start();
            InitializeLabels();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (!(sender is LinkLabel)) return;
            checkBox4.CheckState = CheckState.Indeterminate;
            if (button3.Visible)
            {
                LinkLabel[] labels = Labels;
                foreach (LinkLabel lab in labels) lab.BorderStyle = BorderStyle.None;
                List<List<LinkLabel>> l = GetOKOffsetLabels();
                List<LinkLabel> ls = l[new Random().Next(l.Count - 1)];
                foreach (LinkLabel labe in ls)
                {
                    labe.BorderStyle = BorderStyle.FixedSingle;
                }
                return;
            }
            LinkLabel label = sender as LinkLabel;
            SelectItemsString = "";
            if (label.BorderStyle != BorderStyle.FixedSingle)
            {
                if (SelectItems == -114514)
                {
                    SelectItems = 0;
                }
                if (checkBox1.Checked)
                {
                    progressBar1.Visible = true;
                    label11.Visible = true;
                    label10.Visible = true;
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
            if (SelectItemsList.Count == 0) checkBox4.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            label11.Visible = false;
            label10.Visible = false;
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
                    if (Second <= 0) { lose("时间到了"); return; }
                    lose("还有可以抵消的数，可行的选择方案: \n" + text);
                    return;
                }
                InitializeLabels();
                return;
            }
            if (SelectItems != 0)
            {
                if (Second <= 0) { lose("时间到了"); return; }
                lose(SelectItemsString + " = " + SelectItems + " ≠ 0");
                return;
            }
            foreach (LinkLabel label in SelectItemsList)
            {
                label.Text = RandomLabelText();
                label.BorderStyle = BorderStyle.None;
                Thread.Sleep(10);
            }
            Second = MaxSecond;
            Score++;
            checkBox4.Checked = false;
            SelectItemsList.Clear();
            SelectItems = -114514;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double tmp;
            progressBar2.Value = (100/MaxSecond) * Second;
            progressBar2.Visible = checkBox2.Checked && !button3.Visible;
            if (checkBox1.Checked)
            {
                label10.Visible = true;
                label10.Text = "得数: " + SelectItems;
                if (SelectItems == 0) label11.ForeColor = label10.ForeColor = Color.Green;
                else label11.ForeColor = label10.ForeColor = Color.Black;
                if (SelectItems != -114514)
                {
                    progressBar1.Visible = true;
                    label11.Visible = true;
                    tmp = (Math.Abs(GetBiggestChoise()) > Math.Abs(GetBiggestChoise(false))) ? Math.Abs(GetBiggestChoise()) : Math.Abs(GetBiggestChoise(false));
                    tmp = 100 / Math.Abs(tmp) * Math.Abs(0 - SelectItems);
                    progressBar1.Value = (int)tmp;
                }
            }
            else
            {
                progressBar1.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
            }
            if (SelectItemsList.Count == 0) { SelectItems = -114514; progressBar1.Visible = false; label11.Visible = false; label10.Text = "未选择数字"; }
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
                        result.Add(tmp);
                    }
                }
            }
            return result;
        }

        private void lose(string reason)
        {
            timer2.Stop();
            button1.Visible =
                checkBox4.Checked =
                progressBar2.Visible =
                button4.Visible =
                checkBox1.Enabled =
                checkBox2.Enabled =
                checkBox3.Enabled =
                checkBox4.Enabled = false;
            button3.Visible = true;
            button3.Focus();
            LinkLabel[] labels = Labels;
            foreach (LinkLabel lab in labels) lab.BorderStyle = BorderStyle.None;
            MessageBox.Show(reason + "\n游戏结束", "你输了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (BSODToolStripMenuItem.Checked) BSOD();
            if (checkBox3.Checked) Environment.Exit(0);
        }

        int openHelpMenu = 0;
        private void Form1_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            checkBox5.Checked = true;
            e.Cancel = true;
            bool boole = button3.Visible || (Help >= 3) || checkBox3.Checked;
            openHelpMenu++;
            string HelpText = "玩法：\n" +
                    "打开软件后可以看到左上方的横式，右上方的分数，下方有两个按钮\n" +
                    "选择一些加起来等于0的数字（单独一个0也可以，或者-3，-2，+5这三个数也可以，不限项数）\n" +
                    "选择好后，下方的“检测是否死局”按钮会变成“抵消”\n" +
                    "如果所有选择的数相加等于0，按下“抵消”按钮即可得1分，如果选错了会结束游戏\n" +
                    "如果觉得陷入了死局，可以按下“检测是否死局”，如确实死局，可以重新生成一次，如没有，将会结束游戏" +
                    (boole ? (checkBox3.Checked ? "\n\n极限模式下不可使用提示" : (button3.Visible ? "\n\n游戏已经结束，不可使用提示。可以在游戏结束时单击等式上任意数字获取随机解" : ("\n\n已使用" + Help + "次提示，不可再次使用"))) : ("\n\n陷入迷茫？点击“是”获取提示\n" + "已使用" + Help + "次提示，为了游戏体验，只可使用3次提示，你还剩" + (3 - Help) + "次机会"));
            DialogResult dialogResult;
            switch (openHelpMenu)
            {
                case 5:
                    dialogResult = MessageBox.Show("这个菜单好看吗\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 6:
                    dialogResult = MessageBox.Show("别看了，有啥看头?\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 7:
                    dialogResult = MessageBox.Show("算了你看吧\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 8:
                    dialogResult = MessageBox.Show("...\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 9:
                    dialogResult = MessageBox.Show("真就继续看呗!\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 10:
                    dialogResult = MessageBox.Show("既然你这么喜欢这个菜单,就去GitHub给这个项目点个Star呗~\n点击“帮助”前往GitHub项目地址\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2,0,"https://github.com/WillamSun/NumberGame");
                    break;
                case 11:
                    dialogResult = MessageBox.Show("行行，我坦白，我确实sha人了\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    break;
                case 12:
                case 13:
                case 14:
                    dialogResult = MessageBox.Show("6\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 15:
                case 16:
                case 17:
                    dialogResult = MessageBox.Show("大哥佩服\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                case 18:
                case 19:
                    dialogResult = MessageBox.Show("爸爸\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    break;
                default:
                    if (openHelpMenu >= 20)
                    {
                        dialogResult = MessageBox.Show("傻逼别看了\n\n" + HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        break;
                    }
                    dialogResult = MessageBox.Show(HelpText, "帮助", boole ? MessageBoxButtons.OK : MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
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
            checkBox5.Checked = false;
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
            List<bool> bools = new List<bool>();
            foreach (ToolStripMenuItem toolStrip in 究极极限模式ToolStripMenuItem.DropDownItems) bools.Add(toolStrip.Checked);
            bool[] args = { checkBox1.Checked,checkBox2.Checked };
            timer1.Stop();
            timer2.Stop();
            Controls.Clear();
            InitializeComponent();
            Form1_Load(sender, e);
            checkBox1.Checked = args[0];
            checkBox2.Checked = args[1];
            for (int i = 0; i < bools.Count;i++) ((ToolStripMenuItem)究极极限模式ToolStripMenuItem.DropDownItems[i]).Checked = bools[i];
        }

        private int GetBiggestChoise(bool Biggest = true)
        {
            int BiggestInt = 0;
            LinkLabel[] labels = Labels;
            foreach (Label label in labels)
            {
                int i = int.Parse(label.Text.Replace("(", string.Empty).Replace(")", string.Empty));
                if (Biggest ? (i > 0) : (i < 0))
                {
                    BiggestInt += i;
                }
            }
            return BiggestInt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否开启新局，本局将会立刻结束","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                button3_Click(sender, e);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Second > 0 && checkBox2.Checked && !button3.Visible) Second--;
            if (Second <= 0)
            {
                button1_Click(sender, e);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Indeterminate) return;
            LinkLabel[] labels = Labels;
            SelectItemsString = "";
            if (checkBox4.Checked)
            {
                foreach (LinkLabel label in labels)
                {
                    if (label.BorderStyle != BorderStyle.FixedSingle)
                    {
                        if (SelectItems == -114514)
                        {
                            SelectItems = 0;
                        }
                        if (checkBox1.Checked)
                        {
                            progressBar1.Visible = true;
                            label11.Visible = true;
                            label10.Visible = true;
                        }
                        SelectItems += int.Parse(label.Text.Replace("(", string.Empty).Replace(")", string.Empty));
                        SelectItemsList.Add(label);
                        label.BorderStyle = BorderStyle.FixedSingle;
                        foreach (LinkLabel l in SelectItemsList)
                        {
                            if (SelectItemsList.Last() == l) SelectItemsString += l.Text;
                            else SelectItemsString += l.Text + " + ";
                        }
                    }
                }
                return;
            }
            foreach (LinkLabel label in labels)
            {
                if (label.BorderStyle != BorderStyle.None)
                {
                    SelectItems -= int.Parse(label.Text.Replace("(", string.Empty).Replace(")", string.Empty));
                    SelectItemsList.Remove(label);
                    label.BorderStyle = BorderStyle.None;
                    foreach (LinkLabel l in SelectItemsList)
                    {
                        if (SelectItemsList.Last() == l) SelectItemsString += l.Text;
                        else SelectItemsString += l.Text + " + ";
                    }
                }
            }
        }

        bool checkBox1Lastcheck = false;
        bool checkBox2Lastcheck = false;
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = checkBox2.Enabled = button4.Visible = !checkBox3.Checked;
            if (checkBox3.Checked)
            {

                if (checkBox3.Checked)
                {
                    checkBox1Lastcheck = checkBox1.Checked;
                    checkBox2Lastcheck = checkBox2.Checked;
                    button1.Size = new Size(568, 23);
                    checkBox2.Checked = true;
                    checkBox1.Checked = false;
                }
                return;
            }
            button1.Size = new Size(481, 23);
            checkBox2.Checked = checkBox2Lastcheck;
            checkBox1.Checked = checkBox1Lastcheck;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                foreach (Control control in Controls) control.Enabled = false;
                foreach (Control control in groupBox2.Controls) control.Enabled = false;
                groupBox2.Enabled = checkBox5.Enabled = true;
                LinkLabel[] ls = Labels;
                foreach (LinkLabel l in ls) l.Visible = false;
                label1.Text = "已暂停...";
                timer2.Stop();
            }
            else
            {
                foreach (Control control in Controls) control.Enabled = true;
                foreach (Control control in groupBox2.Controls) control.Enabled = true;
                checkBox1.Enabled = checkBox2.Enabled = !checkBox3.Checked;
                LinkLabel[] ls = Labels;
                foreach (LinkLabel l in ls) l.Visible = true;
                timer2.Start();
                label1.Text = "      +       +        +        +        +   ;    +";
            }
        }

        private void BSODToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
            checkBox5.Checked = true;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator) && BSODToolStripMenuItem.Checked == true)
            {
                MessageBox.Show("请先以管理员启动程序","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                BSODToolStripMenuItem.Checked = false;
                return;
            }
            if (BSODToolStripMenuItem.Checked && MessageBox.Show("警告(我劝你还是看看): \n本软件里（我不能保证其他软件也这么善良）的蓝屏指的是会导致电脑报错并关机，但是并不会损坏电脑，只不过是多了一个错误提示，只需正常重启即可。但即使是这样，也请慎重选择，因为这个操作可能会影响其他打开的软件的正常运行！所以为了减少损失，请在选择此选项前保存并关闭所有打开的软件。除此之外，我也不建议你经常这样做，开关机很麻烦\n别怪我没有警告你\n单击“是”以继续，在你输之前，你可以随时关闭这个功能\n此操作有可能会被杀毒软件拦截", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) BSODToolStripMenuItem.Checked = false; 
            checkBox5.Checked = false;
        }

        private void Second5ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false && Second5ToolStripMenuItem.Checked) { checkBox5.Checked = true; MessageBox.Show("请勾选“计时”以启用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); checkBox5.Checked = false; }
            if (Second10ToolStripMenuItem.Checked && Second5ToolStripMenuItem.Checked) Second10ToolStripMenuItem.Checked = false;
            if (Second5ToolStripMenuItem.Checked) { Second = MaxSecond = 5; }
        }

        private void Second10ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false && Second10ToolStripMenuItem.Checked) { checkBox5.Checked = true;MessageBox.Show("请勾选“计时”以启用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); checkBox5.Checked = false; }
            if (Second5ToolStripMenuItem.Checked && Second10ToolStripMenuItem.Checked) Second5ToolStripMenuItem.Checked = false;
            if (Second10ToolStripMenuItem.Checked) {Second = MaxSecond = 10; }
        }

        private void BSOD()
        {
            int isCritical = 1;  // we want this to be a Critical Process
            int BreakOnTermination = 0x1D;  // value for BreakOnTermination (flag)
            Process.EnterDebugMode();  //acquire Debug Privileges
            // setting the BreakOnTermination = 1 for the current process
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
        }
    }
}
