<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_SecurityController_WSC_Role_Module" Codebehind="WSC_Role_Module.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Role security</title>
   <link href="../styles/TreeStyle.css" rel="stylesheet" type="text/css" />  
   <script language="javascript" src="../JavaScript/ForTreeCheckBox.js"></script>
</head>
<body>
    <form id="ROLE_MODULE" runat="server">
    <div>
     <table border="0" style="width: 760px">
            <tr>
                <td style="width: 30px; height: 4px; font-size: 1pt;">
                </td>
                <td style="height: 4px; font-size: 1pt;">
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 26px"></td>
                <td style="height: 26px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text="Security-->Role security setup"
                        Width="280px" Font-Size="11pt"></asp:Label></td>
            </tr>
         <tr>
             <td style="width: 30px; height: 5px; font-size: 1pt;">
             </td>
             <td style="height: 5px; font-size: 1pt;">
             </td>
         </tr>
         <tr>
             <td style="font-size: 1pt; width: 30px">
             </td>
             <td style="font-size: 1pt;">
             </td>
         </tr>
         <tr>
             <td style="width: 30px; font-size: 1pt;">
             </td>
             <td style="font-size: 1pt;">
                <table style="width: 322px; height: 15px;" background="../images/n3.gif">
                    <tr>
                        <td style="height: 15px; width: 100px;" background="../images/n3.gif">
                            <asp:Label ID="Label3" runat="server" Font-Size="9pt" Text=" Display style" Width="78px" Font-Bold="True" ForeColor="BlueViolet" Height="15px" Font-Names="Times New Roman"></asp:Label></td>
                        <td style="width: 100px; height: 15px; font-size: 1pt;">
                            &nbsp;<asp:RadioButton ID="rdbtnByRole" runat="server" Font-Size="9pt" GroupName="DType" AutoPostBack="True" Checked="True" OnCheckedChanged="rdbtnByRole_CheckedChanged" Text="by Role" Width="80px" ForeColor="White" Height="15px" Font-Bold="True" Font-Names="Times New Roman" /></td>
                        <td style="width: 100px; height: 15px">
                            <asp:RadioButton ID="rdbtnByModule" runat="server" Font-Size="9pt" GroupName="DType" AutoPostBack="True" OnCheckedChanged="rdbtnByModule_CheckedChanged" Text="by Module" Width="85px" ForeColor="White" Height="15px" Font-Bold="True" /></td>
                    </tr>
                </table>
             </td>
         </tr>
            <tr>
                <td style="width: 30px; height: 6px; font-size: 1pt;">
                </td>
                <td style="font-size: 1pt; height: 6px">
                </td>
            </tr>
         <tr>
             <td style="font-size: 1pt; width: 30px; height: 1px;">
             </td>
             <td style="font-size: 1pt; background-image: url(../images/shadow.gif); width: 675px; height: 1px;">
             </td>
         </tr>
            <tr>
                <td style="width: 30px; height: 18px">
                </td>
                <td style="font-size: 1pt;">
                    <table cellpadding="0" cellspacing="0" style="width: 640px">
                        <tr>
                            <td background="../images/bgcolor.gif" style="width: 12px; height: 22px">
                            </td>
                            <td style="height: 22px; width: 48px;" background="../images/bgcolor.gif">
                                <asp:Label ID="lblRoleName" runat="server" Font-Size="9pt" Text="Role Name" Width="66px"></asp:Label></td>
                            <td style="width: 138px; height: 15px; font-size: 1pt;" background="../images/bgcolor.gif">
                                &nbsp;<asp:DropDownList ID="ddlRole" runat="server" Width="145px" Font-Size="9pt" CssClass="DropDownList">
                                </asp:DropDownList></td>
                            <td style="width: 76px; height: 15px" background="../images/bgcolor.gif">
                                <asp:Label ID="lblModuleName" runat="server" Font-Size="9pt" Text="Module Name" Width="82px" Height="15px"></asp:Label></td>
                            <td style="width: 106px; height: 15px" background="../images/bgcolor.gif">
                                <asp:DropDownList ID="ddlModule" runat="server" Width="152px" Font-Size="9pt" CssClass="DropDownList">
                                </asp:DropDownList></td>
                            <td style="width: 32px; height: 15px">
                            </td>
                            <td style="height: 15px; width: 50px;">
                                <asp:Button ID="btnAdd" runat="server" CssClass="ButtonLong" OnClick="btnAdd_Click"
                                    Text="Add" /></td>
                            <td style="height: 15px; width: 12px;">
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; height: 2px; width: 30px;">
                </td>
                <td  style="font-size: 1pt; background-image: url(../images/shadow.gif); height: 2px;">
                </td>
            </tr>
            <tr>
                <td style="width: 30px; height: 10px">
                </td>
                <td align="left" style="font-size: 1pt;">                
                    <table align="left" style="width: 98%">
                        <tr>
                            <td style="font-size: 1pt; width: 119px;" valign="top">
                            </td>
                            <td style="font-size: 1pt;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 119px;font-size: 1pt;" valign="top">
                                <table cellpadding="0" cellspacing="0" style="width: 192px">
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
                                                        <asp:Button ID="btnExpand" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                                            OnClick="btnExpand_Click" Text="Expand" /></td>
                                                    <td style="height: 36px">
                                                        <asp:Button ID="btnCollapse" runat="server" CssClass="ButtonLong" Font-Size="9pt" OnClick="btnCollapse_Click" Text="Collapse" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 36px">
                                                        <asp:Button ID="btnDel" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                                            OnClick="btnDel_Click" Text="Delete" /></td>
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
                            <td valign=top style="font-size: 1pt">
                                <table style="width: 500px;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td background="../images/bg-up_500.gif" colspan="3" style="height: 20px;background-repeat: no-repeat;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td background="../images/bg-middle_500.gif" colspan="3" style="height: 5px">
                                            <table>
                                                <tr>
                                                    <td colspan="1" rowspan="3" style="width: 14px">
                                                    </td>
                                                    <td colspan="1" rowspan="3">
                                  <asp:TreeView ID="Tree1" runat="server" Font-Size="9pt" ImageSet="Arrows" 
                                    ShowCheckBoxes="All" Font-Names="宋体">
                                    <ParentNodeStyle CssClass="TreeNodeParent" Font-Size="9pt" ForeColor="SteelBlue" ImageUrl="../images/WSC_Role.gif" />
                                    <HoverNodeStyle CssClass="TreeNodeHover" ForeColor="MediumSlateBlue" />
                                    <RootNodeStyle CssClass="TreeNodeRoot" Font-Size="9pt" ForeColor="SteelBlue" ImageUrl="../images/WSC_Role.gif" />
                                    <LeafNodeStyle CssClass="TreeNodeLeaf" Font-Size="9pt" ForeColor="DimGray" ImageUrl="../images/WSC_Module.gif" />
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
                                        <td background="../images/bg-down_500.gif" colspan="3" style="height: 22px;background-repeat: no-repeat;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
            <tr>
                <td style="width: 119px; height: 4px;">
                </td>
                <td style="font-size: 1pt; width: 667px; height: 4px;">
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
