<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameMain" Codebehind="FrameMain.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=m_strSysName%>
        WSC framework-ITS</title>
        
    <!--IE地址栏前换成自己的图标 -->
    <link rel="SHORTCUT ICON" href="images/favicon.ico" />
    <!--收藏夹中显示出你的图标 -->
    <link rel="BOOKMARK" href="images/favicon.ico" />

    <script language="javascript">
       
       function checkState()
			{	
			    try
			    {
				    if(document.all("leftFrame").readyState=="complete")
				    {
				        rightFrame.document.all("divLoading").style.display="NONE";				    
					    return true;
				    }
				    else
				    {
				        rightFrame.document.all("divLoading").style.display="BLOCK";		    
					    window.setTimeout("checkState()",1000);
					    return false;
				    }
				}
				catch(e)
				{
				    return true;
				}
			}
    </script>

</head>
<frameset rows="64,*,25" id="allMain" framespacing="0" cols="*" frameborder="NO" border="0"
    onload="checkState();"> 
  <frame name="topFrame" scrolling="NO" noresize src="FrameHead.aspx" MARGINWIDTH="0" MARGINHEIGHT="0">
  <frameset cols="180,10,*,0" id=main  framespacing=0 frameborder="NO" border="0" onload="checkState();">        
    	<frame src="FrameLeft.aspx"  id="leftFrame" scrolling="auto" MARGINWIDTH="0" MARGINHEIGHT="0">
    	<frame src="FrameMiddle.aspx" id="midFrame" noresize MARGINWIDTH="0" MARGINHEIGHT="0">   
        <frame src="<%=ResolveUrl1(System.Configuration.ConfigurationSettings.AppSettings["WSC_FrameAbout"])%>"  id="rightFrame" scrolling="auto" name="rightFrame" MARGINWIDTH="0" MARGINHEIGHT="0">  
        <frame id="frameid" width="0" height="0"  src="<%=ResolveUrl1(WSC.GlobalDefinition.GetPageUrl())%>"></frame>       
  </frameset>
  <frame name="endFrame" scrolling="NO" noresize src="Footer.aspx" MARGINWIDTH="0" MARGINHEIGHT="0">
</frameset>
<body>
    <form id="MainFrame" runat="server">
        <noframes>
        </noframes>
    </form>
</body>
</html>
