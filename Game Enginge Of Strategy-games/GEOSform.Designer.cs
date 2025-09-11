namespace Game_Enginge_Of_Strategy_games
{
    partial class GEOSform
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
            mouseCoordinates = new Label();
            tileCoords = new Label();
            tilePicker = new Label();
            tileInfoLabel = new Label();
            button1 = new Button();
            currentTurnLbl = new Label();
            SuspendLayout();
            // 
            // mouseCoordinates
            // 
            mouseCoordinates.AutoSize = true;
            mouseCoordinates.Location = new Point(0, 624);
            mouseCoordinates.Name = "mouseCoordinates";
            mouseCoordinates.Size = new Size(135, 20);
            mouseCoordinates.TabIndex = 4;
            mouseCoordinates.Text = "mouse coordinates";
            // 
            // tileCoords
            // 
            tileCoords.AutoSize = true;
            tileCoords.Location = new Point(165, 602);
            tileCoords.Name = "tileCoords";
            tileCoords.Size = new Size(160, 20);
            tileCoords.TabIndex = 14;
            tileCoords.Text = "whichTileBellowMouse";
            // 
            // tilePicker
            // 
            tilePicker.AutoSize = true;
            tilePicker.Location = new Point(0, 553);
            tilePicker.Name = "tilePicker";
            tilePicker.Size = new Size(69, 20);
            tilePicker.TabIndex = 15;
            tilePicker.Text = "tilePicker";
            // 
            // tileInfoLabel
            // 
            tileInfoLabel.AutoSize = true;
            tileInfoLabel.Location = new Point(328, 625);
            tileInfoLabel.Name = "tileInfoLabel";
            tileInfoLabel.Size = new Size(60, 20);
            tileInfoLabel.TabIndex = 16;
            tileInfoLabel.Text = "tile info";
            // 
            // button1
            // 
            button1.Location = new Point(1034, 602);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 17;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // currentTurnLbl
            // 
            currentTurnLbl.AutoSize = true;
            currentTurnLbl.Location = new Point(888, 22);
            currentTurnLbl.Name = "currentTurnLbl";
            currentTurnLbl.Size = new Size(86, 20);
            currentTurnLbl.TabIndex = 18;
            currentTurnLbl.Text = "CurrentTurn";
            // 
            // GEOSform
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1182, 653);
            Controls.Add(currentTurnLbl);
            Controls.Add(button1);
            Controls.Add(tileInfoLabel);
            Controls.Add(tilePicker);
            Controls.Add(tileCoords);
            Controls.Add(mouseCoordinates);
            DoubleBuffered = true;
            Name = "GEOSform";
            Text = "GEOS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label mouseCoordinates;
        private Label tileCoords;
        private Label tilePicker;
        private Label tileInfoLabel;
        private Button button1;
        private Label currentTurnLbl;
    }
}
