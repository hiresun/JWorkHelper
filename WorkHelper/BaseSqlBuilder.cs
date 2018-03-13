using System;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using DevLogHelper.Resources;
using System.Data;
using System.Collections;
using System.Web.Caching;
using System.Web;

namespace DevLogHelper
{
    public partial class BaseSqlBuilder : Form
    {
        readonly ResourceManager _rm = new ResourceManager(typeof(ResourceDevCode));
        public BaseSqlBuilder()
        {
            InitializeComponent();
        }

        private void BaseSqlBuilder_Load(object sender, EventArgs e)
        {
            //初始化数据库链接
            this.txtdataSource.Text = "192.168.2.230";
            this.txtdataCatalog.Text = "tjprj";
            this.txtuserName.Text = "erptest";
            this.txtuserPwd.Text = "test@123456";
        }
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string msg = _rm.GetString("BaseSqlTip");
            try
            {

                //缓存数据库链接
                HttpRuntime.Cache.Insert("dataSource", this.txtdataSource.Text);
                HttpRuntime.Cache.Insert("dataCatalog", this.txtdataCatalog.Text);
                HttpRuntime.Cache.Insert("userName", this.txtuserName.Text);
                HttpRuntime.Cache.Insert("userPwd", this.txtuserPwd.Text);
                if (String.IsNullOrEmpty(txt_TableName.Text) || txt_TableName.Text.Trim() == string.Empty) { MessageBox.Show("请输入表名"); return; }


                BaseSql.BaseSql sq = new BaseSql.BaseSql();
                StringBuilder str = sq.BuilderCode(txtInput.Text, cbIsModel, txt_TableName.Text, ckb_Model.Checked, ckb_Insert.Checked, ckb_Update.Checked, ckb_Select.Checked, ckb_Delete.Checked, ckbExcel.Checked);
                txtResult.Text = str.ToString();
                Clipboard.SetDataObject(str.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            labTip.Text = msg;
        }

    }
}
