<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UCT_LastPList.aspx.cs" Inherits="BigShow4K.UCT_LastPList1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>

    <script type="text/javascript">
        function sx() {
            window.location.reload();
        }
        setInterval(sx, 60000);
    </script>
    <form id="form1" runat="server">


        <table style="color: #fff; font-size: 9pt; width: 100%;">
            <tr style="color: #5db2db; font-size: 26pt;">
                <td style="padding: 0 100px 0 40px">服务事项类别</td>
                <td>发起人</td>
                <td style="float: right; margin-right: 130px;">发起时间</td>


            </tr>

            <asp:Repeater ID="GridView_Main" runat="server" OnItemDataBound="GridView_Main_ItemDataBound">
                <ItemTemplate>
                    <tr style="height: 70px; font-size: 26pt;">
                        <td style="min-width: 150px; max-width: 280px; padding: 0 0 0 35px">
                            <span class=" label label-primary" style="<%# GetColor((string)Eval("PROCESSNAME")) %>">
                                <%# Eval("PROCESSNAME")%>  
                            </span>
                        </td>
                        <td style="min-width: 80px;"><%# Eval("CREATOR")%>
                        </td>
                        <td style="min-width: 80px; float: right; margin-right: 55px;"><%# Eval("CREATETIME")%></td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>


    </form>
</body>
</html>
