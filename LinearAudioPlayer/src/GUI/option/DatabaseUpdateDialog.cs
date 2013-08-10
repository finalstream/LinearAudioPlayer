using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FINALSTREAM.Commons.Database;
using FINALSTREAM.Commons.Utils;
using FINALSTREAM.LinearAudioPlayer.Resources;

namespace FINALSTREAM.LinearAudioPlayer.GUI
{
    public partial class DatabaseUpdateDialog : Form
    {
        private bool _result = false;



        public DatabaseUpdateDialog()
        {
            InitializeComponent();
        }

        public bool Result
        {
            get { return _result; }
        }

        private void DatabaseUpdateDialog_Load(object sender, System.EventArgs e)
        {
            object[][] resultList = SQLiteManager.Instance.executeQueryNormal(SQLResource.SQL020);

            foreach (var recordList in resultList)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = recordList[0].ToString();
                lvi.SubItems.Add(recordList[1].ToString());

                string exists = "";
                if (Directory.Exists(recordList[0].ToString()))
                {
                    exists = "○";
                } else
                {
                    exists = "×";
                }
                lvi.SubItems.Add(exists);

                filepathList.Items.Add(lvi);
            }

        }

        private void filepathList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            txtBefore.Text = e.Item.Text;
        }

        private void txtAfter_TextChanged(object sender, System.EventArgs e)
        {
            if (Directory.Exists(txtAfter.Text))
            {
                buttonUpdate.Enabled = true;
            }
            else
            {
                buttonUpdate.Enabled = false;
            }
        }

        private void buttonUpdate_Click(object sender, System.EventArgs e)
        {

            if (MessageUtils.showQuestionMessage(txtBefore.Text + "\nから\n" + txtAfter.Text + "\nに更新します。よろしいですか？") == DialogResult.OK)
            {
                update();
                _result = true;
                this.Close();
            }

        }

        private void update()
        {
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            
            paramDic.Add("BeforeString", txtBefore.Text);
            paramDic.Add("AfterString", txtAfter.Text);
            paramDic.Add("BeforeLikeString", "%" + txtBefore.Text + "%");

            SQLiteManager.Instance.executeNonQuery(SQLResource.SQL021);

        }
    }
}
