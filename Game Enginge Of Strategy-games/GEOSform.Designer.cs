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
            xScrollBar = new HScrollBar();
            yScrollBar = new VScrollBar();
            tileCoordinates = new Label();
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
            // xScrollBar
            // 
            xScrollBar.Location = new Point(508, 618);
            xScrollBar.Maximum = 250;
            xScrollBar.Minimum = -250;
            xScrollBar.Name = "xScrollBar";
            xScrollBar.Size = new Size(172, 26);
            xScrollBar.TabIndex = 12;
            xScrollBar.Scroll += xScrollBar_Scroll;
            // 
            // yScrollBar
            // 
            yScrollBar.Location = new Point(1147, 242);
            yScrollBar.Maximum = 250;
            yScrollBar.Minimum = -250;
            yScrollBar.Name = "yScrollBar";
            yScrollBar.Size = new Size(26, 161);
            yScrollBar.TabIndex = 13;
            yScrollBar.Scroll += yScrollBar_Scroll;
            // 
            // tileCoordinates
            // 
            tileCoordinates.AutoSize = true;
            tileCoordinates.Location = new Point(178, 626);
            tileCoordinates.Name = "tileCoordinates";
            tileCoordinates.Size = new Size(112, 20);
            tileCoordinates.TabIndex = 14;
            tileCoordinates.Text = "tile coordinates";
            // 
            // GEOSform
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1182, 653);
            Controls.Add(tileCoordinates);
            Controls.Add(yScrollBar);
            Controls.Add(xScrollBar);
            Controls.Add(clickedOnPlayerLabel);
            Controls.Add(mouseCoordinates);
            DoubleBuffered = true;
            Name = "GEOSform";
            Text = "GEOS";
            Load += GEOSform_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label mouseCoordinates;
        private Label clickedOnPlayerLabel;
        private HScrollBar xScrollBar;
        private VScrollBar yScrollBar;
        private Label tileCoordinates;
    }
}
