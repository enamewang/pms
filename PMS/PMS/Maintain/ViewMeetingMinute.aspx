<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMeetingMinute.aspx.cs"
    Inherits="PMS.PMS.Maintain.ViewMeetingMinute" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>View Meeting Minute</title>
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 10px; width: 1000px;">
        <div id="DivCreateMeetingMinute" style="width: 100%">
            <div>
                <asp:Label ID="LabelMeetingType" runat="server" Text="Meeting Type :" Width="130px" />
                <asp:Label ID="LabelMeetingTypeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                <asp:DropDownList ID="DropDownListMeetingMinuteType" runat="server" CssClass="DropDownList" />
                <asp:Label ID="LabelBlank1" runat="server" Text="" Width="38px" />
                <asp:Label ID="LabelHost" runat="server" Text="Host :" Width="130px" />
                <asp:Label ID="LabelHostMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                <asp:TextBox ID="TextBoxHost" runat="server" CssClass="TextBoxNormalBottomBorderOnly"
                    MaxLength="30" />
                <br />
                <br />
                <asp:Label ID="LabelVenue" runat="server" Text="Venue :" Width="130px" />
                <asp:Label ID="LabelVenueMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                <asp:DropDownList ID="DropDownListVenue" runat="server" CssClass="DropDownList" />
                <asp:Label ID="LabelBlank2" runat="server" Text="" Width="38px" />
                <asp:Label ID="LabelRecorder" runat="server" Text="Recorder :" Width="130px" />
                <asp:Label ID="LabelRecorderMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                <asp:TextBox ID="TextBoxRecorder" runat="server" CssClass="TextBoxNormalBottomBorderOnly"
                    MaxLength="30" />
                <br />
                <br />
                <asp:Label ID="LabelMeetingStartTime" runat="server" Text="Meeting Start Time :"
                    Width="130px" />
                <asp:Label ID="LabelMeetingStartTimeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                <asp:TextBox ID="TextBoxMeetingStartDate" runat="server" CssClass="TextBoxShortBottomBorderOnly" />
                <asp:DropDownList ID="DropDownListStartHour" runat="server" Height="21px" CssClass="DropDownListVeryShort" />
                <asp:DropDownList ID="DropDownListStartMinute" runat="server" Height="21px" CssClass="DropDownListVeryShort" />
                <asp:Label ID="LabelBlank3" runat="server" Text="" Width="38px" />
                <asp:Label ID="LabelMeetingEndTime" runat="server" Text="Meeting End Time :" Width="130px" />
                <asp:Label ID="LabelMeetingEndTimeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                <asp:TextBox ID="TextBoxMeetingEndDate" runat="server" CssClass="TextBoxShortBottomBorderOnly" />
                <asp:DropDownList ID="DropDownListEndHour" runat="server" Height="21px" CssClass="DropDownListVeryShort" />
                <asp:DropDownList ID="DropDownListEndMinute" runat="server" Height="21px" CssClass="DropDownListVeryShort" />
                <br />
                <br />
                <asp:Label ID="LabelSubject" runat="server" Text="Subject :" Width="130px" />
                <asp:Label ID="LabelSubjectMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                <asp:TextBox ID="TextBoxSubject" runat="server" CssClass="TextBoxLongBottomBorderOnly"
                    MaxLength="500" />
                <br />
                <br />
                <asp:Label ID="LabelAttendee" runat="server" Text="Attendee :" Width="130px" />
                <asp:Label ID="LabelAttendeeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                <asp:TextBox ID="TextBoxAttendee" runat="server" CssClass="TextBoxLongBottomBorderOnly"
                    MaxLength="100" />
                <br />
                <br />
                <asp:Label ID="LabelConclusion" runat="server" Text="Conclusions :" Width="130px" />
                <br />
            </div>
            <div id="DivConclusion" style="width: 100%">
                <asp:GridView ID="GridViewConclusion" runat="server" Width="664px" CssClass="DIVGrid"
                    AutoGenerateColumns="False" AllowPaging="False" BackColor="White" BorderColor="Gray"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ShowFooter="True" Font-Names="Times New Roman"
                    Font-Size="9pt" DataKeyNames="Serial" ForeColor="Gray" GridLines="Horizontal"
                    EmptyDataText="No data." HeaderStyle-BackColor="#a6bce6">
                    <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                            <FooterStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="LabelDesc" runat="server" Width="100%" Text='<%#  DataBinder.Eval(Container.DataItem, "Description" )%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="450px" />
                            <FooterStyle Width="450px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif" ID="QimagebuttonEdit"
                                    CommandName="Edit" ToolTip="Edit" Enabled="false" CommandArgument="<%# Container.DataItemIndex %>">
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
                                <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonDelete.gif" ID="ImageButtonDelete"
                                    ToolTip="Delete" CommandName="Delete" Enabled="false" CommandArgument="<%# Container.DataItemIndex %>"
                                    OnClientClick="ImagebuttonDelete_OnClientClick()"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <br />
            <div id="Div1" style="width: 100%">
                <asp:Label ID="Label3" runat="server" Text="Issues :" Width="130px" />
                <asp:GridView ID="GridViewIssue" runat="server" Width="658px" CssClass="DIVGrid"
                    AutoGenerateColumns="False" AllowPaging="False" BackColor="White" BorderColor="Gray"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ShowFooter="True" Font-Names="Times New Roman"
                    Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal" EmptyDataText="No data."
                    HeaderStyle-BackColor="#a6bce6">
                    <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                    <FooterStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="Issue ID">
                            <ItemTemplate>
                                <a href='<%=ConfigurationManager.AppSettings["IssueViewUrl"]%>+ <%#  DataBinder.Eval(Container.DataItem, "IssueID" )%>'
                                     target="_blank" style="color: Blue" >
                                    <%# DataBinder.Eval(Container.DataItem, "IssueID" )%></a>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="LabelDesc" runat="server" Width="100%" Text='<%#  DataBinder.Eval(Container.DataItem, "IssueTitle" )%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Creator">
                            <ItemTemplate>
                                <asp:Label ID="LabelCreator" runat="server" Width="100%" Text='<%#  DataBinder.Eval(Container.DataItem, "OpenedBy" )%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assign to">
                            <ItemTemplate>
                                <asp:Label ID="LabelOwner" runat="server" Width="100%" Text='<%#  DataBinder.Eval(Container.DataItem, "AssignedTo" )%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="LabelStatus" runat="server" Width="100%" Text='<%#  DataBinder.Eval(Container.DataItem, "IssueStatus" )%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
