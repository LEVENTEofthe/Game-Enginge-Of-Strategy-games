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
            exportBtn = new Button();
            Side_SubmenuPanel = new Panel();
            mapParameterMenuBtn = new Button();
            drawMenuBtn = new Button();
            eventMenuBtn = new Button();
            Top_SubmenuPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)MapDrawingField).BeginInit();
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
            // exportBtn
            // 
            exportBtn.Location = new Point(628, 409);
            exportBtn.Name = "exportBtn";
            exportBtn.Size = new Size(94, 29);
            exportBtn.TabIndex = 2;
            exportBtn.Text = "Export map";
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += exportBtn_Click;
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
            mapParameterMenuBtn.Font = new Font("Segoe UI", 5F);
            mapParameterMenuBtn.Location = new Point(12, 3);
            mapParameterMenuBtn.Name = "mapParameterMenuBtn";
            mapParameterMenuBtn.Size = new Size(94, 29);
            mapParameterMenuBtn.TabIndex = 8;
            mapParameterMenuBtn.Text = "Map parameters";
            mapParameterMenuBtn.UseVisualStyleBackColor = true;
            mapParameterMenuBtn.Click += mapParameterMenuBtn_Click;
            // 
            // drawMenuBtn
            // 
            drawMenuBtn.Font = new Font("Segoe UI", 8F);
            drawMenuBtn.Location = new Point(110, 3);
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
            eventMenuBtn.Location = new Point(210, 3);
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
            // TileMapEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(Top_SubmenuPanel);
            Controls.Add(mapParameterMenuBtn);
            Controls.Add(Side_SubmenuPanel);
            Controls.Add(eventMenuBtn);
            Controls.Add(drawMenuBtn);
            Controls.Add(exportBtn);
            Controls.Add(MapDrawingField);
            Name = "TileMapEditor";
            Text = "GEOS Map Editor";
            ((System.ComponentModel.ISupportInitialize)MapDrawingField).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox MapDrawingField;
        private Button exportBtn;
        private Panel Side_SubmenuPanel;
        private Button mapParameterMenuBtn;
        private Button drawMenuBtn;
        private Button eventMenuBtn;
        private Panel Top_SubmenuPanel;
    }
}
