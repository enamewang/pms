<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameLeftMenu" Codebehind="FrameLeftMenu.aspx.cs" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">  
    <title>Header menu link</title>
    <link href="../styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <script language=javascript>
                    
         function AddMenuData()
          {        
              var strFeatures = "dialogWidth=480px;dialogHeight=300px;center=yes;help=no;status=no";
              var strUrl = "FrameLeftMenuEdit.aspx?ACTION=NEW&NODE_ID=";
              var strR = showModalDialog(strUrl,'_blank',strFeatures);
              
              if(strR=="SAVE")
                 document.location.href = "FrameLeftMenu.aspx";
          }
          
          function EditMenuData(strNod_ID)
          {                      
              var strFeatures = "dialogWidth=480px;dialogHeight=300px;center=yes;help=no;status=no";
              var strUrl = "FrameLeftMenuEdit.aspx?ACTION=EDIT&NODE_ID=" + strNod_ID;
              var strR = showModalDialog(strUrl,'_blank',strFeatures);
             
              if(strR=="SAVE")
                 document.location.href = "FrameLeftMenu.aspx";
          }
      document.close();                          
    </script>
    
    <!--<script type="text/javascript">
        
       function ChangeCheck()
        {            
           var o = window.event.srcElement; 

           if (o.tagName == "INPUT" && o.type == "checkbox") 
           {           
              alert('Name: ' + o.name + '--|--Title: ' + o.title);
                                
              CallServer(o.title, '');
            
           }   
        }
        
       function ReceiveServerData(rValue)
        {
            //alert(rValue);
        }
    </script>-->
    
    
    <link href="../styles/TreeStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../javascript/FrameMain.js"></script>
</head>
<body>
    <form id="LeftMenu" runat="server">
    <div>
        <table border="0" style="width: 650px;">
            <tr>
                <td style="width: 100px; height: 8px; font-size: 1pt;">
                </td>
                <td style="height: 8px; font-size: 1pt;">
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 26px">
                </td>
                <td style="height: 26px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="SteelBlue" Text="Left navigation setup"
                        Width="212px" Font-Size="11pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px; height: 5px; font-size: 1pt;">
                </td>
                <td align="left" style="font-size: 1pt;height: 5px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="font-size: 1pt;" align="left" valign="top">
                    &nbsp;<table cellpadding="0" cellspacing="0" style="width: 500px">
                        <tr>
                            <td background="../images/bg-up_500.gif" colspan="3" valign="bottom" style="height: 22px;background-repeat: no-repeat;"><table style="width: 260px; height: 16px;">
                                <tr>
                                    <td style="font-size: 1pt; width: 8px;">
                                    </td>
                                    <td style="width: 88px; font-size: 1pt;">
                                        &nbsp;<asp:Label ID="lblH" runat="server" Font-Names="Times New Roman" Font-Size="9pt"
                                            ForeColor="SlateBlue" Text="Left navigation" Height=12px Width="168px"></asp:Label></td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td background="../images/bg-middle_500.gif" colspan="3">
                                <table>
                                    <tr>
                                        <td colspan="1" rowspan="3" style="width: 46px">
                                        </td>
                                        <td colspan="1" rowspan="3">
                    <asp:TreeView ID="Tree1" runat="server" Font-Size="9pt" ShowCheckBoxes="All" ImageSet="Arrows" OnTreeNodeCheckChanged="Tree1_CheckChanged" onclick="javascript:PostBack();" >
                        <ParentNodeStyle Font-Size="9pt" ForeColor="SteelBlue" CssClass="TreeNodeParent" />
                        <HoverNodeStyle ForeColor="MediumSlateBlue" CssClass="TreeNodeHover" />
                        <RootNodeStyle Font-Size="9pt" ForeColor="DarkGreen" CssClass="TreeNodeRoot" />
                        <LeafNodeStyle Font-Size="9pt" ForeColor="DimGray" CssClass="TreeNodeLeaf" Font-Names="Times New Roman" />
                        <NodeStyle NodeSpacing="3px" />                        
                    </asp:TreeView>
                                            &nbsp;</td>
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
                <td style="width: 100px; height: 9px; font-size: 1pt;">
                </td>
                <td style="font-size: 1pt; width: 675px; height: 9px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 3px">
                </td>
                <td style="height: 3px">
                    <table style="width: 518px">
                        <tr>
                            <td style="height: 31px">
                            </td>
                            <td style="width: 87px; height: 31px">
                                &nbsp;</td>
                            <td style="width: 105px; height: 31px">
                                <input id="btnAdd" class="ButtonLong" onclick="javascript:AddMenuData();" type="button" value="Add" runat="server" /></td>
                            <td style="height: 31px">
                                <asp:Button ID="btnDel" runat="server" CssClass="ButtonLong" OnClick="btnDel_Click"
                                    Text="Delete" /></td>
                            <td style="height: 31px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
