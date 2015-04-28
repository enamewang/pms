//Created by Paul L Wang on July,27,2006

//////////////////////////////////////////////////////////////////////////////////////
//当TreeView控件某一结点的CheckBox被钩选/取消时,自动将该结点的所有子结点的CheckBox钩选/取消

window.onload=DetectCheckTree; //自动注册

 var keyCheckTree  = "Tree";  //keyCheckTree代表TreeView控件id开头关键字，TreeView内部的所有CheckBox的id也以该关键字开头
 var keyCheckLevel = "&nbsp;";//keyCheckLevel代表TreeView内部的所有CheckBox用于区分位于不同层次的title关键字
 
 var allCheckBox=document.all.tags("input"); //这里大小写无所谓
 
//自动检测TreeView控件内部的CheckBox，并为其注册点击事件，对标记id属性的大小写敏感★
function DetectCheckTree()
{  
    var i;

    //找出名字以keyCheckTree字符串开头的CheckBox控件
    for (i=0;i<allCheckBox.length;i++)
    {
        if (allCheckBox[i].type.toLowerCase()=="checkbox") //是CheckBox
        {
            if (allCheckBox[i].disabled==false) //是没有被禁止的
            {
                if (allCheckBox[i].id.indexOf(keyCheckTree)>=0) //名称中含有keyCheckTree关键字
                {
                    if( allCheckBox[i].title.lastIndexOf( keyCheckLevel+keyCheckLevel+keyCheckLevel ) >= 0 ) //第二层结点
                    {
                        allCheckBox[i].onclick = CheckParentNode;//第二层结点注册不同的动作
                    }
                    else //第一层结点
                    {
                        allCheckBox[i].onclick=CheckAllTree; //把CheckAll动作注册给它，允许多个注册组
                    }
                }
            }
        }
    }
}

//当子结点被点击时触发该事件，若被取消钩选，则使其父结点取消钩选
function CheckParentNode( Sender )
{
    if(Sender==null) { Sender = this;  }
    
    if( Sender.checked == true )
    {
        return;
    }
    
    var title = Sender.title;
    //alert(Sender.id);
    
    GetTreeViewControlName( Sender.id );
    
    var i ; //本checkbox在allCheckBox数组中的下标

    for( i = 0 ; i < allCheckBox.length ; i++)
    {
        if( allCheckBox[i].id == Sender.id )
        {
            break;
        }
    }
      
    for( i = i - 1 ; i >= 0 ; i--)
    {
        if( allCheckBox[i].title.lastIndexOf( keyCheckLevel+keyCheckLevel+keyCheckLevel ) < 0 )
        {
            break;
        }
    }
    
    allCheckBox[i].checked = false;
}

/*
//检查某一子结点下方的兄弟结点是否都被钩选
function LatterNodeCheck( no )
{
    //var flag = false;
    var i ;
    
    for( i = no + 1 ; i < allCheckBox.length ; i++)
    {
        if( allCheckBox[i].title.lastIndexOf( keyCheckLevel+keyCheckLevel+keyCheckLevel ) < 0 )
        {
            break;
        }
        else if( allCheckBox[i].checked == true )
        {
            //flag = true;
        }
        else if( allCheckBox[i].checked == false )
        {
            return false;
        }
    }
    
    return true;
}
*/

//改变组选作用范围内CheckBox控件的状态
function CheckAllTree(Sender)
{
    if(Sender==null) { Sender = this;  }
    
    var title = Sender.title;
    var level = title.lastIndexOf(keyCheckLevel);//不同层次中的CheckBox的title包含有不同数量的title关键字
    //alert(Sender.id + " " + title + " " + level);
    
    GetTreeViewControlName( Sender.id );//获取该TreeView控件的id名称

    var no = GetNumOfCheckBox( Sender );//获取该CheckBox在TreeView控件中的序号
    //alert( no );
        
    //var allCheckBox=document.all.tags("input");
    var i;
    
    var newCheckValue=Sender.checked; //设置CheckAll按钮自己的状态为新状态

    //设置所有作用范围内的CheckBox的值
    for (i=0;i<allCheckBox.length;i++)
    {
        if (allCheckBox[i].type.toLowerCase()=="checkbox") //是CheckBox
        {
            if (allCheckBox[i].disabled==false) //是没有被禁止的
            {
                if (allCheckBox[i].id.indexOf(keyCheckTree)==0) //名称里以keyCheckTree字符串开头，表示是TreeView控件内的CheckBox
                {
                    var numb = GetNumOfCheckBox( allCheckBox[i] );//正在比较的CheckBox的序号
                    var titleofcheckbox = allCheckBox[i].title;   //正在比较的CheckBox的title
                    var levelofcheckbox = titleofcheckbox.lastIndexOf(keyCheckLevel);//正在比较的CheckBox的层次
                    
                    //alert( levelofcheckbox );
                    
                    if( numb > no && levelofcheckbox > level )//若正在比较的CheckBox的序号和层次都大于本CheckBox，则为本CheckBox的子结点
                    {
                        allCheckBox[i].checked=newCheckValue;
                    }
                    else if( numb > no && levelofcheckbox == level )////若正在比较的CheckBox的层次等于CheckBox，表示循环到该层的下一个结点，本CheckBox的子结点查找完毕
                    {
                        return;
                    }
                }
            }
        }
    }
}

//获取CheckBox在TreeView中的序号
function GetNumOfCheckBox( Sender )
{
    var endIndex = Sender.id.lastIndexOf("CheckBox");
    var NumbOfCheckbox = Sender.id.substring( keyCheckTree.length + 1, endIndex );
    //alert(keyCheckAll.length);
    var no = parseInt( NumbOfCheckbox , 0 );
    return no;
}

//获取TreeView控件的id
function GetTreeViewControlName(id)
{
    var length = id.lastIndexOf("n");
    keyCheckTree = id.substring( 0 , length );
    //alert(keyCheckTree);
}