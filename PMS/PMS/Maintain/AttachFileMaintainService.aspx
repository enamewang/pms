<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachFileMaintainService.aspx.cs"
    Inherits="PMS.PMS.Maintain.AttachFileMaintainService" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attach File</title>
    <base target="_self" />
    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
         <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
     function ButtonUpload_ClientClick()
     {
       var 	hiddenField1=window.parent.document.getElementById("HiddenField1");
       var  hiddenField2=window.parent.document.getElementById("HiddenField2");
       var 	hiddenField3=document.getElementById("HiddenField3");
       var  hiddenField4=document.getElementById("HiddenField4");
     	hiddenField3.value = hiddenField1.value;
     	hiddenField4.value = hiddenField2.value; 
     }

     

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="SpaceTdWidth" style="height: 8px; width: 25px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="width: 25px">
                </td>
                <td>
                    <asp:Label ID="labelTitle" runat="server" CssClass="HeadLabel" Text="Attach File"
                        ForeColor="SteelBlue" Font-Bold="True" Font-Size="11pt" Width="300px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 6px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td class="TDWidth" align="left">
                    <asp:FileUpload ID="FileUpload" runat="server" Width="270px" />
                    <asp:Label ID="labelUploadBank" runat="server" Text="  " Width="30px"></asp:Label>
                    <asp:Button ID="ButtonUpload" runat="server" Text="Upload" OnClientClick="ButtonUpload_ClientClick();"
                        OnClick="ButtonUpload_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 8px">
                </td>
                <td>
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
