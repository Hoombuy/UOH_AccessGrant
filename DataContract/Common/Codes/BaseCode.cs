using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Common.Codes
{

    /// <summary>
    /// 基础代码基类
    /// </summary>
    [Serializable]
    public class BaseCode : BaseModelClass
    {
        #region 属性
        /// <summary>
        /// 代码
        /// </summary>
        public string DM
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string MC
        {
            get;
            set;
        }

        /// <summary>
        /// 对应代码表名
        /// </summary>
        public virtual string Code_TableName
        {
            get { return null; }
        }

        #endregion

        public override string ToString()
        {
            return MC;
        }

    }
}
