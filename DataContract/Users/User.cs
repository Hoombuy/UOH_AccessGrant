using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Users
{

    /// <summary>
    /// 系统用户类
    /// </summary>  
    [Serializable]
    public class User : Common.BaseModelClass
    {
        #region 属性
      
        /// <summary>
        /// 用户号
        /// </summary>
        public string U_USERNAME
        {
            get;
            set;
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string U_PASSWORD
        {
            get;
            set;
        }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string U_REALNAME
        {
            get;
            set;
        }

        /// <summary>
        /// 用户所属部门
        /// </summary>
        public DataContract.Common.Codes.Code_XY U_BM
        {
            get;
            set;
        }
 
        ///<summary>
        ///用户类型
        ///</summary>
        public string U_USERTYPE
        {
            get;
            set;
        }

        ///<summary>
        ///用户电话号码
        ///</summary>
        public string U_PHONE
        {
            get;
            set;
        }

        

        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string _U_BMMC
        {
            get
            {
                return this.U_BM.MC;
            }
        }

        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string _U_USERTYPEMC
        {
            get
            {
                if (this.U_USERTYPE == "Admin")
                    return "系统管理员";
                else
                    return "普通管理员";
            }
        }

        #endregion

        public override string ToString()
        {
            return U_USERNAME;
        }

    }
}
