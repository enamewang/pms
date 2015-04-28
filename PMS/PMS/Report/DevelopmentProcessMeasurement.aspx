<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DevelopmentProcessMeasurement.aspx.cs"
    Inherits="PMS.PMS.Report.DevelopmentProcessMeasurement" %>

<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Development Process Measurement </title>
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <base target="_self" />

    <script language="javascript" type="text/javascript">
        function pageLoad() {
            //debugger;

            AutoResize();
            window.onresize = AutoResize;


        }
        function AutoResize() {
            var o_r1 = document.getElementById("<%=ReportViewer1.ClientID %>");

            var hWindow = document.documentElement.offsetHeight - 110;
            //debugger;
            if (o_r1 != null) {
                o_r1.style.height = hWindow.toString() + "px";
            }
        }

        function DropDownListDepartmentOnChange() {
            var userDept = document.getElementById("DropDownListDepartment").value;
            if (userDept != "") {
                document.getElementById("TextBoxUserName").value = "";
            }
        }

        function TextBoxUserNameOnBlur() {
            var userName = document.getElementById("TextBoxUserName").value;
            if (userName != "") {
                document.getElementById("DropDownListDepartment").value = "";
            }
        }

        function ButtonInquiry_OnClientClick() {
            var userDept = document.getElementById("DropDownListDepartment").value;
            var userName = document.getElementById("TextBoxUserName").value;
            if (userDept == "" && userName == "") {
                alert("UserDept and UserName can not be empty at the same time!");
                return false;
            }
            if (document.getElementById("TextBoxDateFrom").value == "") {
                alert("Date From can not be empty!");
                return false;
            }
            if (document.getElementById("TextBoxDateTo").value == "") {
                alert("Date To can not be empty!");
                return false;
            }
            return true;
        }     
                     
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <div style="margin: 10px; width: 1000px;">
        <div>
            <asp:Label ID="LabelDepartment" runat="server" Text="RD Dept :" Width="130px" />
            <asp:DropDownList ID="DropDownListDepartment" runat="server" Width="150px" CssClass="DropDownList"
                onchange="DropDownListDepartmentOnChange()" />
            <asp:Label ID="LabelBlank1" runat="server" Text="" Width="38px" />
            <asp:Label ID="LabelUserName" runat="server" Text="User Name :" Width="130px" />
            <asp:TextBox ID="TextBoxUserName" runat="server" Width="142px" onblur="TextBoxUserNameOnBlur()" />
            <br />
            <br />
            <asp:Label ID="LabelDateFrom" runat="server" Text="Date From :" Width="130px" />
            <TW:DateTextBox ID="TextBoxDateFrom" Width="142px" runat="server" IsDisplayTime="false"
                Language="English"></TW:DateTextBox>
            <asp:Label ID="Label1" runat="server" Text="" Width="40px" />
            <asp:Label ID="LabelDateTo" runat="server" Text="Date To :" Width="130px" />
            <TW:DateTextBox ID="TextBoxDateTo" Width="142px" runat="server" IsDisplayTime="false"
                Language="English"></TW:DateTextBox>
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="labelInquiryBank" runat="server" Text="  " Width="510px"></asp:Label>
            <asp:Button ID="ButtonInquiry" runat="server" Text="Inquiry" CssClass="ButtonLong"
                Width="100px" OnClick="ButtonInquiry_Click" OnClientClick="return ButtonInquiry_OnClientClick()" />
            <br />
            <br />
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        Width="100%" Height="100%" ProcessingMode="Remote">
                        <ServerReport ReportServerUrl="http://10.85.10.3/ReportServer_MSSQLSERVER2008" />
                    </rsweb:ReportViewer>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ButtonInquiry" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
