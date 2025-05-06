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
            TilesetPanel = new PictureBox();
            MapDrawingField = new PictureBox();
            exportBtn = new Button();
            tilesetSelectBtn = new Button();
            panel1 = new Panel();
            mapParameterMenuBtn = new Button();
            drawMenuBtn = new Button();
            eventMenuBtn = new Button();
            ribbonPanel = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)TilesetPanel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MapDrawingField).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // TilesetPanel
            // 
            TilesetPanel.BackColor = SystemColors.ControlLightLight;
            TilesetPanel.Location = new Point(18, 47);
            TilesetPanel.Name = "TilesetPanel";
            TilesetPanel.Size = new Size(106, 147);
            TilesetPanel.TabIndex = 0;
            TilesetPanel.TabStop = false;
            TilesetPanel.Paint += TilesetPanel_Paint;
            TilesetPanel.MouseClick += TilesetPanel_MouseClick;
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
            // tilesetSelectBtn
            // 
            tilesetSelectBtn.Font = new Font("Segoe UI", 6F);
            tilesetSelectBtn.Location = new Point(18, 12);
            tilesetSelectBtn.Name = "tilesetSelectBtn";
            tilesetSelectBtn.Size = new Size(106, 29);
            tilesetSelectBtn.TabIndex = 6;
            tilesetSelectBtn.Text = "select other tileset";
            tilesetSelectBtn.UseVisualStyleBackColor = true;
            tilesetSelectBtn.Click += tilesetSelectBtn_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Controls.Add(tilesetSelectBtn);
            panel1.Controls.Add(TilesetPanel);
            panel1.Location = new Point(10, 116);
            panel1.Name = "panel1";
            panel1.Size = new Size(137, 204);
            panel1.TabIndex = 7;
            // 
            // mapParameterMenuBtn
            // 
            mapParameterMenuBtn.Font = new Font("Segoe UI", 5F);
            mapParameterMenuBtn.Location = new Point(12, 3);
            mapParameterMenuBtn.Name = "mapParameterMenuBtn";
            mapParameterMenuBtn.Size = new Size(94, 29);
            mapParameterMenuBtn.TabIndex = 8;
            mapParameterMenuBtn.Text = "Set map parameters";
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
            eventMenuBtn.Text = "Place events";
            eventMenuBtn.UseVisualStyleBackColor = true;
            eventMenuBtn.Click += eventMenuBtn_Click;
            // 
            // ribbonPanel
            // 
            ribbonPanel.BackColor = SystemColors.ActiveBorder;
            ribbonPanel.Location = new Point(10, 32);
            ribbonPanel.Name = "ribbonPanel";
            ribbonPanel.Size = new Size(712, 78);
            ribbonPanel.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 364);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 12;
            label1.Text = "label1";
            // 
            // TileMapEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(mapParameterMenuBtn);
            Controls.Add(label1);
            Controls.Add(ribbonPanel);
            Controls.Add(panel1);
            Controls.Add(eventMenuBtn);
            Controls.Add(drawMenuBtn);
            Controls.Add(exportBtn);
            Controls.Add(MapDrawingField);
            Name = "TileMapEditor";
            Text = "GEOS Map Editor";
            ((System.ComponentModel.ISupportInitialize)TilesetPanel).EndInit();
            ((System.ComponentModel.ISupportInitialize)MapDrawingField).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox TilesetPanel;
        private PictureBox MapDrawingField;
        private Button exportBtn;
        private Button tilesetSelectBtn;
        private Panel panel1;
        private Button mapParameterMenuBtn;
        private Button drawMenuBtn;
        private Button eventMenuBtn;
        private Panel ribbonPanel;
        private Label label1;
    }
}
