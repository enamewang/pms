<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaintainUser.aspx.cs" Inherits="PMS.PMS.Maintain.MaintainUser" %>

<%@ Register Src="~/SysFrame/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MainTain User</title>

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <base target="_self" />

    <script type="text/javascript">
        //获取页面大小和窗口大小
        function getPageSize() {
            var scrW, scrH;
            if (window.innerHeight && window.scrollMaxY) {
                // Mozilla
                scrW = window.innerWidth + window.scrollMaxX;
                scrH = window.innerHeight + window.scrollMaxY;
            } else if (document.body.scrollHeight > document.body.offsetHeight) {
                // all but IE Mac
                scrW = document.body.scrollWidth;
                scrH = document.body.scrollHeight;
            } else if (document.body) { // IE Mac
                scrW = document.body.offsetWidth;
                scrH = document.body.offsetHeight;
            }
            var winW, winH;
            if (window.innerHeight) { // all except IE
                winW = window.innerWidth;
                winH = window.innerHeight;
            } else if (document.documentElement
                && document.documentElement.clientHeight) {
                // IE 6 Strict Mode
                winW = document.documentElement.clientWidth;
                winH = document.documentElement.clientHeight;
            } else if (document.body) { // other
                winW = document.body.clientWidth;
                winH = document.body.clientHeight;
            }
            // for small pages with total size less than the viewport
            var pageW = (scrW < winW) ? winW : scrW;
            var pageH = (scrH < winH) ? winH : scrH;
            return { pageWidth: pageW, pageHeight: pageH, winWidth: winW, winHeight: winH };
        }

        Number.prototype.NaN0 = function() { return isNaN(this) ? 0 : this; }



        function OpenModalDialogWindow() {
            var url = "AddUser.aspx";
            var features = "dialogWidth=750px;dialogHeight=250px;center=yes;help=no;status=no;scroll=yes";
            var returnValue = window.showModalDialog(url, "_blank", features);

           document.getElementById("ButtonInquiry").click();
//            //window.location.reload();  
//            if (returnValue == true) {
//                return true;
//            }

//            return false;
        }



    </script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 145px;
        }
        .style3
        {
            width: 279px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerInquiry" runat="server" EnablePartialRendering="true"
        ScriptMode="Debug">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="updatePanelPage" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="ButtonInquiry" />
        </Triggers>
        <ContentTemplate>
            <div>
                <table>
                    <tr>
                        <td class="style2" align="right">
                            Department:
                        </td>
                        <td class="style3">
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownListDepartment" runat="server" Height="20px" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Emp No:
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="TextBoxEmpNo" runat="server" Height="18px" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" align="right">
                            English Name:
                        </td>
                        <td class="style3">
                            &nbsp;&nbsp;
                            <asp:TextBox ID="TextBoxEnglishName" runat="server" Height="18px" Width="195px"></asp:TextBox>
                        </td>
                        <td align="right">
                            Extention:
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="TextBoxextention" runat="server" Width="200px" Height="18px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" align="right">
                            NT Domain:
                        </td>
                        <td class="style3">
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownListNTDomain" runat="server" Height="20px" Width="200px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>QCS</asp:ListItem>
                                <asp:ListItem>Qisda</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Role:
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="TextBoxRole" runat="server" Width="200px" Height="18px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" align="right">
                            Domain:
                        </td>
                        <td class="style3">
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownListDomain" runat="server" Height="20px" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" align="right">
                            <asp:Button ID="ButtonInquiry" runat="server" Text="Inquiry" Height="27px" Width="70px"
                                OnClick="ButtonInquiry_Click" />
                            &nbsp;&nbsp; &nbsp;&nbsp;
                            <asp:Button ID="ButtonAdd" runat="server" Text="Add" Height="27px" Width="70px" OnClientClick="OpenModalDialogWindow();"/>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table id="Table_Visable" runat="server">
                    <tr>
                        <td>
                            <uc1:GridViewPager ID="GridViewPager1" Init_Grid_ID="grdViewData" OnBindGrid="BindGrid"
                                runat="server" SetPagerButtonImageStyle="Default" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <anthem:GridView ID="grdViewData" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                EmptyDataText="No data." PageSize="10" Width="100%" OnRowDeleting="grdViewData_RowDeleting"
                                OnRowCancelingEdit="grdViewData_RowCancelingEdit" OnRowCommand="grdViewData_RowCommand"
                                OnRowDataBound="grdViewData_RowDataBound" 
                                OnRowEditing="grdViewData_RowEditing" onrowupdating="grdViewData_RowUpdating">
                                <PagerSettings Visible="False" />
                                <AlternatingRowStyle BackColor="#F5FAFA" />
                                <RowStyle BackColor="White" />
                                <EmptyDataRowStyle BackColor="White" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="bg_GridHeader" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif" ID="btnModify"
                                                CommandName="Edit" ToolTip="Edit" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                Height="16px" Width="16px" />
                                            <asp:Label ID="Label1" runat="server" Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/update.gif" ID="btnSave"
                                                CommandName="Update" ToolTip="Save" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                OnClientClick="return confirm('Are you sure save. Y/N?');" Height="16px" Width="16px" />
                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/undo.gif" ID="ibtnCancel"
                                                CommandName="Cancel" ToolTip="Cancel" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                Height="16px" Width="16px" />
                                            <asp:Label ID="Label1" runat="server" Width="40px"></asp:Label>
                                             <asp:Label ID="LabelUpdateid" runat="server" Style="display: none" Text='<%# Eval("Id")%>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/SysFrame/images/ButtonDelete.gif"
                                                HorizontalAlign="Center" CommandName="Delete" ToolTip="Delete" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                OnClientClick="return confirm('Are you sure to delete ?');" Height="16px" Width="16px">
                                            </asp:ImageButton>
                                            <asp:Label ID="Label2" runat="server" Width="45px"></asp:Label>
                                            <asp:Label ID="Labelid" runat="server" Style="display: none" Text='<%# Eval("Id")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp No">
                                        <EditItemTemplate>
                                         <asp:Label ID="lblUserEmployeeNoold" runat="server" Width="100px" Text='<%# Eval("UserEmployeeNo")%>'></asp:Label>
                                           <%-- <asp:TextBox ID="TextBoxEmpNo" runat="server" Text='<%# Eval("UserEmployeeNo")%>'></asp:TextBox>--%>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserEmployeeNo" runat="server" Width="100px" Text='<%# Eval("UserEmployeeNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NT Domain">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownListNTDomain" runat="server">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>QCS</asp:ListItem>
                                                <asp:ListItem>Qisda</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:Label ID="lblNtdomainold" runat="server" Width="100px" Text='<%# Eval("Ntdomain") %>' Style="display: none" ></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNtdomain" runat="server" Width="100px" Text='<%# Eval("Ntdomain") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="English Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxUserName" runat="server" Text='<%# Eval("UserName")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Width="100px" Text='<%# Eval("UserName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mail Address">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxMailAddress" runat="server" Text='<%# Eval("MailAddress")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMailAddress" runat="server" Width="100px" Text='<%# Eval("MailAddress")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Domain">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownListDomain" runat="server">
                                            </asp:DropDownList>
                                              <asp:Label ID="LbDomainid" runat="server" Text='<%# Eval("Domainid")%>' Style="display: none" ></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbDoMainName" runat="server" Text='<%# Eval("DoMainName")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxRole" runat="server" Text='<%# Eval("Role")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbRole" runat="server" Text='<%# Eval("Role")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Extention">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxExtention" runat="server" Text='<%# Eval("Extention")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbExtention" runat="server" Text='<%# Eval("Extention")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownListDepartment" runat="server">
                                            </asp:DropDownList>
                                               <asp:Label ID="LbDepartMentId" runat="server" Text='<%# Eval("Departmentid")%>' Style="display: none" ></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbDepartMentName" runat="server" Text='<%# Eval("DepartMentName")%>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Create User">
                                        <EditItemTemplate>
                                            <asp:Label ID="LbCreateUserEdit" runat="server" Text='<%# Eval("CreateUser")%>' Width="100px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbCreateUser" runat="server" Text='<%# Eval("CreateUser")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Create Date">
                                        <EditItemTemplate>
                                            <asp:Label ID="LbCreateDateCreate" runat="server" Text='<%# Eval("CreateDate")%>'
                                                Width="100px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbCreateDate" runat="server" Text='<%# Eval("CreateDate")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maintain Date">
                                        <EditItemTemplate>
                                            <asp:Label ID="LbMaintainDateMD" runat="server" Text='<%# Eval("MaintainDate")%>'
                                                Width="100px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbMaintainDate" runat="server" Text='<%# Eval("MaintainDate")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maintain User">
                                        <EditItemTemplate>
                                            <asp:Label ID="LbMaintainUserMU" runat="server" Text='<%# Eval("MaintainUser")%>'
                                                Width="100px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbMaintainUser" runat="server" Text='<%# Eval("MaintainUser")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </anthem:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
