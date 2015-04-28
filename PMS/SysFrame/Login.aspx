<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_Login" Codebehind="Login.aspx.cs" %>

<!--*************************************************************************************************
Author        : Anson.Lin
Date	      : 20-Jan-2006
Description   : 
**************************************************************************************************-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=System.Configuration.ConfigurationSettings.AppSettings["SYSTEM_NAME"]%>
    </title>
    <link href="styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="styles/GlobalStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
     function Login()
     {        
        var e = event.srcElement; 
        var k = event.keyCode; 
        
        if (k == 13 && e.type != "textarea") 
        {   
            if(e.id=="<%=txtUserName.ClientID%>")       
            {
                document.all.<%=txtPassword.ClientID%>.focus();                
            }
            else 
            {   
                if(document.all.<%=txtUserName.ClientID%>.value!=="" && document.all.<%=txtPassword.ClientID%>.value!=="")
                {                
                    document.all.<%=btnLogin.ClientID%>.click();                     
                }
                else
                {
                    document.all.<%=txtPassword.ClientID%>.focus();
                }
            }   
            event.cancelBubble = true; 
            event.returnValue = false;         
        }        
     }
    </script>

</head>
<body style="font-size: 9pt; background-image: url(images/bg_about.jpg)">
    <form id="myLogin" runat="server">
    <div>
        <table style="width: 98%; height: 98%">
            <%--<tr>
                <td align="center" style="height: 190px">
                </td>
            </tr>--%>
            <tr>
                <td align="center" valign="middle">
                    <table class="TableLogin" cellpadding="0">
                        <tr class="BannerBlue">
                        <td class="Width20"></td>
                            <td valign="middle" align="left">
                        <asp:label id="LogoL" runat="server" CssClass="LoginTitle" ></asp:label></td>
                        <td id="HeaderMid" style="width: 40px"></td>
                       <td align="right" style="height:36px;">
                           <img id="imglogoR" src="images/QTYPE_BW_REV.jpg" height="36px"></td>
                       <td class="Width20"></td>
                       </tr>
                       <tr class="BannerGray">
                            <td colspan =4 align=right> 
                            <asp:DropDownList ID="ddlLang" runat="server" CssClass="DropDownListLogLan" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged"
                                AutoPostBack="True" ></asp:DropDownList></td>
                             <td class="Width20"></td>
                       </tr>

                        <tr bgcolor="#e8e8e8">
                           <td colspan=5 style="height: 160px;" valign="top">
                                <div id="TreeMenu" style="vertical-align:top;height:80%;width:100%;">
                                <table style="height:100%;width:100%;" cellpadding="0" cellspacing="10">
                                  <tr>
                                     <td style="width:126px; height:27px;" align=right><asp:Label ID="lblUserName" runat="server" Text="UserName" CssClass="LoginLbl"></asp:Label></td>
                                     <td style="width:152px; height:27px;" align=left><asp:TextBox ID="txtUserName" runat="server" AutoCompleteType="DisplayName"
                                                    CssClass="LoginTextBox" onkeydown="Login();"></asp:TextBox></td>
                                     <td style="width:122px; height:27px;">&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td align=right style="height:27px;"><asp:Label ID="lblPassword" runat="server" Text="Password"  CssClass="LoginLbl"></asp:Label></td>
                                      <td align=left style="height:27px;"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"
                                                    CssClass="LoginTextBox" onkeydown="Login();"></asp:TextBox></td>
                                      <td style="height:27px;" align=left>
                                          <asp:Button ID="btnGetPassword" runat="server" CssClass="ButtonPwd"
                                                                OnClick="btnGetPassword_Click" Text="Forget Password" /></td>
                                  </tr>
                                  <tr>
                                      <td style="height:27px;">&nbsp;</td>
                                      <td style="height:27px;"><asp:Button ID="btnLogin" runat="server" CssClass="LoginButton" OnClick="btnLogin_Click"
                                                                Text="Login" /></td>
                                      <td style="height:27px;">&nbsp;</td>
                                  </tr>
                                </table>
                                </div>
                                 <table style="width:400px;">
                                       <tr>
                                         <td class="LoginLine"></td>
                                       </tr>
                                       <tr>
                                          <td class="LoginFooter"></td>
                                       </tr>
                                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
