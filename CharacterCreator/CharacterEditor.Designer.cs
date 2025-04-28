namespace CharacterCreator
{
    partial class CharacterEditor
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
            nameTextbox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            characterImagePicbox = new PictureBox();
            importPicBtn = new Button();
            exportBtn = new Button();
            HPNumupdown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)characterImagePicbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HPNumupdown).BeginInit();
            SuspendLayout();
            // 
            // nameTextbox
            // 
            nameTextbox.Location = new Point(12, 33);
            nameTextbox.Name = "nameTextbox";
            nameTextbox.Size = new Size(125, 27);
            nameTextbox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 9);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 1;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 71);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 3;
            label2.Text = "Max HP";
            // 
            // characterImagePicbox
            // 
            characterImagePicbox.Location = new Point(34, 183);
            characterImagePicbox.Name = "characterImagePicbox";
            characterImagePicbox.Size = new Size(73, 79);
            characterImagePicbox.TabIndex = 7;
            characterImagePicbox.TabStop = false;
            // 
            // importPicBtn
            // 
            importPicBtn.Location = new Point(24, 148);
            importPicBtn.Name = "importPicBtn";
            importPicBtn.Size = new Size(94, 29);
            importPicBtn.TabIndex = 8;
            importPicBtn.Text = "Import picture";
            importPicBtn.UseVisualStyleBackColor = true;
            importPicBtn.Click += importPicBtn_Click;
            // 
            // exportBtn
            // 
            exportBtn.Location = new Point(25, 332);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(120, 51);
            exportBtn.TabIndex = 9;
            exportBtn.Text = "Finish character creation";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += exportBtn_Click;
            // 
            // HPNumupdown
            // 
            HPNumupdown.Location = new Point(12, 94);
            HPNumupdown.Name = "HPNumupdown";
            HPNumupdown.Size = new Size(125, 27);
            HPNumupdown.TabIndex = 10;
            // 
            // CharacterEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(HPNumupdown);
            Controls.Add(exportBtn);
            Controls.Add(importPicBtn);
            Controls.Add(characterImagePicbox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(nameTextbox);
            Name = "CharacterEditor";
            Text = "Character Editor";
            ((System.ComponentModel.ISupportInitialize)characterImagePicbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)HPNumupdown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nameTextbox;
        private Label label1;
        private Label label2;
        private PictureBox characterImagePicbox;
        private Button importPicBtn;
        private Button exportBtn;
        private NumericUpDown HPNumupdown;
    }
}
