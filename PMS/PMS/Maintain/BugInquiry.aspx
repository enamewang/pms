<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BugInquiry.aspx.cs" Inherits="PMS.PMS.Maintain.BugInquiry" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register Src="../../SysFrame/AspGridViewPager.ascx" TagName="AspGridViewPager"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style
        {
            width: 98px;
        }
        .styleTdMark
        {
            width: 50px;
        }
    </style>
   
     <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
      <link href="~/Style/myStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div width="800px">
        <p>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </p>
        <table width="800px">
            <tr>
                <td class="style2">
                    <asp:Label ID="LabelCrNo" runat="server" Text="CR No"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxCrNo" runat="server" Width="128px"></asp:TextBox>
                </td>
                <td class="styleTdMark">
                </td>
                <td>
                    <asp:Label ID="LabelCrSd" runat="server" Text="CR SD"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListCrSd" runat="server" Width="128px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="LabelCrQa" runat="server" Text="CR QA"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListCrQa" runat="server" Width="128px">
                    </asp:DropDownList>
                </td>
                <td class="styleTdMark">
                </td>
                <td>
                    <asp:Label ID="LabelTeam" runat="server" Text="Team"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListTeam" runat="server" Width="128px">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label ID="LabelCrCloseDataBetween" runat="server" Text="CR Close Data Between"></asp:Label>
                </td>
                <td>
                    <TW:DateTextBox ID="dateTextBoxCrCloseDataFrom" IsDisplayTime="false" runat="server"
                        Language="English" Width="128px"></TW:DateTextBox>
                </td>
                <td class="styleTdMark">
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="~"></asp:Label>
                </td>
                <td>
                    <TW:DateTextBox ID="dateTextBoxCrCloseDataTo" IsDisplayTime="false" runat="server"
                        Language="English" Width="128px"></TW:DateTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelBugOwner" runat="server" Text="Bug Owner"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListBugOwner" runat="server" Width="128px">
                    </asp:DropDownList>
                </td>
                <td class="styleTdMark">
                </td>
                <td>
                    <asp:Label ID="LabelBugCreator" runat="server" Text="Bug Creator"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListBugCreator" runat="server" Width="128px">
                    </asp:DropDownList>
                </td>
            </tr>
           
            <tr>
                <td>
                    <asp:Label ID="LabelBugCreateDateBetween" runat="server" Text="Bug Create Date Between"></asp:Label>
                </td>
                <td>
                    <TW:DateTextBox ID="dateTextBoxBugCreateDateFrom" IsDisplayTime="false" runat="server"
                        Language="English" Width="128px"></TW:DateTextBox>
                </td>
                <td class="styleTdMark">
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label2" runat="server" Text="~"></asp:Label>
                </td>
                <td>
                    <TW:DateTextBox ID="dateTextBoxBugCreateDateTo" IsDisplayTime="false" runat="server"
                        Language="English" Width="128px"></TW:DateTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelBugResolveDateBetween" runat="server" Text="Bug Resolve Date Between"></asp:Label>
                </td>
                <td>
                    <TW:DateTextBox ID="dateTextBoxBugResolveDateFrom" IsDisplayTime="false" runat="server"
                        Language="English" Width="128px"></TW:DateTextBox>
                </td>
                <td class="styleTdMark">
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Text="~"></asp:Label>
                </td>
                <td>
                    <TW:DateTextBox ID="dateTextBoxBugResolveDateTo" IsDisplayTime="false" runat="server"
                        Language="English" Width="128px"></TW:DateTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelBugCloseDateBetween" runat="server" Text="Bug Close Date Between"></asp:Label>
                </td>
                <td>
                    <TW:DateTextBox ID="dateTextBoxBugCloseDateFrom" IsDisplayTime="false" runat="server"
                        Language="English" Width="128px"></TW:DateTextBox>
                </td>
                <td class="styleTdMark">
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label4" runat="server" Text="~"></asp:Label>
                </td>
                <td>
                    <TW:DateTextBox ID="dateTextBoxBugCloseDateTo" IsDisplayTime="false" runat="server"
                        Language="English" Width="128px"></TW:DateTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:Button ID="ButtonInquiry" runat="server" Text="Inquiry" OnClick="ButtonInquiry_Click" />
                </td>
            </tr>
        </table>
        <p align="center">
        </p>
        <asp:UpdatePanel ID="UpdatePanelGridViewBug" runat="server">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <uc2:AspGridViewPager ID="AspGridViewPager1" runat="server" Init_Grid_ID="GridViewBug"
                                    OnBindGrid="BindGrid" SetPagerButtonImageStyle="Default" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewBug" runat="server" AutoGenerateColumns="false" CssClass="DIVGrid"
                                    AllowPaging="True" BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                                    ForeColor="Gray" GridLines="Horizontal" EmptyDataText="No data." HeaderStyle-BackColor="DimGray"
                                    OnRowDataBound="GridViewBug_RowDataBound">
                                    <PagerSettings Position="TopAndBottom" Visible="False" />
                                    <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                                    <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                                        BorderWidth="0px" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                                    <HeaderStyle BorderStyle="None" Font-Bold="True" HorizontalAlign="Left" CssClass="GridHead" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                                        BorderWidth="0px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="CR No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("CrId") %>' ID="lblCrId">
                                                </asp:Label>
                                                <asp:HiddenField ID="HiddenFieldPmsId" Value='<%# Eval("PmsId") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="CR Name" DataField="CrName">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="CR SD" DataField="Sd">
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Bug Owner" DataField="DutyBy">
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Bug ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("BugId") %>' ID="lblBugId">                                                   
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Bug Title" DataField="BugTitle">
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Bug Close Date" DataField="ClosedDate" HeaderStyle-Width="150px">
                                            <HeaderStyle Width="170px" />
                                            <ItemStyle Width="170px"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ButtonInquiry" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
