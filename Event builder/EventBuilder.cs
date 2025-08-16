using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Event_builder
{
    public partial class EventBuilder : Form
    {
        #region Blockly setup
        private async void EventBuilder_Load(object sender, EventArgs e)
        {
            blocklyWebView2.WebMessageReceived += blocklyWebView2_WebMessageRecieved;
            string htmlPath = Path.Combine(Application.StartupPath, "BlocklyEditor.html");
            await blocklyWebView2.EnsureCoreWebView2Async();
            blocklyWebView2.Source = new Uri(htmlPath);
        }

        private void blocklyWebView2_WebMessageRecieved(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string json = e.WebMessageAsJson;
            File.WriteAllText("EventScript.json", json);
        }

        private async void LoadBlocks(string json)
        {
            string script = $"loadBlocksJson({json});";
            await blocklyWebView2.ExecuteScriptAsync(script);
        }
        #endregion
        
        public EventBuilder()
        {
            InitializeComponent();
        }
    }
}
