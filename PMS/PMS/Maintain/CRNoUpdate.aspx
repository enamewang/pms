<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRNoUpdate.aspx.cs" Inherits="PMS.PMS.Maintain.CRNoUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CR No Update</title>
    <base target="_self" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">

       
        function ButtonOK_ClientClick() {

            var textBoxTempCrNo = document.getElementById("TextBoxTempCrNo");
            var textBoxFormalCrNo = document.getElementById("TextBoxFormalCrNo");
            if (textBoxTempCrNo.value == "") {
                alert("临时 CR No 不能为空");
                event.returnValue = false;
                return;
            }
            if (textBoxFormalCrNo.value == "") {
                alert("正式 CR No 不能为空");
                event.returnValue = false;
                return;
            }
        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 10px; width: 1000px;">
        <label id="lblTitle" class="PageTitle" style="height: 20px; width: 95%">
            CR No Update</label>
        <br />
        <br />
        <asp:Label ID="LabelTempCrNo" Text="临时 CR No (以'T'开头)" Width="160px" runat="server" />
        <asp:TextBox ID="TextBoxTempCrNo" runat="server" Width="160px"  MaxLength="20" />
        <asp:Label ID="Label1" Text="" runat="server" Width="50px" />
        <asp:Label ID="LabelFormalCrNo" Text="正式 CR No (以'CR'开头)" Width="160px" runat="server" />
        <asp:TextBox ID="TextBoxFormalCrNo" runat="server" Width="160px" MaxLength="20"  />
        <br />
        <br />
        <br />
        <asp:Label ID="Label2" Text="" runat="server" Width="620px" />
        <asp:Button ID="ButtonOK" runat="server" Text="OK" CssClass="ButtonLong" OnClientClick="ButtonOK_ClientClick();"
            OnClick="ButtonOK_Click" />
    </div>
    </form>
</body>
</html>
