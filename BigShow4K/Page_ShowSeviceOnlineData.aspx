<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_ShowSeviceOnlineData.aspx.cs" MasterPageFile="~/BasePage_HugeDataShow.Master" Inherits="BigShow4K.Page_ShowSeviceOnlineData" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Src="~/DataPanelBorder.ascx" TagPrefix="uc1" TagName="DataPanelBorder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div {
            /*border: 1px solid #fff;*/
        }

        .min-box-1 img {
            width: 8px;
            height: 8px;
            position: absolute;
            left: 0;
            top: 0;
        }

        .min-box-2 img {
            width: 8px;
            height: 8px;
            position: absolute;
            right: 0;
            top: 0;
        }

        .min-box-3 img {
            width: 8px;
            height: 8px;
            position: absolute;
            left: 0;
            bottom: 0;
        }

        .min-box-4 img {
            width: 8px;
            height: 8px;
            position: absolute;
            right: 0;
            bottom: 0;
        }

        .datapanel {
            border: 1px solid rgb(8, 43, 110);
            margin: 10px;
            position: relative;
            padding-top: 10px;
            background: url(images/row背景-深色.png);
        }

        iframe {
            border: none;
            height: 100%;
            width: 100%;
            margin-left: auto;
            margin-right: auto;
            background-color: transparent;
        }


        .a_th, .b_th {
            padding-left: 50px;
            width: 50%;
            height: 70px;
        }

        .c_th {
            font-size: 21px;
            color: aqua;
        }

        a:hover {
            text-decoration: none;
        }

        .infoimg {
            width: 39px;
            height: 36px;
        }

        .q_th {
            font-size: 17px;
            color: white;
            padding-top: 2px;
        }

        .innerbox::-webkit-scrollbar { /*滚动条整体样式*/
            width: 4px; /*高宽分别对应横竖滚动条的尺寸*/
            height: 4px;
            scrollbar-arrow-color: red;
        }

        .innerbox::-webkit-scrollbar-thumb { /*滚动条里面小方块*/
            border-radius: 5px;
            -webkit-box-shadow: inset 0 0 5px rgba(0,0,0,0.2);
            background: #0e1734;
            scrollbar-arrow-color: red;
        }

        .innerbox::-webkit-scrollbar-track { /*滚动条里面轨道*/
            -webkit-box-shadow: inset 0 0 5px rgba(0,0,0,0.2);
            border-radius: 0;
            background: #5db2db;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">


    <div style="display: flex; flex-direction: row; padding: 0px; height: 100%;">


        <div style="flex: 2 0 200px; display: flex; flex-direction: column;">
            <div style="flex: 2 0 200px;" class="datapanel">
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="BigShow_Click" CommandName="SystemTree">
                    <uc1:DataPanelBorder runat="server" ID="DataPanelBorder2" Title="在线服务事项运行总计" />
                </asp:LinkButton>

                <div style="width: 100%; height: 90%;">
                    <table style="width: 100%; font-size: 12px; color: white;" id="dvdatetable ">
                        <tr>
                            <td class=" a_th">
                                <asp:LinkButton ID="B_DDMY_ZH_GDP" runat="server" OnClick="DevDataLineShow_Click" CommandArgument="服务事项总数">  
                            <table>
                                <tr>
                                    <td rowspan="2"><img src="images/hong1.png" class="infoimg"  /></td>
                                    <td class="c_th"><%=FWSXZS%> <div style="color:#fff; float:right">（人次）</div></td>
                                </tr>
                                <tr>
                                    <td style="color:#fff;">服务事项总数</td>
                                </tr>
                            </table>
                                </asp:LinkButton>
                            </td>
                            <td class=" b_th">
                                <asp:LinkButton ID="B_DDMY_ZH_BSNZZ" runat="server" OnClick="DevDataLineShow_Click" CommandArgument="办结事项总数">  
                            <table>
                                <tr>
                                    
                                     <td rowspan="2"><img src="images/lv2.png"   class="infoimg"  /></td>
                                    <td class="c_th"><%=BJSXZS%><div style="color:#fff; float:right">（人次）</div></td>
                                </tr>
                                <tr>
                                         <td style="color:#fff;">办结事项总数</td>
                                </tr>
                            </table>
                                </asp:LinkButton>
                            </td>
                        </tr>


                        <tr>
                            <td class=" a_th">
                                <asp:LinkButton ID="B_DDMY_ZH_DYCYZJZ" runat="server" OnClick="DevDataLineShow_Click" CommandArgument="服务类别"> 
                            <table>
                                <tr>
                                    <td rowspan="2"><img src="images/hong1.png"  class="infoimg"   /></td>
                                    <td class="c_th"><%=FWLBS%><div style="color:#fff; float:right">（项）</div></td>
                                </tr>
                                <tr>           
                                       <td style="color:#fff;">服务类别</td>
                                </tr>
                            </table>
                                </asp:LinkButton>
                            </td>
                            <td class=" b_th">
                                <asp:LinkButton ID="B_DDMY_ZH_DECYZJZ" runat="server" OnClick="DevDataLineShow_Click" CommandArgument="办理环节操作数">
                            <table>
                                <tr>
                                    <td rowspan="2"><img src="images/lv2.png"   class="infoimg"  /></td>
                                   
                                    <td class="c_th"><%=BLHJCZS %><div style="color:#fff; float:right">（次）</div></td>
                                </tr>
                                <tr>          
                                         <td style="color:#fff;">办理环节操作数</td>
                                </tr>
                            </table>
                                </asp:LinkButton>
                            </td>


                        </tr>

                        <tr>
                            <td class=" a_th">
                                <asp:LinkButton ID="B_DDMY_ZH_DSCYZJZ" runat="server" OnClick="DevDataLineShow_Click" CommandArgument="今日服务人次">
                            <table>
                                <tr>
                                    <td rowspan="2"><img src="images/hong1.png"   class="infoimg"  /></td>
                                    <td class="c_th"><%=4444 %><div style="color:#fff; float:right">（人次）</div></td>
                                </tr>
                                <tr>
                                    <td style="color:#fff;">今日服务事项</td>
                                </tr>
                            </table>
                                </asp:LinkButton>
                            </td>
                            <td class=" b_th">
                                <asp:LinkButton ID="B_DDMY_GDZCTZ_GDZZTZ" runat="server" OnClick="DevDataLineShow_Click" CommandArgument="本周服务人次">
                            <table>
                                <tr>
                                    <td rowspan="2"><img src="images/lv2.png"  class="infoimg"    /></td>
                                    <td class="c_th"><%=5555 %><div style="color:#fff; float:right">（人次）</div></td>
                                </tr>
                                <tr>
                                    <td style="color:#fff;">本周服务事项</td>
                                </tr>
                            </table>
                                </asp:LinkButton>
                            </td>
                        </tr>

                        <tr>
                            <td class=" b_th">
                                <asp:LinkButton ID="B_DDMY_NY_NLZCZ" runat="server" OnClick="DevDataLineShow_Click" CommandArgument="本月服务人次">
                            <table>
                                <tr>
                                    <td rowspan="2"><img src="images/hong1.png"  class="infoimg"   /></td>
                                    <td class="c_th"><%=666%><div style="color:#fff; float:right">（人次）</div></td>
                                </tr>
                                <tr>          
                                    <td style="color:#fff;">本月服务事项</td>
                                </tr>
                            </table>
                                </asp:LinkButton>
                            </td>
                            <td class=" a_th">
                                <asp:LinkButton ID="B_DDMY_NY_BSNZZ" runat="server" OnClick="DevDataLineShow_Click" CommandArgument="本年服务人次">
                            <table>
                                <tr>
                                    <td rowspan="2"><img src="images/lv2.png"   class="infoimg"  /></td>
                                    <td class="c_th"><%=77%><div style="color:#fff; float:right">（人次）</div></td>
                                </tr>
                                <tr>
                                    <td style="color:#fff;">本年服务事项</td>
                                </tr>
                            </table>
                                </asp:LinkButton>
                            </td>
                        </tr>

                    </table>

                </div>
            </div>
            <%-- <div style="flex: 1 0 200px;" class="datapanel">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="BigShow_Click" CommandName="SystemTree">
                    <uc1:DataPanelBorder runat="server" ID="DataPanelBorder1" Title="红河学院信息系统列表" />
                </asp:LinkButton>

                <div style="width: 100%; height: 90%;">

                    <iframe src="/CommonPage/EChart/Page_EChart3_Common_Pie.aspx?SN=ShowTypePie&&Title=服务事项类别占比"></iframe>
                </div>
            </div>--%>
            <div style="flex: 3 0 300px; padding: 10px;" class="datapanel">
                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="BigShow_Click" CommandName="LastPList">
                    <uc1:DataPanelBorder runat="server" ID="DataPanelBorder5" Title="最新事项列表" />
                </asp:LinkButton>
                <table style="color: #fff; font-size: 9pt; width: 100%;">
                    <tr style="color: #5db2db;">
                        <td>服务事项类别</td>
                        <td>发起人</td>
                        <td>发起时间</td>


                    </tr>

                    <asp:Repeater ID="GridView_Main" runat="server" OnItemDataBound="GridView_Main_ItemDataBound">
                        <ItemTemplate>
                            <tr style="height: 30px;">
                                <td style="min-width: 150px; max-width: 280px;">
                                     <span class=" label label-primary"  style="<%# GetColor((string)Eval("PROCESSNAME")) %>">
                                          <%# Eval("PROCESSNAME")%>  
                                     </span>
                                </td>
                                <td style="min-width: 80px;"> <%# Eval("CREATOR")%>
                                </td>
                                <td style="min-width: 80px;"><%# Eval("CREATETIME")%></td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>



        <div class="datapanel" style="flex: 4 0 400px; display: flex; flex-direction: column;">



            <div style="flex: 4 0 400px;">

                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="BigShow_Click" CommandName="TypeSun">

                    <uc1:DataPanelBorder runat="server" ID="DataPanelBorder4" Title="服务事项分类图" />
                </asp:LinkButton>
                <div style="width: 100%; height: 100%;">
                    <iframe src="CommonPage/EChart/Page_EChart3_Common_SunDrink.aspx?SN=TypeSun&&Title=服务事项分类图"></iframe>

                </div>


            </div>

            <%--右下角长框--%>
            <div style="flex: 2 0 200px;">
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="BigShow_Click" CommandName="TimeLine">

                    <uc1:DataPanelBorder runat="server" ID="DataPanelBorder3" Title="服务事项时间分布图" />
                </asp:LinkButton>
                <div style="width: 100%; height: 90%;">
                    <iframe src="CommonPage/EChart/Page_EChart3_Common_Line.aspx?SN=TimeLine&&Title=服务事项时间分布图"></iframe>

                </div>
            </div>
        </div>


    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderModal" runat="server">
    <div class="modal fade" id="BigShowModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" style="width: 100%; height: 100%; text-align: center;">
        <div class="modal-dialog" style="width: 70%; height: 95%; margin-left: auto; margin-right: auto;">
            <div class="modal-content" style="height: 100%; background-color: black;">



                <div class="modal-body" style="height: 90%;">
                    <iframe src=" " style="border: none; height: 100%; width: 100%; margin-left: auto; margin-right: auto; background-color: transparent;" id="if_BigShow" runat="server"></iframe>


                    <div class="modal-footer">

                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                            关闭   
                        </button>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
