<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameParameterEdit" Codebehind="FrameParameterEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Parameter edit</title>
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <base target=_self></base>
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate">
    <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT">
    <script language=javascript>
       function closeTop()
       {
           //top.returnValue=document.all("hidAction").value;     //Modify by lee chen on 20091014         
	       top.close();
       }
    
    </script>
</head>
<body>
    <form id="ParameterE" runat="server">
      <div> 
          <table style="width: 412px; height: 182px">
              <tr>
                  <td colspan="1" style="height: 28px; width: 12px;">
                  </td>
                  <td colspan="2" style="height: 28px">
                      <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text=" Edit Parameter"
                          Width="129px" Font-Size="11pt"></asp:Label></td>
              </tr>
              <tr>
                  <td style="width: 12px">
                  </td>
                  <td style="width: 82px">
                  </td>
                  <td style="width: 302px">
                  </td>
              </tr>
              <tr>
                  <td style="width: 12px">
                  </td>
                  <td style="width: 82px">
                      <asp:Label ID="lblName" runat="server" Text="Name" Width="80px"></asp:Label></td>
                  <td style="width: 302px">
                      <asp:TextBox ID="txtName" runat="server" Height="12px" Font-Size="9pt" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 12px">
                  </td>
                  <td style="width: 82px">
                      <asp:Label ID="lblValue" runat="server" Text="Value" Width="80px"></asp:Label></td>
                  <td style="width: 302px">
                      <asp:TextBox ID="txtValue" runat="server" Height="12px" Width="295px" Font-Size="9pt" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 12px">
                  </td>
                  <td style="width: 82px">
                      <asp:Label ID="lblDesc" runat="server" Text="Description" Width="80px"></asp:Label></td>
                  <td style="width: 302px">
                      <asp:TextBox ID="txtDesc" runat="server" Height="12px" Width="295px" Font-Size="9pt" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 12px; height: 16px">
                  </td>
                  <td style="width: 82px; height: 16px;">
                  </td>
                  <td style="height: 16px; width: 302px;">
                  </td>
              </tr>
              <tr>
                  <td colspan="1" style="font-size: 1pt; width: 12px;">
                  </td>
                  <td colspan="2" style="font-size: 1pt">
                      <table style="width: 379px">
                          <tr>
                              <td style="width: 193px">
                                  <input id="hidAction" runat="server" style="width: 75px" type="hidden" /></td>
                              <td>
                                  <asp:Button ID="btnSave" runat="server" CssClass="ButtonLong" OnClick="btnSave_Click"
                                      Text="Save" Font-Size="9pt" /></td>
                              <td>
                                  <input id="btnClose" class="ButtonLong" onclick="javascript:closeTop();" style="font-size: 9pt;" type="button" value="Close" runat="server" /></td>
                          </tr>
                      </table>
                  </td>
              </tr>
          </table>
                  
          
      </div>
    </form>
</body>
</html>
