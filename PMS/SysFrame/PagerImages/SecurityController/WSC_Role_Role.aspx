<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_SecurityController_WSC_Role_Role" Codebehind="WSC_Role_Role.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Role relation</title>
     <link href="../styles/TreeStyle.css" rel="stylesheet" type="text/css" />  
     <script language="javascript" src="../javascript/FrameMain.js"></script>
</head>
<body>
    <form id="RoleRelation" runat="server">
    <div>
        <table border="0" style="width: 716px">
            <tr>
                <td style="font-size: 1pt; width: 26px; height: 4px">
                </td>
                <td style="font-size: 1pt; height: 4px">
                </td>
            </tr>
            <tr>
                <td style="width: 26px; height: 26px">
                </td>
                <td style="height: 26px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="SteelBlue"
                        Text="Security-->Role-Role Relation setup" Width="280px"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 26px; height: 5px">
                </td>
                <td style="font-size: 1pt; height: 5px">
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 26px; height: 6px">
                </td>
                <td style="font-size: 1pt; height: 6px">
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 26px; height: 1px">
                </td>
                <td style="font-size: 1pt; background-image: url(../images/shadow.gif); width: 675px;
                    height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 26px; height: 18px">
                </td>
                <td style="font-size: 1pt">
                    <table cellpadding="0" cellspacing="0" style="width: 702px">
                        <tr>
                            <td background="../images/bgcolor.gif" style="width: 12px; height: 22px">
                            </td>
                            <td style="width: 48px; height: 22px" background="../images/bgcolor.gif">
                                <asp:Label ID="lblRoleName" runat="server" Font-Size="9pt" Text="Role Name" Width="66px"></asp:Label></td>
                            <td style="font-size: 1pt; width: 119px; height: 15px" background="../images/bgcolor.gif">
                                &nbsp;<asp:DropDownList ID="ddlRole" runat="server" Font-Size="9pt" Width="146px" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" CssClass="DropDownList">
                                </asp:DropDownList></td>
                            <td style="width: 45px; height: 15px" background="../images/bgcolor.gif">
                                <asp:Label ID="Label4" runat="server" Font-Size="9pt" Height="15px" Text="Role" Width="46px"></asp:Label></td>
                            <td style="width: 70px; height: 15px" background="../images/bgcolor.gif">
                                <asp:DropDownList ID="ddlRole2" runat="server" Font-Size="9pt" Width="146px" CssClass="DropDownList">
                                </asp:DropDownList></td>
                            <td style="width: 30px; height: 15px">
                            </td>
                            <td style="width: 68px; height: 15px">
                                <asp:Button ID="btnAdd" runat="server" CssClass="ButtonLong" Height="18px"
                                    OnClick="btnAdd_Click" Text="Add New" Width="77px" /></td>
                            <td style="width: 102px; height: 15px">
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 26px; height: 2px">
                </td>
                <td style="font-size: 1pt; background-image: url(../images/shadow.gif); height: 2px">
                </td>
            </tr>
            <tr>
                <td style="width: 26px; height: 10px">
                </td>
                <td align="left" style="font-size: 1pt">
                    <table align="left" style="width: 98%">
                        <tr>
                            <td style="font-size: 1pt; width: 119px" valign="top">
                            </td>
                            <td style="font-size: 1pt">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 1pt; width: 119px" valign="top">
                                <table cellpadding="0" cellspacing="0" style="width: 196px">
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
                                                        <asp:Button ID="btnExpand" runat="server" CssClass="ButtonLong" Font-Size="9pt" Height="18px"
                                                            OnClick="btnExpand_Click" Text="Expand" Width="77px" /></td>
                                                    <td style="height: 36px">
                                                        <asp:Button ID="btnCollapse" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                                            Height="18px" OnClick="btnCollapse_Click" Text="Collapse" Width="77px" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 36px">
                                                        <asp:Button ID="btnDel" runat="server" CssClass="ButtonLong" Font-Size="9pt" Height="18px"
                                                            OnClick="btnDel_Click" Text="Delete" Width="77px" /></td>
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
                            <td style="font-size: 1pt">
                                &nbsp;<table cellpadding="0" cellspacing="0" style="width: 400px">
                                    <tr>
                                        <td background="../images/1.gif" colspan="3" style="height: 37px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td background="../images/3.gif" colspan="3" style="height: 5px">
                                            <table>
                                                <tr>
                                                    <td colspan="1" rowspan="3" style="width: 14px">
                                                    </td>
                                                    <td colspan="1" rowspan="3">
                                                        <asp:TreeView ID="Tree1" runat="server" Font-Names="宋体" Font-Size="9pt" ImageSet="Arrows"
                                                            onclick="javascript:PostBack();" OnTreeNodeCheckChanged="Tree1_CheckChanged"
                                                            ShowCheckBoxes="All">
                                                            <ParentNodeStyle CssClass="TreeNodeParent" Font-Size="9pt" ForeColor="SteelBlue"
                                                                ImageUrl="../images/WSC_Role.gif" />
                                                            <HoverNodeStyle CssClass="TreeNodeHover" ForeColor="MediumSlateBlue" />
                                                            <RootNodeStyle CssClass="TreeNodeRoot" Font-Size="9pt" ForeColor="SteelBlue" ImageUrl="../images/WSC_Role.gif" />
                                                            <LeafNodeStyle CssClass="TreeNodeLeaf" Font-Size="9pt" ForeColor="DimGray" ImageUrl="~/SysFrame/images/WSC_Role.gif" />
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
                                        <td background="../images/2.gif" colspan="3" style="height: 40px">
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
    
    </div>
    </form>
</body>
</html>
