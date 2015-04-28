<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequirementDataMaintain.aspx.cs"
    Inherits="PMS.PMS.Maintain.RequirementDataMaintain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Requirement Data Maintain</title>

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
        <div>
            <asp:Label ID="LabelYearMonth" runat="server" Text="Year / Month :" Width="130px" />
            <asp:DropDownList ID="DropDownListYearMonth" runat="server" CssClass="DropDownList"
                OnSelectedIndexChanged="DropDownListYearMonth_SelectedIndexChanged" AutoPostBack="true" />
            <asp:Label ID="LabelBlank1" runat="server" Text="" Width="38px" />
            <asp:Label ID="LabelWeekPeriod" runat="server" Text="Week Period :" Width="130px" />
            <asp:DropDownList ID="DropDownListWeekPeriod" runat="server" CssClass="DropDownList"
                OnSelectedIndexChanged="DropDownListWeekPeriod_SelectedIndexChanged" AutoPostBack="true" />
            <br />
            <br />
            <asp:Label ID="LabelExcelFile" runat="server" Text="Excel File :" Width="130px" />
            <asp:FileUpload ID="FileUpload" runat="server" Width="415px" />
            <asp:Label ID="labelUploadBank" runat="server" Text="  " Width="30px"></asp:Label>
            <asp:Button ID="ButtonUpload" runat="server" Text="Import" OnClick="ButtonUpload_Click" />
            <br />
            <br />
            <asp:Label ID="labelInquiryBank" runat="server" Text="  " Width="300px"></asp:Label>
            <asp:Button ID="ButtonInquiry" runat="server" Text="Inquiry" OnClick="ButtonInquiry_Click" />
        </div>
        <br />
        <br />
        <div id="divGridView" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <anthem:GridView ID="GridViewRequirement" CssClass="DIVGrid" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                        Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal"
                        Width="1050px" EmptyDataText="No data." DataKeyNames="Serial" OnRowDeleting="GridView_RowDeleting"
                        OnRowCommand="GridViewRequirement_RowCommand" OnRowDataBound="GridViewRequirement_RowDataBound"
                        OnRowCancelingEdit="GridViewRequirement_RowCancelingEdit" OnRowEditing="GridViewRequirement_RowEditing"
                        OnRowUpdating="GridViewRequirement_RowUpdating">
                        <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                        <PagerSettings Position="TopAndBottom" Visible="False" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>User Dept</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labelUserDept" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserDept").ToString())%>'> 
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>CR No</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labelCRId" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CRId").ToString())%>'> 
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Width="95px" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CRId").ToString())%>'
                                        ID="textBoxCRId" MaxLength="15">
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Type</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labelType" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>CR Name</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labelCRName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CRName").ToString())%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>System</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labelSystem" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>SD</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labelSd" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span>Status</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="labelStatus" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Status").ToString())%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownListStatus" runat="server" Style="width: 95%">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif" ID="QimagebuttonEdit"
                                        CommandName="Edit" ToolTip="Edit" CommandArgument="<%# Container.DataItemIndex %>">
                                    </asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/update.gif" ID="QimagebuttonUpdate"
                                        CommandName="Update" ToolTip="Update" CommandArgument="<%# Container.DataItemIndex %>">
                                    </asp:ImageButton>
                                    <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/undo.gif" ID="QimagebuttonCancel"
                                        CommandName="Cancel" ToolTip="Cancel" CommandArgument="<%# Container.DataItemIndex %>">
                                    </asp:ImageButton>
                                </EditItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imageButtonDelete" runat="server" OnClientClick="javascript:if (!confirmDelete()) return;"
                                        CommandName="Delete" ImageUrl="~/SysFrame/images/ButtonDelete.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
