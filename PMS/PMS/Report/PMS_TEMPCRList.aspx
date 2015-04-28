<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PMS_TEMPCRList.aspx.cs" Inherits="PMS.PMS.Report.PMSTEMPCRList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Not Closed CR List</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="460px" ProcessingMode="Remote" Width="996px">
            <ServerReport ReportPath="/PMS Reporting Service/PMS_TEMPCRList" 
                ReportServerUrl="http://10.85.10.3/ReportServer_MSSQLSERVER2008" />
        </rsweb:ReportViewer>
    </div>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            //debugger;

            AutoResize();
            window.onresize = AutoResize;


        }
        function AutoResize() {
            var o_r1 = document.getElementById("<%=ReportViewer1.ClientID %>");

            var hWindow = document.documentElement.offsetHeight - 50;
            //debugger;
            if (o_r1 != null) {
                o_r1.style.height = hWindow.toString() + "px";
            }
        }
        </script>
    </form>
</body>
</html>
