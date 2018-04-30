namespace GATest
{
    partial class GATestForm
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
            this.CBFunction = new System.Windows.Forms.ComboBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.RTBInfo = new System.Windows.Forms.RichTextBox();
            this.tbForSinX = new System.Windows.Forms.TextBox();
            this.tbForSinResult = new System.Windows.Forms.TextBox();
            this.lbForSinSin = new System.Windows.Forms.Label();
            this.lbForSinEqual = new System.Windows.Forms.Label();
            this.tbForGetXFunction = new System.Windows.Forms.TextBox();
            this.lbForGetXEqual = new System.Windows.Forms.Label();
            this.tbForGetXResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CBFunction
            // 
            this.CBFunction.FormattingEnabled = true;
            this.CBFunction.Items.AddRange(new object[] {
            "ArcSin",
            "x^n",
            "x,y,z..."});
            this.CBFunction.Location = new System.Drawing.Point(13, 13);
            this.CBFunction.Name = "CBFunction";
            this.CBFunction.Size = new System.Drawing.Size(135, 21);
            this.CBFunction.TabIndex = 0;
            this.CBFunction.SelectedIndexChanged += new System.EventHandler(this.CBFunctionSelectedIndexChanged);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(187, 11);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(93, 23);
            this.BtnStart.TabIndex = 1;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStartClick);
            // 
            // RTBInfo
            // 
            this.RTBInfo.Location = new System.Drawing.Point(13, 137);
            this.RTBInfo.Name = "RTBInfo";
            this.RTBInfo.Size = new System.Drawing.Size(267, 124);
            this.RTBInfo.TabIndex = 2;
            this.RTBInfo.Text = "";
            // 
            // tbForSinX
            // 
            this.tbForSinX.Enabled = false;
            this.tbForSinX.Location = new System.Drawing.Point(38, 72);
            this.tbForSinX.Name = "tbForSinX";
            this.tbForSinX.Size = new System.Drawing.Size(146, 20);
            this.tbForSinX.TabIndex = 3;
            this.tbForSinX.Visible = false;
            // 
            // tbForSinResult
            // 
            this.tbForSinResult.Location = new System.Drawing.Point(254, 73);
            this.tbForSinResult.Name = "tbForSinResult";
            this.tbForSinResult.Size = new System.Drawing.Size(26, 20);
            this.tbForSinResult.TabIndex = 4;
            this.tbForSinResult.Visible = false;
            // 
            // lbForSinSin
            // 
            this.lbForSinSin.AutoSize = true;
            this.lbForSinSin.Location = new System.Drawing.Point(10, 73);
            this.lbForSinSin.Name = "lbForSinSin";
            this.lbForSinSin.Size = new System.Drawing.Size(22, 13);
            this.lbForSinSin.TabIndex = 5;
            this.lbForSinSin.Text = "Sin";
            this.lbForSinSin.Visible = false;
            // 
            // lbForSinEqual
            // 
            this.lbForSinEqual.AutoSize = true;
            this.lbForSinEqual.Location = new System.Drawing.Point(213, 76);
            this.lbForSinEqual.Name = "lbForSinEqual";
            this.lbForSinEqual.Size = new System.Drawing.Size(13, 13);
            this.lbForSinEqual.TabIndex = 6;
            this.lbForSinEqual.Text = "=";
            this.lbForSinEqual.Visible = false;
            // 
            // tbForGetXFunction
            // 
            this.tbForGetXFunction.Location = new System.Drawing.Point(13, 72);
            this.tbForGetXFunction.Name = "tbForGetXFunction";
            this.tbForGetXFunction.Size = new System.Drawing.Size(172, 20);
            this.tbForGetXFunction.TabIndex = 7;
            // 
            // lbForGetXEqual
            // 
            this.lbForGetXEqual.AutoSize = true;
            this.lbForGetXEqual.Location = new System.Drawing.Point(214, 75);
            this.lbForGetXEqual.Name = "lbForGetXEqual";
            this.lbForGetXEqual.Size = new System.Drawing.Size(13, 13);
            this.lbForGetXEqual.TabIndex = 8;
            this.lbForGetXEqual.Text = "=";
            this.lbForGetXEqual.Visible = false;
            // 
            // tbForGetXResult
            // 
            this.tbForGetXResult.Location = new System.Drawing.Point(255, 72);
            this.tbForGetXResult.Name = "tbForGetXResult";
            this.tbForGetXResult.Size = new System.Drawing.Size(26, 20);
            this.tbForGetXResult.TabIndex = 9;
            // 
            // GATestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.tbForGetXResult);
            this.Controls.Add(this.lbForGetXEqual);
            this.Controls.Add(this.tbForGetXFunction);
            this.Controls.Add(this.lbForSinEqual);
            this.Controls.Add(this.lbForSinSin);
            this.Controls.Add(this.tbForSinResult);
            this.Controls.Add(this.tbForSinX);
            this.Controls.Add(this.RTBInfo);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.CBFunction);
            this.Name = "GATestForm";
            this.Text = "GATestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CBFunction;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.RichTextBox RTBInfo;
        private System.Windows.Forms.TextBox tbForSinX;
        private System.Windows.Forms.TextBox tbForSinResult;
        private System.Windows.Forms.Label lbForSinSin;
        private System.Windows.Forms.Label lbForSinEqual;
        private System.Windows.Forms.TextBox tbForGetXFunction;
        private System.Windows.Forms.Label lbForGetXEqual;
        private System.Windows.Forms.TextBox tbForGetXResult;

    }
}