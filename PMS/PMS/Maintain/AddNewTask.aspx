<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddNewTask.aspx.cs"
    Inherits="PMS.PMS.Maintain.AddNewTask" %>

<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Add New Task</title>
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/pms.js" type="text/javascript"></script>

    <script type="text/javascript">

        function SaveSuccess() {
            // 返回JS对象，包含字段Serial,TaskName等
            var oResult = new Object();
            var obj;
            var index;

            //任务阶段
            var phaseNull = "<%=PhaseNull %>";
            if (phaseNull == "True") {
                obj = document.getElementById("<%=DropDownListPhases.ClientID %>");
                index = obj.selectedIndex;
                oResult.Phase = obj.options[index].value;
            }
            else
                oResult.Phase = "<%=Phase %>";

            switch (oResult.Phase) {
                case "4":
                    oResult.PhaseDesc = "Design";
                    break;
                case "5":
                    oResult.PhaseDesc = "Development";
                    break;
                case "6":
                    oResult.PhaseDesc = "Test";
                    break;
                case "7":
                    oResult.PhaseDesc = "Release";
                    break;
                case "8":
                    oResult.PhaseDesc = "Support";
                    break;
            }

            //任务类型
            obj = document.getElementById("<%=DropDownListTaskType.ClientID %>");
            index = obj.selectedIndex;
            oResult.TaskType = obj.options[index].value;
            oResult.TaskTypeDesc = obj.options[index].text;

            //任务名称
            oResult.TaskName = document.getElementById("<%=TextBoxTaskName.ClientID %>").value;

            //功能分类
            obj = document.getElementById("<%=DropDownListFunctionType.ClientID %>");
            index = obj.selectedIndex;
            oResult.FunctionType = obj.options[index].value;
            oResult.FunctionTypeDesc = obj.options[index].text;

            //审核状态说明
            oResult.AuditStatus = "1";
            oResult.AuditStatusDesc = "未提交";

            //任务状态说明
            oResult.TaskStatus = "1";
            oResult.TaskStatusDesc = "未开始";

            //计划开始
            oResult.PlanStart = document.getElementById("<%=DateTextBoxPlanStartDate.ClientID %>").value;

            //计划结束
            oResult.PlanEnd = document.getElementById("<%=DateTextBoxPlanEndDate.ClientID %>").value;

            //计划工时
            oResult.PlanCost = document.getElementById("<%=TextBoxPlanCost.ClientID %>").value;

            //参考工时
            oResult.RefCost = document.getElementById("<%=HiddenFieldRefCost.ClientID %>").value;

            //角色
            obj = document.getElementById("<%=HiddenFieldRole.ClientID %>");
            oResult.Role = obj.value;

            //资源
            obj = document.getElementById("<%=DropDownListResource.ClientID %>");
            index = obj.selectedIndex;
            oResult.Resource = obj.options[index].text;

            //审核人
            obj = document.getElementById("<%=HiddenFieldAuditor.ClientID %>");
            oResult.Auditor = obj.value;

            //代理审核人
            obj = document.getElementById("<%=HiddenFieldAuditorAgent.ClientID %>");
            oResult.AuditorAgent = obj.value;

            //创建人            
            oResult.CreateUser = "<%=LoginName %>".toUpperCase();

            //Serial Serial
            oResult.Serial = document.getElementById("<%=HiddenFieldSerial.ClientID %>").value;

            var parentWindow = window.dialogArguments.window;
            var tableSelector = phaseNull == "True" ? "table[id*='SchedulePlanByPhase" + oResult.PhaseDesc + "']" : "#" + window.dialogArguments.tableSelector;
            parentWindow.InsertRowShow(oResult, tableSelector);
            
            if ($("#HiddenFieldClickButtonName").val() == "Save")
                window.close();

            ShowRole();
        }

        function CheckDate(oSdpDetail) {
            var planStartDateTime = new Date(oSdpDetail.planStartDate).getTime();
            var planEndDateTime = new Date(oSdpDetail.planEndDate).getTime();
            var dueDateTime = new Date(oSdpDetail.dueDate).getTime();

            if (dueDateTime < new Date("1900/1/1").getTime())
                return false;
            if (oSdpDetail.type != oSdpDetail.service) {
                if (planStartDateTime > dueDateTime && oSdpDetail.phase != oSdpDetail.support) {
                    alert("The planned start date should be less than the CR due date!");
                    return false;
                }
                if (planEndDateTime > dueDateTime && oSdpDetail.phase != oSdpDetail.support) {
                    alert("The planned end date should be less than the CR due date!");
                    return false;
                }
            }
            if (planStartDateTime > planEndDateTime && oSdpDetail.phase != oSdpDetail.support) {
                alert("The planned end date should be more than the plan start date!");
                return false;
            }
            var startDate = oSdpDetail.planStartDate;
            var endDate = oSdpDetail.planEndDate;
            if (startDate.setDate(startDate.getDate() + 2) < planEndDateTime) {
                alert("Task duration should be less than 3 days!");
                return false;
            }
            startDate.setDate(startDate.getDate() - 2);

            var week = 2;
            if (startDate.setDate(startDate.getDate() + 7) > planEndDateTime) {
                week = 1;
                startDate.setDate(startDate.getDate() - 7);
                var start = new Date(startDate.setDate(startDate.getDate() - 1)).getDay();
                var end = new Date(endDate.setDate(endDate.getDate() - 1)).getDay()
                if (start > end)
                    week = 2;
            }
            if (week > 1) {
                alert("Task period should not be cross week!");
                return false;
            }
            return true;
        }
        function SaveAndAddNew() {
            $("#HiddenFieldClickButtonName").val("SaveAndAddNew");
            return Save();
        }
        function OnlySave() {
            $("#HiddenFieldClickButtonName").val("Save");
            return Save();
        }
        function Save() {
            $("#HiddenFieldRole").val($("#DropDownListRole option:selected").text());
            $("#HiddenFieldRefCost").val($("#TextBoxRefCost").val());
            PlanCostOutRefCost();

            var oSdpDetail = new Object();
            oSdpDetail.service = "<%=Service %>";
            oSdpDetail.support = "<%=Support %>";
            oSdpDetail.type = "<%=ObjPmsHead.Type %>";
            oSdpDetail.phase = "<%=Phase %>";
            var planStartDate = $("#DateTextBoxPlanStartDate").val();
            var planEndDate = $("#DateTextBoxPlanEndDate").val();
            var dueDate = "<%=ObjPmsHead.DueDate %>";

            planStartDate = planStartDate.replace(/\-/gi, "/");
            planEndDate = planEndDate.replace(/\-/gi, "/");
            dueDate = dueDate.replace(/\-/gi, "/");

            oSdpDetail.planStartDate = new Date(planStartDate);
            oSdpDetail.planEndDate = new Date(planEndDate);
            oSdpDetail.dueDate = new Date(dueDate);

            //Check 填写是否完整            
            var phaseNull = "<%=PhaseNull %>";
            if (phaseNull == "True" && $("#DropDownListPhases").val() == "") {
                alert("Phase can not be empty !");
                return false;
            }
            if ($("#TextBoxTaskName").val() == "") {
                alert("Taskname can not be empty !");
                return false;
            }


            if ($("#TextBoxPlanCost").val() == "") {
                alert("Plan cost can not be empty !");
                return false;
            }
            if ($("#TextBoxRefCost").val() == "") {
                alert("Plan cost can not be empty !");
                return false;
            }
            if (isNaN($("#TextBoxPlanCost").val()) || $("#TextBoxPlanCost").val() <= 0) {
                alert("Plan cost must be a valid number !");
                return false;
            }
            if ($("#DateTextBoxPlanStartDate").val() == "") {
                alert("Plan start date can not be empty !");
                return false;
            }
            if ($("#DateTextBoxPlanEndDate").val() == "") {
                alert("Plan end date can not be empty !");
                return false;
            }
            if ($("#DropDownListTaskComplexity").val() == "") {
                alert("Please select complexity!");
                return false;
            }
            if ($("#DropDownListResource").val() == "") {
                alert("Please select resource!");
                return false;
            }
            //Check Date            
            if (!CheckDate(oSdpDetail))
                return false;
            if (!CheckPlanCostRange()) {
                alert("Plan cost must be a  reasonable number !");
                return false;
            }

            //如果任务类型为 Review Meeting 不检查任务名和资源等重复
            if ($("#DropDownListTaskType option:selected").text() != "Review Meeting") {
                //Jquery ajax 调用 WebService  CheckTaskNameAndResource 不重复
                var pmsid = "<%=PmsId %>";
                var taskName = $("#TextBoxTaskName").val();
                var phase = "<%=Phase %>";
                if (phaseNull == "True")
                    phase = $("#DropDownListPhases option:selected").val();
                var role = $("#DropDownListRole option:selected").text();
                var resource = $("#DropDownListResource option:selected").text();
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "../WebService/AddNewTask.asmx/CheckTaskNameAndResource",
                    data: "{ taskName:'" + taskName + "', resource:'" + resource + "', pmsid:'" + pmsid + "', phase:'" + phase + "', role:'" + role + "' }",
                    dataType: 'json',
                    contentType: "application/json",
                    success: function(result) {
                        var str = result.d;
                        var obj = eval('(' + str + ')');
                        var exist = obj.Exist;
                        if (exist == "Y") {
                            alert("The same taskname and  resource has been exist!");
                            $("#HiddenFieldTaskName").val("NO");
                        } else $("#HiddenFieldTaskName").val("YES");
                    },
                    error: function(xmlHttpRequest, msg) {
                        alert("Call WebService Error!");
                    }
                });
                //如果任务名、资源等重复 不允许保存 返回。
                if ($("#HiddenFieldTaskName").val() == "NO")
                    return false;
            }
        }
        function GetRefCost() {
            var complexity = $("#DropDownListTaskComplexity option:selected").val();
            var operationType = $("#DropDownListOperationType option:selected").val();
            var taskType = $("#DropDownListTaskType option:selected").val();
            var functionType = $("#DropDownListFunctionType option:selected").val();
            var programLanguage = $("#DropDownListProgramLanguage option:selected").val();
            if (typeof (taskType) == "undefined")
                return;
            if (complexity == "")
                complexity = "0";
            $.ajax({
                type: "post",
                url: "../WebService/AddNewTask.asmx/GetRefCost",
                data: "{ complexity:'" + complexity + "', operationType:'" + operationType + "', taskType:'" + taskType + "', functionType:'" + functionType + "', programLanguage:'" + programLanguage + "' }",
                dataType: 'json',
                contentType: "application/json",
                success: function(result) {
                    var refCost = result.d;
                    $("#TextBoxRefCost").val(refCost);
                    $("#HiddenFieldRefCost").val(refCost);
                },
                error: function(state) { alert("Call WebService Error!"); }
            });
        }
        function Refresh() {
            $("#TextBoxRefCost").val($("#HiddenFieldRefCost").val());
            if ($("#DropDownListResource").val() != "")
                ShowRole();
            var taskType = $("#DropDownListTaskType option:selected").val();
            if (taskType != "")
                GetRefCost();
        }

        function ShowRole() {
            $("#DropDownListRole").empty();
            $("#DropDownListRoleInfo option").each(function() {
                var roles = $(this).val();
                var resource = $("#DropDownListResource option:selected").text();
                if (resource == "") { resource = "****"; } //给一个不存在的值
                if (roles.indexOf(resource) >= 0) {
                    $("#DropDownListRole").append("<option value=" + $(this).text() + ">" + $(this).text() + "</option>");
                }
            });
        }
        function CheckTaskNameLength() {
            if ($("#TextBoxTaskName").val().length >= 300)
                return false;
        }
        function CheckRemarkLength() {
            if ($("#TextBoxPlanRemark").val().length >= 500)
                return false;
        }
        function PlanCostOutRefCost() {
            var planCost = document.getElementById("<%=TextBoxPlanCost.ClientID %>").value;
            var RefCost = document.getElementById("<%=TextBoxRefCost.ClientID %>");
            if (RefCost.value == "" && !isNaN(planCost)) {
                RefCost.value = planCost;
                $("#HiddenFieldRefCost").val(planCost);
            }
        }

        function checkKeyForFloat(thisObj, evt) {
            evt = (evt) ? evt : ((window.event) ? window.event : "");
            var key = evt.keyCode ? evt.keyCode : evt.which;
            if (key != 46 && key < 45 || key > 57) {
                return false;
            }
            if (key == 45)
                return false;
            //如果已存在小数点
            if (key === 46 && $.trim(thisObj.value).indexOf('.') > 0) {
                return false;
            }
            if ($.trim(thisObj.value).indexOf('.') > 0) {
                if (thisObj.value.length >= 9 || thisObj.value.substring(thisObj.value.indexOf("."), thisObj.value.length).length > 2)
                    return false;
            } else {
                if (thisObj.value.length >= 6)
                    return false;
            }
            return true;
        }
        function CheckPlanCostRange() {
            var planStartDate = $("#DateTextBoxPlanStartDate").val();
            var planEndDate = $("#DateTextBoxPlanEndDate").val();
            var Days = GetDateRegion(planStartDate, planEndDate);
            if ($("#TextBoxPlanCost").val() > Days * 24)
                return false;
            else
                return true;
        }
        $(function() {
            ShowRole();
        });



       
   
    
    </script>

    <style type="text/css">
        .style1
        {
            height: 30px;
            width: 80px;
            padding-left: 5px;
            padding-right: 1px;
        }
        .DropDownList
        {
            width: 167px;
        }
        select
        {
            width: 179px;
            height: 24px;
        }
        .UnderLineOnlyTextBox
        {
            border-top-style: none;
            border-bottom: 1px solid #CCCCCC;
            border-right-style: none;
            border-left-style: none;
            width: 471px;
            vertical-align: bottom;
            font-family: Arial;
        }
        input[type="text"]
        {
            width: 161px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" EnablePartialRendering="true" runat="server"
        ScriptMode="Debug">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updatePanelAddNewTask" runat="server" RenderMode="Inline"
        UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 530px; margin: 10px auto; padding-left: 20px; padding-right: 20px;
                padding-top: 20px;">
                <table style="border: 1px dashed #CCCCCC; width: 530px">
                    <tr class="style1" style="background-color: #EFEFEF">
                        <td colspan="4">
                            <span>Add New Task</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 25px;">
                            <asp:Label ID="LabelCrNo" runat="server" Text="CR No"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxCrNo" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 25px;">
                            <asp:Label ID="LabelCrName" runat="server" Text="CR Name"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxCrName" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"
                                Width="422px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 25px;">
                            <asp:Label ID="LabelPhase" runat="server" Text="任务阶段"></asp:Label>
                            <asp:Label ID="LabelPhaseMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPhase" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                            <asp:DropDownList ID="DropDownListPhases" runat="server" OnSelectedIndexChanged="DropDownListPhases_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td class="style1" style="height: 25px;">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 25px;">
                            <asp:Label ID="LabelTaskName" runat="server" Text="任务名称"></asp:Label>
                            <asp:Label ID="LabelTaskNameMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxTaskName" Style="padding-top: 4px;" runat="server" Width="424px"
                                onkeypress="return CheckTaskNameLength();"></asp:TextBox>
                            <asp:HiddenField ID="HiddenFieldTaskName" Value="" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelOperationType" runat="server" Text="作业方式"></asp:Label>
                            <asp:Label ID="LabelOperationTypeMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListOperationType" onchange="GetRefCost();" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelTaskType" runat="server" Text="任务类型"></asp:Label>
                            <asp:Label ID="LabelTaskTypeMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListTaskType" onchange="GetRefCost();" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelFunctionType" runat="server" Text="功能分类"></asp:Label>
                            <asp:Label ID="LabelFunctionTypeMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListFunctionType" onchange="GetRefCost();" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelProgramLanguage" runat="server" Text="语言"></asp:Label>
                            <asp:Label ID="LabelProgramLanguageMark" runat="server" ForeColor="red" Text="*"
                                Style="margin-left: 38px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListProgramLanguage" onchange="GetRefCost();" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelTaskComplexity" runat="server" Text="复杂度"></asp:Label>
                            <asp:Label ID="LabelTaskComplexityMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 27px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="DropDownListTaskComplexity" onchange="GetRefCost();" runat="server"
                                Width="430px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelRefCost" runat="server" Text="参考工时"></asp:Label>
                            <asp:Label ID="LabelRefCostMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxRefCost" Style="padding-top: 4px;" runat="server" ReadOnly="true"
                                CssClass="UnderLineOnlyTextBox" EnableViewState="true"></asp:TextBox>
                            <asp:HiddenField ID="HiddenFieldRefCost" Value="" runat="server" />
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelPlanCost" runat="server" Text="计划工时"></asp:Label>
                            <asp:Label ID="LabelPlanCostMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPlanCost" Style="padding-top: 4px;" runat="server" onkeypress="return checkKeyForFloat(this,event);"
                                onblur="PlanCostOutRefCost();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelPlanStartDate" runat="server" Text="计划开始"></asp:Label>
                            <asp:Label ID="LabelPlanStartDateMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <TW:DateTextBox ID="DateTextBoxPlanStartDate" runat="server" IsDisplayTime="false"
                                Language="English" Width="159px"></TW:DateTextBox>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelPlanEndDate" runat="server" Text="计划结束"></asp:Label>
                            <asp:Label ID="LabelPlanEndDateMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <TW:DateTextBox ID="DateTextBoxPlanEndDate" runat="server" IsDisplayTime="false"
                                Language="English" Width="159px"></TW:DateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelResource" runat="server" Text="指派给"></asp:Label>
                            <asp:Label ID="LabelResourceMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 27px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListResource" runat="server" onchange="ShowRole();">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelRole" runat="server" Text="资源角色"></asp:Label>
                            <asp:Label ID="LabelRoleMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListRole" runat="server">
                            </asp:DropDownList>
                            <asp:HiddenField ID="HiddenFieldRole" Value="" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelPlanRemark" runat="server" Text="说明"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxPlanRemark" runat="server" Style="padding-top: 4px;" Width="424px"
                                onkeypress="return CheckRemarkLength();"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 530px; margin: 10px auto; padding-left: 20px; padding-right: 20px;
                padding-top: 20px">
                <asp:Label ID="label1" runat="server" Text="  " Width="100px"></asp:Label>
                <asp:Button ID="ButtonSave" runat="server" Text="Save" Width="80px" OnClientClick="return OnlySave();"
                    OnClick="ButtonSave_Click" />
                <asp:Label ID="label3" runat="server" Text="  " Width="30px"></asp:Label>
                <asp:Button ID="ButtonSaveAndAddNew" runat="server" Text="Save&New" Width="160px"
                    OnClientClick="return SaveAndAddNew();" OnClick="ButtonSave_Click" />
                <asp:Label ID="label2" runat="server" Text="  " Width="30px"></asp:Label>
                <asp:Button ID="ButtonEdit" runat="server" Text="Exit" Width="80px" OnClientClick="window.close();" />
            </div>
            <div style="display: none">
                <asp:DropDownList ID="DropDownListRoleInfo" runat="server">
                </asp:DropDownList>
                <asp:HiddenField ID="HiddenFieldSerial" Value="" runat="server" />
                <asp:HiddenField ID="HiddenFieldAuditor" runat="server" />
                <asp:HiddenField ID="HiddenFieldAuditorAgent" runat="server" />
                <asp:HiddenField ID="HiddenFieldClickButtonName" Value="Save" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
