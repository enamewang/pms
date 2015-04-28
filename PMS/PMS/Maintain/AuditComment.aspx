<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditComment.aspx.cs" Inherits="PMS.PMS.Maintain.AuditComment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Aduit Comment</title>
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Save() {
            var auditComment = document.getElementById("<%=TextBoxAuditComment.ClientID%>").value;
            if (auditComment == "") {
                alert("The auditComment can not empty!");
                return false;
            }
            window.returnValue = auditComment;
            window.close();
            //return true;
        }
        function SaveSuccess() {
            window.returnValue = "已拒绝";
            window.close();
        }        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" EnablePartialRendering="true" runat="server"
        ScriptMode="Debug">
    </asp:ScriptManager>
    <div style="width: 530px; margin: 10px auto; padding-left: 15px; padding-top: 20px;">
        <table style="border: 1px dashed #CCCCCC; width: 530px">
            <tr style="background-color: #EFEFEF">
                <td colspan="2">
                    <span>Aduit Comment</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelAduitComment" runat="server" Text=" Aduit Comment:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxAuditComment" runat="server" Width="380px" Style="height: 80px"
                        TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 530px; margin: 10px auto; padding-left: 20px; padding-top: 20px;">
        <asp:Label ID="label1" runat="server" Text="  " Width="100px"></asp:Label>
        <asp:Button ID="ButtonSave" runat="server" Text="Save" Width="80px" OnClientClick="return Save();"
            />
        <asp:Label ID="label2" runat="server" Text="  " Width="100px"></asp:Label>
        <asp:Button ID="ButtonEdit" runat="server" Text="Exit" Width="80px" OnClientClick="window.close();" />
    </div>
    </form>
</body>
</html>
