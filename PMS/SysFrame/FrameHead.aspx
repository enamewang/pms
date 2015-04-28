<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysFrame_FrameHead"
    EnableEventValidation="false" Codebehind="FrameHead.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- 
  ************************************************************************************************
  *********Created by Anson Lin on 18-Jan-2006                                           *********
  *********Generate the header link.                                                     *********
  ************************************************************************************************
-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Header</title>
    <link href="styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="styles/GlobalStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
  
       //Time
//        function showTime()
//        {
//            if(!document.layers && !document.all)
//              return
//              
//            var Digital = new Date()
//           
//            var year    = Digital.getFullYear();
//            var month   = Digital.getMonth() + 1; 
//            var day     = Digital.getDate();   
//            /*            
//            var hours   = Digital.getHours();
//            var minutes = Digital.getMinutes();
//            var seconds = Digital.getSeconds();
//            
//            var dn="AM"
//            
//            if(hours>12)
//            {
//               dn = "PM";
//               hours = hours - 12;
//            }

//            if(hours==0)   hours   = 12;
//            if(minutes<=9) minutes = "0" + minutes;
//            if(seconds<=9) seconds = "0" + seconds;
//            if(month<=9)   month   = "0" + month;
//            if(day<=9)     day     = "0" + day;
//            
//            //change font size here to your desire
//            myclock = "&nbsp;&nbsp" + year + "-" + month + "-" + day + "&nbsp;&nbsp;&nbsp;"//"<font size='1'face='Bodoni' ></font>" 
//                  + hours + ":" + minutes + ":" + seconds + "&nbsp;&nbsp;" + dn;
//             */
//             
//            myclock = "&nbsp;&nbsp" + year + "-" + month + "-" + day + "&nbsp;&nbsp;&nbsp;";
//            
//            if(document.layers)
//            {
//               document.layers.liveclock.document.write(myclock);
//               document.layers.liveclock.document.close();
//            }
//            else if(document.all)
//               liveclock.innerHTML = myclock;

//            //setTimeout("showTime()",1000);
//           
//        }
        
        //-->
    </script>
</head>
<body>
    <form id="frmFrameHead" runat="server">
        <table id="tblHeader" class="BannerBlue">
            <tr class="BannerBlue">
            <td class="Width20">
                </td>
                    <td style="height: 36px; width: 65%;" valign="middle" align="left">
                        <asp:label id="LogoL" runat="server" CssClass="BannerTitle"></asp:label>
                        <asp:Label ID="lblFullName" runat="server" CssClass="BannerFullName"></asp:Label></td>
                <td id="HeaderMid">
                </td>
                <td align="right" style="height: 36px; width: 20%;">
                     <img id="imglogoR" src="images/QTYPE_BW_REV.jpg" height="36px"></td>
                     <td class="Width20">
                </td>
            </tr>
        </table>
        <div style="position: absolute; height: 28px; z-index: 0; left: 0px; top: 36px; width: 100%;">
            <table class="BannerGray">
                <tr class="BannerGray">
                <td class="Width20">
                    </td>
                    <td style="width: 340px; width:60%">
                        <asp:Label ID="lblLoginName" runat="server" CssClass="BannerGrayInfo"></asp:Label></td>
                    <td valign="middle">
                    <span>&nbsp;</span>
                        <%        
                            string strAddr = "", strType = "", strName = "", strDesc = "";
                            string strTarget = "";
                            try
                            {
                                if (m_ds != null && m_ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < m_ds.Tables[0].Rows.Count; i++)
                                    {
                                        strAddr = m_ds.Tables[0].Rows[i]["laddr"].ToString().Trim();
                                        strType = m_ds.Tables[0].Rows[i]["ltype"].ToString().Trim();
                                        strName = m_ds.Tables[0].Rows[i]["lname"].ToString().Trim();
                                        strDesc = m_ds.Tables[0].Rows[i]["ldesc"].ToString().Trim();

                                        if (strType == "P")
                                            strTarget = "new";
                                        else
                                            strTarget = "_top";
                        %>
                        <font size="1" color="#ffffff">&nbsp;&nbsp;</font> <a style="font-family: Arial, Garamond, sans-serif;font-size: 9pt;color: #D7D7D7;text-decoration: none;" title="<%=strDesc%>"
                            href="<%=strAddr%>" target="<%=strTarget%>">
                            <%=strName%>
                        </a><font size="1"><font color="#ffffff">&nbsp;&nbsp;&nbsp;&nbsp;</font></font>
                        <%
                            ;
                        }
                    }
                }
                catch { }
                m_ds = null;
        
                        %>
                    </td>
                    <td align="right" style="width:22%" valign="middle">
            <asp:DropDownList ID="ddlMultLang" runat="server" AutoPostBack="True"
                CssClass="DropDownListLan" OnSelectedIndexChanged="ddlMultLang_SelectedIndexChanged">
            </asp:DropDownList>
            <span>&nbsp;&nbsp;</span>
            <a style="font-family: Arial;font-size: 9pt;color: #D7D7D7;text-decoration: underline;" href="login.aspx" target="_top">Logout</a>
               </td>
                    <td class="Width20">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
