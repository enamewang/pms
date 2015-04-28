<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMeetingMinuteExcessivePage.aspx.cs"
    Inherits="PMS.PMS.EditMeetingMinuteExcessivePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <base target="_self" />

    <script type="text/javascript">

        //获取页面大小和窗口大小
        function GetIframeStatus() {

            var iframe = document.getElementById("IFrameCreatPage");
            var iframeWindow = iframe.contentWindow;
            var iframeWidth, iframeHeight;
            //获取Iframe的内容实际宽度
            iframeWidth = iframeWindow.document.documentElement.scrollWidth;
            //获取Iframe的内容实际高度
            iframeHeight = iframeWindow.document.documentElement.scrollHeight;

            // var bHeight = iframe.contentWindow.document.body.scrollHeight;
            // var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
            // var height = Math.max(bHeight, dHeight);

            //内容是否加载完
            if (iframeWindow.document.readyState == "complete") {

                var ua = window.navigator.userAgent;

                var msie = ua.indexOf("MSIE ");


                if (msie > 0)      // If Internet Explorer, return version number 

                    switch (ua.substring(msie + 5, ua.indexOf(".", msie))) {
                    case "6":
                        //设置Iframe的宽度
                        iframe.width = iframeWidth - 5;
                        //设置Iframe的高度
                        iframe.height = iframeHeight - 30;
                        //alert("ie6");
                        break;

                    case "7":
                        //设置Iframe的宽度
                        iframe.width = iframeWidth;
                        //设置Iframe的高度
                        iframe.height = iframeHeight;
                        //alert("ie7");
                        break;

                    case "8":
                    default:
                        //设置Iframe的宽度
                        iframe.width = iframeWidth;
                        //设置Iframe的高度
                        iframe.height = iframeHeight;
                        //alert("ie8" + ";" + iframe.height);
                }
                else    // If another browser, return 0
                {
                    //设置Iframe的宽度
                    iframe.width = iframeWidth + 10;
                    //设置Iframe的高度
                    iframe.height = iframeHeight;

                    alert("your web browser is not IE or andvace than IE 6.0!");
                }

            }
            else {
                timeIframe = setTimeout(GetIframeStatus, 10);
            }
        }

        function onLoad() {

            GetIframeStatus();
        }

    </script>

</head>
<body onload="onLoad()" style="overflow: scroll; overflow-x: hidden">
    <form id="form1" runat="server">
    <iframe id="IFrameCreatPage" runat="server" scrolling="no" frameborder="0"></iframe>
    </form>
</body>
</html>