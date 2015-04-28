<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchedulePlanByPhase.ascx.cs"
    Inherits="PMS.PMS.UserControls.SchedulePlanByPhase" %>
<style type="text/css">
    .hidden
    {
        display: none;
    }
</style>

<script src="../JavaScript/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>

<script type="text/javascript">

    function SelectAll(checkBoxSelectAll) {
        debugger;
        var gridViewByPhase = checkBoxSelectAll.parentNode.parentNode.parentNode;
        var checkBox = $("input[name = 'CheckItem']", gridViewByPhase);
        var checkBoxs = $(gridViewByPhase).find("tr:visible").find("input[name = 'CheckItem']");
        checkBoxs.each(function() {
            checkBoxSelectAll.checked == true ? $(this).attr("checked", true) : $(this).attr("checked", false);
        });
    }

    function ShowTaskDetail(divTaskNo) {

        var pmsId = "<%=PmsID %>";
        var crId = "<%=CrId %>";
        var tr = $(divTaskNo).closest("tr")
        var serial = tr.attr("serial");
        var phase = tr.attr("phase");
        var currentElement = window.event.srcElement;
        var positionX = CalculateLeft(currentElement);
        var positionY = CalculateTop(currentElement);

        var url = "../Maintain/ViewTask.aspx?PmsID=" + pmsId + "&CrId=" + crId + "&Phase=" + phase + "&Serial=" + serial + "&Radom=" + Math.random();
        $.get(url, function(result) {

            var divTaskDetail = document.getElementById("divTaskDetail");
            divTaskDetail.style.left = positionX + 10;
            divTaskDetail.style.top = positionY + 20;
            divTaskDetail.style.display = "block";
            divTaskDetail.innerHTML = result;
        })
    }

    function HideTaskDetail() {
        $("#divTaskDetail").hide();
    }

    function InsertDesignTableRow(t) {
        InsertRow(t, 4);
        return false
    }
    function InsertDevelopmentTableRow(t) {
        InsertRow(t, 5);
        return false
    }

    function InsertTestTableRow(t) {
        InsertRow(t, 6);
        return false
    }

    function InsertReleaseTableRow(t) {
        InsertRow(t, 7);
        return false
    }

    function InsertSupportTableRow(t) {
        InsertRow(t, 8);
        return false
    }

    function InsertRow(t, phase) {

        var pmsId = "<%=PmsID %>";
        var crId = "<%=CrId %>";
        var url = "../Maintain/AddNewTask.aspx?Action=Add&PmsID=" + pmsId + "&CrId=" + crId + "&Phase=" + phase + "&Radom=" + Math.random();
        var features = "dialogWidth=630px;dialogHeight=510px;center=yes;help=no;status=no;scroll=no";
        var dialogArguments = new Object();
        dialogArguments.window = window;
        dialogArguments.tableSelector = t;
        window.showModalDialog(url, dialogArguments, features);
    }

    function InsertRowShow(result, tableSelector) {

        if (result == undefined || result == null) {
            return;
        }

        var table = $(tableSelector);

        //check table exist
        if (table == null) {
            return;
        }

        //clone last tr append to table
        var tr = $(table).find("tr:last").clone();
        tr.appendTo(table);
        //replace new tr
        tr = $(table).find("tr:last");
        tr.show();
        tr.attr({ "serial": result.Serial, "createuser": result.CreateUser.toUpperCase(), "Auditor": result.Auditor.toUpperCase(), "AuditorAgent": result.AuditorAgent.toUpperCase(), "Resource": result.Resource.toUpperCase(), "phase": result.Phase, "auditstatus": result.AuditStatus, "taskstatus": result.TaskStatus });
        tr.find("#divTaskNo").text($(table).find("tr").size() - 1);
        tr.find("#divTaskNo").hover(ShowTaskDetail(this), HideTaskDetail()).css("cursor", "pointer");
        tr.find("#divTaskType").text(result.TaskTypeDesc);
        tr.find("#divTaskType").attr("title", result.TaskTypeDesc);
        tr.find("#divTaskName").text(result.TaskName);
        tr.find("#divTaskName").attr("title", result.TaskName);
        tr.find("#divFunctionType").text(result.FunctionTypeDesc);
        tr.find("#divFunctionType").attr("title", result.FunctionTypeDesc);
        tr.find("#divAuditStatus").text(result.AuditStatusDesc);
        tr.find("#divAuditStatus").attr("title", result.AuditStatusDesc);
        tr.find("#divTaskStatus").text(result.TaskStatusDesc);
        tr.find("#divTaskStatus").attr("title", result.TaskStatusDesc);
        tr.find("#divPlanStart").text(result.PlanStart);
        tr.find("#divPlanStart").attr("title", result.PlanStart);
        tr.find("#divPlanEnd").text(result.PlanEnd);
        tr.find("#divPlanEnd").attr("title", result.PlanEnd);
        tr.find("#divPlanCost").text(result.PlanCost);
        tr.find("#divPlanCost").attr("title", result.PlanCost);
        tr.find("#divRefCost").text(result.RefCost);
        tr.find("#divRefCost").attr("title", result.RefCost);
        tr.find("#divRole").text(result.Role);
        tr.find("#divRole").attr("title", result.Role);
        tr.find("#divResource").text(result.Resource);
        tr.find("#divResource").attr("title", result.Resource);

        //重新计算Head上的参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均
        SetSchedulePlanAllPhaseHead();
    }


    function EditRow(oImgEdit) {

        var pmsId = "<%=PmsID %>";
        var crId = "<%=CrId %>";
        var tr = $(oImgEdit).closest("tr");
        var serial = tr.attr("serial");
        var phase = tr.attr("phase");

        //只有创建者才能编辑
        var creator = $(oImgEdit).closest("tr").attr("createuser");
        if ($.trim(creator) != "" && creator != LoginName) {
            alert("You can not edit it, you are not creator!");
            return;
        }

        var url = "../Maintain/EditWaitingSubmitTask.aspx?PmsID=" + pmsId + "&CrId=" + crId + "&Phase=" + phase + "&Serial=" + serial + "&Radom=" + Math.random();
        var features = "dialogWidth=630px;dialogHeight=520px;center=yes;help=no;status=no;scroll=no";
        var result = window.showModalDialog(url, window, features);

        if (result == undefined || result == null) {
            return;
        }
        tr.attr({ "serial": result.Serial, "createuser": result.CreateUser.toUpperCase(), "Auditor": result.Auditor.toUpperCase(), "Resource": result.Resource.toUpperCase(), "phase": phase, "auditstatus": result.AuditStatus, "taskstatus": result.TaskStatus });
        tr.find("#divTaskType").text(result.TaskTypeDesc);
        tr.find("#divTaskType").attr("title", result.TaskTypeDesc);
        tr.find("#divTaskName").text(result.TaskName);
        tr.find("#divTaskName").attr("title", result.TaskName);
        tr.find("#divFunctionType").text(result.FunctionTypeDesc);
        tr.find("#divFunctionType").attr("title", result.FunctionTypeDesc);
        tr.find("#divAuditStatus").text(result.AuditStatusDesc);
        tr.find("#divAuditStatus").attr("title", result.AuditStatusDesc);
        tr.find("#divTaskStatus").text(result.TaskStatusDesc);
        tr.find("#divTaskStatus").attr("title", result.TaskStatusDesc);
        tr.find("#divPlanStart").text(result.PlanStart);
        tr.find("#divPlanStart").attr("title", result.PlanStart);
        tr.find("#divPlanEnd").text(result.PlanEnd);
        tr.find("#divPlanEnd").attr("title", result.PlanEnd);
        tr.find("#divPlanCost").text(result.PlanCost);
        tr.find("#divPlanCost").attr("title", result.PlanCost);
        tr.find("#divRefCost").text(result.RefCost);
        tr.find("#divRefCost").attr("title", result.RefCost);
        tr.find("#divRole").text(result.Role);
        tr.find("#divRole").attr("title", result.Role);
        tr.find("#divResource").text(result.Resource);
        tr.find("#divResource").attr("title", result.Resource);

        //重新计算Head上的参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均
        SetSchedulePlanAllPhaseHead();
    }

    function DeleteRow(oImgDelete) {

        if ($(oImgDelete).closest("table").find("tr").size() == 2) {
            alert("this is the last item,can not be deleted!");
            return;
        }

        //只有创建者才能删除
        var creator = $(oImgDelete).closest("tr").attr("createuser");
        var auditStatus = $(oImgDelete).closest("tr").attr("auditstatus");
        var taskStatus = $(oImgDelete).closest("tr").attr("taskstatus");

        if ($.trim(creator) != "" && creator != LoginName) {
            alert("You can not delete it, you are not creator!");
            return;
        }
        if (auditStatus != 1) {
            alert("You can't delete the task beacause it has been submitted or approved!");
            return;
        }
        if (taskStatus != 1) {
            alert("You can't delete the task beacause it has manpower already!");
            return;
        }
        if (confirm("Are you sure to delete this item")) {

            var serial = $(oImgDelete).closest("tr").attr("serial");

            //Jquery ajax 调用 WebService           
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../WebService/SchedulePlanByPhase.asmx/DeleteSdpDetail",
                data: "{serial:'" + serial + "'}",
                dataType: 'json',
                success: function(result) {
                    if (result) {
                        $(oImgDelete).closest("tr").remove();
                        //重新计算Head上的参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均
                        SetSchedulePlanAllPhaseHead();
                    } else {
                        alert("Delete this item failed!");
                    }
                }
            });
        }
    }

    function CopyRow(oImgCopy) {

        var pmsId = "<%=PmsID %>";
        var crId = "<%=CrId %>";
        var table = $(oImgCopy).closest("table");
        var trThis = $(oImgCopy).closest("tr");
        var serial = trThis.attr("serial");
        var phase = trThis.attr("phase");

        var url = "../Maintain/AddNewTask.aspx?Action=Copy&PmsID=" + pmsId + "&CrId=" + crId + "&Phase=" + phase + "&Serial=" + serial + "&Radom=" + Math.random();
        var features = "dialogWidth=630px;dialogHeight=520px;center=yes;help=no;status=no;scroll=no";
        var result = window.showModalDialog(url, "", features);

        if (result == undefined || result == null) {
            return;
        }

        //clone last tr append to table        
        var tr = $(table).find("tr:last").clone();
        tr.appendTo(table);

        //replace new tr
        tr = $(table).find("tr:last");
        tr.show();
        tr.attr({ "serial": result.Serial, "createuser": result.CreateUser.toUpperCase(), "Auditor": result.Auditor.toUpperCase(), "AuditorAgent": result.AuditorAgent.toUpperCase(), "Resource": result.Resource.toUpperCase(), "phase": phase, "auditstatus": result.AuditStatus });
        tr.find("#divTaskNo").text($(table).find("tr").size() - 1);
        tr.find("#divTaskNo").hover(ShowTaskDetail(this), HideTaskDetail()).css("cursor", "pointer");
        tr.find("#divTaskType").text(result.TaskTypeDesc);
        tr.find("#divTaskType").attr("title", result.TaskTypeDesc);
        tr.find("#divTaskName").text(result.TaskName);
        tr.find("#divTaskName").attr("title", result.TaskName);
        tr.find("#divFunctionType").text(result.FunctionTypeDesc);
        tr.find("#divFunctionType").attr("title", result.FunctionTypeDesc);
        tr.find("#divAuditStatus").text(result.AuditStatusDesc);
        tr.find("#divAuditStatus").attr("title", result.AuditStatusDesc);
        tr.find("#divTaskStatus").text(result.TaskStatusDesc);
        tr.find("#divTaskStatus").attr("title", result.TaskStatusDesc);
        tr.find("#divPlanStart").text(result.PlanStart);
        tr.find("#divPlanStart").attr("title", result.PlanStart);
        tr.find("#divPlanEnd").text(result.PlanEnd);
        tr.find("#divPlanEnd").attr("title", result.PlanEnd);
        tr.find("#divPlanCost").text(result.PlanCost);
        tr.find("#divPlanCost").attr("title", result.PlanCost);
        tr.find("#divRefCost").text(result.RefCost);
        tr.find("#divRefCost").attr("title", result.RefCost);
        tr.find("#divRole").text(result.Role);
        tr.find("#divRole").attr("title", result.Role);
        tr.find("#divResource").text(result.Resource);
        tr.find("#divResource").attr("title", result.Resource);

        //重新计算Head上的参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均
        SetSchedulePlanAllPhaseHead();
    }
    
    
</script>

<div id="divHead">
    <table style="width: 100%; line-height: 25px; height: 25px; background-color: #E0ECFB">
        <tr>
            <td>
                <asp:Image ID="ImageShowHideGrid" runat="server" ImageUrl="~/SysFrame/images/hide.gif"
                    Style="cursor: pointer"></asp:Image>
            </td>
            <td style="width: 95px">
                <asp:Button ID="ButtonAddTask" runat="server" Text="设计阶段" CssClass="ButtonTransparent"
                    onmouseover="this.className='ButtonBackground'" onmouseout="this.className='ButtonTransparent'"
                    ForeColor="Black" Style="text-align: left; width: 85px" />
                <input type="hidden" id="HiddenPhase" runat="server" />
            </td>
            <td>
                <span class="SpanPhase">参考总工时：</span>
            </td>
            <td style="width: 30px">
                <div>
                    <asp:Label ID="LabelPhaseRefCost" name="PhaseRefCost" runat="server" ForeColor="#EA6F34"> </asp:Label>
                </div>
                <div id="divPhaseLastRefCost" style="display: none">
                    <asp:Label ID="Label1" name="PhaseLastRefCost" runat="server" ForeColor="Blue" Text="56"> </asp:Label>
                </div>
            </td>
            <td>
                <span class="SpanPhase">计划总工时：</span>
            </td>
            <td style="width: 30px">
                <div>
                    <asp:Label ID="LabelPhasePlanCost" name="PhasePlanCost" runat="server" ForeColor="#EA6F34"></asp:Label>
                </div>
                <div id="divPhaseLastPlanCost" style="display: none">
                    <asp:Label ID="Label2" name="PhaseLastPlanCost" runat="server" ForeColor="Blue"> </asp:Label>
                </div>
            </td>
            <td>
                <span class="SpanPhase">计划总工期：</span>
            </td>
            <td style="width: 25px">
                <div>
                    <asp:Label ID="LabelPhasePlanDay" runat="server" name="PhasePlanDay" ForeColor="#EA6F34"></asp:Label>
                </div>
                <div id="divPhaseLastPlanDay" style="display: none">
                    <asp:Label ID="Label3" runat="server" name="PhaseLastPlanDay" ForeColor="Blue"></asp:Label>
                </div>
            </td>
            <td>
                <span class="SpanPhase">计划开始：</span>
            </td>
            <td style="width: 80px">
                <div>
                    <asp:Label ID="LabelPhasePlanStart" name="PhasePlanStart" runat="server" ForeColor="#EA6F34"></asp:Label>
                </div>
                <div id="divPhaseLastPlanStart" style="display: none">
                    <asp:Label ID="Label4" name="PhaseLastPlanStart" runat="server" ForeColor="Blue"></asp:Label>
                </div>
            </td>
            <td>
                <span class="SpanPhase">计划结束：</span>
            </td>
            <td style="width: 80px">
                <div>
                    <asp:Label ID="LabelPhasePlanEnd" runat="server" name="PhasePlanEnd" ForeColor="#EA6F34"></asp:Label>
                </div>
                <div id="divPhaseLastPlanEnd" style="display: none">
                    <asp:Label ID="Label5" runat="server" name="PhaseLastPlanEnd" ForeColor="Blue"></asp:Label>
                </div>
            </td>
            <td>
                <span class="SpanPhase">计划平均：</span>
            </td>
            <td style="width: 50px">
                <div>
                    <asp:Label ID="LabelPhasePlanAverage" runat="server" name="PhasePlanAverage" ForeColor="#EA6F34"></asp:Label>
                </div>
                <div id="divPhaseLastPlanAverage" style="display: none">
                    <asp:Label ID="Label6" runat="server" name="PhaseLastPlanAverage" ForeColor="Blue"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="divGrid" runat="server" style="display: none">
    <asp:GridView ID="GridViewByPhase" runat="server" Width="100%" AutoGenerateColumns="False"
        DataKeyNames="Serial" AllowPaging="False" selectmark="ScheduleGridView" OnRowDataBound="GridViewByPhase_OnRowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Select">
                <HeaderTemplate>
                    <input type="checkbox" onclick="SelectAll(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" name="CheckItem" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <div id="divTaskNo" style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                        text-align: center; cursor: pointer" onmouseover="ShowTaskDetail(this)" onmouseout="HideTaskDetail()">
                        <%# Container.DataItemIndex + 1%>
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="30px"></HeaderStyle>
                <ItemStyle Width="30px" />
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="任务类型">
                <ItemTemplate>
                    <div id="divTaskType" style="width: 60px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskTypeDesc").ToString())%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskTypeDesc").ToString())%>
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle Width="60px" />
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="任务名称">
                <ItemTemplate>
                    <div id="divTaskName" style="width: 120px; overflow: hidden; white-space: normal;
                        text-overflow: ellipsis; text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle Width="120px" />
                <HeaderStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能分类">
                <ItemTemplate>
                    <div id="divFunctionType" style="width: 52px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FunctionTypeDesc").ToString())%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FunctionTypeDesc").ToString())%>
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle Width="52px" />
                <HeaderStyle Width="52px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="审核状态">
                <ItemTemplate>
                    <div id="divAuditStatus" style="width: 52px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AuditStatusDesc").ToString())%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AuditStatusDesc").ToString())%>
                        <input type="hidden" name="AuditStatus" value="<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "AuditStatus").ToString())%>" />
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle Width="52px" />
                <HeaderStyle Width="52px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="任务状态">
                <ItemTemplate>
                    <div id="divTaskStatus" style="width: 52px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskStatusDesc").ToString())%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskStatusDesc").ToString())%>
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle Width="52px" />
                <HeaderStyle Width="52px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划开始">
                <ItemTemplate>
                    <div id="divPlanStart" style="width: 75px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis" title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}"))%>'>
                        <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}"))%>
                    </div>
                    <div id="divLastPlanStart" style="width: 75px; overflow: hidden; white-space: nowrap;
                        color: Blue; display: none; text-overflow: ellipsis" title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "LastPlanStartDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "LastPlanStartDay", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "LastPlanStartDay","{0:yyyy-MM-dd}"))%>'>
                        <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "LastPlanStartDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "LastPlanStartDay", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : DataBinder.Eval(Container.DataItem, "LastPlanStartDay", "{0:yyyy-MM-dd}"))%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="75px" />
                <HeaderStyle Width="75px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划结束">
                <ItemTemplate>
                    <div id="divPlanEnd" style="width: 75px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                        title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}"))%>'>
                        <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}"))%>
                    </div>
                    <div id="divLastPlanEnd" style="width: 75px; overflow: hidden; white-space: nowrap;
                        color: Blue; display: none; text-overflow: ellipsis" title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "LastPlanEndDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "LastPlanEndDay", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "LastPlanEndDay","{0:yyyy-MM-dd}"))%>'>
                        <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "LastPlanEndDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "LastPlanEndDay", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : DataBinder.Eval(Container.DataItem, "LastPlanEndDay", "{0:yyyy-MM-dd}"))%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="75px" />
                <HeaderStyle Width="75px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划工时">
                <ItemTemplate>
                    <div id="divPlanCost" name="divPlanCost" style="width: 30px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanCost")%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PlanCost").ToString())%>
                    </div>
                    <div id="divLastPlanCost" name="divLastPlanCost" style="width: 30px; overflow: hidden;
                        color: Blue; display: none; white-space: nowrap; text-overflow: ellipsis; text-align: center"
                        title='<%# (DataBinder.Eval(Container.DataItem, "LastPlanCost").ToString() == "0") ? "" : DataBinder.Eval(Container.DataItem, "LastPlanCost").ToString()%>'>
                        <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "LastPlanCost").ToString() == "0") ? "" : DataBinder.Eval(Container.DataItem, "LastPlanCost").ToString())%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="30px" />
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="参考工时">
                <ItemTemplate>
                    <div id="divRefCost" name="divRefCost" style="width: 30px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "RefCost")%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "RefCost").ToString())%>
                    </div>
                    <div id="divLastRefCost" name="divLastRefCost" style="width: 30px; overflow: hidden;
                        color: Blue; display: none; white-space: nowrap; text-overflow: ellipsis; text-align: center"
                        title='<%# (DataBinder.Eval(Container.DataItem, "LastRefCost").ToString() == "0") ? "" : DataBinder.Eval(Container.DataItem, "LastRefCost").ToString()%>'>
                        <%# Server.HtmlEncode((DataBinder.Eval(Container.DataItem, "LastRefCost").ToString() == "0") ? "" : DataBinder.Eval(Container.DataItem, "LastRefCost").ToString())%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="30px" />
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="资源">
                <ItemTemplate>
                    <div id="divResource" style="width: 90px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'>
                        <%# DataBinder.Eval(Container.DataItem, "Resource")%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="90px" />
                <HeaderStyle Width="90px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <div id="divOperation">
                        <div style="width: 20px; text-align: center; float: left" title="Edit">
                            <img src="../../SysFrame/images/ButtonEdit_1.gif" alt="Edit" onclick="EditRow(this)"
                                onmouseover="this.style.cursor='hand'" />
                        </div>
                        <div style="width: 20px; text-align: center; float: left" title="Delete">
                            <img src="../../SysFrame/images/ButtonDelete.gif" alt="Delete" onclick="DeleteRow(this)"
                                onmouseover="this.style.cursor='hand'" />
                        </div>
                        <div style="width: 20px; text-align: center; float: left" title="Copy">
                            <img src="../../SysFrame/images/ButtonYes.gif" alt="Copy" onclick="CopyRow(this)"
                                onmouseover="this.style.cursor='hand'" />
                        </div>
                    </div>
                </ItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<div id="divTaskDetail" style="z-index: 101; position: absolute; background-color: #feffdf;
    padding: 5px; display: none">
</div>
