<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BasicInformationDetail.ascx.cs"
    Inherits="PMS.PMS.UserControls.BasicInformationDetail" %>
<link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
<link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
<link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">
    function ShowSystemListPopUp() {
        var ddlSite = document.getElementById("<%=dropdownlistSite.ClientID %>");
        var ddlDomain = document.getElementById("<%=dropdownlistDomain.ClientID %>");
        var textPM = document.getElementById("<%=txtPM.ClientID %>");
        var textSystem = document.getElementById("<%=txtSystem.ClientID %>");

        var features = "dialogWidth=1000px;dialogHeight=440px;scrollbars=yes;help=no;resizable=no;status=no;center=yes";
        var url = "../Maintain/SystemListPopUp.aspx?Site=" + EncodeURI(ddlSite.value, false) + "&Domain=" + EncodeURI(ddlDomain.value, false) + "&PM=" + EncodeURI(textPM.value.trim(), false) + "&System=" + EncodeURI(textSystem.value.trim(), false);
        var recData = window.showModalDialog(url, "", features);

        if (recData != null) {
            ddlSite.value = recData.Site;
            ddlDomain.value = recData.SystemDomain;
            //textPM.value = recData.PM.replace(" ",".");
            textSystem.value = recData.SystemBriefName;
        }
    }

    function dropdownlistType_onchange() {
        //通过dropdownlistType的值来决定是否显示lblRelatedCrNo，txtRelatedCrNo
        var ddlType = document.getElementById("<%=dropdownlistType.ClientID %>");
        var trRelatedCrNo = document.getElementById("<%=TrRelatedCrNo.ClientID %>");

        if (ddlType.value == "Service") {
            trRelatedCrNo.style["display"] = "block";
        }
        else {
            trRelatedCrNo.style["display"] = "none";
        }
    }

    function GetNewCrId() {
        var textCRID = document.getElementById("<%=txtCRID.ClientID %>");
        var features = "dialogWidth=700px;dialogHeight=440px;scrollbars=yes;help=no;resizable=no;status=no;center=yes";
        var url = "../Maintain/CRNoInquiry.aspx";
        var receData = window.showModalDialog(url, "", features);
        if (receData != null) {
            textCRID.value = receData;
        }
        return false;
    }

    function SettxtActualStartDateValue(actualStartDate) {
        var textASD = document.getElementById("<%=txtActualStartDate.ClientID %>");
        if (textASD != null && textASD != undefined)
            textASD.value = actualStartDate;
    }

    function SettxtProgressValue(progress) {
        var textProgress = document.getElementById("<%=txtProgress.ClientID %>");
        if (textProgress != null && textProgress != undefined)
            textProgress.value = progress;
    }

    function SettxtTotalManpowerValue(totalManpower) {
        var textTotalManpower = document.getElementById("<%=txtTotalManpower.ClientID %>");
        if (textTotalManpower != null && textTotalManpower != undefined)
            textTotalManpower.value = totalManpower;
    }

    //    function SettxtCloseDateValue(closeDate) {
    //        var textCloseDate = document.getElementById("<%=txtCloseDate.ClientID %>");
    //        if (textCloseDate != null && textCloseDate != undefined)
    //            textCloseDate.value = closeDate;
    //    }


    function relatedItem(strItem) {
        window.location = "ProjectsInformation.aspx?PmsID=" + strItem;
        return false;
    }


    function txtSystem_onblur() {
        var ddlSite = document.getElementById("<%=dropdownlistSite.ClientID %>");
        var ddlDomain = document.getElementById("<%=dropdownlistDomain.ClientID %>");
        var txtSystem = document.getElementById("<%=txtSystem.ClientID %>");
        //调用页面的后台方法，获取version
        WebServiceFun(ddlDomain.value, txtSystem.value, ddlSite.value);

        //PageMethods.GetVersionNewAndOld(ddlDomain.value, txtSystem.value, ddlSite.value, GetVersion);
    }

    //接收后台返回的值，并赋值给相应的控件。
    function GetVersion(result) {
        var txtOldVersion = document.getElementById("<%=txtOldVision.ClientID %>");
        var txtNewVersion = document.getElementById("<%=txtNewVision.ClientID %>");
        var hidFieldOldVersion = document.getElementById("<%=HiddenFieldOldVision.ClientID %>");

        if (result != undefined && result != null) {
            txtOldVersion.value = result.OldVersion;
            if (result.NewVersion != null) {
                txtNewVersion.value = result.NewVersion.trim();
            }

            hidFieldOldVersion.value = result.OldVersion;

        }
        else {
            txtOldVersion.value = "";
            txtNewVersion.value = "";
            hidFieldOldVersion.value = "";
        }
    }


    //显示修改历史
    function ShowChangeHistory() {

        document.getElementById("TDReason").innerHTML
        = document.getElementById("<%= HiddenFieldChangeHistory.ClientID%>").value;

        var currentElement = window.event.srcElement;
        var positionX = CalculateLeft(currentElement);
        var positionY = CalculateTop(currentElement);

        document.getElementById("divReason").style.left = positionX + 10;
        document.getElementById("divReason").style.top = positionY + 20;
        document.getElementById("divReason").style.display = "block";

        SearchIconPositionXX = positionX;
        SearchIconPositionYY = positionY;
    }

    // 隐藏修改历史
    function HideChangeHistory() {

        document.getElementById("divReason").style.display = "none";
    }

    function CalculateTop(currentElement) {
        var positionY = 0;
        while (currentElement) {
            positionY += currentElement.offsetTop;
            currentElement = currentElement.offsetParent;
        }
        return positionY;
    }

    function CalculateLeft(currentElement) {
        var positionX = 0;
        while (currentElement) {
            positionX += currentElement.offsetLeft;
            currentElement = currentElement.offsetParent;
        }
        return positionX;
    }
        
    

</script>

<body>
    <asp:ScriptManagerProxy ID="ScriptManagerProxyBasicInformation" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/PMS/JavaScript/PmsCommonJSFuction.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <div id="DivBasicInformationDetail" style="padding-left: 4px;">
        <table>
            <tr>
                <td style="height: 25px; width: 110%; background-color: aliceblue">
                    <asp:Button ID="ButtonEdit" runat="server" Text="Edit" Width="110px" OnClick="ButtonEdit_Click" />
                    <asp:Button ID="ButtonPending" runat="server" Text="Pending" Width="110px" OnClick="ButtonPending_Click" />
                    <asp:Button ID="ButtonHardColse" runat="server" Text="Hard Close" Width="110px" OnClick="ButtonHardColse_Click" />
                    <asp:Button ID="ButtonCancelled" runat="server" Text="Cancelled" Width="110px" OnClick="ButtonCancelled_Click" />
                    <asp:Button ID="ButtonReactive" runat="server" Text="Reactive" Width="110px" OnClick="ButtonReactive_Click" />
                    <asp:Button ID="ButtonPRelease" runat="server" Text="Partial Release" Width="110px"
                        OnClick="ButtonPRelease_Click" />
                    <asp:Button ID="ButtonRelease" runat="server" Text="Release" Width="110px" OnClick="ButtonRelease_Click" />
                    <%-- <asp:Button ID="Button1" runat="server" Text="Refresh" Width="110px" />--%>
                </td>
            </tr>
        </table>
        <br />
        <table style="padding-top: 16px; padding-left: 24px;" cellpadding="1" cellspacing="0">
            <tr runat="server" id="tr1">
                <td colspan="2" align="right">
                    <asp:Button ID="ButtonOKTop" runat="server" Text="OK" Width="110px" Visible="False"
                        OnClick="ButtonOKTop_Click" />
                    <asp:Button ID="ButtonCancelTop" runat="server" Text="Cancel" Width="110px" Visible="False" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    <asp:Label ID="lblCRID" Text="CR No" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td style="width: 470px">
                    <asp:TextBox ID="txtCRID" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"
                        MaxLength="15"></asp:TextBox>
                </td>
                <td style="width: 10px">
                    <asp:ImageButton ID="ImageButtonSearchCrId" runat="server" OnClientClick="javascript:GetNewCrId();return false;"
                        ImageUrl="~/Style/Images/CrNoSearch.gif" />
                    <%-- <asp:Button ID="ButtonGetCrId" Width="9px" runat="server" Text="&gt;" OnClientClick="javascript:GetNewCrId();return false;" />--%>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblPMSName" Text="CR Name" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPMSName" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"
                        MaxLength="100"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblType" Text="Type" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropdownlistType" runat="server" Width="476px" Enabled="false"
                        onchange="dropdownlistType_onchange()">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr id="TrRelatedCrNo" runat="server" style="display: none">
                <td style="width: 180px">
                    <asp:Label ID="lblRelatedCrNo" Text="Related CR No" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td style="width: 470px">
                    <asp:TextBox ID="txtRelatedCrNo" runat="server" CssClass="UnderLineOnlyTextBox" MaxLength="15"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 10px">
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblDescription" Text="Description" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td style="width: 470px">
                    <asp:TextBox ID="txtDescription" ReadOnly="true" runat="server" CssClass="CrDescriptionReadOnly"
                        MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="width: 10px">
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblStage" Text="Stage" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStage" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblSystem" Text="System" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSystem" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"
                        MaxLength="100"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblCreateDate" Text="Create Date" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreateDate" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblActualSDate" Text="Actual Start Date" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtActualStartDate" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblDueDate" Text="Due Date" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="dateTextBoxDueDate" runat="server" ReadOnly="True" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                    <asp:Image ID="ImageRedFlag" runat="server" ImageUrl="~/Style/Images/redflag.gif"
                        onmouseover="ShowChangeHistory()" onmouseout="HideChangeHistory()" Visible="false"
                        Width="15px" Height="15px" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="PanelDueDateChangeReason" runat="server" Visible="false">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td style="width: 212px;">
                                    <asp:Label ID="Label1" Text="Due Date Change Reason Type" runat="server" CssClass="labelleft110"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDueDateChangeReasonType" runat="server" Width="476px" CssClass="NoLineDisplay">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 182px;">
                                    <asp:Label ID="Label2" Text="Due Date Change Reason" runat="server" CssClass="labelleft110"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDueDateChangeReason" runat="server" CssClass="NoLineDisplay"
                                        MaxLength="500"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblReleaseDate" Text="Release Date" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReleaseDate" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblCloseDate" Text="Close Date" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCloseDate" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblDomain" Text="System Domain" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropdownlistDomain" runat="server" Width="476px" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblPriority" Text="Priority" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropdownlistPriority" runat="server" Width="476px" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblSite" Text="Site" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropdownlistSite" runat="server" Width="476px" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblImpactSite" Text="Impact Site" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropdownlistImpactSite" runat="server" Width="476px" Enabled="false"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblPlanStart" Text="Plan Start Date" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="dateTextBoxPlanStart" runat="server" ReadOnly="True" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblProgress" Text="Progress" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProgress" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblTotalManpower" Text="Total Manpower(H)" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotalManpower" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="LabelDuration" Text="Duration" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDuration" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="labelNeedSTP" runat="server" Text="Need STP" Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="RadioButtonNeedSTPYes" Text="Yes" GroupName="RadioButtonGroupNeedSTP"
                        runat="server" Enabled="False" />
                    <asp:RadioButton ID="RadioButtonNeedSTPNo" Text="No" GroupName="RadioButtonGroupNeedSTP"
                        runat="server" Enabled="False" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="labelNeedSTC" runat="server" Text="Need STC" Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="RadioButtonNeedSTCYes" Text="Yes" GroupName="RadioButtonGroupNeedSTC"
                        runat="server" Enabled="False" />
                    <asp:RadioButton ID="RadioButtonNeedSTCNo" Text="No" GroupName="RadioButtonGroupNeedSTC"
                        runat="server" Enabled="False" />
                </td>
                <td>
                </td>
            </tr>
             <tr style="display:none">
                <td>
                    <asp:Label ID="labelVB2Net" runat="server" Text="VB2Net" Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="RadioButtonVB2NetYes" Text="Yes" GroupName="RadioButtonGroupNeedVB2Net"
                        runat="server" Enabled="False" />
                    <asp:RadioButton ID="RadioButtonVB2NetNo" Text="No" Checked="true" GroupName="RadioButtonGroupNeedVB2Net"
                        runat="server" Enabled="False" />
                </td>
                <td>
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="labelCodeReview" runat="server" Text="Code Review" Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="RadioButtonCodeReviewYes" Text="Yes" GroupName="RadioButtonGroupCodeReview"
                        runat="server" Enabled="False" />
                    <asp:RadioButton ID="RadioButtonCodeReviewNo" Text="No" Checked="true" GroupName="RadioButtonGroupCodeReview"
                        runat="server" Enabled="False" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="labelSelfTesting" runat="server" Text="Self Testing Audit" Width="120px"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="RadioButtonSelfTestingAuditYes" Text="Yes" GroupName="RadioButtonGroupSelfTesting"
                        runat="server" Enabled="False" />
                    <asp:RadioButton ID="RadioButtonSelfTestingAuditNo" Text="No" Checked="true" GroupName="RadioButtonGroupSelfTesting"
                        runat="server" Enabled="False" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblNewVision" Text="New Version" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNewVision" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblOldVision" Text="Old Version" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOldVision" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"
                        MaxLength="20"></asp:TextBox>
                    <asp:HiddenField ID="HiddenFieldOldVision" runat="server" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblPM" Text="PM" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPM" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBox"
                        MaxLength="100"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblSD" Text="SD" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSD" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"
                        MaxLength="100"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblQA" Text="QA" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQA" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"
                        MaxLength="200"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblSE" Text="SE" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSE" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"
                        MaxLength="500"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 180px">
                    <asp:Label ID="lblReleaseItem" Text="Cooperation CR" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <asp:Panel ID="panelItem" runat="server" CssClass="UnderLineOnlyTextBox">
                    </asp:Panel>
                </td>
                <td>
                </td>
            </tr>
            <tr runat="server" id="tr2">
                <td colspan="2" align="right">
                    <%--<asp:UpdatePanel ID="updatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                    <asp:Button ID="ButtonOKUnder" runat="server" Text="OK" Width="110px" Visible="False"
                        OnClick="ButtonOKUnder_Click" />
                    <asp:Button ID="ButtonCancelUnder" runat="server" Text="Cancel" Width="110px" Visible="False" />
                    <%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                            <asp:AsyncPostBackTrigger ControlID="btnOKTop" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelTop" />
                            <asp:AsyncPostBackTrigger ControlID="btnOKUnder" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelUnder" />
                        </Triggers>
                        </asp:UpdatePanel>  --%>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div style="z-index: 101; position: absolute; display: none" id="divReason">
            <table border="0" cellspacing="0" cellpadding="0" width="220px">
                <tr>
                    <td style="padding-left: 5px; padding-right: 5px" id="TDReason" class="DivDetailBorder"
                        bgcolor="#feffdf" width="220px">
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenFieldChangeHistory" runat="server" />
        </div>
    </div>

    <script type="text/javascript">
        //        var tabContainer1 = parent.document.getElementById("TabContainer1_body");
        //        tabContainer1.style.height = document.body.scrollHeight + 100;
        //        tabContainer1.style.width = document.body.scrollWidth + 200;
    </script>

</body>
