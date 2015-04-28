<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_UserSelector" ValidateRequest="false"  Codebehind="UserSelector.aspx.cs" %>

<%@ Register Src="GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
		*******************************************************************************************************
		Created by Anson Lin on 18-Feb-2006
		Description:  Upload local files to server for specified Item No. 
			          The first picture user uploaded is the item's photo, 
	                  user can arrange the file list to reset the photo for item.
		*******************************************************************************************************
		-->
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="UserSelectorHead" runat="server">
    <title>Select user</title>
    <link href="styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <%--<script language="javascript" src="javascript/FrameMain.js"></script>--%>
    <BASE target="_self"></BASE>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT" />
    <script language="javascript" type="text/javascript">
                    
           function closeTop()
	          {		            	           
	            try{top.close();}catch(e){}  
	          }	
	          
	          
	       function SelectRow(SelectedItemName, SelectedItemDept)
	          {		            	           
	            try
	            {
	               var strRtnCtrlName="";                   
                   try {  strRtnCtrlName  = document.all("hidRtnCtrlName").value;  }         catch(e){}     
                           	              
                   var strRtnCtrlDept="";                   
                   try {  strRtnCtrlDept  = document.all("hidRtnCtrlDept").value;  }         catch(e){}
                           	          
                   var obj;
                   if(strRtnCtrlName!="")                  
                   {       
                       obj = window.opener.document.getElementById(strRtnCtrlName);
                      
			           try{  obj.value     = SelectedItemName;    }   catch(e){}    
			           try{  obj.innerHTML = SelectedItemName;    }   catch(e){}    	    			       
			           try{  obj.innerText = SelectedItemName;    }   catch(e){}       			       
			       }	              
			       
			       if(strRtnCtrlDept!="")                  
                   {                  
                       obj = window.opener.document.getElementById(strRtnCtrlDept);
                      
			           try{  obj.value     = SelectedItemDept;    }   catch(e){}    
			           try{  obj.innerHTML = SelectedItemDept;    }   catch(e){}    	    			       
			           try{  obj.innerText = SelectedItemDept;    }   catch(e){}       			 			           
			       }	              
			       
//                   if(strRtnCtrlName!="")                  
//                   {                                         
//			           try{  window.opener.document.all(strRtnCtrlName).value     = SelectedItemName;    }   catch(e){}    
//			           try{  window.opener.document.all(strRtnCtrlName).innerHTML = SelectedItemName;    }   catch(e){}    	    			       
//			           try{  window.opener.document.all(strRtnCtrlName).innerText = SelectedItemName;    }   catch(e){}       			       
//			       }	              
//			       
//			       if(strRtnCtrlDept!="")                  
//                   {                  
//			           try{  window.opener.document.all(strRtnCtrlDept).value     = SelectedItemDept;    }   catch(e){}    
//			           try{  window.opener.document.all(strRtnCtrlDept).innerHTML = SelectedItemDept;    }   catch(e){}    	    			       
//			           try{  window.opener.document.all(strRtnCtrlDept).innerText = SelectedItemDept;    }   catch(e){}       			       
//			       }	              
			        	   	              
	               top.close();		               
	            }  
	            catch(e)
	            {}
	          }  	   
    </script>  

</head>
<body>
    <form id="UserSelectorForm" runat="server">
    <div>
        <table border="0" style="width: 98%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 16px; height: 20px">
                </td>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 16px; height: 14px">
                </td>
                <td style="font-size: 1pt; height: 14px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 78px; height: 22px">
                                &nbsp;<asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="SteelBlue"
                                    Text="Select User" Width="122px"></asp:Label></td>
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
                <td style="font-size: 1pt; width: 16px; height: 18px">
                </td>
                <td style="font-size: 1pt; height: 18px">
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 16px; height: 26px">
                </td>
                <td style="font-size: 1pt; height: 26px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 78px; height: 26px">
                                &nbsp;<asp:Label ID="lblKey" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    ForeColor="RoyalBlue" Width="90px">Key</asp:Label></td>
                            <td style="height: 26px">
                                <asp:TextBox ID="txtKey" runat="server" CssClass="EditTextBox" Width="98%"></asp:TextBox></td>
                            <td style="width: 6px; height: 26px">
                                <anthem:Button ID="btnSearch" runat="server" CssClass="ButtonLong"  OnClick="btnSearch_Click"
                                    Text="Search"  TextDuringCallBack="Working..." EnabledDuringCallBack="false" PreCallBackFunction="ShowLoading('Searching ...')" PostCallBackFunction="RemoveLoading" /></td>
                            <td style="width: 78px; height: 26px">
                                <input id="btnClose" runat="server" class="ButtonLong" name="btnClose" onclick="javascript:closeTop()"
                                    type="button" value="Close" /></td>
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
                <td align="left" style="width: 16px" valign="top">
                </td>
                <td align="left" style="font-size: 1pt" valign="top">
                    <table align="left" style="width: 100%">
                        <tr>
                            <td style="font-size: 1pt; height: 8px; width: 286px;">
                            </td>
                            <td align="left" style="font-size: 1pt; width: 340px; height: 8px">                               
                                <uc1:GridViewPager ID="Pager1" runat="server" Init_Grid_ID="grd" OnBindGrid="BindGrid"
                                    SetPagerButtonImageStyle="Default" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 16px; font-size: 1pt;" valign="top">
                </td>
               
                <td align="left" style="font-size: 1pt" valign="top">
                   
                    <anthem:GridView ID="grd" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="Gray" BorderStyle="Double" BorderWidth="1px"
                        CellPadding="3" CssClass="DIVGrid" Font-Names="Times New Roman" Font-Size="9pt"
                        ForeColor="Black" GridLines="Horizontal"  Width="100%" 
                        OnRowDataBound="grd_RowDataBound" DataKeyNames="LOGIN_NAME,EMP_NAME,EMP_NO,DEPT_CODE,EXT_NO"
                        EmptyDataText="No data.">
                        <emptydatarowstyle backcolor="LightYellow" forecolor="Black"/>
                        <PagerSettings Position="TopAndBottom" Visible="False" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnSel" runat="server" ImageAlign="Middle" ImageUrl="images/Hello_2.gif"
                                        ToolTip="Select" CommandName="Select" />
                                        <ItemStyle Width="15%" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LOGIN_NAME" HeaderText="Login Name" ReadOnly="True">
                                <ItemStyle width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_NAME" HeaderText="Name" ReadOnly="True">
                                <ItemStyle width="20%" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMP_NO" HeaderText="EMP_NO" ReadOnly="True">
                                <ItemStyle width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEPT_CODE" HeaderText="Department" ReadOnly="True">
                                <ItemStyle width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EXT_NO" HeaderText="Ext" ReadOnly="True">
                                <ItemStyle width="15%" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle Font-Size="9pt" ForeColor="DimGray" BackColor="LightYellow" />
                        <SelectedRowStyle BackColor="#FFFFC0" Font-Bold="False" ForeColor="DarkSlateBlue" BorderStyle="Solid" />
                        <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                        <HeaderStyle BorderStyle="None" CssClass="bg_GridHeader" Font-Bold="True" ForeColor="White"
                            HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" Font-Size="9pt" ForeColor="DimGray" />
                        <EditRowStyle Font-Size="9pt" />
                        </anthem:GridView>
                                           
                </td>
            </tr>
            <tr>
                <td style="font-size: 1pt; width: 16px">
                </td>
                <td style="font-size: 1pt">
                    <table align="left" style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-size: 1pt; height: 22px">
                                <asp:Label ID="lblTips" runat="server" Font-Names="宋体" Font-Size="9pt" ForeColor="RoyalBlue"
                                    Visible="true" Width="100%">Tips: You may keyin the department code or login name.</asp:Label></td>
                            <td align="left" style="font-size: 1pt; height: 22px">
                            </td>
                            <td align="left" style="font-size: 1pt; width: 82px; height: 22px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>      
        <input type="hidden" id="hidRtnCtrlName" runat="server" value="" />
        <input type="hidden" id="hidRtnCtrlDept" runat="server" value="" />
    </form>
</body>
</html>
