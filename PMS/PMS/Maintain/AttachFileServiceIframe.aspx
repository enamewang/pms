<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachFileServiceIframe.aspx.cs"
    Inherits="PMS.PMS.Maintain.AttachFileServiceIframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AttachFile</title>
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <iframe id="file" name="file" src="AttachFileMaintainService.aspx" width="500px" height="180px"
            frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes" ></iframe>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <div>
    </div>
    </form>
</body>
</html>
