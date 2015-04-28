<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListBoxSelect.ascx.cs"
    Inherits="PMS.PMS.UserControls.ListBoxSelect" %>

<script type="text/javascript">

    function listBoxSelectExt_PageLoad(lstLeftId, lstRightId, hiddenFieldSelectedId, hiddenFieldUnSelectedId, sortFlag) {
        var lstLeft = document.getElementById(lstLeftId);
        var lstRight = document.getElementById(lstRightId);
        var hiddenFieldSelected = document.getElementById(hiddenFieldSelectedId);
        var hiddenFieldUnSelected = document.getElementById(hiddenFieldUnSelectedId);

        SetLstValue(lstRight, hiddenFieldSelected.value, sortFlag);
        SetLstValue(lstLeft, hiddenFieldUnSelected.value, sortFlag);
    }


    function dblArrowRight_OnClientClick(lstLeftId, lstRightId, hiddenFieldSelectedId, hiddenFieldUnSelectedId, sortFlag) {
        var lstLeft = document.getElementById(lstLeftId);
        var lstRight = document.getElementById(lstRightId);
        var hiddenFieldSelected = document.getElementById(hiddenFieldSelectedId);
        var hiddenFieldUnSelected = document.getElementById(hiddenFieldUnSelectedId);

        MoveAllToAnothor(lstLeft, lstRight);

        SetHidValue(lstRight, hiddenFieldSelected, sortFlag);
        SetHidValue(lstLeft, hiddenFieldUnSelected, sortFlag);
        SetLstValue(lstRight, hiddenFieldSelected.value, sortFlag);
        SetLstValue(lstLeft, hiddenFieldUnSelected.value, sortFlag);
        return false;
    }

    function ArrowRight_OnClientClick(lstLeftId, lstRightId, hiddenFieldSelectedId, hiddenFieldUnSelectedId, sortFlag) {
        var lstLeft = document.getElementById(lstLeftId);
        var lstRight = document.getElementById(lstRightId);
        var hiddenFieldSelected = document.getElementById(hiddenFieldSelectedId);
        var hiddenFieldUnSelected = document.getElementById(hiddenFieldUnSelectedId);

        MoveSelectedItemToAnother(lstLeft, lstRight);

        SetHidValue(lstRight, hiddenFieldSelected, sortFlag);
        SetHidValue(lstLeft, hiddenFieldUnSelected, sortFlag);
        SetLstValue(lstRight, hiddenFieldSelected.value, sortFlag);
        SetLstValue(lstLeft, hiddenFieldUnSelected.value, sortFlag);

        return false;

    }

    function ArrowLeft_OnClientClick(lstLeftId, lstRightId, hiddenFieldSelectedId, hiddenFieldUnSelectedId, sortFlag) {
        var lstLeft = document.getElementById(lstLeftId);
        var lstRight = document.getElementById(lstRightId);
        var hiddenFieldSelected = document.getElementById(hiddenFieldSelectedId);
        var hiddenFieldUnSelected = document.getElementById(hiddenFieldUnSelectedId);

        MoveSelectedItemToAnother(lstRight, lstLeft);

        SetHidValue(lstRight, hiddenFieldSelected, sortFlag);
        SetHidValue(lstLeft, hiddenFieldUnSelected, sortFlag);
        SetLstValue(lstRight, hiddenFieldSelected.value, sortFlag);
        SetLstValue(lstLeft, hiddenFieldUnSelected.value, sortFlag);

        return false;
    }

    function dblArrowLeft_OnClientClick(lstLeftId, lstRightId, hiddenFieldSelectedId, hiddenFieldUnSelectedId, sortFlag) {
        var lstLeft = document.getElementById(lstLeftId);
        var lstRight = document.getElementById(lstRightId);
        var hiddenFieldSelected = document.getElementById(hiddenFieldSelectedId);
        var hiddenFieldUnSelected = document.getElementById(hiddenFieldUnSelectedId);

        MoveAllToAnothor(lstRight, lstLeft);

        SetHidValue(lstRight, hiddenFieldSelected, sortFlag);
        SetHidValue(lstLeft, hiddenFieldUnSelected, sortFlag);
        SetLstValue(lstRight, hiddenFieldSelected.value, sortFlag);
        SetLstValue(lstLeft, hiddenFieldUnSelected.value, sortFlag);

        return false;
    }

    //////////////////////////////////////////
    //清空lst中的项
    function DelAll(lst) {
        var length = lst.options.length;
        for (var i = length; i > 0; i--) {
            lst.options[i - 1].parentNode.removeChild(lst.options[i - 1]);
        }
    }

    //清除lst中被选中的单个项
    function DelOne(lst) {
        var lstindex = lst.selectedIndex;
        if (lstindex < 0)
            return;
        lst.options[lstindex].parentNode.removeChild(lst.options[lstindex]);
    }

    //移动lst1中获得焦点的项目到lst2(一次只移动一个)
    function MoveOneToAnother(lst1, lst2) {
        var lstindex = lst1.selectedIndex;
        if (lstindex < 0)
            return;
        var v = lst1.options[lstindex].value;
        var t = lst1.options[lstindex].text;

        //new Option(文本, 值, 默认选中的选项, 选中的选项)
        lst2.options[lst2.options.length] = new Option(t, v, false, false);
        //删除lst1中的被选中项
        DelOne(lst1);
    }


    //移动lst1中获得焦点的所有项目到lst2(一次将选中的全部移动过去)
    function MoveSelectedItemToAnother(lst1, lst2) {
        var lst1Length = lst1.options.length;
        var lstindex = lst1.selectedIndex;
        if (lstindex < 0)
            return;

        for (var i = 0; i < lst1Length; i++) {
            if (lst1.options[i].selected) {
                var v = lst1.options[i].value;
                var t = lst1.options[i].text;
                lst2.options[lst2.options.length] = new Option(t, v, false, false);
            }
        }

        for (var j = lst1Length - 1; j >= 0; j--) {
            if (lst1.options[j].selected) {
                lst1.options[j].parentNode.removeChild(lst1.options[j]);
            }
        }
    }



    //将lst1中的项全部移动到lst2中
    function MoveAllToAnothor(lst1, lst2) {
        var lst1Length = lst1.options.length;
        var lst2Length = lst2.options.length;

        for (var i = 0; i < lst1Length; i++) {
            var v = lst1.options[i].value;
            var t = lst1.options[i].text;
            lst2.options[lst2Length + i] = new Option(t, v, false, false);
        }
        //清空lst1中的项
        DelAll(lst1);
    }

    ////////////////////////////////
    function SelectExtEntity(value, text) {
        this.Value = value;
        this.Text = text;
    }

    //比较两个字符串，用于排序
    function SelectExtCompare(x, y) {
        return (x.Text).localeCompare(y.Text);
    }


    function SetHidValue(lst, hiddenField, sortFlag) {
        var arrResult = new Array();
        var length = lst.options.length;
        for (var i = 0; i < length; i++) {
            var lstItem = lst.options[i];
            var item = new SelectExtEntity(lstItem.value, lstItem.text);
            //将item添加到数组中
            arrResult.push(item);
        }
        if (sortFlag) { arrResult.sort(SelectExtCompare); }
        var result = Sys.Serialization.JavaScriptSerializer.serialize(arrResult);
        hiddenField.value = result;
    }

    //lst为需要赋值的listbox控件
    //lstValues为ListBox控件的键值对集合序列化后的字符串
    //sortFlag是否排序
    function SetLstValue(lst, lstValues, sortFlag) {

        //先清除lst中的内容
        DelAll(lst);

        if (lstValues == null || lstValues == "") {
            return;
        }
        var arryAllValues = Sys.Serialization.JavaScriptSerializer.deserialize(lstValues);

        if (arryAllValues == null || arryAllValues.length == 0) {
            return;
        }

        if (sortFlag) {
            arryAllValues.sort(SelectExtCompare);
        }

        for (var i = 0; i < arryAllValues.length; i++) {
            var item = arryAllValues[i];
            var v = item.Value;
            var t = item.Text;
            lst.options[i] = new Option(t, v, false, false);
        }

    }
            
</script>

<div id="divSelectExt" runat="server" style="width: 460px">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:HiddenField ID="HiddenFieldSort" runat="server" />
    <asp:HiddenField ID="HiddenFieldSelected" runat="server" />
    <asp:HiddenField ID="HiddenFieldUnSelected" runat="server" />
    <table style="width: 100%; border: 0px;">
        <tr>
            <td style="width: 45%; text-align: left;">
                <asp:ListBox ID="lstLeft" runat="server" AutoPostBack="false" SelectionMode="Multiple"
                    Width="100%" Height="120px"></asp:ListBox>
            </td>
            <td style="width: 10%; text-align: center;">
                <asp:ImageButton ID="dblArrowRight" runat="server" ImageUrl="../../images/Button_ArrowRight1.gif">
                </asp:ImageButton>
                <br />
                <asp:ImageButton ID="ArrowRight" runat="server" ImageUrl="../../images/Button_ArrowRight2.gif">
                </asp:ImageButton>
                <br />
                <asp:ImageButton ID="ArrowLeft" runat="server" ImageUrl="../../Images/Button_ArrowLeft2.gif">
                </asp:ImageButton>
                <br />
                <asp:ImageButton ID="dblArrowLeft" runat="server" ImageUrl="../../Images/Button_ArrowLeft1.gif">
                </asp:ImageButton>
            </td>
            <td style="width: 45%; text-align: left;">
                <asp:ListBox ID="lstRight" runat="server" AutoPostBack="false" SelectionMode="Multiple"
                    Width="100%" Height="120px"></asp:ListBox>
            </td>
        </tr>
    </table>
</div>
