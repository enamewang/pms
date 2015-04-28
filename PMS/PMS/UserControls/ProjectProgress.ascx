<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectProgress.ascx.cs"
    Inherits="PMS.PMS.UserControls.ProjectProgress" %>
<body>
    <div id="DivProjectProgress" style="padding-left: 4px;">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color: LightBlue; height: 32px;" align="center">
                    <table style="width: 150px">
                        <tr align="center">
                            <td style="width: 150px">
                                <asp:Label ID="LabelProjectTitle" runat="server" Text="">"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="width: 150px; height: 16px;">
                                <asp:Label ID="LabelPMSIdTitle" runat="server" Text="">"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="background-color: LightGrey; height: 32px; width: 600px;" align="left">
                    <table>
                        <tr runat="server" id="TRFlow" align="left">
                        </tr>
                        <tr runat="server" id="TRImage">
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:ImageButton ID="ImageButtonStage" runat="server" OnClick="ImageButtonStage_Click" />
</body>
