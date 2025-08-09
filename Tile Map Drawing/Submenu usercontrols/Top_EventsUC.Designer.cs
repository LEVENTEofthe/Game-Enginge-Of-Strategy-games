namespace Tile_Map_Drawing
{
    partial class Top_EventsUC
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
            menuStrip1 = new MenuStrip();
            placeActorToolStripMenuItem = new ToolStripMenuItem();
            playerUnitToolStripMenuItem = new ToolStripMenuItem();
            setDirectToolStripMenuItem = new ToolStripMenuItem();
            setEnemyUnitToolStripMenuItem = new ToolStripMenuItem();
            setNPCToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.GripStyle = ToolStripGripStyle.Visible;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { placeActorToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(245, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // placeActorToolStripMenuItem
            // 
            placeActorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { playerUnitToolStripMenuItem, setDirectToolStripMenuItem, setEnemyUnitToolStripMenuItem, setNPCToolStripMenuItem });
            placeActorToolStripMenuItem.Name = "placeActorToolStripMenuItem";
            placeActorToolStripMenuItem.Size = new Size(129, 24);
            placeActorToolStripMenuItem.Text = "Place characters";
            // 
            // playerUnitToolStripMenuItem
            // 
            playerUnitToolStripMenuItem.Name = "playerUnitToolStripMenuItem";
            playerUnitToolStripMenuItem.Size = new Size(242, 26);
            playerUnitToolStripMenuItem.Text = "Player unit choice field";
            playerUnitToolStripMenuItem.Click += playerUnitToolStripMenuItem_Click;
            // 
            // setDirectToolStripMenuItem
            // 
            setDirectToolStripMenuItem.Name = "setDirectToolStripMenuItem";
            setDirectToolStripMenuItem.Size = new Size(242, 26);
            setDirectToolStripMenuItem.Text = "Set direct player unit";
            setDirectToolStripMenuItem.Click += setDirectToolStripMenuItem_Click;
            // 
            // setEnemyUnitToolStripMenuItem
            // 
            setEnemyUnitToolStripMenuItem.Name = "setEnemyUnitToolStripMenuItem";
            setEnemyUnitToolStripMenuItem.Size = new Size(242, 26);
            setEnemyUnitToolStripMenuItem.Text = "Set enemy unit";
            // 
            // setNPCToolStripMenuItem
            // 
            setNPCToolStripMenuItem.Name = "setNPCToolStripMenuItem";
            setNPCToolStripMenuItem.Size = new Size(242, 26);
            setNPCToolStripMenuItem.Text = "Set NPC";
            // 
            // Top_EventsUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuStrip1);
            Name = "Top_EventsUC";
            Size = new Size(245, 83);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem placeActorToolStripMenuItem;
        private ToolStripMenuItem playerUnitToolStripMenuItem;
        private ToolStripMenuItem setDirectToolStripMenuItem;
        private ToolStripMenuItem setEnemyUnitToolStripMenuItem;
        private ToolStripMenuItem setNPCToolStripMenuItem;
    }
}
