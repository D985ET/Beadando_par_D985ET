namespace _GraphicsWinForm
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timerLbl = new System.Windows.Forms.Label();
            this.CurveListBox = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MultiThreadCheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(12, 36);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(850, 517);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(789, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 1;
            this.button1.Text = "Execute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose curvetype";
            // 
            // timerLbl
            // 
            this.timerLbl.AutoSize = true;
            this.timerLbl.Location = new System.Drawing.Point(686, 9);
            this.timerLbl.Name = "timerLbl";
            this.timerLbl.Size = new System.Drawing.Size(0, 13);
            this.timerLbl.TabIndex = 4;
            // 
            // CurveListBox
            // 
            this.CurveListBox.FormattingEnabled = true;
            this.CurveListBox.Items.AddRange(new object[] {
            "Lissajous",
            "Butterfly",
            "Cardioid",
            "Logarithmic"});
            this.CurveListBox.Location = new System.Drawing.Point(111, 3);
            this.CurveListBox.Name = "CurveListBox";
            this.CurveListBox.Size = new System.Drawing.Size(120, 34);
            this.CurveListBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(619, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Elapsed time";
            // 
            // MultiThreadCheck
            // 
            this.MultiThreadCheck.AutoSize = true;
            this.MultiThreadCheck.Location = new System.Drawing.Point(250, 13);
            this.MultiThreadCheck.Name = "MultiThreadCheck";
            this.MultiThreadCheck.Size = new System.Drawing.Size(84, 17);
            this.MultiThreadCheck.TabIndex = 7;
            this.MultiThreadCheck.Text = "Multithread?";
            this.MultiThreadCheck.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 564);
            this.Controls.Add(this.MultiThreadCheck);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CurveListBox);
            this.Controls.Add(this.timerLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Graphics";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timerLbl;
        private System.Windows.Forms.CheckedListBox CurveListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox MultiThreadCheck;
    }
}

