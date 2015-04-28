<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InquiryAndExport.aspx.cs"
    Inherits="PMS.PMS.Maintain.InquiryAndExport" EnableEventValidation="false" %>

<%@ Register Src="~/SysFrame/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>
<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inquiry</title>

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <base target="_self" />

    <script type="text/javascript">
        //获取页面大小和窗口大小
        function getPageSize() {
            var scrW, scrH;
            if (window.innerHeight && window.scrollMaxY) {
                // Mozilla
                scrW = window.innerWidth + window.scrollMaxX;
                scrH = window.innerHeight + window.scrollMaxY;
            } else if (document.body.scrollHeight > document.body.offsetHeight) {
                // all but IE Mac
                scrW = document.body.scrollWidth;
                scrH = document.body.scrollHeight;
            } else if (document.body) { // IE Mac
                scrW = document.body.offsetWidth;
                scrH = document.body.offsetHeight;
            }
            var winW, winH;
            if (window.innerHeight) { // all except IE
                winW = window.innerWidth;
                winH = window.innerHeight;
            } else if (document.documentElement
                && document.documentElement.clientHeight) {
                // IE 6 Strict Mode
                winW = document.documentElement.clientWidth;
                winH = document.documentElement.clientHeight;
            } else if (document.body) { // other
                winW = document.body.clientWidth;
                winH = document.body.clientHeight;
            }
            // for small pages with total size less than the viewport
            var pageW = (scrW < winW) ? winW : scrW;
            var pageH = (scrH < winH) ? winH : scrH;
            return { pageWidth: pageW, pageHeight: pageH, winWidth: winW, winHeight: winH };
        }

        Number.prototype.NaN0 = function() { return isNaN(this) ? 0 : this; }

    </script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="formInquiryAndExport" runat="server" defaultbutton="buttonInquiry">
    <asp:ScriptManager ID="ScriptManagerInquiry" runat="server" EnablePartialRendering="true"
        ScriptMode="Debug">
    </asp:ScriptManager>
    <div>
        <table class="MyTable" width="1100px">
            <tr>
                <td class="SpaceTdWidth" style="height: 22px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelCrNo" runat="server" Text="CR No" Width="120px"></asp:Label>
                    <asp:TextBox ID="textboxCrNo" runat="server" Width="144px" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="labelCrNoBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelDomain" runat="server" Text="Team Domain" Width="120px"></asp:Label>
                    <asp:DropDownList ID="dropdownlistDomain" runat="server" Width="148px">
                    </asp:DropDownList>
                    <asp:Label ID="labelDomainBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelSystem" runat="server" Text="System" Width="120px"></asp:Label>
                    <asp:TextBox ID="textboxSystem" runat="server" Width="144px" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="labelSystemBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelPmsName" runat="server" Text="CR Name" Width="120px"></asp:Label>
                    <asp:TextBox ID="textboxPmsName" runat="server" Width="144px" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="labelPmsNameBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelType" runat="server" Text="Type" Width="120px"></asp:Label>
                    <asp:DropDownList ID="dropdownlistType" runat="server" Width="148px">
                    </asp:DropDownList>
                    <asp:Label ID="labelTypeBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelSite" runat="server" Text="Site" Width="120px"></asp:Label>
                    <asp:DropDownList ID="dropdownlistSite" runat="server" Width="148px">
                    </asp:DropDownList>
                    <asp:Label ID="labelSiteBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelStage" runat="server" Text="Stage" Width="120px"></asp:Label>
                    <asp:DropDownList ID="dropdownlistStage" runat="server" Width="148px">
                    </asp:DropDownList>
                    <%-- <asp:DropDownCheckBoxes ID="dropdownlistStage" runat="server" Width="148px" UseButtons="False"
                        UseSelectAllNode="True">
                    </asp:DropDownCheckBoxes>--%>
                    <asp:Label ID="labelStageBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelPriority" runat="server" Text="Priority" Width="120px"></asp:Label>
                    <asp:DropDownList ID="dropdownlistPriority" runat="server" Width="148px">
                    </asp:DropDownList>
                    <asp:Label ID="labelPriorityBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelUserName" runat="server" Text="User" Width="120px"></asp:Label>
                    <asp:TextBox ID="textboxUserName" runat="server" Width="144px" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="labelUserNameBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelCreateDateFrom" runat="server" Text="Create Date From" Width="120px"></asp:Label>
                    <TW:DateTextBox ID="dateTextBoxCreateDateFrom" Width="142px" runat="server" IsDisplayTime="false"
                        Language="English"></TW:DateTextBox>
                    <asp:Label ID="labelCreateDateFromBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelCreateDateTo" runat="server" Text="Create Date To" Width="120px"></asp:Label>
                    <TW:DateTextBox ID="dateTextBoxCreateDateTo" Width="142px" runat="server" IsDisplayTime="false"
                        Language="English"></TW:DateTextBox>
                    <asp:Label ID="labelCreateDateToBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelReleaseDateFrom" runat="server" Text="Release Date From" Width="120px"></asp:Label>
                    <TW:DateTextBox ID="dateTextBoxReleaseDateFrom" Width="142px" runat="server" IsDisplayTime="false"
                        Language="English"></TW:DateTextBox>
                    <asp:Label ID="labelReleaseDateFromBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelReleaseDateTo" runat="server" Text="Release Date To" Width="120px"></asp:Label>
                    <TW:DateTextBox ID="dateTextBoxReleaseDateTo" Width="142px" runat="server" IsDisplayTime="false"
                        Language="English"></TW:DateTextBox>
                    <asp:Label ID="labelReleaseDateToBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelDueDateFrom" runat="server" Text="Due Date From" Width="120px"></asp:Label>
                    <TW:DateTextBox ID="dateTextBoxDueDateFrom" Width="142px" runat="server" IsDisplayTime="false"
                        Language="English"></TW:DateTextBox>
                    <asp:Label ID="labelDueDateFromBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Label ID="labelDueDateTo" runat="server" Text="Due Date To" Width="120px"></asp:Label>
                    <TW:DateTextBox ID="dateTextBoxDueDateTo" Width="142px" runat="server" IsDisplayTime="false"
                        Language="English"></TW:DateTextBox>
                    <asp:Label ID="labelDueDateToBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="height: 22px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth" align="center">
                    <asp:Button ID="buttonInquiry" runat="server" Text="Inquiry" Width="110px" OnClick="buttonInquiry_Click" />
                    <asp:Label ID="labelQuiryBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Button ID="buttonCreateService" runat="server" Text="Create Service" Width="110px"
                        OnClick="buttonCreateService_Click" />
                    <asp:Label ID="labelCreateServiceBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Button ID="buttonCreate" runat="server" Text="Create CR" Width="110px" OnClick="buttonCreate_Click" />
                    <asp:Label ID="labelCreateBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:Button ID="buttonExport" runat="server" Text="Export" Width="110px" OnClick="buttonExport_Click" />
                    <asp:Label ID="labelExportBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="height: 22px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth" align="center">
                    <table align="left" style="width: 1050px">
                        <tr>
                            <td style="width: 718px">
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="updatePanelPage" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <uc1:GridViewPager ID="GridViewPager1" Init_Grid_ID="gridViewMain" OnBindGrid="BindGrid"
                                            runat="server" SetPagerButtonImageStyle="Default" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="buttonInquiry" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td>
                    <asp:UpdatePanel ID="updatePanelGridView" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <anthem:GridView ID="gridViewMain" CssClass="DIVGrid" runat="server" AllowPaging="True"
                                PageSize="20" AllowSorting="true" AutoGenerateColumns="False" BorderColor="Gray"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Times New Roman"
                                Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal" Width="1050px" EmptyDataText="No data."
                                UpdateAfterCallBack="False" OnRowCreated="gridViewMain_RowCreated" DataKeyNames="PmsId"
                                OnRowDataBound="gridViewMain_RowDataBound" OnSorting="gridViewMain_OnSorting">
                                <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                                <PagerSettings Position="TopAndBottom" Visible="False" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Bugfree">
                                        <ItemTemplate>
                                            <div style="width: 55px; text-align: center;">
                                                <asp:ImageButton ID="imageButtonBugfree" OnClick="imageButtonDetail_Click" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="Check" ImageUrl="~/Style/Images/Bug_Creat.jpg"
                                                    ToolTip="Create Bug"></asp:ImageButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Detail">
                                        <ItemTemplate>
                                            <div style="width: 100px; text-align: center;">
                                                <asp:ImageButton ID="imageButtonDetail" OnClick="imageButtonDetail_Click" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" ImageUrl="~/SysFrame/Images/detail.gif"
                                                    ToolTip="Detail"></asp:ImageButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CR No" SortExpression="CrId">
                                        <ItemTemplate>
                                            <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CrId").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CrId").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                        <ItemStyle Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" SortExpression="Type">
                                        <ItemTemplate>
                                            <div style="width: 55px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="55px" />
                                        <ItemStyle Width="55px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CR Name" SortExpression="PmsName">
                                        <ItemTemplate>
                                            <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsName").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsName").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                        <ItemStyle Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%" SortExpression="Progress">
                                        <ItemTemplate>
                                            <div style="width: 55px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Progress").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Progress").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                        <ItemStyle Width="55px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate">
                                        <ItemTemplate>
                                            <div style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "DueDate","{0:yyyy-MM-dd}"))%>'>
                                                <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}")) == "1900-01-01")? "" : DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}"))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="78px" HorizontalAlign="Center" />
                                        <ItemStyle Width="85px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Release Date" SortExpression="ReleaseDate">
                                        <ItemTemplate>
                                            <div style="width: 90px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "ReleaseDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":DataBinder.Eval(Container.DataItem, "ReleaseDate","{0:yyyy-MM-dd}"))%>'>
                                                <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "ReleaseDate", "{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":(DataBinder.Eval(Container.DataItem, "ReleaseDate", "{0:yyyy-MM-dd}")).ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Create Date" SortExpression="CreateDate">
                                        <ItemTemplate>
                                            <div style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":(DataBinder.Eval(Container.DataItem, "CreateDate","{0:yyyy-MM-dd}")))%>'>
                                                <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : (DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="85px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stage" SortExpression="StageName">
                                        <ItemTemplate>
                                            <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StageName").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StageName").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PM" SortExpression="Pm">
                                        <ItemTemplate>
                                            <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Pm").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SD" SortExpression="Sd">
                                        <ItemTemplate>
                                            <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="65px" />
                                        <ItemStyle Width="75px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="System" SortExpression="System">
                                        <ItemTemplate>
                                            <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="65px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                                        <ItemTemplate>
                                            <div style="width: 60px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <span>PmsId</span>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsId").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsId").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <span>BugFreeProject</span>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BugFreeProject").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BugFreeProject").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <span>BugFreeModule</span>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                                title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BugFreeModule").ToString())%>'>
                                                <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "BugFreeModule").ToString())%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                                    BorderWidth="0px" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                                <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                    CssClass="bg_GridHeader" BackColor="#a6bce6" />
                                <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                                    BorderWidth="0px" />
                            </anthem:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="buttonInquiry" />
                            <asp:AsyncPostBackTrigger ControlID="buttonCreate" />
                            <asp:AsyncPostBackTrigger ControlID="buttonCreateService" />
                            <asp:AsyncPostBackTrigger ControlID="GridViewPager1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="height: 22px">
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
