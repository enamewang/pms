<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemListPopUp.aspx.cs"
    Inherits="PMS.PMS.Maintain.SystemListPopUp" %>

<%@ Register Src="~/SysFrame/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>
<%--<%@ Register TagPrefix="uc1" TagName="GridViewPager" Src="~/SysFrame/GridViewPager.ascx" %>--%>
<%--<%@ Register src="../UserControls/Pager.ascx" tagname="Pager" tagprefix="uc2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="pragma" content="no-cache" />
    <%-- <base target="_self" />--%>
    <title>Select System</title>
    <style type="text/css">
        .style1
        {
            width: 174px;
        }
        .style2
        {
            width: 170px;
        }
        .style3
        {
            width: 177px;
        }
    </style>
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divJavascript" runat="server">

        <script language="javascript" type="text/javascript">

            function gridDbClick(obj) {
                selectRow(obj);
                confirm_record();
            }

            function selectRow(row) {
                unselectAll(row);
                var inputs = row.getElementsByTagName('INPUT');
                if (inputs != null && inputs.length > 0) {
                    for (var i = 0; i < inputs.length; i++) {
                        if (inputs[i].type.toLowerCase() == 'radio') {
                            inputs[i].checked = true;
                            break;
                        }
                    }
                    document.getElementById('ButtonOK').disabled = false;
                }
            }

            function unselectAll(row) {
                var inputs = row.parentNode.getElementsByTagName('INPUT');
                if (inputs != null && inputs.length > 0) {
                    for (var i = 0; i < inputs.length; i++) {
                        if (inputs[i].type.toLowerCase() == 'radio') {
                            inputs[i].checked = false;
                        }
                    }
                }
            }

            function pageLoad() {
                document.getElementById('ButtonOK').disabled = true;
            }

            function GetCheckedRadioButtonRowNum(grid) {
                var inputs = grid.getElementsByTagName('INPUT'); //获取GridView的Inputhtml
                if (inputs != null && inputs.length > 0) {
                    var rowIndex = 0;
                    for (var i = 0; i < inputs.length; i++) {
                        if (inputs[i].type.toLowerCase() == 'radio') {
                            rowIndex = rowIndex + 1;
                            if (inputs[i].checked == true) {
                                return rowIndex;
                            }
                        }
                    }
                    return -1;
                }
            }

            function confirm_record() {
                var grid = document.getElementById("<%=GridViewSystem.ClientID %>");
                if (grid == undefined || grid == null) {
                    window.returnValue = {
                        Site: "",
                        SystemDomain: "",
                       // PM: "",
                        SystemBriefName: ""
                    };
                   window.close();
                }
                else {
                    var rowIndex = GetCheckedRadioButtonRowNum(grid);
                    if (rowIndex != -1) {
                        var row = grid.firstChild.childNodes[rowIndex];
                        var rec = new Object();
                        rec.Site = row.childNodes[1].innerText;
                        rec.SystemDomain = row.childNodes[2].innerText;
                        // rec.PM = row.childNodes[6].innerText;
                        rec.SystemBriefName = row.childNodes[3].innerText;
                        window.returnValue = rec;
                        window.close();
                    }
                    else {
                        window.returnValue = {
                            Site: "",
                            SystemDomain: "",
                           // PM: "",
                            SystemBriefName: ""
                        };
                       window.close();
                    }
                }
            }
        
        
        </script>

    </div>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding-left: 10px;">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="LabelSite" runat="server" Text="Site" />
                        </td>
                        <td class="style3">
                            <asp:DropDownList ID="DropDownListSite" runat="server" CssClass="DropDownListGeneral"
                                Width="165px" />
                        </td>
                        <td align="right">
                            <asp:Label ID="LabelDomain" runat="server" Text="Domain" />
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="DropDownListDomain" runat="server" CssClass="DropDownListGeneral"
                                Width="165px" />
                        </td>
                        <td align="right">
                            <asp:Label ID="LabelPm" runat="server" Text="PM" meta:resourcekey="LabelPmResource1" />
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="DropDownListPM" runat="server" CssClass="DropDownListGeneral"
                                Width="165px" />
                        </td>
                        <td align="right">
                            <asp:Label ID="LabelSysName" runat="server" Text="System Name" />
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSysName" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="ButtonInquiry" runat="server" OnClick="ButtonInquiry_Click" Text="Query" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updatePanelPage" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc1:GridViewPager ID="GridViewPager1" Init_Grid_ID="GridViewSystem" OnBindGrid="BindGrid"
                            runat="server" SetPagerButtonImageStyle="Default" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="buttonInquiry" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updatePanelGridView" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <anthem:GridView ID="GridViewSystem" runat="server" OnRowCreated="GridViewSystem_RowCreated"
                            OnRowDataBound="GridViewSystem_RowDataBound" AutoGenerateColumns="False" Style="border-width: 1px;
                            border-style: Solid; width: 980px; border-collapse: collapse;" EmptyDataText="No data."
                            Font-Size="9pt" GridLines="Horizontal" HorizontalAlign="Left" CssClass="myGV"
                            AllowPaging="true">
                            <HeaderStyle CssClass="GVHeader" HorizontalAlign="Center" />
                            <RowStyle CssClass="GVDataTR" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GVAlterDataTR" />
                            <PagerSettings Visible="False" PageButtonCount="15" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HiddenFieldKey" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SystemId") %>' />
                                        <asp:RadioButton runat="server" ID="RadioButtonSelector" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Site" DataField="Site">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Domain" DataField="SystemDomain">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Brief Name" DataField="SystemBname">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="English Name" DataField="SystemEname">
                                    <HeaderStyle Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Chinese Name" DataField="SystemCname">
                                    <HeaderStyle Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="PM" DataField="EnglishName">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="System Id" DataField="SystemId" Visible="false">
                                    <HeaderStyle CssClass="NoneColumnStyle" />
                                </asp:BoundField>
                            </Columns>
                        </anthem:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="buttonInquiry" />
                        <asp:AsyncPostBackTrigger ControlID="GridViewPager1" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center" style="padding-top: 10px">
                <asp:Button ID="ButtonOK" runat="server" Text="Confirm" OnClientClick="confirm_record();return false;"
                    Width="80px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
