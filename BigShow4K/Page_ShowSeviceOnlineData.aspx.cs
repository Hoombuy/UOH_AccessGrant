using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BigShow4K
{
    public partial class Page_ShowSeviceOnlineData : System.Web.UI.Page
    {
        DataAccess.DAL_Base GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                this.ShowData();
            }

        }

        public void ShowData()
        {

            this.ShowTypePie();
            this.ShowDataTranRule();

        }

        private void ShowTypePie()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable Table_Pie = GD_ZF.GetDataTable("select processname as Name, count(*) as Value from ZFBPMX.BPM_PRO_RUN_HIS t group by processname ");
            Session["ShowTypePie"] = BusinessControls.Chart.Control_Chart.Fill_Common(Table_Pie);
        }
        private void ShowDataTranRule()
        {
           

            DataTable TableCJ = GD_ZF.GetDataTable("   select a.datasource||a.datatarget||sm ITEMID, a.datasource as Name from   ZFDXC.UOH_DATATRANRULE  a union  select a.datasource || a.datatarget || sm ITEMID, a.datatarget as Name from ZFDXC.UOH_DATATRANRULE a      ");

            DataTable TableKC = GD_ZF.GetDataTable("    select MC as Name,Datacount   as Value, TypeMC as  LevelNo from  ZFDXC.UOH_SYSTEMINFO   ");

            Session["DataTranRule"] = BusinessControls.Chart.Control_Chart.Fill_ForGraph(TableKC, TableCJ);
        }



        public string FWSXZS
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count(*) from ZFBPMX.BPM_PRO_RUN_HIS  ");

            }
        }
        public string BJSXZS
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count(*) from ZFBPMX.BPM_PRO_RUN_HIS where status <> 1  ");
            }
        }
        public string FWLBS
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count( distinct processname) from ZFBPMX.BPM_PRO_RUN_HIS    ");
            }
        }
        public string BLHJCZS
        {
            get
            {
                return GD_ZF.GetDataNumValue("  select count(*) from ZFBPMX.BPM_PRO_STATUS t   ");
            }
        }
        



        protected void BigShow_Click(object sender, EventArgs e)
        {
            //if (((LinkButton)sender).CommandName == "SystemTree")
            //{
            //    this.if_BigShow.Src = "/CommonPage/EChart/Page_EChart3_Common_Tree.aspx?SN=SystemTree&&Title=红河学院信息系统列表";
            //}
            //if (((LinkButton)sender).CommandName == "DataAssets")
            //{
            //    this.if_BigShow.Src = "/CommonPage/EChart/Page_EChart3_Common_PctorialBar.aspx?SN=DataAssets&&Title=数据资产";
            //}
            //if (((LinkButton)sender).CommandName == "DataTranRule")
            //{
            //    this.if_BigShow.Src = "/CommonPage/EChart/Page_EChart3_Common_Graph.aspx?SN=DataTranRule&&Title=数据交换规则";
            //}


            //this.ClientScript.RegisterStartupScript(this.GetType(), "updateScriptmessage", @"<script>$('#BigShowModal').modal('show');</script>");

        }

        protected void DevDataLineShow_Click(object sender, EventArgs e)
        {

        }
    }
}
