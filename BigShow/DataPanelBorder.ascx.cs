using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebInterface.HugeDataShow
{
    public partial class DataPanelBorder : System.Web.UI.UserControl
    {
        protected string _title;

        public string Title
        {
            set
            {
                this._title = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}