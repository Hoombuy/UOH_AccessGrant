<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_ShowDataAssets.aspx.cs" MasterPageFile="~/BasePage_HugeDataShow.Master" Inherits="BigShow4K.Page_ShowDataAssets" %>

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

        .message {
            background-image: url(images/row背景.png);
            background-size: cover;
            margin: 2px;
            padding: 2px 5px;
        }

        .compare {
            vertical-align: bottom;
            color: rgb(0,240,255);
            font-size: 0.5em;
            float: right;
            position: relative;
            bottom: -5px;
        }

            .compare img {
                width: 9px;
                height: 6px;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">


    <div style="display: flex; flex-direction: row; padding: 0px; height: 100%;">


        <div style="flex: 2 0 200px;" class="datapanel">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="BigShow_Click" CommandName="SystemTree">
                <uc1:DataPanelBorder runat="server" ID="DataPanelBorder1" Title="红河学院信息系统列表" />
            </asp:LinkButton>

            <iframe src="/CommonPage/EChart/Page_EChart3_Common_Tree.aspx?SN=SystemTree&&Title=红河学院信息系统列表"></iframe>

        </div>
   <%--     <div style="flex: 3 0 200px; display: flex; flex-direction: column;">
   --%>       
          <div class="datapanel" style="flex: 2 0 200px;">
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="BigShow_Click" CommandName="DataAssets">

                    <uc1:DataPanelBorder runat="server" ID="DataPanelBorder2" Title="数据资产统计" />
                </asp:LinkButton>
                <div style="width: 100%; height: 90%;">
                    <iframe src="/CommonPage/EChart/Page_EChart3_Common_PctorialBar.aspx?SN=DataAssets&&Title=数据资产"></iframe>
                </div>
            </div>

            <div class="datapanel" style="flex: 2 0 200px;">
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="BigShow_Click" CommandName="DataTranRule">

                    <uc1:DataPanelBorder runat="server" ID="DataPanelBorder3" Title="数据交换规则" />
                </asp:LinkButton>
                <div style="width: 100%; height: 90%;">
                    <iframe src="/CommonPage/EChart/Page_EChart3_Common_Graph.aspx?SN=DataTranRule&&Title=数据交换规则"></iframe>

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

<%--           <div style="width: 100%; height: 80%; display: flex; flex-direction: row;">
                    <div style="flex: 1 1 120px;">

                        <div class="message">
                                <span style="color: rgb(1,169,232); font-size: 0.8em;">团组织</span><br/>
                                <span style="color: rgb(0,255,0); font-size: 1.3em;"><%=_X %></span>
                                <span class="compare">比上月0.6%<img src="images/绿三角.png"/></span>
                        </div>

                        <iframe src="/CommonPage/EChart/Page_EChart3_Common_Pie.aspx?SN=O_HYLB&Title="></iframe>
                            
                    </div>
                    <div style="flex: 1 1 120px;">

                        <div class="message">
                            <span style="color: rgb(1,169,232); font-size: 0.8em;">团员</span><br/>
                            <span style="color: rgb(255,180,0); font-size: 1.3em;"><%=_Y %></span>
                            <span class="compare">比上月0.6%<img src="images/红三角.png"/></span>
                        </div>

                        <iframe src="/CommonPage/EChart/Page_EChart3_Common_Pie.aspx?SN=ER_Carrier"></iframe>

                    </div>
                    <div style="flex: 1 1 120px;">

                        <div class="message">
                            <span style="color: rgb(1,169,232); font-size: 0.8em;">团组织---</span><br/>
                            <span style="color: rgb(248,114,30); font-size: 1.3em;"><%=_Z %></span>
                            <span class="compare">比上月0.6%<img src="images/绿三角.png"/></span>
                        </div>

                        <iframe src="/CommonPage/EChart/Page_EChart3_Common_Pie.aspx?SN=O_WHCD&Rose=true"></iframe>

                    </div>
                </div>--%>
