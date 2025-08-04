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
            clickedOnPlayerLabel = new Label();
            tileCoords = new Label();
            tilePicker = new Label();
            tileInfoLabel = new Label();
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
            // clickedOnPlayerLabel
            // 
            clickedOnPlayerLabel.AutoSize = true;
            clickedOnPlayerLabel.Location = new Point(0, 573);
            clickedOnPlayerLabel.Name = "clickedOnPlayerLabel";
            clickedOnPlayerLabel.Size = new Size(128, 20);
            clickedOnPlayerLabel.TabIndex = 7;
            clickedOnPlayerLabel.Text = "clicked on player?";
            // 
            // tileCoords
            // 
            tileCoords.AutoSize = true;
            tileCoords.Location = new Point(158, 624);
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
            // GEOSform
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1182, 653);
            Controls.Add(tileInfoLabel);
            Controls.Add(tilePicker);
            Controls.Add(tileCoords);
            Controls.Add(clickedOnPlayerLabel);
            Controls.Add(mouseCoordinates);
            DoubleBuffered = true;
            Name = "GEOSform";
            Text = "GEOS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label mouseCoordinates;
        private Label clickedOnPlayerLabel;
        private Label tileCoords;
        private Label tilePicker;
        private Label tileInfoLabel;
    }
}
