<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeHistory.aspx.cs"
    Inherits="PMS.PMS.Maintain.ChangeHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change History</title>

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/myStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divGridView" runat="server">
        <anthem:GridView ID="GridViewChangeHistory" CssClass="DIVGrid" runat="server"
            ShowHeader="false" AutoGenerateColumns="False" BackColor="White" BorderColor="Gray"
            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Times New Roman"
            Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal" Width="100%" EmptyDataText="No data."
            DataKeyNames="PmsId,Seq" AllowPaging="false">
            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
            <PagerSettings Position="TopAndBottom" Visible="False" />
            <FooterStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div style="width: 100%; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                            title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Description").ToString())%>'>
                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Description").ToString())%>
                        </div>
                    </ItemTemplate>
                    <ItemStyle Width="100%" />
                </asp:TemplateField>
            </Columns>
            <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                BorderWidth="0px" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
            <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                CssClass="bg_GridHeader" />
            <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                BorderWidth="0px" />
        </anthem:GridView>
    </div>
    </form>
</body>
</html>
