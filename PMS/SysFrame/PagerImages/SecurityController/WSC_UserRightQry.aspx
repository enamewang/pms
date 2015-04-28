<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_SecurityController_WSC_Security_Qry" Codebehind="WSC_UserRightQry.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Security Query</title>
    <link href="../styles/TreeStyle.css" rel="stylesheet" type="text/css" />  
    <script language="javascript" src="../javascript/FrameMain.js"></script>
</head>
<body>
    <form id="SecurityQuery" runat="server">
    <div>
        
           <table border="0" style="width: 740px">
                    <tr>
                        <td style="font-size: 1pt; width: 30px; height: 4px">
                        </td>
                        <td style="font-size: 1pt; height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px; height: 26px">
                        </td>
                        <td style="height: 26px">
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="SteelBlue"
                                Text="Security-->Security Query" Width="280px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-size: 1pt; width: 30px; height: 5px">
                        </td>
                        <td style="font-size: 1pt; height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 1pt; width: 30px">
                        </td>
                        <td style="font-size: 1pt">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 1pt; width: 30px; height: 6px">
                        </td>
                        <td style="font-size: 1pt; height: 6px">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 1pt; width: 30px; height: 1px">
                        </td>
                        <td style="font-size: 1pt; width: 675px; height: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px; height: 18px">
                        </td>
                        <td style="font-size: 1pt">
                            <table style="width: 702px">
                                <tr>
                                    <td style="width: 48px; height: 22px">
                                        <asp:Label ID="lblDept" runat="server" Font-Size="9pt" Height="15px" Text="Department" Width="56px"></asp:Label></td>
                                    <td style="font-size: 1pt; width: 100px; height: 15px">
                                        &nbsp;<asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" Font-Size="9pt"
                                            OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" Width="80px">
                                        </asp:DropDownList></td>
                                    <td style="width: 45px; height: 15px">
                                        <asp:Label ID="lblUserName" runat="server" Font-Size="9pt" Height="15px" Text="User Name" Width="66px"></asp:Label></td>
                                    <td style="width: 75px; height: 15px">
                                        <asp:DropDownList ID="ddlUser" runat="server" Font-Size="9pt" Width="145px">
                                        </asp:DropDownList></td>
                                    <td style="width: 49px; height: 15px">
                                        <asp:Button ID="btnQry" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                             Text="Query" OnClick="btnQry_Click" ForeColor="RoyalBlue" /></td>
                                    <td style="width: 141px; height: 15px">
                                        <asp:Button ID="btnQryAll" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                            Text="Query All" OnClick="btnQryAll_Click" ForeColor="DodgerBlue" /></td>
                                    <td style="width: 12px; height: 15px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 1pt; width: 30px; height: 1px">
                        </td>
                        <td style="font-size: 1pt; background-image: url(../images/DotLine.gif); height: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px; height: 10px">
                        </td>
                        <td align="left" style="font-size: 1pt">
                            <table align="left" style="width: 98%">
                                <tr>
                                    <td style="font-size: 1pt; width: 230px; height: 4px" valign="top">
                                    </td>
                                    <td style="font-size: 1pt; height: 4px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 1pt; width: 230px; height: 200px;" valign="top">
                                                                              
                                        <table>
                                            <tr>
                                                <td style="width: 132px">
                                                    <table cellpadding="0" cellspacing="0" style="width: 188px; height: 128px">
                                                        <tr>
                                                            <td background="../images/box-line-tl2.gif" colspan=2 style="width: 12px; background-repeat: no-repeat;
                                                    height: 18px; font-size: 9pt; color: darkslategray;">
                                                            </td>
                                                            <td background="../images/box-line-tr2.gif"  style="width: 26px; background-repeat: no-repeat;
                                                    height: 18px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td background="../images/box-line.jpg" style="width: 26px; height: 96px;">
                                                            </td>
                                                            <td style="width: 162px; height: 96px">
                                                                &nbsp;<table style="width: 136px; height: 46px">
                                                                    <tr>
                                                                        <td style="width: 36px; height: 36px;">
                                                    <asp:Button ID="btnExpand" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                                        OnClick="btnExpand_Click" Text="Expand" ToolTip="Expand all nodes" /></td>
                                                                        <td style="height: 36px">
                                                    <asp:Button ID="btnCollapse" runat="server" CssClass="ButtonLong" Font-Size="9pt" OnClick="btnCollapse_Click" Text="Collapse" ToolTip="Collapse all nodes" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 36px">
                                                    <asp:Button ID="btnDel" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                                        OnClick="btnDel_Click" Text="Delete" ToolTip="Delete the user account including its security right setup." /></td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <br />
                                                            </td>
                                                            <td  background="../images/box-line-tr2_NoConner.gif" style="width: 26px; height: 96px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td background="../images/box-line-bl.gif" colspan=2 style="height: 13px;background-repeat: no-repeat;">
                                                            </td>
                                                            <td background="../images/box-line-br.gif" style="width: 26px; height: 13px;background-repeat: no-repeat">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    
                                                </td>
                                            </tr>
                                            <tr><td style="height: 18px"></td></tr>
                                            <tr>
                                                <td style="width: 131px; height: 27px">
                                                    <table cellpadding="0" cellspacing="0" style="width: 188px;">
                                                        <tr>
                                                            <td background="../images/box-line-tl2.gif" colspan=2 style="width: 12px; background-repeat: no-repeat;
                                                    height: 18px; font-size: 1pt; color: darkslategray;">
                                                    <table style="width: 160px">
                                                       <tr>
                                                           <td>
                                                           </td>
                                                        <td><asp:Label ID="lblCardinality" Font-Size="12px" Text="Cardinality" runat=server Font-Names="Times New Roman" Width="106px" Font-Bold="False" Font-Italic="False"></asp:Label></td>
                                                       </tr>
                                                    </table>
                                                    </td>
                                                        <td background="../images/box-line-tr2.gif"  style="width: 26px; background-repeat: no-repeat;
                                                    height: 18px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td background="../images/box-line.jpg" style="width: 26px; height: 106px;">
                                                        </td>
                                                        <td style="width: 162px; height: 106px">
                                                            &nbsp;<table style="width: 136px;">
                                                                <tr>
                                                                    <td style="width: 36px">
                                                                        <img src="../images/WSC_User.gif" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblUser" runat="server" Font-Size="9pt" ForeColor="SteelBlue" Text="User"
                                                                            Width="66px"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 36px; background-image: url(../images/DotLine.gif); height: 1px;">
                                                                    </td>
                                                                    <td style="background-image: url(../images/DotLine.gif); height: 1px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 36px">
                                                                        <img src="../images/WSC_Role.gif" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblRole" runat="server" Font-Size="9pt" ForeColor="SteelBlue" Text="Role"
                                                                            Width="66px"></asp:Label></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td style="width: 36px; background-image: url(../images/DotLine.gif); height: 1px;">
                                                                    </td>
                                                                    <td style="background-image: url(../images/DotLine.gif); height: 1px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 36px">
                                                                        <img src="../images/WSC_Module.gif" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblMod" runat="server" Font-Size="9pt" ForeColor="SteelBlue" Text="Module"
                                                                            Width="66px"></asp:Label></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td style="width: 36px; background-image: url(../images/DotLine.gif); height: 1px;">
                                                                    </td>
                                                                    <td style="background-image: url(../images/DotLine.gif); height: 1px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 36px">
                                                                        <img src="../images/WSC_Dept.gif" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblDept1" runat="server" Font-Size="9pt" ForeColor="SteelBlue" Text="Department"
                                                                            Width="66px"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 36px; background-image: url(../images/DotLine.gif); height: 1px;">
                                                                    </td>
                                                                    <td style="background-image: url(../images/DotLine.gif); height: 1px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                        <td  background="../images/box-line-tr2_NoConner.gif" style="width: 26px; height: 106px;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td background="../images/box-line-bl.gif" colspan=2 style="height: 13px;background-repeat: no-repeat;">
                                                        </td>
                                                        <td background="../images/box-line-br.gif" style="width: 26px; height: 13px;background-repeat: no-repeat">
                                                        </td>
                                                    </tr>
                                                </table>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                    <td style="font-size: 1pt;" valign="top">
                                        &nbsp;<table cellpadding="0" cellspacing="0" style="width: 438px;">
                                           
                                            <tr>
                                                <td background="../images/box-line-tl2.gif" colspan=2 style="width: 12px; background-repeat: no-repeat;
                                                    height: 22px">
                                                </td>
                                                
                                                <td background="../images/box-line-tr2.gif"  style="width: 15px; background-repeat: no-repeat;
                                                    height: 22px">
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td background="../images/box-line.jpg" style="width: 12px;">
                                                </td>
                                                <td>
                                                <!--
                                                  <div id="divLoading" style="display: none">
                                                       <table>
                                                           <tr> <td height=22 style="font-size: 9pt;"> <span>Data loading...</span> </td> </tr>
                                                           <tr> <td> <img src="../images/Loading.gif" /> </td> </tr>
                         
                                                       </table>
                                                  </div>
                                                  -->
                                                  <div id="TreeMenu" style="display: block">
                                                    <asp:TreeView ID="Tree1" runat="server" Font-Names="宋体" Font-Size="9pt" ImageSet="Arrows"
                                                        ShowCheckBoxes="All" >
                                                        <ParentNodeStyle CssClass="TreeNodeParent" Font-Size="9pt" ForeColor="SteelBlue"
                                                            ImageUrl="../images/WSC_User.gif" />
                                                        <HoverNodeStyle CssClass="TreeNodeHover" ForeColor="MediumSlateBlue" />
                                                        <RootNodeStyle CssClass="TreeNodeRoot" Font-Size="9pt" ForeColor="SteelBlue" ImageUrl="../images/WSC_User.gif" />
                                                        <LeafNodeStyle CssClass="TreeNodeLeaf" Font-Size="9pt" ForeColor="DimGray" ImageUrl="../images/WSC_Module.gif" />
                                                        <NodeStyle NodeSpacing="4px" />
                                                    </asp:TreeView>
                                                  </div>
                                                </td>
                                                <td  background="../images/box-line-tr2_NoConner.gif" style="width: 15px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td background="../images/box-line-bl.gif" colspan=2 style="height: 12px;background-repeat: no-repeat;">
                                                </td>
                                                
                                                <td background="../images/box-line-br.gif" style="width: 15px; height: 12px;background-repeat: no-repeat">
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 230px; height: 4px">
                                    </td>
                                    <td style="font-size: 1pt; width: 667px; height: 4px">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
             
    </div>
        &nbsp;&nbsp;
    </form>
</body>
</html>
