namespace Kursova_rab
{
    partial class Form1
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.BoxBorder = new System.Windows.Forms.CheckBox();
            this.BoxChangeColor = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectSize = new System.Windows.Forms.ComboBox();
            this.BoxEdges = new System.Windows.Forms.CheckBox();
            this.BoxArea = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BoxEdit = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BoxBorder
            // 
            this.BoxBorder.AutoSize = true;
            this.BoxBorder.Location = new System.Drawing.Point(25, 109);
            this.BoxBorder.Name = "BoxBorder";
            this.BoxBorder.Size = new System.Drawing.Size(133, 20);
            this.BoxBorder.TabIndex = 0;
            this.BoxBorder.Text = "Display Circle (D)";
            this.BoxBorder.UseVisualStyleBackColor = true;
            this.BoxBorder.CheckedChanged += new System.EventHandler(this.BoxBorder_CheckedChanged);
            // 
            // BoxChangeColor
            // 
            this.BoxChangeColor.AutoSize = true;
            this.BoxChangeColor.Location = new System.Drawing.Point(25, 135);
            this.BoxChangeColor.Name = "BoxChangeColor";
            this.BoxChangeColor.Size = new System.Drawing.Size(131, 20);
            this.BoxChangeColor.TabIndex = 1;
            this.BoxChangeColor.Text = "Change Color (C)";
            this.BoxChangeColor.UseVisualStyleBackColor = true;
            this.BoxChangeColor.CheckedChanged += new System.EventHandler(this.BoxColorChange_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Color (A)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(70, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "               ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectSize
            // 
            this.SelectSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectSize.FormattingEnabled = true;
            this.SelectSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.SelectSize.Location = new System.Drawing.Point(73, 214);
            this.SelectSize.Name = "SelectSize";
            this.SelectSize.Size = new System.Drawing.Size(49, 24);
            this.SelectSize.TabIndex = 5;
            this.SelectSize.SelectedIndexChanged += new System.EventHandler(this.SelectSize_SelectedIndexChanged);
            // 
            // BoxEdges
            // 
            this.BoxEdges.AutoSize = true;
            this.BoxEdges.Location = new System.Drawing.Point(25, 57);
            this.BoxEdges.Name = "BoxEdges";
            this.BoxEdges.Size = new System.Drawing.Size(138, 20);
            this.BoxEdges.TabIndex = 6;
            this.BoxEdges.Text = "Display Edges (E)";
            this.BoxEdges.UseVisualStyleBackColor = true;
            this.BoxEdges.CheckedChanged += new System.EventHandler(this.BoxDisplayEdges_CheckedChanged);
            // 
            // BoxArea
            // 
            this.BoxArea.AutoSize = true;
            this.BoxArea.Location = new System.Drawing.Point(25, 83);
            this.BoxArea.Name = "BoxArea";
            this.BoxArea.Size = new System.Drawing.Size(128, 20);
            this.BoxArea.TabIndex = 7;
            this.BoxArea.Text = "Display Area (R)";
            this.BoxArea.UseVisualStyleBackColor = true;
            this.BoxArea.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BoxEdit);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BoxChangeColor);
            this.panel1.Controls.Add(this.BoxArea);
            this.panel1.Controls.Add(this.SelectSize);
            this.panel1.Controls.Add(this.BoxEdges);
            this.panel1.Controls.Add(this.BoxBorder);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(998, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 303);
            this.panel1.TabIndex = 8;
            // 
            // BoxEdit
            // 
            this.BoxEdit.AutoSize = true;
            this.BoxEdit.Location = new System.Drawing.Point(25, 31);
            this.BoxEdit.Name = "BoxEdit";
            this.BoxEdit.Size = new System.Drawing.Size(112, 20);
            this.BoxEdit.TabIndex = 8;
            this.BoxEdit.Text = "Edit Mode (M)";
            this.BoxEdit.UseVisualStyleBackColor = true;
            this.BoxEdit.CheckedChanged += new System.EventHandler(this.BoxEdit_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "W -> change shape";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Z -> Undo";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1173, 688);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.CheckBox BoxBorder;
        private System.Windows.Forms.CheckBox BoxChangeColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SelectSize;
        private System.Windows.Forms.CheckBox BoxEdges;
        private System.Windows.Forms.CheckBox BoxArea;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox BoxEdit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

