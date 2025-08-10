namespace Tile_Map_Drawing
{
    partial class TileMapEditor
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
            MapDrawingField = new PictureBox();
            Side_SubmenuPanel = new Panel();
            mapParameterMenuBtn = new Button();
            drawMenuBtn = new Button();
            eventMenuBtn = new Button();
            Top_SubmenuPanel = new Panel();
            menuStrip1 = new MenuStrip();
            fileMenuStrip = new ToolStripMenuItem();
            exportMapToolStripMenuItem = new ToolStripMenuItem();
            imToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)MapDrawingField).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // MapDrawingField
            // 
            MapDrawingField.BackColor = SystemColors.ControlLightLight;
            MapDrawingField.Location = new Point(169, 116);
            MapDrawingField.Name = "MapDrawingField";
            MapDrawingField.Size = new Size(553, 280);
            MapDrawingField.TabIndex = 1;
            MapDrawingField.TabStop = false;
            MapDrawingField.Paint += MapDrawingField_Paint;
            MapDrawingField.MouseClick += MapDrawingField_MouseClick;
            // 
            // Side_SubmenuPanel
            // 
            Side_SubmenuPanel.BackColor = SystemColors.ActiveBorder;
            Side_SubmenuPanel.Location = new Point(10, 116);
            Side_SubmenuPanel.Name = "Side_SubmenuPanel";
            Side_SubmenuPanel.Size = new Size(137, 204);
            Side_SubmenuPanel.TabIndex = 7;
            // 
            // mapParameterMenuBtn
            // 
            mapParameterMenuBtn.Font = new Font("Segoe UI", 8.25F);
            mapParameterMenuBtn.Location = new Point(110, 1);
            mapParameterMenuBtn.Name = "mapParameterMenuBtn";
            mapParameterMenuBtn.Size = new Size(94, 29);
            mapParameterMenuBtn.TabIndex = 8;
            mapParameterMenuBtn.Text = "Properties";
            mapParameterMenuBtn.UseVisualStyleBackColor = true;
            mapParameterMenuBtn.Click += propertiesBtn_Click;
            // 
            // drawMenuBtn
            // 
            drawMenuBtn.Font = new Font("Segoe UI", 8F);
            drawMenuBtn.Location = new Point(210, 1);
            drawMenuBtn.Name = "drawMenuBtn";
            drawMenuBtn.Size = new Size(94, 29);
            drawMenuBtn.TabIndex = 9;
            drawMenuBtn.Text = "Draw";
            drawMenuBtn.UseVisualStyleBackColor = true;
            drawMenuBtn.Click += drawMenuBtn_Click;
            // 
            // eventMenuBtn
            // 
            eventMenuBtn.Font = new Font("Segoe UI", 8F);
            eventMenuBtn.Location = new Point(310, 1);
            eventMenuBtn.Name = "eventMenuBtn";
            eventMenuBtn.Size = new Size(94, 29);
            eventMenuBtn.TabIndex = 10;
            eventMenuBtn.Text = "Events";
            eventMenuBtn.UseVisualStyleBackColor = true;
            eventMenuBtn.Click += eventMenuBtn_Click;
            // 
            // Top_SubmenuPanel
            // 
            Top_SubmenuPanel.BackColor = SystemColors.ActiveBorder;
            Top_SubmenuPanel.Location = new Point(10, 32);
            Top_SubmenuPanel.Name = "Top_SubmenuPanel";
            Top_SubmenuPanel.Size = new Size(712, 78);
            Top_SubmenuPanel.TabIndex = 11;
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenuStrip });
            menuStrip1.Location = new Point(10, 1);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(54, 28);
            menuStrip1.TabIndex = 13;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuStrip
            // 
            fileMenuStrip.DropDownItems.AddRange(new ToolStripItem[] { exportMapToolStripMenuItem, imToolStripMenuItem });
            fileMenuStrip.Name = "fileMenuStrip";
            fileMenuStrip.Size = new Size(46, 24);
            fileMenuStrip.Text = "File";
            // 
            // exportMapToolStripMenuItem
            // 
            exportMapToolStripMenuItem.Name = "exportMapToolStripMenuItem";
            exportMapToolStripMenuItem.Size = new Size(224, 26);
            exportMapToolStripMenuItem.Text = "Export map";
            exportMapToolStripMenuItem.Click += exportMapToolStripMenuItem_Click;
            // 
            // imToolStripMenuItem
            // 
            imToolStripMenuItem.Name = "imToolStripMenuItem";
            imToolStripMenuItem.Size = new Size(224, 26);
            imToolStripMenuItem.Text = "Import map";
            // 
            // TileMapEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            Controls.Add(Top_SubmenuPanel);
            Controls.Add(mapParameterMenuBtn);
            Controls.Add(Side_SubmenuPanel);
            Controls.Add(eventMenuBtn);
            Controls.Add(drawMenuBtn);
            Controls.Add(MapDrawingField);
            MainMenuStrip = menuStrip1;
            Name = "TileMapEditor";
            Text = "GEOS Map Editor";
            ((System.ComponentModel.ISupportInitialize)MapDrawingField).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox MapDrawingField;
        private Panel Side_SubmenuPanel;
        private Button mapParameterMenuBtn;
        private Button drawMenuBtn;
        private Button eventMenuBtn;
        private Panel Top_SubmenuPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileMenuStrip;
        private ToolStripMenuItem exportMapToolStripMenuItem;
        private ToolStripMenuItem imToolStripMenuItem;
    }
}
