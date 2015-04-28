<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SdpDetailInformation.ascx.cs"
    Inherits="PMS.PMS.UserControls.SdpDetailInformation" %>
<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<link href="../../Style/myStyle.css" type="text/css" rel="stylesheet" />
<base target="_self" />
<meta http-equiv="pragma" content="no-cache">
<meta http-equiv="Cache-Control" content="no-cache, must-revalidate">
<meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT">

<script language="javascript" type="text/javascript">


    function ShowDetail(div, img) {
        if (div.style["display"] == "none") {
            div.style["display"] = "block";
            img.src = "../../SysFrame/images/appear.gif";
        }
        else {
            div.style["display"] = "none";
            img.src = "../../SysFrame/images/hide.gif";
        }
    }

    function ImageDivDesignDetailOpen_OnClientClick() {
        var div = document.getElementById("<%=DivDesignDetail.ClientID%>");
        var img = document.getElementById("<%=ImageDivDesignDetailOpen.ClientID %>");
        ShowDetail(div, img);
    }

    function ImageDivDevelopmentDetailOpen_OnClientClick() {
        var div = document.getElementById("<%=DivDevelopmentDetail.ClientID%>");
        var img = document.getElementById("<%=ImageDivDevelopmentDetailOpen.ClientID %>");
        ShowDetail(div, img);

    }

    function ImageDivTestDetailOpen_OnClientClick() {
        var div = document.getElementById("<%=DivTestDetail.ClientID%>");
        var img = document.getElementById("<%=ImageDivTestDetailOpen.ClientID %>");
        ShowDetail(div, img);
    }

    function ImageDivReleaseSDetailOpen_OnClientClick() {
        var div = document.getElementById("<%=DivReleaseSDetail.ClientID%>");
        var img = document.getElementById("<%=ImageDivReleaseSDetailOpen.ClientID %>");
        ShowDetail(div, img);
    }

    function ImageDivSupportDetailOpen_OnClientClick() {
        var div = document.getElementById("<%=DivSupportDetail.ClientID%>");
        var img = document.getElementById("<%=ImageDivSupportDetailOpen.ClientID %>");
        ShowDetail(div, img);
    }

    function ShowDetailAllOrNone(object) {
        var imageString = document.getElementById("<%=imageButtonExpand.ClientID%>").src;
        var showOrHide = imageString.substring(imageString.lastIndexOf("/"), imageString.length);

        var divDesign = document.getElementById("<%=DivDesignDetail.ClientID%>");
        var imageDivDesign = document.getElementById("<%=ImageDivDesignDetailOpen.ClientID%>");

        var divDevelopment = document.getElementById("<%=DivDevelopmentDetail.ClientID%>");
        var imageDivDevelopment = document.getElementById("<%=ImageDivDevelopmentDetailOpen.ClientID%>");

        var divTest = document.getElementById("<%=DivTestDetail.ClientID%>");
        var imageDivTest = document.getElementById("<%=ImageDivTestDetailOpen.ClientID%>");

        var divRelease = document.getElementById("<%=DivReleaseSDetail.ClientID%>");
        var imageDivRelease = document.getElementById("<%=ImageDivReleaseSDetailOpen.ClientID%>");

        var divSupport = document.getElementById("<%=DivSupportDetail.ClientID%>");
        var imageDivSupport = document.getElementById("<%=ImageDivSupportDetailOpen.ClientID%>");

        var imageExpand = document.getElementById("<%=imageButtonExpand.ClientID%>");

        if (showOrHide.toLowerCase() == "/hide.gif") {
            DivShowChange(divDesign, imageDivDesign, "block", "appear.gif");
            DivShowChange(divDevelopment, imageDivDevelopment, "block", "appear.gif");
            DivShowChange(divTest, imageDivTest, "block", "appear.gif");
            DivShowChange(divRelease, imageDivRelease, "block", "appear.gif");
            DivShowChange(divSupport, imageDivSupport, "block", "appear.gif");

            imageExpand.src = "../../SysFrame/images/appear.gif";
        }
        else if (showOrHide.toLowerCase() == "/appear.gif") {
            DivShowChange(divDesign, imageDivDesign, "none", "hide.gif");
            DivShowChange(divDevelopment, imageDivDevelopment, "none", "hide.gif");
            DivShowChange(divTest, imageDivTest, "none", "hide.gif");
            DivShowChange(divRelease, imageDivRelease, "none", "hide.gif");
            DivShowChange(divSupport, imageDivSupport, "none", "hide.gif");

            imageExpand.src = "../../SysFrame/images/hide.gif";
        }
    }

    function DivShowChange(divGrd, imageDiv, styleValue, imgName) {
        if (divGrd != null) {
            if (divGrd.Visble = true) {
                divGrd.style["display"] = styleValue;
                imageDiv.src = "../../SysFrame/images/" + imgName;
            }
        }
    }

    //strGridViewDesc的取值只能为：Design，Development，Test，Release，Support其中之一。
    function QimagebuttonDelete_OnClientClick(strGridViewDesc) {
        var gridView;
        if (strGridViewDesc == "Design")
            gridView = document.getElementById("<%=gridViewDesign.ClientID %>");
        if (strGridViewDesc == "Development")
            gridView = document.getElementById("<%=gridViewDevelopment.ClientID %>");
        if (strGridViewDesc == "Test")
            gridView = document.getElementById("<%=gridViewTest.ClientID %>");
        if (strGridViewDesc == "Release")
            gridView = document.getElementById("<%=gridViewRelease.ClientID %>");
        if (strGridViewDesc == "Support")
            gridView = document.getElementById("<%=gridViewSupport.ClientID %>");
        if (!confirmDeleteItem(gridView)) return;
    }

    //获取相应的值，更新Basic页签
    function GetActualStartDateValue() {
        var hidActualStartDateValue = document.getElementById("<%=HiddenFieldActualStartDateValue.ClientID %>");
        if (hidActualStartDateValue != null && hidActualStartDateValue != undefined)
            return hidActualStartDateValue.value;
        else return "";
    }

    function GetProgressValue() {
        var hidProgress = document.getElementById("<%=HiddenFieldProgressValue.ClientID %>");
        if (hidProgress != null && hidProgress != undefined)
            return hidProgress.value;
        else return "";
    }

    function GetTotalManpowerValue() {
        var totalManpower = document.getElementById("<%=HiddenFieldTotalManpowerValue.ClientID %>");
        if (totalManpower != null && totalManpower != undefined)
            return totalManpower.value;
        else return "";
    }      
    
    function GetDurationForServiceValue() {
        var duration = document.getElementById("<%=HiddenFieldDurationValue.ClientID %>");
        if (duration != null && duration != undefined)
            return duration.value;
        else return "";
    }
    
    function ButtonConfirm_ClientClick() {
    	
                if (confirm("Are you sure to confirm the SDP task?") == true) {
                    return true;
                }
                else {
                	event.returnValue = false;
                	return false;
                }

    }
    
</script>

<body>
    <div>
        <asp:ScriptManagerProxy ID="ScriptManagerProxySdpDetail" runat="server">
        </asp:ScriptManagerProxy>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" ID="LabelBlank" CssClass="label110" Visible="false"></asp:Label>
                    <asp:Button runat="server" ID="ButtonConfirm" Text="SDP Confirm" 
                        Width="120px" Enabled="false" OnClientClick="ButtonConfirm_ClientClick();" OnClick="ButtonConfirm_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <table style="width: 100%;">
            <tr>
                <td style="height: 25px; width: 100%;" bgcolor="aliceblue">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table border="0">
            <tr>
                <td colspan="2">
                    <table border="0" align="left">
                        <tr>
                            <td>
                                <table class="layout" id="tableDetail" runat="server" cellspacing="0" cellpadding="0"
                                    border="0" tabindex="20">
                                    <tr>
                                        <td class="hint">
                                            <asp:Image ID="imageButtonExpand" onclick="javascript:ShowDetailAllOrNone(this);"
                                                runat="server" ImageUrl="~/SysFrame/images/hide.gif"></asp:Image>
                                            <asp:Label ID="labelExpand" runat="server">Expand All</asp:Label>&nbsp;
                                            <asp:UpdatePanel ID="updatePanelAllPhasePercent" runat="server" RenderMode="Inline"
                                                UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="label27" runat="server" Text="计划总工期："></asp:Label>
                                                    <asp:Label ID="labelAllPhasePlanTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label62" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label28" runat="server" Text="实际总工期："></asp:Label>
                                                    <asp:Label ID="labelAllPhaseActualTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label91" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label10" runat="server" Text="完成百分比："></asp:Label>
                                                    <asp:Label ID="labelAllPhasePercentValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <div>
                                                        <asp:HiddenField ID="HiddenFieldActualStartDateValue" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldProgressValue" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldTotalManpowerValue" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldDurationValue" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <div id="DivDesign" runat="server">
                                    <table class="layout" id="tableDivDesignDetail" runat="server" cellspacing="0" cellpadding="0"
                                        border="0" tabindex="21">
                                        <tr>
                                            <td class="hint">
                                                &nbsp;&nbsp;&nbsp;<asp:Image ID="ImageDivDesignDetailOpen" onclick="ImageDivDesignDetailOpen_OnClientClick()"
                                                    runat="server" ImageUrl="~/SysFrame/images/hide.gif"></asp:Image>
                                                <asp:Label ID="label1" runat="server" Text="设计阶段" Width="85px"></asp:Label>
                                                <asp:UpdatePanel ID="updatePanelDesignCompletedPercent" runat="server" RenderMode="Inline"
                                                    UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="label25" runat="server" Text="计划总工期："></asp:Label>
                                                        <asp:Label ID="labelDesignPlanTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                        <asp:Label ID="Label49" runat="server" Width="10px">&nbsp;</asp:Label>
                                                        <asp:Label ID="label8" runat="server" Text="实际总工期："></asp:Label>
                                                        <asp:Label ID="labelDesignActualTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                        <asp:Label ID="Label52" runat="server" Width="10px">&nbsp;</asp:Label>
                                                        <asp:TextBox ID="textBoxActualTime" runat="server" Visible="false" ReadOnly="true"
                                                            Width="40px"></asp:TextBox>
                                                        <asp:Label ID="labelDesignBeginDate" runat="server" Text="计划开始时间："></asp:Label>
                                                        <asp:Label ID="labelDesignBeginDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                        <asp:Label ID="label11" runat="server" Width="10px">&nbsp;</asp:Label>
                                                        <asp:Label ID="labelDesignEndDate" runat="server" Text="计划结束时间："></asp:Label>
                                                        <asp:Label ID="labelDesignEndDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                        <asp:Label ID="label9" runat="server" Width="10px">&nbsp;</asp:Label>
                                                        <asp:Label ID="labelDesignCompletedPercent" runat="server" Text="完成百分比："></asp:Label>
                                                        <asp:Label ID="labelDesignCompletedPercentValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="DivDesignDetail" runat="server" style="display: none">
                                        <%--  <div id="DivDesignDetail" runat="server" style="display: none">--%>
                                        <table class="layout" id="tableDateTime" cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td style="width: 25px;">
                                                    &nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelDesign" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gridViewDesign" runat="server" CssClass="DIVGrid" AutoGenerateColumns="False"
                                                                AllowPaging="False" BackColor="White" BorderColor="Gray" BorderStyle="Solid"
                                                                BorderWidth="1px" CellPadding="1" ShowFooter="True" Font-Names="Times New Roman"
                                                                Font-Size="9pt" DataKeyNames="Serial" ForeColor="Gray" GridLines="Horizontal"
                                                                EmptyDataText="No data." HeaderStyle-BackColor="DimGray" OnRowDataBound="gridViewDesign_RowDataBound"
                                                                OnRowEditing="gridViewDesign_RowEditing" OnRowCancelingEdit="gridViewDesign_RowCancelingEdit"
                                                                OnRowCreated="gridViewDesign_RowCreated" OnRowDeleting="gridViewDesign_RowDeleting"
                                                                OnRowUpdating="gridViewDesign_RowUpdating" OnRowCommand="gridViewDesign_RowCommand">
                                                                <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                                                                <FooterStyle BackColor="WhiteSmoke" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="识别码">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskNo") %>'
                                                                                ID="textBoxTaskNo" MaxLength="3">
                                                                            </asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox runat="server" Width="30px" ID="textBoxTaskNo" MaxLength="3"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'>
                                                                                <asp:Label ID="labelTaskNo" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" Width="30px"></HeaderStyle>
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="任务名称">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox runat="server" Width="250px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskName") %>'
                                                                                ID="textBoxTaskName" MaxLength="300">
                                                                            </asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox runat="server" Width="250px" ID="textBoxTaskName" MaxLength="300"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 250px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'>
                                                                                <asp:Label ID="labelTaskName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="250px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="计划工期(H)">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanCost") %>'
                                                                                ID="textBoxPlanCost" MaxLength="18">
                                                                            </asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox runat="server" Width="40px" ID="textBoxPlanCost" MaxLength="18"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanCost")%>'>
                                                                                <asp:Label ID="labelPlanCost" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PlanCost").ToString())%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="40px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="计划开始日期">
                                                                        <EditItemTemplate>
                                                                            <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanStartDay","{0:yyyy-MM-dd}") %>'
                                                                                runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                                Language="English"></TW:DateTextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay")%>'>
                                                                                <asp:Label ID="labelPlanStartDay" runat="server" Width="73px" Text='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}")%>'
                                                                                    MaxLength="10"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="计划结束日期">
                                                                        <EditItemTemplate>
                                                                            <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanEndDay","{0:yyyy-MM-dd}") %>'
                                                                                runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                                Language="English"></TW:DateTextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay")%>'>
                                                                                <asp:Label ID="labelPlanEndDay" runat="server" Width="73px" Text='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}")%>'
                                                                                    MaxLength="10"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="实际工期(H)">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualCost") %>'
                                                                                ID="textBoxActualCost" MaxLength="18">
                                                                            </asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox runat="server" Width="40px" ID="textBoxActualCost" MaxLength="18"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'>
                                                                                <asp:Label ID="labelActualCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="40px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="完成百分比(%)">
                                                                        <EditItemTemplate>
                                                                            <asp:UpdatePanel ID="updatePanelDesignComPercent" runat="server" RenderMode="Inline"
                                                                                UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.CompletedPercent") %>'
                                                                                        ID="textBoxCompletedPercent" MaxLength="18">
                                                                                    </asp:TextBox>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="textBoxActualCost" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox runat="server" Width="40px" ID="textBoxCompletedPercent" MaxLength="18"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'>
                                                                                <asp:Label ID="labelCompletedPercent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="40px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="实际开始日期">
                                                                        <EditItemTemplate>
                                                                            <TW:DateTextBox ID="calendarActualStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualStartDay","{0:yyyy-MM-dd}") %>'
                                                                                runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <TW:DateTextBox ID="calendarActualStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                                Language="English"></TW:DateTextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay")%>'>
                                                                                <asp:Label ID="labelActualStartDay" runat="server" Width="73px" Text='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay","{0:yyyy-MM-dd}")%>'
                                                                                    MaxLength="10"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="实际结束日期">
                                                                        <EditItemTemplate>
                                                                            <TW:DateTextBox ID="calendarActualEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualEndDay","{0:yyyy-MM-dd}") %>'
                                                                                runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <TW:DateTextBox ID="calendarActualEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                                Language="English"></TW:DateTextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay")%>'>
                                                                                <asp:Label ID="labelActualEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay","{0:yyyy-MM-dd}")%>'
                                                                                    MaxLength="10" Width="73px"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="前置任务">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.PreTaskNo") %>'
                                                                                ID="textBoxPreTaskNo" MaxLength="10">
                                                                            </asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox runat="server" Width="30px" ID="textBoxPreTaskNo" MaxLength="10"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'>
                                                                                <asp:Label ID="labelPreTaskNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="角色">
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                            </asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                            </asp:DropDownList>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 45px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Role")%>'>
                                                                                <asp:Label ID="labelRole" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Role")%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="45px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="资源">
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                            </asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                            </asp:DropDownList>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'>
                                                                                <asp:Label ID="labelResource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remark">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox runat="server" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.Remark") %>'
                                                                                ID="textBoxRemark" MaxLength="300">
                                                                            </asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox runat="server" Width="150px" ID="textBoxRemark" MaxLength="300"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="width: 150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                                text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'>
                                                                                <asp:Label ID="labelRemark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="150px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Edit">
                                                                        <EditItemTemplate>
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/update.gif" ID="QimagebuttonUpdate"
                                                                                CommandName="Update" ToolTip="Update" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/undo.gif" ID="QimagebuttonCancel"
                                                                                CommandName="Cancel" ToolTip="Cancel" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <div id="divDesignSave" runat="server" style="width: 30px; overflow: hidden; white-space: nowrap;
                                                                                text-overflow: ellipsis; text-align: center">
                                                                                <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/save.gif" ID="QimagebuttonSave"
                                                                                    CommandName="Save" ToolTip="Save" CommandArgument="<%# Container.DataItemIndex %>">
                                                                                </asp:ImageButton>
                                                                            </div>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div id="divDesignEdit" runat="server" style="width: 30px; overflow: hidden; white-space: nowrap;
                                                                                text-overflow: ellipsis; text-align: center">
                                                                                <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif" ID="QimagebuttonEdit"
                                                                                    CommandName="Edit" ToolTip="Edit" CommandArgument="<%# Container.DataItemIndex %>">
                                                                                </asp:ImageButton>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <div id="divDesignDelete" runat="server" style="width: 30px; overflow: hidden; white-space: nowrap;
                                                                                text-overflow: ellipsis; text-align: center">
                                                                                <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonDelete.gif" ID="QimagebuttonDelete"
                                                                                    CommandName="Delete" ToolTip="Delete" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                    OnClientClick="javascript:QimagebuttonDelete_OnClientClick('Design')"></asp:ImageButton>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Copy">
                                                                        <ItemTemplate>
                                                                            <div id="divDesignCopy" runat="server" style="width: 30px; overflow: hidden; white-space: nowrap;
                                                                                text-overflow: ellipsis; text-align: center">
                                                                                <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonYes.gif" ID="QimagebuttonSelect"
                                                                                    CommandName="Copy" ToolTip="Copy the item" CommandArgument="<%# Container.DataItemIndex %>">
                                                                                </asp:ImageButton>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                                                                    BorderWidth="0px" />
                                                                <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                                                                <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                    CssClass="bg_GridHeader" BackColor="#a6bce6" />
                                                                <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                                                                    BorderWidth="0px" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <%--<asp:AsyncPostBackTrigger ControlID="buttonSaveHeadInfo" />
                                                                <asp:AsyncPostBackTrigger ControlID="buttonQuery" />--%>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <p>
                                </p>
                                <table class="layout" id="tableDivDevelopmentDetail" runat="server" cellspacing="0"
                                    cellpadding="0" border="0" tabindex="21">
                                    <tr>
                                        <td class="hint" style="height: 14px">
                                            &nbsp;&nbsp;&nbsp<asp:Image ID="ImageDivDevelopmentDetailOpen" onclick="ImageDivDevelopmentDetailOpen_OnClientClick()"
                                                runat="server" ImageUrl="~/SysFrame/images/hide.gif"></asp:Image>
                                            <asp:Label ID="label2" runat="server" Text="开发阶段" Width="85px"></asp:Label>
                                            <asp:UpdatePanel ID="updatePanelDevelopmentCompletedPercent" runat="server" RenderMode="Inline"
                                                UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="label12" runat="server" Text="计划总工期："></asp:Label>
                                                    <asp:Label ID="labelDevelopmentPlanTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label90" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label13" runat="server" Text="实际总工期："></asp:Label>
                                                    <asp:Label ID="labelDevelopmentActualTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="Label36" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:TextBox ID="textBox1" runat="server" Visible="false" ReadOnly="true" Width="40px"></asp:TextBox>
                                                    <asp:Label ID="label15" runat="server" Text="计划开始时间："></asp:Label>
                                                    <asp:Label ID="labelDevelopmentBeginDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label17" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label18" runat="server" Text="计划结束时间："></asp:Label>
                                                    <asp:Label ID="labelDevelopmentEndDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label20" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label21" runat="server" Text="完成百分比："></asp:Label>
                                                    <asp:Label ID="labelDevelopmentCompletedPercentValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <div id="DivDevelopmentDetail" runat="server" style="display: none">
                                    <%-- <div id="DivDevelopmentDetail" runat="server" style="display: none">--%>
                                    <table class="layout" id="table2" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td style="width: 25px">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelDevelopment" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gridViewDevelopment" runat="server" CssClass="DIVGrid" AutoGenerateColumns="False"
                                                            AllowPaging="False" BackColor="White" BorderColor="Gray" BorderStyle="Solid"
                                                            BorderWidth="1px" CellPadding="1" ShowFooter="True" Font-Names="Times New Roman"
                                                            Font-Size="9pt" DataKeyNames="Serial" ForeColor="Gray" GridLines="Horizontal"
                                                            EmptyDataText="No data." HeaderStyle-BackColor="#a6bce6" OnRowCancelingEdit="gridViewDevelopment_RowCancelingEdit"
                                                            OnRowCommand="gridViewDevelopment_RowCommand" OnRowDataBound="gridViewDevelopment_RowDataBound"
                                                            OnRowDeleting="gridViewDevelopment_RowDeleting" OnRowEditing="gridViewDevelopment_RowEditing"
                                                            OnRowUpdating="gridViewDevelopment_RowUpdating">
                                                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                                                            <FooterStyle BackColor="WhiteSmoke" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="识别码">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskNo") %>'
                                                                            ID="textBoxTaskNo" MaxLength="3">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" ID="textBoxTaskNo" MaxLength="3"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'>
                                                                            <asp:Label ID="labelTaskNo" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Width="30px"></HeaderStyle>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="任务名称">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="250px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskName") %>'
                                                                            ID="textBoxTaskName" MaxLength="300">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="250px" ID="textBoxTaskName" MaxLength="300"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 250px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'>
                                                                            <asp:Label ID="labelTaskName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                    <ItemStyle Width="250px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划工期(H)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanCost") %>'
                                                                            ID="textBoxPlanCost" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxPlanCost" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanCost")%>'>
                                                                            <asp:Label ID="labelPlanCost" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PlanCost").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划开始日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanStartDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay")%>'>
                                                                            <asp:Label ID="labelPlanStartDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划结束日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanEndDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay")%>'>
                                                                            <asp:Label ID="labelPlanEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际工期(H)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualCost") %>'
                                                                            ID="textBoxActualCost" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxActualCost" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'>
                                                                            <asp:Label ID="labelActualCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="完成百分比(%)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.CompletedPercent") %>'
                                                                            ID="textBoxCompletedPercent" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxCompletedPercent" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'>
                                                                            <asp:Label ID="labelCompletedPercent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际开始日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarActualStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualStartDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarActualStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay")%>'>
                                                                            <asp:Label ID="labelActualStartDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际结束日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarActualEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualEndDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarActualEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay")%>'>
                                                                            <asp:Label ID="labelActualEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="前置任务">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.PreTaskNo") %>'
                                                                            ID="textBoxPreTaskNo" MaxLength="10">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" ID="textBoxPreTaskNo" MaxLength="10"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'>
                                                                            <asp:Label ID="labelPreTaskNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="角色">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 45px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Role")%>'>
                                                                            <asp:Label ID="labelRole" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Role")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="45px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="资源">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'>
                                                                            <asp:Label ID="labelResource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remark">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.Remark") %>'
                                                                            ID="textBoxRemark" MaxLength="300">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="150px" ID="textBoxRemark" MaxLength="300"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'>
                                                                            <asp:Label ID="labelRemark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/update.gif" ID="QimagebuttonUpdate"
                                                                            CommandName="Update" ToolTip="Update" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/undo.gif" ID="QimagebuttonCancel"
                                                                            CommandName="Cancel" ToolTip="Cancel" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div id="divDevelopmentSave" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/save.gif" ID="QimagebuttonSave"
                                                                                CommandName="Save" ToolTip="Save" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentEdit" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif" ID="QimagebuttonEdit"
                                                                                CommandName="Edit" ToolTip="Edit" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentDelete" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonDelete.gif" ID="QimagebuttonDelete"
                                                                                CommandName="Delete" ToolTip="Delete" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                OnClientClick="javascript:QimagebuttonDelete_OnClientClick('Development')"></asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Copy">
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentCopy" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonYes.gif" ID="QimagebuttonSelect"
                                                                                CommandName="Copy" ToolTip="Copy the Item" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                                                                BorderWidth="0px" />
                                                            <SelectedRowStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                                                            <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                CssClass="bg_GridHeader" BackColor="#a6bce6" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                                                                BorderWidth="0px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <p>
                                </p>
                                <table class="layout" id="tableDivTestDetail" runat="server" cellspacing="0" cellpadding="0"
                                    border="0">
                                    <tr>
                                        <td class="hint" style="height: 14px">
                                            &nbsp;&nbsp;&nbsp;<asp:Image ID="ImageDivTestDetailOpen" onclick="ImageDivTestDetailOpen_OnClientClick()"
                                                runat="server" ImageUrl="~/SysFrame/images/hide.gif"></asp:Image>
                                            <asp:Label ID="label7" runat="server" Text="测试阶段" Width="85px"></asp:Label>
                                            <asp:UpdatePanel ID="updatePanelTestCompletedPercent" runat="server" RenderMode="Inline"
                                                UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="label19" runat="server" Text="计划总工期："></asp:Label>
                                                    <asp:Label ID="labelTestPlanTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="Label53" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label24" runat="server" Text="实际总工期："></asp:Label>
                                                    <asp:Label ID="labelTestActualTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="Label55" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:TextBox ID="textBox2" runat="server" Visible="false" ReadOnly="true" Width="40px"></asp:TextBox>
                                                    <asp:Label ID="label23" runat="server" Text="计划开始时间："></asp:Label>
                                                    <asp:Label ID="labelTestBeginDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label14" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label16" runat="server" Text="计划结束时间："></asp:Label>
                                                    <asp:Label ID="labelTestEndDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label30" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label31" runat="server" Text="完成百分比："></asp:Label>
                                                    <asp:Label ID="labelTestCompletedPercentValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <div id="DivTestDetail" runat="server" style="display: none">
                                    <%-- <div id="DivTestDetail" runat="server" style="display: none">--%>
                                    <table class="layout" id="table5" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td style="width: 25px">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelTest" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gridViewTest" runat="server" CssClass="DIVGrid" AutoGenerateColumns="False"
                                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                                            Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal"
                                                            EmptyDataText="No data." AllowPaging="False" ShowFooter="true" DataKeyNames="Serial"
                                                            HeaderStyle-BackColor="#a6bce6" OnRowCommand="gridViewTest_RowCommand" OnRowDeleting="gridViewTest_RowDeleting"
                                                            OnRowEditing="gridViewTest_RowEditing" OnRowUpdating="gridViewTest_RowUpdating"
                                                            OnRowCancelingEdit="gridViewTest_RowCancelingEdit" OnRowDataBound="gridViewTest_RowDataBound">
                                                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                                                            <FooterStyle BackColor="WhiteSmoke" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="识别码">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskNo") %>'
                                                                            ID="textBoxTaskNo" MaxLength="3">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" ID="textBoxTaskNo" MaxLength="3"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'>
                                                                            <asp:Label ID="labelTaskNo" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Width="30px"></HeaderStyle>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="任务名称">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="250px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskName") %>'
                                                                            ID="textBoxTaskName" MaxLength="300">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="250px" ID="textBoxTaskName" MaxLength="300"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 250px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'>
                                                                            <asp:Label ID="labelTaskName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                    <ItemStyle Width="250px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划工期(H)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanCost") %>'
                                                                            ID="textBoxPlanCost" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxPlanCost" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanCost")%>'>
                                                                            <asp:Label ID="labelPlanCost" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PlanCost").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划开始日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanStartDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay")%>'>
                                                                            <asp:Label ID="labelPlanStartDay" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PlanStartDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划结束日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanEndDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay")%>'>
                                                                            <asp:Label ID="labelPlanEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际工期(H)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualCost") %>'
                                                                            ID="textBoxActualCost" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxActualCost" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'>
                                                                            <asp:Label ID="labelActualCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="完成百分比(%)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.CompletedPercent") %>'
                                                                            ID="textBoxCompletedPercent" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxCompletedPercent" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'>
                                                                            <asp:Label ID="labelCompletedPercent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际开始日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarActualStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualStartDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarActualStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay")%>'>
                                                                            <asp:Label ID="labelActualStartDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际结束日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarActualEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualEndDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarActualEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay")%>'>
                                                                            <asp:Label ID="labelActualEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="前置任务">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.PreTaskNo") %>'
                                                                            ID="textBoxPreTaskNo" MaxLength="10">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" ID="textBoxPreTaskNo" MaxLength="10"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'>
                                                                            <asp:Label ID="labelPreTaskNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="角色">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 45px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Role")%>'>
                                                                            <asp:Label ID="labelRole" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Role")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="45px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="资源">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'>
                                                                            <asp:Label ID="labelResource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remark">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.Remark") %>'
                                                                            ID="textBoxRemark" MaxLength="300">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="150px" ID="textBoxRemark" MaxLength="300"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'>
                                                                            <asp:Label ID="labelRemark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/update.gif" ID="QimagebuttonUpdate"
                                                                            CommandName="Update" ToolTip="Update" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/undo.gif" ID="QimagebuttonCancel"
                                                                            CommandName="Cancel" ToolTip="Cancel" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div id="divDevelopmentSave" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/save.gif" ID="QimagebuttonSave"
                                                                                CommandName="Save" ToolTip="Save" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentEdit" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif" ID="QimagebuttonEdit"
                                                                                CommandName="Edit" ToolTip="Edit" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentDelete" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonDelete.gif" ID="QimagebuttonDelete"
                                                                                CommandName="Delete" ToolTip="Delete" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                OnClientClick="javascript:QimagebuttonDelete_OnClientClick('Test')"></asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Copy">
                                                                    <FooterTemplate>
                                                                        <div id="divDevelopmentCopy" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonYes.gif" ID="QimagebuttonSelect"
                                                                                CommandName="CopyTest" ToolTip="Copy From Development" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonYes.gif" ID="QimagebuttonSelect"
                                                                            CommandName="Copy" ToolTip="Copy the Item" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                                                                BorderWidth="0px" />
                                                            <SelectedRowStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                                                            <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                CssClass="bg_GridHeader" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                                                                BorderWidth="0px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <p>
                                </p>
                                <table class="layout" id="tableDivReleaseSDetail" runat="server" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <tr>
                                        <td class="hint" style="height: 14px">
                                            &nbsp;&nbsp;&nbsp;<asp:Image ID="ImageDivReleaseSDetailOpen" onclick="ImageDivReleaseSDetailOpen_OnClientClick()"
                                                runat="server" ImageUrl="~/SysFrame/images/hide.gif"></asp:Image>
                                            <asp:Label ID="label3" runat="server" Text="Release阶段" Width="100px"></asp:Label>
                                            <asp:UpdatePanel ID="updatePanelReleaseCompletedPercent" runat="server" RenderMode="Inline"
                                                UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="label29" runat="server" Text="计划总工期："></asp:Label>
                                                    <asp:Label ID="labelReleasePlanTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="Label57" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label34" runat="server" Text="实际总工期："></asp:Label>&nbsp;
                                                    <asp:Label ID="labelReleaseActualTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="Label58" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:TextBox ID="textBox3" runat="server" Visible="false" ReadOnly="true" Width="40px"></asp:TextBox>
                                                    <asp:Label ID="label35" runat="server" Text="计划开始时间："></asp:Label>
                                                    <asp:Label ID="labelReleaseBeginDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label37" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label38" runat="server" Text="计划结束时间："></asp:Label>
                                                    <asp:Label ID="labelReleaseEndDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label40" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label41" runat="server" Text="完成百分比："></asp:Label>
                                                    <asp:Label ID="labelReleaseCompletedPercentValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <div id="DivReleaseSDetail" runat="server" style="display: none">
                                    <table class="layout" id="table3" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td style="width: 25px">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelRelease" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gridViewRelease" runat="server" CssClass="DIVGrid" AutoGenerateColumns="False"
                                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                                            Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal"
                                                            EmptyDataText="No data." AllowPaging="False" ShowFooter="true" DataKeyNames="Serial"
                                                            HeaderStyle-BackColor="#a6bce6" OnRowCommand="gridViewRelease_RowCommand" OnRowDeleting="gridViewRelease_RowDeleting"
                                                            OnRowEditing="gridViewRelease_RowEditing" OnRowUpdating="gridViewRelease_RowUpdating"
                                                            OnRowCancelingEdit="gridViewRelease_RowCancelingEdit" OnRowDataBound="gridViewRelease_RowDataBound">
                                                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                                                            <FooterStyle BackColor="WhiteSmoke" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="识别码">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskNo") %>'
                                                                            ID="textBoxTaskNo" MaxLength="3">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" ID="textBoxTaskNo" MaxLength="3"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'>
                                                                            <asp:Label ID="labelTaskNo" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Width="30px"></HeaderStyle>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="任务名称">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="250px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskName") %>'
                                                                            ID="textBoxTaskName" MaxLength="300">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="250px" ID="textBoxTaskName" MaxLength="300"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 250px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'>
                                                                            <asp:Label ID="labelTaskName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle Width="250px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划工期(H)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanCost") %>'
                                                                            ID="textBoxPlanCost" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxPlanCost" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanCost")%>'>
                                                                            <asp:Label ID="labelPlanCost" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PlanCost").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划开始日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanStartDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay")%>'>
                                                                            <asp:Label ID="labelPlanStartDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划结束日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanEndDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay")%>'>
                                                                            <asp:Label ID="labelPlanEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际工期(H)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualCost") %>'
                                                                            ID="textBoxActualCost" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxActualCost" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'>
                                                                            <asp:Label ID="labelActualCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="完成百分比(%)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.CompletedPercent") %>'
                                                                            ID="textBoxCompletedPercent" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxCompletedPercent" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'>
                                                                            <asp:Label ID="labelCompletedPercent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际开始日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarActualStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualStartDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarActualStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay")%>'>
                                                                            <asp:Label ID="labelActualStartDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际结束日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarActualEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualEndDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarActualEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay")%>'>
                                                                            <asp:Label ID="labelActualEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="前置任务">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.PreTaskNo") %>'
                                                                            ID="textBoxPreTaskNo" MaxLength="10">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" ID="textBoxPreTaskNo" MaxLength="10"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'>
                                                                            <asp:Label ID="labelPreTaskNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="角色">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 45px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Role")%>'>
                                                                            <asp:Label ID="labelRole" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Role")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="45px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="资源">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'>
                                                                            <asp:Label ID="labelResource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remark">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.Remark") %>'
                                                                            ID="textBoxRemark" MaxLength="300">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="150px" ID="textBoxRemark" MaxLength="300"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'>
                                                                            <asp:Label ID="labelRemark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/update.gif" ID="QimagebuttonUpdate"
                                                                            CommandName="Update" ToolTip="Update" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/undo.gif" ID="QimagebuttonCancel"
                                                                            CommandName="Cancel" ToolTip="Cancel" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div id="divDevelopmentSave" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/save.gif" ID="QimagebuttonSave"
                                                                                CommandName="Save" ToolTip="Save" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentEdit" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif" ID="QimagebuttonEdit"
                                                                                CommandName="Edit" ToolTip="Edit" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentDelete" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonDelete.gif" ID="QimagebuttonDelete"
                                                                                CommandName="Delete" ToolTip="Delete" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                OnClientClick="javascript:QimagebuttonDelete_OnClientClick('Release')"></asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Copy">
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentCopy" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonYes.gif" ID="QimagebuttonSelect"
                                                                                CommandName="Copy" ToolTip="Copy the Item" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                                                                BorderWidth="0px" />
                                                            <SelectedRowStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                                                            <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                CssClass="bg_GridHeader" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                                                                BorderWidth="0px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <p>
                                </p>
                                <table class="layout" id="tableDivSupportDetail" runat="server" cellspacing="0" cellpadding="0"
                                    border="0" tabindex="21">
                                    <tr>
                                        <td class="hint" style="height: 14px">
                                            &nbsp;&nbsp;&nbsp;<asp:Image ID="ImageDivSupportDetailOpen" onclick="ImageDivSupportDetailOpen_OnClientClick()"
                                                runat="server" ImageUrl="~/SysFrame/images/hide.gif"></asp:Image>
                                            <asp:Label ID="label4" runat="server" Text="Support阶段" Width="100px"></asp:Label>
                                            <asp:UpdatePanel ID="updatePanelSupportCompletedPercent" runat="server" RenderMode="Inline"
                                                UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="label39" runat="server" Text="计划总工期："></asp:Label>
                                                    <asp:Label ID="labelSupportPlanTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="Label61" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label44" runat="server" Text="实际总工期："></asp:Label>
                                                    <asp:Label ID="labelSupportActualTime" runat="server" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="Label59" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:TextBox ID="textBox4" runat="server" Visible="false" ReadOnly="true" Width="40px"></asp:TextBox>
                                                    <asp:Label ID="label45" runat="server" Text="计划开始时间："></asp:Label>
                                                    <asp:Label ID="labelSupportBeginDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label47" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label48" runat="server" Text="计划结束时间："></asp:Label>
                                                    <asp:Label ID="labelSupportEndDateValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                    <asp:Label ID="label50" runat="server" Width="10px">&nbsp;</asp:Label>
                                                    <asp:Label ID="label51" runat="server" Text="完成百分比："></asp:Label>
                                                    <asp:Label ID="labelSupportCompletedPercentValue" runat="server" Text="" CssClass="StatisticLabel"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <div id="DivSupportDetail" runat="server" style="display: none">
                                    <%-- <div id="DivSupportDetail" runat="server" style="display: none">--%>
                                    <table class="layout" id="table4" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td style="width: 25px">
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelSupport" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gridViewSupport" runat="server" CssClass="DIVGrid" AutoGenerateColumns="False"
                                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                                            Font-Names="Times New Roman" Font-Size="9pt" ForeColor="Gray" GridLines="Horizontal"
                                                            EmptyDataText="No data." AllowPaging="False" ShowFooter="true" DataKeyNames="Serial"
                                                            HeaderStyle-BackColor="#a6bce6" OnRowCommand="gridViewSupport_RowCommand" OnRowDeleting="gridViewSupport_RowDeleting"
                                                            OnRowEditing="gridViewSupport_RowEditing" OnRowUpdating="gridViewSupport_RowUpdating"
                                                            OnRowDataBound="gridViewSupport_RowDataBound" OnRowCancelingEdit="gridViewSupport_RowCancelingEdit">
                                                            <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                                                            <FooterStyle BackColor="WhiteSmoke" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="识别码">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskNo") %>'
                                                                            ID="textBoxTaskNo" MaxLength="3">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" ID="textBoxTaskNo" MaxLength="3"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'>
                                                                            <asp:Label ID="labelTaskNo" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskNo").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Width="30px"></HeaderStyle>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="任务名称">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="250px" Text='<%# DataBinder.Eval(Container, "DataItem.TaskName") %>'
                                                                            ID="textBoxTaskName" MaxLength="300">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="250px" ID="textBoxTaskName" MaxLength="300"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 250px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'>
                                                                            <asp:Label ID="labelTaskName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                                    <ItemStyle Width="250px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划工期(H)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanCost") %>'
                                                                            ID="textBoxPlanCost" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxPlanCost" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanCost")%>'>
                                                                            <asp:Label ID="labelPlanCost" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PlanCost").ToString())%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划开始日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanStartDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay")%>'>
                                                                            <asp:Label ID="labelPlanStartDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="计划结束日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.PlanEndDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarPlanEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay")%>'>
                                                                            <asp:Label ID="labelPlanEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际工期(H)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualCost") %>'
                                                                            ID="textBoxActualCost" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxActualCost" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'>
                                                                            <asp:Label ID="labelActualCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="完成百分比(%)">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.CompletedPercent") %>'
                                                                            ID="textBoxCompletedPercent" MaxLength="18">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="40px" ID="textBoxCompletedPercent" MaxLength="18"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'>
                                                                            <asp:Label ID="labelCompletedPercent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompletedPercent")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际开始日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarActualStartDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualStartDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarActualStartDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay")%>'>
                                                                            <asp:Label ID="labelActualStartDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualStartDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="实际结束日期">
                                                                    <EditItemTemplate>
                                                                        <TW:DateTextBox ID="calendarActualEndDay" Width="73px" Text='<%# DataBinder.Eval(Container, "DataItem.ActualEndDay","{0:yyyy-MM-dd}") %>'
                                                                            runat="server" IsDisplayTime="false" Language="English"></TW:DateTextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <TW:DateTextBox ID="calendarActualEndDay" Width="73px" runat="server" IsDisplayTime="false"
                                                                            Language="English"></TW:DateTextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 73px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay")%>'>
                                                                            <asp:Label ID="labelActualEndDay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualEndDay","{0:yyyy-MM-dd}")%>'
                                                                                MaxLength="10" Width="73px"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="前置任务">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" Text='<%# DataBinder.Eval(Container, "DataItem.PreTaskNo") %>'
                                                                            ID="textBoxPreTaskNo" MaxLength="10">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="30px" ID="textBoxPreTaskNo" MaxLength="10"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'>
                                                                            <asp:Label ID="labelPreTaskNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PreTaskNo")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="角色">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" Width="45px" ID="dropDownListRole">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 45px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Role")%>'>
                                                                            <asp:Label ID="labelRole" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Role")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="45px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="资源">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList runat="server" Width="100px" ID="dropDownListUser">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'>
                                                                            <asp:Label ID="labelResource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remark">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.Remark") %>'
                                                                            ID="textBoxRemark" MaxLength="300">
                                                                        </asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox runat="server" Width="150px" ID="textBoxRemark" MaxLength="300"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                                                                            text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'>
                                                                            <asp:Label ID="labelRemark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/update.gif" ID="QimagebuttonUpdate"
                                                                            CommandName="Update" ToolTip="Update" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                        <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/undo.gif" ID="QimagebuttonCancel"
                                                                            CommandName="Cancel" ToolTip="Cancel" CommandArgument="<%# Container.DataItemIndex %>">
                                                                        </asp:ImageButton>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div id="divDevelopmentSave" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/save.gif" ID="QimagebuttonSave"
                                                                                CommandName="Save" ToolTip="Save" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentEdit" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonEdit_1.gif" ID="QimagebuttonEdit"
                                                                                CommandName="Edit" ToolTip="Edit" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                    <ItemStyle Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentDelete" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonDelete.gif" ID="QimagebuttonDelete"
                                                                                CommandName="Delete" ToolTip="Delete" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                OnClientClick="javascript:QimagebuttonDelete_OnClientClick('Support')"></asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Copy">
                                                                    <ItemTemplate>
                                                                        <div id="divDevelopmentCopy" runat="server" style="width: 30px; overflow: hidden;
                                                                            white-space: nowrap; text-overflow: ellipsis; text-align: center">
                                                                            <asp:ImageButton runat="server" ImageUrl="~/SysFrame/images/ButtonYes.gif" ID="QimagebuttonSelect"
                                                                                CommandName="Copy" ToolTip="Copy the Item" CommandArgument="<%# Container.DataItemIndex %>">
                                                                            </asp:ImageButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Bold="True"></HeaderStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                                                                BorderWidth="0px" />
                                                            <SelectedRowStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                                                            <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                CssClass="bg_GridHeader" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                                                                BorderWidth="0px" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 12px; width: 15px">
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="labelSpace5" runat="server" Width="20px">&nbsp;</asp:Label>
                    <%--<asp:Button ID="buttonReturn" runat="server" Text="Return" Width="80px" Visible="false" />--%>
                </td>
            </tr>
        </table>
    </div>
    <br />
</body>
