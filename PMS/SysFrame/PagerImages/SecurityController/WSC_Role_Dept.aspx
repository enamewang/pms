<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_SecurityController_WSC_Role_Dept" Codebehind="WSC_Role_Dept.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Role-Dept Security</title>
    <link href="../styles/TreeStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../javascript/ForTreeCheckBox.js"></script>
</head>
<body>
    <form id="RoleDept" runat="server">
        <table border="0" style="width: 760px">
            <tr>
                <td style="font-size: 1pt; width: 30px; height: 4px">
                </td>
                <td style="font-size: 1pt; height: 4px; width: 710px;">
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 26px">
                </td>
                <td style="height: 26px; width: 710px;">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="SteelBlue"
                        Text="Security-->Role-Dept setup" Width="280px"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 30px; height: 5px">
                </td>
                <td style="font-size: 1pt; height: 5px; width: 710px;">
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 30px">
                </td>
                <td style="font-size: 1pt; width: 710px;">
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 30px">
                </td>
                <td style="font-size: 1pt; width: 710px;">
                    <table background="../images/n5.gif" style="width: 360px; height: 15px">
                        <tr>
                            <td style="width: 100px; height: 15px">
                                <asp:Label ID="lblDispStyle" runat="server" Font-Bold="True" Font-Names="Times New Roman"
                                    Font-Size="9pt" ForeColor="Yellow" Height="15px" Text=" Display style" Width="78px"></asp:Label></td>
                            <td style="font-size: 1pt; width: 100px; height: 15px">
                                <asp:RadioButton ID="rdbtnByRole" runat="server" AutoPostBack="True" Checked="True"
                                    Font-Bold="True" Font-Names="Times New Roman" Font-Size="9pt" ForeColor="White"
                                    GroupName="DType" Height="15px" OnCheckedChanged="rdbtnByRole_CheckedChanged"
                                    Text="by Role" Width="80px" /></td>
                            <td style="width: 120px; height: 15px">
                                <asp:RadioButton ID="rdbtnByDept" runat="server" AutoPostBack="True" Font-Bold="True" 
                                    Font-Size="9pt" Font-Names="Times New Roman" ForeColor="White" GroupName="DType" Height="15px" OnCheckedChanged="rdbtnByModule_CheckedChanged"
                                    Text="by Department" Width="116px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 30px; height: 6px">
                </td>
                <td style="font-size: 1pt; height: 6px; width: 710px;">
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 30px; height: 1px">
                </td>
                <td style="font-size: 1pt; background-image: url(../images/shadow.gif); width: 710px;
                    height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 18px">
                </td>
                <td style="font-size: 1pt; width: 710px;">
                    <table style="width: 568px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td background="../images/bgcolor.gif" style="width: 12px; height: 22px">
                            </td>
                            <td style="width: 48px; height: 22px" background="../images/bgcolor.gif">
                                <asp:Label ID="lblRoleName" runat="server" Font-Size="9pt" Text="Role Name" Width="66px"></asp:Label></td>
                            <td style="font-size: 1pt; width: 119px; height: 15px" background="../images/bgcolor.gif">
                                <asp:DropDownList ID="ddlRole" runat="server" Font-Size="9pt" Width="145px" CssClass="DropDownList">
                                </asp:DropDownList></td>
                            <td style="width: 45px; height: 15px" background="../images/bgcolor.gif">
                                <asp:Label ID="lblDept" runat="server" Font-Size="9pt" Height="15px" Text="Deparment"
                                    Width="68px"></asp:Label></td>
                            <td style="width: 66px; height: 15px" background="../images/bgcolor.gif">
                                <asp:DropDownList ID="ddlDept" runat="server" Font-Size="9pt" Width="80px"
                                    AutoPostBack="false" CssClass="DropDownList">
                                </asp:DropDownList></td>
                            <td style="width: 12px; height: 15px">
                                </td>
                            <td style="width: 11px; height: 15px">
                            </td>
                            <td style="width: 40px; height: 15px">
                                <asp:Button ID="btnAdd" runat="server" CssClass="ButtonLong" Font-Size="8pt" OnClick="btnAdd_Click"
                                    Text="Add" /></td>
                            <td style="width: 7px; height: 15px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 30px; height: 2px">
                </td>
                <td style="font-size: 1pt; background-image: url(../images/shadow.gif); height: 2px;
                    width: 710px;">
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 10px">
                </td>
                <td align="left" style="font-size: 1pt; width: 710px;">
                    <table align="left" style="width: 98%">
                        <tr>
                            <td style="font-size: 1pt; width: 119px" valign="top">
                            </td>
                            <td style="font-size: 1pt; width: 667px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 1pt; width: 119px" valign="top">
                                <table cellpadding="0" cellspacing="0" style="width: 192px; height: 118px">
                                    <tr>
                                        <td background="../images/box-line-tl2.gif" colspan="2" style="font-size: 9pt; width: 12px;
                                            color: darkslategray; background-repeat: no-repeat; height: 18px">
                                        </td>
                                        <td background="../images/box-line-tr2.gif" style="width: 56px; background-repeat: no-repeat;
                                            height: 18px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td background="../images/box-line.jpg" style="width: 22px; height: 73px">
                                        </td>
                                        <td style="width: 162px; height: 73px">
                                            &nbsp;<table style="width: 136px; height: 46px">
                                                <tr>
                                                    <td style="width: 36px; height: 36px">
                                                        <asp:Button ID="btnExpand" runat="server" CssClass="ButtonLong" Font-Size="9pt" OnClick="btnExpand_Click"
                                                            Text="Expand" /></td>
                                                    <td style="height: 36px">
                                                        <asp:Button ID="btnCollapse" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                                            OnClick="btnCollapse_Click" Text="Collapse" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 36px">
                                                        <asp:Button ID="btnDel" runat="server" CssClass="ButtonLong" Font-Size="9pt" OnClick="btnDel_Click"
                                                            Text="Delete" /></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </td>
                                        <td background="../images/box-line-tr2_NoConner.gif" style="width: 56px; height: 73px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td background="../images/box-line-bl.gif" colspan="2" style="background-repeat: no-repeat;
                                            height: 13px">
                                        </td>
                                        <td background="../images/box-line-br.gif" style="width: 56px; background-repeat: no-repeat;
                                            height: 13px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign=top align=left style="font-size: 1pt; width: 668px;">
                               <table cellpadding="0" cellspacing="0" style="width: 500px;">
                                    <tr>
                                        <td background="../images/bg-up_500.gif" colspan="3" style="height: 22px; background-repeat: no-repeat;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td background="../images/bg-middle_500.gif" colspan="3" style="height: 5px">
                                            <table>
                                                <tr>
                                                    <td colspan="1" rowspan="3" style="width: 14px">
                                                    </td>
                                                    <td colspan="1" rowspan="3">
                                                        <asp:TreeView ID="Tree1" runat="server" Font-Names="宋体" Font-Size="9pt" ImageSet="Arrows"
                                                            ShowCheckBoxes="All">
                                                            <ParentNodeStyle CssClass="TreeNodeParent" Font-Size="9pt" ForeColor="SteelBlue"
                                                                ImageUrl="../images/WSC_Role.gif" />
                                                            <HoverNodeStyle CssClass="TreeNodeHover" ForeColor="MediumSlateBlue" />
                                                            <RootNodeStyle CssClass="TreeNodeRoot" Font-Size="9pt" ForeColor="SteelBlue" ImageUrl="../images/WSC_Role.gif" />
                                                            <LeafNodeStyle CssClass="TreeNodeLeaf" Font-Size="9pt" ForeColor="DimGray" ImageUrl="../images/WSC_Dept.gif" />
                                                            <NodeStyle NodeSpacing="4px" />
                                                        </asp:TreeView>
                                                    </td>
                                                    <td colspan="3" rowspan="3">
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td background="../images/bg-down_500.gif" colspan="3" style="height: 22px; background-repeat: no-repeat;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 119px; height: 4px">
                            </td>
                            <td style="font-size: 1pt; width: 667px; height: 4px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
