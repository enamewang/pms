<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotCloseCRComments.aspx.cs" Inherits="PMS.PMS.Maintain.NotCloseCRComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Not Close CR Comments</title>
    <script src="../Javascript/sdp.js" type="text/javascript"></script>
    <script src="../Javascript/PmsCommonJSFuction.js" type="text/javascript"></script>

    <link href="../../Style/MyTaskStyle.css" type="text/css" rel="stylesheet" />
    <base target="_self" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT" />
    <style type="text/css">
        .style1
        {
            width: 171px;
        }
        .style2
        {
            width: 208px;
        }
        #TableCRInfo
        {
            width: 633px;
        }
        .style3
        {
            width: 173px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </p>
        <asp:UpdatePanel ID="UpdatePanelGridViewBug" runat="server">
            <ContentTemplate>
                <div>
                <b>CR information</b>
                <ul class="myTaskUl">
                    <Table ID="tablecrinfo" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="LabelCRID" runat="server" Text="CR ID：">
                                </asp:Label>
                            </td>
                            <td class="style2">
                                <asp:DropDownList ID="DropDownListCRID" runat="server" Width="163px"
                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownListCRID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelCRName" runat="server" Text="CR Name：">
                                </asp:Label>
                            </td>
                            <td class="style1" colspan=3>
                                <asp:Label ID="LabelCRNameD" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelType" runat="server" Text="CR Type：">
                                </asp:Label>
                            </td>
                            <td class="style2">
                                <asp:Label ID="LabelTypeD" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelSystem" runat="server" Text="System：">
                                </asp:Label>
                            </td>
                            <td class="style3">
                                <asp:Label ID="LabelSystemD" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelPM" runat="server" Text="PM：">
                                </asp:Label>
                            </td>
                            <td class="style2">
                                <asp:Label ID="LabelPMD" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelSD" runat="server" Text="SD：">
                                </asp:Label>
                            </td>
                            <td class="style3">
                                <asp:Label ID="LabelSDD" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelReleaseDate" runat="server" Text="Release Date：">
                                </asp:Label>
                            </td>
                            <td class="style2">
                                <asp:Label ID="LabelReleaseDateD" runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelCRCost" runat="server" Text="Cost(day)：">
                                </asp:Label>
                            </td>
                            <td class="style3">
                                <asp:Label ID="LabelCRCostD" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                    </Table>
                </ul>
                <b>PM Comments</b>
                <ul class="myTaskUl">
                    <asp:TextBox ID="TextBoxPMCom" runat="server" Height="90px" Width="676px" 
                        TextMode="MultiLine"></asp:TextBox>
                </ul>
                <b>RD Comments</b>
                <ul class="myTaskUl">
                    <asp:TextBox ID="TextBoxSDCom" runat="server" Height="90px" Width="670px" 
                        TextMode="MultiLine"></asp:TextBox>
                </ul>
            </div>
            <div>
                <ul class="myTaskUl">
                    <asp:Button ID="ButtonSave" runat="server" Text="SAVE" 
                        onclick="ButtonSave_Click" />
                </ul>
            </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ButtonSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="DropDownListCRID" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
