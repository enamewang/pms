<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRNoInquiry.aspx.cs" Inherits="PMS.PMS.Maintain.CRNoInquiry" %>

<%@ Register TagPrefix="uc1" TagName="gridviewpager" Src="~/SysFrame/GridViewPager.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--  <base target="_self" />--%>
    <title>CRNo Inquiry</title>
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
                document.getElementById("<%=ButtonOK.ClientID %>").disabled = true;
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
                var grid = document.getElementById("<%=GridViewCrId.ClientID %>");
                if (grid == undefined || grid == null) {
                    window.returnValue = "";
                    window.close();
                }
                else {
                    var rowIndex = GetCheckedRadioButtonRowNum(grid);
                    if (rowIndex != -1) {
                        var row = grid.firstChild.childNodes[rowIndex];
                        window.returnValue = row.childNodes[1].innerText;
                        window.close();
                    } else {
                        window.returnValue = "";
                        window.close();
                    }
                }
            }
           
        </script>

    </div>
    <table border="0" cellpadding="1" cellspacing="0" style="width: 100%; padding-top: 10px;
        padding-left: 10px;">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelCRId" runat="server" Text="CR No " />
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="TextBoxCRId" runat="server"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelCRName" runat="server" Text="CR Name " />
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="TextBoxCRName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelPM" runat="server" Text="PM " meta:resourcekey="LabelPmResource1" />
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TextBoxPM" runat="server"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelSystemName" runat="server" Text="System Name " />
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSystemName" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="ButtonInquiry" runat="server" OnClick="ButtonInquiry_Click" Text="Query" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <span></span>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updatePanelPage" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc1:gridviewpager ID="GridViewPager1" Init_Grid_ID="GridViewCrId" OnBindGrid="BindGrid"
                            runat="server" SetPagerButtonImageStyle="Default" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ButtonInquiry" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updatePanelGridView" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <anthem:GridView ID="GridViewCrId" runat="server" OnRowCreated="GridViewCrId_RowCreated"
                            OnRowDataBound="GridViewCrId_RowDataBound" AutoGenerateColumns="False" Style="border-width: 1px;
                            border-style: Solid; border-collapse: collapse;" EmptyDataText="No data." Font-Size="9pt"
                            GridLines="Horizontal" HorizontalAlign="Left" CssClass="myGV" AllowPaging="True">
                            <HeaderStyle CssClass="GVHeader" HorizontalAlign="Center" />
                            <RowStyle CssClass="GVDataTR" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GVAlterDataTR" />
                            <PagerSettings Visible="False" PageButtonCount="15" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HiddenFieldKey" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CrId") %>' />
                                        <asp:RadioButton runat="server" ID="RadioButtonSelector" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="CR No" DataField="CrId">
                                    <HeaderStyle Width="12%" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        CR Name</HeaderTemplate>
                                    <HeaderStyle Width="30%" />
                                    <ItemTemplate>
                                        <asp:Label ID="LableCrName" runat="server" Text='<%#Server.HtmlDecode(DataBinder.Eval(Container.DataItem, "CrName").ToString()) %> '></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Site" DataField="Site">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="PM" DataField="Pm">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="System Name" DataField="System">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundField>
                            </Columns>
                        </anthem:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ButtonInquiry" />
                        <asp:AsyncPostBackTrigger ControlID="GridViewPager1" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div style="text-align: center">
        <asp:Button ID="ButtonOK" runat="server" Text="Confirm" OnClientClick="confirm_record();return false;"
            Width="80px" />
    </div>
    </form>
</body>
</html>
