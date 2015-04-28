<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameUserMaintain" Codebehind="FrameUserMaintain.aspx.cs" %>
<%@ Register Src="../GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!-- 
  ************************************************************************************************
  *********Created by Lee Chen on 5-jun-2009                                           *********
  *********                       .                                                      *********
  ************************************************************************************************
-->
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">  
    <title>User Maintain</title>
     <base target=_self></base>
     <script language="javascript" src="../javascript/FrameMain.js"></script>
     
    <script language="javascript" type="text/javascript">
         
     function AddNewConfig()
      {             
          var strFeatures = "dialogWidth=500px;dialogHeight=500px;center=yes;help=no;status=no;scroll=no";
          var strUrl = "FrameUserMaintainEdit.aspx?ACTION=NEW&LOGIN_NAME=";
          var strR = showModalDialog(strUrl,'_blank',strFeatures);
          if(strR=="SAVE")
             document.location.href = "FrameUserMaintain.aspx";
      }
               
    </script>
    
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="UserMaintain" runat="server">
    <div>
        <table border="0" style="width: 762px;">
            <tr>
                <td style="height: 22px">
                </td>
                <td style="width: 644px; height: 22px">
                </td>
            </tr>
            <tr>
                <td style="height: 26px">
                </td>
                <td style="width: 644px; height: 26px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text="User Maintain"
                        Width="154px" Font-Size="11pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td style="width: 644px; height: 22px">
                </td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td style="width: 644px; height: 22px" align="right">
                    <span><asp:Label ID="lblLoginName" runat="server" ForeColor="SteelBlue" Text="Login Name" Width="99px"></asp:Label></span>
                    <span><asp:TextBox ID="txtLoginName" runat="server" CssClass="EditTextBox" EnableTheming="False"></asp:TextBox></span>
                    <span><asp:Button ID="btnQuery" runat="server" Text="Query" OnClick="btnQuery_Click" Width="79px" CssClass="ButtonLong" /></span><%--<table>
                      <tr>
                         <td style="height: 33px"><asp:Label ID="lblLoginName" runat="server" ForeColor="SteelBlue" Text="LOGIN NAME" Width="99px"></asp:Label></td>
                         <td style="height: 33px"><asp:TextBox ID="txtLoginName" runat="server" CssClass="EditTextBox" EnableTheming="False"></asp:TextBox></td>
                         <td style="width: 63px; height: 33px"><asp:Button ID="btnQuery" runat="server" Text="Query" OnClick="btnQuery_Click" Width="60px" CssClass="ButtonLong" /></td>
                      </tr>
                   </table>--%>
               </td>
           </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td align="left" style="font-size: 1pt; width: 644px; height: 10px">
                    <table align="left" style="width: 720px">
                        <tr>
                            <td style="width: 278px; height: 10px">
                            </td>
                            <td align="left" style="height: 10px; width: 331px;">
                                &nbsp;
                                <uc1:GridViewPager ID="Pager1" Init_Grid_ID="grd" OnBindGrid="BindGrid" runat="server" SetPagerButtonImageStyle="Default"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 644px; font-size: 1pt;">
                    <anthem:GridView ID="grd" CssClass="DIVGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Gray"
                        GridLines="Horizontal" OnRowDataBound="grd_RowDataBound" Width="720px" EmptyDataText="No data." UpdateAfterCallBack="False">
                        <emptydatarowstyle backcolor="LightYellow" forecolor="Black"/>
                        <PagerSettings Position="TopAndBottom" Visible="False" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:checkAll()" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                        <asp:CheckBox id="chkDel" runat="server" __designer:wfdid="w3"></asp:CheckBox> 
                        </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Login Name">
                                <ItemTemplate>
                        <asp:HyperLink id="HyperLinkName" runat="server" Font-Size="9pt" Width="100%" Font-Names="SimSun" __designer:wfdid="w4" Target="_blank"></asp:HyperLink> 
                        </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SITE_CODE" HeaderText="Site" ReadOnly="True">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEPT_CODE" HeaderText="Dept">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MAIL_ACCOUNT" HeaderText="Mail" ReadOnly="True">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ACTIVE" HeaderText="Active">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>                            
                        </Columns>
                        <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None" BorderWidth="0px" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                        <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" CssClass="bg_GridHeader" />
                        <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None" BorderWidth="0px" />
                    </anthem:GridView>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">
                </td>
                <td style="font-size: 1pt; width: 644px; height: 5px">
                    <table style="width: 107%">
                        <tr>
                            <td style="height: 29px">
                            </td>
                            <td style="width: 517px; height: 29px">
                                &nbsp;</td>
                            <td style="width: 105px; height: 29px" valign="bottom">
                                <input id="btnAdd" class="ButtonLong" onclick="javascript:AddNewConfig();" type="button" value="Add" runat="server" /></td>
                            <td style="height: 29px" valign="bottom">
                                <anthem:Button ID="btnDel" runat="server" CssClass="ButtonLong" OnClick="btnDel_Click"
                                    Text="Delete" EnabledDuringCallBack="false" TextDuringCallBack="Working..." PreCallBackFunction="ConfirmDelHasChk_PreCallBack" PostCallBackFunction="RemoveLoading"/></td>
                            <td style="height: 29px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 3px">
                </td>
                <td style="width: 644px; height: 3px">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>