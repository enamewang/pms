<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequirementsFulfillmentRate.aspx.cs"
    Inherits="PMS.PMS.Maintain.RequirementsFulfillmentRate" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Requirements Fulfillment Rate Report</title>
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
            <asp:Label ID="LabelUserDept" runat="server" Text="User Dept :" Width="130px" />
            <asp:DropDownList ID="DropDownListUserDept" runat="server" Width="150px" CssClass="DropDownList"
                OnSelectedIndexChanged="DropDownListUserDept_SelectedIndexChanged" AutoPostBack="true" />
            <asp:Label ID="LabelBlank1" runat="server" Text="" Width="40px" />
            <asp:Label ID="LabelYearMonthFrom" runat="server" Text="Year/Month From :" Width="130px" />
            <asp:TextBox ID="TextBoxYearMonthFrom" runat="server" Width="142px" />
            <asp:Label ID="LabelYearMonthFromFormatTips" runat="server" Text="  (ex:2013/01)" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxYearMonthFrom"
                ErrorMessage="Year/Month From can not be empty!" />
            <br />
            <br />
            <asp:Label ID="LabelDivisionByDept" runat="server" Text="Division By Dept :" Width="130px" />
            <asp:CheckBox ID="CheckBoxDivisionByDept" runat="server" CssClass="DropDownList" />
            <asp:Label ID="LabelBlank2" runat="server" Text="" Width="170px" />
            <asp:Label ID="LabelYearMonthTo" runat="server" Text="Year/Month To :" Width="130px" />
            <asp:TextBox ID="TextBoxYearMonthTo" runat="server" Width="142px" />
            <asp:Label ID="LabelYearMonthToFormatTips" runat="server" Text="  (ex:2013/01)" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxYearMonthTo"
                ErrorMessage="Year/Month To can not be empty!" />
            <br />
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TextBoxYearMonthFrom"
                runat="server" ValidationExpression="^[0-9]{4}\/[0-9][0-9]$" Display="Dynamic"
                ErrorMessage="Year/Month From is error" />
            <asp:Label ID="label1" runat="server" Text="  " Width="50px"></asp:Label>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="TextBoxYearMonthTo"
                runat="server" ValidationExpression="^[0-9]{4}\/[0-9][0-9]$" Display="Dynamic"
                ErrorMessage="Year/Month To is error" />
            <br />
            <br />
            <asp:Label ID="labelInquiryBank" runat="server" Text="  " Width="510px"></asp:Label>
            <asp:Button ID="ButtonInquiry" runat="server" Text="Inquiry" CssClass="ButtonLong"
                Width="100px" OnClick="ButtonInquiry_Click" />
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
