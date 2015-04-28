<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameSetup_ValidateConfiguration" Codebehind="ValidateConfiguration.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Validate Configuration</title>
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="ValidateConfiguration" runat="server">
    <div>
        <table border="0" style="width: 760px;">
            <tr>
                <td style="width: 22px; height: 22px">
                </td>
                <td style="width: 712px; height: 22px">
                </td>
            </tr>
            <tr>
                <td style="width: 22px; height: 26px">
                </td>
                <td style="width: 712px; height: 26px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="SteelBlue"
                        Text="Validating system configuration" Width="256px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 22px; height: 10px">
                </td>
                <td align="left" style="font-size: 1pt; height: 10px">
                    <table align="left" style="width: 100%">
                        <tr>
                            <td style="font-size: 1pt; width: 60px; height: 22px">
                            </td>
                            <td align="left" style="font-size: 1pt; height: 22px; width: 600px;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="height: 36px">
                                        </td>
                                        <td style="width: 352px; height: 36px">
                                            &nbsp;</td>
                                        <td style="width: 106px; height: 36px">
                                        <anthem:Button ID="btnValidate" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                                 Text="Validate" OnClick="btnValidate_Click" EnabledDuringCallBack="false" TextDuringCallBack="Working..." PreCallBackFunction="ShowLoading" PostCallBackFunction="btn_PostCallBack"/></td>
                                        <td style="height: 36px; width: 100px;">
                                            <anthem:Button ID="btnRepaire" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                                 Text="Repaire" OnClick="btnRepaire_Click"  EnabledDuringCallBack="false" TextDuringCallBack="Working..." PreCallBackFunction="ShowLoading" PostCallBackFunction="btn_PostCallBack"/></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 1pt; width: 60px; height: 22px">
                                <asp:Label ID="lblRTitle" runat="server" Font-Size="10pt" ForeColor="DarkSlateBlue" Text="Result:"
                                    ></asp:Label></td>
                            <td align="left" style="font-size: 1pt; height: 22px; width: 600px;">
                                <anthem:Label ID="lblResult" runat="server" Font-Size="10pt" ForeColor="DarkSlateBlue" Width="100%" UpdateAfterCallBack="False"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td background="../images/DotLine.gif" style="font-size: 1pt; width: 60px; height: 1px">
                            </td>
                            <td align="left" background="../images/DotLine.gif" style="font-size: 1pt; width: 600px;
                                height: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 1pt; width: 60px; height: 10px">
                            </td>
                            <td align="left" style="font-size: 1pt; height: 10px; width: 600px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 22px" valign="top">
                </td>
                <td align="left" style="font-size: 1pt;" valign="top">
                    <anthem:GridView ID="grd" CssClass="DIVGrid" runat="server" BackColor="White" BorderColor="Gray" BorderStyle="Solid"
                        BorderWidth="1px" CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt"
                        ForeColor="Black" GridLines="Horizontal" Width="98%" EmptyDataText="Everything is OK.">
                        <emptydatarowstyle backcolor="Lightyellow" forecolor="Black"/>
                        <PagerSettings Position="TopAndBottom" Visible="False" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <RowStyle BackColor="lightyellow" ForeColor="DimGray" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#5F7FAF" BorderStyle="None" Font-Bold="True" ForeColor="White"
                            HorizontalAlign="Left" CssClass="bg_GridHeader" />
                        <AlternatingRowStyle BackColor="White" ForeColor="DimGray" />
                    </anthem:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 22px; height: 5px">
                </td>
                <td style="font-size: 1pt; height: 5px">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
