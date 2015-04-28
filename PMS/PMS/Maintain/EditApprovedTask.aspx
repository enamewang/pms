<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditApprovedTask.aspx.cs"
    Inherits="PMS.PMS.Maintain.EditApprovedTask" %>

<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Approved Task</title>
    <base target="_self" />

    <script src="../JavaScript/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/pms.js" type="text/javascript"></script>

    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 30px;
            width: 80px;
            text-align: left;
            padding-left: 5px;
            padding-right: 1px;
        }
        .DropDownList
        {
            width: 171px;
        }
        select
        {
            width: 179px;
            height: 24px;
            padding-bottom: 3px;
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
            width: 166px;
        }
    </style>

    <script type="text/javascript">
        //1","未开始"    // ID="btnSave
        //2","进行中"    // ID="btnPending
        //3","已完成"    // ID="bthCancelled
        //4","已关闭"    // ID="btnHardClose
        //5","已取消"    // ID="bthReactive
        //6","已暂缓"    // ID="btnExit"

        $(document).ready(function() {
        });

        function SaveSuccess() {
            // 返回JS对象，
            var oResult = new Object();

            //实际开始
            oResult.ActualStart = document.getElementById("<%=DateTextBoxActualStartDate.ClientID %>").value;

            //实际结束
            oResult.ActualEnd = document.getElementById("<%=DateTextBoxActualEndDate.ClientID %>").value;

            //实际工时
            oResult.ActualCost = document.getElementById("<%=TextBoxActualCost.ClientID %>").value;

            //完成百分比
            oResult.CompletePercent = document.getElementById("<%=TextBoxCompletionRate.ClientID %>").value;

            //说明
            oResult.Remark = document.getElementById("<%=TextBoxExecuteRemark.ClientID %>").value;

            window.returnValue = oResult;
            window.close();
        }

        var taskStatus = "<%=SdpDetailResult.TaskStatus %>";
        function Save() {
            taskStatus = $("#HiddenFieldResultTaskStatus").val();
            if (!OutHiddenPercent())
                return false;
            if (taskStatus == "4" || taskStatus == "5" || taskStatus == "6") {
                alert("This taskstatus can not save!");
                return false;
            }
            if ($("[id$=DateTextBoxActualStartDate]").val() == "") {
                alert("Please fill in actual start date !");
                return false;
            }
            if ($("[id$=TextBoxActualCost]").val() == "" || $("[id$=TextBoxActualCost]").val() == 0) {
                alert("Please fill in actual cost !");
                return false;
            }
            if ($("[id$=TextBoxCompletionRate]").val() == "" || $("[id$=TextBoxCompletionRate]").val() == 0) {
                alert("Please fill in completion percent !");
                return false;
            }          
  
            if (!CheckMoreThanToday($("[id$=DateTextBoxActualStartDate]").val())) {
                alert("Actual start date can not more than today!");
                return false;
            }
            if ($("[id$=DateTextBoxActualEndDate]").val() != "") {
                if (!CheckMoreThanToday($("[id$=DateTextBoxActualEndDate]").val())) {
                    alert("Actual end date can not more than today!");
                    return false;
                }
            }
            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "") {
                if (!CheckDate())
                    return false;
                if (!CheckActualCostRange()) {
                    alert("Actual cost must be a  reasonable number !");
                    return false;
                }
            }
            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "" && $("[id$=TextBoxActualCost]").val() == "") {
                alert("This task has been finished; actual cost can not be empty !");
                return false;
            }
            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() == "" && $("[id$=TextBoxCompletionRate]").val() == "100") {
                alert("The completion percent is 100; actual end date can not be empty !");
                return false;
            }
            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "" && $("[id$=TextBoxCompletionRate]").val() != "100") {
                alert("This task has been finished; the completion percent must be 100!");
                return false;
            } else
                $("[id$=HiddenTaskStatus]").val("3");

            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() == "")
                $("[id$=HiddenTaskStatus]").val("2");

            $("[id$=HiddenButtonName]").val("Save");
        }

        function Pending() {
            taskStatus = $("#HiddenFieldResultTaskStatus").val();
            if (!OutHiddenPercent())
                return false;
            if (taskStatus == "3" || taskStatus == "4" || taskStatus == "5" || taskStatus == "6") {
                alert("This task status can not pending");
                return false;
            }
            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "") {
                if (!CheckDate())
                    return false;
            }
            if ($("[id$=DateTextBoxActualEndDate]").val() != "" || $("[id$=TextBoxCompletionRate]").val() == "100") {
                alert("The actual end date has been filled or completion percent is 100,can not pending");
                return false;
            }
            if (confirm("Are you sure pending!"))
                $("[id$=HiddenTaskStatus]").val("6");
            else
                return false;
            $("[id$=HiddenButtonName]").val("Pending");
        }

        function Cancelled() {
            taskStatus = $("#HiddenFieldResultTaskStatus").val();
            if (!OutHiddenPercent())
                return false;
            if (taskStatus == "3" || taskStatus == "4" || taskStatus == "5") {
                alert("This task status can not cancelled");
                return false;
            }
            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "") {
                if (!CheckDate())
                    return false;
            }
            if ($("[id$=TextBoxActualCost]").val() != "" && $("[id$=TextBoxActualCost]").val() != "0") {
                alert("This actual cost greater than 0, please click \"HardClose\"");
                return false;
            }
            if ($("[id$=DateTextBoxActualEndDate]").val() != "" || $("[id$=TextBoxCompletionRate]").val() == "100") {
                alert("The actual end date has been filled ,can not cancelled");
                return false;
            }
            if (confirm("Are you sure cancelled!"))
                $("[id$=HiddenTaskStatus]").val("5");
            else
                return false;
            $("[id$=HiddenButtonName]").val("Cancelled");
        }

        function HardClose() {
            taskStatus = $("#HiddenFieldResultTaskStatus").val();
            if (!OutHiddenPercent())
                return false;
            if (taskStatus == "1" || taskStatus == "3" || taskStatus == "4" || taskStatus == "5") {
                alert("This taskstatus can not hardclose");
                return false;
            }
            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "") {
                if (!CheckDate())
                    return false;
            }
            if ($("[id$=TextBoxActualCost]").val() == "" || $("[id$=TextBoxActualCost]").val() == "0") {
                alert("This actual cost is 0, please click \"Cancelled\"");
                return false;
            }
            if ($("[id$=DateTextBoxActualEndDate]").val() != "" || $("[id$=TextBoxCompletionRate]").val() == "100") {
                alert("The actual end date has been filled ,can not hardclose");
                return false;
            }
            if (confirm("Are you sure hardclose!"))
                $("[id$=HiddenTaskStatus]").val("4");

            else
                return false;
            $("[id$=HiddenButtonName]").val("HardClose");
        }

        function Reactive() {
            taskStatus = $("#HiddenFieldResultTaskStatus").val();
            if (!OutHiddenPercent())
                return false;
            if (taskStatus == "1" || taskStatus == "2" || taskStatus == "3") {
                alert("This taskstatus can not reactive");
                return false;
            }
            //            if (!CheckDate())
            //                return false;

            //            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "" && $("[id$=TextBoxActualCost]").val() == "") {
            //                alert("This Task has ended; ActualCost can not be empty !");
            //                return false;
            //            }
            //            if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "" && $("[id$=TextBoxCompletionRate]").val() != "100") {
            //                alert("This Task has ended; the CompletionRate must be 100!");
            //                return false;
            //            } else
            //                $("[id$=HiddenTaskStatus]").val("3");            

            if (confirm("Are you sure reactive!")) {
                if ($("[id$=DateTextBoxActualStartDate]").val() == "" && $("[id$=DateTextBoxActualEndDate]").val() == "")
                    $("[id$=HiddenTaskStatus]").val("1");
                if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() == "")
                    $("[id$=HiddenTaskStatus]").val("2");
                if ($("[id$=DateTextBoxActualStartDate]").val() != "" && $("[id$=DateTextBoxActualEndDate]").val() != "")
                    $("[id$=HiddenTaskStatus]").val("3");
            }
            else
                return false;
            $("[id$=HiddenButtonName]").val("Reactive");
        }

        function OutCompletionRate() {
            if ($("[id$=DateTextBoxActualStartDate]").val() == "") {
                alert("Actual start date can not be empty !");
                return;
            }
            if ($("#TextBoxActualCost").val() == "") {
                $("[id$=TextBoxCompletionRate]").val("");
                $("[id$=HiddenPercent]").val("");
                return;
            }
            var percent = 100 * parseFloat($("[id$=TextBoxActualCost]").val()) / parseFloat($("[id$=TextBoxRefCost]").val())

            percent = (percent <= 100) ? percent : 100;
            percent = Math.round(percent * 100) / 100;
            $("[id$=TextBoxCompletionRate]").val(percent);
            $("[id$=HiddenPercent]").val(percent);
        }

        function CheckPercent() {
            if ($("[id$=DateTextBoxActualStartDate]").val() == "") {
                $("[id$=TextBoxCompletionRate]").val("");
                alert("Actual start date can not be empty !");
                return;
            }
            if ($("[id$=TextBoxActualCost]").val() == "") {
                $("[id$=TextBoxCompletionRate]").val("");
                alert("Actual cost can not be empty !");
                return;
            }
            if ($("[id$=TextBoxCompletionRate]").val() > 100) {
                $("[id$=TextBoxCompletionRate]").val("100");
                alert("Completion percent can not more than 100");
                return;
            }
            if ($("[id$=TextBoxCompletionRate]").val() == 100 && $("#DateTextBoxActualEndDate").val() == "") {
                alert("Actual end date can not be empty !");
                $("[id$=TextBoxCompletionRate]").val("");
                return;
            }
            $("[id$=HiddenPercent]").val($("[id$=TextBoxCompletionRate]").val());
        }
        function CheckDate() {
            var actualStartDate = $("#DateTextBoxActualStartDate").val();
            var actualEndDate = $("#DateTextBoxActualEndDate").val();

            actualStartDate = actualStartDate.replace(/\-/gi, "/");
            actualEndDate = actualEndDate.replace(/\-/gi, "/");

            actualStartDate = new Date(actualStartDate);
            actualEndDate = new Date(actualEndDate);

            if (actualStartDate.getTime() > actualEndDate.getTime()) {
                alert("The actual end date should be more than the actual start date!");
                return false;
            } else return true;
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
        function CheckMoreThanToday(date) {          
            date = date.replace(/\-/gi, "/");
            var thisDate = new Date(date);
            var today = new Date();
            if (thisDate > today)
                return false;
            else
                return true;
        }
        function CheckActualCostRange() {
            var actualStartDate = $("#DateTextBoxActualStartDate").val();
            var actualEndDate = $("#DateTextBoxActualEndDate").val();
            var Days = GetDateRegion(actualStartDate, actualEndDate);
            if ($("#TextBoxActualCost").val() > Days * 24.00)
                return false;
            else
                return true;
        }

        function CheckActualCost() {
            OutCompletionRate();
            return true;
        }
        function OutHiddenPercent() {
            if ($("[id$=TextBoxCompletionRate]").val() == 100 && $("#DateTextBoxActualEndDate").val() == "") {
                alert("Completion percent is 100,actual end date can not be empty !");
                $("[id$=TextBoxCompletionRate]").val("");
                return false;
            }
            $("[id$=HiddenPercent]").val($("[id$=TextBoxCompletionRate]").val());
            return true;
        }     
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" EnablePartialRendering="true" runat="server"
        ScriptMode="Debug">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updatePanelEditApprovedTask" runat="server" RenderMode="Inline"
        UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 540px; margin: 8px auto; padding-left: 20px; padding-right: 20px;
                padding-top: 20px">
                <table style="border: 1px dashed #CCCCCC; width: 540px">
                    <tr class="style1" style="background-color: #EFEFEF">
                        <td colspan="4">
                            Edit Approved Task
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelCrNo" runat="server" Text="CR No"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxCrNo" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelCrName" runat="server" Text="CR Name"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxCrName" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"
                                Width="430px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelPhase" runat="server" Text="任务阶段"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPhase" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelAuditStatus" runat="server" Text="审核状态"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxAuditStatus" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelTaskStatus" runat="server" Text="任务状态"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxTaskStatus" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                            <asp:HiddenField ID="HiddenTaskStatus" Value="-1" runat="server" />
                        </td>
                        <td class="style1" style="height: 20px;">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelTaskName" runat="server" Text="任务名称"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxTaskName" runat="server" ReadOnly="true" Width="435px" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelOperationType" runat="server" Text="作业方式"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListOperationType" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:Label ID="TaskType" runat="server" Text="任务类型"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListTaskType" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelFunctionType" runat="server" Text="功能分类"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListFunctionType" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelLanguage" runat="server" Text="语言"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListProgramLanguage" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelTaskComplexity" runat="server" Text="复杂度"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="DropDownListTaskComplexity" runat="server" Width="440px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelRefCost" runat="server" Text="参考工时"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxRefCost" runat="server" Style="padding-top: 4px;" ReadOnly="true"
                                CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelPlanCost" runat="server" Text="计划工时"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPlanCost" runat="server" Style="padding-top: 4px;" ReadOnly="true"
                                CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelPlanStartDate" runat="server" Text="计划开始"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="DateTextBoxPlanStartDate" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                        <td class="style1" style="height: 20px;">
                            <asp:Label ID="LabelPlanEndDate" runat="server" Text="计划结束"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="DateTextBoxPlanEndDate" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelActualStartDate" runat="server" Text="实际开始"></asp:Label>
                            <asp:Label ID="LabelActualStartDateMark" runat="server" ForeColor="red" Text="*"
                                Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <TW:DateTextBox ID="DateTextBoxActualStartDate" runat="server" IsDisplayTime="false"
                                Language="English" Width="164px"></TW:DateTextBox>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelActualEndDate" runat="server" Text="实际结束"></asp:Label>
                        </td>
                        <td>
                            <TW:DateTextBox ID="DateTextBoxActualEndDate" runat="server" IsDisplayTime="false"
                                Language="English" Width="164px"></TW:DateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelActualCost" runat="server" Text="实际工时"></asp:Label>
                            <asp:Label ID="LabelActualCostMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 15px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxActualCost" runat="server" Style="padding-top: 4px; width: 166px;"
                                onkeypress="return checkKeyForFloat(this,event);" onkeyup="CheckActualCost();"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelCompletionRate" runat="server" Text="完成百分比"></asp:Label>
                            <asp:Label ID="LabelCompletionRateMark" runat="server" ForeColor="red" Text="*" Style="margin-left: 5px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxCompletionRate" runat="server" class="EditTextBox" onkeyup="CheckPercent();"
                                Style="padding-top: 4px; width: 166px;" onkeypress="return checkKeyForFloat(this,event);"
                                onchange="OutHiddenPercent();"></asp:TextBox>
                            <asp:HiddenField ID="HiddenPercent" Value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelResource" runat="server" Text="指派给"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListResource" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:Label ID="LabelRole" runat="server" Text="资源角色"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListRole" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelExecuteRemark" runat="server" Text="说明"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBoxExecuteRemark" runat="server" Style="padding-top: 4px;" Width="435px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 530px; margin: 8px auto; padding-left: 20px; padding-right: 20px;
                padding-top: 20px">
                <asp:Label ID="Label2" runat="server" Text="" Width="40px"></asp:Label>
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClientClick="return Save();"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnPending" runat="server" Text="Pending" Width="70px" OnClientClick="return Pending();"
                    OnClick="btnSave_Click" />
                <asp:Button ID="bthCancelled" runat="server" Text="Cancelled" Width="80px" OnClientClick="return Cancelled();"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnHardClose" runat="server" Text="Hard Close" Width="88px" OnClientClick="return HardClose();"
                    OnClick="btnSave_Click" />
                <asp:Button ID="bthReactive" runat="server" Text="Reactive" Width="75px" OnClientClick="return Reactive();"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnExit" runat="server" Text="Exit" Width="70px" OnClientClick="window.close();" />
            </div>
            <asp:HiddenField ID="HiddenButtonName" Value="" runat="server" />
            <asp:HiddenField ID="HiddenFieldResultTaskStatus" Value="0" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
