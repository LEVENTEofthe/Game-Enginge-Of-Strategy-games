namespace Tile_Map_Drawing.MenuRibbons
{
    partial class Top_MapParametersRibbon
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mapRowsNumupdown = new NumericUpDown();
            mapColumnsNumupdown = new NumericUpDown();
            invalidateMapBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)mapRowsNumupdown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mapColumnsNumupdown).BeginInit();
            SuspendLayout();
            // 
            // mapRowsNumupdown
            // 
            mapRowsNumupdown.Location = new Point(23, 44);
            mapRowsNumupdown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            mapRowsNumupdown.Name = "mapRowsNumupdown";
            mapRowsNumupdown.Size = new Size(49, 27);
            mapRowsNumupdown.TabIndex = 1;
            mapRowsNumupdown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            mapRowsNumupdown.ValueChanged += mapRowsNumupdown_ValueChanged;
            // 
            // mapColumnsNumupdown
            // 
            mapColumnsNumupdown.Location = new Point(23, 11);
            mapColumnsNumupdown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            mapColumnsNumupdown.Name = "mapColumnsNumupdown";
            mapColumnsNumupdown.Size = new Size(49, 27);
            mapColumnsNumupdown.TabIndex = 2;
            mapColumnsNumupdown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            mapColumnsNumupdown.ValueChanged += mapColumnsNumupdown_ValueChanged;
            // 
            // invalidateMapBtn
            // 
            invalidateMapBtn.Location = new Point(78, 25);
            invalidateMapBtn.Name = "invalidateMapBtn";
            invalidateMapBtn.Size = new Size(94, 29);
            invalidateMapBtn.TabIndex = 3;
            invalidateMapBtn.Text = "set map";
            invalidateMapBtn.UseVisualStyleBackColor = true;
            invalidateMapBtn.Click += invalidateMapBtn_Click;
            // 
            // MapParametersRibbon
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(invalidateMapBtn);
            Controls.Add(mapColumnsNumupdown);
            Controls.Add(mapRowsNumupdown);
            Name = "MapParametersRibbon";
            Size = new Size(492, 89);
            ((System.ComponentModel.ISupportInitialize)mapRowsNumupdown).EndInit();
            ((System.ComponentModel.ISupportInitialize)mapColumnsNumupdown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private NumericUpDown mapRowsNumupdown;
        private NumericUpDown mapColumnsNumupdown;
        private Button invalidateMapBtn;
    }
}
