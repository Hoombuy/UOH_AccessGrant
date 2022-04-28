using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BigShow4K
{
    public partial class UCT_LastPList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataAccess.DAL_Base GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);
            DataTable Table_ZFJXRW = GD_ZF.GetDataTable("  select * from  (select processname, createtime,  rpad(substr(creator,1,1), length(creator)+1,'*') as creator  from ZFBPMX.BPM_PRO_RUN_HIS t order by createtime desc) where rownum <= 15");
            this.GridView_Main.DataSource = Table_ZFJXRW;
            this.GridView_Main.DataBind();
        }

        protected string GetColor(string proname)
        {
            if (proname == "学生外出申请审批V3")
            {
                return "background-color : #0562ad;";
            }
            return "background-color:#ff9800;";
        }

        protected void DevDataLineShow_Click(object sender, EventArgs e)
        {

        }

        protected void GridView_Main_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}