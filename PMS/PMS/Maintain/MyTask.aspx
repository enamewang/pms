<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyTask.aspx.cs" Inherits="PMS.PMS.Maintain.MyTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>My Task</title>

    <script src="../Javascript/sdp.js" type="text/javascript"></script>

    <script src="../Javascript/PmsCommonJSFuction.js" type="text/javascript"></script>

    <link href="../../Style/MyTaskStyle.css" type="text/css" rel="stylesheet" />
    <base target="_self" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="Sat, 1 Jan 2000 00:00:00 GMT" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <div style="display:none ">
        <asp:Button ID="ButtonRefresh" runat="server"  />
    </div>
    <div>
        <b>Past Task</b>
        <ul class="myTaskUl">
            <asp:DataList runat="server" ID="DataListMyTaskPast" AlternatingItemStyle-BackColor="#F1F1F1"
                RepeatColumns="1" RepeatDirection="horizontal" RepeatLayout="Table" OnItemDataBound="DataListMyTaskPast_ItemDataBound"
                ShowHeader="true">
                <HeaderTemplate>
                    <div class="example">
                        <span class="span">
                            <asp:Label ID="LabelPlanStartDate" runat="server" ToolTip="Plan Start Date" Text='Plan Start Day'> </asp:Label></span>
                        <span class="span">
                            <asp:Label ID="LabelPlanEndDate" runat="server" ToolTip="Plan End Date" Text='Plan End Day'> </asp:Label></span>
                        <span class="crBreak" style="padding-left: 0px;">
                            <asp:Label ID="LabelProject" runat="server" ToolTip="View Project" Text='CR Name'> </asp:Label>
                        </span>
                        <span class="phaseBreak" style="padding-left: 0px;">
                            <asp:Label ID="LabelPhase" runat="server" ToolTip="Phase" Text='Phase'> </asp:Label>
                        </span>
                        <span class="taskStatusBreak" style="padding-left: 0px;">
                            <asp:Label ID="LabelTaskStatus" runat="server" ToolTip="Task Status" Text='Task Status'> </asp:Label>
                        </span>
                        <span class="myTaskBreak">
                            <asp:Label ID="LabelTaskName" runat="server" ToolTip="Edit My Task" Text='Task Name'> </asp:Label>
                        </span>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="HiddenFieldSerial" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Serial")%>' />
                    <asp:HiddenField ID="HiddenFieldHeadSerial" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PMSId")%>' />
                    <span class="span">
                        <asp:Label ID="LabelPlanStartDate" runat="server" ToolTip="Plan Start Date" Text='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}")%>'> </asp:Label></span>
                    <span class="span"><b>~</b></span> <span class="span">
                        <asp:Label ID="LabelPlanEndDate" runat="server" ToolTip="Plan End Date" Text='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}")%>'> </asp:Label></span>
                    <span class="crBreak"><b>
                        <asp:Label ID="LabelProject" runat="server" ToolTip="View Project" Font-Underline="True"
                            Text='<%# DataBinder.Eval(Container.DataItem, "PMSName")%>'> </asp:Label>
                        <%-- <%# DataBinder.Eval(Container.DataItem, "CrName")%>--%>
                    </b>&nbsp;</span> <span class="phaseBreak">
                        <asp:Label ID="LabelPhase" ToolTip="Phase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Phase")%>'> </asp:Label>
                    </span><span class="taskStatusBreak itemStyle" >
                        <asp:Label ID="LabelTaskStatus" ToolTip="Task Status" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaskStatus")%>'> </asp:Label>
                    </span><span class="myTaskBreak"><b>
                        <asp:Label ID="LabelTaskName" ToolTip="Edit My Task" runat="server" Font-Underline="True"
                            Text='<%# DataBinder.Eval(Container.DataItem, "TaskName")%>'> </asp:Label>
                    </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </ItemTemplate>
            </asp:DataList>
        </ul>
        <b>Today Task</b>
        <ul class="myTaskUl">
            <asp:DataList runat="server" ID="DataListMyTaskToday" AlternatingItemStyle-BackColor="#F1F1F1"
                RepeatColumns="1" RepeatDirection="horizontal" RepeatLayout="Table" OnItemDataBound="DataListMyTaskToday_ItemDataBound"
                ShowHeader="true">
                <HeaderTemplate>
                    <div class="example">
                        <span class="span">
                            <asp:Label ID="LabelPlanStartDate" runat="server" ToolTip="Plan Start Date" Text='Plan Start Day'> </asp:Label></span>
                        <span class="span">
                            <asp:Label ID="LabelPlanEndDate" runat="server" ToolTip="Plan End Date" Text='Plan End Day'> </asp:Label></span>
                        <span class="crBreak" style="padding-left: 0px;">
                            <asp:Label ID="LabelProject" runat="server" ToolTip="View Project" Text='CR Name'> </asp:Label>
                            &nbsp;</span> <span class="phaseBreak">
                                <asp:Label ID="LabelPhase" ToolTip="Phase" runat="server" Text='Phase'> </asp:Label></span>
                        <span class="taskStatusBreak" style="padding-left: 0px;">
                            <asp:Label ID="LabelTaskStatus" runat="server" ToolTip="Task Status" Text='Task Status'> </asp:Label>
                        </span><span class="myTaskBreak">
                            <asp:Label ID="LabelTaskName" ToolTip="Edit My Task" runat="server" Text='Task Name'> </asp:Label></span>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="HiddenFieldSerial" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Serial")%>' />
                    <asp:HiddenField ID="HiddenFieldHeadSerial" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PMSId")%>' />
                    <span class="span">
                        <asp:Label ID="LabelPlanStartDate" runat="server" ToolTip="Plan Start Date" Text='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}")%>'> </asp:Label></span>
                    <span class="span"><b>~</b></span> <span class="span">
                        <asp:Label ID="LabelPlanEndDate" runat="server" ToolTip="Plan End Date" Text='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}")%>'> </asp:Label></span>
                    <span class="crBreak"><b>
                        <asp:Label ID="LabelProject" runat="server" ToolTip="View Project" Font-Underline="True"
                            Text='<%# DataBinder.Eval(Container.DataItem, "PMSName")%>'> </asp:Label>
                        <%-- <%# DataBinder.Eval(Container.DataItem, "CrName")%>--%>
                    </b>&nbsp;</span> <span class="phaseBreak">
                        <asp:Label ID="LabelPhase" ToolTip="Phase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Phase")%>'> </asp:Label></span>
                    <span class="taskStatusBreak itemStyle">
                        <asp:Label ID="LabelTaskStatus" ToolTip="Task Status" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaskStatus")%>'> </asp:Label>
                    </span><span class="myTaskBreak"><b>
                        <asp:Label ID="LabelTaskName" ToolTip="Edit My Task" runat="server" Font-Underline="True"
                            Text='<%# DataBinder.Eval(Container.DataItem, "TaskName")%>'> </asp:Label>
                    </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </ItemTemplate>
            </asp:DataList>
        </ul>
        <b>Future Task</b>
        <ul class="myTaskUl">
            <asp:DataList runat="server" ID="DataListMyTaskFuture" AlternatingItemStyle-BackColor="#F1F1F1"
                RepeatColumns="1" RepeatDirection="horizontal" RepeatLayout="Table" OnItemDataBound="DataListMyTaskFuture_ItemDataBound">
                <HeaderTemplate>
                    <div class="example">
                        <span class="span">
                            <asp:Label ID="LabelPlanStartDate" runat="server" ToolTip="Plan Start Date" Text='Plan Start Day'> </asp:Label></span>
                        <span class="span">
                            <asp:Label ID="LabelPlanEndDate" runat="server" ToolTip="Plan End Date" Text='Plan End Day'> </asp:Label></span>
                        <span class="crBreak" style="padding-left: 0px;">
                            <asp:Label ID="LabelProject" runat="server" ToolTip="View Project" Text='CR Name'> </asp:Label>
                            &nbsp;</span> <span class="phaseBreak">
                                <asp:Label ID="LabelPhase" ToolTip="Phase" runat="server" Text='Phase'> </asp:Label></span>
                        <span class="taskStatusBreak" style="padding-left: 0px;">
                            <asp:Label ID="LabelTaskStatus" runat="server" ToolTip="Task Status" Text='Task Status'> </asp:Label>
                        </span><span class="myTaskBreak">
                            <asp:Label ID="LabelTaskName" ToolTip="Edit My Task" runat="server" Text='Task Name'> </asp:Label></span>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="HiddenFieldSerial" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Serial")%>' />
                    <asp:HiddenField ID="HiddenFieldHeadSerial" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PMSId")%>' />
                    <span class="span">
                        <asp:Label ID="LabelPlanStartDate" runat="server" ToolTip="Plan Start Date" Text='<%# DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}")%>'> </asp:Label></span>
                    <span class="span"><b>~</b></span> <span class="span">
                        <asp:Label ID="LabelPlanEndDate" runat="server" ToolTip="Plan End Date" Text='<%# DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}")%>'> </asp:Label></span>
                    <span class="crBreak"><b>
                        <asp:Label ID="LabelProject" runat="server" ToolTip="View Project" Font-Underline="True"
                            Text='<%# DataBinder.Eval(Container.DataItem, "PMSName")%>'> </asp:Label>
                        <%-- <%# DataBinder.Eval(Container.DataItem, "CrName")%>--%>
                    </b>&nbsp;</span> <span class="phaseBreak">
                        <asp:Label ID="LabelPhase" ToolTip="Phase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Phase")%>'> </asp:Label></span>
                    <span class="taskStatusBreak itemStyle">
                        <asp:Label ID="LabelTaskStatus" ToolTip="Task Status" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaskStatus")%>'> </asp:Label>
                    </span><span class="myTaskBreak"><b>
                        <asp:Label ID="LabelTaskName" ToolTip="Edit My Task" runat="server" Font-Underline="True"
                            Text='<%# DataBinder.Eval(Container.DataItem, "TaskName")%>'> </asp:Label>
                    </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </ItemTemplate>
            </asp:DataList>
        </ul>
        <div id="DivPmoTaskAgent" runat="server" visible="false">
            <b>Waiting Schedule Plan Agent Approve CR List</b>
            <ul class="myTaskUl">
                <anthem:GridView ID="gridViewTaskAgent" CssClass="DIVGrid" runat="server" AllowPaging="false"
                    AllowSorting="true" AutoGenerateColumns="False" BorderColor="Gray" BorderStyle="Solid"
                    BorderWidth="1px" CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt"
                    ForeColor="Gray" GridLines="Horizontal" Width="1050px" EmptyDataText="No data."
                    DataKeyNames="PmsId" OnRowCreated="gridViewTaskAgent_RowCreated">
                    <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                    <PagerSettings Position="TopAndBottom" Visible="False" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Detail">
                            <ItemTemplate>
                                <div style="width: 50px; text-align: center;">
                                    <asp:ImageButton ID="imageButtonDetail" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Select" ImageUrl="~/SysFrame/images/detail.gif" ToolTip="Detail">
                                    </asp:ImageButton>
                                </div>
                            </ItemTemplate>
                            <%-- <headerstyle width="40px" />
                                <itemstyle width="40px" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CR No" SortExpression="CrId">
                            <ItemTemplate>
                                <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CrId").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CrId").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" SortExpression="Type">
                            <ItemTemplate>
                                <div style="width: 55px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="55px" />
                            <ItemStyle Width="55px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CR Name" SortExpression="PmsName">
                            <ItemTemplate>
                                <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsName").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsName").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%" SortExpression="Progress">
                            <ItemTemplate>
                                <div style="width: 55px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Progress").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Progress").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" HorizontalAlign="Center" />
                            <ItemStyle Width="55px" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate">
                            <ItemTemplate>
                                <div style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "DueDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":DataBinder.Eval(Container.DataItem, "DueDate","{0:yyyy-MM-dd}"))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}")).ToString() == "0001-01-01" ? "" : DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}"))%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="78px" HorizontalAlign="Center" />
                            <ItemStyle Width="85px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Release Date" SortExpression="ReleaseDate">
                            <ItemTemplate>
                                <div style="width: 90px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "ReleaseDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":DataBinder.Eval(Container.DataItem, "ReleaseDate","{0:yyyy-MM-dd}"))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "ReleaseDate", "{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":(DataBinder.Eval(Container.DataItem, "ReleaseDate", "{0:yyyy-MM-dd}")).ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="90px" HorizontalAlign="Center" />
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Create Date" SortExpression="CreateDate">
                            <ItemTemplate>
                                <div style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "CreateDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":(DataBinder.Eval(Container.DataItem, "CreateDate","{0:yyyy-MM-dd}")))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")).ToString() == "0001-01-01" ? "" : (DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")))%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="85px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage" SortExpression="StageName">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StageName").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StageName").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                            <ItemTemplate>
                                <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SD" SortExpression="Sd">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="65px" />
                            <ItemStyle Width="75px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="System" SortExpression="System">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="65px" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderTemplate>
                                <span>PmsId</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsId").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsId").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                        BorderWidth="0px" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                    <%--<HeaderStyle BorderStyle="None" Font-Bold="True" HorizontalAlign="Left" CssClass="GridHead" />--%>
                    <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                        CssClass="bg_GridHeader" BackColor="#a6bce6" />
                    <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                        BorderWidth="0px" />
                </anthem:GridView>
            </ul>
        </div>
        <div id="DivPmoTaskAudit" runat="server" visible="false">
            <b>Waiting Schedule Plan Approve CR List</b>
            <ul class="myTaskUl">
                <anthem:GridView ID="gridViewTaskAudit" CssClass="DIVGrid" runat="server" AllowPaging="false"
                    AllowSorting="true" AutoGenerateColumns="False" BorderColor="Gray" BorderStyle="Solid"
                    BorderWidth="1px" CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt"
                    ForeColor="Gray" GridLines="Horizontal" Width="1050px" EmptyDataText="No data."
                    DataKeyNames="PmsId" OnRowCreated="gridViewTaskAudit_RowCreated">
                    <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                    <PagerSettings Position="TopAndBottom" Visible="False" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Detail">
                            <ItemTemplate>
                                <div style="width: 50px; text-align: center;">
                                    <asp:ImageButton ID="imageButtonDetail" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Select" ImageUrl="~/SysFrame/images/detail.gif" ToolTip="Detail">
                                    </asp:ImageButton>
                                </div>
                            </ItemTemplate>
                            <%-- <headerstyle width="40px" />
                                <itemstyle width="40px" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CR No" SortExpression="CrId">
                            <ItemTemplate>
                                <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CrId").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CrId").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" SortExpression="Type">
                            <ItemTemplate>
                                <div style="width: 55px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="55px" />
                            <ItemStyle Width="55px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CR Name" SortExpression="PmsName">
                            <ItemTemplate>
                                <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsName").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsName").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%" SortExpression="Progress">
                            <ItemTemplate>
                                <div style="width: 55px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Progress").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Progress").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" HorizontalAlign="Center" />
                            <ItemStyle Width="55px" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate">
                            <ItemTemplate>
                                <div style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "DueDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":DataBinder.Eval(Container.DataItem, "DueDate","{0:yyyy-MM-dd}"))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}")).ToString() == "0001-01-01" ? "" : DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}"))%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="78px" HorizontalAlign="Center" />
                            <ItemStyle Width="85px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Release Date" SortExpression="ReleaseDate">
                            <ItemTemplate>
                                <div style="width: 90px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "ReleaseDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":DataBinder.Eval(Container.DataItem, "ReleaseDate","{0:yyyy-MM-dd}"))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "ReleaseDate", "{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":(DataBinder.Eval(Container.DataItem, "ReleaseDate", "{0:yyyy-MM-dd}")).ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="90px" HorizontalAlign="Center" />
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Create Date" SortExpression="CreateDate">
                            <ItemTemplate>
                                <div style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "CreateDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":(DataBinder.Eval(Container.DataItem, "CreateDate","{0:yyyy-MM-dd}")))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")).ToString() == "0001-01-01" ? "" : (DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")))%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="85px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage" SortExpression="StageName">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StageName").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StageName").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                            <ItemTemplate>
                                <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SD" SortExpression="Sd">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="65px" />
                            <ItemStyle Width="75px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="System" SortExpression="System">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="65px" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderTemplate>
                                <span>PmsId</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsId").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsId").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                        BorderWidth="0px" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                    <%--<HeaderStyle BorderStyle="None" Font-Bold="True" HorizontalAlign="Left" CssClass="GridHead" />--%>
                    <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                        CssClass="bg_GridHeader" BackColor="#a6bce6" />
                    <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                        BorderWidth="0px" />
                </anthem:GridView>
            </ul>
        </div>
        <div id="DivPmoTask" runat="server" visible="false">
            <b>Waiting Assign Member CR List</b>
            <ul class="myTaskUl">
                <anthem:GridView ID="gridViewMain" CssClass="DIVGrid" runat="server" AllowPaging="false"
                    AllowSorting="true" AutoGenerateColumns="False" BorderColor="Gray" BorderStyle="Solid"
                    BorderWidth="1px" CellPadding="3" Font-Names="Times New Roman" Font-Size="9pt"
                    ForeColor="Gray" GridLines="Horizontal" Width="1050px" EmptyDataText="No data."
                    DataKeyNames="PmsId" OnSorting="gridViewMain_OnSorting" OnRowCreated="gridViewMain_RowCreated">
                    <EmptyDataRowStyle BackColor="LightYellow" ForeColor="Black" />
                    <PagerSettings Position="TopAndBottom" Visible="False" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Detail">
                            <ItemTemplate>
                                <div style="width: 50px; text-align: center;">
                                    <asp:ImageButton ID="imageButtonDetail" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Select" ImageUrl="~/SysFrame/images/detail.gif" ToolTip="Detail">
                                    </asp:ImageButton>
                                </div>
                            </ItemTemplate>
                            <%-- <headerstyle width="40px" />
                                <itemstyle width="40px" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CR No" SortExpression="CrId">
                            <ItemTemplate>
                                <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CrId").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CrId").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" SortExpression="Type">
                            <ItemTemplate>
                                <div style="width: 55px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Type").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="55px" />
                            <ItemStyle Width="55px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CR Name" SortExpression="PmsName">
                            <ItemTemplate>
                                <div style="width: 120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsName").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsName").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%" SortExpression="Progress">
                            <ItemTemplate>
                                <div style="width: 55px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Progress").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Progress").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" HorizontalAlign="Center" />
                            <ItemStyle Width="55px" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate">
                            <ItemTemplate>
                                <div style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "DueDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":DataBinder.Eval(Container.DataItem, "DueDate","{0:yyyy-MM-dd}"))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}")).ToString() == "0001-01-01" ? "" : DataBinder.Eval(Container.DataItem, "DueDate", "{0:yyyy-MM-dd}"))%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="78px" HorizontalAlign="Center" />
                            <ItemStyle Width="85px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Release Date" SortExpression="ReleaseDate">
                            <ItemTemplate>
                                <div style="width: 90px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "ReleaseDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":DataBinder.Eval(Container.DataItem, "ReleaseDate","{0:yyyy-MM-dd}"))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "ReleaseDate", "{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":(DataBinder.Eval(Container.DataItem, "ReleaseDate", "{0:yyyy-MM-dd}")).ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="90px" HorizontalAlign="Center" />
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Create Date" SortExpression="CreateDate">
                            <ItemTemplate>
                                <div style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "CreateDate","{0:yyyy-MM-dd}")).ToString()=="0001-01-01"?"":(DataBinder.Eval(Container.DataItem, "CreateDate","{0:yyyy-MM-dd}")))%>'>
                                    <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")).ToString() == "0001-01-01" ? "" : (DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")))%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="85px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage" SortExpression="StageName">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StageName").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "StageName").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                            <ItemTemplate>
                                <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Priority").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SD" SortExpression="Sd">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Sd").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="65px" />
                            <ItemStyle Width="75px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="System" SortExpression="System">
                            <ItemTemplate>
                                <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "System").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="65px" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderTemplate>
                                <span>PmsId</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="width: 50px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                                    title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsId").ToString())%>'>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PmsId").ToString())%>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle ForeColor="DimGray" BackColor="LightYellow" Font-Size="9pt" BorderStyle="None"
                        BorderWidth="0px" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gainsboro" BorderStyle="None" ForeColor="DarkSlateBlue" HorizontalAlign="Right" />
                    <%--<HeaderStyle BorderStyle="None" Font-Bold="True" HorizontalAlign="Left" CssClass="GridHead" />--%>
                    <HeaderStyle BorderStyle="None" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                        CssClass="bg_GridHeader" BackColor="#a6bce6" />
                    <AlternatingRowStyle BackColor="White" ForeColor="DimGray" Font-Size="9pt" BorderStyle="None"
                        BorderWidth="0px" />
                </anthem:GridView>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
