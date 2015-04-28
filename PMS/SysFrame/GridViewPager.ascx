<%@ Control Language="C#" AutoEventWireup="true" Inherits="GridViewPager" Codebehind="GridViewPager.ascx.cs" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem"  %>
    
<style type="text/css">
    .PagerEditTextBox /* 可编辑框 */
      {
	    font-size: 9pt;
	    color: #666666;
	    font-family: Verdana, Arial, Helvetica;
	    border-right: #999999 1px solid;
	    border-top: #999999 1px solid;
	    border-left: #999999 1px solid;
	    border-bottom: #999999 1px solid;
	    background-image: url(images/bg_Input.gif);
	    height:16px;
      }
</style>

<script language="javascript" type="text/javascript">
     function Pager_GotoPage()
     {        
        var e = event.srcElement; 
        var k = event.keyCode; 
        
        if (k == 13 && e.type != "textarea") 
        {           
            document.all.<%=imgbtnGo.ClientID%>.click(); 
            event.cancelBubble = true; 
            event.returnValue = false; 
        } 
     }
          
     function Pager_SetRecPerPage()
     {        
        var e = event.srcElement; 
        var k = event.keyCode; 
        
        if (k == 13 && e.type != "textarea") 
        {        
            document.all.<%=imgbtnSetRecPerPage.ClientID%>.click(); 
            event.cancelBubble = true; 
            event.returnValue = false; 
        } 
     }
     
     
     //if the parent control is not a callback control,then do Post action.
     //by Anson on 11-Apr-2006
     function Pager_PostCallBack(arg)
     {  
       /*
        if(arg!=null && arg!="" && typeof(arg)!="undefined" && typeof(arg)!="[object]")      
         { 
           //参数不为空时做相应事
           //alert(arg);
         }
        */
        
        try
        {
            RemoveLoading();   
            
            var t = "<%=t.FullName %>";
            
            if(t=="System.Web.UI.WebControls.GridView" || t=="System.Web.UI.WebControls.DataGrid")
            {
              try
              {
                 __doPostBack("", ""); 
              }
              catch(e)
              { 
                 __PostBack("", "");
              }   
            }
        }
        catch(e){}
     }
   function CheckValue()
   {
      if(document.all.<%=txtPage.ClientID%>.value=="" || document.all.<%=txtPage.ClientID%>.value=="0")
      {        
        return;
      }          
   }
</script>

<table align="left" id="tblGrdbtn" style="width: 420px; font-size: 9pt; float: left; border-bottom-width: 1px; border-bottom-color: dimgray; vertical-align: middle; text-align: left; z-index: 0;" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td style="width: 2px;"></td>
        <td align="center"><anthem:Label ID="lblTotalCount" Height="12px" runat="server" Font-Size="9pt" Font-Overline="False" Font-Names="Times New Roman" ForeColor="Black" Font-Bold="False">Total Record:&nbsp;&nbsp;10 </anthem:Label></td>
        <td style="width: 26px;"><anthem:ImageButton ID="btnPageF" ImageUrl="PagerImages/nv_First2.gif" runat="server" OnClick="btnPageF_Click" PostCallBackFunction="Pager_PostCallBack" PreCallBackFunction="ShowLoading('Loading...')" ></anthem:ImageButton></td>
        <td style="width: 26px;"><anthem:ImageButton ID="btnPageP" ImageUrl="PagerImages/nv_Prev2.gif" runat="server" OnClick="btnPageP_Click" PostCallBackFunction="Pager_PostCallBack" PreCallBackFunction="ShowLoading('Loading...')" ></anthem:ImageButton></td>
        <td align="center"><anthem:Label ID="lblPage" Height="12px" runat="server" Font-Size="9pt" Font-Overline="False" Font-Names="Times New Roman" ForeColor="Black" Font-Bold="False">Page:&nbsp;&nbsp; 1/1</anthem:Label></td>
        <td style="width: 26px;"><anthem:ImageButton ID="btnPageN" ImageUrl="PagerImages/nv_Next2.gif" runat="server" OnClick="btnPageN_Click" PostCallBackFunction="Pager_PostCallBack" PreCallBackFunction="ShowLoading('Loading...')" ></anthem:ImageButton></td>
        <td style="width: 28px;"><anthem:ImageButton ID="btnPageL" ImageUrl="PagerImages/nv_Last2.gif" runat="server" OnClick="btnPageL_Click" PostCallBackFunction="Pager_PostCallBack" PreCallBackFunction="ShowLoading('Loading...')" ></anthem:ImageButton></td>
        <td style="width: 50px;">
            <!--限定只能输入数字-->
            <anthem:TextBox CssClass="PagerEditTextBox" ID="txtPage" onkeydown="Pager_GotoPage();" Style="ime-mode: Disabled" runat="server" Width="52px" Font-Size="9pt" Height="16px" EnableViewState="true" OnTextChanged="txtPage_TextChanged"></anthem:TextBox>
        </td>
        <td style="width: 32px;" align="center">
            <anthem:ImageButton ID="imgbtnGo" runat="server" ImageUrl="PagerImages/go_6.gif" OnClick="imgbtnGo_Click" PostCallBackFunction="Pager_PostCallBack" PreCallBackFunction="ShowLoading" />
        </td>
        <td align="right" style="width: 38px">
            <!--限定只能输入数字-->
            <anthem:TextBox  CssClass="PagerEditTextBox" ID="txtRecPerPage" onkeydown="Pager_SetRecPerPage();" runat="server" Font-Size="9pt" Height="16px" Style="ime-mode: disabled" Width="36px">10</anthem:TextBox></td>
        <td align="right" style="width: 22px">
            <anthem:ImageButton ID="imgbtnSetRecPerPage" runat="server" ImageUrl="PagerImages/nv_SetDispPerPage_1.gif" PostCallBackFunction="Pager_PostCallBack" PreCallBackFunction="ShowLoading" OnClick="imgbtnSetRecPerPage_Click" /></td>
    </tr>
</table>
<input id="hidGrdID" type="hidden" name="hidGrdID" runat="server" style="width: 1px; height: 18px">
