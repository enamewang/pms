<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentsMaintain.aspx.cs"
    Inherits="PMS.PMS.Maintain.Documents_Maintain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documents Maintain</title>

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/myStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formInquiryAndExport" runat="server">
    <asp:ScriptManager ID="ScriptManagerInquiry" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="height: 25px; width: 100%;" bgcolor="aliceblue">
                    <asp:Button ID="buttonAttachFile" runat="server" Text="Attach File" Width="100px"
                        OnClick="buttonAttachFile_Click" />
                    <asp:Label ID="Label1" runat="server" Width="20px"></asp:Label>
                    <asp:Button ID="buttonGetFile" runat="server" Text="Get File" Width="100px" OnClick="buttonGetFile_Click" />
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td>
                </td>
                <td>
                    <div id="divGridView" runat="server">
                        <anthem:GridView ID="gridViewService" CssClass="DIVGrid" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="Gray" BorderStyle="Solid"
                            BorderWidth="1px" CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt"
                            ForeColor="Gray" GridLines="Horizontal" Width="100%" EmptyDataText="No data."
                            DataKeyNames="DocTypeId,FileName,Path,Creator" OnRowDeleting="gridView_RowDeleting">
                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                            <PagerSettings Position="TopAndBottom" Visible="False" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span>Document Type</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="labelTypeName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TypeName").ToString())%>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="110px" />
                                    <ItemStyle Width="110px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span>File Name</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="linkButtonFileName" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName").ToString())%>'
                                            ForeColor="Blue" CommandArgument='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Path").ToString())%>'
                                            OnCommand="linkButton_OnCommand">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="350px" />
                                    <ItemStyle Width="350px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span>Owner</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="labelCreator" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Creator").ToString())%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span>Modified Date</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="labelCreateDate" runat="server" Text='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")))%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="95px" />
                                    <ItemStyle Width="95px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imageButtonDelete" runat="server" OnClientClick="javascript:if (!confirmDelete()) return;"
                                            CommandName="Delete" ImageUrl="~/SysFrame/images/ButtonDelete.gif"></asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="55px" />
                                    <ItemStyle Width="100px" />
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
                        <anthem:GridView ID="gridViewMain" CssClass="DIVGrid" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="Gray" BorderStyle="Solid"
                            BorderWidth="1px" CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt"
                            ForeColor="Gray" GridLines="Horizontal" Width="100%" EmptyDataText="No data."
                            DataKeyNames="DocTypeId,FileName,Creator" OnRowDeleting="gridView_RowDeleting">
                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                            <PagerSettings Position="TopAndBottom" Visible="False" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span>Document Type</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="labelTypeName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TypeName").ToString())%>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="110px" />
                                    <ItemStyle Width="110px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span>File Name</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a id="hrefFileName" runat="server" style="text-decoration: underline; color: #0000FF;"
                                            href='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Path").ToString())%>'
                                            target="_blank">
                                            <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FileName").ToString())%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span>Owner</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="labelCreator" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Creator").ToString())%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span>Modified Date</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="labelCreateDate" runat="server" Text='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")))%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="95px" />
                                    <ItemStyle Width="95px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imageButtonDelete" runat="server" OnClientClick="javascript:if (!confirmDelete()) return;"
                                            CommandName="Delete" ImageUrl="~/SysFrame/images/ButtonDelete.gif"></asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="55px" />
                                    <ItemStyle Width="55px" />
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
                    <div id="divGridViewService" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
