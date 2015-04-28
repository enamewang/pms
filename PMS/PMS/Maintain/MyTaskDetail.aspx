<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyTaskDetail.aspx.cs" Inherits="PMS.PMS.Maintain.MyTaskDetail" %>

<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>My Task Detail</title>

    <script src="../Javascript/sdp.js" type="text/javascript"></script>

    <link href="../../Style/MyTaskStyle.css" type="text/css" rel="stylesheet" />
    <base target="_self" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT" />
    <style type="text/css">
        .style1
        {
            float: left;
            height: 40px;
            width: 122px;
            text-align: left;
            padding-left: 5px;
            padding-right: 1px;
        }
        .style2
        {
            width: 122px;
        }
        .style3
        {
            float: left;
            height: 40px;
            width: 116px;
            text-align: left;
            padding-left: 5px;
            padding-right: 1px;
        }
        .style4
        {
            width: 116px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" EnablePartialRendering="true" runat="server"
        ScriptMode="Debug">
    </asp:ScriptManager>
    <%--<div class="example">Edit My Task Info</div>--%>
    <div style="padding-left: 40px; padding-right: 40px; padding-top: 40px">
        <table>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelProjects" runat="server" Text="My Project"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxProjectName" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="173px"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:Label ID="LabelSysName" runat="server" Text="System"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxSysName" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td class="style1">
                    <asp:Label ID="LabelTaskStatus" runat="server" Text="Task Status"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxTaskStatus" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="168px"></asp:TextBox>
                </td>
               <%-- <td class="style1">
                    <asp:Label ID="LabelForeTask" runat="server" Text="Pre Task"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxForeTask" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="168px"></asp:TextBox>
                </td>--%>
                <td class="style3">
                    <asp:Label ID="LabelTaskName" runat="server" Text="Task Name"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxTaskName" runat="server" Width="201px" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelRole" runat="server" Text="Role"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxRole" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="167px"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:Label ID="LabelResource" runat="server" Text="Resource"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxResource" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="197px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelPlanStart" runat="server" Text="Plan Start Date"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxPlanStart" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="167px"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:Label ID="LabelPlanEnd" runat="server" Text="Plan End Date"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxPlanEnd" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="196px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelPlanCost" runat="server" Text="Plan Cost  (H)"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:TextBox ID="TextBoxPlanCost" runat="server" ReadOnly="true" Style="border: none;
                        border-bottom: 1px solid #000;" Width="168px"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:Label ID="LabelActualCost" runat="server" Text="Actual Cost(H)"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <asp:UpdatePanel ID="updateActualCost" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="TextBoxActualCost" runat="server" CssClass="textBoxCost" OnTextChanged="TextBoxActualCost_TextChanged"
                                AutoPostBack="true" onkeyup="clearNoNum(this)"></asp:TextBox>
                            <asp:TextBox ID="TextBoxComplete" runat="server" CssClass="textBoxCost" onkeyup="clearNoNum(this)"></asp:TextBox><b>%</b>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelActualStrat" runat="server" Text="Actual Start Date"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <TW:DateTextBox ID="DateTextBoxActualStart" CssClass="textBox" runat="server" IsDisplayTime="false"
                        Language="English" Width="100px"></TW:DateTextBox>
                </td>
                <td class="style3">
                    <asp:Label ID="LabelActualEnd" runat="server" Text="Actual End Date"></asp:Label>
                </td>
                <td class="textBoxLi">
                    <TW:DateTextBox ID="DateTextBoxActualEnd" CssClass="textBox" runat="server" IsDisplayTime="false"
                        Language="English" Width="100px"></TW:DateTextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                </td>
                <td>
                </td>
                <td class="style4">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LabelRemark" runat="server" Text="Remark"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBoxRemark" runat="server" TextMode="MultiLine" Height="80px"
                        Width="98%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <%-- CssClass="multiLine"--%>
    </div>
    <div>
        <br />
        <p style="text-align: center">
            <asp:Button ID="ButtonSave" runat="server" Text="Save" Width="80" OnClick="ButtonSave_Click"
                OnClientClick="return CheckSaveMyTask();" /></p>
    </div>
    </form>
</body>
</html>
