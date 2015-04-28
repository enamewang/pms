<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateMeetingMinute.aspx.cs"
    Inherits="PMS.PMS.Maintain.CreateMeetingMinute" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>Create Meeting Minute</title>
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function CheckBeforeSave() {

            var dropDownListMeetingMinuteType = document.getElementById("<%=DropDownListMeetingMinuteType.ClientID %>");
            var textBoxHost = document.getElementById("<%=TextBoxHost.ClientID %>");
            var dropDownListVenue = document.getElementById("<%=DropDownListVenue.ClientID %>");
            var textBoxRecorder = document.getElementById("<%=TextBoxRecorder.ClientID %>");
            var textBoxMeetingStartDate = document.getElementById("<%=TextBoxMeetingStartDate.ClientID %>");
            var textBoxMeetingEndDate = document.getElementById("<%=TextBoxMeetingEndDate.ClientID %>");
            var dropDownListStartHour = document.getElementById("<%=DropDownListStartHour.ClientID %>");
            var dropDownListStartMinute = document.getElementById("<%=DropDownListStartMinute.ClientID %>");
            var dropDownListEndHour = document.getElementById("<%=DropDownListEndHour.ClientID %>");
            var dropDownListEndMinute = document.getElementById("<%=DropDownListEndMinute.ClientID %>");

            var textBoxSubject = document.getElementById("<%=TextBoxSubject.ClientID %>");
            var textBoxAttendee = document.getElementById("<%=TextBoxAttendee.ClientID %>");

            var gridViewConclusion = document.getElementById("<%=GridViewConclusion.ClientID %>");
            var gridViewIssue = document.getElementById("<%=GridViewIssue.ClientID %>");




            if (dropDownListMeetingMinuteType.value == "") {
                alert("Meeting Minute Type is empty");
                dropDownListMeetingMinuteType.focus();
                event.returnValue = false;
                return false;
            }

            if (textBoxHost.value == "") {
                alert("Host is empty");
                textBoxHost.focus();
                event.returnValue = false;
                return false;
            }

            if (dropDownListVenue.value == "") {
                alert("Venue is empty");
                dropDownListVenue.focus();
                event.returnValue = false;
                return false;
            }


            if (textBoxRecorder.value == "") {
                alert("Recorder is empty");
                textBoxRecorder.focus();
                event.returnValue = false;
                return false;
            }

            if (textBoxMeetingStartDate.value == "") {
                textBoxMeetingStartDate.focus();
                alert("Meeting Start Date is empty");
                event.returnValue = false;
                return false;

            }

            if (textBoxMeetingEndDate.value == "") {
                textBoxMeetingEndDate.focus();
                alert("Meeting End Date is empty");
                event.returnValue = false;
                return false;

            }

            // 检查时间是否为同一天，开始时间小于结束时间
            if (textBoxMeetingStartDate.value != textBoxMeetingEndDate.value) {
                alert("Start Date and End Date must be the same!");
                event.returnValue = false;
                return false;
            }

            var startDateArray = new Array();
            startDateArray = textBoxMeetingStartDate.value.split('-');
            var startDateTime = new Date(startDateArray[0], startDateArray[1], startDateArray[2], parseInt(dropDownListStartHour.value), parseInt(dropDownListStartMinute.value));

            var endDateArray = new Array();
            endDateArray = textBoxMeetingEndDate.value.split('-');
            var endDateTime = new Date(endDateArray[0], endDateArray[1], endDateArray[2], parseInt(dropDownListEndHour.value), parseInt(dropDownListEndMinute.value));

            if (startDateTime >= endDateTime) {
                alert("Start Time must be smaller than End Time!");
                event.returnValue = false;
                return false;

            }

            if (textBoxSubject.value == "") {
                textBoxSubject.focus();
                alert("Subject is empty");
                event.returnValue = false;
                return false;

            }

            if (textBoxAttendee.value == "") {
                textBoxAttendee.focus();
                alert("Attendee is empty");
                event.returnValue = false;
                return false;

            }

            //检查Conclusions 和 Issues 不能同时为空
            //这边只有完成Issue刷新功能后才能完善
            //gridViewIssue.length始终小于1可以放到后台
            //            if (!CheckConclusionsIssues(gridViewConclusion, gridViewIssue)) {
            //                alert("Conclusions and Issues must not be empty at the same time!");
            //                event.returnValue = false;
            //                return false;
            //            }

            if (confirm("Confirm to Save and Send mail ?") == true) {
                return true;
            }
            else {
                event.returnValue = false;
                return false;
            }

            return true;
        }


        function CheckConclusionsIssues(gridViewConclusion, gridViewIssue) {

            if (gridViewConclusion.rows.length > 2) {
                return true;
            }

            if (gridViewIssue != null) {
                if (gridViewIssue.rows.length > 1) {
                    return true;
                }
            }

            return false;
        }

        function Refresh() {

            // 调用父窗口的onLoad()
            window.top.onLoad();
        }
        
        
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin: 10px; width: 750px;">
        <div id="DivCreateMeetingMinute" style="width: 100%">
            <asp:Label ID="LabelTop" runat="server" Text="" Width="415px" />
            <asp:Button ID="ButtonSaveTop" runat="server" Text="Save and Send Mail" CssClass="ButtonVeryLong"
                OnClick="ButtonSaveTop_Click" />
            <asp:Label ID="Label2" runat="server" Text="" Width="20px" />
            <asp:Button ID="ButtonCancelTop" runat="server" Text="Cancel" CssClass="ButtonLong" />
            <br />
            <br />
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="LabelMeetingType" runat="server" Text="Meeting Type :" Width="130px" />
                        <asp:Label ID="LabelMeetingTypeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                        <asp:DropDownList ID="DropDownListMeetingMinuteType" runat="server" AutoPostBack="true"
                            CssClass="DropDownList" OnSelectedIndexChanged="DropDownListMeetingMinuteType_SelectedIndexChanged" />
                        <asp:Label ID="LabelBlank1" runat="server" Text="" Width="38px" />
                        <asp:Label ID="LabelHost" runat="server" Text="Host :" Width="130px" />
                        <asp:Label ID="LabelHostMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                        <asp:TextBox ID="TextBoxHost" runat="server" CssClass="TextBoxNormal" MaxLength="30" />
                        <br />
                        <br />
                        <asp:Label ID="LabelVenue" runat="server" Text="Venue :" Width="130px" />
                        <asp:Label ID="LabelVenueMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                        <asp:DropDownList ID="DropDownListVenue" runat="server" CssClass="DropDownList" />
                        <asp:Label ID="LabelBlank2" runat="server" Text="" Width="38px" />
                        <asp:Label ID="LabelRecorder" runat="server" Text="Recorder :" Width="130px" />
                        <asp:Label ID="LabelRecorderMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                        <asp:TextBox ID="TextBoxRecorder" runat="server" CssClass="TextBoxNormal" MaxLength="30" />
                        <br />
                        <br />
                        <asp:Label ID="LabelMeetingStartTime" runat="server" Text="Meeting Start Time :"
                            Width="130px" />
                        <asp:Label ID="LabelMeetingStartTimeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                        <asp:TextBox ID="TextBoxMeetingStartDate" runat="server" CssClass="TextBoxShort" />
                        <asp:DropDownList ID="DropDownListStartHour" runat="server" Height="21px" CssClass="DropDownListVeryShort" />
                        <asp:DropDownList ID="DropDownListStartMinute" runat="server" Height="21px" CssClass="DropDownListVeryShort" />
                        <asp:Label ID="LabelBlank3" runat="server" Text="" Width="38px" />
                        <asp:Label ID="LabelMeetingEndTime" runat="server" Text="Meeting End Time :" Width="130px" />
                        <asp:Label ID="LabelMeetingEndTimeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                        <asp:TextBox ID="TextBoxMeetingEndDate" runat="server" CssClass="TextBoxShort" />
                        <asp:DropDownList ID="DropDownListEndHour" runat="server" Height="21px" CssClass="DropDownListVeryShort" />
                        <asp:DropDownList ID="DropDownListEndMinute" runat="server" Height="21px" CssClass="DropDownListVeryShort" />
                        <br />
                        <br />
                        <asp:Label ID="LabelSubject" runat="server" Text="Subject :" Width="130px" />
                        <asp:Label ID="LabelSubjectMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                        <asp:TextBox ID="TextBoxSubject" runat="server" CssClass="TextBoxLong" MaxLength="900" />
                        <br />
                        <br />
                        <asp:Label ID="LabelAttendee" runat="server" Text="Attendee :" Width="130px" />
                        <asp:Label ID="LabelAttendeeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                        <asp:TextBox ID="TextBoxAttendee" runat="server" CssClass="TextBoxLong" MaxLength="900" />
                        <br />
                        <br />
                        <asp:Label ID="LabelConclusion" runat="server" Text="Conclusions :" Width="130px" />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="DivConclusion" style="width: 100%">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridViewConclusion" runat="server" Width="665px" CssClass="DIVGrid"
                            AutoGenerateColumns="False" AllowPaging="False" BackColor="White" BorderColor="Gray"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ShowFooter="True" Font-Names="Times New Roman"
                            Font-Size="9pt" DataKeyNames="Serial" ForeColor="Gray" GridLines="Horizontal"
                            EmptyDataText="No data." HeaderStyle-BackColor="#a6bce6" OnRowCommand="GridViewConclusion_RowCommand"
                            OnRowDataBound="GridViewConclusion_RowDataBound" OnRowDeleting="GridViewContactWindow_RowDeleting"
                            OnRowCancelingEdit="GridViewContactWindow_RowCancelingEdit" OnRowUpdating="GridViewContactWindow_RowUpdating"
                            OnRowEditing="GridViewInquiry_RowEditing">
                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                            <FooterStyle BackColor="WhiteSmoke" />
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
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxDesc" runat="server" MaxLength="50" Width="100%" Text='<%#  DataBinder.Eval(Container.DataItem, "Description" )%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBoxDesc" runat="server" MaxLength="50" Width="100%" />
                                    </FooterTemplate>
                                    <ItemStyle Width="450px" />
                                    <FooterStyle Width="450px" />
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
                                    <FooterTemplate>
                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonSave.gif" ID="QimagebuttonSave"
                                            CommandName="Save" ToolTip="Save" CommandArgument="<%# Container.DataItemIndex %>">
                                        </asp:ImageButton>
                                    </FooterTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    <FooterStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonDelete.gif" ID="ImageButtonDelete"
                                            ToolTip="Delete" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    <FooterStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <br />
            <div id="Div1" style="width: 100%">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="Label3" runat="server" Text="Issues :" Width="130px" />
                        <asp:Button ID="ButtonCreateIssue" runat="server" Text="Crearte Issue" Width="110px"
                            CssClass="ButtonVeryLong" OnClick="ButtonCreateIssue_Click" />
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
                                            target="_blank" style="color:Blue">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <asp:Label ID="LabelBlankUnder" runat="server" Text="" Width="415px" />
            <asp:Button ID="ButtonSaveUnder" runat="server" Text="Save and Send Mail" CssClass="ButtonVeryLong"
                OnClick="ButtonSaveUnder_Click" />
            <asp:Label ID="LabelBlank9" runat="server" Text="" Width="20px" />
            <asp:Button ID="ButtonCancelUnder" runat="server" Text="Cancel" CssClass="ButtonLong" />
        </div>
        <div>
        </div>
    </div>
    </form>
</body>
</html>
