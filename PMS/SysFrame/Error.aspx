<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_Error" Codebehind="Error.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Information</title>
    <link href="styles/FrameStyle.css" type="text/css" rel="stylesheet">

    <script language="javascript">
		   function Refresh()
		   {
		       window.open("default.aspx","_parent");
		      
		   }
    </script>

</head>
<body>
    <form id="ErrorPage" runat="server">
        <div>
            <table id="tblMain" style="z-index: 101; left: 110px; width: 550px; position: absolute;
                top: 110px;" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="left" style="height: 20px; width: 36px;" valign="top">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/SysFrame/images/info_i.jpg" Height="28px" Width="28px" />
                    <td align="left" style="height: 20px; width: 509px;" valign="middle">
                       <asp:Label ID="lblTitle" Style="z-index: 102;" runat="server" Font-Size="10pt" Font-Names="宋体"
                            Width="370px" Height="16px" Font-Bold="True" Font-Underline="False" ForeColor="DarkOrchid"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="font-size: 1pt; background-image: url(images/n6.gif);
                        height: 1px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="font-size: 1pt; 
                        height: 8px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 36px;" valign="top">
                    </td>
                    <td valign="top" align="left" style="width: 509px">
                        <asp:Label ID="lblError" Style="z-index: 103;" runat="server" Font-Size="10pt" Font-Names="宋体"
                            Width="98%" Height="98%" ForeColor="SteelBlue">Description: </asp:Label></td>
                </tr>
                <tr>
                    <td align="left" style="width: 36px; height: 50px" valign="top">
                    </td>
                    <td align="left" style="height: 50px; width: 509px;" valign="bottom">
            <input class="ButtonLong" id="btnGoBack" style="z-index: 106;" onclick="javascript:history.back();"
                type="button" value="Go back" name="btnGoBack">&nbsp; &nbsp;<input class="ButtonLong" id="btnRef" style="z-index: 106;" onclick="javascript:Refresh();"
                type="button" value="Refresh" name="btnRef"></td>
                </tr>
            </table>
            &nbsp; &nbsp;&nbsp;
            <img src="images/error.gif" style="width: 100px; height: 102px; left: 18px; position: absolute; top: 88px;" /></div>
    </form>
</body>
</html>
