<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingMinuteMaintain.aspx.cs"
    Inherits="PMS.PMS.Maintain.MeetingMinuteMaintain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MeetingMinute Maintain</title>

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/myStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function OpenEditMeetingMinute(pmsid, crid, minid) {

            var url = "EditMeetingMinuteExcessivePage.aspx?PmsID=" + pmsid + "&CrID=" + crid + "&MinID=" + minid;
            var features = "dialogWidth=750px;dialogHeight=auto;center=yes;help=no;status=no";
            window.showModalDialog(url, "", features);
        }

        function OpenViewMeetingMinute(pmsid, crid, minid) {
            var url = "ViewMeetingMinuteExcessivePage.aspx?PmsID=" + pmsid + "&CrID=" + crid +  "&MinID=" + minid;
            var features = "dialogWidth=700px;dialogHeight=auto;center=yes;help=no;status=no";
            window.showModalDialog(url, "", features);
        }

        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%; background-color: aliceblue;">
                <asp:Button ID="ButtonCreateMeetingMinute" runat="server" Text="Create Meeting Minutes"
                    Width="170px" Height="20px" OnClick="ButtonCreateMeetingMinute_Click" />
            </div>
            <br />
            <div id="divGridView" runat="server">
                <anthem:GridView ID="gridViewMeetingMinute" CssClass="DIVGrid" runat="server" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal"
                    Width="100%" EmptyDataText="No data." OnRowCommand="gridViewMeetingMinute_RowCommand"
                    OnRowDataBound="gridViewMeetingMinute_RowDataBound" OnRowDeleting="gridViewMeetingMinute_RowDeleting"
                    OnRowEditing="gridViewMeetingMinute_RowEditing">
                    <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                    <PagerSettings Position="TopAndBottom" Visible="False" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span>Meeting Type</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="labelTypeName" runat="server"> 
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="160px" />
                            <ItemStyle Width="160px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span>Subject</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="labelSubject" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Subject").ToString())%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="350px" />
                            <ItemStyle Width="350px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span>Open Issue</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                               <div Runat="server" ID="divOpenIssue"></div>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span>Meeting Date</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="labelCreateDate" runat="server" Text='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "StartTime", "{0:yyyy-MM-dd}")))%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="95px" />
                            <ItemStyle Width="95px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span>Recorder</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="labelRecorder" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Recorder").ToString())%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="95px" />
                            <ItemStyle Width="95px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <div style="width: 50px; text-align: center;">
                                    <asp:ImageButton ID="imageButtonEdit" runat="server" CommandName="EditPage" CommandArgument="<%# Container.DataItemIndex %>"
                                        OnClick="ButtonEditMeetingMinute_Click" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif">
                                    </asp:ImageButton>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <div style="width: 50px; text-align: center;">
                                    <asp:ImageButton ID="imageButtonDetail" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        ImageUrl="~/SysFrame/images/detail.gif"></asp:ImageButton>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <div style="width: 50px; text-align: center;">
                                    <asp:ImageButton ID="imageButtonDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>"
                                        OnClientClick="javascript:if (!confirmDelete()) return;" ImageUrl="~/SysFrame/images/ButtonDelete.gif">
                                    </asp:ImageButton>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="labelMinId" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Mnid").ToString())%>'> 
                                </asp:Label>
                            </ItemTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
