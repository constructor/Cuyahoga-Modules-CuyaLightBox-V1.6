<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CuyaLightBox.ascx.cs" Inherits="Cuyahoga.Modules.CuyaLightBox.Web.CuyaLightBox" %>
<asp:Label ID="lblMessages" runat="server" Text=""></asp:Label>
<asp:Panel ID="pnlGallery" runat="server">
    <asp:Repeater ID="rptImageItems" runat="server">
    <HeaderTemplate><ul></HeaderTemplate>
    <ItemTemplate>
        <li><a href='<%# GetImage(Eval("Filename").ToString()) %>' title='<%# Eval("Title") %>'><img src='<%# GetThumbImage(Eval("Filename").ToString()) %>' width='<%# Eval("Width") %>' height='<%# Eval("Height") %>' alt='<%# Eval("AltText") %>' /></a></li>
    </ItemTemplate>
    <FooterTemplate></ul></FooterTemplate>
    </asp:Repeater>
</asp:Panel>