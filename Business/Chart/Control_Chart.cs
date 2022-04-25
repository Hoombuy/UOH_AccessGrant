using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataContract.Common;

namespace BusinessControls.Chart
{
    public class Control_Chart : Control_BaseClass
    {
        public Control_Chart()
        {
            this.GD = DataAccess.DAL_Contol.Get_GetDataObject();
        }

        public DataContract.Chart.ChartPoints_List GetChartPoints_List(string TheSQL)
        {
            DataContract.Chart.ChartPoints_List ThePL = new DataContract.Chart.ChartPoints_List();
            DataTable Table = new DataTable();
            Table = this.GD.GetDataTable(TheSQL);
            if (Table != null)
            {
                ThePL.TheDataTable = Table;
                int n = 0;
                for (int i = 0; i < Table.Columns.Count; i++)
                {
                    if (Table.Columns[i].ColumnName.Contains("VALUES"))
                    {
                        n++;
                        ThePL.ValueTitles.Add(Table.Columns[i].ColumnName.Replace("VALUES", ""));
                    }
                }
                ThePL.ValueCount = n;
                foreach (DataRow row in Table.Rows)
                {
                    DataContract.Chart.ChartPoints NewXX_JXDJBSJ = new DataContract.Chart.ChartPoints();
                    NewXX_JXDJBSJ.TextArgument = row["TextArgument"].ToString();
                    NewXX_JXDJBSJ.Values = new double[ThePL.ValueCount];
                    n = 0;
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        if (row.Table.Columns[i].ColumnName.Contains("VALUES"))
                        {
                            if (row[row.Table.Columns[i].ColumnName].ToString() == "")
                            {
                                row[row.Table.Columns[i].ColumnName] = 0;
                            }
                            NewXX_JXDJBSJ.Values[n] = double.Parse(row[row.Table.Columns[i].ColumnName].ToString());
                            n++;
                        }
                    }
                    ThePL.ObjectList.Add(NewXX_JXDJBSJ);
                }
            }
            return ThePL;
        }

        /// <summary>
        /// 为日历热力图(CalendarHeatmap)填充数据 
        /// </summary>
        /// <param name="Range">日历中显示的范围，以"2020-02"的格式表示月，"2020"表示全年</param>
        /// <param name="_TableData">数据对象列表【包含Name（日期）， VALUE（值） 字段】</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_ForMap(  DataTable _TableData)
        {

            #region 调用示例
            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);
            //DataTable TableCJ = GD_ZF.GetDataTable("    select b.mc as Name , count(*) as Value  from UOH_ES.REPORTINFO t inner  join uoh_es.dm_dq b on t.ri_szd3 = b.dm  where ri_date like '2021-01-03'  group by b.mc ");
            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForMap(TableCJ);
            #endregion

            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = "" };
            _TheData.TheNodes = new List<Node_ECharts>();
            for (int i = 0; i < _TableData.Rows.Count; i++)
            {
                _TheData.TheNodes.Add(new Node_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString(), VALUE = float.Parse(_TableData.Rows[i]["Value"].ToString()) });
            }

            return _TheData;
        }

        /// <summary>
        /// 为日历热力图(CalendarHeatmap)填充数据 
        /// </summary>
        /// <param name="Range">日历中显示的范围，以"2020-02"的格式表示月，"2020"表示全年</param>
        /// <param name="_TableData">数据对象列表【包含Name（日期）， VALUE（值） 字段】</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_ForCalendarHeatmap(string Range, DataTable _TableData)
        {

            #region 调用示例
            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable TableCJ = GD_ZF.GetDataTable(" select ri_date as Name ,count(*) as Value  from UOH_ES.REPORTINFO t where ri_date like '2020-12%'  group by ri_date order by ri_date ");
            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForCalendarHeatmap("2020-12", TableCJ);
            #endregion

            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = Range };
            _TheData.TheNodes = new List<Node_ECharts>();
            for (int i = 0; i < _TableData.Rows.Count; i++)
            {
                _TheData.TheNodes.Add(new Node_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString(), VALUE = float.Parse(_TableData.Rows[i]["Value"].ToString()) });
            }

            return _TheData;
        }

        /// <summary>
        /// 为雷达图(Rader)填充数据【一个需要统计的数据实体对应_TableData表中一条数据，每一个数据实体同时也将生成成为Legend、Series中的一项】【 即坐标节点量不变，实体增加时增加Series】
        /// </summary>
        /// <param name="_TableIndicator">坐标轴及极值对象列表【包含Name（坐标轴名称），Value（该轴最大值Max）】</param>
        /// <param name="_TableData">数据对象列表【包含Name（统计对象名称）， 任意其他字段（这些字段列数量应与_TableIndicator表中数据行数量一致，顺序一致，每一个字段值将代表本行统计对象在对应顺序坐标轴上的描值）】</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_ForRader(DataTable _TableIndicator, DataTable _TableData)
        {

            #region 调用示例
            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);
            //DataTable TableIn = GD_ZF.GetDataTable("select  kcxz NAME, max(cj) Value from zfxfzb.cjb where xh in (select xh from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj ='2017') and(cj is not null and cj <> 0) group by   kcxz  ");

            //DataTable TableCJ = GD_ZF.GetDataTable(" select xm||' '||xh as Name, " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识必修课') 通识必修课, " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识选修课') 通识选修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '平台必修课' ) 平台必修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通必专项课' ) 通必专项课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业必修课' ) 专业必修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业选修课' ) 专业选修课  " +
            //                                                                   "   from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj = '2017'  and (xm like '周子%' or xm like '谢丹丹')   ");


            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForRader(TableIn, TableCJ);
            #endregion

            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = "" };
            _TheData.TheNodes = new List<DataContract.Common.Node_ECharts>();
            _TheData.TheIndicator = new List<Node_ECharts>();
            _TheData.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();

            //参考坐标对象列表处理
            for (int i = 0; i < _TableIndicator.Rows.Count; i++)
            {
                _TheData.TheIndicator.Add(new DataContract.Common.Node_ECharts() { NAME = _TableIndicator.Rows[i]["NAME"].ToString(), VALUE = float.Parse(_TableIndicator.Rows[i]["VALUE"].ToString()) });
            }

            for (int i = 0; i < _TableData.Rows.Count; i++)
            {
                _TheData.TheNodes.Add(new DataContract.Common.Node_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString() });
                List<DataContract.Common.Data_ECharts> _List = new List<DataContract.Common.Data_ECharts>();

                for (int j = 1; j < _TableData.Columns.Count; j++)
                {
                    _List.Add(new Data_ECharts() { NAME = _TableData.Columns[j].ColumnName, VALUE = _TableData.Rows[i][j].ToString() });

                }

                _TheData.TheDatasList.Add(_List);
            }
            return _TheData;
        }

        /// <summary>
        /// 为柱状图(Bar)填充数据【一个需要统计的数据实体对应_TableData表中一条数据， 该数据/实体可以有多个值，这些值将分别生成成为Legend、Series中的一项。 而每一个实体将作为主坐标上的一个节点出现】【即体系不变，实体增加时增加节点】
        /// </summary>
        /// <param name="_TableData">数据表[ 包含NAME（统计对象名称）、 任意其他字段（ColumnName将作为值项名称，对应到Legend中）]</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_ForBar(DataTable _TableData)
        {

            #region 调用示例
            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable TableCJ = GD_ZF.GetDataTable(" select xm||' '||xh as Name, " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识必修课') 通识必修课, " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识选修课') 通识选修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '平台必修课' ) 平台必修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通必专项课' ) 通必专项课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业必修课' ) 专业必修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业选修课' ) 专业选修课  " +
            //                                                                   "   from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj = '2017'    ");


            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForBar(TableCJ);
            #endregion

            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = "" };
            _TheData.TheNodes = new List<DataContract.Common.Node_ECharts>();
            _TheData.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();

            for (int i = 0; i < _TableData.Rows.Count; i++)
            {
                _TheData.TheNodes.Add(new DataContract.Common.Node_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString() });
                List<DataContract.Common.Data_ECharts> _List = new List<DataContract.Common.Data_ECharts>();

                for (int j = 1; j < _TableData.Columns.Count; j++)
                {
                    _List.Add(new Data_ECharts() { NAME = _TableData.Columns[j].ColumnName, VALUE = _TableData.Rows[i][j].ToString() });

                }

                _TheData.TheDatasList.Add(_List);
            }
            return _TheData;
        }

        /// <summary>
        /// 为折现图(Line)填充数据【一个实体对象作为数据表中一个数据行】【一个需要统计的数据实体对应_TableData表中一条数据，每一个数据实体同时也将生成成为Legend、Series中的一项】【 即坐标节点量不变，实体增加时增加Series】
        /// </summary>
        /// <param name="_TableData">数据表[ 包含NAME（统计对象名称）、 任意其他字段（每一个字段将生成主坐标轴上的一个描点项）]</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_ForLine_ObjectRow(DataTable _TableData)
        {
            #region 调用示例
            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable TableCJ = GD_ZF.GetDataTable(" select xm||' '||xh as Name, " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识必修课') 通识必修课, " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识选修课') 通识选修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '平台必修课' ) 平台必修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通必专项课' ) 通必专项课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业必修课' ) 专业必修课， " +
            //                                                                " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业选修课' ) 专业选修课  " +
            //                                                                   "   from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj = '2017'    ");


            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForBar(TableCJ);
            #endregion

            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = "" };
            _TheData.TheNodes = new List<DataContract.Common.Node_ECharts>();
            _TheData.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();

            for (int i = 0; i < _TableData.Rows.Count; i++)
            {
                _TheData.TheNodes.Add(new DataContract.Common.Node_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString() });
                List<DataContract.Common.Data_ECharts> _List = new List<DataContract.Common.Data_ECharts>();

                for (int j = 1; j < _TableData.Columns.Count; j++)
                {
                    _List.Add(new Data_ECharts() { NAME = _TableData.Columns[j].ColumnName, VALUE = _TableData.Rows[i][j].ToString() });

                }

                _TheData.TheDatasList.Add(_List);
            }
            return _TheData;
        }

        /// <summary>
        /// 为折现图(Line)填充数据【一个实体对象作为数据表中n行， 每行只有一个value值，n为主坐标轴项数数量】【一个需要统计的数据实体对应_TableData表中n条数据，每一个数据实体同时也将生成成为Legend、Series中的一项】【 即坐标节点量不变，实体增加时增加Series】
        /// </summary>
        /// <param name="_TableData">数据表[ 包含NAME(统计内容名称/主坐标轴项名称)、ITEMID（统计对象名称）、 VALUE（值）]【请先按照ItemID、Name排序，且保障每一个实体对象一定都对应固定的n行，不多不少】</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_ForLine_ObjectItem(DataTable _TableData)
        {
            #region 调用示例
            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable TableCJ = GD_ZF.GetDataTable("  select kcxz as Name, xm||' '||xh as ItemID ,  cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) Value  from zfxfzb.cjb  where xh in (select xh   from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj = '2017' and ( xm like '陈雪' or xm like '罗素芬'))   group by kcxz, xm, xh   order by xh, kcxz  ");
            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForLine_ObjectItem(TableCJ);
            #endregion

            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = "" };
            _TheData.TheNodes = new List<DataContract.Common.Node_ECharts>();
            _TheData.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();



            for (int i = 0; i < _TableData.Rows.Count; i++)
            {

                bool cf = false;
                for (int j = 0; j < _TheData.TheDatasList.Count; j++)
                {
                    if (_TableData.Rows[i]["ITEMID"].ToString() == _TheData.TheNodes[j].NAME)
                    {
                        cf = true;
                        _TheData.TheDatasList[j].Add(new Data_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString(), VALUE = _TableData.Rows[i]["VALUE"].ToString() });
                        break;
                    }
                }
                if (cf == false)
                {
                    _TheData.TheNodes.Add(new DataContract.Common.Node_ECharts() { NAME = _TableData.Rows[i]["ITEMID"].ToString() });
                    List<DataContract.Common.Data_ECharts> _List = new List<DataContract.Common.Data_ECharts>();
                    _List.Add(new Data_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString(), VALUE = _TableData.Rows[i]["VALUE"].ToString() });
                    _TheData.TheDatasList.Add(_List);
                }
            }
            return _TheData;
        }

        /// <summary>
        /// 为关系图(Graph)填充数据【_TableLink 数据表中存在2条数据，同一个ITemID号，对应两个节点名，则两个节点连线（产生关系）】
        /// </summary>
        /// <param name="_TableData">节点数据表[ 包含NAME（节点名）、VALUE（值，决定节点大小）,LEVELNO (节点分类，legend 设置) 字段]</param>
        /// <param name="_TableLink">关系数据表[ 包含ITEMID（数据项标识）、NAME（节点名） ]_，如该数据表中存在2条数据，同一个ITemID号，对应两个节点名，则两个节点连线（产生关系）</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_ForGraph(DataTable _TableData, DataTable _TableLink)
        {

            #region 调用示例
            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable TableCJ = GD_ZF.GetDataTable(" select xh as ITEMID, kcmc as Name, a.xkkh from xsxkbo a,   (select  xkkh, kcmc from zfxfzb.jxrwbview b1 where xn = '2020-2021' and xq = '1' and kcxz = '通识选修课' and  ( kcgs ='科技进步与科学精神' or  kcgs ='语言与基本技能选修' or kcgs='生态环境与生命关怀'  or kcgs='文史经典与文化传承' or kcgs='学习与素质提高') group by kcmc, xkkh)  b where a.xkkh = b.xkkh and a.xh like '2019%'   ");

            //DataTable TableKC = GD_ZF.GetDataTable("   select kcmc as Name, sum(rs)  as Value, kcgs as LevelNo from (select kcmc, xkkh, nvl(kcgs,kcxz) as kcgs,  (select count(*) rs from zfxfzb.xsxkbo b where a.xkkh = b.xkkh and b.xh like '2019%'  ) rs from zfxfzb.jxrwbview a where xn = '2020-2021' and xq = '1' and kcxz = '通识选修课' and ( kcgs ='科技进步与科学精神' or  kcgs ='语言与基本技能选修' or kcgs='生态环境与生命关怀' or kcgs='文史经典与文化传承' or kcgs ='学习与素质提高' ) group by kcmc, xkkh, kcgs, kcxz) group by kcmc, kcgs order by kcgs ");

            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForGraph(TableKC, TableCJ);
            #endregion

            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = "" };
            _TheData.TheNodes = new List<DataContract.Common.Node_ECharts>();
            _TheData.TheLinks = new List<DataContract.Common.Link_ECharts>();

            for (int i = 0; i < _TableData.Rows.Count; i++)
            {

                _TheData.TheNodes.Add(new DataContract.Common.Node_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString(), LEVELNO = _TableData.Rows[i]["LEVELNO"].ToString(), VALUE = float.Parse(_TableData.Rows[i]["VALUE"].ToString()), });

            }
            _TableLink.TableName = "LINK";
            DataView ViewCJ = new DataView() { Table = _TableLink };
            DataView ViewCJ2 = new DataView() { Table = _TableLink };

            for (int i1 = 0; i1 < _TheData.TheNodes.Count; i1++)
            {


                for (int i2 = 0; i2 < _TheData.TheNodes.Count; i2++)
                {
                    int v = 0;
                    ViewCJ.RowFilter = "NAME='" + _TheData.TheNodes[i1].NAME + "' or   NAME='" + _TheData.TheNodes[i2].NAME + "'";
                    ViewCJ.Sort = "ITEMID";

                    for (int j = 1; j < ViewCJ.Count; j++)
                    {
                        if (ViewCJ[j]["ITEMID"].ToString() == ViewCJ[j - 1]["ITEMID"].ToString())
                        {
                            v++;
                        }
                    }



                    for (int j = 0; j < v; j++)
                    {
                        _TheData.TheLinks.Add(new DataContract.Common.Link_ECharts()
                        {
                            Source = _TheData.TheNodes[i1].NAME,
                            Target = _TheData.TheNodes[i2].NAME,
                            Value = j + 1
                        });
                    }


                }

            }

            return _TheData;
        }

        /// <summary>
        /// 为桑基图填充数据【TableLink表中仅需原始映射记录，关系将按Data表中层次数据依次自动创建】
        /// </summary>
        /// <param name="_TableData">层次数据表[ 包含NAME（层次名）、VALUE（同层次不同段值/名） 字段]</param>
        /// <param name="_TableLink">关系数据表[ 包含ITEMID（数据项标识）、NAME（层次名）、VALUE 字段（同层次不同段值/名）]</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_ForSanKey(DataTable _TableData, DataTable _TableLink)
        {

            #region 调用示例
            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable TableCJ = GD_ZF.GetDataTable(" select xh  as ITEMID, kcmc as NAME , max(floor(jd)) as VALUE from zfxfzb.cjb where xh in (select xh from zfxfzb.xsjbxxb where dqszj ='2017' and zymc like '计算机科学与技术') and kcxz like '专业必修%'  and kcmc in ( select kcmc from (select xh, kcmc ,max(jd) jd from zfxfzb.cjb where xh in (select xh from zfxfzb.xsjbxxb where dqszj ='2017' and zymc like '计算机科学与技术') and kcxz like '专业必修课' and ( kcmc like '%离散%'  or kcmc like '%数据结构%'  or kcmc like '%计算机网络%'  or kcmc like '%数据库%'  ) group by xh, kcmc) group by kcmc, jd having count(*) >1 ) group by xh, kcmc order by kcmc  ");

            //DataTable TableKC = GD_ZF.GetDataTable("   select kcmc as NAME , jd as VALUE from (select xh, kcmc ,max(floor(jd)) jd from zfxfzb.cjb where xh in (select xh from zfxfzb.xsjbxxb where dqszj ='2017' and zymc like '计算机科学与技术') and kcxz like '专业必修课' and ( kcmc like '%离散%'  or kcmc like '%数据结构%'  or kcmc like '%计算机网络%'  or kcmc like '%数据库%'  )  group by xh, kcmc) group by kcmc, jd having count(*) >1 order by kcmc, jd    ");

            //Session["S_JKZYKCJ"] = this.Fill(TableKC, TableCJ);
            #endregion

            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = "2017级计算机科学与技术本科专业课程修读成绩关联性" };
            _TheData.TheNodes = new List<DataContract.Common.Node_ECharts>();
            _TheData.TheLinks = new List<DataContract.Common.Link_ECharts>();
            int c = 1;
            for (int i = 0; i < _TableData.Rows.Count; i++)
            {
                if (_TheData.TheNodes.Count != 0)
                {
                    if (_TableData.Rows[i]["NAME"].ToString() != _TheData.TheNodes[_TheData.TheNodes.Count - 1].NAME)
                    {
                        c++;
                    }
                }
                _TheData.TheNodes.Add(new DataContract.Common.Node_ECharts() { NAME = _TableData.Rows[i]["NAME"].ToString(), LEVELNO = c.ToString(), VALUE = float.Parse(_TableData.Rows[i]["VALUE"].ToString()) });

            }
            _TableLink.TableName = "LINK";
            DataView ViewCJ = new DataView() { Table = _TableLink };
            DataView ViewCJ2 = new DataView() { Table = _TableLink };

            for (int i1 = 0; i1 < _TheData.TheNodes.Count; i1++)
            {
                ViewCJ.RowFilter = "NAME='" + _TheData.TheNodes[i1].NAME + "' and VALUE =" + _TheData.TheNodes[i1].VALUE + " ";
                for (int i2 = 0; i2 < _TheData.TheNodes.Count; i2++)
                {
                    if (_TheData.TheNodes[i1].LEVELNO == (int.Parse(_TheData.TheNodes[i2].LEVELNO) - 1).ToString())
                    {
                        int v = 0;

                        ViewCJ2.RowFilter = "NAME='" + _TheData.TheNodes[i2].NAME + "' and VALUE =" + _TheData.TheNodes[i2].VALUE + " ";

                        for (int j = 0; j < ViewCJ.Count; j++)
                        {
                            for (int j2 = 0; j2 < ViewCJ2.Count; j2++)
                            {
                                if (ViewCJ2[j2]["ITEMID"].ToString() == ViewCJ[j]["ITEMID"].ToString())
                                    v++;
                            }
                        }
                        _TheData.TheLinks.Add(new DataContract.Common.Link_ECharts()
                        {
                            Source = _TheData.TheNodes[i1].NAME + '-' + _TheData.TheNodes[i1].VALUE,
                            Target = _TheData.TheNodes[i2].NAME + '-' + _TheData.TheNodes[i2].VALUE,
                            Value = v
                        });

                    }
                }
            }

            return _TheData;
        }

        /// <summary>
        /// 为树图（Tree）填充数据
        /// </summary>
        /// <param name="_TableData">数据表[ 可包含任何字段 ]</param>
        /// <param name="_LinkField">从根目录开始的分组关键字段【按顺序进行】</param>
        /// <param name="_NodeField">叶节点显示内容对应字段</param>
        /// <param name="RootName">根名称</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_Tree(DataTable _TableData, ArrayList _LinkField, string _NodeField, string RootName)
        {
            #region 调用示例

            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable Table_ZFJXRW = GD_ZF.GetDataTable(" select kcdm, kcmc, xf, zxs, kkxy, kcxz, nvl(kcgs,'') kcgs from zfxfzb.jxrwbview where xn ='2020-2021' and xq ='1' and xkzt ='1' and kcxz like '通识%'  group by kcdm, kcmc, xf, zxs, kkxy, kcxz, kcgs   ");

            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_Tree(Table_ZFJXRW, new ArrayList() { "KCXZ", "KCGS" }, "KCMC", "2020-2021秋季通识课");

            #endregion
            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { TheName = RootName };
            _TheData.TheChildren = new List<DataContract.Common.DataObject_ECharts>();
            for (int i = 0; i < _TableData.Rows.Count; i++)
            {

                AppendChild_Tree(_TheData, _TableData.Rows[i], _LinkField, _NodeField);
            }

            return _TheData;

        }

        private static void AppendChild_Tree(DataContract.Common.DataObject_ECharts _TheData, DataRow _Row, ArrayList _LinkField, string _NodeField)
        {
            bool cf = false;
            for (int j = 0; j < _TheData.TheChildren.Count; j++)
            {
                if (_TheData.TheChildren[j].TheName == _Row[_LinkField[0].ToString()].ToString())
                {
                    cf = true;
                    break;
                }
            }
            if (cf == false)
            {
                DataContract.Common.DataObject_ECharts _N = new DataContract.Common.DataObject_ECharts() { TheName = _Row[_LinkField[0].ToString()].ToString(), TheChildren = new List<DataContract.Common.DataObject_ECharts>() };
                _N.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();
                _N.TheDatasList.Add(new List<DataContract.Common.Data_ECharts>());
                _TheData.TheChildren.Add(_N);

            }
            for (int j = 0; j < _TheData.TheChildren.Count; j++)
            {
                if (_TheData.TheChildren[j].TheName == _Row[_LinkField[0].ToString()].ToString())
                {
                    if (_LinkField.Count == 1)
                    {
                        _TheData.TheChildren[j].TheDatasList[0].Add(new DataContract.Common.Data_ECharts() { NAME = _Row[_NodeField].ToString(), VALUE = "1" });
                    }
                    else
                    {
                        ArrayList _LowLinkField = _LinkField.GetRange(1, _LinkField.Count - 1);


                        AppendChild_Tree(_TheData.TheChildren[j], _Row, _LowLinkField, _NodeField);
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// 为矩形树图（Tree）或 鸡尾酒旭日图（Sun-Drink）填充数据
        /// </summary>
        /// <param name="_TableData">数据表[ 可包含任何字段 ]</param>
        /// <param name="_LinkField">从根目录开始的分组关键字段【按顺序进行,最好只放一项，二级目录以下不会以不同颜色区分】</param>
        /// <param name="_NodeField">叶节点显示内容对应字段</param>
        /// <param name="_ValueField">值对应字段名</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_TreeMap(DataTable _TableData, ArrayList _LinkField, string _NodeField, string _ValueField)
        {
            #region 调用示例

            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable Table_ZFJXRW = GD_ZF.GetDataTable(" select kcdm, kcmc, xf, zxs, kkxy, kcxz, nvl(kcgs,kcxz) kcgs, count( distinct xkkh) as bs from zfxfzb.jxrwbview where xn ='2020-2021' and xq ='1' and xkzt ='1' and kcxz like '通识%'  group by kcdm, kcmc, xf, zxs, kkxy, kcxz, kcgs   ");

            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_TreeMap(Table_ZFJXRW, new ArrayList() { "KCGS" }, "KCMC", "BS");

            #endregion
            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { };
            _TheData.TheChildren = new List<DataContract.Common.DataObject_ECharts>();
            for (int i = 0; i < _TableData.Rows.Count; i++)
            {

                AppendChild_TreeMap(_TheData, _TableData.Rows[i], _LinkField, _NodeField, _ValueField);
            }

            return _TheData;

        }

        private static void AppendChild_TreeMap(DataContract.Common.DataObject_ECharts _TheData, DataRow _Row, ArrayList _LinkField, string _NodeField, string _ValueField)
        {
            bool cf = false;
            for (int j = 0; j < _TheData.TheChildren.Count; j++)
            {
                if (_TheData.TheChildren[j].TheName == _Row[_LinkField[0].ToString()].ToString())
                {
                    cf = true;
                    break;
                }
            }
            if (cf == false)
            {
                DataContract.Common.DataObject_ECharts _N = new DataContract.Common.DataObject_ECharts() { TheName = _Row[_LinkField[0].ToString()].ToString(), TheChildren = new List<DataContract.Common.DataObject_ECharts>() };
                _N.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();
                _N.TheDatasList.Add(new List<DataContract.Common.Data_ECharts>());
                _TheData.TheChildren.Add(_N);

            }
            for (int j = 0; j < _TheData.TheChildren.Count; j++)
            {
                if (_TheData.TheChildren[j].TheName == _Row[_LinkField[0].ToString()].ToString())
                {
                    if (_LinkField.Count == 1)
                    {
                        _TheData.TheChildren[j].TheDatasList[0].Add(new DataContract.Common.Data_ECharts() { NAME = _Row[_NodeField].ToString(), VALUE = _Row[_ValueField].ToString() });
                    }
                    else
                    {
                        ArrayList _LowLinkField = _LinkField.GetRange(1, _LinkField.Count - 1);
                        AppendChild_TreeMap(_TheData.TheChildren[j], _Row, _LowLinkField, _NodeField, _ValueField);
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// 为旭日图（Sun）填充数据
        /// </summary>
        /// <param name="_TableData">数据表[ 可包含任何字段 ]</param>
        /// <param name="_LinkField">从根目录开始的分组关键字段【按顺序进行，某子节点可跳过（不同枝条深度可不同，需要跳过时设置相关字段为空字符串值）】</param>
        /// <param name="_NodeField">叶节点显示内容对应字段名</param>
        /// <param name="_ValueField">值对应字段名</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_Sun(DataTable _TableData, ArrayList _LinkField, string _NodeField, string _ValueField)
        {
            #region 调用示例

            //var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable Table_ZFJXRW = GD_ZF.GetDataTable(" select kcdm, kcmc, xf, zxs, kkxy, kcxz, nvl(kcgs,'') kcgs, count( distinct xkkh) as bs from zfxfzb.jxrwbview where xn ='2020-2021' and xq ='1' and xkzt ='1' and kcxz like '通识%'  group by kcdm, kcmc, xf, zxs, kkxy, kcxz, kcgs   ");
            //Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_Sun(Table_ZFJXRW, new ArrayList() { "KCXZ", "KCGS" }, "KCMC", "BS");

            #endregion
            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts() { };
            _TheData.TheChildren = new List<DataContract.Common.DataObject_ECharts>();
            for (int i = 0; i < _TableData.Rows.Count; i++)
            {

                AppendChild_Sun(_TheData, _TableData.Rows[i], _LinkField, _NodeField, _ValueField);
            }

            return _TheData;

        }

        private static float AppendChild_Sun(DataContract.Common.DataObject_ECharts _TheData, DataRow _Row, ArrayList _LinkField, string _NodeField, string _ValueField)
        {
            bool cf = false;
            float _Value = 0;
            if (_Row[_LinkField[0].ToString()].ToString() == "")   // 某一层次，统计名称为空时，直接跳过本级的操作【如教务系统教学任务表中通识必修课的课程归属字段】
            {
                if (_LinkField.Count == 1)
                {
                    _TheData.TheDatasList[0].Add(new DataContract.Common.Data_ECharts() { NAME = _Row[_NodeField].ToString(), VALUE = _Row[_ValueField].ToString() });
                    _TheData.TheValue += float.Parse(_Row[_ValueField].ToString());
                    _Value += float.Parse(_Row[_ValueField].ToString());
                }
                else
                {
                    ArrayList _LowLinkField = _LinkField.GetRange(1, _LinkField.Count - 1);
                    _Value += AppendChild_Sun(_TheData, _Row, _LowLinkField, _NodeField, _ValueField);
                }

                return _Value;
            }
            else
            {
                for (int j = 0; j < _TheData.TheChildren.Count; j++)
                {
                    if (_TheData.TheChildren[j].TheName == _Row[_LinkField[0].ToString()].ToString())
                    {
                        cf = true;
                        break;
                    }
                }
                if (cf == false)
                {
                    DataContract.Common.DataObject_ECharts _N = new DataContract.Common.DataObject_ECharts() { TheName = _Row[_LinkField[0].ToString()].ToString(), TheChildren = new List<DataContract.Common.DataObject_ECharts>() };
                    _N.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();
                    _N.TheDatasList.Add(new List<DataContract.Common.Data_ECharts>());
                    _TheData.TheChildren.Add(_N);
                }

                for (int j = 0; j < _TheData.TheChildren.Count; j++)
                {
                    if (_TheData.TheChildren[j].TheName == _Row[_LinkField[0].ToString()].ToString())
                    {
                        if (_LinkField.Count == 1)
                        {
                            _TheData.TheChildren[j].TheDatasList[0].Add(new DataContract.Common.Data_ECharts() { NAME = _Row[_NodeField].ToString(), VALUE = _Row[_ValueField].ToString() });
                            _TheData.TheChildren[j].TheValue += float.Parse(_Row[_ValueField].ToString());
                            _Value += float.Parse(_Row[_ValueField].ToString());
                        }
                        else
                        {
                            ArrayList _LowLinkField = _LinkField.GetRange(1, _LinkField.Count - 1);

                            _Value += AppendChild_Sun(_TheData.TheChildren[j], _Row, _LowLinkField, _NodeField, _ValueField);

                        }
                        break;
                    }
                }
                _TheData.TheValue += _Value;
                return _Value;
            }
        }


        /// <summary>
        /// 为通用图型填充数据
        /// </summary>
        /// <param name="_Table">数据表【包含NAME（名称）、VALUE（值） 字段】</param>
        /// <param name="_TheData">需要填充的目标对象【同一对象可多次填充，用于同一坐标系内容的不同Series显示】，该参数为null时创建新的对象</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_Common(DataTable _Table, DataContract.Common.DataObject_ECharts _TheData)
        {
            if (_TheData == null)
            {
                _TheData = new DataContract.Common.DataObject_ECharts();
            }
            if (_TheData.TheDatasList == null)
            {
                _TheData.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();
            }
            List<DataContract.Common.Data_ECharts> _List = new List<DataContract.Common.Data_ECharts>();
            for (int i = 0; i < _Table.Rows.Count; i++)
            {
                _List.Add(new DataContract.Common.Data_ECharts() { NAME = _Table.Rows[i]["Name"].ToString(), VALUE = _Table.Rows[i]["Value"].ToString() });
            }
            _TheData.TheDatasList.Add(_List);

            return _TheData;
        }

        /// <summary>
        /// 为通用图型填充数据
        /// </summary>
        /// <param name="_Table">数据表【包含NAME（名称）、VALUE（值） 字段】</param>
        /// <returns>Echarts数据对象</returns>
        public static DataContract.Common.DataObject_ECharts Fill_Common(DataTable _Table)
        {
            DataContract.Common.DataObject_ECharts _TheData = new DataContract.Common.DataObject_ECharts();

            if (_TheData.TheDatasList == null)
            {
                _TheData.TheDatasList = new List<List<DataContract.Common.Data_ECharts>>();
            }
            List<DataContract.Common.Data_ECharts> _List = new List<DataContract.Common.Data_ECharts>();
            for (int i = 0; i < _Table.Rows.Count; i++)
            {
                _List.Add(new DataContract.Common.Data_ECharts() { NAME = _Table.Rows[i]["Name"].ToString(), VALUE = _Table.Rows[i]["Value"].ToString() });
            }
            _TheData.TheDatasList.Add(_List);

            return _TheData;
        }


    }
}
