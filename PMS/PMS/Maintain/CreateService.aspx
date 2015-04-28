<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateService.aspx.cs"
    Inherits="PMS.PMS.Maintain.CreateService" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Service</title>
    <link href="../../SysFrame/styles/FrameStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="../JavaScript/jquery/uploadify.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function ButtonSave_ClientClick() {
            // check items is empty or not

            
            var serviceName = document.getElementById("<%=textboxServiceName.ClientID %>");
            var dropdownlistPriority = document.getElementById("<%=dropdownlistPriority.ClientID %>");
            var dropdownlistTeamDomain = document.getElementById("<%=dropdownlistTeamDomain.ClientID %>");
            var dropdownlistSite = document.getElementById("<%=dropdownlistSite.ClientID %>");
            var dropdownlistSystem = document.getElementById("<%=dropdownlistSystem.ClientID %>");
            var textBoxDescription = document.getElementById("<%=TextBoxDescription.ClientID %>");

            if (serviceName.value == "") {
                alert("Service Name is empty");
                event.returnValue = false;
                return false;
            }

            if (dropdownlistPriority.value == "") {
                alert("Priority is empty");
                event.returnValue = false;
                return false;
            }

            if (dropdownlistTeamDomain.value == "") {
                alert("Team Domain is empty");
                event.returnValue = false;
                return false;
            }

            if (dropdownlistSite.value == "") {
                alert("Site is empty");
                event.returnValue = false;
                return false;

            }

            if (dropdownlistSystem.value == "") {
                alert("System is empty");
                event.returnValue = false;
                return false;

            }
            

            if (textBoxDescription.value == "") {
                alert("Description is empty");
                event.returnValue = false;
                return false;

            }

            
            return true;
        }

        function Refresh() {
            // 调用父窗口的onLoad()

            window.top.onLoad();
        }
        
        
        
    </script>

</head>
<body>
    <form id="formCreateService" runat="server">
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
                    <asp:Label ID="labelTitle" runat="server" CssClass="HeadLabel" Text="Create Service"
                        ForeColor="SteelBlue" Font-Bold="True" Font-Size="11pt" Width="300px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth" style="height: 4px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelPmsName" runat="server" Text="Service Name" Width="100px"></asp:Label>
                    <asp:Label ID="labelPmsNameMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:TextBox ID="textboxServiceName" runat="server" Width="208px" MaxLength="100"></asp:TextBox>
                    <asp:Label ID="labelPmsNameBank" runat="server" Text="  " Width="50px"></asp:Label>
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
                    <asp:Label ID="labelTeamDomain" runat="server" Text="TeamDomain" Width="100px"></asp:Label>
                    <asp:Label ID="labelTeamDomainMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="dropdownlistTeamDomain" runat="server" Width="214px" AutoPostBack="true"
                        OnSelectedIndexChanged="dropdownlistTeamDomain_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="labelTeamDomainBank" runat="server" Text="  " Width="50px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth">
                    <asp:Label ID="labelSite" runat="server" Text="Site" Width="100px"></asp:Label>
                    <asp:Label ID="labelSiteMark" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <asp:DropDownList ID="dropdownlistSite" runat="server" Width="214px" AutoPostBack="true"
                        OnSelectedIndexChanged="dropdownlistSite_SelectedIndexChanged">
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
                    <asp:DropDownList ID="dropdownlistSystem" runat="server" Width="214px">
                    </asp:DropDownList>
                    <asp:Label ID="labelSystemBank" runat="server" Text="  " Width="15px"></asp:Label>
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" Width="70px" OnClientClick="ButtonSave_ClientClick();"
                        OnClick="ButtonSave_Click" />
                    <asp:Label ID="labelOKBank" runat="server" Text="  " Width="15px"></asp:Label>
                    <asp:Button ID="buttonCancel" runat="server" Text="Exit" Width="70px" OnClientClick="JavaScript:window.close();" />
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth" align="left">
                    <%--<asp:Label ID="labelServiceDesc" runat="server" Text="Service Desc" Width="100px"></asp:Label>
                    <asp:Label ID="label2" runat="server" ForeColor="red" Text="*"></asp:Label>
                    <br/>
                    <br/>--%>
                    <asp:TextBox ID="TextBoxDescription" runat="server" Width="520px" Height="220px"
                        TextMode="MultiLine" CssClass="TextBoxDescription" />
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth" align="left">
                    <asp:FileUpload ID="FileUpload" runat="server" Width="428px" />
                    <asp:Label ID="labelUploadBank" runat="server" Text="  " Width="18px"></asp:Label>
                    <asp:Button ID="ButtonUpload" runat="server" Text="Upload" OnClick="ButtonUpload_Click" />
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth" align="left">
                    <asp:GridView ID="GridViewResult" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                        Width="495px" AllowPaging="false" ShowFooter="false" OnRowDataBound="GridViewResult_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="linkFileName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="deleteButton" runat="server" ImageUrl="~/Images/delete.gif"
                                        OnClick="Button_delete" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="SpaceTdWidth">
                </td>
                <td class="TDWidth" align="left">
                    <br />
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
