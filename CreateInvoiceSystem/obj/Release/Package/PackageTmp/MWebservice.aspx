<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MWebservice.aspx.cs" Inherits="CreateInvoiceSystem.MWebservice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-lg-3">
        SessionURL
    </div>
    <div class="col-lg-9">
        <asp:Label ID="lblSessionURL" runat="server" Text=""></asp:Label>

    </div>
    <div class="col-lg-3">
        BookingURL
    </div>
    <div class="col-lg-9">
        <asp:Label ID="lblBookingURL" runat="server" Text=""></asp:Label>

    </div>
    <div class="col-lg-3">
        Username
    </div>
    <div class="col-lg-9">
        <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>

    </div>
    <div class="col-lg-3">
        Password
    </div>
    <div class="col-lg-9">
        <asp:Label ID="lblpassword" runat="server" Text=""></asp:Label>

    </div>



</asp:Content>
