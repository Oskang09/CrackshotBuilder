namespace Updater
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
            this.S_U_Progress = new System.Windows.Forms.ProgressBar();
            this.S_U_ProgressNum = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.updateTxt = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // S_U_Progress
            // 
            this.S_U_Progress.Location = new System.Drawing.Point(12, 128);
            this.S_U_Progress.Name = "S_U_Progress";
            this.S_U_Progress.Size = new System.Drawing.Size(349, 23);
            this.S_U_Progress.TabIndex = 8;
            // 
            // S_U_ProgressNum
            // 
            this.S_U_ProgressNum.AutoSize = true;
            this.S_U_ProgressNum.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold);
            this.S_U_ProgressNum.Location = new System.Drawing.Point(12, 105);
            this.S_U_ProgressNum.Name = "S_U_ProgressNum";
            this.S_U_ProgressNum.Size = new System.Drawing.Size(35, 20);
            this.S_U_ProgressNum.TabIndex = 9;
            this.S_U_ProgressNum.Text = "100%";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Agency FB", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(83, 33);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(185, 45);
            this.title.TabIndex = 10;
            this.title.Text = "Downloading ...";
            // 
            // updateTxt
            // 
            this.updateTxt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.updateTxt.Location = new System.Drawing.Point(12, 191);
            this.updateTxt.Name = "updateTxt";
            this.updateTxt.ReadOnly = true;
            this.updateTxt.Size = new System.Drawing.Size(349, 182);
            this.updateTxt.TabIndex = 11;
            this.updateTxt.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Update";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 385);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.updateTxt);
            this.Controls.Add(this.title);
            this.Controls.Add(this.S_U_Progress);
            this.Controls.Add(this.S_U_ProgressNum);
            this.Name = "Form1";
            this.Text = "CrackshotBuilder Updater";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar S_U_Progress;
        private System.Windows.Forms.Label S_U_ProgressNum;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.RichTextBox updateTxt;
        private System.Windows.Forms.Label label1;
    }
}

