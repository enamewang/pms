<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameHeaderLink" Codebehind="FrameHeaderLink.aspx.cs" %>

<%@ Register Src="../GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- 
  ************************************************************************************************
  *********Created by Anson Lin on 18-Jan-2006                                           *********
  *********                       .                                                      *********
  ************************************************************************************************
-->
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">  
    <title>Header menu link</title>
     <base target=_self></base>
     <script language="javascript" src="../javascript/FrameMain.js"></script>
     
    <script language="javascript" type="text/javascript">
         
     function AddNewConfig()
      {             
          var strFeatures = "dialogWidth=445px;dialogHeight=235px;center=yes;help=no;status=no;scroll=no";
          var strUrl = "FrameHeaderLinkEdit.aspx?ACTION=NEW&LINK_NAME=";
          var strR = showModalDialog(strUrl,'_blank',strFeatures);
          if(strR=="SAVE")
             document.location.href = "FrameHeaderLink.aspx";
      }
               
    </script>
    
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="HeaderLink" runat="server">
    <div>
        <table border="0" style="width: 762px;">
            <tr>
                <td style="height: 22px">
                </td>
                <td style="width: 675px; height: 22px">
                </td>
            </tr>
            <tr>
                <td style="height: 26px">
                </td>
                <td style="width: 675px; height: 26px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text="Header menu link "
                        Width="154px" Font-Size="11pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td align="left" style="font-size: 1pt; width: 675px; height: 10px">
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
                <td style="width: 675px; font-size: 1pt;">
                    <anthem:GridView ID="grd" CssClass="DIVGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Black"
                        GridLines="Horizontal" OnRowDataBound="grd_RowDataBound" Width="720px" EmptyDataText="No data.">
                        <emptydatarowstyle backcolor="Lightyellow" forecolor="Black"/>
                        <PagerSettings Position="TopAndBottom" Visible="False" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:checkAll()" type="checkbox" />
                                </HeaderTemplate>
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDel" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemStyle Width="120px" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLinkName" runat="server" Font-Names="SimSun" Font-Size="9pt"
                                        Target="_blank" Width="100%"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LADDR" HeaderText="Link Address" ReadOnly="True">
                                <ItemStyle Width="280px" Wrap="True" />
                                <HeaderStyle Width="280px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LDESC" HeaderText="Description">
                                <ItemStyle Width="160px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LTYPE" HeaderText="Link type">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="IS_SYS" HeaderText="Sys.Protected" ReadOnly="True">
                                <ItemStyle Width="82px" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle ForeColor="DimGray" BackColor="lightyellow" Font-Size="9pt" BorderStyle="None" BorderWidth="0px" />
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
                <td style="font-size: 1pt; width: 675px; height: 5px">
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
                <td style="width: 675px; height: 3px">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
