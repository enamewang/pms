<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectsInformation.aspx.cs"
    Inherits="PMS.PMS.Maintain.ProjectsInformation" %>

<%@ Register Src="~/PMS/UserControls/ProjectProgress.ascx" TagName="ProjectProgress"
    TagPrefix="uc1" %>
<%@ Register Src="~/PMS/UserControls/BasicInformationDetail.ascx" TagName="BasicInformationDetail"
    TagPrefix="uc2" %>
<%@ Register Src="~/PMS/UserControls/BasicInformationDetailService.ascx" TagName="BasicInformationDetailService"
    TagPrefix="uc3" %>
<%@ Register Src="~/PMS/UserControls/SchedulePlan.ascx" TagName="SchedulePlan" TagPrefix="uc4" %>
<%@ Register Src="~/PMS/UserControls/ExecutePlan.ascx" TagName="ExecutePlan" TagPrefix="uc5" %>
<%@ Register Src="~/PMS/UserControls/SdpDetailInformation.ascx" TagName="SdpDetailInformation"
    TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/CustomTabStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="~/SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" src="~/SysFrame/JavaScript/Calendar.js"></script>

    <link href="~/SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />

    <script src="~/PMS/JavaScript/pms.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function WebServiceFun(ddlDomain, txtSystem, ddlSite) {
            //调用页面的后台方法，获取version
            PageMethods.GetVersionNewAndOld(ddlDomain, txtSystem, ddlSite, GetVersion);
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ToolkitScriptManager1" AsyncPostBackTimeout="1200"
        EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="~/PMS/JavaScript/PmsCommonJSFuction.js" />
        </Scripts>
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divJavaScriptTotal" runat="server">
        <%--//Abel 注释掉 on 2014-01-22
        <script language="javascript" type="text/javascript">

            function Tabs1_ClientActiveTabChanged() {

                var projectType = "<%=ProjectType %>";

                // Type=="Service"的时候，不需要SettxtActualStartDateValue(asd)
                if (projectType == "Service") {

                    var tm = GetTotalManpowerValue();
                    SettxtTotalManpowerValueService(tm);
                    var duration = GetDurationForServiceValue();
                    SettxDurationValueService(duration);

                }
                else {
                    var p = GetProgressValue();
                    SettxtProgressValue(p);
s
                    var tm = GetTotalManpowerValue();
                    SettxtTotalManpowerValue(tm);

                    var asd = GetActualStartDateValue();
                    SettxtActualStartDateValue(asd);
                }
                return false;
            }
        </script>--%>
    </div>
    <div>
        <div style="margin: 10px; width: 1000px">
            <br />
            <div>
                <uc1:ProjectProgress ID="ProjectProgress1" runat="server" />
            </div>
            <div style="height: 5px">
            </div>
            <div>
                <ajaxToolkit:TabContainer Style="margin: 3px" ID="Tabs1" runat="server" ScrollBars="auto"
                    ActiveTabIndex="0" Width="980px">
                    <ajaxToolkit:TabPanel runat="server" ID="TabPanelBasicInformation" CssClass="TabVisableCss">
                        <HeaderTemplate>
                            <span>Basic Information</span>
                        </HeaderTemplate>
                        <ContentTemplate>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%--<ajaxToolkit:TabPanel runat="server" ID="TabPanelSDP">
                        <HeaderTemplate>
                            <span>SDP</span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc6:SdpDetailInformation ID="SdpDetailInformation1" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>--%>
                    <ajaxToolkit:TabPanel runat="server" ID="TabPanelSchedulePlan">
                        <HeaderTemplate>
                            <span>Schedule Plan</span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc4:SchedulePlan ID="SchedulePlan1" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="TabPanelExecutePlan">
                        <HeaderTemplate>
                            <span>Execute Plan</span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc5:ExecutePlan ID="ExecutePlan1" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelDocuments" runat="server">
                        <HeaderTemplate>
                            <span>Documents</span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="iFrameDOC" src="<%=DocUrl%>" width="100%" height="400px" frameborder="0"
                                scrolling="auto"></iframe>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelMeetingMinute" runat="server">
                        <HeaderTemplate>
                            <span>Meeting Minutes</span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="iFrameMeetingMinute" src="<%=MeetingMinuteUrl%>" width="100%" height="400px"
                                frameborder="0" scrolling="auto"></iframe>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelChangeHistory" runat="server">
                        <HeaderTemplate>
                            <span>Change History</span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="iFrameChangeHistory" src="<%=ChangeHistoryUrl%>" width="100%" height="400px"
                                frameborder="0" scrolling="auto"></iframe>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </div>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
