<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameLeft" Codebehind="FrameLeft.aspx.cs" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<!-- 
  ************************************************************************************************
  *********Created by Anson Lin on 18-Jan-2006                                           *********
  *********Generate the menu style.                                                      *********
  ************************************************************************************************
-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Left menu tree</title>
   <link href="styles/MenuTreeStyle.css" rel="stylesheet" type="text/css" />
   <meta http-equiv="Page-Enter" content="progid:DXImageTransform.Microsoft.Fade()" />
   <script language="javascript">
       
       function checkState()
			{	
				if(document.readyState=="complete")
				{
				    document.all("divLoading").style.display="NONE";
				    document.all("TreeMenu").style.display="BLOCK";				    				   
					return true;
				}
				else
				{ 
				    document.all("divLoading").style.display="BLOCK";
				    document.all("TreeMenu").style.display="NONE";				    
					window.setTimeout("checkState()",200);
					return false;
				}
			}
	
    </script>

    <style>
        .SC 
        {
            SCROLLBAR-ARROW-COLOR: #7aacee;
            /*SCROLLBAR-ARROW-COLOR: #9e9e9e; */
            SCROLLBAR-FACE-COLOR: #ffffff; 
            SCROLLBAR-HIGHLIGHT-COLOR: #9e9e9e; 
            SCROLLBAR-SHADOW-COLOR: #9e9e9e; 
            SCROLLBAR-3DLIGHT-COLOR: #ffffff;         
            SCROLLBAR-TRACK-COLOR: #eaeaea; 
            SCROLLBAR-DARKSHADOW-COLOR: #eaeaea;
        }
    </style>
</head>
<body class="SC" style="background-color :#EEEEEE;" onload="checkState();">
    <form id="LeftMenu" runat="server">
        <div id="divLoading" style="display: block;">
            <table>
                <tr>
                    <td height="15" style="font-size: 9pt; color: DimGray; width: 79px;">
                        <%--<span>
                            <img src="images/Loading_small.gif" />
                            <img src="images/Loading_2.gif" /></span>--%>
                    </td>
                </tr>
            <%--    <tr>
                    <td style="width: 79px">
                        <img src="images/Loading.gif" visible="false" />
                    </td>
                </tr>--%>
            </table>
        </div>
        <div id="TreeMenu" style="display:none; clip: rect(0px auto auto 0px);">
            <table border="0" style="height: 100%; width: 100%;">
                <%--<tr>
                    <td style="font-size: 1pt; height: 3px;background-image: url(images/bg_Hello.gif);">
                        <table>
                            <tr>
                                <td style="font-size: 1pt">
                                    <asp:Image ID="imgHello" ImageUrl="images/Hello_2.gif" runat="server" /></td>
                                <td style="font-size: 1pt">
                                    <asp:Label ID="lblTitle" runat="server" Font-Bold="False" ForeColor="White" Text="Hello "
                                        Height="16px" Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="font-size: 1pt; height: 1px; background-image: url(images/bg02.gif)">
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="font-size: 1pt; height: 1px;">
                    </td>
                </tr>--%>
                <tr>
                    <td style="font-size: 1pt; height: 90%;" align="left" valign="top">
                        <div id="TreeCell">
                            <asp:TreeView ID="Tree1" runat="server" ExpandDepth="0" >
                                <ParentNodeStyle CssClass="TreeNodeStyle" />
                                <RootNodeStyle CssClass="TreeNodeStyle" />
                                <LeafNodeStyle CssClass="TreeNodeStyle" Font-Italic="False" />
                                <HoverNodeStyle CssClass="TreeNodeHover" />
                            </asp:TreeView>
                        </div>
                    </td>
                </tr>
              <%--  <tr>
                    <td>
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="font-size: 1pt; background-image: url(images/leftnav_divider.gif); height: 1px">
                    </td>
                </tr>--%>
                <%--<tr>
                    <td align="center" valign="bottom">
                        <img src="images/Logo.gif" /></td>
                </tr>--%>
              <%--  <tr>
                    <td align="center" valign="bottom" style="height: 22px">
                        <asp:Label ID="lblCounter" runat="server" Text="访问次数: 1" Font-Size="8pt" ForeColor="DimGray"
                            Font-Names="Goudy Old Style"></asp:Label>
                    </td>
                </tr>--%>
            </table>
        </div>
    </form>
</body>
</html>
