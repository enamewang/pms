<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachFileMaintain.aspx.cs"
    Inherits="PMS.PMS.Maintain.AttachFileMaintain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attach File</title>
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formInquiryAndExport" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td class="SpaceTdWidth" style="height: 8px; width: 25px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="SpaceTdWidth" style="width: 25px">
                        </td>
                        <td>
                            <asp:Label ID="labelTitle" runat="server" CssClass="HeadLabel" Text="Attach File"
                                ForeColor="SteelBlue" Font-Bold="True" Font-Size="11pt" Width="300px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 6px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px">
                        </td>
                        <td>
                            <asp:Label ID="labelDocType" runat="server" Text="Document Type" Width="120px"></asp:Label>
                            <asp:DropDownList ID="dropdownlistDocType" runat="server" Width="384px" AutoPostBack="true"
                                OnSelectedIndexChanged="dropdownlistDocType_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="divUrl">
                        <td style="height: 22px">
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="labelUrl" runat="server" Text="URL" Width="120px"></asp:Label>
                                <asp:TextBox ID="textboxUrl" runat="server" Width="380px" MaxLength="500"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="Tr1">
                        <td style="height: 22px">
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="labelFileName" runat="server" Text="FileName" Width="120px" Visible="false"
                                    ForeColor="red"></asp:Label>
                                <asp:TextBox ID="labelFileNameIuput" runat="server" CssClass="UnderLineOnlyTextBoxForFileName"
                                    Visible="false" ForeColor="red"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="Tr2">
                        <td style="height: 22px">
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="labelFileNameUIPIS" runat="server" Text="or" Width="120px" Visible="false"
                                    ForeColor="red"></asp:Label>
                                <asp:TextBox ID="labelFileNameUIPISIuput" runat="server" CssClass="UnderLineOnlyTextBoxForFileName"
                                    Visible="false" ForeColor="red"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="Tr3">
                        <td style="height: 22px">
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="labelFileNameServicePIS" runat="server" Text=" " Width="120px" Visible="false"
                                    ForeColor="red"></asp:Label>
                                <asp:TextBox ID="labelFileNameServicePISIuput" runat="server" CssClass="UnderLineOnlyTextBoxForFileName"
                                    Visible="false" ForeColor="red"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="Tr4">
                        <td style="height: 22px">
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="labelFileNameBusinessPIS" runat="server" Text=" " Width="120px" Visible="false"
                                    ForeColor="red"></asp:Label>
                                <asp:TextBox ID="labelFileNameBusinessPISIuput" runat="server" CssClass="UnderLineOnlyTextBoxForFileName"
                                    Visible="false" ForeColor="red"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 45px">
                        </td>
                        <td>
                            <asp:Label ID="labelBank" runat="server" Text="  " Width="160px"></asp:Label>
                            <asp:Button ID="buttonSave" runat="server" Text="Save" Width="80px" OnClick="buttonSave_Click" />
                            <asp:Label ID="labelBankSave" runat="server" Text="  " Width="35px"></asp:Label>
                            <asp:Button ID="buttonCancel" runat="server" Text="Cancel" Width="80px" OnClientClick="JavaScript:window.close();" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
