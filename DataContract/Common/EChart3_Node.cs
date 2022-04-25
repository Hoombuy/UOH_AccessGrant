using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Common
{
     

    [Serializable]
    public class Node_ECharts : Common.BaseModelClass
    {
        #region 属性

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        public float VALUE
        {
            get;
            set;
        }

        /// <summary>
        /// 层级
        /// </summary>
        public string LEVELNO
        {
            get;
            set;
        }
        
        #endregion
    }

    [Serializable]
    public class Link_ECharts : Common.BaseModelClass
    {
        #region 属性
        /// <summary>
        /// 源
        /// </summary>
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// 目标
        /// </summary>
        public string Target
        {
            get;
            set;
        }

        /// <summary>
        /// 连接类别
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public string WEIGHT
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public float Value
        {
            get;
            set;
        }

        

        public char categories { get; set; }


        #endregion
    }

    [Serializable]
    public class Data_ECharts : Common.BaseModelClass
    {
        #region 属性


        /// <summary>
        /// 名称
        /// </summary>
        public string NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string VALUE
        {
            get;
            set;
        }
       
    
        #endregion
    }

    [Serializable]
    public class DataObject_ECharts : DataContract.Common.BaseModelClass
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string TheName
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string TheValue
        {
            get;
            set;
        }


        private List<Node_ECharts> _TheNodes;
        public List<Node_ECharts> TheNodes
        {
            get
            {
                if (_TheNodes == null && TheLinks != null)
                {
                    _TheNodes = new List<Node_ECharts>();
                    for (int i = 0; i < TheLinks.Count; i++)
                    {
                        bool yy = false;
                        for (int j = 0; j < _TheNodes.Count; j++)
                        {
                            if (_TheNodes[j].NAME == TheLinks[i].Source)
                            {
                                yy = true;
                                break;
                            }
                        }
                        if (yy == false)
                        {
                            _TheNodes.Add(new Node_ECharts() { NAME = TheLinks[i].Source });
                        }
                        yy = false;
                        for (int j = 0; j < _TheNodes.Count; j++)
                        {
                            if (_TheNodes[j].NAME == TheLinks[i].Target)
                            {
                                yy = true;
                                break;
                            }
                        }
                        if (yy == false)
                        {
                            _TheNodes.Add(new Node_ECharts() { NAME = TheLinks[i].Target });
                        }
                    }
                }
                return this._TheNodes;
            }
            set
            {
                this._TheNodes = value;
            }
        }

        public List<Node_ECharts> TheIndicator
        {
            get;
            set;
        }

        public List<Link_ECharts> TheLinks
        {
            get;
            set;
        }


        public List<List<Data_ECharts>> TheDatasList
        {
            get;
            set;
        }


        public List<DataObject_ECharts> TheChildren
        {
            get;
            set;
        }

        public DataTable TheDataTable
        {
            get;
            set;
        }

        public void FillData(DataTable _Table)
        {
            if (TheDatasList == null)
            {
                this.TheDatasList = new List<List<Data_ECharts>>();
            }
            List<Data_ECharts> _List = new List<Data_ECharts>();
            for (int i = 0; i < _Table.Rows.Count; i++)
            {
                _List.Add(new Data_ECharts() { NAME = _Table.Rows[i]["Name"].ToString(), VALUE = _Table.Rows[i]["Value"].ToString() });
            }
            this.TheDatasList.Add(_List);
        }

      
    }

}
