<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="PMS.PMS.Maintain.AddUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add User</title>

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>
<%@ OutPutCache Location="None"%>
    <base target="_self" />

    <script type="text/javascript">
        //获取页面大小和窗口大小
        function getPageSize() {
            var scrW, scrH;
            if (window.innerHeight && window.scrollMaxY) {
                // Mozilla
                scrW = window.innerWidth + window.scrollMaxX;
                scrH = window.innerHeight + window.scrollMaxY;
            } else if (document.body.scrollHeight > document.body.offsetHeight) {
                // all but IE Mac
                scrW = document.body.scrollWidth;
                scrH = document.body.scrollHeight;
            } else if (document.body) { // IE Mac
                scrW = document.body.offsetWidth;
                scrH = document.body.offsetHeight;
            }
            var winW, winH;
            if (window.innerHeight) { // all except IE
                winW = window.innerWidth;
                winH = window.innerHeight;
            } else if (document.documentElement
                && document.documentElement.clientHeight) {
                // IE 6 Strict Mode
                winW = document.documentElement.clientWidth;
                winH = document.documentElement.clientHeight;
            } else if (document.body) { // other
                winW = document.body.clientWidth;
                winH = document.body.clientHeight;
            }
            // for small pages with total size less than the viewport
            var pageW = (scrW < winW) ? winW : scrW;
            var pageH = (scrH < winH) ? winH : scrH;
            return { pageWidth: pageW, pageHeight: pageH, winWidth: winW, winHeight: winH };
        }

        Number.prototype.NaN0 = function() { return isNaN(this) ? 0 : this; }

    </script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function ButtonSave_ClientClick() {

            var empNo = document.getElementById("<%=TextBoxEmpNo.ClientID %>");
            var englishName = document.getElementById("<%=TextBoxEnglishName.ClientID %>");
            var extention = document.getElementById("<%=TextBoxextention.ClientID %>");
            var role = document.getElementById("<%=TextBoxRole.ClientID %>");

            var ntDomain = document.getElementById("<%=DropDownListNTDomain.ClientID %>");
            var department = document.getElementById("<%=DropDownListDepartment.ClientID %>");
            var domain = document.getElementById("<%=DropDownListDomain.ClientID %>");


            if (ntDomain.value == "") {
                alert("NT Domain is empty");
                return false;
            }
            if (department.value == "") {
                alert("Department is empty");
                return false;
            }
            if (domain.value == "") {
                alert("Domain is empty");
                return false;
            }
 
            if (empNo.value == "") {
                alert("Emp No is empty");
                return false;
            }

            if (englishName.value == "") {
                alert("English Name is empty");
                return false;
            }
            if (extention.value == "") {
                alert("Extention is empty");
                return false;
            }
            if (role.value == "") {
                alert("Role is empty");
                return false;
            }

            return true;
        }

        function ClosedWindow() {
            window.opener = null;
            window.close();
        }
    </script>

    <style type="text/css">
        .style2
        {
            width: 145px;
        }
        .style3
        {
            width: 279px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
 
      <br />
       <br />
    <div>
        <table>
            <tr>
                <td class="style2" align="right">
                    Department:
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="labelPriorityMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="DropDownListDepartment" runat="server" Height="20px" Width="200px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Emp No:
                </td>
                <td>
                    &nbsp;
                    <asp:Label ID="label1" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="TextBoxEmpNo" runat="server" Height="18px" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2" align="right">
                    English Name:
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="label2" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="TextBoxEnglishName" runat="server" Height="18px" Width="195px"></asp:TextBox>
                </td>
                <td align="right">
                    Extention:
                </td>
                <td>
                    &nbsp;
                    <asp:Label ID="label3" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="TextBoxextention" runat="server" Width="200px" Height="18px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2" align="right">
                    NT Domain:
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="label4" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="DropDownListNTDomain" runat="server" Height="20px" Width="200px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>QCS</asp:ListItem>
                        <asp:ListItem>Qisda</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Role:
                </td>
                <td>
                    &nbsp;
                    <asp:Label ID="label5" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="TextBoxRole" runat="server" Width="200px" Height="18px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2" align="right">
                    Domain:
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="label6" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="DropDownListDomain" runat="server" Height="20px" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2" align="right">
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" Height="27px" 
                        Width="70px" OnClientClick="return ButtonSave_ClientClick();" 
                        onclick="ButtonSave_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;  <asp:Button ID="ButtonClose" runat="server" Text="Close" Height="27px" 
                        Width="70px" OnClientClick="return ClosedWindow();" 
                       />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
