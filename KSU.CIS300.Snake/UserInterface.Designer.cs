namespace KSU.CIS300.Snake
{
    partial class UserInterface
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
            this.uxPictureBox = new System.Windows.Forms.PictureBox();
            this.uxMenuStrip = new System.Windows.Forms.MenuStrip();
            this.uxToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.uxToolStripMenuItem_NewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.uxEasyGame = new System.Windows.Forms.ToolStripMenuItem();
            this.uxNormalGame = new System.Windows.Forms.ToolStripMenuItem();
            this.uxHardGame = new System.Windows.Forms.ToolStripMenuItem();
            this.uxlabelScore = new System.Windows.Forms.Label();
            this.uxScore = new System.Windows.Forms.Label();
            this.uxIsAI = new System.Windows.Forms.CheckBox();
            this.uxAIspeed = new System.Windows.Forms.NumericUpDown();
            this.uxAIspeedLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uxPictureBox)).BeginInit();
            this.uxMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxAIspeed)).BeginInit();
            this.SuspendLayout();
            // 
            // uxPictureBox
            // 
            this.uxPictureBox.Location = new System.Drawing.Point(4, 29);
            this.uxPictureBox.Margin = new System.Windows.Forms.Padding(1);
            this.uxPictureBox.Name = "uxPictureBox";
            this.uxPictureBox.Size = new System.Drawing.Size(404, 239);
            this.uxPictureBox.TabIndex = 0;
            this.uxPictureBox.TabStop = false;
            this.uxPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Paint);
            // 
            // uxMenuStrip
            // 
            this.uxMenuStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.uxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxToolStripMenuItem_File});
            this.uxMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.uxMenuStrip.Name = "uxMenuStrip";
            this.uxMenuStrip.Padding = new System.Windows.Forms.Padding(2, 1, 0, 1);
            this.uxMenuStrip.Size = new System.Drawing.Size(416, 24);
            this.uxMenuStrip.TabIndex = 1;
            this.uxMenuStrip.Text = "menuStrip1";
            // 
            // uxToolStripMenuItem_File
            // 
            this.uxToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxToolStripMenuItem_NewGame});
            this.uxToolStripMenuItem_File.Name = "uxToolStripMenuItem_File";
            this.uxToolStripMenuItem_File.Size = new System.Drawing.Size(37, 22);
            this.uxToolStripMenuItem_File.Text = "File";
            // 
            // uxToolStripMenuItem_NewGame
            // 
            this.uxToolStripMenuItem_NewGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxEasyGame,
            this.uxNormalGame,
            this.uxHardGame});
            this.uxToolStripMenuItem_NewGame.Name = "uxToolStripMenuItem_NewGame";
            this.uxToolStripMenuItem_NewGame.Size = new System.Drawing.Size(132, 22);
            this.uxToolStripMenuItem_NewGame.Text = "New Game";
            // 
            // uxEasyGame
            // 
            this.uxEasyGame.Name = "uxEasyGame";
            this.uxEasyGame.Size = new System.Drawing.Size(114, 22);
            this.uxEasyGame.Text = "Easy";
            this.uxEasyGame.Click += new System.EventHandler(this.EasyGame_Click);
            // 
            // uxNormalGame
            // 
            this.uxNormalGame.Name = "uxNormalGame";
            this.uxNormalGame.Size = new System.Drawing.Size(114, 22);
            this.uxNormalGame.Text = "Normal";
            this.uxNormalGame.Click += new System.EventHandler(this.NormalGame_Click);
            // 
            // uxHardGame
            // 
            this.uxHardGame.Name = "uxHardGame";
            this.uxHardGame.Size = new System.Drawing.Size(114, 22);
            this.uxHardGame.Text = "Hard";
            this.uxHardGame.Click += new System.EventHandler(this.HardGame_Click);
            // 
            // uxlabelScore
            // 
            this.uxlabelScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxlabelScore.AutoSize = true;
            this.uxlabelScore.BackColor = System.Drawing.Color.White;
            this.uxlabelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxlabelScore.Location = new System.Drawing.Point(311, 5);
            this.uxlabelScore.Name = "uxlabelScore";
            this.uxlabelScore.Size = new System.Drawing.Size(55, 17);
            this.uxlabelScore.TabIndex = 2;
            this.uxlabelScore.Text = "Score:";
            this.uxlabelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uxScore
            // 
            this.uxScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxScore.BackColor = System.Drawing.Color.White;
            this.uxScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxScore.Location = new System.Drawing.Point(359, 6);
            this.uxScore.Name = "uxScore";
            this.uxScore.Size = new System.Drawing.Size(49, 17);
            this.uxScore.TabIndex = 3;
            this.uxScore.Text = "0";
            this.uxScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxIsAI
            // 
            this.uxIsAI.AutoSize = true;
            this.uxIsAI.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uxIsAI.Location = new System.Drawing.Point(41, 5);
            this.uxIsAI.Name = "uxIsAI";
            this.uxIsAI.Size = new System.Drawing.Size(68, 17);
            this.uxIsAI.TabIndex = 4;
            this.uxIsAI.Text = "AI Player";
            this.uxIsAI.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.uxIsAI.UseVisualStyleBackColor = false;
            this.uxIsAI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserInterface_KeyDown);
            this.uxIsAI.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.UserInterface_PreviewKeyDown);
            // 
            // uxAIspeed
            // 
            this.uxAIspeed.Location = new System.Drawing.Point(172, 2);
            this.uxAIspeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxAIspeed.Name = "uxAIspeed";
            this.uxAIspeed.Size = new System.Drawing.Size(50, 20);
            this.uxAIspeed.TabIndex = 5;
            this.uxAIspeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // uxAIspeedLabel
            // 
            this.uxAIspeedLabel.AutoSize = true;
            this.uxAIspeedLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.uxAIspeedLabel.Location = new System.Drawing.Point(115, 6);
            this.uxAIspeedLabel.Name = "uxAIspeedLabel";
            this.uxAIspeedLabel.Size = new System.Drawing.Size(51, 13);
            this.uxAIspeedLabel.TabIndex = 6;
            this.uxAIspeedLabel.Text = "AI Speed";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(416, 278);
            this.Controls.Add(this.uxAIspeedLabel);
            this.Controls.Add(this.uxAIspeed);
            this.Controls.Add(this.uxIsAI);
            this.Controls.Add(this.uxScore);
            this.Controls.Add(this.uxlabelScore);
            this.Controls.Add(this.uxPictureBox);
            this.Controls.Add(this.uxMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.uxMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "UserInterface";
            this.Text = "Classic Snake";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserInterface_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.UserInterface_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uxPictureBox)).EndInit();
            this.uxMenuStrip.ResumeLayout(false);
            this.uxMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxAIspeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox uxPictureBox;
        private System.Windows.Forms.MenuStrip uxMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uxToolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem uxToolStripMenuItem_NewGame;
        private System.Windows.Forms.Label uxlabelScore;
        private System.Windows.Forms.Label uxScore;
        private System.Windows.Forms.ToolStripMenuItem uxEasyGame;
        private System.Windows.Forms.ToolStripMenuItem uxNormalGame;
        private System.Windows.Forms.ToolStripMenuItem uxHardGame;
        private System.Windows.Forms.CheckBox uxIsAI;
        private System.Windows.Forms.NumericUpDown uxAIspeed;
        private System.Windows.Forms.Label uxAIspeedLabel;
    }
}

