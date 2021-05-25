
namespace B_A_Mazing
{
    partial class Form1
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
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.buttonAMaze = new System.Windows.Forms.Button();
            this.buttonSaveImage = new System.Windows.Forms.Button();
            this.pictureBoxMazeInput = new System.Windows.Forms.PictureBox();
            this.numericUpDownPrunning = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.buttonAMazeII = new System.Windows.Forms.Button();
            this.buttonPrunning = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMazeInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrunning)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.Location = new System.Drawing.Point(48, 48);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Size = new System.Drawing.Size(176, 47);
            this.buttonLoadImage.TabIndex = 0;
            this.buttonLoadImage.Text = "Load Image";
            this.buttonLoadImage.UseVisualStyleBackColor = true;
            this.buttonLoadImage.Click += new System.EventHandler(this.buttonLoadImage_Click);
            // 
            // buttonAMaze
            // 
            this.buttonAMaze.Enabled = false;
            this.buttonAMaze.Location = new System.Drawing.Point(419, 164);
            this.buttonAMaze.Name = "buttonAMaze";
            this.buttonAMaze.Size = new System.Drawing.Size(190, 47);
            this.buttonAMaze.TabIndex = 1;
            this.buttonAMaze.Text = "A-Maze";
            this.buttonAMaze.UseVisualStyleBackColor = true;
            this.buttonAMaze.Click += new System.EventHandler(this.buttonAMaze_Click);
            // 
            // buttonSaveImage
            // 
            this.buttonSaveImage.Enabled = false;
            this.buttonSaveImage.Location = new System.Drawing.Point(786, 48);
            this.buttonSaveImage.Name = "buttonSaveImage";
            this.buttonSaveImage.Size = new System.Drawing.Size(179, 47);
            this.buttonSaveImage.TabIndex = 2;
            this.buttonSaveImage.Text = "Save Image";
            this.buttonSaveImage.UseVisualStyleBackColor = true;
            this.buttonSaveImage.Click += new System.EventHandler(this.buttonSaveImage_Click);
            // 
            // pictureBoxMazeInput
            // 
            this.pictureBoxMazeInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxMazeInput.Location = new System.Drawing.Point(48, 165);
            this.pictureBoxMazeInput.Name = "pictureBoxMazeInput";
            this.pictureBoxMazeInput.Size = new System.Drawing.Size(335, 250);
            this.pictureBoxMazeInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMazeInput.TabIndex = 3;
            this.pictureBoxMazeInput.TabStop = false;
            // 
            // numericUpDownPrunning
            // 
            this.numericUpDownPrunning.Location = new System.Drawing.Point(543, 96);
            this.numericUpDownPrunning.Name = "numericUpDownPrunning";
            this.numericUpDownPrunning.Size = new System.Drawing.Size(101, 31);
            this.numericUpDownPrunning.TabIndex = 4;
            this.numericUpDownPrunning.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownPrunning.ValueChanged += new System.EventHandler(this.numericUpDownPrunning_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Prunning Passes";
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Location = new System.Drawing.Point(649, 224);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.Size = new System.Drawing.Size(316, 191);
            this.richTextBoxMessages.TabIndex = 6;
            this.richTextBoxMessages.Text = "";
            // 
            // buttonAMazeII
            // 
            this.buttonAMazeII.Enabled = false;
            this.buttonAMazeII.Location = new System.Drawing.Point(419, 241);
            this.buttonAMazeII.Name = "buttonAMazeII";
            this.buttonAMazeII.Size = new System.Drawing.Size(190, 46);
            this.buttonAMazeII.TabIndex = 7;
            this.buttonAMazeII.Text = "A-Maze II";
            this.buttonAMazeII.UseVisualStyleBackColor = true;
            this.buttonAMazeII.Click += new System.EventHandler(this.buttonAMazeII_Click);
            // 
            // buttonPrunning
            // 
            this.buttonPrunning.Enabled = false;
            this.buttonPrunning.Location = new System.Drawing.Point(419, 35);
            this.buttonPrunning.Name = "buttonPrunning";
            this.buttonPrunning.Size = new System.Drawing.Size(190, 46);
            this.buttonPrunning.TabIndex = 8;
            this.buttonPrunning.Text = "Prunning";
            this.buttonPrunning.UseVisualStyleBackColor = true;
            this.buttonPrunning.Click += new System.EventHandler(this.buttonPrunning_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 450);
            this.Controls.Add(this.buttonPrunning);
            this.Controls.Add(this.buttonAMazeII);
            this.Controls.Add(this.richTextBoxMessages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownPrunning);
            this.Controls.Add(this.pictureBoxMazeInput);
            this.Controls.Add(this.buttonSaveImage);
            this.Controls.Add(this.buttonAMaze);
            this.Controls.Add(this.buttonLoadImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMazeInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrunning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadImage;
        private System.Windows.Forms.Button buttonAMaze;
        private System.Windows.Forms.Button buttonSaveImage;
        private System.Windows.Forms.PictureBox pictureBoxMazeInput;
        private System.Windows.Forms.NumericUpDown numericUpDownPrunning;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.Button buttonAMazeII;
        private System.Windows.Forms.Button buttonPrunning;
    }
}

