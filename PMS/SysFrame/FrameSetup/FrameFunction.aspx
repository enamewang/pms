<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameSetup_FrameFunction" Codebehind="FrameFunction.aspx.cs" %>

<%@ Register Src="../GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Function</title>
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../javascript/FrameMain.js"></script>

    <script language="javascript" type="text/javascript">
           
    </script>

</head>
<body>
    <form id="ModuleFunction" runat="server">
        <div>
            <table border="0" style="width: 828px;">
                <tr>
                    <td style="width: 20px; height: 22px">
                    </td>
                    <td style="width: 675px; height: 22px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px; height: 26px">
                    </td>
                    <td style="width: 676px; height: 26px">
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="SteelBlue"
                            Text="Module's Function setup" Width="280px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 20px; height: 18px">
                    </td>
                    <td style="width: 675px; height: 18px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px; height: 18px">
                    </td>
                    <td style="width: 675px; height: 18px; font-size: 1pt;">
                        <table style="width: 760px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td background="../images/bgcolor.gif" style="width: 12px; height: 22px">
                                </td>
                                <td style="width: 78px; height: 22px" background="../images/bgcolor.gif">
                                    &nbsp;<asp:Label ID="lblFuncID" runat="server" Text="Function ID" Width="66px" Font-Size="9pt"></asp:Label></td>
                                <td style="width: 100px; height: 22px" background="../images/bgcolor.gif">
                                    <asp:TextBox ID="txtFuncID" runat="server" Height="16px" Width="88px" Font-Size="9pt"
                                        CssClass="EditTextBox"></asp:TextBox></td>
                                <td style="width: 38px; height: 22px" background="../images/bgcolor.gif">
                                    <asp:Label ID="lblName" runat="server" Text="Name" Width="38px" Font-Size="9pt"></asp:Label></td>
                                <td style="width: 100px; height: 22px" background="../images/bgcolor.gif">
                                    <asp:TextBox ID="txtFuncName" runat="server" Height="16px" Width="88px" Font-Size="9pt"
                                        CssClass="EditTextBox"></asp:TextBox></td>
                                <td style="width: 82px; height: 22px" background="../images/bgcolor.gif">
                                    <asp:Label ID="lblParentMod" runat="server" Text="Parent Module" Width="80px" Font-Size="9pt"></asp:Label></td>
                                <td style="width: 106px; height: 22px" background="../images/bgcolor.gif">
                                    <asp:DropDownList ID="ddlPModuleID" runat="server" Font-Names="Times New Roman" Font-Size="9pt"
                                        Width="108px" CssClass="DropDownList">
                                    </asp:DropDownList></td>
                                <td style="width: 36px; height: 22px">
                                </td>
                                <td style="width: 80px; height: 22px">
                                    <anthem:Button ID="btnAdd" PreCallBackFunction="ShowLoading('Working ...')" PostCallBackFunction="RemoveLoading"
                                        runat="server" CssClass="ButtonLong" Font-Size="9pt" Text="Add" TextDuringCallBack="Working..."
                                        OnClick="btnAdd_Click" EnabledDuringCallBack="false" /></td>
                                <td style="height: 22px; width: 80px;">
                                    <anthem:Button ID="btnQry" PreCallBackFunction="ShowLoading('Processing ...')" PostCallBackFunction="RemoveLoading"
                                        runat="server" CssClass="ButtonLong" Font-Size="9pt" Text="Query" TextDuringCallBack="Working..."
                                        OnClick="btnQry_Click" EnabledDuringCallBack="false" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px; height: 16px">
                    </td>
                    <td style="font-size: 1pt; width: 675px; height: 16px">
                        </td>
                </tr>
                <tr>
                    <td style="width: 20px; height: 10px">
                    </td>
                    <td align="left" style="font-size: 1pt; height: 10px">
                        <table align="left" style="width: 98%">
                            <tr>
                                <td style="height: 22px; width: 332px;">
                                    <anthem:Button ID="btnDel" runat="server" CssClass="ButtonLong" Font-Size="9pt" Height="21px"
                                        Width="77px" Text="Delete" OnClick="btnDel_Click" PreCallBackFunction="ConfirmDelHasChk_PreCallBack"
                                        PostCallBackFunction="btn_PostCallBack" TextDuringCallBack="Working..." EnabledDuringCallBack="false" />
                                </td>
                                <td align="left" style="width: 318px; height: 22px">
                                    <uc1:GridViewPager ID="Pager1" runat="server" OnBindGrid="BindGrid" Init_Grid_ID="grd"
                                        SetPagerButtonImageStyle="Default" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 675px; font-size: 1pt;" valign="top">
                        <anthem:GridView ID="grd" CssClass="DIVGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Black" GridLines="Horizontal"
                            OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDataBound="grd_RowDataBound" OnRowDeleting="grd_RowDeleting"
                            OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" Width="780px" EmptyDataText="No data.">
                            <EmptyDataRowStyle BackColor="Lightyellow" ForeColor="Black" />
                            <PagerSettings Position="TopAndBottom" Visible="False" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField>
                                    <itemstyle width="20px" />
                                    <itemtemplate>
                                        <asp:CheckBox ID="chkDel" runat="server" />
                                    </itemtemplate>
                                    <headertemplate>
                                        <input id="chkAll" onclick="javascript:checkAll()" type="checkbox" />
                                    </headertemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MOD_ID" HeaderText="Function ID" ReadOnly="True">
                                    <itemstyle width="260px" wrap="True" />
                                    <headerstyle width="260px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOD_NAME" HeaderText="Name">
                                    <itemstyle width="260px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Parent Module">
                                    <edititemtemplate>
                                        <asp:DropDownList ID="ddlPModID" runat="server">
                                        </asp:DropDownList>
                                    </edititemtemplate>
                                    <itemstyle width="400px" />
                                    <itemtemplate>
                                        <asp:Label ID="lblPModID" runat="server" Text='<%# Bind("MOD_DESC") %>'></asp:Label>
                                        <asp:Label ID="lblPModSep" runat="server" Text='|'></asp:Label>
                                        <asp:Label ID="lblPModName" runat="server" Text='<%# Bind("PMOD_NAME")%>'></asp:Label>
                                    </itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <edititemtemplate>
                                        <anthem:ImageButton ID="ibtnUpdate"  runat="server" CausesValidation="false"
                                            CommandName="Update" ImageUrl="../images/ButtonSave_1.gif"  EnabledDuringCallBack="false" PreCallBackFunction="ShowLoading('Saving ...')" PostCallBackFunction="RemoveLoading"/>&nbsp;
                                        <anthem:ImageButton ID="ibtnCancel"  runat="server" CausesValidation="false"
                                            CommandName="Cancel" ImageUrl="../images/ButtonCancel.gif"  EnabledDuringCallBack="false" PreCallBackFunction="ShowLoading('Cancelling ...')" PostCallBackFunction="RemoveLoading"/>
                                    </edititemtemplate>
                                    <itemtemplate>
                                        <anthem:ImageButton ID="ibtnEdit"  runat="server" CausesValidation="false"
                                            CommandName="Edit" ImageUrl="../images/ButtonEdit_1.gif"  EnabledDuringCallBack="false" PreCallBackFunction="ShowLoading('Processing ...')" PostCallBackFunction="RemoveLoading"/>
                                    </itemtemplate>
                                    <itemstyle width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <itemstyle width="36px" />
                                    <itemtemplate>
                                        <anthem:ImageButton ID="ibtnDelete"  runat="server" CausesValidation="false"
                                            CommandName="Delete" ImageUrl="../images/ButtonDelete.gif"  EnabledDuringCallBack="false"
                                            PreCallBackFunction="ConfirmDel_PreCallBack" PostCallBackFunction="RemoveLoading"/>
                                    </itemtemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                            <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                CssClass="bg_GridHeader" />
                            <AlternatingRowStyle BackColor="White" ForeColor="DimGray" />
                            <RowStyle ForeColor="DimGray" BackColor="lightyellow" />
                        </anthem:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
