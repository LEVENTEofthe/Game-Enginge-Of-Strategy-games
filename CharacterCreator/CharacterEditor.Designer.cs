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
            TurnSpeed = new NumericUpDown();
            actorActions = new CheckedListBox();
            MoveNumupdown = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)characterImagePicbox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TurnSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MoveNumupdown).BeginInit();
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
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 1;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 73);
            label2.Name = "label2";
            label2.Size = new Size(28, 20);
            label2.TabIndex = 3;
            label2.Text = "HP";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // characterImagePicbox
            // 
            characterImagePicbox.Location = new Point(25, 245);
            characterImagePicbox.Name = "characterImagePicbox";
            characterImagePicbox.Size = new Size(73, 79);
            characterImagePicbox.TabIndex = 7;
            characterImagePicbox.TabStop = false;
            // 
            // importPicBtn
            // 
            importPicBtn.Location = new Point(14, 210);
            importPicBtn.Name = "importPicBtn";
            importPicBtn.Size = new Size(94, 29);
            importPicBtn.TabIndex = 8;
            importPicBtn.Text = "Import picture";
            importPicBtn.UseVisualStyleBackColor = true;
            importPicBtn.Click += importPicBtn_Click;
            // 
            // exportBtn
            // 
            exportBtn.Font = new Font("Segoe UI", 8F);
            exportBtn.Location = new Point(12, 387);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(120, 51);
            exportBtn.TabIndex = 9;
            exportBtn.Text = "Finish character creation";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += exportBtn_Click;
            // 
            // TurnSpeed
            // 
            TurnSpeed.Location = new Point(12, 96);
            TurnSpeed.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            TurnSpeed.Name = "TurnSpeed";
            TurnSpeed.Size = new Size(55, 27);
            TurnSpeed.TabIndex = 10;
            // 
            // actorActions
            // 
            actorActions.FormattingEnabled = true;
            actorActions.Location = new Point(170, 33);
            actorActions.Name = "actorActions";
            actorActions.Size = new Size(150, 114);
            actorActions.TabIndex = 12;
            // 
            // MoveNumupdown
            // 
            MoveNumupdown.Location = new Point(12, 163);
            MoveNumupdown.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            MoveNumupdown.Name = "MoveNumupdown";
            MoveNumupdown.Size = new Size(55, 27);
            MoveNumupdown.TabIndex = 14;
            MoveNumupdown.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 140);
            label3.Name = "label3";
            label3.Size = new Size(82, 20);
            label3.TabIndex = 13;
            label3.Text = "Turn speed";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(170, 10);
            label4.Name = "label4";
            label4.Size = new Size(58, 20);
            label4.TabIndex = 15;
            label4.Text = "Actions";
            // 
            // CharacterEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(MoveNumupdown);
            Controls.Add(label3);
            Controls.Add(actorActions);
            Controls.Add(TurnSpeed);
            Controls.Add(exportBtn);
            Controls.Add(importPicBtn);
            Controls.Add(characterImagePicbox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(nameTextbox);
            Name = "CharacterEditor";
            Text = "Character Editor";
            ((System.ComponentModel.ISupportInitialize)characterImagePicbox).EndInit();
            ((System.ComponentModel.ISupportInitialize)TurnSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)MoveNumupdown).EndInit();
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
        private NumericUpDown TurnSpeed;
        private ListBox listBox1;
        private CheckedListBox actorActions;
        private NumericUpDown MoveNumupdown;
        private Label label3;
        private Label label4;
    }
}
