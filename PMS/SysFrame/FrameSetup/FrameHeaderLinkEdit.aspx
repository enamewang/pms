<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameHeaderLinkEdit" Codebehind="FrameHeaderLinkEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>HeaderLink Edit</title>
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <base target=_self></base>
    <script language=javascript>
       function closeTop()
       {
           //top.returnValue=document.all("hidAction").value;    //Modify by lee chen on 20091014       
	       top.close();
       }
    
    </script>
   <meta http-equiv="pragma" content="no-cache">
   <meta http-equiv="Cache-Control" content="no-cache, must-revalidate">
   <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT">
</head>
<body>
    <form id="HeaderLink" runat="server">    
      <div>      
          <table style="width: 408px; height: 206px">
              <tr>
                  <td colspan="1" style="width: 22px; height: 22px">
                  </td>
                  <td colspan="2" style="height: 22px">
                      <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text=" Edit Header link"
                          Width="209px" Font-Size="11pt"></asp:Label></td>
              </tr>
              <tr>
                  <td style="width: 22px">
                  </td>
                  <td style="width: 74px">
                  </td>
                  <td style="width: 304px">
                  </td>
              </tr>
              <tr>
                  <td style="width: 22px">
                  </td>
                  <td style="width: 74px">
                      <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></td>
                  <td style="width: 304px">
                      <asp:TextBox ID="txtName" runat="server" Height="12px" Font-Size="9pt" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 74px; height: 23px;">
                      <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                  <td style="height: 23px; width: 304px;">
                      <asp:TextBox ID="txtAddr" runat="server" Height="12px" Width="295px" Font-Size="9pt" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 74px; height: 23px">
                      <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label></td>
                  <td style="height: 23px; width: 304px;">
                      <asp:TextBox ID="txtDesc" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 11px">
                  </td>
                  <td style="width: 74px; height: 11px;">
                      <asp:Label ID="lblLinkType" runat="server" Text="Link type"></asp:Label></td>
                  <td style="font-size: 1pt; height: 11px; width: 304px;">
                      &nbsp;<table style="width: 301px; border-top-style: none; border-right-style: none;
                          border-left-style: none; border-bottom-style: none">
                          <tr>
                              <td style="width: 150px">
                                  <asp:RadioButton ID="rdTypeNew" runat="server" Font-Size="9pt" Text="P--Pop up new window" GroupName="rdType" Width="150px" /></td>
                              <td style="width: 102px">
                                  <asp:RadioButton ID="rdTypeSelf" runat="server" Font-Size="9pt" Text="L--Self window" GroupName="rdType" Width="102px" /></td>
                          </tr>
                      </table>
                  </td>
              </tr>
              <tr>
                  <td style="width: 304px; height: 8px; font-size: 1pt;">
                  </td>
                  <td style="width: 304px; height: 8px; font-size: 1pt;">
                  </td>
                  <td style="height: 8px; font-size: 1pt; width: 304px;"><!--<asp:RadioButton ID="rdTypeMain" runat="server" Font-Size="9pt" Text="M--Main area of framework" GroupName="rdType" Width="168px" />--></td>
              </tr>
              <tr>
                  <td colspan="1" style="font-size: 1pt; width: 22px">
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
