namespace Triangulation
{
    partial class DisplayMatrix
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbxBeta = new System.Windows.Forms.TextBox();
            this.tbxSigma = new System.Windows.Forms.TextBox();
            this.tbxUEnv = new System.Windows.Forms.TextBox();
            this.tbxF = new System.Windows.Forms.TextBox();
            this.tbxD = new System.Windows.Forms.TextBox();
            this.tbxA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(602, 480);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(624, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Beta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(628, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "uEnv";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(624, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sigma";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Triangle",
            "Rectangle",
            "Trapeze",
            "DoubleTriangle",
            "DoubleRectangle",
            "DoubleTrapeze",
            "Star"});
            this.comboBox1.Location = new System.Drawing.Point(654, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tbxBeta
            // 
            this.tbxBeta.Location = new System.Drawing.Point(675, 59);
            this.tbxBeta.Name = "tbxBeta";
            this.tbxBeta.Size = new System.Drawing.Size(100, 20);
            this.tbxBeta.TabIndex = 5;
            // 
            // tbxSigma
            // 
            this.tbxSigma.Location = new System.Drawing.Point(675, 85);
            this.tbxSigma.Name = "tbxSigma";
            this.tbxSigma.Size = new System.Drawing.Size(100, 20);
            this.tbxSigma.TabIndex = 6;
            // 
            // tbxUEnv
            // 
            this.tbxUEnv.Location = new System.Drawing.Point(675, 111);
            this.tbxUEnv.Name = "tbxUEnv";
            this.tbxUEnv.Size = new System.Drawing.Size(100, 20);
            this.tbxUEnv.TabIndex = 7;
            // 
            // tbxF
            // 
            this.tbxF.Location = new System.Drawing.Point(675, 244);
            this.tbxF.Name = "tbxF";
            this.tbxF.Size = new System.Drawing.Size(100, 20);
            this.tbxF.TabIndex = 13;
            this.tbxF.Text = "2";
            // 
            // tbxD
            // 
            this.tbxD.Location = new System.Drawing.Point(675, 218);
            this.tbxD.Name = "tbxD";
            this.tbxD.Size = new System.Drawing.Size(100, 20);
            this.tbxD.TabIndex = 12;
            this.tbxD.Text = "1";
            // 
            // tbxA
            // 
            this.tbxA.Location = new System.Drawing.Point(675, 192);
            this.tbxA.Name = "tbxA";
            this.tbxA.Size = new System.Drawing.Size(100, 20);
            this.tbxA.TabIndex = 11;
            this.tbxA.Text = "4 3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(624, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "d";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(628, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "f";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(624, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "A";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(675, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Show Matrixes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(675, 355);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Display general matrix";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DisplayMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 481);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbxF);
            this.Controls.Add(this.tbxD);
            this.Controls.Add(this.tbxA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxUEnv);
            this.Controls.Add(this.tbxSigma);
            this.Controls.Add(this.tbxBeta);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "DisplayMatrix";
            this.Text = "DisplayMatrix";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox tbxBeta;
        private System.Windows.Forms.TextBox tbxSigma;
        private System.Windows.Forms.TextBox tbxUEnv;
        private System.Windows.Forms.TextBox tbxF;
        private System.Windows.Forms.TextBox tbxD;
        private System.Windows.Forms.TextBox tbxA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}