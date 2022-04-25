using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;

namespace WebInterface.CommonPage
{
    public partial class UCT_Common_RepeaterList : System.Web.UI.UserControl
    {
        protected virtual System.Web.UI.WebControls.Repeater TheGridView
        {
            get
            {
                return null;
            }
        }

        protected virtual Label LabelSession
        {
            get
            {
                return null;
            }
        }

        protected virtual Label TheLabelInfo
        {
            get
            {
                return null;
            }
        }

        public int DataCount
        {
            get
            {
                return this.TheGridView.Items.Count;
            }
        }

        public bool IfShowTitle
        {
            set
            {
                if (this.TheLabelInfo != null)
                {
                    this.TheLabelInfo.Visible = value;
                }
            }
        }

        protected object TheDataList
        {
            get
            {
                return Session[String.Format("ObjectList_{0}{1}", this.ID, this.LabelSession.Text)];
            }
            set
            {
                if (String.Compare(this.LabelSession.Text, "", false) == 0)
                {
                    this.LabelSession.Text = DateTime.Now.ToString();
                }
                Session[String.Format("ObjectList_{0}{1}", this.ID, this.LabelSession.Text)] = value;
            }
        }

        protected void GridView_Main_PageIndexChanged(object sender, EventArgs e)
        {

            this.TheGridView.DataBind();
        }
    }
}