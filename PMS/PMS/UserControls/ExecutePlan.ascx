<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExecutePlan.ascx.cs"
    Inherits="PMS.PMS.UserControls.ExecutePlan" %>
<%@ Register Src="ExecutePlanByPhase.ascx" TagName="ExecutePlanByPhase" TagPrefix="uc1" %>
<link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #divAllPhaseHead ul
    {
        height: auto;
        float: left;
        list-style: none;
        margin: 0px;
        padding: 0px;
    }
    #divAllPhaseHead li
    {
        height: auto;
        float: left;
    }
</style>

<script src="../JavaScript/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>

<script type="text/javascript">

    //页面加载时执行
    $(document).ready(
    function() {
        window.$ExecutePlanTables = $("table[id*='ExecutePlanByPhase']"); //ExecutePlan所有阶段Table
        window.$ExecutePlanTableDataTrs = $("table[id*='ExecutePlanByPhase']").find("tr:not(.GVHeader)"); //SchedulePlan所有阶段Table数据行
        SetHeadSpanClass(); //设置HeadSpan样式
        SetExecutePlanImg(); //设置IMG样式和事件
        SetExecutePlanButton(); //设置按钮样式和事件
        SetDropDownListFilter(); //设置下拉框样式和事件
        SetExecutePlanAllPhaseHead(); //计算ExecutePlan参考总工时,计划总工时,计划总工期,计划开始,计划结束, 计划平均     
    }
  );

    //设置HeadSpan样式
    function SetHeadSpanClass() {
        $("#divAllPhaseHead li span:first-child").addClass("SpanAllPhase");
        $("#divAllPhaseHead li span:nth-child(2)").addClass("SpanAllPhaseValue");
        $("div[id='divPhaseHead'] li span:nth-child(1)").addClass("SpanPhase");
        $("div[id='divPhaseHead'] li span:nth-child(2)").addClass("SpanPhaseValue");
    }

    //设置IMG样式和事件
    function SetExecutePlanImg() {
        $("#imgExceutePlanAllPhaseHide").hide();
        $("img[id$='imgExceutePlanPhaseHide']").hide();

        $("#imgExceutePlanAllPhaseShow").click(function() {
            $(this).hide();
            $("img[id$='imgExceutePlanPhaseShow']").hide();
            $("#imgExceutePlanAllPhaseHide").show();
            $("img[id$='imgExceutePlanPhaseHide']").show();
            $ExecutePlanTables.parent().parent().show();
        });

        $("#imgExceutePlanAllPhaseHide").click(function() {
            $(this).hide();
            $("img[id$='imgExceutePlanPhaseHide']").hide();
            $("#imgExceutePlanAllPhaseShow").show();
            $("img[id$='imgExceutePlanPhaseShow']").show();
            $ExecutePlanTables.parent().parent().hide();
        });

        $("img[id='imgExceutePlanPhaseShow']").click(function() {
            $(this).hide();
            $(this).siblings("#imgExceutePlanPhaseHide").show();
            $(this).closest("div").next().show();
        });

        $("img[id='imgExceutePlanPhaseHide']").click(function() {
            $(this).hide();
            $(this).siblings("#imgExceutePlanPhaseShow").show();
            $(this).closest("div").next().hide();
        });
    }

    //设置按钮样式和事件
    function SetExecutePlanButton() {
        //所有按钮设置样式
        var $ExecutePlanButtons = $("#divExecutePlanHead input[type='button']");
        $ExecutePlanButtons.addClass("ButtonTransparent").hover(function() {
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
        $("#ButtonShowExecutePlanTaskAll").addClass("ButtonCurrent");

        //添加按钮事件
        $("#ButtonShowExecutePlanTaskAll").click(function() {
            $(this).removeClass();
            $(this).addClass("ButtonCurrent");
            $("#ButtonShowExecutePlanTaskAssignToMe").removeClass();
            $("#ButtonShowExecutePlanTaskAssignToMe").addClass("ButtonTransparent");
            $("#DropDownListFilter").val("");
            $("#ButtonSearch").removeClass();
            $("#ButtonSearch").addClass("ButtonTransparent");
            ShowExecutePlanTaskAll();
        });

        $("#ButtonShowExecutePlanTaskAssignToMe").click(function() {
            $(this).removeClass();
            $(this).addClass("ButtonCurrent");
            $("#ButtonShowExecutePlanTaskAll").removeClass();
            $("#ButtonShowExecutePlanTaskAll").addClass("ButtonTransparent");
            $("#DropDownListFilter").val("");
            $("#ButtonSearch").removeClass();
            $("#ButtonSearch").addClass("ButtonTransparent");
            ShowExecutePlanTaskAssignToMe();
        });

        $("#ButtonShowHideSearch").click(function() {
            $("#divSearch").toggle();
        });

        $("#ButtonSearch").click(function() {
            $(this).removeClass();
            $(this).addClass("ButtonCurrent");
            $("#ButtonShowExecutePlanTaskAll").removeClass();
            $("#ButtonShowExecutePlanTaskAll").addClass("ButtonTransparent");
            $("#ButtonShowExecutePlanTaskAssignToMe").removeClass();
            $("#ButtonShowExecutePlanTaskAssignToMe").addClass("ButtonTransparent");
            $("#DropDownListFilter").val("");
            ShowExecutePlanTaskOnSearch();
        });
    }

    //所有任务
    function ShowExecutePlanTaskAll() {
        $ExecutePlanTableDataTrs.show();
        SetExecutePlanAllPhaseHead();
        return false;
    }

    //指派给我
    function ShowExecutePlanTaskAssignToMe() {
        $ExecutePlanTableDataTrs.each(function() {
            if ($(this).attr("resource") == LoginName) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
        SetExecutePlanAllPhaseHead();
        return false;
    }

    //查询过滤下拉单
    function SetDropDownListFilter() {

        var $DropDownListFilter = $("#DropDownListFilter");
        $DropDownListFilter.addClass("DropDownList");

        //选项改变事件
        $DropDownListFilter.change(function() {
            var selectedValue = $(this).val();
            DropDownListFilterOnChange(selectedValue);
        });
    }

    //查询过滤下拉单OnChange事件
    function DropDownListFilterOnChange(selectedValue) {

        //重置过滤按钮样式
        $("#ButtonShowExecutePlanTaskAll").removeClass();
        $("#ButtonShowExecutePlanTaskAll").addClass("ButtonTransparent");
        $("#ButtonShowExecutePlanTaskAssignToMe").removeClass();
        $("#ButtonShowExecutePlanTaskAssignToMe").addClass("ButtonTransparent");
        $("#ButtonSearch").removeClass();
        $("#ButtonSearch").addClass("ButtonTransparent");
        switch (selectedValue) {
            case "1": //未开始
            case "2": //进行中
            case "3": //已完成
            case "4": //已关闭
            case "5": //已取消
            case "6": //已暂缓
                $ExecutePlanTableDataTrs.each(function() {
                    if ($(this).attr("taskstatus") == selectedValue) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
                SetExecutePlanAllPhaseHead();
                break;
            case "7": //由我创建 
                $ExecutePlanTableDataTrs.each(function() {
                    if ($(this).attr("createuser") == LoginName) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
                SetExecutePlanAllPhaseHead();
                break;
            case "8": //由别人创建
                $ExecutePlanTableDataTrs.each(function() {
                    if ($(this).attr("createuser") == LoginName) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
                SetExecutePlanAllPhaseHead();
                break;
            default: break;
        }
    }

    //自定义条件搜索
    function ShowExecutePlanTaskOnSearch() {

        var taskText = $("#DropDownListTask").val();
        var taskEqualContain = $("#DropDownListTaskEqualContain").val();
        var taskValue = $.trim($("#inputTaskValue").val()).toUpperCase();
        var andOr = $("#DropDownListAndOr").val();
        var taskCostText = $("#DropDownListTaskCost").val();
        var taskCostEqualContain = $("#DropDownListTaskCostEqualContain").val();
        var taskCostValue = $.trim($("#inputTaskCostValue").val()).toUpperCase();
        var taskValueInGrid = "";
        var taskCostValueInGrid = "";
        var evalResult = false;

        $ExecutePlanTableDataTrs.each(function() {
            taskValueInGrid = taskText == "1" ? $(this).attr("tasktypedesc") : (taskText == "2" ? $(this).attr("taskname") : $(this).attr("resource")); //任务类型或者任务名称或者资源
            taskCostValueInGrid = taskCostText == "1" ? $(this).attr("actualcost") : $(this).attr("plancost"); //实际工时或者计划工时
            evalResult = GetEvalResult(andOr, taskValue, taskValueInGrid, taskEqualContain, taskCostValue, taskCostValueInGrid, taskCostEqualContain);
            evalResult == true ? $(this).show() : $(this).hide();
        });
        SetExecutePlanAllPhaseHead();
        return false;
    }

    function GetEvalResult(andOr, taskValue, taskValueInGrid, taskEqualContain, taskCostValue, taskCostValueInGrid, taskCostEqualContain) {

        var evalResult = true;
        var evalString = "";
        var taskEvalString = "";
        var taskCostEvalString = "";

        if (taskValue == "" && taskCostValue == "") {
            return true;
        }

        taskEvalString = taskValue == "" ? "true" : (taskEqualContain == "1" ? taskValueInGrid == taskValue ? "true" : "false" : taskValueInGrid.indexOf(taskValue) != -1 ? "true" : "false");
        taskCostEvalString = taskCostValue == "" ? "true" : parseFloat(taskCostValueInGrid) + taskCostEqualContain + parseFloat(taskCostValue);
        evalString = andOr == "&&" || (taskValue != "" && taskCostValue != "") ? taskEvalString + andOr + taskCostEvalString : taskValue == "" ? taskCostEvalString : taskEvalString;
        evalResult = eval(evalString);

        return evalResult;
    }

    function SetExecutePlanAllPhaseHead() {

        var dateNowString = (new Date()).formatDate(); //当前日期

        var allRefCost = 0; //所有阶段参考总工时
        var allPlanCost = 0; //所有阶段计划总工时
        var allPlanDay = 0; //所有阶段计划总工期
        var allPlanStart; //所有阶段计划开始
        var allPlanEnd; //所有阶段计划结束
        var allPlanAverage = 0; //所有阶段计划平均

        var allTotalPercent = 0; //所有阶段总百分比
        var allCompletePercent = 0; //所有阶段完成百分比
        var allActualCost = 0; //所有阶段实际总工时
        var allActualDay = 0; //所有阶段实际总工期
        var allActualStart; //所有阶段实际开始
        var allActualEnd; //所有阶段实际结束
        var allActualAverage = 0; //所有阶段实际平均
        var allProgressDeviation = 0; //所有阶段进度偏差

        $ExecutePlanTables.each(function() {

            //阶段参考总工时
            var phaseRefCost = GetExecutePlanPhaseRefCost(this);
            allRefCost = allRefCost + phaseRefCost;

            //阶段计划总工时
            var phasePlanCost = GetExecutePlanPhasePlanCost(this);
            allPlanCost = allPlanCost + phasePlanCost;

            //阶段计划开始
            var phasePlanStart = GetExecutePlanPhasePlanStart(this);

            if (allPlanStart == undefined) {
                allPlanStart = phasePlanStart;
            }
            else {
                if (allPlanStart > phasePlanStart && phasePlanStart != "") {
                    allPlanStart = phasePlanStart;
                }
            }

            //阶段计划结束
            var phasePlanEnd = GetExecutePlanPhasePlanEnd(this);

            if (allPlanEnd == undefined) {
                allPlanEnd = phasePlanEnd;
            }
            else {
                if (allPlanEnd < phasePlanEnd) {
                    allPlanEnd = phasePlanEnd;
                }
            }

            //阶段计划总工期
            var phasePlanDay = DateSpan(phasePlanStart, phasePlanEnd);
            var span = $(this).parent().parent().prev().find("span[id='PhasePlanDay']");
            $(span).text(phasePlanDay);

            //阶段计划平均
            var phasePlanAverage = 0;
            var taskNum = $(this).find("tr:not(.GVHeader):visible").size();
            if (taskNum > 0) {
                phasePlanAverage = parseFloat(phasePlanCost / taskNum);
            }
            var span = $(this).parent().parent().prev().find("span[id='PhasePlanAverage']");
            $(span).text(parseFloat(phasePlanAverage).toFixed(2) == 0.00 ? "" : parseFloat(phasePlanAverage).toFixed(2));

            //阶段完成百分比
            var phaseCompletePercent = 0;
            if (phasePlanCost > 0) {
                var phaseTotalPercent = GetExecutePlanPhaseTotalPercent(this);
                allTotalPercent = allTotalPercent + phaseTotalPercent;
                var span = $(this).parent().parent().prev().find("span[id='PhaseCompletePercent']");
                phaseCompletePercent = parseFloat(phaseTotalPercent / phasePlanCost / 100);
                $(span).text((phaseCompletePercent * 100).toFixed(1) + "%");
            }
            else {
                var span = $(this).parent().parent().prev().find("span[id='PhaseCompletePercent']");
                $(span).text("");
            }

            //阶段实际总工时
            var phaseActualCost = GetExecutePlanPhaseActualCost(this);
            allActualCost = allActualCost + phaseActualCost;

            //阶段实际开始
            var phaseActualStart = GetExecutePlanPhaseActualStart(this);

            if (allActualStart == undefined) {
                allActualStart = phaseActualStart;
            }
            else {
                if (allActualStart > phaseActualStart && phaseActualStart != "") {
                    allActualStart = phaseActualStart;
                }
            }

            //阶段实际结束
            var phaseActualEnd = GetExecutePlanPhaseActualEnd(this);

            if (allActualEnd == undefined) {
                allActualEnd = phaseActualEnd;
            }
            else {
                if (phaseActualEnd == "" || (allActualEnd != "" && phaseActualEnd > allActualEnd)) {
                    allActualEnd = phaseActualEnd;
                }
            }

            //阶段实际总工期         
            var phaseActualDay = DateSpan(phaseActualStart, phaseActualEnd);

            var span = $(this).parent().parent().prev().find("span[id='PhaseActualDay']");
            $(span).text(phaseActualDay);

            //阶段实际平均
            var phaseActualAverage = 0;
            var taskNum = $(this).find("tr:not(.GVHeader):visible").size();
            if (taskNum > 0) {
                phaseActualAverage = parseFloat(phaseActualCost / taskNum);
            }
            var span = $(this).parent().parent().prev().find("span[id='PhaseActualAverage']");
            $(span).text(parseFloat(phaseActualAverage).toFixed(2) == 0.00 ? "" : parseFloat(phaseActualAverage).toFixed(2));

            //阶段进度偏差
            //计算每个任务进度偏差
            CalculateExecutePlanEveryTaskProgressDeviation(this);

            //阶段进度偏差整个阶段看成一个大任务           
            var phaseProgressDeviation = GetProgressDeviation(phaseCompletePercent, phasePlanCost, phasePlanEnd, phasePlanStart, dateNowString, phaseActualEnd);
            var span = $(this).parent().parent().prev().find("span[id='PhaseProgressDeviation']");
            $(span).text((taskNum == 0 && phaseProgressDeviation.toFixed(1) == 0.0) ? "" : phaseProgressDeviation.toFixed(1));
        });

        var totalTaskNum = $("table[id*='ExecutePlanByPhase']").find("tr:not(.GVHeader):visible").size();
        if (totalTaskNum > 0) {
            allPlanAverage = allPlanCost / totalTaskNum;
            allActualAverage = allActualCost / totalTaskNum;
        }

        //计划
        $("span[id='ExecutePlanAllPhaseRefCost']").text((totalTaskNum == 0 && allRefCost == 0) ? "" : allRefCost);
        $("span[id='ExecutePlanAllPhasePlanCost']").text((totalTaskNum == 0 && allPlanCost == 0) ? "" : allPlanCost);
        $("span[id='ExecutePlanAllPhasePlanStart']").text(allPlanStart);
        $("span[id='ExecutePlanAllPhasePlanEnd']").text(allPlanEnd);
        $("span[id='ExecutePlanAllPhasePlanAverage']").text((totalTaskNum == 0 && parseFloat(allPlanAverage).toFixed(2) == 0.00) ? "" : parseFloat(allPlanAverage).toFixed(2));


        //实际
        if (allPlanCost > 0) {
            allCompletePercent = parseFloat(allTotalPercent / allPlanCost / 100);
            $("span[id='ExecutePlanAllPhaseCompletePercent']").text((allCompletePercent * 100).toFixed(1) + "%");
        }
        else {
            $("span[id='ExecutePlanAllPhaseCompletePercent']").text("");
        }

        $("span[id='ExecutePlanAllPhaseActualCost']").text((totalTaskNum == 0 && allActualCost == 0) ? "" : allActualCost);
        $("span[id='ExecutePlanAllPhaseActualStart']").text(allActualStart);
        $("span[id='ExecutePlanAllPhaseActualEnd']").text(allActualEnd);
        $("span[id='ExecutePlanAllPhaseActualAverage']").text((totalTaskNum == 0 && parseFloat(allActualAverage).toFixed(2) == 0.00) ? "" : parseFloat(allActualAverage).toFixed(2));

        //项目进度偏差整个项目看成一个大任务
        allProgressDeviation = GetProgressDeviation(allCompletePercent, allPlanCost, allPlanEnd, allPlanStart, dateNowString, allActualEnd);
        $("span[id='ExecutePlanAllPhaseProgressDeviation']").text((totalTaskNum == 0 && allProgressDeviation.toFixed(1) == 0.0) ? "" : allProgressDeviation.toFixed(1));
    }

    //取得阶段完成百分比
    function GetExecutePlanPhaseTotalPercent(table) {

        var phaseTotalPercent = 0;

        var trs = $(table).find("tr:not(.GVHeader):visible");
        trs.each(function() {
            phaseTotalPercent = phaseTotalPercent + $(this).find("div[id='divPlanCost']").text() * $(this).find("div[id='divCompletePercent']").text()
        });

        return phaseTotalPercent;
    }

    function DateSpan(dateStart, dateEnd) {

        var dateSpan = 0;
        if (dateStart == undefined || dateEnd == undefined || dateStart == "" || dateEnd == "") {
            return "";
        }
        var s_date = dateStart.split('-');
        var e_date = dateEnd.split('-');
        var s = new Date(s_date[0], s_date[1], s_date[2]);
        var e = new Date(e_date[0], e_date[1], e_date[2]);
        dateSpan = e >= s ? (parseInt(Math.abs(e - s) / 1000 / 60 / 60 / 24) + 1) : -(parseInt(Math.abs(e - s) / 1000 / 60 / 60 / 24) + 1);
        return dateSpan;
    }

    Date.prototype.formatDate = function() {
        var s = '', d = this.getMonth() + 1;
        s += this.getFullYear();
        s += '-' + (d < 10 ? '0' + d : d);
        d = this.getDate();
        s += '-' + (d < 10 ? '0' + d : d);
        return s;
    }

    //取得阶段中每个任务的进度偏差 
    function CalculateExecutePlanEveryTaskProgressDeviation(table) {

        var dateNowString = (new Date()).formatDate(); //当前日期
        var trPlanStart = "";
        var trPlanEnd = "";
        var trActualStart = "";
        var trActualEnd = "";
        var trPlanCost = 0;
        var trCompletePercent = 0;
        var trSV = 0;

        var datatr = $(table).find("tr:not(.GVHeader):visible");
        datatr.each(function() {

            //完成百分比
            trCompletePercent = parseFloat($(this).find("div[id='divCompletePercent']").text()) / 100;

            //计划开始
            trPlanStart = $(this).find("div[id='divPlanStart']").text();

            //计划结束
            trPlanEnd = $(this).find("div[id='divPlanEnd']").text();

            //计划工时
            trPlanCost = parseInt($(this).find("div[id='divPlanCost']").text());

            //实际开始
            trActualStart = $(this).find("div[id='divActualStart']").text();

            //实际结束
            trActualEnd = $(this).find("div[id='divActualEnd']").text();

            //实际工时
            trActualCost = parseInt($(this).find("div[id='divActualCost']").text());

            //未开始||已取消||已关闭任务
            if ($.trim(trActualStart) == "" || $.trim(trActualCost + "") == "" || ($(this).attr("taskstatus") == "1") || ($(this).attr("taskstatus") == "4") || ($(this).attr("taskstatus") == "5")) {
                trSV = 0;
            }
            else {
                trSV = GetProgressDeviation(trCompletePercent, trPlanCost, trPlanEnd, trPlanStart, dateNowString, trActualEnd);
            }
            $(this).find("div[id='divProgressDeviation']").attr("title", trSV.toFixed(1));
            $(this).find("div[id='divProgressDeviation']").text(trSV.toFixed(1));
        });
    }

    //取得进度偏差
    function GetProgressDeviation(trCompletePercent, trPlanCost, trPlanEnd, trPlanStart, dateNowString, trActualEnd) {

        var trPlanDay = 0;
        var trPlanStartToNow = 0;
        var trPlanEndToNow = 0;

        var datePlanEnd;
        var datePlanStart;
        var dateNow;

        var trEV = 0;
        var trPV = 0;
        var trSV = 0;

        if (trPlanStart != "" && trPlanEnd != "") {
            trPlanDay = DateSpan(trPlanStart, trPlanEnd);
            trPlanStartToNow = DateSpan(trPlanStart, dateNowString);
        }

        if (trPlanDay > 0) {

            //某个任务的进度偏差要两种：分为未完成任务和已完成任务
            //1.未完成任务
            if (trCompletePercent < 1) {

                //1.1 当该任务未完成且目前的时间<=计划完成日时

                //已完工作量的预算工时(EV)
                trEV = parseFloat(trPlanCost * trCompletePercent);

                //计划工作量的预算工时(PV)
                trPV = (trPlanStartToNow / trPlanDay) >= 1 ? trPlanCost : parseFloat(trPlanCost * (trPlanStartToNow / trPlanDay));

                //进度偏差(SV)
                trSV = trEV - trPV;

                //1.2 当该任务未完成且目前时间超出了计划完成日时对上面的SV修正   
                datePlanEnd = new Date(Date.parse(trPlanEnd.replace(/-/g, "/")));
                datePlanStart = new Date(Date.parse(trPlanStart.replace(/-/g, "/")));
                dateNow = new Date(Date.parse(dateNowString.replace(/-/g, "/")));

                if (dateNow > datePlanEnd) {
                    trPlanEndToNow = DateSpan(dateNowString, trPlanEnd) > 0 ? DateSpan(dateNowString, trPlanEnd) - 1 : DateSpan(dateNowString, trPlanEnd) + 1;
                    trSV = trSV + trPlanEndToNow * 8;
                }
            }
            //2.已完成任务
            else {
                trPlanEndToActualEnd = DateSpan(trActualEnd, trPlanEnd) > 0 ? DateSpan(trActualEnd, trPlanEnd) - 1 : DateSpan(trActualEnd, trPlanEnd) + 1;
                trSV = (trPlanEndToActualEnd) * 8;
            }
        }
        return trSV;
    }

    //取得阶段参考工时
    function GetExecutePlanPhaseRefCost(table) {

        var phaseRefCost = 0;

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divRefCost']");
        divs.each(function() {
            phaseRefCost = phaseRefCost + parseInt($(this).text());
        });

        var span = $(table).parent().parent().prev().find("span[id='PhaseRefCost']");
        $(span).text(phaseRefCost == 0 ? "" : phaseRefCost);

        return phaseRefCost;
    }

    //取得阶段计划工时
    function GetExecutePlanPhasePlanCost(table) {

        var phasePlanCost = 0;

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divPlanCost']");
        divs.each(function() {
            phasePlanCost = phasePlanCost + parseInt($(this).text());
        });

        var span = $(table).parent().parent().prev().find("span[id='PhasePlanCost']");
        $(span).text(phasePlanCost == 0 ? "" : phasePlanCost);

        return phasePlanCost;
    }

    //取得阶段计划开始
    function GetExecutePlanPhasePlanStart(table) {

        var phasePlanStart = "";

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divPlanStart']");
        divs.each(function(index, div) {
            if (index == 0) {
                phasePlanStart = $(this).text();
            }
            else {
                if ($(this).text() < phasePlanStart) {
                    phasePlanStart = $(this).text();
                }
            }
        });

        var span = $(table).parent().parent().prev().find("span[id='PhasePlanStart']");
        $(span).text(phasePlanStart);

        return phasePlanStart;
    }

    //取得阶段计划结束
    function GetExecutePlanPhasePlanEnd(table) {

        var phasePlanEnd = "";

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divPlanEnd']");
        divs.each(function(index, div) {
            if (index == 0) {
                phasePlanEnd = $(this).text();
            }
            else {
                if ($(this).text() > phasePlanEnd) {
                    phasePlanEnd = $(this).text();
                }
            }
        });

        var span = $(table).parent().parent().prev().find("span[id='PhasePlanEnd']");
        $(span).text(phasePlanEnd);

        return phasePlanEnd;
    }

    //取得阶段实际工时
    function GetExecutePlanPhaseActualCost(table) {

        var phaseActualCost = 0;

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divActualCost']");
        divs.each(function() {
            phaseActualCost = phaseActualCost + parseInt($(this).text());
        });

        var span = $(table).parent().parent().prev().find("span[id='PhaseActualCost']");
        $(span).text(phaseActualCost == 0 ? "" : phaseActualCost);

        return phaseActualCost;
    }

    //取得阶段实际开始
    function GetExecutePlanPhaseActualStart(table) {

        var phaseActualStart = "";

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divActualStart']");
        divs.each(function(index, div) {
            if (index == 0) {
                phaseActualStart = $(this).text();
            }
            else {
                if (phaseActualStart == "" || ($(this).text() != "" && $(this).text() < phaseActualStart)) {
                    phaseActualStart = $(this).text();
                }
            }
        });

        var span = $(table).parent().parent().prev().find("span[id='PhaseActualStart']");
        $(span).text(phaseActualStart);

        return phaseActualStart;
    }

    //取得阶段实际结束
    function GetExecutePlanPhaseActualEnd(table) {

        var phaseActualEnd = "";

        var divs = $(table).find("tr:not(.GVHeader):visible").find("div[id='divActualEnd']");
        divs.each(function(index, div) {
            if (index == 0) {
                phaseActualEnd = $(this).text();
            }
            else {
                if ($(this).text() == "" || (phaseActualEnd != "" && $(this).text() > phaseActualEnd)) {
                    phaseActualEnd = $(this).text();
                }
            }
        });

        var span = $(table).parent().parent().prev().find("span[id='PhaseActualEnd']");
        $(span).text(phaseActualEnd);

        return phaseActualEnd;
    }  
    
</script>

<div id="divExecutePlan" class="DivPlan">
    <div id="divExecutePlanHead" class="DivPlanHead">
        <div style="float: left">
            <input id="ButtonShowExecutePlanTaskAll" type="button" value="所有" class="ButtonCurrent" />
            <input id="ButtonShowExecutePlanTaskAssignToMe" type="button" value="指派给我" class="ButtonTransparent" />
            <select id="DropDownListFilter" style="width: auto">
                <option value=""></option>
                <option value="1">未开始</option>
                <option value="2">进行中</option>
                <option value="3">已完成</option>
                <option value="4">已关闭</option>
                <option value="5">已取消</option>
                <option value="6">已暂缓</option>
                <option value="7">由我创建</option>
                <option value="8">由他人创建</option>
            </select>
            <input id="ButtonShowHideSearch" type="button" value="自定义搜索" class="ButtonTransparent" />
        </div>
        <div id="divSearch" style="float: left; display: none">
            <select id="DropDownListTask" style="width: auto" class="DropDownList">
                <option value="1">任务类型</option>
                <option value="2">任务名称</option>
                <option value="3">资源</option>
            </select>
            <select id="DropDownListTaskEqualContain" style="width: auto" class="DropDownList">
                <option value="1">等于</option>
                <option value="2">包含</option>
            </select>
            <input id="inputTaskValue" type="text" style="width: 100px" maxlength="300" />
            <select id="DropDownListAndOr" style="width: auto" class="DropDownList">
                <option value="&&">并且</option>
                <option value="||">或者</option>
            </select>
            <select id="DropDownListTaskCost" style="width: auto" class="DropDownList">
                <option value="1">实际工时</option>
                <option value="2">计划工时</option>
            </select>
            <select id="DropDownListTaskCostEqualContain" style="width: auto" class="DropDownList">
                <option value="==">等于</option>
                <option value=">">大于</option>
                <option value="<">小于</option>
                <option value="&gt;=">大于等于</option>
                <option value="<=">小于等于</option>
            </select>
            <input id="inputTaskCostValue" type="text" style="width: 100px" maxlength="50" />
            <input id="ButtonSearch" type="button" value="搜索" class="ButtonTransparent" />
        </div>
    </div>
    <div id="divExecutePlanAllPhase" class="DivAllPhase">
        <div id="divAllPhaseHead" class="DivAllPhaseHead" style="height: 60px">
            <ul>
                <li style="width: 17px;">
                    <img id="imgExceutePlanAllPhaseShow" alt="Show" src="../../SysFrame/images/hide.gif"
                        style="cursor: pointer" />
                    <img id="imgExceutePlanAllPhaseHide" alt="Hide" src="../../SysFrame/images/appear.gif"
                        style="cursor: pointer" />
                </li>
                <li style="width: 110px"><span>总览</span> </li>
                <li style="width: 130px"><span>参考总工时：</span><span id="ExecutePlanAllPhaseRefCost"></span></li>
                <li style="width: 110px"><span>计划总工时：</span><span id="ExecutePlanAllPhasePlanCost"></span>
                </li>
                <li style="width: 100px; display: none"><span>计划总工期：</span> <span id="ExecutePlanAllPhasePlanDay">
                </span></li>
                <li style="width: 150px"><span>计划开始：</span><span id="ExecutePlanAllPhasePlanStart"></span>
                </li>
                <li style="width: 150px"><span>计划结束：</span> <span id="ExecutePlanAllPhasePlanEnd"></span>
                </li>
                <li style="width: 120px"><span>计划平均：</span> <span id="ExecutePlanAllPhasePlanAverage">
                </span></li>
            </ul>
            <ul>
                <li style="width: 17px;"></li>
                <li style="width: 110px;"><span></span></li>
                <li style="width: 130px"><span>完成百分比：</span><span id="ExecutePlanAllPhaseCompletePercent"></span></li>
                <li style="width: 110px"><span>实际总工时：</span><span id="ExecutePlanAllPhaseActualCost"></span>
                </li>
                <li style="width: 100px; display: none"><span>实际总工期：</span> <span id="ExecutePlanAllPhaseActualDay">
                </span></li>
                <li style="width: 150px"><span>实际开始：</span><span id="ExecutePlanAllPhaseActualStart"></span>
                </li>
                <li style="width: 150px"><span>实际结束：</span> <span id="ExecutePlanAllPhaseActualEnd">
                </span></li>
                <li style="width: 120px"><span>实际平均：</span> <span id="ExecutePlanAllPhaseActualAverage">
                </span></li>
                <li style="width: 120px"><span>进度偏差：</span> <span id="ExecutePlanAllPhaseProgressDeviation">
                </span></li>
            </ul>
        </div>
        <div id="divAllPhaseBody" class="DivAllPhaseBody">
            <div id="divDesign">
                <uc1:ExecutePlanByPhase ID="ExecutePlanByPhaseDesign" runat="server" />
            </div>
            <div style="height: 12px">
            </div>
            <div id="divDevelopment">
                <uc1:ExecutePlanByPhase ID="ExecutePlanByPhaseDevelopment" runat="server" />
            </div>
            <div style="height: 12px">
            </div>
            <div id="divTest">
                <uc1:ExecutePlanByPhase ID="ExecutePlanByPhaseTest" runat="server" />
            </div>
            <div style="height: 12px">
            </div>
            <div id="divRelease">
                <uc1:ExecutePlanByPhase ID="ExecutePlanByPhaseRelease" runat="server" />
            </div>
            <div style="height: 12px">
            </div>
            <div id="divSupport">
                <uc1:ExecutePlanByPhase ID="ExecutePlanByPhaseSupport" runat="server" />
            </div>
        </div>
    </div>
</div>
