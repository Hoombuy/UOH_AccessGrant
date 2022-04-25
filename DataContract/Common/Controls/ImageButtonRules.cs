using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Common.Controls
{
    // <summary>
    /// 统计规则类
    /// </summary>

    [Serializable]
    public class ImageButtonRules
    {
        #region 属性

        /// <summary>
        /// 目录图标路径
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 高亮时变化图标路径
        /// </summary>
        public string OnImageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 目录描述信息
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 描述性说明
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 目录跳转路径
        /// </summary>
        public string RedirectUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 跳转打开方式
        /// </summary>
        public string OpenType
        {
            get;
            set;
        }
        #endregion

        public ImageButtonRules()
        {

        }
    }
}
