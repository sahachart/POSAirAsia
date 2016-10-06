<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="CreateInvoiceSystem.Page1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class ="col-lg-12">
        <h3>
            <asp:Label ID="lblHearder" runat="server" Text=""></asp:Label>
        </h3>
    </div>
        <div class ="col-lg-12">
        <h5>
            <asp:Label ID="lblDetail" runat="server" Text=""></asp:Label>
        </h5>
    </div>
</asp:Content>
