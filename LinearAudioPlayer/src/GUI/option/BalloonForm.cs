using FINALSTREAM.Commons.Controls.BalloonWindow;

namespace FINALSTREAM.LinearAudioPlayer.GUI.option
{
    public partial class BalloonForm : BalloonWindow
    {
        public BalloonForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            LinearGlobal.LinearConfig.DatabaseConfig.LimitCount = this.numFilteringCount.Value;
            this.Hide();
        }

        private void BalloonForm_Load(object sender, System.EventArgs e)
        {
            this.numFilteringCount.Value = LinearGlobal.LinearConfig.DatabaseConfig.LimitCount;
        }
    }
}
