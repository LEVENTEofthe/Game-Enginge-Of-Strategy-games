namespace Tile_Map_Drawing.MenuRibbons
{
    partial class Side_TileDrawingRibbon
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
            panel1 = new Panel();
            tilesetSelectBtn = new Button();
            TilesetPanel = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TilesetPanel).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Controls.Add(tilesetSelectBtn);
            panel1.Controls.Add(TilesetPanel);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(143, 204);
            panel1.TabIndex = 8;
            // 
            // tilesetSelectBtn
            // 
            tilesetSelectBtn.Font = new Font("Segoe UI", 6F);
            tilesetSelectBtn.Location = new Point(18, 12);
            tilesetSelectBtn.Name = "tilesetSelectBtn";
            tilesetSelectBtn.Size = new Size(106, 29);
            tilesetSelectBtn.TabIndex = 6;
            tilesetSelectBtn.Text = "select other tileset";
            tilesetSelectBtn.UseVisualStyleBackColor = true;
            // 
            // TilesetPanel
            // 
            TilesetPanel.BackColor = SystemColors.ControlLightLight;
            TilesetPanel.Location = new Point(18, 47);
            TilesetPanel.Name = "TilesetPanel";
            TilesetPanel.Size = new Size(106, 147);
            TilesetPanel.TabIndex = 0;
            TilesetPanel.TabStop = false;
            // 
            // Side_TileDrawingRibbon
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "Side_TileDrawingRibbon";
            Size = new Size(143, 204);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TilesetPanel).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button tilesetSelectBtn;
        private PictureBox TilesetPanel;
    }
}
