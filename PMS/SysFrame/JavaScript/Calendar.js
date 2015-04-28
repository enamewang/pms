
/*
function calendar(source){
	var originalDate = source.value;
	var passedDate = ParseDateValue(originalDate);
	var outDate = showModalDialog("./Scripts/calendar1.htm", passedDate, CalendarDialogParameter(source));
	if(outDate != null && outDate != "" && outDate != "undefined")
    {
        source.value = outDate.split(";")[0];
    }
    else{
		source.value = originalDate;
    }
}
*/
var DATE_SEPARATORS = new Array("/", "-", ".", " ", "");
var DEFAULT_DATE_SEPARATOR = "/";	// Value limited in array DATE_SEPARATORS.
var DEFAULT_DATE_FORMAT = "yyyy/mm/dd";				// Support yyyymmdd, mmddyyyy, ddmmyyyy
var LATIN_MONTH_LIST = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
// Default features in IE6 or below and other browsers.
var CALENDAR_DIALOG_FEATURES = "status:no;dialogWidth:270px;dialogHeight:250px;center:yes;help:no;scroll:no;";

// For IE 7.0 or abover version
var TITLE_BAR_ESTIMATED = 22; // 29 pixels or so for XP, 22 for Win2K...etc.Also relate to various themes.
var CHROME_THICKNESS_ESTIMATED = 2; // about 2 pixels or so...
// For Service Pack 2
var STATUS_BAR_ESTIMATED = 0; // Roughly 25 pixels or so... *no status bar in default security zone.
// Adjust size
var ADJUSTED_WIDTH = 2 * CHROME_THICKNESS_ESTIMATED;
var ADJUSTED_HEIGHT = 2 * CHROME_THICKNESS_ESTIMATED + TITLE_BAR_ESTIMATED + STATUS_BAR_ESTIMATED;	
var BROWSER_VERSION = getBrowserVersion();
if(BROWSER_VERSION > -1 && BROWSER_VERSION > 6.0)	// Browser is IE and version above 6.0, redefine features.
{	
	CALENDAR_DIALOG_FEATURES = "status:no;dialogWidth:" + (270 - ADJUSTED_WIDTH) + "px;dialogHeight:" + (250 - ADJUSTED_HEIGHT) + "px;center:yes;help:no;scroll:no;";
}

// Open calendar. Notic: existes some special code for flowER platform.
function calendar(t) 
{
	// Set calendar1.htm path relative to current document.
	var sPath =  GotRootPath() + "SysFrame/Controls/Calendar/calendar.htm";
	if(typeof getCalendarPath == "function")
	{
		sPath = getCalendarPath() + sPath;
	}
	
	// Set dialog features, shield the browser's version difference.	
	var sFeatures = CALENDAR_DIALOG_FEATURES;	
	// Set dialog position.
	if(typeof getDialogPosition == "function") 
	{
		sFeatures += getDialogPosition();
	}
	
	// Set input date.
	var inDate = t.value;	
	if(typeof beforeCalendar == "function")
	{
		inDate = beforeCalendar(inDate);				
	}
	
	// Open dialog
	var outDate = showModalDialog(sPath, inDate, sFeatures);	
	if(typeof afterCalendar == "function")
	{
		outDate = afterCalendar(outDate);
	}
	
	// Refresh source element's value.	
	t.value = outDate;
}

function GotRootPath(){
	var sPath = location.pathname;
	if (sPath.split.length > 1){
		return sPath.substr(0, sPath.indexOf("/", 1) + 1);
	}
	else{
		return "/";
	}
}

// Returns the version of Internet Explorer or a -1
// (indicating the use of another browser).
function getBrowserVersion()
{
	var rv = -1;	// Return value assumes failure
	if (navigator.appName == 'Microsoft Internet Explorer')
	{
		var ua = navigator.userAgent;
		var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
		if (re.exec(ua) != null)
			rv = parseFloat(RegExp.$1);
	}
	return rv;
}

// Set reasonable position for Calendar according to event object.
function getDialogPosition()
{
	try
	{		
		var sDialogLeft = event.screenX - event.offsetX;
		var sDialogTop = event.screenY + event.srcElement.offsetHeight;
		if(sDialogTop + 250 > screen.availHeight)
			sDialogTop = event.screenY - event.srcElement.offsetHeight - 250;		
		return "dialogLeft:" + sDialogLeft + "px;dialogTop:" + sDialogTop + "px;";
	}
	catch(ex)
	{
		return "";
	}
}

// Get calendar1.htm path relative to current document.
function getCalendarPath()
{
	return "";
}

// Pre-Process input date as you excepted.
function beforeCalendar(inDate)
{	
	// Change next code here.
	// You must ensure calendar can parse 'inDate' correctly .		
	if(inDate == "")
		return inDate;
	var ifDate = inDate.search(/[-./ ]/i);
	if(ifDate != -1)
		inDate = inDate.replace(/[-. ]/g, "/");
	else 
		{
		// *We assume the input date formate is consistent with DEFAULT_DATE_FORMAT strictly.	
			var sDateFormat = DEFAULT_DATE_FORMAT.toLowerCase();
			if (sDateFormat.charAt(0) == "y")
			{
				inDate = inDate.substr(0,4).concat("/",inDate.substr(4,2),"/",inDate.substr(6,2));
			}
			else 
			{
				inDate = inDate.substr(0,2).concat("/",inDate.substr(2,2),"/",inDate.substr(4,4));
			}
		}			
		var aDate = inDate.split("/");	
		if (aDate.length == 3)
		{
			var oDate = new Object();
			var sDateFormat = DEFAULT_DATE_FORMAT.toLowerCase();
			
			oDate[sDateFormat.charAt(0)] = aDate[0];		
			oDate[sDateFormat.charAt(sDateFormat.length - 1)] = aDate[2];
					
			var sFirst = sDateFormat.charAt(0);
			var sPreserve = "ymd";
			for(var i = 1; i < sDateFormat.length; i++)
			{
				if(sPreserve.indexOf(sDateFormat.charAt(i)) != -1)
				{			
					if(sFirst != sDateFormat.charAt(i))	
					{			
						oDate[sDateFormat.charAt(i)] = aDate[1];
						break;
					}
				}				
			}		
			return oDate["y"] + "/" + oDate["m"] + "/" + oDate["d"];
		}
		else
			return inDate;	
}
// Pre-Process output date as you excepted.
// Default date formate is [mm dd, yyyy] return by calendar. Sample: [February 4, 2006].
function afterCalendar(outDate)
{	
	// Change next code here.
	// You can re-formate 'outDate' as your system need.
	return formatDate(outDate, DEFAULT_DATE_FORMAT, DEFAULT_DATE_SEPARATOR);
}

function ToDate(toParse, separator){
	var parts = toParse.split(DEFAULT_DATE_SEPARATOR);
	return new Date(parts[2], parts[1], parts[0]);
}

function formatDateS(sDate){
	return formatDate(sDate, DEFAULT_DATE_FORMAT, DEFAULT_DATE_SEPARATOR);
}
// Formate date according to input date format.
function formatDate(sDate, sDateFormat, sDateSeparator) 
{
	var sScrap = "";
	var dScrap = new Date(sDate);
	
	// If date is invalid, empty string returned.	
	if(isNaN(dScrap)) return "";
	
	iDay = dScrap.getDate();
	iMonth = dScrap.getMonth() + 1;
	iYear = dScrap.getFullYear();

	if (iDay < 10) iDay = "0" + iDay;
	if (iMonth < 10) iMonth ="0" + iMonth;
	
	var oDate = new Object();
	oDate.y = iYear;
	oDate.m = iMonth;
	oDate.d = iDay;

	if (arguments.length == 1 || sDateFormat == "") {sDateFormat = "yyyymmdd";}
	if (arguments.length == 2) {sDateSeparator = "/";}
	sDateFormat = sDateFormat.toLowerCase();
	
	var sPrevious = "";
	var sCurrent = "";
	var sPreserve = "ymd";
	for(var i = 0; i < sDateFormat.length; i++)
	{
		if(sPreserve.indexOf(sDateFormat.charAt(i)) != -1)
		{			
			sCurrent = sDateFormat.charAt(i);
			if(sCurrent != sPrevious)
			{
				sScrap += oDate[sCurrent] + sDateSeparator;
				sPrevious = sCurrent;
			}				
		}
	}
	if(sDateSeparator =="")
		return sScrap == "" ? sScrap : sScrap.substring(0, sScrap.length );
	else
		return sScrap == "" ? sScrap : sScrap.substring(0, sScrap.length - 1);
}