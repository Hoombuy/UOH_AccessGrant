using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract
{
    /// <summary> 
    /// 导师记录信息 
    /// </summary> 
    [Serializable]
    public class JSXXB : Common.BaseModelClass
    {
        /// 001职工号 
        /// </summary> 
        public string ZGH { get; set; }
        /// 002导师姓名 
        /// </summary> 
        public string XM { get; set; }
        /// 003学院名称 
        /// </summary> 
        public string XYMC { get; set; }
        /// 004学院代码 
        /// </summary> 
        public string XYDM { get; set; }
        /// 005性别
        /// </summary> 
        public string XB { get; set; }
        /// 006职称 
        /// </summary> 
        public string ZC { get; set; }
        /// 007联系电话
        /// </summary> 
        public string LXDH { get; set; }
        /// 007名下学生
        /// </summary> 
        public int MXXS { get; set; }
        /// 008已选人数
        /// </summary> 
        public string YXRS { get; set; }
        /// 009剩余名额
        /// </summary> 
        /// 
        public string XH { get; set; }
        /// 010 学号
        /// </summary> 
        public string SYME { get; set; }

        /// 011出生日期 
        /// </summary> 
        public string CSRQ { get; set; }
        /// 012电子邮箱地址 
        /// </summary> 
        public string EMLDZ { get; set; }
        /// 013教职工类别 
        /// </summary> 
        public string JZGKB { get; set; }
        /// 014所属部门（学院） 
        /// </summary> 
        public string BM { get; set; }
        /// 015所属科室（系） 
        /// </summary> 
        public string KS { get; set; }
        /// 016职务 
        /// </summary> 
        public string ZW { get; set; }
        /// 017教师简介 
        /// </summary> 
        public string JSJJ { get; set; }

        public override string ToString()
        {
            return this.ZGH.ToString();
        }
    }
}

