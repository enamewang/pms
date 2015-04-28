<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditWaitingSubmitTask.aspx.cs"
    Inherits="PMS.PMS.Maintain.EditWaitingSubmitTask" %>

<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Waiting Submit Task</title>
    <base target="_self" />

    <script src="../JavaScript/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/pms.js" type="text/javascript"></script>

    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function Test() {
            if (confirm("该任务正在进行，是否修改？")) {
                document.getElementById('hidvalueIsSave').value = "Y";
                document.getElementById('<%=ButtonSave.ClientID %>').click();
            }
            else
                return;
        }
        function SaveSuccess() {
            // 返回JS对象，包含字段Serial,TaskName等
            var oResult = new Object();
            var obj;
            var index;

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
            oResult.AuditStatus = document.getElementById("<%=HiddenFieldAuditStatus.ClientID %>").value;
            oResult.AuditStatusDesc = document.getElementById("<%=HiddenFieldAuditStatusDesc.ClientID %>").value;

            //任务状态说明
            oResult.TaskStatus = document.getElementById("<%=HiddenFieldTaskStatus.ClientID %>").value;
            oResult.TaskStatusDesc = document.getElementById("<%=HiddenFieldTaskStatusDesc.ClientID %>").value;

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

            //创建人            
            oResult.CreateUser = "<%=LoginName %>";
            window.returnValue = oResult;
            window.close();
        }

        function ShowRole() {
            $("#DropDownListRole").empty();
            $("#DropDownListRoleInfo option").each(function() {
                var roles = $(this).val();
                var resource = $("#DropDownListResource option:selected").text();
                var i = 0;
                if (roles.indexOf(resource) >= 0) {
                    i++;
                    $("#DropDownListRole").append("<option value=" + i + ">" + $(this).text() + "</option>");
                }
                $("#DropDownListRole options:first").attr("selected", true);
            });
        }

        function Save() {
            PlanCostOutRefCost();
            var oSdpDetail = new Object();
            var planStartDate = $("#DateTextBoxPlanStartDate").val();
            var planEndDate = $("#DateTextBoxPlanEndDate").val();
            
            planStartDate = planStartDate.replace(/\-/gi, "/");
            planEndDate = planEndDate.replace(/\-/gi, "/");
            
            oSdpDetail.planStartDate = new Date(planStartDate);
            oSdpDetail.planEndDate = new Date(planEndDate);
            
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
            if (!CheckDate(oSdpDetail))
                return false;
            if (!CheckPlanCostRange()) {
                alert("Plan cost must be a  reasonable number !");
                return false;
            }
            $("#HiddenFieldRole").val($("#DropDownListRole option:selected").text());
            return true;
        }

        $(function() {
            $("#DropDownListResource").change(function() { ShowRole(); });
            if ($("#HiddenFieldRefCost").val() != "")
                $("#TextBoxRefCost").val($("#HiddenFieldRefCost").val());
            if ($("#TextBoxRefCost").val() != "")
                $("#HiddenFieldRefCost").val($("#TextBoxRefCost").val());
        });

        function CheckDate(oSdpDetail) {
            var planStartDateTime = new Date(oSdpDetail.planStartDate).getTime();
            var planEndDateTime = new Date(oSdpDetail.planEndDate).getTime();        
         
            if (planStartDateTime > planEndDateTime ) {
                alert("The planned end date should be more than the plan start date!");
                return false;
            }            
            return true;
        }
        function GetRefCost() {
            var complexity = $("#DropDownListTaskComplexity option:selected").val();
            var operationType = $("#DropDownListOperationType option:selected").val();
            var taskType = $("#DropDownListTaskType option:selected").val();
            var functionType = $("#DropDownListFunctionType option:selected").val();
            var programLanguage = $("#DropDownListProgramLanguage option:selected").val();
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
            if ($("#TextBoxPlanCost").val() > Days * 24.00)
                return false;
            else
                return true;
        }
    
    
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
            width: 168px;
        }
        select
        {
            width: 178px;
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
    <asp:UpdatePanel ID="updatePanelEditWaitingSubmitTask" runat="server" RenderMode="Inline"
        UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 530px; margin: 10px auto; padding-left: 20px; padding-right: 20px;
                padding-top: 20px">
                <table style="border: 1px dashed #CCCCCC; width: 530px;">
                    <tr class="style1" style="background-color: #EFEFEF">
                        <td colspan="4">
                            Edit Waiting Submit Task
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
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPhase" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                        <td class="style1" style="height: 25px;">
                            <asp:Label ID="LabelAuditStatus" runat="server" Text="审核状态"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxAuditStatus" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 25px;">
                            <asp:Label ID="LabelTaskStatus" runat="server" Text="任务状态"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxTaskStatus" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                        <td class="style1" style="height: 25px;">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelTaskName" runat="server" Text="任务名称"></asp:Label>
                            <asp:Label ID="LabelTaskNameMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxTaskName" Style="padding-top: 4px;" runat="server" Width="425px"
                                onkeypress="return CheckTaskNameLength();"></asp:TextBox>
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
                            <asp:TextBox ID="TextBoxRefCost" Style="padding-top: 4px; width: 163px;" runat="server"
                                ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                            <asp:HiddenField ID="HiddenFieldRefCost" Value="" runat="server" />
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelPlanCost" runat="server" Text="计划工时"></asp:Label>
                            <asp:Label ID="LabelPlanCostMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPlanCost" Style="padding-top: 4px; width: 163px;" runat="server"
                                onkeypress="return checkKeyForFloat(this,event);" onblur="PlanCostOutRefCost();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelPlanStartDate" runat="server" Text="计划开始"></asp:Label>
                            <asp:Label ID="LabelPlanStartDateMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <TW:DateTextBox ID="DateTextBoxPlanStartDate" runat="server" IsDisplayTime="false"
                                Language="English"></TW:DateTextBox>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelPlanEndDate" runat="server" Text="计划结束"></asp:Label>
                            <asp:Label ID="LabelPlanEndDateMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <TW:DateTextBox ID="DateTextBoxPlanEndDate" runat="server" IsDisplayTime="false"
                                Language="English"></TW:DateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelResource" runat="server" Text="指派给"></asp:Label>
                            <asp:Label ID="LabelResourceMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 27px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListResource" runat="server">
                                <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelRole" runat="server" Text="资源角色"></asp:Label>
                            <asp:Label ID="LabelRoleMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListRole" runat="server">
                                <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="HiddenFieldRole" Value="" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelPlanRemark" runat="server" Text="说明"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxPlanRemark" Style="padding-top: 4px;" runat="server" Width="425px"
                                onkeypress="return CheckRemarkLength();"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 530px; margin: 10px auto; padding-left: 20px; padding-right: 20px;
                padding-top: 20px">
                <asp:Label ID="label1" runat="server" Text="  " Width="160px"></asp:Label>
                <asp:Button ID="ButtonSave" runat="server" Text="Save" Width="80px" OnClientClick="return Save();"
                    OnClick="ButtonSave_Click" />
                <asp:Label ID="label2" runat="server" Text="  " Width="30px"></asp:Label>
                <asp:Button ID="ButtonEdit" runat="server" Text="Exit" Width="80px" OnClientClick="window.close();" />
            </div>
            <input type="hidden" id="hidvalueIsSave" runat="server" />
            <div style="display: none">
                <asp:DropDownList ID="DropDownListRoleInfo" runat="server">
                </asp:DropDownList>
                <asp:HiddenField ID="HiddenFieldAuditStatus" Value="" runat="server" />
                <asp:HiddenField ID="HiddenFieldAuditStatusDesc" Value="" runat="server" />
                <asp:HiddenField ID="HiddenFieldTaskStatus" Value="" runat="server" />
                <asp:HiddenField ID="HiddenFieldTaskStatusDesc" Value="" runat="server" />
                <asp:HiddenField ID="HiddenFieldAuditor" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
