<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameParameter" Codebehind="FrameParameter.aspx.cs" %>

<%@ Register Src="../GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parameter setup</title>
    <script language="javascript" type="text/javascript">
                  
         function AddNewConfig()
          {             
              var strFeatures = "dialogWidth=445px;dialogHeight=235px;center=yes;help=no;status=no;scroll=no";
              var strUrl = "FrameParameterEdit.aspx?ACTION=NEW&CFG_NAME=";
              var strR = showModalDialog(strUrl,'_blank',strFeatures);           
              if(strR=="SAVE") 
                 document.location.href = "FrameParameter.aspx";
          }
          
        document.close();          
    </script>

    <script language="javascript" src="../javascript/FrameMain.js"></script>

    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="ParameterS" runat="server">
        <div>
            <table border="0" style="width: 758px;" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 800px; height: 39px; background-repeat: no-repeat;">
                    </td>
                    <td style="width: 670px; height: 39px;">
                    </td>
                    <td style="background-repeat: no-repeat; width: 502px; height: 39px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 800px; height: 26px;">
                    </td>
                    <td style="width: 670px; height: 26px;">
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Text="Parameter setup" Width="130px"
                            Font-Size="11pt" ForeColor="SteelBlue"></asp:Label></td>
                    <td style="width: 502px; height: 26px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 800px; height: 10px;">
                    </td>
                    <td style="width: 670px; height: 10px; font-size: 1pt;" align="left">
                        <table style="width: 700px" align="left">
                            <tr>
                                <td style="height: 22px; width: 339px;">
                                </td>
                                <td style="height: 22px; width: 350px;" align="left">
                                    <uc1:GridViewPager ID="Pager1" runat="server" OnBindGrid="BindGrid" Init_Grid_ID="grd"
                                        SetPagerButtonImageStyle="Default" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="left" style="font-size: 1pt; width: 502px; height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style="width: 800px; font-size: 1pt;">
                    </td>
                    <td align="left" valign="top" style="width: 670px; font-size: 1pt;">
                        <anthem:GridView ID="grd" CssClass="DIVGrid" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Black"
                            GridLines="Horizontal" Width="700px" OnRowDataBound="grd_RowDataBound" AllowPaging="True"
                            EmptyDataText="No data." DataKeyNames="CFG_ID">
                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                            <PagerSettings Position="TopAndBottom" Visible="False" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField>
                                    <headertemplate>
                                    <input id="chkAll" onclick="javascript:checkAll()" type="checkbox" />
                                
</headertemplate>
                                    <itemstyle width="20px" />
                                    <itemtemplate>
                                    <asp:CheckBox ID="chkDel" runat="server" />
                                
</itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <itemstyle width="140px" />
                                    <itemtemplate>
                                    <asp:HyperLink ID="HyperLinkName" Font-Size=9pt  runat="server" Target="_blank" Width="100%" Font-Names="SimSun"></asp:HyperLink>
                                
</itemtemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CFG_VALUE" HeaderText="Value" ReadOnly="True">
                                    <itemstyle width="200px" wrap="True" />
                                    <headerstyle width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CFG_DESC" HeaderText="Description" />
                                <asp:BoundField DataField="IS_SYS" HeaderText="Sys.Protected" ReadOnly="True">
                                    <itemstyle width="82px" />
                                </asp:BoundField>
                            </Columns>
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Left" BorderStyle="None" Font-Bold="True" ForeColor="White"
                                CssClass="bg_GridHeader" />
                            <AlternatingRowStyle BackColor="White" ForeColor="DimGray" />
                            <RowStyle ForeColor="DimGray" BackColor="LightYellow" />
                        </anthem:GridView>
                    </td>
                    <td align="left" style="width: 502px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 800px; height: 20px;" valign="top">
                    </td>
                    <td align="left" style="width: 670px; height: 20px;" valign="top">
                    </td>
                    <td align="left" style="width: 502px; height: 20px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td style="width: 800px; height: 6px;">
                    </td>
                    <td style="font-size: 1pt; width: 670px; height: 6px;">
                        <table style="width: 98%">
                            <tr>
                                <td style="height: 19px; font-size: 1pt;">
                                </td>
                                <td style="width: 436px; height: 19px; font-size: 1pt;">
                                    &nbsp;</td>
                                <td style="width: 86px; height: 19px; font-size: 1pt;">
                                    <input id="btnAdd" onclick="javascript:AddNewConfig();" class="ButtonLong" type="button"
                                        value="Add" style="font-size: 9pt;" runat="server" /></td>
                                <td style="height: 19px; font-size: 1pt;">
                                    <anthem:Button ID="btnDel" runat="server" Text="Delete" CssClass="ButtonLong" OnClick="btnDel_Click"
                                        Font-Size="9pt" EnabledDuringCallBack="false" TextDuringCallBack="Working..."
                                        PreCallBackFunction="ConfirmDelHasChk_PreCallBack"  PostCallBackFunction="RemoveLoading"/></td>
                            </tr>
                        </table>
                    </td>
                    <td style="font-size: 1pt; width: 502px; height: 6px;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 800px; background-repeat: no-repeat; height: 20px;">
                    </td>
                    <td style="width: 670px; height: 20px;">
                    </td>
                    <td style="width: 502px; background-repeat: no-repeat; height: 20px;">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
