﻿namespace Tile_Map_Drawing.MenuRibbons
{
    partial class Top_TileDrawingUC
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
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 47);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 0;
            label1.Text = "top drawing";
            // 
            // Top_TileDrawingRibbon
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Name = "Top_TileDrawingRibbon";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}
