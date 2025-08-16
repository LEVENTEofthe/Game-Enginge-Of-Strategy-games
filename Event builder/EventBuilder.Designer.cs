namespace Event_builder
{
    partial class EventBuilder
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
            blocklyWebView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)blocklyWebView2).BeginInit();
            SuspendLayout();
            // 
            // blocklyWebView2
            // 
            blocklyWebView2.AllowExternalDrop = true;
            blocklyWebView2.CreationProperties = null;
            blocklyWebView2.DefaultBackgroundColor = Color.White;
            blocklyWebView2.Location = new Point(50, 25);
            blocklyWebView2.Name = "blocklyWebView2";
            blocklyWebView2.Size = new Size(485, 298);
            blocklyWebView2.TabIndex = 0;
            blocklyWebView2.ZoomFactor = 1D;
            // 
            // EventBuilder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(blocklyWebView2);
            Name = "EventBuilder";
            Text = "Form1";
            Load += EventBuilder_Load;
            ((System.ComponentModel.ISupportInitialize)blocklyWebView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 blocklyWebView2;
    }
}
