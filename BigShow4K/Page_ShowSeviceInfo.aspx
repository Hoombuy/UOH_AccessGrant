<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_ShowSeviceInfo.aspx.cs" MasterPageFile="~/BasePage_HugeDataShow.Master" Inherits="BigShow4K.Page_ShowSeviceInfo" %>

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


        <div style="flex: 1 0 200px; display: flex; flex-direction: column;">
          

            </div>
 
       

        <div class="datapanel" style="flex: 4 0 400px; display: flex; flex-direction: column;">



         
        </div>


    </div>
</asp:Content>

 