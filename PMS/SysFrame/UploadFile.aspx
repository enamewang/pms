<%@ Page Language="C#" AutoEventWireup="True" Inherits="SysFrame_UploadFile" Codebehind="UploadFile.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--
		*******************************************************************************************************
		Created by Anson Lin on 18-Feb-2006
		Description:  Upload local files to server for specified Item No. 
			          The first picture user uploaded is the item's photo, 
	                  user can arrange the file list to reset the photo for item.
		*******************************************************************************************************
		-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Upload file</title>
    <link href="styles/FrameStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="javascript/FrameMain.js"></script>

    <base target="_self"></base>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT" />

    <script language="javascript" type="text/javascript">
         
               
           //Refresh client side
           function RefreshOpener()
           {              
               var strHtml="";
               var strRtnTag="";
               var strItem_Pic="";
	               
	               
	           try {  strItem_Pic = "<%=this.ItemPic%>";    }         catch(e){}	               
               try {  strHtml     = "<%=this.ReturnHtml%>"; }         catch(e){}       
               try {  strRtnCtrl  = "<%=this.ReturnCtrl%>";  }        catch(e){}       
                     	              
               if(strRtnCtrl=="")                  
               {
                   try
				    {
				        window.opener.document.all("nameOfFile").innerHTML = strHtml; 				    
				    }
			       catch(e){}    	
			   }
			   else
			   {
			       try
				    {
				        window.opener.document.all(strRtnCtrl).innerHTML = strHtml; 				    
				    }
			       catch(e){}    	
			   }
			   
			   if(strItem_Pic!="")
			   {
			       try
			        {
			           window.opener.document.all("imgPic").src = strItem_Pic;
			           //window.opener.document.all("imgPic").style.display = "block";
					}  						
				   catch(e){}				
			   }
			   else
			   {
			       try
					{
					   window.opener.document.all("imgPic").src = "<%=WSC.GlobalDefinition.SystemWebPath%>images/nophoto320.png";
					   //window.opener.document.all("imgPic").style.display = "none";     												
					}  						
				   catch(e){}			          
			   }			   
           }
           
           
           
           function closeTop()
	          {		            	           
	            try
	            {	
	               try
	                { 
	                    RefreshOpener();
	                }
	               catch(e){}    
	               	   	              
	               top.close();		               
	            }  
	            catch(e)
	            {
	               //alert(e);
	            }
	          }  	
	          
	          
    </script>

</head>
<body>
    <form id="UploadFile" runat="server">
        <div>
            <table border="0" style="width: 98%;">
                <tr>
                    <td style="width: 16px; height: 20px">
                    </td>
                    <td style="height: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 16px; height: 14px">
                    </td>
                    <td style="height: 14px; font-size: 1pt;">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 78px; height: 22px">
                                    &nbsp;<asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="SteelBlue"
                                        Text="Upload file" Width="122px"></asp:Label></td>
                                <td style="width: 132px; height: 22px">
                                </td>
                                <td style="width: 100px; height: 22px">
                                    &nbsp;</td>
                                <td style="width: 80px; height: 22px">
                                </td>
                            </tr>
                            <tr>
                                <td background="images/bg02.gif" colspan="4" style="font-size: 1pt; height: 1px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 16px; height: 18px; font-size: 1pt;">
                    </td>
                    <td style="height: 18px; font-size: 1pt;">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 267px; height: 28px">
                                    &nbsp;<asp:Label ID="lblID" runat="server" Font-Size="9pt" Font-Underline="True"
                                        ForeColor="Chocolate" Style="z-index: 102" Width="100%" Font-Names="Arial">ID: </asp:Label></td>
                                <td style="height: 28px">
                                    <asp:Label ID="lblType" runat="server" Font-Size="9pt" Font-Underline="True" ForeColor="Chocolate"
                                        Style="z-index: 102" Width="100%" Font-Names="Arial">Type: </asp:Label></td>
                                <td style="width: 100px; height: 28px">
                                    &nbsp;</td>
                                <td style="width: 80px; height: 28px">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="font-size: 1pt; background-image: url(images/DotLine.gif);
                                    height: 1px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 16px; height: 26px; font-size: 1pt;">
                    </td>
                    <td style="height: 26px; font-size: 1pt;">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 96px; height: 26px">
                                    &nbsp;<asp:Label ID="lblBrowse" runat="server" Font-Size="9pt" ForeColor="RoyalBlue"
                                        Width="96px" Font-Names="Arial">Browser file</asp:Label></td>
                                <td style="width: auto; height: 26px">
                                    <asp:FileUpload ID="MyFile" runat="server" Width="100%" Font-Size="9pt" CssClass="EditTextBox"
                                        Height="18px" /></td>
                                <td style="width: 6px; height: 26px">
                                </td>
                                <td style="width: 78px; height: 26px">
                                    <asp:Button ID="btnUpload" runat="server" CssClass="ButtonLong" OnClick="btnUpload_Click"
                                        Text="Attach" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 1pt; width: 16px; height: 1px">
                    </td>
                    <td style="font-size: 1pt; height: 1px">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="4" style="font-size: 1pt; background-image: url(images/DotLine.gif);
                                    height: 1px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 16px; height: 26px">
                    </td>
                    <td align="left" style="font-size: 1pt; height: 26px">
                        <table align="left" style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 22px">
                                    <asp:Button ID="btnDelFile" runat="server" CssClass="ButtonLong" Font-Size="9pt"
                                        Text="Delete File" OnClick="btnDelFile_Click" /></td>
                                <!--  TextDuringCallBack="Working..." EnabledDuringCallBack="false" PreCallBackFunction="ConfirmDelHasChk_PreCallBack" PostCallBackFunction="RemoveLoading" -->
                                <td align="left" style="width: 78px; height: 22px">
                                    <input id="btnClose" class="ButtonLong" name="btnClose" onclick="javascript:closeTop()"
                                        type="button" value="Close" runat="server" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 1pt; width: 16px; height: 8px">
                    </td>
                    <td style="font-size: 1pt; height: 8px">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="4" style="font-size: 1pt; background-image: url(images/DotLine.gif);
                                    height: 1px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 16px" valign="top">
                    </td>
                    <td align="left" style="font-size: 1pt;" valign="top">
                        <anthem:GridView ID="grd" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="Gray" BorderStyle="Double" BorderWidth="1px" CellPadding="3"
                            CssClass="DIVGrid" Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Black"
                            GridLines="Horizontal" Width="100%" DataKeyNames="FILE_SEQ,FILE_TAG,FILE_NAME,UP_USER,ATT_DATE"
                            EmptyDataText="No data.">
                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                            <PagerSettings Position="TopAndBottom" Visible="False" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField>
                                    <headerstyle horizontalalign="Center" width="20px" />
                                    <headertemplate>                                    
                                        <input id="ChkAll" name="ChkAll" onclick="javascript:checkAll()" type="CheckBox" />
                                </headertemplate>
                                    <itemtemplate>
                                    <asp:CheckBox ID="chkDel" runat="server" />
                                </itemtemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="myFILE_PATH" DataNavigateUrlFormatString="../Upload/{0}"
                                    DataTextField="FILE_TAG" DataTextFormatString="{0:G}" HeaderText="File Name"
                                    Target="_blank" Text="FILE_TAG"></asp:HyperLinkField>
                                <asp:BoundField DataField="UP_USER" HeaderText="Uploader" ReadOnly="True">
                                    <itemstyle width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ATT_DATE" HeaderText="Time" ReadOnly="True">
                                    <itemstyle width="138px" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle Font-Size="9pt" ForeColor="DimGray" BackColor="LightYellow" />
                            <SelectedRowStyle BackColor="#FFFFC0" Font-Bold="False" ForeColor="DarkSlateBlue"
                                BorderStyle="Solid" />
                            <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                            <HeaderStyle BorderStyle="None" CssClass="bg_GridHeader" Font-Bold="True" ForeColor="White"
                                HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="White" Font-Size="9pt" ForeColor="DimGray" />
                            <EditRowStyle Font-Size="9pt" />
                        </anthem:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 16px; height: 1px; font-size: 1pt;">
                    </td>
                    <td style="font-size: 1pt; width: 674px; height: 1px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 16px; font-size: 1pt;">
                    </td>
                    <td style="font-size: 1pt;">
                        <table align="left" style="width: 100%">
                            <tr>
                                <td style="height: 22px; font-size: 1pt;">
                                    <asp:Label ID="Label2" runat="server" Font-Names="宋体" Font-Size="9pt" ForeColor="RoyalBlue"
                                        Width="343px" Visible="False">Tips: </asp:Label></td>
                                <td align="left" style="height: 22px; font-size: 1pt;">
                                </td>
                                <td align="left" style="width: 82px; height: 22px; font-size: 1pt;">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
