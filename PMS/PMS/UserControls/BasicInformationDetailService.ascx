<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BasicInformationDetailService.ascx.cs"
    Inherits="PMS.PMS.UserControls.BasicInformationDetailService" %>
<link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
<link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
<link href="../../SysFrame/styles/GlobalStyle.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">

    function ShowSystemListPopUpService() {
        var ddlSite = document.getElementById("<%=dropdownlistSite.ClientID %>");
        var ddlDomain = document.getElementById("<%=dropdownlistDomain.ClientID %>");
        var textPm = document.getElementById("<%=txtPM.ClientID %>");
        var textSystem = document.getElementById("<%=txtSystem.ClientID %>");

        var features = "dialogWidth=1000px;dialogHeight=440px;scrollbars=yes;help=no;resizable=no;status=no;center=yes";
        var url = "../Maintain/SystemListPopUp.aspx?Site=" + EncodeURI(ddlSite.value, false) + "&Domain=" + EncodeURI(ddlDomain.value, false) + "&PM=" + EncodeURI(textPm.value.trim(), false) + "&System=" + EncodeURI(textSystem.value.trim(), false);
        var recData = window.showModalDialog(url, "", features);

        if (recData != null) {
            ddlSite.value = recData.Site;
            ddlDomain.value = recData.SystemDomain;
            textSystem.value = recData.SystemBriefName;
        }
    }

    function GetNewCrIdService() {
        var textRelatedCrId = document.getElementById("<%=txtRelatedCrNo.ClientID %>");
        var features = "dialogWidth=700px;dialogHeight=440px;scrollbars=yes;help=no;resizable=no;status=no;center=yes";
        var url = "../Maintain/CRNoInquiry.aspx";
        var receData = window.showModalDialog(url, "", features);
        if (receData != null) {
            textRelatedCrId.value = receData;
        }
        return false;
    }


    function SettxtTotalManpowerValueService(totalManpower) {
        var textTotalManpower = document.getElementById("<%=txtTotalManpower.ClientID %>");
        if (textTotalManpower != null && textTotalManpower != undefined)
            textTotalManpower.value = totalManpower;
    }

    function SettxDurationValueService(duration) {
        var textduration = document.getElementById("<%=txtDuration.ClientID %>");
        if (textduration != null && textduration != undefined)
            textduration.value = duration;
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
                        Enabled="false" />
                    <asp:Button ID="ButtonRelease" runat="server" Text="Release" Width="110px" OnClick="ButtonRelease_Click" />
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
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCRID" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"
                                    MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px">
                </td>
            </tr>
            <tr>
                <td style="height: 5px">
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="lblPMSName" Text="CR Name" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPMSName" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBox"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="lblType" Text="Type" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtType" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBoxHalf">
                                </asp:TextBox>
                            </td>
                            <td style="width: 20px">
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="lblRelatedCrNo" Text="Related CR No" runat="server" CssClass="labelleft110"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRelatedCrNo" runat="server" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="15" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonSearchCrId" runat="server" Visible="false" OnClientClick="GetNewCrIdService();return false;"
                        ImageUrl="~/Style/Images/CrNoSearch.gif" />
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="lblStage" Text="Stage" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStage" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"></asp:TextBox>
                            </td>
                            <td style="width: 20px">
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="lblCreateDate" Text="Create Date" runat="server" CssClass="labelleft110"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreateDate" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="lblDomain" Text="System Domain" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="dropdownlistDomain" runat="server" Width="167px" Enabled="false">
                                </asp:DropDownList>
                                <td style="width: 21px">
                                </td>
                                <td style="width: 105px">
                                    <asp:Label ID="lblPriority" Text="Priority" runat="server" CssClass="labelleft110"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropdownlistPriority" runat="server" Width="167px" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="lblSite" Text="Site" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="dropdownlistSite" runat="server" Width="167px" Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 21px">
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="lblImpactSite" Text="Impact Site" runat="server" CssClass="labelleft110"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="dropdownlistImpactSite" runat="server" Width="167px" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="lblSystem" Text="System" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtSystem" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="width: 21px">
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="lblReleaseDate" Text="Release Date" runat="server" CssClass="labelleft110"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReleaseDate" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="LabelDueDate" Text="Due Date" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBoxDueDate" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="10"></asp:TextBox>
                            </td>
                            <td style="width: 21px">
                                <asp:Image ID="ImageRedFlag" runat="server" ImageAlign="Left" ImageUrl="~/Style/Images/redflag.gif"
                                    onmouseover="ShowChangeHistory()" onmouseout="HideChangeHistory()" Visible="false"
                                    Width="15px" Height="15px" />
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="LabelCloseDate" Text="Close Date" runat="server" CssClass="labelleft110"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxCloseDate" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="PanelDueDateChangeReason" runat="server" Visible="false">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td style="width: 212px;">
                                    <asp:Label ID="Label2" Text="Due Date Change Reason Type" runat="server" CssClass="labelleft110"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDueDateChangeReasonType" runat="server" Width="476px" CssClass="NoLineDisplay">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr style="height: 2px">
                            </tr>
                            <tr>
                                <td style="width: 182px;">
                                    <asp:Label ID="Label3" Text="Due Date Change Reason" runat="server" CssClass="labelleft110"></asp:Label>
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
                <td style="width: 140px">
                    <asp:Label ID="lblTotalManpower" Text="Total Manpower(H)" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtTotalManpower" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBoxHalf"></asp:TextBox>
                            </td>
                            <td style="width: 21px">
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="LabelDuration" Text="Duration" runat="server" CssClass="labelleft110"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDuration" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBoxHalf"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="lblPM" Text="PM" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPM" runat="server" ReadOnly="true" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="width: 21px">
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="lblSD" Text="SD" runat="server" CssClass="labelleft110"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSD" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <asp:Label ID="lblQA" Text="QA" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtQA" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td style="width: 21px">
                            </td>
                            <td style="width: 105px">
                                <asp:Label ID="lblSE" Text="SE" runat="server" CssClass="labelleft110"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSE" ReadOnly="true" runat="server" CssClass="UnderLineOnlyTextBoxHalf"
                                    MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 141px">
                    <asp:Label ID="lblDescription" Text="Service Description" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td style="width: 470px">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDescription" ReadOnly="true" runat="server" TextMode="MultiLine"
                                    CssClass="MutilineTextBoxReadOnly" MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px">
                </td>
            </tr>
            <tr>
                <td style="width: 141px">
                    <asp:Label ID="lblResolveDescription" Text="Resolve Description" runat="server" CssClass="labelleft110"></asp:Label>
                </td>
                <td style="width: 470px">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtResolveDescription" ReadOnly="true" runat="server" TextMode="MultiLine"
                                    CssClass="MutilineTextBoxReadOnly" MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px">
                </td>
            </tr>
            <tr runat="server" id="tr2">
                <td colspan="2" align="right">
                    <asp:Button ID="ButtonOKUnder" runat="server" Text="OK" Width="110px" Visible="False"
                        OnClick="ButtonOKUnder_Click" />
                    <asp:Button ID="ButtonCancelUnder" runat="server" Text="Cancel" Width="110px" Visible="False" />
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
</body>
