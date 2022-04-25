using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessControls.Users;

namespace BigShow4K
{
    public partial class BasePage_HugeDataShow : System.Web.UI.MasterPage
    {
        public bool Div_ContentVisible
        {
            set
            {
                this.contentwrapper.Visible = value;
            }
        }

     
        public System.Object ThePageObject
        {
            get
            {
                if (Session["ObjectPage_" + this.PageTitle + this.PageTag] != null)
                {
                    return Session["ObjectPage_" + this.PageTitle + this.PageTag];
                }
                return null;
            }
            set
            {
                if (this.PageTag == "")
                {
                    this.PageTag = DateTime.Now.ToShortTimeString();
                }
                Session["ObjectPage_" + this.PageTitle + this.PageTag] = value;
            }
        }

        protected string UserName
        {
            get
            {
                if (Session["LoginUser"] != null)
                {
                    return ((DataContract.Users.User)Session["LoginUser"]).U_USERNAME;
                }
                return "";
            }
        }
 
        protected string UserType
        {
            get
            {
                if (Session["LoginUser"] != null)
                {
                    if (((DataContract.Users.User)Session["LoginUser"]).U_USERTYPE == "Admin")
                    {
                        return "系统管理者";
                    }
                    else
                    { return "普通用户"; }
                }
                return "";
            }
        }

        public DataContract.Users.User TheLoginedUser
        {
            get
            {
                if (Session["LoginUser"] != null)
                {
                    return ((DataContract.Users.User)Session["LoginUser"]);
                }
                return null;
            }
        }

    
        public string PageTitle
        {
            get
            {
                if (ViewState["PageTitle"] == null)
                    ViewState["PageTitle"] = "";
                return ViewState["PageTitle"].ToString();
            }
            set
            {
                ViewState["PageTitle"] = value;
            }
        }

       

        public string PageTag
        {
            set
            {
                this.LabelInVisible.Text = value;
            }
            get
            {
                return this.LabelInVisible.Text;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["LoginUser"] == null)
            //{
            //    Response.Redirect("/Login.aspx");
            //}

        }


    }
}