namespace CrackshotBuilder
{
    partial class TTPHelper
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
            this.TTP_Text = new System.Windows.Forms.RichTextBox();
            this.TTPLabel = new System.Windows.Forms.GroupBox();
            this.TTPLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TTP_Text
            // 
            this.TTP_Text.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F);
            this.TTP_Text.Location = new System.Drawing.Point(6, 26);
            this.TTP_Text.Name = "TTP_Text";
            this.TTP_Text.Size = new System.Drawing.Size(318, 198);
            this.TTP_Text.TabIndex = 0;
            this.TTP_Text.Text = "";
            // 
            // TTPLabel
            // 
            this.TTPLabel.Controls.Add(this.TTP_Text);
            this.TTPLabel.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold);
            this.TTPLabel.Location = new System.Drawing.Point(12, 5);
            this.TTPLabel.Name = "TTPLabel";
            this.TTPLabel.Size = new System.Drawing.Size(330, 230);
            this.TTPLabel.TabIndex = 1;
            this.TTPLabel.TabStop = false;
            this.TTPLabel.Text = "AIR";
            // 
            // TTPHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 247);
            this.Controls.Add(this.TTPLabel);
            this.Name = "TTPHelper";
            this.Text = "TTPHelper";
            this.TTPLabel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox TTP_Text;
        public System.Windows.Forms.GroupBox TTPLabel;
    }
}