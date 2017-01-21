namespace CrackshotBuilder
{
    partial class Sounds
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
            this.ASlabel = new System.Windows.Forms.Button();
            this.Delay = new System.Windows.Forms.TextBox();
            this.Delaylabel = new System.Windows.Forms.Label();
            this.Pitch = new System.Windows.Forms.TextBox();
            this.Pitchlabel = new System.Windows.Forms.Label();
            this.Volumelabel = new System.Windows.Forms.Label();
            this.Volume = new System.Windows.Forms.TextBox();
            this.Soundlabel = new System.Windows.Forms.Label();
            this.Sound = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ASlabel
            // 
            this.ASlabel.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.ASlabel.Location = new System.Drawing.Point(84, 139);
            this.ASlabel.Name = "ASlabel";
            this.ASlabel.Size = new System.Drawing.Size(141, 65);
            this.ASlabel.TabIndex = 69;
            this.ASlabel.Text = "Add Sound";
            this.ASlabel.UseVisualStyleBackColor = true;
            this.ASlabel.Click += new System.EventHandler(this.AddSound_Click);
            // 
            // Delay
            // 
            this.Delay.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Delay.Location = new System.Drawing.Point(12, 178);
            this.Delay.Name = "Delay";
            this.Delay.Size = new System.Drawing.Size(64, 26);
            this.Delay.TabIndex = 68;
            // 
            // Delaylabel
            // 
            this.Delaylabel.AutoSize = true;
            this.Delaylabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.Delaylabel.Location = new System.Drawing.Point(12, 157);
            this.Delaylabel.Name = "Delaylabel";
            this.Delaylabel.Size = new System.Drawing.Size(53, 18);
            this.Delaylabel.TabIndex = 67;
            this.Delaylabel.Text = "Delay";
            // 
            // Pitch
            // 
            this.Pitch.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pitch.Location = new System.Drawing.Point(12, 128);
            this.Pitch.Name = "Pitch";
            this.Pitch.Size = new System.Drawing.Size(64, 26);
            this.Pitch.TabIndex = 66;
            // 
            // Pitchlabel
            // 
            this.Pitchlabel.AutoSize = true;
            this.Pitchlabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.Pitchlabel.Location = new System.Drawing.Point(12, 107);
            this.Pitchlabel.Name = "Pitchlabel";
            this.Pitchlabel.Size = new System.Drawing.Size(49, 18);
            this.Pitchlabel.TabIndex = 65;
            this.Pitchlabel.Text = "Pitch";
            // 
            // Volumelabel
            // 
            this.Volumelabel.AutoSize = true;
            this.Volumelabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.Volumelabel.Location = new System.Drawing.Point(12, 57);
            this.Volumelabel.Name = "Volumelabel";
            this.Volumelabel.Size = new System.Drawing.Size(67, 18);
            this.Volumelabel.TabIndex = 64;
            this.Volumelabel.Text = "Volume";
            // 
            // Volume
            // 
            this.Volume.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Volume.Location = new System.Drawing.Point(12, 78);
            this.Volume.Name = "Volume";
            this.Volume.Size = new System.Drawing.Size(64, 26);
            this.Volume.TabIndex = 63;
            // 
            // Soundlabel
            // 
            this.Soundlabel.AutoSize = true;
            this.Soundlabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.Soundlabel.Location = new System.Drawing.Point(12, 9);
            this.Soundlabel.Name = "Soundlabel";
            this.Soundlabel.Size = new System.Drawing.Size(59, 18);
            this.Soundlabel.TabIndex = 62;
            this.Soundlabel.Text = "Sound";
            this.Soundlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Sound
            // 
            this.Sound.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sound.FormattingEnabled = true;
            this.Sound.Location = new System.Drawing.Point(12, 30);
            this.Sound.Name = "Sound";
            this.Sound.Size = new System.Drawing.Size(200, 24);
            this.Sound.TabIndex = 61;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.button1.Location = new System.Drawing.Point(85, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 65);
            this.button1.TabIndex = 70;
            this.button1.Text = "Test Sound";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Sounds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 214);
            this.Controls.Add(this.Delay);
            this.Controls.Add(this.Pitch);
            this.Controls.Add(this.Volume);
            this.Controls.Add(this.Sound);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ASlabel);
            this.Controls.Add(this.Soundlabel);
            this.Controls.Add(this.Delaylabel);
            this.Controls.Add(this.Pitchlabel);
            this.Controls.Add(this.Volumelabel);
            this.Name = "Sounds";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Sounds_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ASlabel;
        private System.Windows.Forms.TextBox Delay;
        private System.Windows.Forms.Label Delaylabel;
        private System.Windows.Forms.TextBox Pitch;
        private System.Windows.Forms.Label Pitchlabel;
        private System.Windows.Forms.Label Volumelabel;
        private System.Windows.Forms.TextBox Volume;
        private System.Windows.Forms.Label Soundlabel;
        private System.Windows.Forms.ComboBox Sound;
        private System.Windows.Forms.Button button1;
    }
}