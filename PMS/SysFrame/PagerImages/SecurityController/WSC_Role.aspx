<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_SecurityController_WSC_SystemList" Codebehind="WSC_Role.aspx.cs" %>
<%@ Register Src="../GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Role list</title>   
   <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
   <script language="javascript" src="../javascript/FrameMain.js"></script>
   
   <script language="javascript" type="text/javascript">
      
   </script>
</head>
<body>
    <form id="RoleMaintain" runat="server">
    <div>
        <table border="0">
            <tr>
                <td style="width: 30px; height: 22px">
                </td>
                <td style="width: 658px; height: 22px">
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 26px">
                </td>
                <td style="width: 658px; height: 26px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text="Security-->Role Maintanance"
                        Width="280px" Font-Size="12pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 30px; height: 12px; font-size: 1pt;">
                </td>
                <td style="width: 658px; height: 12px; font-size: 1pt;">
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 17px">
                </td>
                <td style="font-size: 1pt; width: 658px; height: 17px">
                    <table style="width: 633px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td background="../images/bgcolor.gif" style="width: 12px; height: 12px">
                            </td>
                            <td style="height: 12px; width: 58px;" background="../images/bgcolor.gif">
                                <asp:Label ID="lblRoleID" runat="server" Font-Size="9pt" Text="Role ID" Width="56px"></asp:Label></td>
                            <td style="width: 138px; height: 12px; font-size: 1pt;" background="../images/bgcolor.gif">
                                &nbsp;<asp:TextBox ID="txtRoleID" runat="server" Font-Size="9pt" Height="16px" Width="160px" CssClass="EditTextBox"></asp:TextBox></td>
                            <td style="width: 62px; height: 12px" background="../images/bgcolor.gif">
                                <asp:Label ID="lblRoleName" runat="server" Font-Size="9pt" Text="Role Name" Width="63px"></asp:Label></td>
                            <td style="width: 152px; height: 12px" background="../images/bgcolor.gif">
                                <asp:TextBox ID="txtRoleName" runat="server" Font-Size="9pt" Height="16px" Width="160px" CssClass="EditTextBox"></asp:TextBox></td>
                            <td style="width: 16px; height: 12px">
                            </td>
                            <td style="height: 12px; width: 112px;">
                                <anthem:Button ID="btnAdd" runat="server" CssClass="ButtonLong" OnClick="btnAdd_Click"
                                   EnabledDuringCallBack="false" TextDuringCallBack="Working..."  Text="Add" PreCallBackFunction="ShowLoading" PostCallBackFunction="RemoveLoading"/></td>
                            <td style="height: 12px; width: 3px;">
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 12px; font-size: 1pt;">
                </td>
                <td style="font-size: 1pt; width: 658px; height: 12px">
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 10px; font-size: 1pt;">
                </td>
                <td align="left" style="font-size: 1pt; width: 658px; height: 10px">
                    <table align="left" style="width: 720px">
                        <tr>
                            <td style="width: 424px; height: 8px; font-size: 1pt;">
                            </td>
                            <td align="left" style="width: 340px; height: 8px; font-size: 1pt;">                                
                                <uc1:GridViewPager ID="Pager1" Init_Grid_ID="grd" OnBindGrid="BindGrid" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 30px;" valign="top">
                </td>
                <td style="font-size: 1pt; width: 658px;" align="left" valign="top">
                    <anthem:GridView ID="grd"  CssClass="DIVGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Black"
                        GridLines="Horizontal"  OnRowDataBound="grd_RowDataBound" Width="720px" 
                        OnRowEditing="grd_RowEditing" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" 
                        OnRowUpdating="grd_RowUpdating"   EmptyDataText="No data.">
                        <emptydatarowstyle backcolor="Lightyellow" forecolor="Black"/>
                        <PagerSettings Position="TopAndBottom" Visible="False" />                        
                        <FooterStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:BoundField DataField="ROLE_ID" HeaderText="Role ID" ReadOnly="True" >
                                <ItemStyle Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ROLE_NAME" HeaderText="Role Name" >
                                <ItemStyle Width="280px" Wrap="True" />                                
                            </asp:BoundField>
                            <asp:BoundField DataField="IS_SYS" HeaderText="Sys.Protected" ReadOnly="True" >
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <EditItemTemplate>                                    
                                    <anthem:ImageButton ID="ibtnUpdate"  runat="server" CausesValidation=false CommandName="Update" ImageUrl="../images/ButtonSave_1.gif" PreCallBackFunction="ShowLoading('Saving ...')" PostCallBackFunction="RemoveLoading"/> 
                                      &nbsp;&nbsp;                                    
                                    <anthem:ImageButton ID="ibtnCancel"  runat=server CausesValidation=false CommandName="Cancel" ImageUrl="../images/ButtonCancel.gif" PreCallBackFunction="ShowLoading('Cancelling ...')" PostCallBackFunction="RemoveLoading"/> 
                                </EditItemTemplate>
                                <ItemTemplate>                                                                                                          
                                    <anthem:ImageButton ID="ibtnEdit"  runat=server CausesValidation=false CommandName="Edit" ImageUrl="../images/ButtonEdit_1.gif" PreCallBackFunction="ShowLoading('Processing ...')" PostCallBackFunction="RemoveLoading"/>   
                                </ItemTemplate>
                                <ItemStyle  Width="80px"/>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle  Width="36px" />
                                <ItemTemplate>                                    
                                    <anthem:ImageButton ID="ibtnDelete" PreCallBackFunction="ConfirmDel_PreCallBack"  PostCallBackFunction="RemoveLoading" runat=server CausesValidation=false CommandName="Delete" ImageUrl="../images/ButtonDelete.gif"  />      
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <RowStyle Font-Size="9pt" BackColor="lightyellow" ForeColor="DimGray" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                        <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White"
                            HorizontalAlign="Left" CssClass="bg_GridHeader" />
                        <AlternatingRowStyle BackColor="White" Font-Size="9pt" ForeColor="DimGray" />
                        <EditRowStyle Font-Size="9pt" />
                    </anthem:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
