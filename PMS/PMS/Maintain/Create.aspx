<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="PMS.PMS.Maintain.Create1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/SysFrame/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>
<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>Create CR</title>

    <script language="javascript" type="text/javascript" src="../../SysFrame/JavaScript/FrameMain.js"></script>

    <script type="text/javascript" language="javascript" src="../Javascript/pms.js"></script>

    <script language="javascript" type="text/javascript">

        //Create ButtonSave
        function CreateButtonSave_ClientClick() {
            return true;
        }

        function ShowSystemListPopUp() {
            var ddlSite = document.getElementById("<%=dropdownlistSite.ClientID %>");
            var ddlDomain = document.getElementById("<%=dropdownlistDomain.ClientID %>");
            var txtPM = document.getElementById("<%=textboxPM.ClientID %>");
            var txtSystem = document.getElementById("<%=textboxSystem.ClientID %>");

            var features = "dialogWidth=1000px;dialogHeight=440px;scrollbars=yes;help=no;resizable=no;status=no;center=yes";
            var url = "SystemListPopUp.aspx?Site=" + EncodeURI(ddlSite.value, false) + "&Domain=" + EncodeURI(ddlDomain.value, false) + "&PM=" + EncodeURI(txtPM.value.trim(), false) + "&System=" + EncodeURI(txtSystem.value.trim(), false);

            var recData = window.showModalDialog(url, "", features);

            if (recData != null) {
                ddlSite.value = recData.Site;
                ddlDomain.value = recData.SystemDomain;
                //txtPM.value = recData.PM.replace(" ",".");
                txtSystem.value = recData.SystemBriefName;
                //调用页面的后台方法，获取version
                // PageMethods.GetVersionNewAndOld(recData.SystemDomain, recData.SystemBriefName, GetVersion);
            }
        }


        function SetNeedSTCSTP() {
          //  var ddlDomain = document.getElementById("<%=dropdownlistDomain.ClientID %>");
            var planStartDay = document.getElementById("<%=dateTextBoxPlanStartDate.ClientID %>");
            var dueDate = document.getElementById("<%=dateTextBoxDueDate.ClientID %>");

            if (dueDate.value != null || planStartDay.value != null) {
                //调用页面的后台方法，获取version
                PageMethods.SetStcStp("", planStartDay.value, dueDate.value, GetNeedSTCSTPIsNeed);
            }
        }

        function GetNeedSTCSTPIsNeed(obj) {
            if (obj == "Y") {
                var radioNeedSTCYes = document.getElementById("<%=RadioButtonNeedSTCYes.ClientID %>");
                var radioNeedSTCNo = document.getElementById("<%=RadioButtonNeedSTCNo.ClientID %>");
                var radioNeedSTPYes = document.getElementById("<%=RadioButtonNeedSTPYes.ClientID %>");
                var radioNeedSTPNo = document.getElementById("<%=RadioButtonNeedSTPNo.ClientID %>");

                radioNeedSTCYes.checked = true;
                radioNeedSTCNo.checked = false;
                radioNeedSTPYes.checked = true;
                radioNeedSTPNo.checked = false;
            }

        }

        //接收后台返回的值，并赋值给相应的控件。
        function GetVersion(result) {
            var txtOldVersion = document.getElementById("<%=textboxOldVersion.ClientID %>");
            var txtNewVersion = document.getElementById("<%=textboxNewVersion.ClientID %>");
            var hidFieldOldVersion = document.getElementById("<%=HiddenFieldOldVersion.ClientID %>");

            if (result != undefined && result != null) {
                txtOldVersion.value = result.OldVersion;
                if (result.NewVersion != null) {
                    txtNewVersion.value = result.NewVersion.trim();
                }
                
                hidFieldOldVersion.value = result.OldVersion;
                //设置NeedSTC STP选中状态
                SetNeedSTCSTP(result.BugFreeModule);
            }
            else {
                txtOldVersion.value = "";
                txtNewVersion.value = "";
                hidFieldOldVersion.value = "";
            }
        }

        function textboxSystem_onblur() {
            var ddlSite = document.getElementById("<%=dropdownlistSite.ClientID %>");
            var ddlDomain = document.getElementById("<%=dropdownlistDomain.ClientID %>");
            var txtPM = document.getElementById("<%=textboxPM.ClientID %>");
            var txtSystem = document.getElementById("<%=textboxSystem.ClientID %>");
            var planStartDay = document.getElementById("<%=dateTextBoxPlanStartDate.ClientID %>");
            var dueDate = document.getElementById("<%=dateTextBoxDueDate.ClientID %>");
            //调用页面的后台方法，获取version
            PageMethods.GetVersionNewAndOld(ddlDomain.value, txtSystem.value, ddlSite.value, planStartDay.value, dueDate.value, GetVersion);
        }

        
        
    </script>

    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formCreate" runat="server">
    <asp:ScriptManager ID="ScriptManagerCreate" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="~/PMS/JavaScript/PmsCommonJSFuction.js" />
        </Scripts>
    </asp:ScriptManager>
    <div>
        <table class="MyTable" width="500px">
            <tr>
                <td class="SpaceTdWidth" style="height: 8px; width: 25px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="width: 25px">
                </td>
                <td>
                    <asp:Label ID="labelTitle" runat="server" CssClass="HeadLabel" Text="Create CR" ForeColor="SteelBlue"
                        Font-Bold="True" Font-Size="11pt" Width="300px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="height: 4px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="width: 25px">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelCrNo" runat="server" Text="CR No" Width="100px" Visible="false"></asp:Label>
                    <asp:Label ID="labelCrNoMark" runat="server" ForeColor="red" Text="*" Visible="false"></asp:Label>
                    <asp:TextBox ID="textboxCrNo" runat="server" Width="144px" MaxLength="15" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelPmsName" runat="server" Text="CR Name" Width="100px"></asp:Label>
                    <asp:Label ID="labelPmsNameMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="textboxPmsName" runat="server" Width="210px" MaxLength="100"></asp:TextBox>
                    <asp:Label ID="labelPmsNameBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelType" runat="server" Text="Type" Width="100px"></asp:Label>
                    <asp:Label ID="labelTypeMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="dropdownlistType" runat="server" Width="214px" OnSelectedIndexChanged="dropdownlistType_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="labelTypeBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelRelatedCrNo" runat="server" Text="Related CR No" Width="100px"
                        Visible="false"></asp:Label>
                    <asp:Label ID="labelRelatedCrNoMark" runat="server" ForeColor="red" Text="*" Visible="false"></asp:Label>
                    <asp:TextBox ID="textboxRelatedCrNo" runat="server" Width="210px" MaxLength="15"
                        Visible="false" AutoPostBack="True" 
                        ontextchanged="textboxRelatedCrNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="labelRelatedCrNoBank" runat="server" Text="  " Width="50px" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelPriority" runat="server" Text="Priority" Width="100px"></asp:Label>
                    <asp:Label ID="labelPriorityMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="dropdownlistPriority" runat="server" Width="214px">
                    </asp:DropDownList>
                    <asp:Label ID="labelPriorityBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelPlanStartDate" runat="server" Text="Plan Start Date" Width="100px"></asp:Label>
                    <%--<span style="color: red">*</span>--%>
                    <asp:Label ID="labelPlanStartDateMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <TW:DateTextBox ID="dateTextBoxPlanStartDate" Width="208px" runat="server" IsDisplayTime="false"
                        Language="English"  ></TW:DateTextBox>
                    <%--<asp:TextBox ID="textBoxDueDate" Width="144px" runat="server" 
                        AutoPostBack="true" ontextchanged="textBoxDueDate_TextChanged" ></asp:TextBox>--%>
                    <asp:Label ID="labelPlanStartDateBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelDueDate" runat="server" Text="Due Date" Width="100px"></asp:Label>
                    <asp:Label ID="labelDueDateMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <TW:DateTextBox ID="dateTextBoxDueDate" Width="208px" runat="server" IsDisplayTime="false"
                        Language="English" >
                    </TW:DateTextBox>
                    <%--<asp:TextBox ID="textBoxDueDate" Width="144px" runat="server" 
                        AutoPostBack="true" ontextchanged="textBoxDueDate_TextChanged" ></asp:TextBox>--%>
                    <asp:Label ID="labelDueDateBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelDomain" runat="server" Text="System Domain" Width="100px"></asp:Label>
                    <asp:Label ID="labelDomainMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="dropdownlistDomain" runat="server" Width="214px">
                    </asp:DropDownList>
                    <asp:Label ID="labelDomainBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelSite" runat="server" Text="Site" Width="100px"></asp:Label>
                    <asp:Label ID="labelSiteMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="dropdownlistSite" runat="server" Width="214px">
                    </asp:DropDownList>
                    <asp:Label ID="labelSiteBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelSystem" runat="server" Text="System" Width="100px"></asp:Label>
                    <asp:Label ID="labelSystemMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="textboxSystem" runat="server" Width="210px" MaxLength="100" onblur="textboxSystem_onblur();"></asp:TextBox>
                    <asp:Label ID="labelSystemBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <%--            <tr>
                <td class="SpaceTdWidth"></td>
                <td class="TDWidth">                                        
                    <asp:Label ID="labelSite" runat="server" Text="Site" Width="100px" ></asp:Label>
                    <asp:Label ID="labelSiteMark" runat="server" ForeColor="red" Text="*"></asp:Label> 
                    <asp:DropDownList ID="dropdownlistSite" runat="server" Width="214px" ></asp:DropDownList>
                    <asp:Label ID="labelSiteBank" runat ="server" Text="  " Width="50px" ></asp:Label>                                                         
                </td>
            </tr>--%>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelImpactSite" runat="server" Text="ImpactSite" Width="100px"></asp:Label>
                    <asp:Label ID="labelImpactSiteMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="dropdownlistImpactSite" runat="server" Width="214px">
                    </asp:DropDownList>
                    <asp:Label ID="labelImpactSiteBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelNeedSTP" runat="server" Text="Need STP" Width="100px"></asp:Label>
                    <asp:Label ID="labelNeedSTPMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:RadioButton ID="RadioButtonNeedSTPYes" Text="Yes" GroupName="RadioButtonGroupNeedSTP"
                        runat="server" />
                    <asp:RadioButton ID="RadioButtonNeedSTPNo" Text="No" Checked="true" GroupName="RadioButtonGroupNeedSTP"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelNeedSTC" runat="server" Text="Need STC" Width="100px"></asp:Label>
                    <asp:Label ID="labelNeedSTCMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:RadioButton ID="RadioButtonNeedSTCYes" Text="Yes" GroupName="RadioButtonGroupNeedSTC"
                        runat="server" />
                    <asp:RadioButton ID="RadioButtonNeedSTCNo" Text="No" Checked="true" GroupName="RadioButtonGroupNeedSTC"
                        runat="server" />
                </td>
            </tr>            
              <tr style="display:none">
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelVB2Net" runat="server" Text="VB2Net" Width="100px"></asp:Label>
                    <asp:Label ID="labelVB2NetMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:RadioButton ID="RadioButtonVB2NetYes" Text="Yes" GroupName="RadioButtonGroupVB2Net"
                        runat="server" />
                    <asp:RadioButton ID="RadioButtonVB2NetNo" Text="No" Checked="true" GroupName="RadioButtonGroupVB2Net"
                        runat="server" />
                </td>
            </tr>            
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelPM" runat="server" Text="PM" Width="100px"></asp:Label>
                    <asp:Label ID="labelPMMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="textboxPM" runat="server" Width="210px" MaxLength="50" ></asp:TextBox>
                    <asp:Label ID="labelPMBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <%--            <tr>
                <td class="SpaceTdWidth"></td>
                <td class="TDWidth">                                        
                    <asp:Label ID="labelPlanStartDate" runat="server" Text="Plan Start Date" Width="100px" ></asp:Label>
                    <asp:Label ID="labelPlanStartDateMark" runat="server" ForeColor="red" Text="*"></asp:Label> 
                    <TW:DateTextBox ID="dateTextBoxPlanStartDate" Width="208px" runat="server"  
                        IsDisplayTime="false" Language="English" AutoPostBack="true" ></TW:DateTextBox>                     
                    <asp:Label ID="labelPlanStartDateBank" runat ="server" Text="  " Width="50px"></asp:Label>                                                         
                </td>
            </tr>--%>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelOldVersion" runat="server" Text="Old Version" Width="100px"></asp:Label>
                    <asp:Label ID="labelOldVersionMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="textboxOldVersion" runat="server" Width="210px" MaxLength="20" Enabled="false"></asp:TextBox>
                    <asp:Label ID="labelOldVersionBank" runat="server" Text="  " Width="50px"></asp:Label>
                    <asp:HiddenField ID="HiddenFieldOldVersion" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelNewVersion" runat="server" Text="New Version" Width="100px"></asp:Label>
                    <asp:Label ID="labelNewVersionMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="textboxNewVersion" runat="server" Width="210px" MaxLength="20"></asp:TextBox>
                    <asp:Label ID="labelNewVersionBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelDescription" runat="server" Text="Description" Width="100px"></asp:Label>
                    <asp:Label ID="labelDescriptionMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="textboxDescription" runat="server" Width="210px" MaxLength="500"
                        Height="40px" TextMode="MultiLine" BorderStyle="solid"></asp:TextBox>
                    <asp:Label ID="labelDescriptionBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelPesFile" runat="server" Text="PES File" Width="112px"></asp:Label>                   
                    <input id="pesUpload" runat="server" type="file" style="width: 214px" onkeydown="JavaScript:KeyPressForbidden(event);"
                        oncopy="return false;" oncut="return false;" onpaste="return false" />
                    <asp:Label ID="labelPesFileBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr> --%>
            <tr>
                <td class="SpaceTdWidth" style="height: 12px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth" align="left">
                    <asp:Label ID="labelBank" runat="server" Text="  " Width="80px"></asp:Label>
                    <asp:Button ID="buttonSave" runat="server" Text="Save" Width="80px" OnClientClick="return CreateButtonSave_ClientClick();"
                        OnClick="buttonSave_Click" />
                    <asp:Label ID="labelOKBank" runat="server" Text="  " Width="30px"></asp:Label>
                    <%-- <asp:Button ID="buttonCancel" runat="server" Text="Exit" Width="80px" OnClientClick="JavaScript:window.close();" />--%>
                    <asp:Button ID="buttonCancel" runat="server" Text="Exit" Width="80px" OnClientClick="JavaScript:window.close();" />
                    <asp:Label ID="labelCancelBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="height: 22px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="height: 22px">
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
