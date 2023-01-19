using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;
using System.Runtime.CompilerServices;

namespace NumberGame
{
    public partial class Form1 : Form
    {
        string SelectItemsString;
        int Help;
        int Score;
        int SelectItems;
        bool Reseting = false;
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
            Selectlabel(sender as LinkLabel);
        }

        private void Selectlabel(LinkLabel label, bool flagforcheckBox4 = false)
        {
            if (!flagforcheckBox4) checkBox4.CheckState = CheckState.Indeterminate;
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
            SelectItemsString = "";
            if (label.BorderStyle != BorderStyle.FixedSingle)
            {
                /*if (SelectItems == Max) checkBox4.Checked = true;
                else if (SelectItems == -114514) checkBox4.Checked = false;
                else checkBox4.CheckState = CheckState.Indeterminate;*/
                if (SelectItems == -114514) SelectItems = 0;
                if (checkBox1.Checked)
                {
                    progressBar1.Visible = true;
                    label11.Visible = true;
                    label10.Visible = true;
                }
                SelectItems += ParesString(label.Text);
                SelectItemsList.Add(label);
                label.BorderStyle = BorderStyle.FixedSingle;
                foreach (LinkLabel l in SelectItemsList)
                {
                    if (SelectItemsList.Last() == l) SelectItemsString += l.Text;
                    else SelectItemsString += l.Text + " + ";
                }
                if (SelectItemsList.Count == 7 && !flagforcheckBox4) checkBox4.CheckState = CheckState.Checked;
                return;
            }
            SelectItems -= ParesString(label.Text);
            SelectItemsList.Remove(label);
            label.BorderStyle = BorderStyle.None;
            foreach (LinkLabel l in SelectItemsList)
            {
                if (SelectItemsList.Last() == l) SelectItemsString += l.Text;
                else SelectItemsString += l.Text + " + ";
            }
            if (SelectItemsList.Count == 0 && !flagforcheckBox4) checkBox4.Checked = false;
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
            if ((SelectItems != 0) && !(((SelectItems > 63) && (SelectItems < 700)) || ((SelectItems > 700) && (SelectItems < 7000))))
            {
                if (Second <= 0) { lose("时间到了"); return; }
                lose(SelectItemsString + " = " + SelectItems + " ≠ 0");
                return;
            }
            if (!((SelectItems > 63) && (SelectItems < 700))) foreach (LinkLabel label in SelectItemsList)
                {
                    label.Text = RandomLabelText(ToolsToolStripMenuItem.Checked);
                    label.BorderStyle = BorderStyle.None;
                    Thread.Sleep(10);
                }
            if (!((SelectItems > 700) && (SelectItems < 7000))) Score++;
            if ((SelectItems > 63) && (SelectItems < 700)) { lose("系统错误，无法识别NaN，为了保护程序自动结束本轮游戏"); return; }
            Second = MaxSecond;
            checkBox4.Checked = false;
            SelectItemsList.Clear();
            SelectItems = -114514;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double tmp;
            progressBar2.Value = 100 / MaxSecond * Second;
            progressBar2.Visible = checkBox2.Checked && !button3.Visible;
            if (checkBox1.Checked)
            {
                label10.Visible = true;
                label10.Text = "得数: " + SelectItems;
                if (SelectItems == 0) label11.ForeColor = label10.ForeColor = Color.Green;
                else label11.ForeColor = label10.ForeColor = Color.Black;
                if (SelectItems != -114514 && SelectItems <= 63)
                {
                    progressBar1.Visible = true;
                    label11.Visible = true;
                    tmp = (Math.Abs(GetBiggestChoise()) > Math.Abs(GetBiggestChoise(false))) ? Math.Abs(GetBiggestChoise()) : Math.Abs(GetBiggestChoise(false));
                    tmp = 100 / Math.Abs(tmp) * Math.Abs(0 - SelectItems);
                    progressBar1.Value = (int)tmp;
                }
                else if (SelectItems > 63)
                {
                    if ((SelectItems > 700) && (SelectItems < 7000))
                    {
                        label10.Text = "得数: 0";
                        label11.ForeColor = label10.ForeColor = Color.Green;
                    }
                    else if ((SelectItems > 63) && (SelectItems < 700))
                    {
                        progressBar1.Visible = false;
                        label11.Visible = false;
                        label10.Text = "得数: NaN";
                        label11.ForeColor = label10.ForeColor = Color.Red;
                    }
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

        private string RandomLabelText(bool Special = false)
        {
            int integer = new Random().Next(-9, 10 + (Special ? 3 : 0));
            switch (integer)
            {
                case 1: case 2: case 3: case 4: case 5: case 6:case 7: case 8: case 9:
                    return "(+" + integer + ")";
                case 0:
                    return "(0)";
                case -1: case -2: case -3: case -4: case -5: case -6: case -7: case -8: case -9:
                    return "(" + integer + ")";
                case 10:
                    return "(∞)";
                case 11:
                    return "NaN";
                case 12:
                    return "(±" + new Random().Next(-10, 10) + ")";
            }
            return "";
        }

        private void InitializeLabels()
        {
            foreach (LinkLabel label in Labels)
            {
                label.BorderStyle = BorderStyle.None;
                label.Text = RandomLabelText(ToolsToolStripMenuItem.Checked);
                Thread.Sleep(10);
            }
        }

        private List<List<LinkLabel>> GetOKOffsetLabels()
        {
            List<List<LinkLabel>> result = new List<List<LinkLabel>>();
            List<LinkLabel> labels = Labels.ToList();
            List<LinkLabel> labelsOld = Labels.ToList();
            foreach (LinkLabel linklabel in labelsOld) 
                if (linklabel.Text == "(∞)" || linklabel.Text == "NaN" || linklabel.Text.Contains("±")) 
                {
                    /*if (linklabel.Text.Contains("±"))
                    {
                        LinkLabel llll = new LinkLabel(),lllll = new LinkLabel();
                        lllll.Text = " " + linklabel.Text.Replace("(-", string.Empty).Replace(")", string.Empty).Replace("±", string.Empty) + " ";
                        llll.Text = "(" + linklabel.Text.Replace("(+", string.Empty).Replace(")", string.Empty).Replace("±", string.Empty) + ")";
                        labels.Add(llll);
                        labels.Add(lllll);
                    }*/
                    labels.Remove(linklabel);
                }
            for (int i = 1; i < labels.Count; i++) //选择数
            {
                List<List<int>> list = new List<List<int>>();
                list.AddRange(PermutationCombinations(Enumerable.Range(0,labels.Count).ToList(),i));
                foreach (List<int> ls in list)
                {
                    List<LinkLabel> tmp = new List<LinkLabel>();
                    int integer = 0;
                    foreach (int inte in ls)
                    {
                        integer += ParesString(labels[inte].Text);
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
            e.Cancel = true;
            bool bool1 = checkBox5.Checked;
            bool boole = button3.Visible || (Help >= 3) || checkBox3.Checked || bool1;
            openHelpMenu++;
            string HelpText = "玩法：\n" +
                    "打开软件后可以看到左上方的横式，右上方的分数，下方有两个按钮\n" +
                    "选择一些加起来等于0的数字（单独一个0也可以，或者-3，-2，+5这三个数也可以，不限项数）\n" +
                    "选择好后，下方的“检测是否死局”按钮会变成“抵消”\n" +
                    "如果所有选择的数相加等于0，按下“抵消”按钮即可得1分，如果选错了会结束游戏\n" +
                    "如果觉得陷入了死局，可以按下“检测是否死局”，如确实死局，可以重新生成一次，如没有，将会结束游戏" +
                    (boole ? (bool1 ? "\n\n游戏暂停状态下不可使用提示，想使用提示请先解除暂停状态" : (checkBox3.Checked ? "\n\n极限模式下不可使用提示" : (button3.Visible ? "\n\n游戏已经结束，不可使用提示。可以在游戏结束时单击等式上任意数字获取随机解" : ("\n\n已使用" + Help + "次提示，不可再次使用")))) : ("\n\n陷入迷茫？点击“是”获取提示\n" + "已使用" + Help + "次提示，为了游戏体验，只可使用3次提示，你还剩" + (3 - Help) + "次机会")) +
                    (ToolsToolStripMenuItem.Checked ? "\n\n道具模式出现的道具: \n1. ∞\n无限符号可以消除同时和它选择的数，但它并不会提供分数，你可以用它消除你消除不了的数\n2. NaN\n选择此数将会立刻加一分，但是会使游戏报错并结束游戏\n3. (±n)\nn可以被当作正数，也可以被当作负数" : string.Empty);
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
            Reseting = true;
            List<bool> bools = new List<bool>();
            foreach (ToolStripMenuItem toolStrip in 究极极限模式ToolStripMenuItem.DropDownItems) bools.Add(toolStrip.Checked);
            bool[] args = { checkBox1.Checked,checkBox2.Checked };
            timer1.Stop();
            timer2.Stop();
            Controls.Clear();
            InitializeComponent();
            checkBox1.Checked = args[0];
            checkBox2.Checked = args[1];
            for (int i = 0; i < bools.Count;i++) ((ToolStripMenuItem)究极极限模式ToolStripMenuItem.DropDownItems[i]).Checked = bools[i];
            Form1_Load(sender, e);
            Reseting = false;
        }

        private int GetBiggestChoise(bool Biggest = true)
        {
            int BiggestInt = 0;
            List<LinkLabel> labels = Labels.ToList();
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
            bool bool1 = checkBox5.Checked;
            checkBox5.Checked = true;
            if (MessageBox.Show("是否开启新局，本局将会立刻结束","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                button3_Click(sender, e);
            checkBox5.Checked = false || bool1;
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
            LinkLabel[] ls = Labels;
            foreach (LinkLabel label in ls)
                if (checkBox4.Checked == (label.BorderStyle != BorderStyle.FixedSingle)) Selectlabel(label, true);
        }

        private int ParesString(string inp)
        {
            try
            {
                return int.Parse(inp.Replace("(", string.Empty).Replace(")", string.Empty));
            }
            catch (FormatException ex)
            {
                switch (inp)
                {
                    case "(∞)":
                        return 1000;
                    case "NaN":
                        return 100;
                    default:
                        if (inp.Contains("±")) {return int.Parse(inp.Replace("(", string.Empty).Replace(")", string.Empty).Replace("±", string.Empty))*10000; }
                        throw ex;
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
            if (!Second10ToolStripMenuItem.Checked && !Second5ToolStripMenuItem.Checked)
            {
                MaxSecond = 15;
                return;
            }
            if (checkBox2.Checked == false && Second5ToolStripMenuItem.Checked) { MessageBox.Show("请勾选“计时”以生效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            if (Second10ToolStripMenuItem.Checked && Second5ToolStripMenuItem.Checked) Second10ToolStripMenuItem.Checked = false;
            if (Second5ToolStripMenuItem.Checked) { Second = MaxSecond = 5; }
        }

        private void Second10ToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (!Second10ToolStripMenuItem.Checked && !Second5ToolStripMenuItem.Checked)
            {
                MaxSecond = 15;
                return;
            }
            if (checkBox2.Checked == false && Second10ToolStripMenuItem.Checked) { MessageBox.Show("请勾选“计时”以生效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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

        private void ToolsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (Reseting) Reseting = false;
            else
            {
                if (MessageBox.Show("应用修改需要重新开始一局，是否继续", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) button3_Click(sender, e); 
                else
                {
                    Reseting = true;
                    ToolsToolStripMenuItem.Checked ^= true;
                }
            }
        }
    }
}
