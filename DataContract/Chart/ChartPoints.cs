using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataContract.Chart
{
    /// <summary>
    /// 统计表格数据元类
    /// </summary>
    public class ChartPoints : Common.BaseModelClass
    {
       
        #region 属性
        /// <summary>
        /// 封装的节点涉及显示参数
        /// </summary>
        public string TextArgument
        {
            get;
            set;
        }

        /// <summary>
        /// 封装的节点涉及值集合
        /// </summary>
        public double[] Values
        {
            get;
            set;
        }
        #endregion

    }

    /// <summary>
    /// 统计表格数据元集合类
    /// </summary>
    public class ChartPoints_List
    {
        public List<ChartPoints> ObjectList = new List<ChartPoints>();

        /// <summary>
        /// 该节点集合内的值（Y轴）数量
        /// </summary>
        public int ValueCount
        {
            get;
            set;
        }

        public int Count
        {
            get
            {
                return this.ObjectList.Count;
            }
        }

        /// <summary>
        /// 封装的节点涉及值集合
        /// </summary>
        public List<string> ValueTitles
        {
            get
            {
                return this.ValueTitlesField;
            }
        }
        private List<string> ValueTitlesField = new List<string>();

        public ChartPoints this[int index]
        {
            get
            {
                return (ChartPoints)this.ObjectList[index];
            }
        }
        public ChartPoints this[string Key]
        {
            get
            {
                for (int i = 0; i < this.ObjectList.Count; i++)
                {
                    if (Key.Trim() == ((ChartPoints)this.ObjectList[i]).TextArgument.ToString().Trim())
                    {
                        return (ChartPoints)this.ObjectList[i];
                    }
                }
                return null;
            }
        }

        public DataTable TheDataTable
        {
            get;
            set;
        }

        public ChartPoints_List()
        {

        }
    }
}
