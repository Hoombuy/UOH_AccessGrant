using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BigShow
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowGraph();
        }

        private void ShowMap()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);
            DataTable TableCJ = GD_ZF.GetDataTable("    select b.mc as Name , count(*) as Value  from UOH_ES.REPORTINFO t inner  join uoh_es.dm_dq b on t.ri_szd3 = b.dm  where ri_date like '2021-01-03'  group by b.mc ");
            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForMap(TableCJ);
        }

        private void ShowCalendarHeatmap()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);
            DataTable TableCJ = GD_ZF.GetDataTable(" select ri_date as Name ,count(*) as Value  from UOH_ES.REPORTINFO t where ri_date like '2020%'  group by ri_date order by ri_date ");
            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForCalendarHeatmap("2020", TableCJ);
        }

        private void ShowRader()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);
            DataTable TableIn = GD_ZF.GetDataTable("select  kcxz NAME, max(cj) Value from zfxfzb.cjb where xh in (select xh from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj ='2017') and(cj is not null and cj <> 0) group by   kcxz  ");

            DataTable TableCJ = GD_ZF.GetDataTable(" select xm||' '||xh as Name, " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识必修课') 通识必修课, " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识选修课') 通识选修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '平台必修课' ) 平台必修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通必专项课' ) 通必专项课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业必修课' ) 专业必修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业选修课' ) 专业选修课  " +
                                                                               "   from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj = '2017'  and (xm like '周子%' or xm like '谢丹丹')   ");


            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForRader(TableIn, TableCJ);

        }

        private void ShowLine_Group()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable TableCJ = GD_ZF.GetDataTable("  select kcxz as Name, xm||' '||xh as ItemID ,  cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) Value  from zfxfzb.cjb  where xh in (select xh   from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj = '2017' and ( xm like '陈雪' or xm like '罗素芬'))   group by kcxz, xm, xh   order by xh, kcxz  ");
            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForLine_ObjectItem(TableCJ);

        }

        private void ShowLine_Row()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable TableCJ = GD_ZF.GetDataTable(" select xm||' '||xh as Name, " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识必修课') 通识必修课, " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识选修课') 通识选修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '平台必修课' ) 平台必修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通必专项课' ) 通必专项课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业必修课' ) 专业必修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业选修课' ) 专业选修课  " +
                                                                               "   from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj = '2017'    ");


            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForLine_ObjectRow(TableCJ);

        }

        private void ShowBar()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable TableCJ = GD_ZF.GetDataTable(" select xm||' '||xh as Name, " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识必修课') 通识必修课, " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj  from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通识选修课') 通识选修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '平台必修课' ) 平台必修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '通必专项课' ) 通必专项课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业必修课' ) 专业必修课， " +
                                                                            " (select cast(trunc(sum(xf * cj) / sum(xf),2) as varchar(6)) pjcj from zfxfzb.cjb c where c.xh = x.xh and c.kcxz = '专业选修课' ) 专业选修课  " +
                                                                               "   from zfxfzb.xsjbxxb x where x.zymc like '数字媒体%' and dqszj = '2017'    ");


            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForBar(TableCJ);

        }

        private void ShowGraph()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            //DataTable TableCJ = GD_ZF.GetDataTable(" select xh as ITEMID, kcmc as Name, a.xkkh from xsxkbo a,   (select  xkkh, kcmc from zfxfzb.jxrwbview b1 where xn = '2020-2021' and xq = '1' and kcxz = '通识选修课' and  ( kcgs ='科技进步与科学精神' or  kcgs ='语言与基本技能选修' or kcgs='生态环境与生命关怀'  or kcgs='文史经典与文化传承' or kcgs='学习与素质提高') group by kcmc, xkkh)  b where a.xkkh = b.xkkh and a.xh like '2019%'   ");

            //DataTable TableKC = GD_ZF.GetDataTable("   select kcmc as Name, sum(rs)  as Value, kcgs as LevelNo from (select kcmc, xkkh, nvl(kcgs,kcxz) as kcgs,  (select count(*) rs from zfxfzb.xsxkbo b where a.xkkh = b.xkkh and b.xh like '2019%'  ) rs from zfxfzb.jxrwbview a where xn = '2020-2021' and xq = '1' and kcxz = '通识选修课' and ( kcgs ='科技进步与科学精神' or  kcgs ='语言与基本技能选修' or kcgs='生态环境与生命关怀' or kcgs='文史经典与文化传承' or kcgs ='学习与素质提高' ) group by kcmc, xkkh, kcgs, kcxz) group by kcmc, kcgs order by kcgs ");

            DataTable TableCJ = GD_ZF.GetDataTable("   select xh as ITEMID, kcmc as Name, a.xkkh from xsxkbn a,   (select xkkh, kcmc from zfxfzb.jxrwbview b1 where xn = '2020-2021' and xq = '2' and kcxz like '平台必修课'   group by kcmc, xkkh)  b where a.xkkh = b.xkkh and a.xh like '2020%' ");

            DataTable TableKC = GD_ZF.GetDataTable("   select kcmc as Name, sum(rs)  as Value, kkxy as LevelNo from (select kcmc, xkkh, kkxy,  (select count(*) rs from zfxfzb.xsxkbn b where a.xkkh = b.xkkh and b.xh like '2020%'  ) rs from zfxfzb.jxrwbview a where xn = '2020-2021' and xq = '2' and kcxz like '平台必修课'     group by kcmc, xkkh, kkxy, kcxz)  where rs <> 0 group by kcmc, kkxy order by kkxy ");

         
            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_ForGraph(TableKC, TableCJ);

        }

        private void ShowSunDrink()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable Table_ZFJXRW = GD_ZF.GetDataTable(" select kcdm, kcmc, xf, zxs, kkxy, kcxz, nvl(kcgs,kcxz) kcgs, 1 as bs from zfxfzb.jxrwbview where xn ='2020-2021' and xq ='1' and xkzt ='1' and kcxz like '通识%'  group by kcdm, kcmc, xf, zxs, kkxy, kcxz, kcgs   ");
            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_TreeMap(Table_ZFJXRW, new ArrayList() { "KCXZ", "KCGS" }, "KCMC", "BS");
        }

        private void ShowSun()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable Table_ZFJXRW = GD_ZF.GetDataTable(" select kcdm, kcmc, xf, zxs, kkxy, kcxz, nvl(kcgs,'') kcgs, count( distinct xkkh) as bs from zfxfzb.jxrwbview where xn ='2020-2021' and xq ='1' and xkzt ='1' and kcxz like '通识%'  group by kcdm, kcmc, xf, zxs, kkxy, kcxz, kcgs   ");
            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_Sun(Table_ZFJXRW, new ArrayList() { "KCXZ", "KCGS" }, "KCMC", "BS");
        }

        private void ShowTreeMap()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable Table_ZFJXRW = GD_ZF.GetDataTable(" select kcdm, kcmc, xf, zxs, kkxy, kcxz, nvl(kcgs,kcxz) kcgs, count( distinct xkkh) as bs from zfxfzb.jxrwbview where xn ='2020-2021' and xq ='1' and xkzt ='1' and kcxz like '通识%'  group by kcdm, kcmc, xf, zxs, kkxy, kcxz, kcgs   ");
            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_TreeMap(Table_ZFJXRW, new ArrayList() { "KCGS" }, "KCMC", "BS");
        }

        private void ShowSankey()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable TableCJ = GD_ZF.GetDataTable(" select xh  as ITEMID, kcmc as NAME , max(floor(jd)) as VALUE from zfxfzb.cjb where xh in (select xh from zfxfzb.xsjbxxb where dqszj ='2017' and zymc like '计算机科学与技术') and kcxz like '专业必修%'  and kcmc in ( select kcmc from (select xh, kcmc ,max(jd) jd from zfxfzb.cjb where xh in (select xh from zfxfzb.xsjbxxb where dqszj ='2017' and zymc like '计算机科学与技术') and kcxz like '专业必修课' and ( kcmc like '%离散%'  or kcmc like '%数据结构%'  or kcmc like '%计算机网络%'  or kcmc like '%数据库%'  ) group by xh, kcmc) group by kcmc, jd having count(*) >1 ) group by xh, kcmc order by kcmc  ");

            DataTable TableKC = GD_ZF.GetDataTable("   select kcmc as NAME , jd as VALUE from (select xh, kcmc ,max(floor(jd)) jd from zfxfzb.cjb where xh in (select xh from zfxfzb.xsjbxxb where dqszj ='2017' and zymc like '计算机科学与技术') and kcxz like '专业必修课' and ( kcmc like '%离散%'  or kcmc like '%数据结构%'  or kcmc like '%计算机网络%'  or kcmc like '%数据库%'  )  group by xh, kcmc) group by kcmc, jd having count(*) >1 order by kcmc, jd    ");



            Session["S_JKZYKCJ"] = BusinessControls.Chart.Control_Chart.Fill_ForSanKey(TableKC, TableCJ);

        }

        private void ShowTree()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable Table_ZFJXRW = GD_ZF.GetDataTable(" select kcdm, kcmc, xf, zxs, kkxy, kcxz, nvl(kcgs,'') kcgs from zfxfzb.jxrwbview where xn ='2020-2021' and xq ='1' and xkzt ='1' and kcxz like '通识%'  group by kcdm, kcmc, xf, zxs, kkxy, kcxz, kcgs   ");
            Session["S_KKZTQK"] = BusinessControls.Chart.Control_Chart.Fill_Tree(Table_ZFJXRW, new ArrayList() { "KCXZ", "KCGS" }, "KCMC", "2020-2021秋季通识课");
        }


    }
}