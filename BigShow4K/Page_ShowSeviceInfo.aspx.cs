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
    public partial class Page_ShowSeviceInfo : System.Web.UI.Page
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

       

        }
 
    }
}
