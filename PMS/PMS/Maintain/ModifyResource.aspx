<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyResource.aspx.cs"
    Inherits="PMS.PMS.Maintain.ModifyResource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Modify Resource</title>
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Save() {
            var oldResource = document.getElementById("<%=TextBoxOldResource.ClientID%>").value;
            var obj = document.getElementById("<%=DropDownListNewResource.ClientID %>");
            index = obj.selectedIndex;
            var newResource = obj.options[index].text;
            if (newResource == "") {
                alert("The newResource can not be empty!");
                return false;
            }
            if (oldResource == newResource) {
                alert("The newResource and oldResource cannot be the same");
                return false;
            }
            return true;
        }
        function SaveSuccess() {
            var obj = document.getElementById("<%=DropDownListNewResource.ClientID %>");
            index = obj.selectedIndex;
            var resultResource = obj.options[index].text;
            window.returnValue = resultResource;
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
                    <span>Modify Resource</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelOldResource" runat="server" Text=" Old Resource:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxOldResource" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"
                        Width="130px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelNewResource" runat="server" Text="New Resource:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListNewResource" runat="server" Width="130px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 530px; margin: 10px auto; padding-left: 20px; padding-top: 20px;">
        <asp:Label ID="label1" runat="server" Text="  " Width="100px"></asp:Label>
        <asp:Button ID="ButtonSave" runat="server" Text="Save" Width="80px" OnClientClick="return Save();"
            OnClick="ButtonSave_Click" />
        <asp:Label ID="label2" runat="server" Text="  " Width="100px"></asp:Label>
        <asp:Button ID="ButtonEdit" runat="server" Text="Exit" Width="80px" OnClientClick="window.close();" />
    </div>
    </form>
</body>
</html>
