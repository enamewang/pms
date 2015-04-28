<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameMiddle" Codebehind="FrameMiddle.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Middle</title>

    <script>
        function switchSysBar() {
            if (document.all("hdH").value == "N") {
                document.all("imgSplitter").src = "images/Icon_appear.gif";
                document.all("hdH").style.display = "none";
                document.all("hdH").value = "Y";
                parent.main.cols = "0,10,*";
            }
            else {
                document.all("imgSplitter").src = "images/Icon_hide.gif";
                document.all("hdH").style.display = "block";
                document.all("hdH").value = "N";
                parent.main.cols = "175,10,*";
            }
        }
      
      
    </script>

</head>
<body style="margin: 0px" scroll="no" onclick="javascript:switchSysBar();">
    <form id="Middle" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" style="cursor: hand;">
    <tr style="height:25px">
        <td  style="height: 25px; background-image: url(images/dot.jpg); background-repeat: repeat-y; width: 2px;" align="left"></td>
        <td></td>
    </tr>
        <tr>
            <td style="height: 550px; background-image: url(images/dot.jpg); background-repeat: repeat-y; width: 2px;">
            </td>
            <td valign="top">
                <img id="imgSplitter" src="images/Icon_hide.gif" alt="Hide the menu~" />
                <input type="hidden" id="hdH" value="N" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
