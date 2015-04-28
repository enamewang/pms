

//Open new Page
function PopNewPage() {
    var strFeatures = "dialogWidth=445px;dialogHeight=235px;center=yes;help=no;status=no;resizable=yes;scroll=no"; //set the page style
    var strUrl = "ParameterMaintainEdit.aspx?ACTION=Add";
    var strR = showModalDialog(strUrl, '_blank', strFeatures);
    if (strR == "SAVE")
        document.location.href = "ParameterMaintain.aspx";
}
document.close();

//Confirm the button type
function getconfirm() {
    if (confirm("Are you sure to exit ?") == true) {
        window.close()
        return true;
    }
    else {
        event.returnValue = false;
        return false;
    } CompletePercent
}

//function confirmDeleteItem(gridView) {
//    var grd = document.getElementById(gridView);
//    if (grd == null || grd.rows.length == 3 || grd.rows.length == 2) {
//        alert("The last item can not be deleted!");
//        event.returnValue = false;
//        return false;
//    }
//    else {
//        if (confirm("Confirm to delete this item?") == true) {
//            return true;
//        }
//        else {
//            event.returnValue = false;
//            return false;
//        }
//    }
//}

function getconfirmClear() {
    if (!CheckFlexDataCreated('gridViewMMS')) {
        event.returnValue = false;
        return false;
    }

    if (confirm("Are you sure to clear ?") == true) {
        return true;
    }
    else {
        event.returnValue = false;
        return false;
    }
}

function CheckFlexDataCreated(gridView) {
    var grd = document.getElementById(gridView);

    if (grd != null && grd.rows.length > 1) {
        return true;
    }

    alert('Please click the create button first!');
    return false;
}

function getconfirmPublish() {
    if (confirm("Are you sure to publish the new version?The old version will be deleted!") == true) {
        return true;
    }
    else {
        event.returnValue = false;
        return false;
    }
}

function ShowDetail(div) {
    if (div.style["display"] == "none") {
        div.style["display"] = "block";
        document.getElementById("Image" + div.id + "Open").src = "../../PMS/images/appear.gif";
    }
    else {
        div.style["display"] = "none";
        document.getElementById("Image" + div.id + "Open").src = "../../PMS/images/hide.gif";
    }
}

function ShowDetailAllOrNone(object) {
    var imageString = document.getElementById("imageButtonExpand").src;
    var showOrHide = imageString.substring(imageString.lastIndexOf("/"), imageString.length)
    //DivDateDetail ImageDivDateDetailOpen
    //DivEmailDetail ImageDivEmailDetailOpen
    //debugger;
    //DivMMSDetail ImageDivMMSDetailOpen
    if (showOrHide.toLowerCase() == "/hide.gif") {
        var grd = document.getElementById("DivDesignDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivDesignDetail.style["display"] = "block";
                document.all.ImageDivDesignDetailOpen.src = "../../PMS/images/appear.gif";
            }
        }

        var grd = document.getElementById("DivDevelopmentDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivDevelopmentDetail.style["display"] = "block";
                document.all.ImageDivDevelopmentDetailOpen.src = "../../PMS/images/appear.gif";
            }
        }

        var grd = document.getElementById("DivTestDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivTestDetail.style["display"] = "block";
                document.all.ImageDivTestDetailOpen.src = "../../PMS/images/appear.gif";
            }
        }

        var grd = document.getElementById("DivReleaseSDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivReleaseSDetail.style["display"] = "block";
                document.all.ImageDivReleaseSDetailOpen.src = "../../PMS/images/appear.gif";
            }
        }

        var grd = document.getElementById("DivSupportDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivSupportDetail.style["display"] = "block";
                document.all.ImageDivSupportDetailOpen.src = "../../PMS/images/appear.gif";
            }
        }

        document.getElementById("imageButtonExpand").src = "../../PMS/images/appear.gif";
    }
    else if (showOrHide.toLowerCase() == "/appear.gif") {
        var grd = document.getElementById("DivDesignDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivDesignDetail.style["display"] = "none";
                document.all.ImageDivDesignDetailOpen.src = "../../PMS/images/hide.gif";
            }
        }

        var grd = document.getElementById("DivDevelopmentDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivDevelopmentDetail.style["display"] = "none";
                document.all.ImageDivDevelopmentDetailOpen.src = "../../PMS/images/hide.gif";
            }
        }

        var grd = document.getElementById("DivTestDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivTestDetail.style["display"] = "none";
                document.all.ImageDivTestDetailOpen.src = "../../PMS/images/hide.gif";
            }
        }

        var grd = document.getElementById("DivReleaseSDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivReleaseSDetail.style["display"] = "none";
                document.all.ImageDivReleaseSDetailOpen.src = "../../PMS/images/hide.gif";
            }
        }

        var grd = document.getElementById("DivSupportDetail");
        if (grd != null) {
            if (grd.Visble = true) {
                document.all.DivSupportDetail.style["display"] = "none";
                document.all.ImageDivSupportDetailOpen.src = "../../PMS/images/hide.gif";
            }
        }

        document.getElementById("imageButtonExpand").src = "../../PMS/images/hide.gif";
    }
}

function ShowDetailAll() {
    var grd = document.getElementById(DivDesignDetail);
    if (grd != null) {
        document.all.DivDesignDetail.style["display"] = "block";
        document.all.ImageDivDesignDetailOpen.src = "../../PMS/images/appear.gif";
    }

    var grd = document.getElementById(DivDevelopmentDetail);
    if (grd != null) {
        document.all.DivDevelopmentDetail.style["display"] = "block";
        document.all.ImageDivDevelopmentDetailOpen.src = "../../PMS/images/appear.gif";
    }

    var grd = document.getElementById(DivTestDetail);
    if (grd != null) {
        document.all.DivTestDetail.style["display"] = "block";
        document.all.ImageDivTestDetailOpen.src = "../../PMS/images/appear.gif";
    }

    var grd = document.getElementById(DivReleaseSDetail);
    if (grd != null) {
        document.all.DivReleaseSDetail.style["display"] = "block";
        document.all.ImageDivReleaseSDetailOpen.src = "../../PMS/images/appear.gif";
    }

    var grd = document.getElementById(DivSupportDetail);
    if (grd != null) {
        document.all.DivSupportDetail.style["display"] = "block";
        document.all.ImageDivSupportDetailOpen.src = "../../PMS/images/appear.gif";
    }


    document.getElementById("imageButtonExpand").src = "../../PMS/images/appear.gif";
}


function confirmDelete(gridView) {
    if (confirm("Confirm to delete?") == true) {
        //window.close()
        return true;
    }
    else {
        event.returnValue = false;
        return false;
    }
}

function CheckGridViewSelected(gridView) {
    var intCount = 0;
    var intSum = 0;
    var grd = document.getElementById(gridView);

    if (grd == null || grd.rows.length == 1) {
        alert('There is no data to delete!');
        return false;
    }

    if (grd != null && grd.rows.length > 1) {
        //debugger;

        for (intCount = 0; intCount < document.all.tags("input").length; intCount++) {
            if (document.all.tags("input")[intCount].type == "checkbox") {
                if (document.all.tags("input")[intCount].id.toUpperCase() != "ChkAll".toUpperCase() && document.all.tags("input")[intCount].id.toUpperCase() != "chkIS_SM".toUpperCase() && document.all.tags("input")[intCount].disabled != true) {
                    if (document.all.tags("input")[intCount].checked)
                        intSum++;
                }
            }
        }

        if (intSum == 0) {
            alert('Please select record(s) to delete!');
            return false;
        }
    }

    if (confirm("Confirm to delete " + intSum + " record(s)?") == true) {
        return true;
    }
    else {
        return false;
    }
}

function KeyPressForbidden(event) {
    event.returnValue = false;
}

function CheckHeadInfoSave(nowDate, IsRelease) {
    var textBoxCRID = document.getElementById("textBoxCRID");
    var textBoxCRName = document.getElementById("textBoxCRName");
    var textBoxSysName = document.getElementById("textBoxSysName");
    var textBoxPM = document.getElementById("textBoxPM");
    //   var textBoxSite = document.getElementById("textBoxSite"); 
    var DropDownListSite = document.getElementById("DropDownListSite");
    var textBoxVersion = document.getElementById("textBoxVersion");
    var calendarDueDate = document.getElementById("calendarDueDate");
    var dropDownListType = document.getElementById("dropDownListType");

    var textBoxImpactSite = document.getElementById("textBoxImpactSite");
    var textBoxSourceOldVersion = document.getElementById("textBoxSourceOldVersion");
    var textBoxSourceNewVersion = document.getElementById("textBoxSourceNewVersion");
    if (textBoxCRID.value.trim() == "") {
        alert("Please input CR ID");
        textBoxCRID.focus();
        return false;
    }
    if (textBoxCRName.value.trim() == "") {
        alert("Please input CR name.");
        textBoxCRName.focus();
        return false;
    }

    if (textBoxSysName.value.trim() == "") {
        alert("Please input system name.");
        textBoxSysName.focus();
        return false;
    }

    if (textBoxPM.value.trim() == "") {
        alert("Please input CR PM name.");
        textBoxPM.focus();
        return false;
    }

    //   if( textBoxSite.value.trim() == "")
    //   {
    //      alert("Please input site.");
    //      textBoxSite.focus();
    //       return false;
    //   }
    if (DropDownListSite.value.trim() == "") {
        alert("Please select site.");
        DropDownListSite.focus();
        return false;
    }

    if (textBoxVersion.value.trim() == "") {
        alert("Please input version.");
        textBoxVersion.focus();
        return false;
    }

    if (calendarDueDate.disabled == false) {
        if (calendarDueDate.value.trim() == "") {
            alert("Please input due date.");
            calendarDueDate.focus();

            return false;
        }
        else {
            if (!CheckDate(nowDate, calendarDueDate.value.trim())) {
                alert("Due date should not be less than today.");
                calendarDueDate.value = "";
                calendarDueDate.focus();
                return false;
            }
        }
    }
    else {
        if (calendarDueDate.value.trim() == "" && IsRelease == 1) {
            alert("Please find PM to input due date.");
            return false;
        }
    }

    if (dropDownListType.value.trim() == "") {
        alert("Please select type.");
        dropDownListType.focus();
        return false;
    }

    //added by aic0/hansel on 20101215
    var DropDownListStatus = document.getElementById("DropDownListStatus");
    if (DropDownListStatus.value.trim() == "") {
        alert("Please select CR Status.");
        DropDownListStatus.focus();
        return false;
    }
    //end added

    if (textBoxImpactSite.value.trim() == "") {
        alert("Please input impact site.");
        textBoxImpactSite.focus();
        return false;
    }

    if (textBoxSourceOldVersion.value.trim() == "") {
        alert("Please input suorce old version.");
        textBoxSourceOldVersion.focus();
        return false;
    }
    else if (!CheckVersion("textBoxSourceOldVersion"))//added by Albee 20100721
    {
        return false;
    }


    if (textBoxSourceNewVersion.value.trim() == "") {
        alert("Please input  suorce new version.");
        textBoxSourceNewVersion.focus();
        return false;
    }
    else if (!CheckVersion("textBoxSourceNewVersion"))//added by Albee 20100721
    {
        return false;
    }

    //added by Albee 20100721
    if (IsRelease == 1) {
        var percent = document.getElementById("labelAllPhasePercentValue").innerText;
        if (percent <= "0.0%") {
            alert("The task has not finished!");
            return false;
        }

        var Designpercent = document.getElementById("labelDesignCompletedPercentValue").innerText;
        if (Designpercent != "100.0%") {
            alert("The task of design has not finished!");
            return false;
        }

        var Developmentpercent = document.getElementById("labelDevelopmentCompletedPercentValue").innerText;
        if (Developmentpercent != "100.0%") {
            alert("The task of development has not finished!");
            return false;
        }

        var Testpercent = document.getElementById("labelTestCompletedPercentValue").innerText;
        if (Testpercent != "100.0%") {
            alert("The task of Test has not finished!");
            return false;
        }

        var Releasepercent = document.getElementById("labelReleaseCompletedPercentValue").innerText;
        if (Releasepercent != "100.0%") {
            alert("The task of release has not finished!");
            return false;
        }

        return confirm('Do you really want to release?');
    }

    return true;
}


function CompletePercent(actualCost, planCost, percent) {
    var actualCost = document.getElementById(actualCost);
    var planCost = document.getElementById(planCost);
    var percent = document.getElementById(percent);

    if (planCost.value == "") {
        alert("planCost is empty,please input plancost first");
        planCost.focus();
        return;
    }
    if (parseInt(planCost.value) < parseInt(actualCost.value) || actualCost.value == "") {
        percent.value = "";
        return;
    }
    else {
        percent.value = 100 * parseFloat(actualCost.value) / parseFloat(planCost.value);

    }


}

//function CheckPercent(actualCostID, planCostID, percentID, planStartID, planEndID, actualStartID, actualEndID, RemarkID) {
//    var actualCost = document.getElementById(actualCostID);
//    var planCost = document.getElementById(planCostID);
//    var percent = document.getElementById(percentID);
//    var planStart = document.getElementById(planStartID);
//    var planEnd = document.getElementById(planEndID);
//    var actualStart = document.getElementById(actualStartID);
//    var actualEnd = document.getElementById(actualEndID);
//    //    alert(actualCostID);
//    //    alert(actualCost);
//    if (planStart.value.trim() == "") {
//        alert("The Plan Start Day Can Not Be Null!");
//        return false;
//    }

//    if (planEnd.value.trim() == "") {
//        alert("The Plan End Day Can Not Be Null!");
//        return false;
//    }

//    if (planStart.value.trim() != "" && planEnd.value.trim() != "") {
//        if (!CheckDate(planStart.value.trim(), planEnd.value.trim())) {
//            alert("Plan end date should not be less than plan start date.");
//            planEnd.value = "";
//            planEnd.focus();
//            return false;
//        }
//    }

//    if (actualStart.value.trim() != "" && actualEnd.value.trim() != "") {
//        if (!CheckDate(actualStart.value.trim(), actualEnd.value.trim())) {
//            alert("Actual end date should not be less than actual start date.");
//            actualEnd.value = "";
//            actualEnd.focus();
//            return false;
//        }
//    }

//    if (planCost.value != "" && actualCost.value != "") {
//        if ((parseFloat(planCost.value) < parseInt(parseFloat.value) || actualCost.value != "") && percent.value == "") {
//            alert("Please input complete percent.");
//            percent.focus();
//            return false;
//        }
//        else if (percent.value != "" && parseFloat(percent.value) > 100) {
//            alert("Complete percent should be not more than 100.");
//            percent.value = "";
//            percent.focus();
//            return false;
//        }
//    }

//    if (actualCost.value == "" && percent.value != "") {
//        // alert(actualCost);
//        alert("Please input actual cost value first.");
//        actualCost.focus();
//        percent.value = "";

//        return false;
//    }

//    if (percent.value == "100" || percent.value == "100%") {
//        //added by Albee Check ActualDate  2010-08-26   
//        if (actualStart.value.trim() == "") {
//            alert("The Actual Start Day Can Not Be Null!");
//            actualStart.focus();
//            return false;
//        }

//        if (actualEnd.value.trim() == "") {
//            alert("The Actual End Day Can Not Be Null!");
//            actualEnd.focus();
//            return false;
//        }
//        //return true;       
//    }

//    //added by aic0/hansel on 20101206
//    var Remark = document.getElementById(RemarkID);
//    var plan = new Date(planEnd.value.trim().replace(/-/g, "/"));
//    var actual = new Date(actualEnd.value.trim().replace(/-/g, "/"));
//    if (Date.parse(actual) - Date.parse(plan) > 0) {
//        if (Remark.value.trim() == "") {
//            alert('Please input remark,\nbecause the actual end day is bigger than the plan end day.');
//            Remark.focus();
//            return false;
//        }
//    }
//    //     if (CheckDate(planEnd.value.trim(),actualEnd.value.trim()))
//    //     {
//    //         if (Remark.value.trim() == "")
//    //         {
//    //             alert('Please fill in the Remark!\nFor the Actual End Day is big than the Plan End Day!');
//    //             Remark.focus();
//    //             return false;
//    //         }
//    //     }
//    //end added         

//    return true;
//}

function ConfirmResource() {
    if (!confirm("The resource you selected is right?")) {
        return false;
    }
    else {
        return true;
    }
}

//function TaskNameMouseOver(obj) {
//    obj.style.cursor = "hand";
//    obj.style.color = "red";
//}

//function TaskNameMouseOut(obj) {
//    obj.style.cursor = "pointer";
//    obj.style.color = "#000";
//}




function KeyName() {
    // a-z + A-Z + .+,
    if (!(
                (event.keyCode > 64 && event.keyCode < 91) ||
                (event.keyCode > 96 && event.keyCode < 123) ||
                (event.keyCode == 46) || (event.keyCode == 44) ||
                (event.keyCode == 59) || (event.keyCode == 32)
                )) {
        event.keyCode = 0;
        return;
    }
}

  function clearNoNum(obj)
	{
		//先把非数字的都替换掉，除了数字和.
		obj.value = obj.value.replace(/[^\d.]/g,"");
		//必须保证第一个为数字而不是.
		obj.value = obj.value.replace(/^\./g,"");
		//保证只有出现一个.而没有多个.
		obj.value = obj.value.replace(/\.{2,}/g,".");
		//保证.只出现一次，而不能出现两次以上
		obj.value = obj.value.replace(".","$#$").replace(/\./g,"").replace("$#$",".");
	}

function CheckSaveMyTask() {

    var percent = document.getElementById("TextBoxComplete");
    var actualCost = document.getElementById("TextBoxActualCost");
    var planCost = document.getElementById("TextBoxPlanCost");
    var startDate = document.getElementById("DateTextBoxActualStart");
    var endDate = document.getElementById("DateTextBoxActualEnd");
 
    if(actualCost.value!="")
    {
            if(percent.value=="")
            {
            alert("Please input Percent.");
            percent.focus();
            return false;
            }
            if(startDate.value=="")
            {
            alert("Please input Actual Start Date.");
            startDate.focus();
            return false;
            }
    }
    if(percent.value!="")
    {
           if(actualCost.value=="")
            {
            alert("Please input Actual Cost.");
            actualCost.focus();
            return false;
            }
             if(startDate.value=="")
            {
            alert("Please input Actual Start Date.");
            startDate.focus();
            return false;
            }
      
    }
    if(startDate.value.trim() != "")
     {      
            if(actualCost.value=="")
            {
            alert("Please input Actual Cost.");
            actualCost.focus();
            return false;
            }
            if(percent.value=="")
            {
            alert("Please input Percent.");
            percent.focus();
            return false;
            }
            
     }
	//modified by ename wang on 2011.11.04
    if(endDate.value!=""&&parseFloat(percent.value)<100)
    {
            alert("Percent must be 100.");
            percent.focus();
            return false;
    }
    if(parseFloat(percent.value)>100)
    {
            alert("Percent must be 0 to 100.");
            percent.focus();
            return false;
    }
    if(startDate.value!=""&&endDate.value!="")
    {
       if(parseFloat(actualCost.value)==0)
       {
          alert("Actual Cost must be more than 0 .");
            actualCost.focus();
            return false;
       }
       
    }
    if(percent.value=="100")
    {
       if(parseFloat(actualCost.value)==0)
       {
            alert("Actual Cost must be more than 0 .");
            actualCost.focus();
            return false;
       }
       
        if (startDate.value.trim() == "") 
        {
            alert("Please input Actual Start Date.");
            startDate.focus();
            return false;
        }
        if (endDate.value.trim() == "") 
        {
            alert("Please input Actual End Date.");
            endDate.focus();
            return false;
        }
    }
     

    if (planCost.value != "" && actualCost.value != "") {
        if ((parseFloat(planCost.value) < parseFloat(actualCost.value) || actualCost.value != "") && percent.value == "") {
            alert("Please input complete percent.");
            percent.focus();
            return false;
        }
        else if (percent.value != "" && parseFloat(percent.value) > 100) {
            alert("Complete percent should be not more than 100.");
            percent.value = "";
            percent.focus();
            return false;
        }
    }

    if (actualCost.value.trim() == "" && percent.value.trim() != "") {
        //&&actualCost.value > planCost.value
        alert("Please input actual cost time.");
        actualCost.focus();
        return false;
    }

    //added by Albee 2010-08-27 check actual date
    if (percent.value.trim() == "100") {
        if (startDate.value.trim() == "") {
            alert("Please input Actual Start Date.");
            startDate.focus();
            return false;
        }
        if (endDate.value.trim() == "") {
            alert("Please input Actual End Date.");
            endDate.focus();
            return false;
        }
    }
    //add by ename
   
    if(parseFloat(actualCost.value)>0||parseFloat(percent.value)>0)
    {
       if (startDate.value.trim() == "") {
            alert("Please input Actual Start Date.");
            startDate.focus();
            return false;
        }
      
    }
    var Remark = document.getElementById("TextBoxRemark");
    var planEnd= document.getElementById("TextBoxPlanEnd");
    var actualEnd=document.getElementById("DateTextBoxActualEnd");
    if(actualEnd.value!="")
    {
     if (CheckDate(planEnd.value.trim(),actualEnd.value.trim()))
         {
            if (Remark.value.trim() == "")
            {
                alert('Please fill in the Remark!\nFor the Actual End Day is big than the Plan End Day!');
                Remark.focus();
              return false;
            }
        }
      
     }

    if (startDate.value.trim() != "" && endDate.value.trim() != "") {

        if (CheckDate(endDate.value.trim(), startDate.value.trim())) {
            alert("Actual end date should not be less than actual start date.");
            endDate.value = "";
            endDate.focus();
            return false;
        }
        else {
        return confirm('Have you checking the information you want to save.');
       }
    }
    else 
    {
        return confirm('Have you checking the information you want to save.');
     }
   
   

}
//modified by ename wang on 2011.10.13
function CheckDate(startDate, endDate) {


    var start = new Date(startDate.replace(/-/g, "/"));
    var end = new Date(endDate.replace(/-/g, "/"));
    if (Date.parse(start) - Date.parse(end) >=0) 
    
    {
        return false;
    }
    return true;
}
//end modified by ename wang on 2011.10.13

function openModalDialog() {
    var result = showModalDialog("MyTaskDetail.aspx");

    if (result != null) {
        //待弹出窗体关闭时执行的代码。	
    }
}

//added by Albee 20100721
function CheckVersion(TextBoxId) {
    var VersionBox = document.getElementById(TextBoxId);
    var temp = new Array();
    var strversion;
    strversion = VersionBox.value;
    temp = strversion.split(".");
    if (strversion.length != 0) {
        if (temp.length == 3) {
            if (temp[1].length != 4 || temp[2].length != 2 || temp.length != 3) {
                alert("Version Format: AA.BBCC.DD");
                VersionBox.focus();
                return false;
            }
        }
        else {
            alert("Version Format: AA.BBCC.DD");
            VersionBox.focus();
            return false;
        }
    }
    return true;

}