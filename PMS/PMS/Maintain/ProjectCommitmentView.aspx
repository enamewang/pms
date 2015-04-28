<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectCommitmentView.aspx.cs"
    Inherits="PMS.PMS.Maintain.ProjectCommitmentView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Project Commitment View</title>

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <base target="_self" />

    <script type="text/javascript">
       
    </script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <div style="margin: 10px; width: 1000px;">
        <anthem:GridView ID="GridViewRequirement" CssClass="DIVGrid" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
            Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal"
            Width="1050px" EmptyDataText="No data." DataKeyNames="Serial">
            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
            <PagerSettings Position="TopAndBottom" Visible="False" />
            <FooterStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span>CR No</span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                            title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CRId").ToString())%>'>
                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CRId").ToString())%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Width="150px" />
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span>Type</span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 80px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                            title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>'>
                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span>CR Name</span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 260px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                            title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CRName").ToString())%>'>
                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CRName").ToString())%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Width="260px" />
                    <ItemStyle Width="260px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span>System</span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                            title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>'>
                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span>PM</span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                            title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Pm").ToString())%>'>
                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Pm").ToString())%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span>SD</span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                            title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>'>
                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span>Status</span>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 80px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                            title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Status").ToString())%>'>
                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Status").ToString())%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" />
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
