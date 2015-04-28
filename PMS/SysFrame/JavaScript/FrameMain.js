
 function PostBack()
         {
           var o = window.event.srcElement; 

           if (o.tagName == "INPUT" && o.type == "checkbox") 
           {
               __doPostBack("",""); 
           } 
         }  

 function HtmlEncode(text)
 {
    return text.replace(/&/g, '&amp').replace(/'/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}


//选择所有列表中的附件       
function checkAll() {
    var bolCheck = document.all("ChkAll").checked;
    var intCount = 0;

    for (intCount = 0; intCount < document.all.tags("input").length; intCount++) {
        if (document.all.tags("input")[intCount].type == "checkbox") {
            if (document.all.tags("input")[intCount].id != "ChkAll" && document.all.tags("input")[intCount].id != "chkIS_SM" && document.all.tags("input")[intCount].disabled != true) {
                document.all.tags("input")[intCount].checked = bolCheck;
            }
        }
    }
}     	 
 

//////////////////////////////////////////////////////////////////////////////////////
//Add by Hedda 20060410
//全自动组选模块

window.onload=DetectCheckAll; //自动注册

/***********************************
 组全选模块 开始
 ***********************************/
//自动检测CheckAll按钮，并注册其组全选事件，对标记id属性的大小写敏感★
function DetectCheckAll()
{
    var keyCheckAll="CheckAll"; //CheckAll命令按钮名称中应含有的关键字，有关键字的表示全选的按钮★
    
    var allCheckBox=document.all.tags("input"); //这里大小写无所谓
    var i;

    //找出名字含有keyCheckAll字符串CheckBox控件，它就是CheckAll按钮
    for (i=0;i<allCheckBox.length;i++)
    {
        if (allCheckBox[i].type.toLowerCase()=="checkbox") //是CheckBox
        {
            if (allCheckBox[i].disabled==false) //是没有被禁止的
            {
                if (allCheckBox[i].id.indexOf(keyCheckAll)>=0) //名称中含有keyCheckAll关键字
                {
                    allCheckBox[i].onclick=CheckAllGroup; //把CheckAll动作注册给它，允许多个注册组
                }
            }
        }
    }
}

//改变组选作用范围内CheckBox控件的状态
function CheckAllGroup(Sender)
{
   try{
        if(Sender==null) { Sender = this;  }
        //CheckAll命令按钮能够影响到的控件名称，应与CheckAll按钮相同，将All替换成One★
        
        var allCheckBox=document.all.tags("input");
        var i;
        
        var newCheckValue=this.checked; //设置CheckAll按钮自己的状态为新状态
        var keyCheckOne=this.id.replace("CheckAll","CheckOne"); //将All替换成One，为避免替换掉不必要的，因此加长特征字符串

        //设置所有作用范围内的CheckBox的值
        for (i=0;i<allCheckBox.length;i++)
        {
            if (allCheckBox[i].type.toLowerCase()=="checkbox") //是CheckBox
            {
                if (allCheckBox[i].disabled==false) //是没有被禁止的
                {
                    if (allCheckBox[i].id.indexOf(keyCheckOne)>=0) //名称里含有keyCheckOne字符，表示是CheckAll动作作用范围内的控件
                    {
                        allCheckBox[i].checked=newCheckValue;
                    }
                }
            }
        }
    }
    catch(e)
    {}
}
/***********************************
 组全选模块 结束
 ***********************************/
 
