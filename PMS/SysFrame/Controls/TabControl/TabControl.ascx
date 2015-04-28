<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabControl.ascx.cs" Inherits="Prototype_Controls_TabControl" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
            <td>
                <table  cellpadding="0" cellspacing="0" border="0">
                <tr >
                <!--Step1-7-->
                <td>
                    <asp:Button ID="ButtonMember" CssClass="StaticTab" OnClientClick="return CheckValidate(0);" CommandName="0" runat="server" Text="Member" OnClick="TabControl_SelectedChanged" />
                </td>
                <td>
                    <asp:Button ID="ButtonEducation" OnClientClick="return CheckValidate(1);" CssClass="StaticTab" runat="server" CommandName="1" Text="Education" OnClick="TabControl_SelectedChanged" />
                </td>
                <td>
                    <asp:Button ID="ButtonExperience"  OnClientClick="return CheckValidate(2);" CssClass="StaticTab" runat="server" CommandName="2" Text="Experience" OnClick="TabControl_SelectedChanged" />
                </td>
                <td>
                    <asp:Button ID="ButtonLicense" OnClientClick="return CheckValidate(3);" CssClass="StaticTab" runat="server" CommandName="3" Text="License" OnClick="TabControl_SelectedChanged" />
                </td>
                <td>
                    <asp:Button ID="ButtonAutobiography" OnClientClick="return CheckValidate(4);" CssClass="StaticTab101" runat="server" CommandName="4" Text="Autobiography" OnClick="TabControl_SelectedChanged" />
                </td>
                <td>
                    <asp:Button ID="ButtonOther" OnClientClick="return CheckValidate(5);" CssClass="StaticTab" runat="server" CommandName="5" Text="Other" OnClick="TabControl_SelectedChanged" />
                 </td>
                <td>
                    <asp:Button ID="ButtonPreview" OnClientClick="return CheckValidate(6);" CssClass="StaticTab" runat="server" CommandName="6" Text="Preview" OnClick="TabControl_SelectedChanged" />
                 </td>
                 <td style="width:100%;background-image:url(../images/tab.GIF);background-repeat:repeat-x;" valign="bottom"></td>
                </tr>
                <tr style="background-color:#f5f5f5;">
                <!--Step1-7-->
                <td align="center" style="padding-top:5px;padding-bottom:5px;">
                    <asp:Image ID="ImageStep1" Visible="false" runat="server" ImageUrl="../images/star.GIF" />
                    <asp:Label ID="LabelStep1" runat="server" Text="Step1"></asp:Label>
                </td>
                <td align="center" style="padding-top:5px;padding-bottom:5px;">
                    <asp:Image ID="ImageStep2" Visible="false" runat="server" ImageUrl="../images/star.GIF" />
                    <asp:Label ID="LabelStep2" runat="server" Text="Step2"></asp:Label>
                </td>
                <td align="center" style="padding-top:5px;padding-bottom:5px;">
                    <asp:Image ID="ImageStep3" Visible="false" runat="server" ImageUrl="../images/star.GIF" />
                    <asp:Label ID="LabelStep3" runat="server" Text="Step3"></asp:Label>
                </td>
                <td align="center" style="padding-top:5px;padding-bottom:5px;">
                    <asp:Image ID="ImageStep4" Visible="false" runat="server" ImageUrl="../images/star.GIF" />
                    <asp:Label ID="LabelStep4" runat="server" Text="Step4"></asp:Label>
                </td>
                <td align="center" style="padding-top:5px;padding-bottom:5px;">
                    <asp:Image ID="ImageStep5" Visible="false" runat="server" ImageUrl="../images/star.GIF" />
                    <asp:Label ID="LabelStep5" runat="server" Text="Step5"></asp:Label>
                </td>
                <td align="center" style="padding-top:5px;padding-bottom:5px;">
                    <asp:Image ID="ImageStep6" Visible="false" runat="server" ImageUrl="../images/star.GIF" />
                    <asp:Label ID="LabelStep6" runat="server" Text="Step6"></asp:Label>
                </td>
                <td align="center" style="padding-top:5px;padding-bottom:5px;">
                    <asp:Image ID="ImageStep7" Visible="false" runat="server" ImageUrl="../images/star.GIF" />
                    <asp:Label ID="LabelStep7" runat="server" Text="Step7"></asp:Label>
                </td>
                <td></td>
                </tr>
                </table>
            </td>
            <tr>
            <td>
            <!--??-->
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <uc2:DynamicUserControl id="DynamicUserControl1" runat="server"></uc2:DynamicUserControl>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <uc2:DynamicUserControl id="DynamicUserControl2" runat="server"></uc2:DynamicUserControl>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <uc2:DynamicUserControl id="DynamicUserControl3" runat="server">
                        </uc2:DynamicUserControl>
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        <uc2:DynamicUserControl id="DynamicUserControl4" runat="server">
                        </uc2:DynamicUserControl>
                    </asp:View>
                    <asp:View ID="View5" runat="server">
                        <uc2:DynamicUserControl id="DynamicUserControl5" runat="server">
                        </uc2:DynamicUserControl>
                    </asp:View>
                    <asp:View ID="View6" runat="server">
                        <uc2:DynamicUserControl id="DynamicUserControl6" runat="server">
                        </uc2:DynamicUserControl>
                    </asp:View>
                    <asp:View ID="View7" runat="server">
                        <uc2:DynamicUserControl id="DynamicUserControl7" runat="server">
                        </uc2:DynamicUserControl>
                    </asp:View>
                </asp:MultiView>
            </td>
            </tr>
        </table>
        <asp:Button ID="ButtonSave" style="display:none;" runat="server" Text="Button" OnClick="ButtonSave_Click" />
        <input type="hidden" id="toTabIndex" name="toTabIndex" runat="server" />
        <input type="hidden" id="SaveMessage" name="SaveMessage" runat="server" /> 
        </ContentTemplate>
</asp:UpdatePanel>
