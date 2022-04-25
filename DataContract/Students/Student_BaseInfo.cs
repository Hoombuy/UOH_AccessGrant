using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract
{
    /// <summary>
    /// 留学生基本信息对象类
    /// </summary>
    [Serializable]
    public class XSJBXXB : Common.BaseModelClass
    {
        #region 属性

        /// <summary>
        /// 001学号
        /// </summary>
        public string XH { get; set; }
        /// <summary>
        /// 002姓名
        /// </summary>
        public string XM { get; set; }
        /// <summary>
        /// 003性别
        /// </summary>
        public DataContract.Common.Codes.Code_XB XB { get; set; }
        /// <summary>
        /// 004出生日期
        /// </summary>
        public string CSRQ { get; set; }
        /// <summary>
        /// 005政治面貌
        /// </summary>
        public string ZZMM { get; set; }
        /// <summary>
        /// 006民族
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 099托管学院
        /// </summary>
        public DataContract.Common.Codes.Code_XY XY { get; set; }
        /// <summary>
        /// 011专业
        /// </summary>
        public DataContract.Common.Codes.Code_ZY ZY { get; set; }
        /// <summary>
        /// 012行政班
        /// </summary>
        public DataContract.Common.Codes.Code_BJ XZB { get; set; }
        /// <summary>
        /// 013学制
        /// </summary>
        public int XZ { get; set; }

        /// <summary>
        /// 015学籍状态
        /// </summary>
        public string XJZT { get; set; }

        /// <summary>
        /// 016当前所在级
        /// </summary>
        public int DQSZJ { get; set; }

        /// <summary>
        /// 099是否留学生
        /// </summary>
        public string SFLXS { get; set; }

        /// <summary>
        /// 099手机号码
        /// </summary>


        /// <summary>
        /// 学生照片
        /// </summary>
        public DataContract.Student.Student_Photo ZP { get; set; }


        /// <summary>
        /// 专业名称
        /// </summary>
        public string _ZYMC
        {
            get
            {
                return this.ZY.MC;
            }
        }

        /// <summary>
        /// 学院名称
        /// </summary>
        public string _XYMC
        {
            get
            {
                return this.XY.MC;
            }
        }

        /// <summary>
        /// 学院名称
        /// </summary>
        public string _XZBMC
        {
            get
            {
                return this.XZB.MC;
            }
        }

        /// <summary>
        /// 性别名称
        /// </summary>
        public string _XBMC
        {
            get
            {
                return this.XB.MC;
            }
        }

        #endregion

        public override string ToString()
        {
            return this.XH.ToString();
        }
    }
}