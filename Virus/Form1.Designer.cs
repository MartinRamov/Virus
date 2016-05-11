namespace Virus
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
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnHighscore = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSound = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.Color.Crimson;
            this.btnNewGame.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNewGame.Location = new System.Drawing.Point(245, 190);
            this.btnNewGame.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(369, 55);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "Start a game";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnHighscore
            // 
            this.btnHighscore.BackColor = System.Drawing.Color.YellowGreen;
            this.btnHighscore.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnHighscore.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnHighscore.Location = new System.Drawing.Point(36, 357);
            this.btnHighscore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHighscore.Name = "btnHighscore";
            this.btnHighscore.Size = new System.Drawing.Size(219, 48);
            this.btnHighscore.TabIndex = 2;
            this.btnHighscore.Text = "Highscores";
            this.btnHighscore.UseVisualStyleBackColor = false;
            this.btnHighscore.Click += new System.EventHandler(this.btnHighscore_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExit.Location = new System.Drawing.Point(607, 357);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(219, 48);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSound
            // 
            this.btnSound.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSound.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSound.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSound.Location = new System.Drawing.Point(321, 357);
            this.btnSound.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(219, 48);
            this.btnSound.TabIndex = 3;
            this.btnSound.Text = "Sound: On";
            this.btnSound.UseVisualStyleBackColor = false;
            this.btnSound.Click += new System.EventHandler(this.btnSound_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(304, 268);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(255, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Instructions";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Castellar", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(308, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 73);
            this.label1.TabIndex = 7;
            this.label1.Text = "Virus";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnNewGame;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Virus.Properties.Resources._12_game_background_mesh_cloud;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(859, 543);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSound);
            this.Controls.Add(this.btnHighscore);
            this.Controls.Add(this.btnNewGame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Virus";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnHighscore;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSound;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}

