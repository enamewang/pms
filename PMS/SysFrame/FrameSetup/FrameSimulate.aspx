<%@ Page Language="C#" AutoEventWireup="true" Inherits="FrameSimulate" Codebehind="FrameSimulate.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Simulate</title>
    <link href="../styles/FrameStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="Simulate" runat="server">
       <table border="0" align="center" cellspacing="0" style="height: 266px; width: 374px;">
           <tr>
               <td align="center" style="height: 80px; width: 451px;" valign="top">
               </td>
           </tr>
				<tr>
					<td align="center" valign="top" style="height: 253px; width: 451px;">
						<img src="../images/Welcome.png" /></td>
				</tr>
				<tr>
					<td align="center" style="width: 451px; height: 108px">
						<table width="300" height="67" border="0" cellspacing="0" style="WIDTH: 300px; HEIGHT: 67px">
							<tr valign="middle">
								<td width="80" style="HEIGHT: 22px; font-size: 1pt;" align="center">
                                    <asp:Label ID="lblUserName" runat="server" Font-Size="9pt" ForeColor="#444D6A" Text="User Name"
                                        Width="98px"></asp:Label>&nbsp;</td>
								<td style="HEIGHT: 22px; width: 218px;" align="left">
									<asp:textbox id="txtUserName" runat="server" Columns="15" Width="155px" Height="16px" CssClass="EditTextBox"></asp:textbox>
								</td>
							</tr>
							<tr valign="middle">
								<td width="80" style="height: 38px">
								</td>
								<td style="width: 218px; height: 38px;" align="left">
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="OK" CssClass="ButtonLong" Font-Names="Times New Roman" Font-Size="8pt" /></td>
							</tr>
							<tr align="center" valign="bottom">
								<td colspan="2" style="height: 11px">
									</td>
							</tr>
						</table>
						<div align="right"></div>
					</td>
				</tr>
			</table>
    </form>
</body>
</html>
