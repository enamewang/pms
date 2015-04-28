<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameLeftMenuEdit" Codebehind="FrameLeftMenuEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Left menu setup</title>
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <base target=_self></base>
    <script language=javascript>
       function closeTop()
       {
           //top.returnValue=document.all("hidAction").value;           
	       top.close();
       }
    
    </script>
   <meta http-equiv="pragma" content="no-cache">
   <meta http-equiv="Cache-Control" content="no-cache, must-revalidate">
   <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT">
</head>
<body>
    <form id="LeftMenuEdit" runat="server">    
      <div>      
          <table style="width: 461px; height: 182px">
              <tr>
                  <td colspan="1" style="width: 22px; height: 22px">
                  </td>
                  <td colspan="2" style="height: 22px">
                      <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text=" Edit Navigation menu Item"
                          Width="188px" Font-Size="11pt"></asp:Label></td>
              </tr>
              <tr>
                  <td style="width: 22px">
                  </td>
                  <td style="width: 103px">
                  </td>
                  <td>
                  </td>
              </tr>
              <tr>
                  <td style="width: 22px">
                  </td>
                  <td style="width: 103px">
                      <asp:Label ID="lblID" runat="server" Text="Module ID"></asp:Label></td>
                  <td>
                      <asp:TextBox ID="txtID" runat="server" Height="12px" Font-Size="9pt" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 103px; height: 23px;">
                      <asp:Label ID="lblName" runat="server" Text="Module Name"></asp:Label></td>
                  <td style="height: 23px">
                      <asp:TextBox ID="txtName" runat="server" Height="12px" Width="295px" Font-Size="9pt" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 103px; height: 23px">
                      <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label></td>
                  <td style="height: 23px">
                      <asp:TextBox ID="txtDesc" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 11px">
                  </td>
                  <td style="width: 103px; height: 11px;">
                      <asp:Label ID="lblLink" runat="server" Text="Address"></asp:Label></td>
                  <td style="font-size: 1pt; height: 11px">
                      &nbsp;<asp:TextBox ID="txtAddr" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 16px">
                  </td>
                  <td style="width: 103px; height: 16px;">
                      <asp:Label ID="lblPID" runat="server" Text="Parent Name"></asp:Label></td>
                  <td style="height: 16px">
                      <asp:DropDownList ID="ddlPID" runat="server" Width="302px" Font-Names="Times New Roman" Font-Size="9pt">
                      </asp:DropDownList></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 16px">
                  </td>
                  <td style="width: 103px; height: 16px">
                      <asp:Label ID="lblOrder" runat="server" Text="Order"></asp:Label></td>
                  <td style="height: 16px">
                      <asp:DropDownList ID="ddlOrder" runat="server" Width="103px" Font-Names="Times New Roman" Font-Size="9pt">
                      </asp:DropDownList></td>   
              </tr>
              <tr>
                  <td style="width: 22px; height: 16px">
                  </td>
                  <td style="width: 103px; height: 16px">
                  <asp:Label ID="lblLinkType" runat="server" Text="Link type"></asp:Label>
                  </td>
                  <td style="height: 16px">
                  <table style="width: 301px; border-top-style: none; border-right-style: none;
                          border-left-style: none; border-bottom-style: none">
                     <tr>
                     <td style="width: 150px">
                      <asp:RadioButton ID="rdTypeNew" runat="server" Font-Size="9pt" Text="P--Pop up new window" GroupName="rdType" Width="150px" />
                      </td>
                      <td style="width: 103px">
                      <asp:RadioButton ID="rdTypeSelf" runat="server" Font-Size="9pt" Text="L--Self window" GroupName="rdType" Width="103px" />
                      </td>
                    </tr>
                  </table>
                  </td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 16px">
                  </td>
                  <td style="width: 103px; height: 16px">
                  </td>
                  <td style="height: 16px">
                  </td>
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
