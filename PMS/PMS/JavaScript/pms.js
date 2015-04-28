function getconfirm() {
    if (confirm("Are you sure to exit ?") == true) {
        window.close();
        return true;
    }
    else {
        event.returnValue = false;
        return false;
    }
}

function confirmDelete() {
    if (confirm("Confirm to delete?") == true) {
        return true;
    }
    else {
        event.returnValue = false;
        return false;
    }
}

function KeyPressForbidden(event) {
    event.returnValue = false;
}

//function ShowDetailAllOrNone(object)
//{
//	var imageString = document.getElementById("imageButtonExpand").src;
//	var showOrHide = imageString.substring(imageString.lastIndexOf("/"),imageString.length)

//	if (showOrHide.toLowerCase() == "/hide.gif")
//	{ 
//		var grd = document.getElementById("DivDesignDetail");
//	    if (grd != null )
//	    {
//	        if(grd .Visble=true)
//	        {
//	            document.all.DivDesignDetail.style["display"] = "block";
//	            document.all.ImageDivDesignDetailOpen.src = "../../SysFrame/images/appear.gif";
//	        } 
//        }
//        
//        var grd = document.getElementById("DivDevelopmentDetail");
//	    if (grd != null )
//	    {
//	    	if(grd .Visble=true)
//	        {
//	            document.all.DivDevelopmentDetail.style["display"] = "block";
//	            document.all.ImageDivDevelopmentDetailOpen.src = "../../SysFrame/images/appear.gif"; 
//	        }
//        }
//        
//		var grd = document.getElementById("DivTestDetail");
//	    if (grd != null )
//	    {
//	    	if(grd .Visble=true)
//	        {
//	            document.all.DivTestDetail.style["display"] = "block";
//	            document.all.ImageDivTestDetailOpen.src = "../../SysFrame/images/appear.gif";
//	        }
//        }
//        
//		var grd = document.getElementById("DivReleaseSDetail");
//	    if (grd != null )
//	    {
//	    	if(grd .Visble=true)
//	        {
//	            document.all.DivReleaseSDetail.style["display"] = "block";
//	            document.all.ImageDivReleaseSDetailOpen.src = "../../SysFrame/images/appear.gif";
//	        }
//	    }
//	    
//		var grd = document.getElementById("DivSupportDetail");
//	    if (grd != null )
//	    {
//	        if(grd .Visble=true)
//	        {
//	            document.all.DivSupportDetail.style["display"] = "block";
//	            document.all.ImageDivSupportDetailOpen.src = "../../SysFrame/images/appear.gif";
//	        }
//        }
//        
//		document.getElementById("imageButtonExpand").src = "../../SysFrame/images/appear.gif";
//	}
//	else if (showOrHide.toLowerCase() == "/appear.gif")
//	{
//		var grd = document.getElementById("DivDesignDetail");
//	    if (grd != null )
//	    {
//	        if(grd .Visble=true)
//	        {
//	            document.all.DivDesignDetail.style["display"] = "none";
//	            document.all.ImageDivDesignDetailOpen.src = "../../SysFrame/images/hide.gif";
//	        }
//        }
//        
//        var grd = document.getElementById("DivDevelopmentDetail");
//	    if (grd != null )
//	    {
//	        if(grd .Visble=true)
//	        {
//	            document.all.DivDevelopmentDetail.style["display"] = "none";
//	            document.all.ImageDivDevelopmentDetailOpen.src = "../../SysFrame/images/hide.gif"; 
//	        }
//        }
//        
//        var grd = document.getElementById("DivTestDetail");
//	    if (grd != null )
//	    {
//	        if(grd .Visble=true)
//	        {
//	            document.all.DivTestDetail.style["display"] = "none";
//	            document.all.ImageDivTestDetailOpen.src = "../../SysFrame/images/hide.gif";
//	        }
//        }
//        
//        var grd = document.getElementById("DivReleaseSDetail");
//	    if (grd != null )
//	    {
//	        if(grd .Visble=true)
//	        {
//	            document.all.DivReleaseSDetail.style["display"] = "none";
//	            document.all.ImageDivReleaseSDetailOpen.src = "../../SysFrame/images/hide.gif";
//	        }
//        }
//        
//        var grd = document.getElementById("DivSupportDetail");
//	    if (grd != null )
//	    {
//	        if(grd .Visble=true)
//	        {
//	            document.all.DivSupportDetail.style["display"] = "none";
//	            document.all.ImageDivSupportDetailOpen.src = "../../SysFrame/images/hide.gif";
//	        }
//        }
//        
//		document.getElementById("imageButtonExpand").src = "../../SysFrame/images/hide.gif";
//	}
//}

//function ShowDetail(div)
//{
//    if (div.style["display"] == "none") 
//    {
//	    div.style["display"] = "block";
//	    document.getElementById("Image" + div.id + "Open").src ="../../SysFrame/images/appear.gif";
//    }
//    else 
//    {
//	    div.style["display"] = "none";
//	    document.getElementById("Image" + div.id + "Open").src = "../../SysFrame/images/hide.gif";
//    }
//}

//function confirmDeleteItem(gridView)
//{ 
//    var grd = document.getElementById(gridView);
//    if (grd == null || grd.rows.length ==3 || grd.rows.length ==2)
//    {   
//        alert('The last item can not be deleted!');
//        event.returnValue = false;
//        return false; 
//    }
//    
//    else
//    {
//        if (confirm("Confirm to delete this item?")==true) 
//        { 
//            return true;
//        }
//        else
//        { 
//            event.returnValue = false;
//            return false; 
//        }
//    }
//}

//function TaskNameMouseOver(obj)
//{
//   obj.style.cursor ="hand";
//   obj.style.color ="red";
//}

//function TaskNameMouseOut(obj)
//{
//   obj.style.cursor ="pointer";
//   obj.style.color ="#000";
//}

function Numbers(txt, e, isFloat) {

    var keynum;
    var keychar;
    var numcheck;

    if (window.event) {  // IE
        keynum = e.keyCode;
    }

    else if (e.which) {  // Netscape/Firefox/Opera
        keynum = e.which;
    }

    if (keynum == 46) {
        if (isFloat == false)
            return false;

        if (txt != null) {
            if (txt.value == "")
                return false;
            var pos = txt.value.lastIndexOf(".");
            if (pos > 0)
                return false;
        }
    }
    if (keynum == 8 || keynum == 46) return true;

    keychar = String.fromCharCode(keynum);
    numcheck = /\d/;
    return numcheck.test(keychar);
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

//    alert("fasfdasfd");

//    var actualCost = document.getElementById(actualCostID.toString());

//    alert(actualCost);


//    var planCost = document.getElementById(planCostID.toString());
//    alert(planCost);

//    var percent = document.getElementById(percentID.toString());

//    alert(percent);
//    var planStart = document.getElementById(planStartID.toString());

//    alert(planStart);
//    var planEnd = document.getElementById(planEndID.toString());
//    alert(planEnd);

//    var actualStart = document.getElementById(actualStartID.toString());
//    alert(actualStart);

//    var actualEnd = document.getElementById(actualEndID.toString());



////    alert("fasfdasfd");

////    var actualCost = actualCostID;

////    alert(actualCost);


////    var planCost =planCostID;
////    alert(planCost);

////    var percent =percentID;

////    alert(percent);
////    var planStart = planStartID;

////    alert(planStart);
////    var planEnd = planEndID;
////    alert(planEnd);

////    var actualStart = actualStartID;
////    alert(actualStart);

////    var actualEnd = actualEndID;

//    
//    
//   
//   
//    alert(actualEnd);

//    if (planStart.value.trim() == "") {

//        alert(planStart);
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
//        alert("Please input actual cost value first.");
//        actualCost.focus();
//        percent.value = "";

//        return false;
//    }

//    if (percent.value == "100" || percent.value == "100%") {
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
//    }

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

//    return true;
//}




function CheckPromote() {
    //debugger;

    var sd = CheckList[0];
    var se = CheckList[1];
    var qa = CheckList[2];
    var dueDate = CheckList[3];

    if (sd != null && sd == "") {
        alert("Please assign SD");
        return false;
    }

    if (se != null && se == "") {
        alert("Please assign SE");
        return false;
    }

    if (qa != null && qa == "") {
        alert("Please assign QA");
        return false;
    }

    if (dueDate != null && dueDate == "") {
        alert("Please fill Due Date!");
        return false;
    }

    return false;
}

//add by Abel Li on 2014-2-6
/*
* 计算两个日期的间隔天数
* BeginDate:格式為：2012-01-01
* EndDate:，格式為：2012-01-02
* 返回兩個日期所差的天數
* 調用方法：
* alert("相差"+Computation("date1","date2")+"天");
*/
function GetDateRegion(StartDate, EndDate) {

    var aDate, oDate1, oDate2, iDays;
    var sDate1 = EndDate;   //sDate1和sDate2是2008-12-13格式
    var sDate2 = StartDate;
    aDate = sDate1.split("-");
    oDate1 = new Date(aDate[1] + '/' + aDate[2] + '/' + aDate[0]);   //转换为12/13/2008格式
    aDate = sDate2.split("-");
    oDate2 = new Date(aDate[1] + '/' + aDate[2] + '/' + aDate[0]);
    //iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 /24)+1;   //把相差的毫秒数转换为天数
    var i = (oDate1 - oDate2) / 1000 / 60 / 60 / 24;
    if (i < 0) {
        i -= 1;
    }
    else {
        i += 1;
    }
    iDays = i;   //把相差的毫秒数转换为天数
    //alert(iDays);
    return iDays;
}















