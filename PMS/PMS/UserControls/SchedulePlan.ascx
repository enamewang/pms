<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchedulePlan.ascx.cs"
    Inherits="PMS.PMS.UserControls.SchedulePlan" %>
<%@ Register Src="SchedulePlanByPhase.ascx" TagName="SchedulePlanByPhase" TagPrefix="uc1" %>
<link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />

<script src="../JavaScript/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>

<script type="text/javascript">

    //页面加载时执行
    $(document).ready(
    function() {
        window.$SchedulePlanTables = $("table[id*='SchedulePlanByPhase']"); //SchedulePlan所有阶段Table
        //window.$SchedulePlanTableDataTrs = $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)"); //SchedulePlan所有阶段Table数据行
        window.LoginName = "<%=LoginName%>".toUpperCase(); //当前登录者
        SetLeftFrameHide(); //设置左侧菜单隐藏
        SetDropDownListCondition(); //设置下拉框样式和事件
        SetSchedulePlanButton(); //设置按钮样式和事件
        SetSchedulePlanAllPhaseHead(); //计算参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均     
    }
  );

    function SetLeftFrameHide() {

        var midFrame = $(window.parent.frames["midFrame"].document);
        midFrame.find("#imgSplitter").attr("src", "images/Icon_appear.gif");
        midFrame.find("#hdH").hide();
        midFrame.find("#hdH").val("Y");
        window.parent.main.cols = "0,10,*";
    }

    function SetDropDownListCondition() {
        var $DropDownListCondition = $("#DropDownListCondition");
        $DropDownListCondition.addClass("DropDownList");

        //不是RDLeader移除需我审核和需我代理审核选项
        if ("<%=IsRDLeader%>" == "N") {
            $("#DropDownListCondition option[value='3']").remove();
            $("#DropDownListCondition option[value='4']").remove();
        }

        //选项改变事件
        $DropDownListCondition.change(function() {
            var selectedValue = $(this).val();
            DropDownListConditionOnChange(selectedValue);
        });
    }

    function DropDownListConditionOnChange(selectedValue) {

        //重置过滤按钮样式
        $("#ButtonShowTaskAll").removeClass();
        $("#ButtonShowTaskAll").addClass("ButtonTransparent");
        $("#ButtonShowTaskAssignToMe").removeClass();
        $("#ButtonShowTaskAssignToMe").addClass("ButtonTransparent");

        switch (selectedValue) {
            case "": //默认显示
                $("#ButtonCreate").show();
                $("#ButtonSubmit").show();
                $("#ButtonRecall").show();
                $("#ButtonDelete").show();
                $("#ButtonModify").show();
                $("#ButtonApprove").hide();
                $("#ButtonReject").hide();
                break;
            case "1": //由我创建
                $("#ButtonCreate").show();
                $("#ButtonSubmit").show();
                $("#ButtonRecall").show();
                $("#ButtonDelete").show();
                $("#ButtonModify").show();
                $("#ButtonApprove").hide();
                $("#ButtonReject").hide();
                SetLastVersionHide();
                $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").each(function() {
                    if ($(this).attr("createuser").toUpperCase() == LoginName.toUpperCase()) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
                SetSchedulePlanAllPhaseHead();
                break;
            case "2": //由别人创建
                $("#ButtonCreate").show();
                $("#ButtonSubmit").show();
                $("#ButtonRecall").show();
                $("#ButtonDelete").show();
                $("#ButtonModify").show();
                $("#ButtonApprove").hide();
                $("#ButtonReject").hide();
                SetLastVersionHide();
                $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").each(function() {
                    if ($(this).attr("createuser").toUpperCase() == LoginName.toUpperCase()) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
                SetSchedulePlanAllPhaseHead();
                break;
            case "3": //需我提交
                $("#ButtonCreate").show();
                $("#ButtonSubmit").show();
                $("#ButtonRecall").show();
                $("#ButtonDelete").show();
                $("#ButtonModify").show();
                $("#ButtonApprove").hide();
                $("#ButtonReject").hide();
                SetLastVersionHide();
                $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").each(function() {
                    if (($(this).attr("createuser").toUpperCase() == LoginName.toUpperCase()) && ($(this).attr("auditstatus") == "1")) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
                SetSchedulePlanAllPhaseHead();
                break;
            case "4": //需我审核
                $("#ButtonCreate").hide();
                $("#ButtonSubmit").hide();
                $("#ButtonRecall").hide();
                $("#ButtonDelete").hide();
                $("#ButtonModify").hide();
                $("#ButtonApprove").show();
                $("#ButtonReject").show();
                SetLastVersionHide();
                $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").each(function() {
                    if (($(this).attr("auditor").toUpperCase() == LoginName.toUpperCase()) && ($(this).attr("auditstatus") == "2")) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    };
                });
                SetSchedulePlanAllPhaseHead();
                $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").each(function() {
                    if ($(this).css('display') != "none") {
                        SetLastVersionShow(this);
                    }
                });
                SetLastVersionHeadShow();
                break;
            case "5": //需我代理审核
                $("#ButtonCreate").hide();
                $("#ButtonSubmit").hide();
                $("#ButtonRecall").hide();
                $("#ButtonDelete").hide();
                $("#ButtonModify").hide();
                $("#ButtonApprove").show();
                $("#ButtonReject").show();
                SetLastVersionHide();
                $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").each(function() {
                    if (($(this).attr("auditoragent").toUpperCase() == LoginName.toUpperCase()) && ($(this).attr("auditor").toUpperCase() != LoginName.toUpperCase()) && ($(this).attr("auditstatus") == "2")) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
                SetSchedulePlanAllPhaseHead();
                $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").each(function() {
                    if ($(this).css('display') != "none") {
                        SetLastVersionShow(this);
                    }
                });
                SetLastVersionHeadShow();
                break;
            default:
                break;
        }
    }

    function SetLastVersionHide() {
        $("div[id='divLastPlanStart']").hide();
        $("div[id='divLastPlanEnd']").hide();
        $("div[id='divLastPlanCost']").hide();
        $("div[id='divLastRefCost']").hide();

        $("div[id = 'divPhaseLastRefCost']").hide();
        $("div[id = 'divPhaseLastPlanCost']").hide();
        $("div[id = 'divPhaseLastPlanDay']").hide();
        $("div[id = 'divPhaseLastPlanStart']").hide();
        $("div[id = 'divPhaseLastPlanEnd']").hide();
        $("div[id = 'divPhaseLastPlanAverage']").hide();

        $("div[id = 'divAllPhaseLastRefCost']").hide();
        $("div[id = 'divAllPhaseLastPlanCost']").hide();
        $("div[id = 'divAllPhaseLastPlanDay']").hide();
        $("div[id = 'divAllPhaseLastPlanStart']").hide();
        $("div[id = 'divAllPhaseLastPlanEnd']").hide();
        $("div[id = 'divAllPhaseLastPlanAverage']").hide();

    }
    function SetLastVersionHeadShow() {

        if ($("span[name='AllPhaseRefCost']").text() != $("span[name='AllPhaseLastRefCost']").text()) {
            $("div[id = 'divAllPhaseLastRefCost']").show();
        }
        if ($("span[name='AllPhasePlanCost']").text() != $("span[name='AllPhaseLastPlanCost']").text()) {
            $("div[id = 'divAllPhaseLastPlanCost']").show();
        }
        if ($("span[name='AllPhasePlanDay']").text() != $("span[name='AllPhaseLastPlanDay']").text()) {
            $("div[id = 'divAllPhaseLastPlanDay']").show();
        }
        if ($("span[name='AllPhasePlanStart']").text() != $("span[name='AllPhaseLastPlanStart']").text()) {
            $("div[id = 'divAllPhaseLastPlanStart']").show();
        }
        if ($("span[name='AllPhasePlanEnd']").text() != $("span[name='AllPhaseLastPlanEnd']").text()) {
            $("div[id = 'divAllPhaseLastPlanEnd']").show();
        }
        if ($("span[name='AllPhasePlanAverage']").text() != $("span[name='AllPhaseLastPlanAverage']").text()) {
            $("div[id = 'divAllPhaseLastPlanAverage']").show();
        }
    }

    function SetLastVersionShow(tr) {

        if ($(tr).find("#divPlanStart").text() != $(tr).find("#divLastPlanStart").text()) {
            $(tr).find("#divLastPlanStart").show();
        }
        if ($(tr).find("#divPlanEnd").text() != $(tr).find("#divLastPlanEnd").text()) {
            $(tr).find("#divLastPlanEnd").show();
        }
        if ($(tr).find("#divPlanCost").text() != $(tr).find("#divLastPlanCost").text()) {
            $(tr).find("#divLastPlanCost").show();
        }
        if ($(tr).find("#divRefCost").text() != $(tr).find("#divLastRefCost").text()) {
            $(tr).find("#divLastRefCost").show();
        }

        //RefCost
        var divHead = $(tr).parent().parent().parent().parent().prev();
        var divPhaseLastRefCost = divHead.find("div[id='divPhaseLastRefCost']");
        var spanPhaseLastRefCost = divHead.find("span[name='PhaseLastRefCost']");
        var spanPhaseRefCost = divHead.find("span[name='PhaseRefCost']");
        if (spanPhaseRefCost.text() != spanPhaseLastRefCost.text() && spanPhaseLastRefCost.text() != "0") {
            divPhaseLastRefCost.show();
        }

        //PlanCost
        var divPhaseLastPlanCost = divHead.find("div[id='divPhaseLastPlanCost']");
        var spanPhaseLastPlanCost = divHead.find("span[name='PhaseLastPlanCost']");
        var spanPhasePlanCost = divHead.find("span[name='PhasePlanCost']");
        if (spanPhasePlanCost.text() != spanPhaseLastPlanCost.text() && spanPhaseLastPlanCost.text() != "0") {
            divPhaseLastPlanCost.show();
        }

        //PlanDay                      
        var divPhaseLastPlanDay = divHead.find("div[id='divPhaseLastPlanDay']");
        var spanPhaseLastPlanDay = divHead.find("span[name='PhaseLastPlanDay']");
        var spanPhasePlanDay = divHead.find("span[name='PhasePlanDay']");
        if (spanPhasePlanDay.text() != spanPhaseLastPlanDay.text()) {
            divPhaseLastPlanDay.show();
        }

        //PlanStart
        var divPhaseLastPlanStart = divHead.find("div[id='divPhaseLastPlanStart']");
        var spanPhaseLastPlanStart = divHead.find("span[name='PhaseLastPlanStart']");
        var spanPhasePlanStart = divHead.find("span[name='PhasePlanStart']");
        if (spanPhasePlanStart.text() != spanPhaseLastPlanStart.text()) {
            divPhaseLastPlanStart.show();
        }

        //PlanEnd
        var divPhaseLastPlanEnd = divHead.find("div[id='divPhaseLastPlanEnd']");
        var spanPhaseLastPlanEnd = divHead.find("span[name='PhaseLastPlanEnd']");
        var spanPhasePlanEnd = divHead.find("span[name='PhasePlanEnd']");
        if (spanPhasePlanEnd.text() != spanPhaseLastPlanEnd.text()) {
            divPhaseLastPlanEnd.show();
        }

        //PlanAverage
        var divPhaseLastPlanAverage = divHead.find("div[id='divPhaseLastPlanAverage']");
        var spanPhaseLastPlanAverage = divHead.find("span[name='PhaseLastPlanAverage']");
        var spanPhasePlanAverage = divHead.find("span[name='PhasePlanAverage']");
        if (spanPhasePlanAverage.text() != spanPhaseLastPlanAverage.text() && spanPhaseLastPlanAverage.text() != "0.00") {
            divPhaseLastPlanAverage.show();
        }
    }

    function SetSchedulePlanButton() {

        //所有按钮设置样式
        var $SchedulePlanButtons = $("#divSchedulePlanHead input[type='button']");
        $SchedulePlanButtons.addClass("ButtonTransparent").hover(function() {
            if ($(this).attr("class") == "ButtonTransparent") {
                $(this).removeClass("ButtonTransparent");
                $(this).addClass("ButtonBackground");
            }
        }, function() {
            if ($(this).attr("class") == "ButtonBackground") {
                $(this).removeClass("ButtonBackground");
                $(this).addClass("ButtonTransparent");
            }
        });

        //默认所有样式
        $("#ButtonShowTaskAll").addClass("ButtonCurrent");

        //默认隐藏同意和拒绝按钮
        $("#ButtonApprove").hide();
        $("#ButtonReject").hide();

        //添加按钮事件
        $("#ButtonShowTaskAll").click(function() {
            $(this).removeClass();
            $(this).addClass("ButtonCurrent");
            $("#ButtonShowTaskAssignToMe").removeClass();
            $("#ButtonShowTaskAssignToMe").addClass("ButtonTransparent");
            $("#DropDownListCondition").val("");
            $("#ButtonCreate").show();
            $("#ButtonSubmit").show();
            $("#ButtonRecall").show();
            $("#ButtonDelete").show();
            $("#ButtonModify").show();
            $("#ButtonApprove").hide();
            $("#ButtonReject").hide();
            SetLastVersionHide();
            ShowTaskAll();
        });

        $("#ButtonShowTaskAssignToMe").click(function() {
            $(this).removeClass();
            $(this).addClass("ButtonCurrent");
            $("#ButtonShowTaskAll").removeClass();
            $("#ButtonShowTaskAll").addClass("ButtonTransparent");
            $("#DropDownListCondition").val("");
            $("#ButtonCreate").show();
            $("#ButtonSubmit").show();
            $("#ButtonRecall").show();
            $("#ButtonDelete").show();
            $("#ButtonModify").show();
            $("#ButtonApprove").hide();
            $("#ButtonReject").hide();
            SetLastVersionHide();
            ShowTaskAssignToMe();
        });

        $("#ButtonCreate").click(function() {
            ButtonCreate_OnClientClick();
        });

        $("#ButtonSubmit").click(function() {
            ButtonSubmit_OnClientClick();
        });

        $("#ButtonRecall").click(function() {
            ButtonRecall_OnClientClick();
        });

        $("#ButtonDelete").click(function() {
            ButtonDelete_OnClientClick();
        });

        $("#ButtonModify").click(function() {
            ButtonModify_OnClientClick();
        });

        $("#ButtonApprove").click(function() {
            ButtonApprove_OnClientClick();
        });

        $("#ButtonReject").click(function() {
            ButtonReject_OnClientClick();
        });
    }

    //所有任务
    function ShowTaskAll() {
        $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").show();
        SetSchedulePlanAllPhaseHead();
        return false;
    }

    //指派给我
    function ShowTaskAssignToMe() {
        $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader)").each(function() {
            if ($(this).attr("resource").toUpperCase() == LoginName.toUpperCase()) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
        SetSchedulePlanAllPhaseHead();
        return false;
    }

    //计算参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均
    function SetSchedulePlanAllPhaseHead() {

        var allLastRefCost = 0; //上一版本所有阶段参考总工时
        var allLastPlanCost = 0; //上一版本所有阶段计划总工时
        var allLastPlanDay = 0; //上一版本所有阶段计划总工期
        var allLastPlanStart; //上一版本所有阶段计划开始
        var allLastPlanEnd; //上一版本所有阶段计划结束
        var allLastPlanAverage = 0; //上一版本所有阶段计划平均

        var allRefCost = 0; //所有阶段参考总工时
        var allPlanCost = 0; //所有阶段计划总工时
        var allPlanDay = 0; //所有阶段计划总工期
        var allPlanStart; //所有阶段计划开始
        var allPlanEnd; //所有阶段计划结束
        var allPlanAverage = 0; //所有阶段计划平均

        $SchedulePlanTables.each(function() {

            //阶段参考总工时
            var phaseRefCost = GetPhaseRefCost(this);
            allRefCost = allRefCost + phaseRefCost;

            //阶段计划总工时
            var phasePlanCost = GetPhasePlanCost(this);
            allPlanCost = allPlanCost + phasePlanCost;

            //阶段计划开始
            var phasePlanStart = GetPhasePlanStart(this);
            if (allPlanStart == undefined) {
                allPlanStart = phasePlanStart;
            }
            else {
                if (allPlanStart == "" || (phasePlanStart != "" && phasePlanStart < allPlanStart)) {
                    allPlanStart = phasePlanStart;
                }
            }

            //阶段计划结束
            var phasePlanEnd = GetPhasePlanEnd(this);

            if (allPlanEnd == undefined) {
                allPlanEnd = phasePlanEnd;
            }
            else {
                if (allPlanEnd == "" || (phasePlanEnd != "" && phasePlanEnd < allPlanEnd)) {
                    allPlanEnd = phasePlanEnd;
                }
            }

            //阶段计划总工期
            var phasePlanDay = DateSpan(phasePlanStart, phasePlanEnd);

            var span = $(this).parent().parent().siblings("div[id='divHead']").find("span[name='PhasePlanDay']");
            $(span).text(phasePlanDay);


            //阶段计划平均
            var phasePlanAverage = 0;
            var taskNum = $(this).find("tr:not(.GVHeader):visible").size();
            if (taskNum > 0) {
                phasePlanAverage = parseFloat(phasePlanCost / taskNum);
            }
            var span = $(this).parent().parent().siblings("div[id='divHead']").find("span[name='PhasePlanAverage']");
            $(span).text((taskNum == 0 && parseFloat(phasePlanAverage).toFixed(2) == 0.00) ? "" : parseFloat(phasePlanAverage).toFixed(2));

            //上一版本阶段参考总工时
            var phaseLastRefCost = GetPhaseLastRefCost(this);
            allLastRefCost = allLastRefCost + phaseLastRefCost;

            //上一版本阶段计划总工时
            var phaseLastPlanCost = GetPhaseLastPlanCost(this);
            allLastPlanCost = allLastPlanCost + phaseLastPlanCost;

            //上一版本阶段计划开始
            var phaseLastPlanStart = GetPhaseLastPlanStart(this);
            if (allLastPlanStart == undefined) {
                allLastPlanStart = phaseLastPlanStart;
            }
            else {
                if (allLastPlanStart == "" || (phaseLastPlanStart != "" && phaseLastPlanStart < allLastPlanStart)) {
                    allLastPlanStart = phaseLastPlanStart;
                }
            }

            //上一版本阶段计划结束
            var phaseLastPlanEnd = GetPhaseLastPlanEnd(this);

            if (allLastPlanEnd == undefined) {
                allLastPlanEnd = phaseLastPlanEnd;
            }
            else {
                if (allLastPlanEnd == "" || (phaseLastPlanEnd != "" && phaseLastPlanEnd < allLastPlanEnd)) {
                    allLastPlanEnd = phaseLastPlanEnd;
                }
            }

            //上一版本阶段计划总工期
            var phaseLastPlanDay = DateSpan(phaseLastPlanStart, phaseLastPlanEnd);
            var span = $(this).parent().parent().siblings("div[id='divHead']").find("span[name='PhaseLastPlanDay']");
            $(span).text(phaseLastPlanDay);

            //上一版本阶段计划平均
            var phaseLastPlanAverage = 0;
            var taskNum = $(this).find("tr:not(.GVHeader):visible").size();
            if (taskNum > 0) {
                phaseLastPlanAverage = parseFloat(phaseLastPlanCost / taskNum);
            }
            var span = $(this).parent().parent().siblings("div[id='divHead']").find("span[name='PhaseLastPlanAverage']");
            $(span).text((taskNum == 0 && parseFloat(phaseLastPlanAverage).toFixed(2) == 0.00) ? "" : parseFloat(phaseLastPlanAverage).toFixed(2));

        });
        var totalTaskNum = $("table[id*='SchedulePlanByPhase']").find("tr:not(.GVHeader):visible").size();
        if (totalTaskNum > 0) {
            allPlanAverage = allPlanCost / totalTaskNum;
            allLastPlanAverage = allLastPlanCost / totalTaskNum;
        }

        $("span[id$='labelAllPhaseRefCost']").text((totalTaskNum == 0 && allRefCost == 0) ? "" : allRefCost);
        $("span[id$='labelAllPhasePlanCost']").text((totalTaskNum == 0 && allPlanCost == 0) ? "" : allPlanCost);
        $("span[id$='labelAllPhasePlanStart']").text(allPlanStart);
        $("span[id$='labelAllPhasePlanEnd']").text(allPlanEnd);
        $("span[id$='labelAllPhasePlanDay']").text(DateSpan(allPlanStart, allPlanEnd));
        $("span[id$='labelAllPhasePlanAverage']").text((totalTaskNum == 0 && parseFloat(allPlanAverage).toFixed(2) == 0.00) ? "" : parseFloat(allPlanAverage).toFixed(2));

        $("span[name='AllPhaseLastRefCost']").text((totalTaskNum == 0 && allLastRefCost == 0) ? "" : allLastRefCost);
        $("span[name='AllPhaseLastPlanCost']").text((totalTaskNum == 0 && allLastPlanCost == 0) ? "" : allLastPlanCost);
        $("span[name='AllPhaseLastPlanStart']").text(allLastPlanStart);
        $("span[name='AllPhaseLastPlanEnd']").text(allLastPlanEnd);
        $("span[name='AllPhaseLastPlanDay']").text(DateSpan(allLastPlanStart, allLastPlanEnd));
        $("span[name='AllPhaseLastPlanAverage']").text((totalTaskNum == 0 && parseFloat(allLastPlanAverage).toFixed(2) == 0.00) ? "" : parseFloat(allLastPlanAverage).toFixed(2));
    }

    //取得阶段参考工时
    function GetPhaseRefCost(table) {

        var phaseRefCost = 0;

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divRefCost']");
        divs.each(function() {
            phaseRefCost = phaseRefCost + parseInt($(this).text());
        });

        var span = $(table).parent().parent().siblings("div[id='divHead']").find("span[name='PhaseRefCost']");
        $(span).text((divs.size() == 0 && phaseRefCost == 0) ? "" : phaseRefCost);

        return phaseRefCost;
    }

    //取得阶段计划工时
    function GetPhasePlanCost(table) {

        var phasePlanCost = 0;

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divPlanCost']");
        divs.each(function() {
            phasePlanCost = phasePlanCost + parseInt($(this).text());
        });

        var span = $(table).parent().parent().siblings("div[id='divHead']").find("span[name='PhasePlanCost']");
        $(span).text((divs.size() == 0 && phasePlanCost == 0) ? "" : phasePlanCost);

        return phasePlanCost;
    }

    //取得阶段计划开始
    function GetPhasePlanStart(table) {

        var phasePlanStart = "";

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divPlanStart']");
        divs.each(function(index, div) {
            if (index == 0) {
                phasePlanStart = $(this).text();
            }
            else {
                if ((phasePlanStart == "") || ($(this).text() != "" && $(this).text() < phasePlanStart)) {
                    phasePlanStart = $(this).text();
                }
            }
        });

        var span = $(table).parent().parent().siblings("div[id='divHead']").find("span[name='PhasePlanStart']");
        $(span).text(phasePlanStart);

        return phasePlanStart;
    }

    //取得阶段计划结束
    function GetPhasePlanEnd(table) {

        var phasePlanEnd = "";

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divPlanEnd']");
        divs.each(function(index, div) {
            if (index == 0) {
                phasePlanEnd = $(this).text();
            }
            else {
                if ((phasePlanEnd == "") || ($(this).text() > phasePlanEnd)) {
                    phasePlanEnd = $(this).text();
                }
            }
        });

        var span = $(table).parent().parent().siblings("div[id='divHead']").find("span[name='PhasePlanEnd']");
        $(span).text(phasePlanEnd);

        return phasePlanEnd;
    }

    //取得阶段上一版本参考工时
    function GetPhaseLastRefCost(table) {

        var phaseLastRefCost = 0;
        var divLastRefCost;
        var divRefCost;
        var trs = $(table).find("tr:not(.GVHeader):visible");
        trs.each(function() {
            divLastRefCost = $(this).find("div[id='divLastRefCost']");
            divRefCost = $(this).find("div[id='divRefCost']");
            if ($(divLastRefCost).text() != "") {
                phaseLastRefCost = phaseLastRefCost + parseInt($(divLastRefCost).text());
            }
            else {
                phaseLastRefCost = phaseLastRefCost + parseInt($(divRefCost).text());
            }
        });

        var span = $(table).parent().parent().siblings("div[id='divHead']").find("span[name='PhaseLastRefCost']");
        $(span).text((trs.size() == 0 && phaseLastRefCost == 0) ? "" : phaseLastRefCost);

        return phaseLastRefCost;
    }

    //取得阶段上一版本计划工时
    function GetPhaseLastPlanCost(table) {

        var phaseLastPlanCost = 0;
        var divLastPlanCost;
        var divPlanCost;
        var trs = $(table).find("tr:not(.GVHeader):visible");
        trs.each(function() {
            divLastPlanCost = $(this).find("div[id='divLastPlanCost']");
            divPlanCost = $(this).find("div[id='divPlanCost']");
            if ($(divLastPlanCost).text() != "") {
                phaseLastPlanCost = phaseLastPlanCost + parseInt($(divLastPlanCost).text());
            }
            else {
                phaseLastPlanCost = phaseLastPlanCost + parseInt($(divPlanCost).text());
            }
        });

        var span = $(table).parent().parent().siblings("div[id='divHead']").find("span[name='PhaseLastPlanCost']");
        $(span).text((trs.size() == 0 && phaseLastPlanCost == 0) ? "" : phaseLastPlanCost);

        return phaseLastPlanCost;
    }

    //取得阶段上一版本计划开始
    function GetPhaseLastPlanStart(table) {

        var phaseLastPlanStart = "";
        var divLastPlanStart;
        var divPlanStart;
        var trs = $(table).find("tr:not(.GVHeader):visible");
        trs.each(function(index, div) {
            divLastPlanStart = $(this).find("div[id='divLastPlanStart']");
            divPlanStart = $(this).find("div[id='divPlanStart']");
            if (index == 0) {
                if ($(divLastPlanStart).text() != "") {
                    phaseLastPlanStart = $(divLastPlanStart).text();
                }
                else {
                    phaseLastPlanStart = $(divPlanStart).text();
                }
            }
            else {
                if ($(divLastPlanStart).text() != "") {
                    if ($(divLastPlanStart).text() < phaseLastPlanStart) {
                        phaseLastPlanStart = $(divLastPlanStart).text();
                    }
                }
                else {
                    if ($(divPlanStart).text() < phaseLastPlanStart) {
                        phaseLastPlanStart = $(divPlanStart).text();
                    }
                }
            }
        });

        var span = $(table).parent().parent().siblings("div[id='divHead']").find("span[name='PhaseLastPlanStart']");
        $(span).text(phaseLastPlanStart);

        return phaseLastPlanStart;
    }

    //取得阶段上一版本计划结束
    function GetPhaseLastPlanEnd(table) {

        var phaseLastPlanEnd = "";
        var divLastPlanEnd;
        var divPlanEnd;
        var trs = $(table).find("tr:not(.GVHeader):visible");
        trs.each(function(index, div) {
            divLastPlanEnd = $(this).find("div[id='divLastPlanEnd']");
            divPlanEnd = $(this).find("div[id='divPlanEnd']");
            if (index == 0) {
                if ($(divLastPlanEnd).text() != "") {
                    phaseLastPlanEnd = $(divLastPlanEnd).text();
                }
                else {
                    phaseLastPlanEnd = $(divPlanEnd).text();
                }
            }
            else {
                if ($(divLastPlanEnd).text() != "") {
                    if ($(divLastPlanEnd).text() > phaseLastPlanEnd) {
                        phaseLastPlanEnd = $(divLastPlanEnd).text();
                    }
                }
                else {
                    if ($(divPlanEnd).text() > phaseLastPlanEnd) {
                        phaseLastPlanEnd = $(divPlanEnd).text();
                    }
                }
            }
        });

        var span = $(table).parent().parent().siblings("div[id='divHead']").find("span[name='PhaseLastPlanEnd']");
        $(span).text(phaseLastPlanEnd);

        return phaseLastPlanEnd;
    }

    function ShowHideAllGrid(imageShowHideAllGridId, divDesignId, imgDesignId, divDevelopmentId, imgDevelopmentId, divTestId, imgTestId, divReleaseId, imgReleaseId, divSupportId, imgSupportId) {

        var imageShowHideAllGridSrc = document.getElementById(imageShowHideAllGridId).src;
        var imageShowHideAllGrid = document.getElementById(imageShowHideAllGridId);
        var showOrHide = imageShowHideAllGridSrc.substring(imageShowHideAllGridSrc.lastIndexOf("/"), imageShowHideAllGridSrc.length);

        var divDesign = document.getElementById(divDesignId);
        var imageDivDesign = document.getElementById(imgDesignId);

        var divDevelopment = document.getElementById(divDevelopmentId);
        var imageDivDevelopment = document.getElementById(imgDevelopmentId);

        var divTest = document.getElementById(divTestId);
        var imageDivTest = document.getElementById(imgTestId);

        var divRelease = document.getElementById(divReleaseId);
        var imageDivRelease = document.getElementById(imgReleaseId);

        var divSupport = document.getElementById(divSupportId);
        var imageDivSupport = document.getElementById(imgSupportId);


        if (showOrHide.toLowerCase() == "/hide.gif") {
            DivShowChange(divDesign, imageDivDesign, "block", "appear.gif");
            DivShowChange(divDevelopment, imageDivDevelopment, "block", "appear.gif");
            DivShowChange(divTest, imageDivTest, "block", "appear.gif");
            DivShowChange(divRelease, imageDivRelease, "block", "appear.gif");
            DivShowChange(divSupport, imageDivSupport, "block", "appear.gif");

            imageShowHideAllGrid.src = "../../SysFrame/images/appear.gif";
        }
        else if (showOrHide.toLowerCase() == "/appear.gif") {
            DivShowChange(divDesign, imageDivDesign, "none", "hide.gif");
            DivShowChange(divDevelopment, imageDivDevelopment, "none", "hide.gif");
            DivShowChange(divTest, imageDivTest, "none", "hide.gif");
            DivShowChange(divRelease, imageDivRelease, "none", "hide.gif");
            DivShowChange(divSupport, imageDivSupport, "none", "hide.gif");

            imageShowHideAllGrid.src = "../../SysFrame/images/hide.gif";
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

    function ShowHideGrid(divId, imgId) {

        var div = document.getElementById(divId);
        var img = document.getElementById(imgId);

        if (div.style["display"] == "none") {
            div.style["display"] = "block";
            img.src = "../../SysFrame/images/appear.gif";
        }
        else {
            div.style["display"] = "none";
            img.src = "../../SysFrame/images/hide.gif";
        }
    }

    //新建计划
    function ButtonCreate_OnClientClick() {

        var pmsId = "<%=PmsID %>";
        var crId = "<%=CrId %>";
        var url = "../Maintain/AddNewTask.aspx?Action=Add&PmsID=" + pmsId + "&CrId=" + crId + "&Phase=" + "" + "&Radom=" + Math.random();
        var features = "dialogWidth=630px;dialogHeight=510px;center=yes;help=no;status=no;scroll=no";
        var dialogArguments = new Object();
        dialogArguments.window = window;
        window.showModalDialog(url, dialogArguments, features);
        return false;
    }

    //提交计划
    function ButtonSubmit_OnClientClick() {

        ChangeItemStatus("1", "2", "待审核", "Not Submit!");

        return false;
    }

    //撤回计划
    function ButtonRecall_OnClientClick() {

        ChangeItemStatus("2", "1", "未提交", "Waiting Approve!");

        return false;
    }

    //删除计划
    function ButtonDelete_OnClientClick() {

        ChangeItemsExist();

        return false;
    }

    //修改资源
    function ButtonModify_OnClientClick() {
        var checkBoxs = $("[name=CheckItem]:checkbox:checked");
        if (checkBoxs.length == 0) {
            alert("please at least select one item!");
            return false;
        }

        var serials = "";
        var result = true;
        var oldResource = "";

        checkBoxs.each(function(index, div) {

            var tr = $(this).closest("tr:not(.GVHeader)");
            var creator = tr.attr("createuser");
            var auditStatus = tr.attr("auditstatus");
            var taskStatus = tr.attr("taskstatus");
            var resource = tr.find("#divResource").text();

            if ($.trim(creator) != "" && creator != LoginName) {
                alert("You can not modify resource, you are not creator!");
                result = false;
                return false;
            }            
            if (index == 0) {
                oldResource = resource;
            }
            else {
                oldResource == "" ? resource : oldResource;
            }
            if (resource != oldResource) {
                alert("please ensure all selected item have the same resource!");
                result = false;
                return false;
            }
            serials = serials + tr.attr("serial") + ";";
        });

        if (result == false) {
            return result;
        }

        serials = serials.substr(0, serials.length - 1);
        if (serials.length == 0) {
            return false;
        }
        var pmsId = "<%=PmsID %>";
        var url = "../Maintain/ModifyResource.aspx?PmsID=" + pmsId + "&serials=" + serials + "&resource=" + oldResource + "&Radom=" + Math.random();
        var features = "dialogWidth=630px;dialogHeight=200px;center=yes;help=no;status=no;scroll=no";
        var resultResource = window.showModalDialog(url, "", features);

        if (resultResource == undefined || resultResource == null) {
            return false;
        }
        checkBoxs.each(function() {
            tr = $(this).closest("tr:not(.GVHeader)");
            tr.find("[name = CheckItem]").attr("checked", false);
            tr.find("#divResource").text(resultResource);
            tr.find("#divResource").attr("title", resultResource);
        });

        return false;
    }

    //同意
    function ButtonApprove_OnClientClick() {

        ChangeItemStatus("2", "3", "已批准", "Waiting Approve!");

        return false;
    }

    //拒绝
    function ButtonReject_OnClientClick() {

        ChangeItemStatus("2", "4", "已拒绝", "Waiting Approve!");

        return false;
    }


    //改变选中项存在状态
    function ChangeItemsExist() {
        var checkBoxs = $("[name=CheckItem]:checkbox:checked");
        if (checkBoxs.length == 0) {
            alert("please at least select one item!");
            return false;
        }

        var tr;
        var serials = "";
        var result = true;

        checkBoxs.each(function() {

            tr = $(this).closest("tr:not(.GVHeader)");
            //只有创建者才能删除
            //            var creator = $(this).closest("tr").attr("createuser");
            //            var auditStatus = $(this).closest("tr").attr("auditstatus");
            //            var taskStatus = $(this).closest("tr").attr("taskstatus");
            var creator = tr.attr("createuser");
            var auditStatus = tr.attr("auditstatus");
            var taskStatus = tr.attr("taskstatus");

            if ($(this).closest("table").find("tr").size() == 2) {
                alert("The last item,can not be deleted!");
                result = false;
                return false;
            }
            if ($.trim(creator) != "" && creator != LoginName) {
                alert("You can't delete the task, you are not creator!");
                result = false;
                return false;
            }
            if (auditStatus != 1) {
                alert("You can't delete the task, beacause it has been submitted or approved!");
                result = false;
                return false;
            }
            if (taskStatus != 1) {
                alert("You can't delete the task beacause it has manpower already!");
                result = false;
                return false;
            }
            serials = serials + tr.attr("serial") + ";";
        });

        if (result == false) {
            return result;
        }

        serials = serials.substr(0, serials.length - 1);
        if (serials.length == 0) {
            return false;
        }
        if (confirm("Are you sure to delete this items")) {
            //Jquery ajax 调用 WebService
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../WebService/SchedulePlanByPhase.asmx/DeleteSdpDetailBySerials",
                data: "{serials:'" + serials + "'}",
                dataType: 'json',
                success: function(result) {
                    if (result) {
                        checkBoxs.each(function() {
                            tr = $(this).closest("tr:not(.GVHeader)").remove();
                        });
                        //重新计算Head上的参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均
                        SetSchedulePlanAllPhaseHead();
                    } else {
                        alert("Delete this items failed!");
                    }
                }
            });
        }
    }
    //改变选中项状态

    //ChangeItemStatus("2", "4", "已拒绝", "Waiting Approve!");
    function ChangeItemStatus(thisStatus, submitStatus, submitStatusDesc, message) {
        var checkBoxs = $("[name=CheckItem]:checkbox:checked");
        if (checkBoxs.length == 0) {
            alert("please at least select one item!");
            return false;
        }

        var tr;
        var serials = "";
        var result = true;

        checkBoxs.each(function() {

            tr = $(this).closest("tr:not(.GVHeader)");

            //选中项状态和按钮要匹配,如只能提交未提交的任务
            if (tr.attr("AuditStatus") != thisStatus) {
                alert("please ensure all selected item is " + message);
                result = false;
                return false;
            }

            //提交和撤回必须是本人创建的
            if (submitStatus == "1" || submitStatus == "2") {
                if (tr.attr("CreateUser") != LoginName) {
                    alert("please ensure all selected item be created by yourself!");
                    result = false;
                    return false;
                }
            }
            serials = serials + tr.attr("serial") + ";";
        });

        if (result == false) {
            return result;
        }

        serials = serials.substr(0, serials.length - 1);
        if (serials.length == 0) {
            return false;
        }
        var auditComment = "";
        if (submitStatus == "4") {
            var pmsId = "<%=PmsID %>";
            var url = "../Maintain/AuditComment.aspx?PmsID=" + pmsId + "&serials=" + serials + "&Radom=" + Math.random();
            var features = "dialogWidth=630px;dialogHeight=200px;center=yes;help=no;status=no;scroll=no";
            var resultReturn = window.showModalDialog(url, "", features);

            if (resultReturn == undefined || resultReturn == null) {
                return false;
            }
            auditComment = resultReturn;
        }

        $.ajaxSetup({
            async: true,
            cache: false,
            type: "POST",
            contentType: "application/json",
            url: "../WebService/SchedulePlanByPhase.asmx/UpdateSdpDetailAuditStatus",
            dataType: 'json'
        });

        //Jquery ajax 调用 WebService
        $.ajax({
        //data: "{serials:'" + serials + "',auditStatus:" + parseInt(submitStatus) + "}",
        data: "{serials:'" + serials + "',auditStatus:" + parseInt(submitStatus) + ",auditComment:'" + auditComment + "'}",
            success: function(result) {
                if (result) {
                    checkBoxs.each(function() {
                        tr = $(this).closest("tr");
                        tr.find("[name = CheckItem]").attr("checked", false);
                        tr.find("#divAuditStatus").text(submitStatusDesc);
                        tr.find("#divAuditStatus").attr("title", submitStatusDesc);
                        tr.attr("AuditStatus", submitStatus);
                        tr.removeAttr("auditstatus");
                        tr.attr("auditstatus", submitStatus);
                    });
                    switch (submitStatus) {
                        case "1":
                            alert("Task has been recalled!");
                            break;
                        case "2":
                            alert("Task has been submitted!");
                            break;
                        case "3":
                            alert("Task has been approved!");
                            break;
                        case "4":
                            alert("Task has been rejected!");
                            break;
                        default: break;
                    }
                } else {
                    alert("Submit failed!");
                }
            },
            error: function(xmlHttpRequest, msg) {
                alert("Call WebService Error!");
            }
        });
    }

</script>

<div id="divSchedulePlan" class="DivPlan">
    <div id="divSchedulePlanHead" class="DivPlanHead">
        <input id="ButtonShowTaskAll" type="button" value="所有" class="ButtonCurrent" />
        <input id="ButtonShowTaskAssignToMe" type="button" value="指派给我" class="ButtonTransparent" />
        <select id="DropDownListCondition" style="width: auto">
            <option value=""></option>
            <option value="1">由我创建</option>
            <option value="2">由他人创建</option>
            <option value="3">需我提交</option>
            <option value="4">需我审核</option>
            <option value="5">需我代理审核</option>
        </select>
        <input id="ButtonCreate" type="button" value="新建计划" class="ButtonTransparent" />
        <input id="ButtonSubmit" type="button" value="提交计划" class="ButtonTransparent" />
        <input id="ButtonRecall" type="button" value="撤回计划" class="ButtonTransparent" />
        <input id="ButtonDelete" type="button" value="删除计划" class="ButtonTransparent" />
        <input id="ButtonModify" type="button" value="修改资源" class="ButtonTransparent" />
        <input id="ButtonApprove" type="button" value="同意" class="ButtonTransparent" />
        <input id="ButtonReject" type="button" value="拒绝" class="ButtonTransparent" />
    </div>
    <div id="divSchedulePlanAllPhase" class="DivAllPhase">
        <table class="DivAllPhaseHead" style="width: 910px;">
            <tr>
                <td>
                    <asp:Image ID="ImageShowHideAllGrid" runat="server" ImageUrl="~/SysFrame/images/hide.gif"
                        Style="cursor: pointer"></asp:Image>
                </td>
                <td style="width: 110px">
                    <span class="SpanAllPhase">总览</span>
                </td>
                <td>
                    <span class="SpanAllPhase">参考总工时：</span>
                </td>
                <td style="width: 30px">
                    <div>
                        <span id="labelAllPhaseRefCost" name="AllPhaseRefCost" class="SpanAllPhaseValue">
                        </span>
                    </div>
                    <div id="divAllPhaseLastRefCost" style="display: none">
                        <asp:Label ID="LabelAllPhaseLastRefCost" name="AllPhaseLastRefCost" runat="server"
                            ForeColor="Blue"> </asp:Label>
                    </div>
                </td>
                <td>
                    <span class="SpanAllPhase">计划总工时：</span>
                </td>
                <td style="width: 30px">
                    <div>
                        <span id="labelAllPhasePlanCost" name="AllPhasePlanCost" class="SpanAllPhaseValue">
                        </span>
                    </div>
                    <div id="divAllPhaseLastPlanCost" style="display: none">
                        <asp:Label ID="LabelAllPhaseLastPlanCost" name="AllPhaseLastPlanCost" runat="server"
                            ForeColor="Blue"> </asp:Label>
                    </div>
                </td>
                <td>
                    <span class="SpanAllPhase">计划总工期：</span>
                </td>
                <td style="width: 25px">
                    <div>
                        <span id="labelAllPhasePlanDay" name="AllPhasePlanDay" class="SpanAllPhaseValue">
                        </span>
                    </div>
                    <div id="divAllPhaseLastPlanDay" style="display: none">
                        <asp:Label ID="LabelAllPhaseLastPlanDay" runat="server" name="AllPhaseLastPlanDay"
                            ForeColor="Blue"></asp:Label>
                    </div>
                </td>
                <td>
                    <span class="SpanAllPhase">计划开始：</span>
                </td>
                <td style="width: 80px">
                    <div>
                        <span id="labelAllPhasePlanStart" name="AllPhasePlanStart" class="SpanAllPhaseValue">
                        </span>
                    </div>
                    <div id="divAllPhaseLastPlanStart" style="display: none">
                        <asp:Label ID="LabelAllPhaseLastPlanStart" name="AllPhaseLastPlanStart" runat="server"
                            ForeColor="Blue"></asp:Label>
                    </div>
                </td>
                <td>
                    <span class="SpanAllPhase">计划结束：</span>
                </td>
                <td style="width: 80px">
                    <div>
                        <span id="labelAllPhasePlanEnd" name="AllPhasePlanEnd" class="SpanAllPhaseValue">
                        </span>
                    </div>
                    <div id="divAllPhaseLastPlanEnd" style="display: none">
                        <asp:Label ID="LabelAllPhaseLastPlanEnd" runat="server" name="AllPhaseLastPlanEnd"
                            ForeColor="Blue"></asp:Label>
                    </div>
                </td>
                <td>
                    <span class="SpanAllPhase">计划平均：</span>
                </td>
                <td style="width: 57px">
                    <div>
                        <span id="labelAllPhasePlanAverage" name="AllPhasePlanAverage" class="SpanAllPhaseValue">
                        </span>
                    </div>
                    <div id="divAllPhaseLastPlanAverage" style="display: none">
                        <asp:Label ID="LabelAllPhaseLastPlanAverage" runat="server" name="AllPhaseLastPlanAverage"
                            ForeColor="Blue"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        <div class="DivAllPhaseBody">
            <div id="divDesign">
                <uc1:SchedulePlanByPhase ID="SchedulePlanByPhaseDesign" runat="server" />
            </div>
            <div style="height: 12px">
            </div>
            <div id="divDevelopment">
                <uc1:SchedulePlanByPhase ID="SchedulePlanByPhaseDevelopment" runat="server" />
            </div>
            <div style="height: 12px">
            </div>
            <div id="divTest">
                <uc1:SchedulePlanByPhase ID="SchedulePlanByPhaseTest" runat="server" />
            </div>
            <div style="height: 12px">
            </div>
            <div id="divRelease">
                <uc1:SchedulePlanByPhase ID="SchedulePlanByPhaseRelease" runat="server" />
            </div>
            <div style="height: 12px">
            </div>
            <div id="divSupport">
                <uc1:SchedulePlanByPhase ID="SchedulePlanByPhaseSupport" runat="server" />
            </div>
        </div>
    </div>
</div>
