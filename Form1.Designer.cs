namespace NumberGame
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.LinkLabel();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.究极极限模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BSODToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Second10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Second5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 112);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(641, 29);
            this.button1.TabIndex = 7;
            this.button1.Text = "检测是否死局";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(5, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(675, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "      +       +        +        +        +   ;    +";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 19F);
            this.label2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.label2.LinkColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 33);
            this.label2.TabIndex = 0;
            this.label2.TabStop = true;
            this.label2.Text = "(0)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(756, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数字";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(685, 69);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(59, 19);
            this.checkBox4.TabIndex = 19;
            this.checkBox4.Text = "全选";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 68);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 19);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "显示得数";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(125, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 15);
            this.label10.TabIndex = 17;
            this.label10.Text = "得数: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(456, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(210, 15);
            this.label11.TabIndex = 16;
            this.label11.Text = "进度条显示为得数减0的绝对值";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(121, 66);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(557, 22);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("宋体", 19F);
            this.label8.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.label8.LinkColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(688, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 33);
            this.label8.TabIndex = 6;
            this.label8.TabStop = true;
            this.label8.Text = "(0)";
            this.label8.Click += new System.EventHandler(this.label2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("宋体", 19F);
            this.label7.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.label7.LinkColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(585, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 33);
            this.label7.TabIndex = 5;
            this.label7.TabStop = true;
            this.label7.Text = "(0)";
            this.label7.Click += new System.EventHandler(this.label2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 19F);
            this.label6.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.label6.LinkColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(471, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 33);
            this.label6.TabIndex = 4;
            this.label6.TabStop = true;
            this.label6.Text = "(0)";
            this.label6.Click += new System.EventHandler(this.label2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 19F);
            this.label5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.label5.LinkColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(349, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 33);
            this.label5.TabIndex = 3;
            this.label5.TabStop = true;
            this.label5.Text = "(0)";
            this.label5.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 19F);
            this.label4.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.label4.LinkColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(229, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 33);
            this.label4.TabIndex = 2;
            this.label4.TabStop = true;
            this.label4.Text = "(0)";
            this.label4.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 19F);
            this.label3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.label3.LinkColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(115, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 33);
            this.label3.TabIndex = 1;
            this.label3.TabStop = true;
            this.label3.Text = "(0)";
            this.label3.Click += new System.EventHandler(this.label2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 9F);
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(13, 112);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(757, 29);
            this.button3.TabIndex = 13;
            this.button3.Text = "你输了，点击重开一局";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(13, 148);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(756, 29);
            this.button2.TabIndex = 9;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox5);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(777, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(167, 161);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分数";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(95, 21);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(59, 19);
            this.checkBox5.TabIndex = 3;
            this.checkBox5.Text = "暂停";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.ContextMenuStrip = this.contextMenuStrip1;
            this.checkBox3.Location = new System.Drawing.Point(69, 136);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(89, 19);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "极限模式";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.究极极限模式ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 56);
            // 
            // 究极极限模式ToolStripMenuItem
            // 
            this.究极极限模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BSODToolStripMenuItem,
            this.Second10ToolStripMenuItem,
            this.Second5ToolStripMenuItem});
            this.究极极限模式ToolStripMenuItem.Name = "究极极限模式ToolStripMenuItem";
            this.究极极限模式ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.究极极限模式ToolStripMenuItem.Text = "究极极限模式";
            // 
            // BSODToolStripMenuItem
            // 
            this.BSODToolStripMenuItem.CheckOnClick = true;
            this.BSODToolStripMenuItem.Name = "BSODToolStripMenuItem";
            this.BSODToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.BSODToolStripMenuItem.Text = "答错自动蓝屏";
            this.BSODToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.BSODToolStripMenuItem_Clicked);
            // 
            // Second10ToolStripMenuItem
            // 
            this.Second10ToolStripMenuItem.CheckOnClick = true;
            this.Second10ToolStripMenuItem.Name = "Second10ToolStripMenuItem";
            this.Second10ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.Second10ToolStripMenuItem.Text = "倒计时10秒";
            this.Second10ToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.Second10ToolStripMenuItem_CheckStateChanged);
            // 
            // Second5ToolStripMenuItem
            // 
            this.Second5ToolStripMenuItem.CheckOnClick = true;
            this.Second5ToolStripMenuItem.Name = "Second5ToolStripMenuItem";
            this.Second5ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.Second5ToolStripMenuItem.Text = "倒计时5秒";
            this.Second5ToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.Second5ToolStripMenuItem_CheckStateChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(9, 136);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(59, 19);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "计时";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 20F);
            this.label9.Location = new System.Drawing.Point(8, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 136);
            this.label9.TabIndex = 0;
            this.label9.Text = "0";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(660, 112);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 28);
            this.button4.TabIndex = 8;
            this.button4.Text = "新局";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(13, 112);
            this.progressBar2.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(756, 65);
            this.progressBar2.TabIndex = 14;
            this.progressBar2.Value = 100;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(960, 185);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.progressBar2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "数字抵消";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.LinkLabel label3;
        private System.Windows.Forms.LinkLabel label6;
        private System.Windows.Forms.LinkLabel label5;
        private System.Windows.Forms.LinkLabel label4;
        private System.Windows.Forms.LinkLabel label7;
        private System.Windows.Forms.LinkLabel label8;
        private System.Windows.Forms.LinkLabel label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 究极极限模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BSODToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Second10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Second5ToolStripMenuItem;
    }
}

