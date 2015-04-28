<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExecutePlanByPhase.ascx.cs"
    Inherits="PMS.PMS.UserControls.ExecutePlanByPhase" %>
<style type="text/css">
    #divPhaseHead ul
    {
        height: auto;
        float: left;
        list-style: none;
        margin: 0px;
        padding: 0px;
    }
    #divPhaseHead li
    {
        height: auto;
        float: left;
    }
</style>

<script type="text/javascript">

    function ExcutePlanEditRow(oImgEdit) {

        var pmsId = "<%=PmsID %>";
        var crId = "<%=CrId %>";
        var tr = $(oImgEdit).closest("tr");
        var serial = tr.attr("serial");
        var phase = tr.attr("phase");

        var url = "../Maintain/EditApprovedTask.aspx?PmsID=" + pmsId + "&CrId=" + crId + "&Phase=" + phase + "&Serial=" + serial + "&Radom=" + Math.random();
        var features = "dialogWidth=630px;dialogHeight=540px;center=yes;help=no;status=no;scroll=no";
        var result = window.showModalDialog(url, "", features);

        if (result == undefined || result == null) {
            return;
        }

        tr.find("#divActualStart").attr("title", result.ActualStart);
        tr.find("#divActualStart").text(result.ActualStart);
        tr.find("#divActualEnd").attr("title", result.ActualEnd);
        tr.find("#divActualEnd").text(result.ActualEnd);
        tr.find("#divActualCost").attr("title", result.ActualCost);
        tr.find("#divActualCost").text(result.ActualCost);
        tr.find("#divCompletePercent").attr("title", result.CompletePercent);
        tr.find("#divCompletePercent").text(result.CompletePercent);
        tr.find("#divRemark").attr("title", result.Remark);
        tr.find("#divRemark").text(result.Remark);

        //重新计算ExecutePlan的参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均
        SetExecutePlanAllPhaseHead();
    }
    
</script>

<div id="divPhaseHead" style="line-height: 25px; height: 60px; background-color: #E0ECFB;">
    <ul>
        <li style="width: 17px;">
            <img id="imgExceutePlanPhaseShow" alt="Show" src="../../SysFrame/images/hide.gif"
                style="cursor: pointer" />
            <img id="imgExceutePlanPhaseHide" alt="Hide" src="../../SysFrame/images/appear.gif"
                style="cursor: pointer" />
        </li>
        <li style="width: 95px">
            <asp:Label ID="LabelPhaseName" runat="server" Style="background-color: Transparent;
                border: 0px; height: 20px; color: #333;"></asp:Label></li>
        <li style="width: 130px"><span>参考总工时：</span><span id="PhaseRefCost"></span></li>
        <li style="width: 110px"><span>计划总工时：</span><span id="PhasePlanCost"></span> </li>
        <li style="width: 100px; display: none"><span>计划总工期：</span> <span id="PhasePlanDay">
        </span></li>
        <li style="width: 150px"><span>计划开始：</span><span id="PhasePlanStart"></span></li>
        <li style="width: 150px"><span>计划结束：</span> <span id="PhasePlanEnd"></span></li>
        <li style="width: 120px"><span>计划平均：</span> <span id="PhasePlanAverage"></span></li>
        <li style="width: 110px"><span></span><span id="Span1"></span></li>
    </ul>
    <ul>
        <li style="width: 17px; margin-top: 5px"></li>
        <li style="width: 95px; margin-top: 5px"><span></span></li>
        <li style="width: 130px"><span>完成百分比：</span><span id="PhaseCompletePercent"></span></li>
        <li style="width: 110px"><span>实际总工时：</span><span id="PhaseActualCost"></span> </li>
        <li style="width: 100px; display: none"><span>实际总工期：</span> <span id="PhaseActualDay">
        </span></li>
        <li style="width: 150px"><span>实际开始：</span><span id="PhaseActualStart"></span> </li>
        <li style="width: 150px"><span>实际结束：</span> <span id="PhaseActualEnd"></span></li>
        <li style="width: 120px"><span>实际平均：</span> <span id="PhaseActualAverage"></span>
        </li>
        <li style="width: 110px"><span>进度偏差：</span> <span id="PhaseProgressDeviation"></span>
        </li>
    </ul>
    <input type="hidden" id="HiddenPhase" runat="server" />
</div>
<div id="divPhaseBody" style="display: none">
    <asp:GridView ID="GridViewByPhase" runat="server" Width="100%" AutoGenerateColumns="False"
        AllowPaging="False" OnRowDataBound="GridViewByPhase_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="序号">
                <ItemTemplate>
                    <div id="divTaskNo" style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                        text-align: center; cursor: pointer" onmouseover="ShowTaskDetail(this)" onmouseout="HideTaskDetail()">
                        <%# Container.DataItemIndex + 1%>
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Width="30px"></HeaderStyle>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="任务类型">
                <ItemTemplate>
                    <div style="width: 80px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                        text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskTypeDesc").ToString())%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskTypeDesc").ToString())%>
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="任务名称">
                <ItemTemplate>
                    <div style="width: 150px; overflow: hidden; white-space: normal; text-overflow: ellipsis;
                        text-align: Left" title='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "TaskName").ToString())%>
                    </div>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划开始">
                <ItemTemplate>
                    <div id="divPlanStart" style="width: 75px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis" title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}"))%>'>
                        <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : DataBinder.Eval(Container.DataItem, "PlanStartDay", "{0:yyyy-MM-dd}"))%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划结束">
                <ItemTemplate>
                    <div id="divPlanEnd" style="width: 75px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis"
                        title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "PlanEndDay","{0:yyyy-MM-dd}"))%>'>
                        <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : DataBinder.Eval(Container.DataItem, "PlanEndDay", "{0:yyyy-MM-dd}"))%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划工时">
                <ItemTemplate>
                    <div id="divPlanCost" style="width: 30px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "PlanCost")%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PlanCost").ToString())%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="参考工时">
                <ItemTemplate>
                    <div id="divRefCost" style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                        text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Refcost")%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Refcost").ToString())%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="实际开始">
                <ItemTemplate>
                    <div id="divActualStart" style="width: 75px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis" title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "ActualStartDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "ActualStartDay", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "ActualStartDay","{0:yyyy-MM-dd}"))%>'>
                        <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "ActualStartDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "ActualStartDay", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : DataBinder.Eval(Container.DataItem, "ActualStartDay", "{0:yyyy-MM-dd}"))%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="实际结束">
                <ItemTemplate>
                    <div id="divActualEnd" style="width: 75px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis" title='<%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "ActualEndDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "ActualEndDay", "{0:yyyy-MM-dd}")) == "1900-01-01")?"":DataBinder.Eval(Container.DataItem, "ActualEndDay","{0:yyyy-MM-dd}"))%>'>
                        <%# Server.HtmlEncode(((DataBinder.Eval(Container.DataItem, "ActualEndDay", "{0:yyyy-MM-dd}")) == "0001-01-01" || (DataBinder.Eval(Container.DataItem, "ActualEndDay", "{0:yyyy-MM-dd}")) == "1900-01-01") ? "" : DataBinder.Eval(Container.DataItem, "ActualEndDay", "{0:yyyy-MM-dd}"))%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="实际工时">
                <ItemTemplate>
                    <div id="divActualCost" style="width: 30px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "ActualCost")%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ActualCost").ToString())%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="完成百分比">
                <ItemTemplate>
                    <div id="divCompletePercent" style="width: 40px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Completedpercent")%>'>
                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Completedpercent").ToString())%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="进度偏差">
                <ItemTemplate>
                    <div id="divProgressDeviation" style="width: 35px; overflow: hidden; white-space: nowrap;
                        text-overflow: ellipsis; text-align: center" title=''>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="35px" />
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="角色">
                <ItemTemplate>
                    <div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                        text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Role")%>'>
                        <%# DataBinder.Eval(Container.DataItem, "Role")%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="40px" />
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="资源">
                <ItemTemplate>
                    <div style="width: 90px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                        text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Resource")%>'>
                        <%# DataBinder.Eval(Container.DataItem, "Resource")%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="90px" />
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="说明">
                <ItemTemplate>
                    <div id="divRemark" style="width: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;
                        text-align: center" title='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'>
                        <%# DataBinder.Eval(Container.DataItem, "Remark")%>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="编辑">
                <ItemTemplate>
                    <div style="width: 30px; text-align: center;" title="Edit">
                        <img src="../../SysFrame/images/ButtonEdit_1.gif" alt="Edit" onclick="ExcutePlanEditRow(this)"
                            onmouseover="this.style.cursor='hand'" />
                    </div>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
