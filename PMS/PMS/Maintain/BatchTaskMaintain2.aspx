<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchTaskMaintain2.aspx.cs"
    Inherits="PMS.PMS.Maintain.BatchTaskMaintain2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BatchTaskMaintain2</title>
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery/jquery.url-1.8.1.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery/jquery.json-2.4.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery/jquery.easyui.min-1.3.3.js" type="text/javascript"></script>

    <link href="../../Style/easyui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function() {
            if ("<%=Stop %>" == "False")
                return;
            //选择一个节点时，第一次触发oncheck事件             
            var firstCheck = true;
            //用户选择的节点FunctionId
            var selectFunctionId;
            var pmsid = "<%=pmsId %>";

            $('#treegridTasks').treegrid({
                url: '../Handler/BatchTaskMaintainHandler.ashx?' + $.param({ Pmsid: pmsid }),
                idField: 'Wbs',
                treeField: 'TaskName',
                frozenColumns: [[
                                 { field: 'Select', checkbox: true, width: 50, halign: 'center', align: 'center' }
                             ]],
                columns: [[
                { title: 'Task Name', field: 'TaskName', width: 150, halign: 'center', align: 'left' },
                { field: 'Phase', title: 'Phase', width: 110, halign: 'left', align: 'left',
                    formatter: function(value, rowData, rowIndex) {
                        var phaseDesc;
                        switch (value) {
                            case "4":
                                phaseDesc = "Design";
                                break;
                            case "5":
                                phaseDesc = "Development";
                                break;
                            case "6":
                                phaseDesc = "Test";
                                break;
                            case "7":
                                phaseDesc = "Release";
                                break;
                            case "8":
                                phaseDesc = "Support";
                                break;
                            case "9":
                                phaseDesc = "PES";
                                break;
                            case "10":
                                phaseDesc = "上线实施阶段";
                                break;
                        }
                        return phaseDesc;
                    }
                },

                { field: 'Plancost', title: 'Plancost(H)', width: 80, halign: 'left', align: 'left' },
                { field: 'Planstartday', title: 'Planstartday', width: 90, halign: 'left', align: 'left',
                    formatter: function(value, rowData, rowIndex) {
                        var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10)).format("yyyy-MM-dd ");
                        return date;
                    }
                },
                { field: 'Planendday', title: 'Planendday', width: 90, halign: 'left', align: 'left',
                    formatter: function(value, rowData, rowIndex) {
                        var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10)).format("yyyy-MM-dd ");
                        return date;
                    }
                },
                { field: 'TaskComplexityDesc', title: 'Task Complexity', width: 50, halign: 'left', align: 'left' },
                { field: 'Resource', title: 'Resource', width: 100, halign: 'left', align: 'left' },
                { field: 'Role', title: 'Role', width: 50, halign: 'left', align: 'left' },
                { field: 'ProgramLanguageDesc', title: 'Program Language', width: 40, halign: 'left', align: 'left' },
                { field: 'TaskTypeDesc', title: 'Task Type', width: 115, halign: 'left', align: 'left' },
                { field: 'OperationTypeDesc', title: 'Operation Type', width: 120, halign: 'left', align: 'left' },
                { field: 'FunctionTypeDesc', title: 'Function Type', width: 90, halign: 'left', align: 'left' },
                { field: 'Result', title: 'Result', halign: 'left', align: 'left',
                    styler: function(value, row, index) {
                        if (value != null) {
                            return 'background-color:#ffee00;color:red;';
                        }
                    }
                }
                        ]],
                nowrap: true, //True 就会把数据显示在一行里。
                striped: true, //True 就把行条纹化。（即奇偶行使用不同背景色）
                singleSelect: false, //是否单选 
                selectOnCheck: true, //如果设置为true，单击一个复选框，将始终选择行
                checkOnSelect: true, //如果设置为true，选择一行,复选框将被选中
                animate: true,
                rownumbers: true, //行号

                onLoadSuccess: function(row, data) {
                    //                    if (data) {
                    //                        $.each(data.rows, function(index, item) {
                    //                            if (item.Select) {
                    //                                $('#treegridTasks').treegrid('select', item.Wbs);
                    //                            }
                    //                        });
                    //                    }
                },
                onLoadError: function(data) {
                    $.messager.alert("Error", data.responseText, "error");
                },

                onCheck: function(rowData) {
                    if (firstCheck) {
                        selectFunctionId = rowData.Wbs;
                        //１.先获取选中的Function的所有子节点，存放在全局变量数组中
                        arrayId = new Array();
                        GetChildrenNodeIds(selectFunctionId); //获取所有子节点
                        //２.将选中的标记置为false，以使循环选择行触发的选中事件，不再执行该段代码
                        firstCheck = false;
                        $.each(arrayId, function(index, id) {
                            $('#treegridTasks').treegrid('select', id);
                        });
                        //4.重置第一次选择标记为true,以便用户再次选择其他行
                        firstCheck = true;
                    }
                },
                onUncheck: function(rowData) {
                    var crrentFunctionId = rowData.Wbs;
                    var childrenNodes = $('#treegridTasks').treegrid('getChildren', crrentFunctionId);
                    unCheckedChildrenRecursion(childrenNodes);
                    //取消选择时要恢复选择的初始值，以防用户重新选择
                    //选择一个节点时，第一次触发oncheck事件
                    //取消一个子节点选择时，所有父节点取消选中，还没实现。

                    //                    debugger;
                    //                    if (rowData._parentId != "") {
                    //                        unCheckedParentRecursion(crrentFunctionId);
                    //                        var parentNodes = $('#treegridTasks').treegrid('getParent', rowData.Wbs);
                    //                        $('#treegridTasks').treegrid('unselect', parentNodes.Wbs);
                    //                    }
                    firstCheck = true;
                }
            });
        });
        function GetChildrenNodeIds(currentFunctionId) {
            var childrenNodes = $('#treegridTasks').treegrid('getChildren', currentFunctionId);
            if (childrenNodes != null) {
                var childrenNodes1;
                for (var i = 0; i < childrenNodes.length; i++) {
                    childrenNodes1 = $('#treegridTasks').treegrid('getChildren', childrenNodes[i].Wbs);
                    var id = new Array(childrenNodes[i].Wbs);
                    arrayId = arrayId.concat(id);
                    GetChildrenNodeIds(childrenNodes1);
                }
            }
        }
        function unCheckedChildrenRecursion(childrenNodes) {
            if (childrenNodes != null) {
                var childrenNodes1;
                for (var i = 0; i < childrenNodes.length; i++) {
                    childrenNodes1 = $('#treegridTasks').treegrid('getChildren', childrenNodes[i].Wbs);
                    $('#treegridTasks').treegrid('unselect', childrenNodes[i].Wbs);
                    unCheckedChildrenRecursion(childrenNodes1);
                }
                return null;
            } else {
                return null;
            }
        }
        function unCheckedParentRecursion(idWbs) {
            var parentNodes = $('#treegridTasks').treegrid('getParent', idWbs);
            $('#treegridTasks').treegrid('unselect', parentNodes.Wbs);
            return null;
        }



        //        success: function(result) {
        //                        if (result) {
        //                            for (var i = 0; i < selections.length; i++) {
        //                                var hasChildren = $('#treegridTasks').treegrid('getChildren', selections[i].Wbs);
        //                                if (hasChildren.length > 0)
        //                                    continue;                                
        //                                if (saveSerials.indexOf(selections[i].Serial) >= 0)
        //                                    $('#treegridTasks').treegrid('remove', selections[i].Wbs);
        //                            }
        //                        } else {
        //                            alert("Import failed!");
        //                        }



        function CheckDate(oSdpDetail) {
            var planStartDateTime = new Date(oSdpDetail.planStartDate).getTime();
            var planEndDateTime = new Date(oSdpDetail.planEndDate).getTime();
            var dueDateTime = new Date(oSdpDetail.dueDate).getTime();
            var resultMsg = ""; //返回信息

            if (dueDateTime < new Date("1900/1/1").getTime())
                resultMsg = "Dudate Invalid";
            if (oSdpDetail.type != oSdpDetail.service) {
                if (planStartDateTime > dueDateTime && oSdpDetail.phase != oSdpDetail.support) {
                    resultMsg = "The planned start date should be less than the CR due date!";
                }
                if (planEndDateTime > dueDateTime && oSdpDetail.phase != oSdpDetail.support) {
                    resultMsg = "The planned end date should be less than the CR due date!";
                }
            }
            if (planStartDateTime > planEndDateTime && oSdpDetail.phase != oSdpDetail.support) {
                resultMsg = "The planned end date should be more than the plan start date!";
            }
            var startDate = oSdpDetail.planStartDate;
            var endDate = oSdpDetail.planEndDate;
            if (startDate.setDate(startDate.getDate() + 2) < planEndDateTime) {
                resultMsg = "Task duration should be less than 3 days!";
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
                resultMsg = "Task period should not be cross week!";
            }
            return resultMsg;
        }

        //Jquery ajax 调用 WebService  CheckTaskNameAndResource 不重复
        function CheckTaskNameAndResource(selection) {

            var resultMsg = ""; //返回信息
            var pmsid = selection.Pmsid;
            var taskName = selection.TaskName;
            var phase = selection.Phase;
            var role = selection.Role;
            var resource = selection.Resource;

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
                    exist = obj.Exist;
                    taskStatus = obj.TaskStatus;
                    sdpDetailSerial = obj.serial;
                },
                error: function(xmlHttpRequest, msg) {
                    alert("Call WebService Error!");
                }
            });
            return resultMsg;
        }
        //数组是否包含元素
        function ArrayContainStr(array, str) {
            for (i = 0; i < array.length; i++) {
                if (array[i] == str)
                    return true;
            }
            return false;
        }
        //Check Resource 合法CheckResourceLegal
        function CheckResourceLegal(selection) {
            var resultMsg = ""; //返回信息
            var resource = selection.Resource;
            
            if (!ArrayContainStr(resourceList,resource))
                resultMsg = "This resource name is illegal!";
            return resultMsg;
        }

        //Check Resource 不能为多个
        function CheckResourceNumber(selection) {
            var resultMsg = ""; //返回信息
            var resource = selection.Resource;
            if (resource.indexOf(',') >= 0)
                resultMsg = "This task allows only one of the members!";
            return resultMsg;
        }
        //Check PES 上线实施阶段不能import
        function CheckPhase(selection) {
            var resultMsg = ""; //返回信息
            var phase = selection.Phase;
            if (phase == "9" || phase == "10")
                resultMsg = "The tasks of this phase will not be imported!";
            return resultMsg;
        }
        function CheckSelection(selection) {
            //Check返回信息
            var checkResultMsg = "";
            var oTmpSdpDetail = new Object();
            oTmpSdpDetail.service = "<%=Service %>";
            oTmpSdpDetail.support = "<%=Support %>";
            oTmpSdpDetail.type = "<%=ObjPmsHead.Type %>";
            oTmpSdpDetail.phase = selection.Phase;

            var planStartDate = new Date(parseInt(selection.Planstartday.replace("/Date(", "").replace(")/", ""), 10)).format("yyyy-MM-dd ");
            var planEndDate = new Date(parseInt(selection.Planendday.replace("/Date(", "").replace(")/", ""), 10)).format("yyyy-MM-dd ");
            var dueDate = "<%=ObjPmsHead.DueDate %>";

            planStartDate = planStartDate.replace(/\-/gi, "/");
            planEndDate = planEndDate.replace(/\-/gi, "/");
            dueDate = dueDate.replace(/\-/gi, "/");

            oTmpSdpDetail.planStartDate = new Date(planStartDate);
            oTmpSdpDetail.planEndDate = new Date(planEndDate);
            oTmpSdpDetail.dueDate = new Date(dueDate);

            checkResultMsg = CheckPhase(selection);
            if (checkResultMsg != "")
                return checkResultMsg;

            checkResultMsg = CheckResourceLegal(selection);
            if (checkResultMsg != "")
                return checkResultMsg;

            checkResultMsg = CheckResourceNumber(selection);
            if (checkResultMsg != "")
                return checkResultMsg;

            checkResultMsg = CheckDate(oTmpSdpDetail);
            if (checkResultMsg != "")
                return checkResultMsg;
            //先不考慮Review Meeting
            //if (selection.TaskTypeDesc != "Review Meeting") { }            
            checkResultMsg = CheckTaskNameAndResource(selection);
            if (checkResultMsg != "")
                return checkResultMsg;

            return checkResultMsg;
        }

        //删除这个节点的父节点（当父节点没有子节点了）
        function RemoveParent(parent) {
            var thisParent = $('#treegridTasks').treegrid('getParent', parent.Wbs);
            if (parent.children.length == 0) {
                $('#treegridTasks').treegrid('remove', parent.Wbs);
                //更新临时表里的子节点全部删除的父节点的Falg
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../WebService/AddNewTask.asmx/UpdateParentFlag",
                    data: "{serial:'" + parent.Serial + "'}",
                    dataType: 'json',
                    success: function(result) {
                        if (result)
                            thisParent = thisParent; //没意义（占位）
                        else
                            alert("Update parentNode flag failed!");
                    },
                    error: function(xmlHttpRequest, msg) {
                        alert("Call UpdateParentFlag WebService Error !");
                    }
                });
                if (thisParent != null)
                    RemoveParent(thisParent);
            }
        }
        function GetResourceList() {
            var pmsid = "<%=pmsId %>";
            $.ajax({
                type: "POST",
                async: false,
                contentType: "application/json",
                url: "../WebService/AddNewTask.asmx/GetResourceList",
                data: "{pmsid:'" + pmsid + "'}",
                dataType: 'json',
                success: function(result) {
                    var resultList = result.d; 
                    resultList = resultList.substr(0, resultList.length - 1);
                    resourceList = resultList.split(',');
                },
                error: function(xmlHttpRequest, msg) {
                    alert("Call GetResourceList WebService Error !");
                }
            });

        }
        //导入到表
        var exist;
        var taskStatus;
        var sdpDetailSerial;
        //获取全集变量resourceList  用来判断Resource 是否合法
        var resourceList = "";
        function treegridTasksImport() {

            if (resourceList == "")
                GetResourceList();
            var sdpDetailSerials = "";
            var canSave = false;
            var canEdit = false;
            var canImport = true;
            var checkResultMsg = "";
            var saveSerials = "";
            var editSerials = ""
            var selections = $('#treegridTasks').treegrid("getSelections");

            if (selections.length == 0) {
                $.messager.alert("Warning", "Please select to import items.", "warning");
                return false;
            }
            for (var i = 0; i < selections.length; i++) {
                var hasChildren = $('#treegridTasks').treegrid('getChildren', selections[i].Wbs);
                if (hasChildren.length > 0)
                    continue;
                var checkResultMsg = CheckSelection(selections[i]);


                //错误原因显示在Result栏位
                if (checkResultMsg != "") {
                    selections[i].Result = checkResultMsg;
                    $('#treegridTasks').treegrid('update', {
                        id: selections[i].Wbs
                    });
                    $('#treegridTasks').treegrid('autoSizeColumn', 'Result');
                    canSave = false;
                    canEdit = false; //有错误不能保存,更新。
                    canImport = false;
                }
                if (exist == "Y" && taskStatus != "1") {
                    selections[i].Result = "The task status is not yet to begin,can't update! ";
                    $('#treegridTasks').treegrid('update', {
                        id: selections[i].Wbs
                    });
                    $('#treegridTasks').treegrid('autoSizeColumn', 'Result');
                    canSave = false;
                    canEdit = false; //已存在但任务状态不是未开时不能保存,更新。
                    canImport = false;
                }
                if (exist == "N" && canImport) {
                    saveSerials = saveSerials + selections[i].Serial + ";";
                    canSave = true; //不存在能保存
                }
                if (exist == "Y" && taskStatus == "1" && canImport) {
                    sdpDetailSerials = sdpDetailSerials + sdpDetailSerial + ";";
                    editSerials = editSerials + selections[i].Serial + ";";
                    canEdit = true; //存在且任务状态为未开始，可以更新。
                }
            }
            if (canSave) {
                saveSerials = saveSerials.substr(0, saveSerials.length - 1);
                //Jquery ajax 调用 WebService
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../WebService/AddNewTask.asmx/ImportSdpDetailBySerial",
                    data: "{serials:'" + saveSerials + "'}",
                    dataType: 'json',
                    success: function(result) {
                        if (result) {
                            for (var i = 0; i < selections.length; i++) {
                                var hasChildren = $('#treegridTasks').treegrid('getChildren', selections[i].Wbs);
                                if (hasChildren.length > 0)
                                    continue;
                                if (saveSerials.indexOf(selections[i].Serial) >= 0) {
                                    var thisParent = $('#treegridTasks').treegrid('getParent', selections[i].Wbs);
                                    $('#treegridTasks').treegrid('remove', selections[i].Wbs);
                                    if (thisParent != null)
                                        RemoveParent(thisParent);
                                }
                            }
                        } else {
                            alert("Import failed!");
                        }
                    },
                    error: function(xmlHttpRequest, msg) {
                        alert("Call Save WebService Error !");
                    }
                });
            }
            if (canEdit) {
                selections = $('#treegridTasks').treegrid("getSelections");
                sdpDetailSerials = sdpDetailSerials.substr(0, sdpDetailSerials.length - 1);
                editSerials = editSerials.substr(0, editSerials.length - 1);
                //Jquery ajax 调用 WebService
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "../WebService/AddNewTask.asmx/UpdateSdpDetailBySerial",
                    data: "{serials:'" + editSerials + "',sdpDetailSerials:'" + sdpDetailSerials + "'}",
                    dataType: 'json',
                    success: function(result) {
                        if (result) {
                            for (var i = 0; i < selections.length; i++) {
                                var hasChildren = $('#treegridTasks').treegrid('getChildren', selections[i].Wbs);
                                if (hasChildren.length > 0)
                                    continue;
                                if (editSerials.indexOf(selections[i].Serial) >= 0) {
                                    var thisParent = $('#treegridTasks').treegrid('getParent', selections[i].Wbs);
                                    $('#treegridTasks').treegrid('remove', selections[i].Wbs);
                                    if (thisParent != null)
                                        RemoveParent(thisParent);
                                }
                            }
                        } else {
                            alert("Update failed!");
                        }
                    },
                    error: function(xmlHttpRequest, msg) {
                        alert("Call Edit WebService Error !");
                    }
                });
            }
        }       
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" EnablePartialRendering="true" runat="server"
        ScriptMode="Debug">
    </asp:ScriptManager>
    <div style="width: 1225px; margin: 10px auto; padding-left: 20px; padding-top: 10px;
        border: 1px dashed #CCCCCC;">
        <table id="treegridTasks" style="width: 1205px; height: 500px;">
        </table>
        <div style="margin-bottom: 20px; margin-top: 15px;">
            <%--<input type="button" value="AbelTest" onclick="test();" />--%>
            <asp:Label ID="Label3" runat="server" Text="" Width="565px"></asp:Label>
            <input type="button" value="Import" onclick="treegridTasksImport();" style="width: 80px;
                height: 25px; font-size: 15px" />
            <asp:Label ID="Label2" runat="server" Text="" Width="140px"></asp:Label>
            <input type="button" value="Exit" onclick="window.close();" style="width: 60px; height: 25px;
                font-size: 15px" />
        </div>
    </div>
    </form>
</body>
</html>
