<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchTaskMaintain.aspx.cs"
    Inherits="PMS.PMS.Maintain.BatchTaskMaintain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Batch Task Maintain</title>
    <base target="_self" />
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //跳转到导入页面
        function ButtonNext_OnClientClick() {
            var pmsid = "<%=pmsid %>";
            var url = "../Maintain/BatchTaskMaintain2.aspx?ViewData=N&PmsID=" + pmsid;
            var features = "dialogWidth=1330px;dialogHeight=710px;center=yes;help=no;status=no;scroll=no";
            var result = window.showModalDialog(url, "", features);
        }
        function ButtonViewData_OnClientClick() {
            var pmsid = "<%=pmsid %>";
            var url = "../Maintain/BatchTaskMaintain2.aspx?ViewData=Y&PmsID=" + pmsid;
            var features = "dialogWidth=1330px;dialogHeight=710px;center=yes;help=no;status=no;scroll=no";
            var result = window.showModalDialog(url, "", features);
        }  
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
    </div>
    <div style="width: 630px; margin: 10px auto; padding-left: 20px; padding-right: 20px;
        padding-top: 20px;">
        <div style="margin: 10px auto; padding-left: 90px; padding-right: 20px; padding-top: 20px;">
            <asp:Label ID="Label1" runat="server" Text="Project Template :" Width="100px" />
            <asp:Label ID="Label2" runat="server" Text="Batch Task Import Template" ForeColor="#FF0000"
                Width="168px" />
            <asp:Button ID="BtnDownLoad" runat="server" Text="Template Download" OnClick="BtnTemplateDownLoad_Click"
                Width="145px" />            
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="(" Width="5px" />
            <asp:Label ID="Label3" runat="server" Text="Sample:" Width="90px" />
            <asp:Label ID="Label4" runat="server" Text="APP_BACH2_Plan" ForeColor="#FF0000"
                Width="168px" />
            <asp:Button ID="Button1" runat="server" Text="Sample Download" OnClick="BtnSampleDownLoad_Click"
                Width="140px" />
                <asp:Label ID="Label6" runat="server" Text=")" Width="5px" />
            
            <br />
            <br />
            <asp:Label ID="LabelCrNo" runat="server" Text="CR No :" Width="100px" />
            <asp:TextBox ID="TextBoxCrNo" runat="server" Width="200px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="LabelXmlFile" runat="server" Text="Xml File :" Width="100px" />
            <asp:FileUpload ID="FileUpload" runat="server" Width="315px" />
            <br />
            <br />
            <asp:Label ID="labelNext" runat="server" Text="  " Width="130px"></asp:Label>
            <asp:Button ID="ButtonNext" runat="server" Text="Next" OnClick="ButtonNext_Click" />
            <asp:Label ID="label7" runat="server" Text="  " Width="30px"></asp:Label>
            <asp:Button ID="ButtonViewData" runat="server" Text="View Data" Width="100px" OnClick="ButtonViewData_Click" />
        </div>
    </div>
    </form>
</body>
</html>
