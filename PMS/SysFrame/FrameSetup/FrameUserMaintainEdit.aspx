<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameUserMaintainEdit" Codebehind="FrameUserMaintainEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Maintain Edit</title>
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <base target=_self></base>
    <script language=javascript>
       function closeTop()
       {
           //top.returnValue=document.all("hidAction").value;          //Modify by lee chen on 20091014   
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
          <table style="width: 460px; height: 215px">
              <tr>
                  <td colspan="1" style="width: 22px; height: 22px">
                  </td>
                  <td colspan="2" style="height: 22px">
                      <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text=" User Maintain Edit"
                          Width="209px" Font-Size="11pt"></asp:Label></td>
              </tr>
              <tr>
                  <td style="width: 22px">
                  </td>
                  <td style="width: 352px">
                  </td>
                  <td style="width: 409px">
                  </td>
              </tr>
              <tr>
                  <td style="width: 22px">
                  </td>
                  <td style="width: 352px">
                      <asp:Label ID="lblEmpId" runat="server" Text="Emp Id"></asp:Label></td>
                  <td style="width: 409px">
                      <asp:TextBox ID="txtEmpId" runat="server" Height="12px" Font-Size="9pt" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px">
                  </td>
                  <td style="width: 352px">
                      <asp:Label ID="lblEmpNo" runat="server" Text="Emp No."></asp:Label></td>
                  <td style="width: 409px">
                      <asp:TextBox ID="txtEmpNo" runat="server" Height="12px" Font-Size="9pt" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px">
                  </td>
                  <td style="width: 352px">
                      <asp:Label ID="lblLoginName" runat="server" Text="Login Name"></asp:Label></td>
                  <td style="width: 409px" valign="middle">
                      <asp:TextBox ID="txtLoginName" runat="server" Height="12px" Font-Size="9pt" Width="295px" CssClass="EditTextBox"></asp:TextBox>&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLoginName"
                          ErrorMessage="Login name can not be blank!" Height="1px" Width="4px" Display="Dynamic">*</asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 352px; height: 23px;">
                      <asp:Label ID="lblEmpName" runat="server" Text="Emp Name"></asp:Label></td>
                  <td style="height: 23px; width: 409px;">
                      <asp:TextBox ID="txtEmpName" runat="server" Height="12px" Width="295px" Font-Size="9pt" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 352px; height: 23px">
                      <asp:Label ID="lblMail" runat="server" Text="Mail"></asp:Label></td>
                  <td style="height: 23px; width: 409px;">
                      <asp:TextBox ID="txtMail" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox>&nbsp;
                      <asp:RegularExpressionValidator ID="revMail" runat="server" ControlToValidate="txtMail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="true" Text="*" Height="1px" Width="4px" Display="Dynamic" ErrorMessage="Please input correct mail address!"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMail"
                          Height="1px" Width="4px" Display="Dynamic" ErrorMessage="Mail address can not be blank!">*</asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 352px; height: 23px">
                      <asp:Label ID="lblExtNo" runat="server" Text="Ext No"></asp:Label></td>
                  <td style="height: 23px; width: 409px;">
                      <asp:TextBox ID="txtExtNo" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 352px; height: 23px">
                      <asp:Label ID="lblSite" runat="server" Text="Site"></asp:Label></td>
                  <td style="height: 23px; width: 409px;">
                      <asp:TextBox ID="txtSite" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 352px; height: 23px">
                      <asp:Label ID="lblDept" runat="server" Text="Dept"></asp:Label></td>
                  <td style="height: 23px; width: 409px;">
                      <asp:TextBox ID="txtDept" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 352px; height: 23px">
                      <asp:Label ID="lblDesc" runat="server" Text="Job Desc"></asp:Label></td>
                  <td style="height: 23px; width: 409px;">
                      <asp:TextBox ID="txtDesc" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 352px; height: 23px">
                      <asp:Label ID="lblPwd" runat="server" Text="Password"></asp:Label></td>
                  <td style="height: 23px; width: 409px;">
                      <asp:TextBox ID="txtPwd" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox" TextMode="Password"></asp:TextBox>&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPwd"
                          ErrorMessage="Password can not be blank!" Height="1px" Width="4px" Display="Dynamic">*</asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 23px">
                  </td>
                  <td style="width: 352px; height: 23px">
                      <asp:Label ID="lblDipName" runat="server" Text="Display Name"></asp:Label></td>
                  <td style="height: 23px; width: 409px;">
                      <asp:TextBox ID="txtDipName" runat="server" Font-Size="9pt" Height="12px" Width="295px" CssClass="EditTextBox"></asp:TextBox></td>
              </tr>
              <tr>
                  <td style="width: 22px; height: 11px">
                  </td>
                  <td style="width: 352px; height: 11px;">
                      <asp:Label ID="lblActive" runat="server" Text="Active"></asp:Label></td>
                  <td style="font-size: 1pt; height: 11px; width: 409px;">
                      &nbsp;<table style="width: 301px; border-top-style: none; border-right-style: none;
                          border-left-style: none; border-bottom-style: none">
                          <tr>
                              <td style="width: 150px">
                                  <asp:RadioButton ID="rdActiveY" runat="server" Font-Size="9pt" Text="Y--Active" GroupName="rdActive" Width="150px" /></td>
                              <td style="width: 102px">
                                  <asp:RadioButton ID="rdActiveN" runat="server" Font-Size="9pt" Text="N--Inactive" GroupName="rdActive" Width="102px" /></td>
                          </tr>
                      </table>
                  </td>
              </tr>
              <tr>
                  <td style="width: 304px; height: 8px; font-size: 1pt;">
                  </td>
                  <td style="width: 352px; height: 8px; font-size: 1pt;">
                  </td>
                  <td style="height: 8px; font-size: 1pt; width: 409px;"><!--<asp:RadioButton ID="rdTypeMain" runat="server" Font-Size="9pt" Text="M--Main area of framework" GroupName="rdType" Width="168px" />--></td>
              </tr>
              <tr>
                  <td colspan="1" style="font-size: 1pt; width: 22px">
                  </td>
                  <td colspan="2" style="font-size: 1pt">
                      <table style="width: 379px">
                          <tr>
                              <td style="width: 100px">
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
              <tr>
                  <td colspan="3" style="height: 17px">
                      <asp:ValidationSummary ID="ValSum" runat="server"
                          ShowMessageBox="True" ShowSummary="False" />
                  </td>
              </tr>
          </table>
                  
          
      </div>
    </form>
</body>
</html>

