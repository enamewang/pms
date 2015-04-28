<%@ Control Language="C#" AutoEventWireup="true" Codebehind="MyTaskDataList.ascx.cs"
    Inherits="PMS.PMS.UserControl.MyTaskDataList" %>
<asp:DataList runat="server" ID="DataList">
    <ItemTemplate>
        <asp:HiddenField ID="HiddenFieldSerial" runat="server" />
        <%# DataBinder.Eval(Container.DataItem, "PlanStartDay","{0:yyyy-MM-dd}")%>
    </ItemTemplate>
    <ItemTemplate>
       <b> <%# DataBinder.Eval(Container.DataItem, "CrName")%></b>
         <br />
       <%# DataBinder.Eval(Container.DataItem, "TaskName")%>

    </ItemTemplate>
</asp:DataList>
