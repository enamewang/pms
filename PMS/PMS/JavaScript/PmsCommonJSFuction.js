//////////////////////////////////////////////////////////////////////////////////
// File name: PmsCommonJSFuction.js    
// Copyright (C) 2011 Qisda Corporation. All rights reserved.    
// Author:    Kite Zhang   
// ALTER  Date:   2011/08/03
// Current Version:  1.0
// Description:   Common JS Fuction for PMS
// History: 
// 
//      Date     |    Time    |    Author   |  Modification
// 1. 2011/08/06 |   14:05:50 |  Kite Zhang |  Create
//////////////////////////////////////////////////////////////////////////////////


//函数名：CheckControlValueIsNull
//功能介绍：判断控件值是否为空
//参数说明：ControlID(控件的ID), message(控件值为空时的提示信息)
//返回值：false：值为空  true：值不为空
function CheckControlValueIsNull(ControlID, message) {
    if (ControlID == undefined || ControlID == null) {
        alert("Control is null!");
        return false;
    }
    if (ControlID.value == "") {
        alert(message);
        txtControl.focus();
        return false;
    }
    return true;
}

function confirmDeleteItem(gridView) {
   
   //var grd = document.getElementById(gridView);
    if (gridView == null || gridView.rows.length == 3 || gridView.rows.length == 2) {    
      
        alert("The last item can not be deleted!");
        event.returnValue = false;
        return false;
    }
    else {
        if (confirm("Confirm to delete this item?") == true) {
            return true;
        }
        else {
            event.returnValue = false;
            return false;
        }
    }
}


function TaskNameMouseOver(obj) {
    obj.style.cursor = "hand";
    obj.style.color = "red";
}

function TaskNameMouseOut(obj) {
    obj.style.cursor = "pointer";
    obj.style.color = "#000";
}


//实现Server.UrlEncode和Server.UrlDecode功能的js代码
//使用方法："http://www.fanchuanbook.com/search.aspx?q=" + EncodeURI(q,false)
var EncodeURI = function(unzipStr, isCusEncode) {
    if (isCusEncode) {
        var zipArray = new Array();
        var zipstr = "";
        var lens = new Array();
        for (var i = 0; i < unzipStr.length; i++) {
            var ac = unzipStr.charCodeAt(i);
            zipstr += ac;
            lens = lens.concat(ac.toString().length);
        }
        zipArray = zipArray.concat(zipstr);
        zipArray = zipArray.concat(lens.join("O"));
        return zipArray.join("N");
    } else {
        //return encodeURI(unzipStr);
        var zipstr = "";
        var strSpecial = "!\"#$%&'()*+,/:;<=>?[]^`{|}~%";
        var tt = "";

        for (var i = 0; i < unzipStr.length; i++) {
            var chr = unzipStr.charAt(i);
            var c = StringToAscii(chr);
            tt += chr + ":" + c + "n";
            if (parseInt("0x" + c) > 0x7f) {
                zipstr += encodeURI(unzipStr.substr(i, 1));
            } else {
                if (chr == " ")
                    zipstr += "+";
                else if (strSpecial.indexOf(chr) != -1)
                    zipstr += "%" + c.toString(16);
                else
                    zipstr += chr;
            }
        }
        return zipstr;
    }
}

var DecodeURI = function(zipStr, isCusEncode) {
    if (isCusEncode) {
        var zipArray = zipStr.split("N");
        var zipSrcStr = zipArray[0];
        var zipLens;
        if (zipArray[1]) {
            zipLens = zipArray[1].split("O");
        } else {
            zipLens.length = 0;
        }

        var uzipStr = "";

        for (var j = 0; j < zipLens.length; j++) {
            var charLen = parseInt(zipLens[j]);
            uzipStr += String.fromCharCode(zipSrcStr.substr(0, charLen));
            zipSrcStr = zipSrcStr.slice(charLen, zipSrcStr.length);
        }
        return uzipStr;
    } else {
        //return decodeURI(zipStr);
        var uzipStr = "";

        for (var i = 0; i < zipStr.length; i++) {
            var chr = zipStr.charAt(i);
            if (chr == "+") {
                uzipStr += " ";
            } else if (chr == "%") {
                var asc = zipStr.substring(i + 1, i + 3);
                if (parseInt("0x" + asc) > 0x7f) {
                    uzipStr += decodeURI("%" + asc.toString() + zipStr.substring(i + 3, i + 9).toString()); ;
                    i += 8;
                } else {
                    uzipStr += AsciiToString(parseInt("0x" + asc));
                    i += 2;
                }
            } else {
                uzipStr += chr;
            }
        }
        return uzipStr;
    }
}

var StringToAscii = function(str) {
    return str.charCodeAt(0).toString(16);
}

var AsciiToString = function(asccode) {
    return String.fromCharCode(asccode);
}


function CheckPercent(actualCostID, planCostID, percentID, planStartID, planEndID, actualStartID, actualEndID,taskNameID,UserID, RemarkID) {

    var actualCost = document.getElementById(actualCostID);
    var planCost = document.getElementById(planCostID);
    var percent = document.getElementById(percentID);
    var planStart = document.getElementById(planStartID);
    var planEnd = document.getElementById(planEndID);
    var actualStart = document.getElementById(actualStartID);
    var actualEnd = document.getElementById(actualEndID);    
    
    
    // Add by Ename Wang on 20111208 10:35
    
    var taskName=document.getElementById(taskNameID);
    var user=document.getElementById(UserID);
    if(taskName.value.trim()=="")
    {
      alert("Task Name Can not be empty");
      taskName.focus()
      event.returnValue=false;
      return false;
    }
    
   // 选择项的索引
    var index   =   user.selectedIndex;   
   //选择项的值
    var value   =   user.options[index].value;   
    //选择项的内容
    var text     =   user.options[index].text;
   //下拉框选项个数
   // window.alert(user.options.length);
   
   //计划工期不为空时
   if(planCost.value.trim()!="")
   {
      if(planStart.value.trim() == ""||planEnd.value.trim() == ""||text=="")
      {
         alert(" PlanStartDate,PlanEndDate,and Resource  Can not be empty");
         event.returnValue=false;
         return false;
      }
      
   }
   
   //计划开始日期不为空时
   if(planStart.value.trim()!="")
   {
      if(planCost.value.trim() == ""||planEnd.value.trim() == ""||text=="")
      {
         alert(" PlanCost,PlanEndDate,and Resource  Can not be empty");
         event.returnValue=false;
         return false;
      }
      
   }
   
   //计划结束日期不为空时
  if(planEnd.value.trim()!="")
   {
      if(planStart.value.trim() == ""||planCost.value.trim() == ""||text=="")
      {
         alert(" planCost,PlanStartDate,and Resource  Can not be empty");
         event.returnValue=false;
         return false;
      }
      
   }
   
  //Resource不为空时
  if(text!="")
   {
      if(planStart.value.trim() == ""||planCost.value.trim() == ""||planEnd.value.trim() == "")
      {
         alert(" planCost,PlanStartDate,and PlanEndDate  Can not be empty");
         event.returnValue=false;
         return false;
      }
      
   }
  
    
   
    if (planStart.value.trim() != "" && planEnd.value.trim() != "") {
        if (!CheckDate(planStart.value.trim(), planEnd.value.trim())) {
            alert("Plan end date should not be less than plan start date.");
            planEnd.value = "";
            planEnd.focus();
            event.returnValue = false; return false;
        }
    }

    if (actualStart.value.trim() != "" && actualEnd.value.trim() != "") {
        if (!CheckDate(actualStart.value.trim(), actualEnd.value.trim())) {
            alert("Actual end date should not be less than actual start date.");
            actualEnd.value = "";
            actualEnd.focus();
            event.returnValue = false; return false;
        }
    }

    if (planCost.value != "" && actualCost.value != "") {
        if ((parseFloat(planCost.value) < parseInt(parseFloat.value) || actualCost.value != "") && percent.value == "") {
            alert("Please input complete percent.");
            percent.focus();
            event.returnValue = false; return false;
        }
        else if (percent.value != "" && parseFloat(percent.value) > 100) {
            alert("Complete percent should be not more than 100.");
            percent.value = "";
            percent.focus();
            event.returnValue = false; return false;
        }
    }

    if (actualCost.value == "" && percent.value != "") {
        // alert(actualCost);
        alert("Please input actual cost value first.");
        actualCost.focus();
        percent.value = "";

        event.returnValue = false; return false;
    }

    if (percent.value == "100" || percent.value == "100%") {      
        if (actualStart.value.trim() == "") {
            alert("The Actual Start Day Can Not Be Null!");
            actualStart.focus();
            event.returnValue = false; return false;
        }

        if (actualEnd.value.trim() == "") {
            alert("The Actual End Day Can Not Be Null!");
            actualEnd.focus();
            event.returnValue = false; return false;
        }
        return true;       
    }


    var Remark = document.getElementById(RemarkID);
    if (!CheckDate(actualEnd.value.trim(), planEnd.value.trim())) {
        if (Remark.value.trim() == "") {
            alert('Please input remark,\n because the actual end day is bigger than the plan end day.');
            Remark.focus();
            event.returnValue = false; return false;
        }
    }
       

    return true;

}

function CheckDate(startDate, endDate) {
    var start = new Date(startDate.replace(/-/g, "/"));
    var end = new Date(endDate.replace(/-/g, "/"));    
    if (Date.parse(start) - Date.parse(end) > 0) {
        return false;
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


//求两个时间的天数差 日期格式为“YYYY-MM-dd”
//   function DaysBetween(dateOne, dateTwo) {
//       var oneMonth = dateOne.substring(5, dateOne.lastIndexOf('-'));
//       var oneDay = dateOne.substring(dateOne.length, dateOne.lastIndexOf('-') + 1);
//       var oneYear = dateOne.substring(0, dateOne.indexof('-'));

//       var twoMonth = dateTwo.substring(5, dateTwo.lastIndexOf('-'));
//       var twoDay = dateTwo.substring(dateTwo.length, dateTwo.lastIndexOf('-') + 1);
//       var twoYear = dateTwo.substring(0, dateTwo.indexof('-'));
//       
////       var cha=((Date.parse(oneMonth+'/'+oneDay
//       
//       
//
//   }




