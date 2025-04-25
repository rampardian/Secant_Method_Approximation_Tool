namespace Secant_Method_Approximation_Tool
{
    partial class Menu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Iterationsdgv = new DataGridView();
            label1 = new Label();
            XMinustxtbx = new TextBox();
            label2 = new Label();
            Ruleslbl = new Label();
            Formulaslbl = new Label();
            label3 = new Label();
            AppxErrortxtbx = new TextBox();
            label4 = new Label();
            Functiontxtbx = new TextBox();
            label5 = new Label();
            Xtxtbx = new TextBox();
            Calcbtn = new Button();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            ((System.ComponentModel.ISupportInitialize)Iterationsdgv).BeginInit();
            SuspendLayout();
            // 
            // Iterationsdgv
            // 
            Iterationsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Iterationsdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Iterationsdgv.Location = new Point(10, 38);
            Iterationsdgv.Margin = new Padding(3, 2, 3, 2);
            Iterationsdgv.Name = "Iterationsdgv";
            Iterationsdgv.RowHeadersWidth = 51;
            Iterationsdgv.Size = new Size(390, 246);
            Iterationsdgv.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(10, 7);
            label1.Name = "label1";
            label1.Size = new Size(324, 30);
            label1.TabIndex = 1;
            label1.Text = "Secant Root Approximation Tool";
            // 
            // XMinustxtbx
            // 
            XMinustxtbx.Location = new Point(130, 433);
            XMinustxtbx.Margin = new Padding(3, 2, 3, 2);
            XMinustxtbx.Name = "XMinustxtbx";
            XMinustxtbx.Size = new Size(100, 23);
            XMinustxtbx.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(93, 435);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 3;
            label2.Text = "xi-1:";
            // 
            // Ruleslbl
            // 
            Ruleslbl.AutoSize = true;
            Ruleslbl.Location = new Point(438, 298);
            Ruleslbl.Name = "Ruleslbl";
            Ruleslbl.Size = new Size(35, 15);
            Ruleslbl.TabIndex = 4;
            Ruleslbl.Text = "Rules";
            // 
            // Formulaslbl
            // 
            Formulaslbl.AutoSize = true;
            Formulaslbl.Location = new Point(10, 298);
            Formulaslbl.Name = "Formulaslbl";
            Formulaslbl.Size = new Size(56, 15);
            Formulaslbl.TabIndex = 5;
            Formulaslbl.Text = "Formulas";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 460);
            label3.Name = "label3";
            label3.Size = new Size(107, 15);
            label3.TabIndex = 7;
            label3.Text = "Approximate Error:";
            // 
            // AppxErrortxtbx
            // 
            AppxErrortxtbx.Location = new Point(130, 458);
            AppxErrortxtbx.Margin = new Padding(3, 2, 3, 2);
            AppxErrortxtbx.Name = "AppxErrortxtbx";
            AppxErrortxtbx.Size = new Size(255, 23);
            AppxErrortxtbx.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(62, 410);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 9;
            label4.Text = "Function:";
            // 
            // Functiontxtbx
            // 
            Functiontxtbx.Location = new Point(130, 408);
            Functiontxtbx.Margin = new Padding(3, 2, 3, 2);
            Functiontxtbx.Name = "Functiontxtbx";
            Functiontxtbx.Size = new Size(255, 23);
            Functiontxtbx.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(258, 435);
            label5.Name = "label5";
            label5.Size = new Size(19, 15);
            label5.TabIndex = 11;
            label5.Text = "xi:";
            // 
            // Xtxtbx
            // 
            Xtxtbx.Location = new Point(285, 433);
            Xtxtbx.Margin = new Padding(3, 2, 3, 2);
            Xtxtbx.Name = "Xtxtbx";
            Xtxtbx.Size = new Size(100, 23);
            Xtxtbx.TabIndex = 10;
            // 
            // Calcbtn
            // 
            Calcbtn.BackColor = SystemColors.ActiveCaption;
            Calcbtn.Location = new Point(521, 460);
            Calcbtn.Margin = new Padding(3, 2, 3, 2);
            Calcbtn.Name = "Calcbtn";
            Calcbtn.Size = new Size(292, 22);
            Calcbtn.TabIndex = 12;
            Calcbtn.Text = "Calculate Secant Root";
            Calcbtn.UseVisualStyleBackColor = false;
            Calcbtn.Click += Calcbtn_Click;
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new Point(406, 38);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(389, 246);
            formsPlot1.TabIndex = 13;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 489);
            Controls.Add(formsPlot1);
            Controls.Add(Calcbtn);
            Controls.Add(label5);
            Controls.Add(Xtxtbx);
            Controls.Add(label4);
            Controls.Add(Functiontxtbx);
            Controls.Add(label3);
            Controls.Add(AppxErrortxtbx);
            Controls.Add(Formulaslbl);
            Controls.Add(Ruleslbl);
            Controls.Add(label2);
            Controls.Add(XMinustxtbx);
            Controls.Add(label1);
            Controls.Add(Iterationsdgv);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Secant Method";
            ((System.ComponentModel.ISupportInitialize)Iterationsdgv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Iterationsdgv;
        private Label label1;
        private TextBox XMinustxtbx;
        private Label label2;
        private Label Ruleslbl;
        private Label Formulaslbl;
        private Label label3;
        private TextBox AppxErrortxtbx;
        private Label label4;
        private TextBox Functiontxtbx;
        private Label label5;
        private TextBox Xtxtbx;
        private Button Calcbtn;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
    }
}
