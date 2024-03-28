namespace Naval_battle
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.black = new System.Windows.Forms.Button();
            this.playerOne = new System.Windows.Forms.Button();
            this.playerTwo = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.black);
            this.panel1.Controls.Add(this.playerOne);
            this.panel1.Controls.Add(this.playerTwo);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 165);
            this.panel1.TabIndex = 0;
            // 
            // black
            // 
            this.black.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.black.Location = new System.Drawing.Point(20, 109);
            this.black.Name = "black";
            this.black.Size = new System.Drawing.Size(175, 41);
            this.black.TabIndex = 2;
            this.black.Text = "Выход";
            this.black.UseVisualStyleBackColor = true;
            this.black.Click += new System.EventHandler(this.black_Click);
            // 
            // playerOne
            // 
            this.playerOne.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerOne.Location = new System.Drawing.Point(20, 62);
            this.playerOne.Name = "playerOne";
            this.playerOne.Size = new System.Drawing.Size(175, 41);
            this.playerOne.TabIndex = 1;
            this.playerOne.Text = "Игра для одного";
            this.playerOne.UseVisualStyleBackColor = true;
            this.playerOne.Click += new System.EventHandler(this.playerOne_Click);
            // 
            // playerTwo
            // 
            this.playerTwo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.playerTwo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerTwo.Location = new System.Drawing.Point(20, 15);
            this.playerTwo.Name = "playerTwo";
            this.playerTwo.Size = new System.Drawing.Size(175, 41);
            this.playerTwo.TabIndex = 0;
            this.playerTwo.Text = "Игра для двоих";
            this.playerTwo.UseVisualStyleBackColor = false;
            this.playerTwo.Click += new System.EventHandler(this.playerTwo_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 188);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button black;
        private Button playerOne;
        private Button playerTwo;
    }
}