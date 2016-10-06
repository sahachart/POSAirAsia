<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="invoice_info.aspx.cs" Inherits="CreateInvoiceSystem.invoice_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/dataTables.bootstrap.min.css" rel="stylesheet" />
    <%--<link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>



    <asp:HiddenField ID="hidfOpenMode" runat="server" Value="NEW" ClientIDMode="Static" />
    <asp:HiddenField ID="hidfRowID" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="hidfInvoiceNo" runat="server" Value="" ClientIDMode="Static" />

    <asp:HiddenField ID="hidfTranID" runat="server" Value="" ClientIDMode="Static" />

    <asp:HiddenField ID="hidfCustCode" runat="server" Value="" ClientIDMode="Static" />
    <div class="container" style="padding-top: 20px;">
        <div class="panel panel-default">
            <div class="panel-heading">Basic Information</div>



            <table class="searchCriteria">
                <tr>
                    <td class="tableform" style="width: 141px; height: 27px;">Booking No.</td>
                    <td class="tableform" style="width: 349px; text-align: left; height: 27px;">
                        <asp:TextBox ID="txtBookingNo" runat="server" BackColor="#FFFF99" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 115px; height: 27px;">Invoice No.</td>
                    <td class="tableform" style="text-align: left; width: 58px; height: 27px;">
                        <asp:TextBox ID="txtInvoiceNo" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 90px; height: 27px;">&nbsp;</td>
                    <td class="tableform" style="text-align: left; height: 27px;">&nbsp;</td>
                    <td style="width: 30px; height: 27px;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tableform" style="width: 141px; height: 27px;">Booking Date</td>
                    <td class="tableform" style="width: 349px; text-align: left; height: 27px;">
                        <asp:TextBox ID="tbx_date_booking" runat="server" BackColor="#FFFF99" ReadOnly="True" Width="120px"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 115px; height: 27px;">ABB No</td>
                    <td class="tableform" style="text-align: left; width: 58px; height: 27px;">
                        <asp:TextBox ID="tbx_pos_no" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 90px; height: 27px;"></td>
                    <td class="tableform" style="text-align: left; height: 27px;"></td>
                    <td style="width: 30px; height: 27px;"></td>
                </tr>
                <tr>
                    <td class="tableform" style="width: 141px; height: 27px;">Customer Name</td>
                    <td class="tableform" style="width: 349px; text-align: left; height: 27px;">
                        <asp:TextBox ID="txtCustCode" runat="server" Width="80px" ClientIDMode="Static"></asp:TextBox>
                        <asp:TextBox ID="txtFullName" runat="server" Width="200px" ClientIDMode="Static" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                        &nbsp;<asp:ImageButton ID="imgSearchCus" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" ClientIDMode="Static" OnClientClick="ChooseCustClick(); return false;" />
                        <p style="display: none;">
                            <asp:Button ID="btnSelCust" runat="server" Text="Button" ClientIDMode="Static" OnClick="btnSelCust_Click" />
                            <asp:HiddenField ID="hidfSelCus" runat="server" Value="" ClientIDMode="Static" />
                        </p>

                    </td>
                    <td class="tableform" style="width: 115px; height: 27px;">Branch</td>
                    <td class="tableform" style="text-align: left; width: 58px; height: 27px;">
                        <asp:TextBox ID="txtBranchNo" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 90px; height: 27px;">&nbsp;</td>
                    <td class="tableform" style="text-align: left; height: 27px;">&nbsp;</td>
                    <td style="width: 30px; height: 27px;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tableform" style="width: 141px; height: 27px;">Tax ID</td>
                    <td class="tableform" style="width: 349px; text-align: left; height: 27px;">
                        <asp:TextBox ID="tbx_booking_taxid" runat="server" MaxLength="13" ClientIDMode="Static" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 115px; height: 27px;">POS No.</td>
                    <td class="tableform" style="text-align: left; width: 58px; height: 27px;">
                        <asp:TextBox ID="tbx_pos_no1" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 90px; height: 27px;">&nbsp;</td>
                    <td class="tableform" style="text-align: left; height: 27px;">&nbsp;</td>
                    <td style="width: 30px; height: 27px;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tableform" style="width: 141px">Address</td>
                    <td class="tableform" style="width: 349px; text-align: left" rowspan="3">
                        <asp:TextBox ID="tbx_booking_address1" runat="server" MaxLength="255" Width="100%" Height="70" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 115px">Cashier No</td>
                    <td class="tableform" style="text-align: left; width: 58px;">
                        <asp:TextBox ID="tbx_cashier_no" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 90px"></td>
                    <td class="tableform" style="text-align: left"></td>
                    <td style="width: 30px;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tableform" style="width: 141px"></td>
                    <%--   <td class="tableform" style="width: 349px; text-align: left">
                        </td>--%>
                    <td class="tableform" style="width: 115px">Create by</td>
                    <td class="tableform" style="text-align: left; width: 58px;">
                        <asp:TextBox ID="tbx_create_by" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 90px">Update by</td>
                    <td class="tableform" style="text-align: left">
                        <asp:TextBox ID="tbx_update_by" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True" Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 30px;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tableform" style="width: 141px"></td>
                    <%--  <td class="tableform" style="width: 349px; text-align: left">
                        </td>--%>
                    <td class="tableform" style="width: 115px">Create Date</td>
                    <td class="tableform" style="text-align: left; width: 58px;">
                        <asp:TextBox ID="tbx_create_date" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 90px">Update Date</td>
                    <td class="tableform" style="text-align: left">
                        <asp:TextBox ID="tbx_update_date" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True" Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 30px;">&nbsp;</td>
                </tr>

            </table>





            <table class="searchCriteria">

                <tbody>
                    <tr>
                        <%--<td class="headlabel" colspan="7" >Payment Information</td>--%>
                        <td class="tableform" colspan="7">
                            <table class="" style="width: 100%;">
                                <tr style="margin-bottom: 10px;">
                                    <td class="headlabel" colspan="7">
                                        <h5 class="header smaller lighter blue">Flight Detail</h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="form-group">
                                            <div class="col-sm-8">
                                                <table id="table_flight" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                                    <thead>
                                                        <tr style="background-color: #007acc; color: white">
                                                            <th>Flight No.</th>
                                                            <th>Flight Date</th>
                                                            <th>From</th>
                                                            <th>To</th>
                                                            <th>From Time</th>
                                                            <th>Arrive Time</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <%= TableFlight()%>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <%--<td class="headlabel" colspan="7" >Payment Information</td>--%>
                        <td class="tableform" colspan="7">
                            <table class="" style="width: 100%;">
                                <tr style="margin-bottom: 10px;">
                                    <td class="headlabel" colspan="7">
                                        <h5 class="header smaller lighter blue">Passenger</h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-4">
                                            <table id="table_passenger" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr style="background-color: #007acc; color: white">
                                                        <th>No.</th>
                                                        <th>Passenger Name</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%= TablePassenger()%>
                                                </tbody>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%-- <tr>
            
                <td class="tableform" colspan="7">
                    <table class="" style="width: 100%;">
                        <tr>
                            <td class="headlabel" colspan="7">Payment</td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-8">
                                    <table id="table_payment" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th nowrap>Payment Agent</th>
                                                <th nowrap>Payment Date</th>
                                                <th nowrap>Payment Type</th>
                                                <th>Currency</th>
                                                <th nowrap>Payment Amt.</th>
                                                <th nowrap>Payment Amt.(THB)</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%= TablePayment()%>
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
                </tbody>
            </table>

            <%--
        </ContentTemplate>
    </asp:UpdatePanel>--%>

            <div class="form-group">
                <div class="col-sm-12">
                    <h5 class="header smaller lighter blue">Fare</h5>

                    <div style="overflow-x: auto; width: 900px;">
                        <table id="table_fare" class="table table-striped table-bordered" cellspacing="0">
                            <thead>
                                <tr style="background-color: #007acc; color: white">
                                    <th style="text-align: center;">Pax Type</th>
                                    <th style="text-align: center;">ChargeCode</th>
                                    <th style="text-align: center;">Currency</th>
                                    <th style="text-align: center;">Ex.</th>
                                    <th style="text-align: center;">Amount</th>
                                    <th style="text-align: center;">Amount (THB)</th>
                                    <th style="text-align: center;">Quantity</th>
                                    <th style="text-align: center;">Discount (THB)</th>
                                    <th style="text-align: center;">Total(THB)</th>
                                    <th style="text-align: center;">Group</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptFare" runat="server" OnItemDataBound="rptFare_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trdata" runat="server">
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_PaxType" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_ChargeCode" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_Currency" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_ExchangeRateTH" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_Amount" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_AmountTH" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_Quantity" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_DiscountTH" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_TotalTH" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Group" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan="4" style="text-align: right">Total:</th>
                                    <th style="text-align: right;">
                                        <asp:Label ID="lbl_TotalFareAmountOri" runat="server" Text="0.00"></asp:Label>
                                    </th>
                                    <th style="text-align: right;">
                                        <asp:Label ID="lbl_TotalFareAmountTH" runat="server" Text="0.00"></asp:Label>
                                    </th>
                                    <th></th>
                                    <th></th>
                                    <th style="text-align: right;">
                                        <asp:Label ID="lbl_TotalFareTH" runat="server" Text="0.00"></asp:Label>
                                    </th>
                                    <th></th>
                                </tr>
                            </tfoot>
                        </table>

                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <h5 class="header smaller lighter blue">Fee</h5>
                    <div style="overflow-x: auto; width: 900px;">
                        <table id="table_fee" class="table table-striped table-bordered" cellspacing="0">
                            <thead>
                                <tr style="background-color: #007acc; color: white">
                                    <th style="text-align: center; width: 250px;">Passenger</th>
                                    <th style="text-align: center;">ChargeCode</th>
                                    <th style="text-align: center;">Currency</th>
                                    <th style="text-align: center;">Ex.</th>
                                    <th style="text-align: center;">Amount</th>
                                    <th style="text-align: center;">Quantity</th>
                                    <th style="text-align: center;">Discount (THB)</th>
                                    <th style="text-align: center;">Amount (THB)</th>
                                    <th style="text-align: center;">Group</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptFee" runat="server" OnItemDataBound="rptFee_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trdata" runat="server">
                                            <td>
                                                <asp:Label ID="lbl_PassengerName" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_FeeChargeCode" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_FeeCurrency" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_FeeExchangeRateTH" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_FeeAmount" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_FeeQuantity" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_FeeDiscountTH" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_FeeAmountTH" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl_FeeGroup" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan="4" style="text-align: right">Total:</th>
                                    <th style="text-align: right;">
                                        <asp:Label ID="lbl_FeeTotalAmonut" runat="server" Text=""></asp:Label>
                                    </th>
                                    <th></th>
                                    <th></th>
                                    <th style="text-align: right;">
                                        <asp:Label ID="lbl_FeeTotalAmonutTH" runat="server" Text=""></asp:Label>
                                    </th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <th colspan="5" style="text-align: right">Total Fare + Fee:</th>
                                    <th colspan="3" style="text-align: right;">
                                        <asp:Label ID="lbl_TotalFareAndFee" runat="server" Text="0.0"></asp:Label>
                                    </th>
                                    <th></th>
                                </tr>
                            </tfoot>

                        </table>
                    </div>
                </div>
            </div>


            <div class="form-group">
                <div class="col-sm-12">
                    <h5 class="header smaller lighter blue">Payment</h5>

                    <div style="overflow-x: auto; width: 900px;">
                        <table id="table_payment" class="table table-striped table-bordered" cellspacing="0" width="100%">
                            <thead>
                                <tr style="background-color: #007acc; color: white">
                                    <th>Payment Agent</th>
                                    <th>Payment Date</th>
                                    <th>Payment Type</th>
                                    <th>Currency</th>
                                    <th>Payment Amt.</th>
                                    <th>Payment Amt.(THB)</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptPayment" runat="server" OnItemDataBound="rptPayment_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trdata" runat="server">

                                            <td>
                                                <asp:Label ID="lblPaymentAgent" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblPaymentDate" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblPaymentType" runat="server"></asp:Label></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblCurrency" runat="server"></asp:Label></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblPaymentAmountTHB" runat="server"></asp:Label></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan="4" style="text-align: right">Total:</th>
                                    <th style="text-align: right;">
                                        <asp:Label ID="lbl_TotalPayment" runat="server" Text=""></asp:Label>
                                    </th>
                                    <th style="text-align: right;">
                                        <asp:Label ID="lbl_TotalPaymentTHB" runat="server" Text=""></asp:Label>
                                    </th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>

            </div>





            <h3 class="header smaller lighter blue"></h3>
            <div class="row">
                <div style="text-align: center">
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" Height="40px" Width="80px" OnClick="btnSave_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" Height="40px" Width="80px" OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-primary" Text="Print" Height="40px" Width="80px" OnClick="btnPrint_Click" Enabled="false" OnClientClick="return false;" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            <div style="display: none;">
                <asp:Button runat="server" ID="btnSearch" Text="" OnClick="btnSearch_Click" ClientIDMode="Static" />
                <asp:HiddenField ID="hidfSearch" runat="server" Value="" ClientIDMode="Static" />
            </div>
                </div>
            </div>


        </div>
    </div>

    <div id="dialog_PrintMode" style="display: none;">
        <div class="container col-lg-12">
            <div class="row">
                <asp:RadioButtonList ID="rdb_PrintMode" ClientIDMode="Static" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Selected="True" Value="1">&nbsp;ค่าตั๋วโดยสารและค่าธรรมเนียมอื่นๆ&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;ค่าตั๋วโดยสาร&nbsp;&nbsp;</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>



    <div id="dialog-confirm-SearchCustomer" title="Search Customer">

        <div class="container col-lg-12">
            <div class="row">
                <div class="col-lg-3" style="text-align: right; margin-top: 10px;">
                    Search
                </div>
                <div class="col-lg-6">
                    <asp:TextBox ID="txtSearchCustomer" runat="server" CssClass="txtcalendar" ClientIDMode="Static" Width="100%" EnableViewState="true"></asp:TextBox>
                </div>
                <div class="col-lg-2" style="margin-top: 5px;">
                    <asp:Button ID="btnPopupSearch" runat="server" Text="Search" ClientIDMode="Static" OnClientClick=" $('#hidfSearch').val( $('#txtSearchCustomer').val() );  $('#btnSearch').click();" />
                </div>
                <div class="col-lg-1">
                    <img id="Img1" src="Images/add.png" width="20px" style="cursor: pointer;" />
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <table class="table table-striped table-bordered" cellspacing="0" width="100%">
                            <thead>
                                <tr style="background-color: #007acc; color: white; text-align: center;">
                                    <td style="width: 25px;">Choose</td>
                                    <td>Name</td>
                                    <td style="width: 70px;">Tax ID</td>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptCustomer" runat="server" OnItemDataBound="rptCustomer_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trdata" runat="server">
                                            <td style="text-align: center;">
                                                <asp:Image ID="imgAction" runat="server" Style="width: 18px;" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hidfCusVal" runat="server" Value="" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTaxID" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSelCust" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>


        <script>


            function ChooseCus(hidfid) {

                var str = $('#' + hidfid).val();
                var sp = str.split('|');
                $('#hidfSelCust').val(str);
                if (sp.length > 0) {
                    $('#txtCustCode').val(sp[0]);
                    $('#hidfCustCode').val(sp[0]);

                    $('#txtFullName').val(sp[2] + ' ' + sp[3]);
                    $('#tbx_booking_taxid').val(sp[4]);
                    $('#tbx_booking_address1').val(sp[5]);
                    //$('#tbx_booking_address2').val(sp[6]);
                    //$('#tbx_booking_address3').val(sp[7]);
                    // $('#btnSelCust').click();
                    $("#dialog-confirm-SearchCustomer").dialog("close");
                }

            }
        </script>
    </div>




    <div id="dialog-formFullTax" style="display: none">
        <iframe id="pdfObject" src="" width="100%" height="100%"></iframe>
    </div>
    <script src="Scripts/Bootstrap/bootstrap.min.js"></script>
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/Bootstrap/dataTables.bootstrap.min.js"></script>
    <script src="Scripts/WebForms/CIS-Customer/cis-master.js"></script>
    <script>
        $(document).ready(function () {
            $("#txtCustCode").hide();
            $("#dialog-customer").hide();
            $("#dialog-confirm-SearchCustomer").hide();
            var val = urlParam('key', window.location.href);
            $('#MainContent_Button_print').click(function () {
                //var key = $('#MainContent_tbx_receipt_no').val();
                if (val != "") {
                    popup_preview(val);
                } else {
                    return;
                }
            });

            $('#MainContent_btnPrint').click(function (e) {

                $("#dialog_PrintMode").dialog({
                    resizable: false,
                    height: 200,
                    width: 600,
                    modal: true,
                    open: function (event, ui) {
                        $('.ui-dialog').css('z-index', 2000);
                        $('.ui-widget-overlay').css('z-index', 1000);
                        $('.ui-dialog-buttonpane').find('button:contains("Ok")').addClass('btn btn-primary');
                        $('.ui-dialog-buttonpane').find('button:contains("Cancel")').addClass('btn btn-default');
                    },
                    title: 'Please select the description',
                    buttons: {
                        Ok: function () {
                            $(this).dialog("close");
                            var urltext = 'fulltax_form.aspx?INV_No=' + $('#MainContent_txtInvoiceNo').val() + '&Mode=' + $('#rdb_PrintMode [type="radio"]:checked').val();
                            openPopupPreview(urltext, 'Invoice', 900, 500);
                        },
                        Cancel: function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });


            popup_preview = function (key) {
                var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
                var urltext = 'fulltax_form.aspx?key=' + key;
                if (is_chrome) {
                    window.open(urltext);
                    wcppDetectOnSuccess(urltext);
                } else {
                    var content = "<object id='pdfObject' style='overflow-x: hidden;' type='application/pdf' data='" + urltext + "' width='100%' height='100%' />";
                    $("#dialog-formFullTax").html(content);
                    $("#dialog-formFullTax").dialog({
                        modal: true,
                        width: 900,
                        height: 600,
                        title: 'Bill Invoice Preview',
                        resizable: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        buttons: {
                            'Close': function () {
                                $(this).dialog('close');
                            }
                        }
                    });
                }
                return false;
            };


            //$('#imgSearchCus').click(function (e) {
            //    e.preventDefault();

            //    $("#dialog-confirm-SearchCustomer").hide();



            //    $("#dialog-confirm-SearchCustomer").dialog({
            //        resizable: false,
            //        height: 400,
            //        width: 600,
            //        modal: true,
            //        open: function (event, ui) {
            //            $('.ui-dialog').css('z-index', 103);
            //            $('.ui-widget-overlay').css('z-index', 102);
            //        },
            //        title: 'Edit Payment',
            //        buttons: {

            //            Cancel: function () {
            //                $(this).dialog("close");
            //            }
            //        }
            //    });




            //});

        });


        $('#Img1').click(function () {
            $("#dialog-confirm-SearchCustomer").dialog("close");
            $("#dialog-customer").hide();

            $("#dialog-customer").dialog({
                resizable: false,
                height: window.innerHeight,
                width: 800,
                modal: true,
                title: 'Update Customer Info',
                open: function (event, ui) {
                    $('.ui-dialog').css('z-index', 103);
                    $('.ui-widget-overlay').css('z-index', 102);
                },
                buttons: {
                    "Save": function () {
                        var cust_code = '00000000-0000-0000-0000-000000000000';//$("#MainContent_tbx_cust_code").val();
                        //if ($("#MainContent_tbx_cust_code").val() == "") {
                        //    alert('Please fill the customer code.');
                        //    return;
                        //}
                        var prfx_name = $("#MainContent_tbx_prfx_name").val();

                        //if (prfx_name == "") {
                        //    alert('Please fill prefix name');
                        //    return;
                        //}
                        var first_name = $("#MainContent_tbx_first_name").val();

                        if (first_name == "") {
                            alert('Please fill the customer name.');
                            return;
                        }

                        var Branch_No = $('#MainContent_txt_BranchNo').val();

                        var last_name = $("#MainContent_tbx_last_name").val();

                        //if (last_name == "") {
                        //    alert('Please fill the lastname.');
                        //    return;
                        //}
                        var gndr_code = Validation();

                        if (!gndr_code) {
                            alert('Please choose gender type.');
                            return;
                        }
                        var Addr_1 = $("#MainContent_tbx_Addr_1").val();
                        var Addr_2 = $("#MainContent_tbx_Addr_2").val();
                        var Addr_3 = $("#MainContent_tbx_Addr_3").val();
                        var Addr_4 = $("#MainContent_tbx_Addr_4").val();
                        var Addr_5 = $("#MainContent_tbx_Addr_5").val();

                        if (Addr_1 == "") {
                            alert('Please fill in the address.');
                            return;
                        }
                        //var Zip_Code = $("#MainContent_tbx_ZipCode").val();

                        //if (Zip_Code == "") {
                        //    alert('Please fill in zip code.');
                        //    return;
                        //}
                        var Home_Phone = $("#MainContent_tbx_Home_Phone").val();

                        //if (Home_Phone == "") {
                        //    alert('Please fill in telephone number.');
                        //    return;
                        //}
                        var Email = $("#MainContent_tbx_Email").val();

                        //if (Email == "") {
                        //    alert('Please fill in email. ');
                        //    return;
                        //}

                        var prov = ""; //$("#MainContent_province").val();

                        //if (prov == "") {
                        //    alert('Please fill in province.');
                        //    return;
                        //}
                        var countr = ""; //$("#MainContent_country").val();

                        //if (countr == "") {
                        //    alert('Please fill in country. ');
                        //    return;
                        //}
                        var taxid = $("#MainContent_tbx_taxid").val();

                        if (taxid == "") {
                            alert('Please fill in Tax ID. ');
                            return;
                        }

                        var remark = $('#MainContent_tbx_remark').val();
                        var obj = {
                            CustomerID: cust_code,
                            first_name: first_name,
                            Branch_No: Branch_No,
                            last_name: last_name,
                            prfx_name: prfx_name,
                            gndr_code: gndr_code,
                            Addr_1: Addr_1,
                            Addr_2: Addr_2,
                            Addr_3: Addr_3,
                            Addr_4: Addr_4,
                            Addr_5: Addr_5,
                            //Prvn_Code: prov,
                            //Cntr_Code: countr,
                            //Zip_Code: Zip_Code,
                            Home_Phone: Home_Phone,
                            Email: Email,
                            remark: remark,
                            crtd_dt: null,
                            crtd_by: "",
                            last_upd_dt: null,
                            last_upd_by: "",
                            IsActive: "Y",
                            TaxID: taxid
                        };
                        var msg = '';
                        try {
                            $.ajax({
                                url: 'servicepos.asmx/SaveCustomer',
                                type: "POST",
                                //dataType: "xml",
                                //cache: false,
                                //data: { 'key': obj.cust_code, 'mode': $('#txt_FormState').val() },
                                data: JSON.stringify({ obj: obj, mode: 'insert' }),
                                //data: "{ 'cust_code':'" + obj.cust_code + "','first_name':'" + obj.first_name +
                                //    "','last_name':'" + obj.last_name + "','prfx_name':'" + obj.prfx_name +
                                //    "','mode':'" + $('#txt_FormState').val() + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    if (data.d.IsSuccessfull) {
                                        //alert('Save Success');
                                        $("#dialog-customer").dialog("close");
                                        alert(data.d.Message);

                                    }
                                    else {
                                        msg = data.d.Message;
                                        alert(msg);
                                    }

                                },
                                error: function (xmlHttpRequest, textStatus, errorThrown) {
                                    console.log(xmlHttpRequest.responseText);
                                    console.log(textStatus);
                                    console.log(errorThrown);
                                }
                            });
                        } catch (ex) {

                        }

                        if (msg == '') {
                            //dialog.dialog("close");
                        }
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                }
            });

        });

        Validation = function () {
            var radioList = "MainContent_rbl_gndr_code";
            var radioButtonList = document.getElementById(radioList);
            var listItems = radioButtonList.getElementsByTagName("input");
            for (var i = 0; i < listItems.length; i++) {
                if (listItems[i].checked) {
                    //alert("Selected value: " + listItems[i].value);
                    return listItems[i].value;
                    break;
                }
            }
        };

        function ChooseCustClick() {


            // txtCustCode  txtSearchCustomer
            var txtCustCode = $('#txtCustCode').val();
            if (txtCustCode != '') {
                //$('#txtSearchCustomer').val(txtCustCode);
                $('#hidfSearch').val(txtCustCode);
                $('#btnSearch').click();


            }

            $("#dialog-confirm-SearchCustomer").hide();



            $("#dialog-confirm-SearchCustomer").dialog({
                resizable: false,
                height: 400,
                width: 600,
                modal: true,
                open: function (event, ui) {
                    $('.ui-dialog').css('z-index', 103);
                    $('.ui-widget-overlay').css('z-index', 102);
                },
                title: 'Edit Payment',
                buttons: {

                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }

    </script>







    <div id="dialog-customer">
        <input class="hidden" type="text" id="txt_FormState" />
        <table style="width: 100%">
            <tr>
                <td class="headlabel" colspan="2">Customer Profile</td>
            </tr>
            <tr style="display: none;">
                <td class="tableform" nowrap>Customer Code<span style="color: red">&nbsp;*</span></td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="tbx_cust_code" runat="server" CssClass="txtFormCell" Width="120px" MaxLength="30"></asp:TextBox>

                </td>
            </tr>
            <tr style="display: none;">
                <td class="tableform" nowrap>NameTitle<span style="color: red">&nbsp;*</span>  </td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="tbx_prfx_name" runat="server" CssClass="txtFormCell" Width="120px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Customer Name<span style="color: red">&nbsp;*</span></td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="tbx_first_name" runat="server" CssClass="txtFormCell" Width="250px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Branch No</td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="txt_BranchNo" runat="server" CssClass="txtFormCell" Width="250px"></asp:TextBox>

                </td>
            </tr>
            <tr style="display: none;">
                <td class="tableform" nowrap>LastName<span style="color: red">&nbsp;*</span></td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="tbx_last_name" runat="server" CssClass="txtFormCell" Width="250px"></asp:TextBox>

                </td>
            </tr>
            <tr style="display: none">
                <td class="tableform" style="vertical-align: top" nowrap>Sex<span style="color: red">&nbsp;*</span>

                </td>
                <td class="tableform" style="width: 70%">
                    <asp:RadioButtonList ID="rbl_gndr_code" runat="server">
                        <asp:ListItem Text="Male" Value="M" />
                        <asp:ListItem Text="Female" Value="F" />
                        <asp:ListItem Text="No Special" Value="N" Selected="True" />
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Address 1<span style="color: red">&nbsp;*</span></td>
                <td class="tableform" style="width: 100%">
                    <asp:TextBox ID="tbx_Addr_1" runat="server" CssClass="txtFormCell" Width="80%"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Address 2</td>
                <td class="tableform" style="width: 100%">
                    <asp:TextBox ID="tbx_Addr_2" runat="server" CssClass="txtFormCell" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Address 3</td>
                <td class="tableform" style="width: 100%">
                    <asp:TextBox ID="tbx_Addr_3" runat="server" CssClass="txtFormCell" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Address 4</td>
                <td class="tableform" style="width: 100%">
                    <asp:TextBox ID="tbx_Addr_4" runat="server" CssClass="txtFormCell" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Address 5</td>
                <td class="tableform" style="width: 100%">
                    <asp:TextBox ID="tbx_Addr_5" runat="server" CssClass="txtFormCell" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="tableform" style="text-align: left; padding-right: 10px; width: 40%;" nowrap>Province<span style="color: red">*</span></td>
                <td class="tableform" style="width: 100%">
                    <asp:DropDownList ID="province" runat="server" Width="250px" CssClass="txtcalendar">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="tableform" style="text-align: left; padding-right: 10px; width: 40%;" nowrap>Country<span style="color: red; text-align: left;">*</span></td>
                <td class="tableform" style="width: 100%">
                    <asp:DropDownList ID="country" runat="server" Width="250px" CssClass="txtcalendar">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="tableform" nowrap>Zip Code<span style="color: red">&nbsp;*</span>  </td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="tbx_ZipCode" runat="server" CssClass="txtFormCell" Width="250px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Tel.<%--<span style="color: red">&nbsp;*</span>--%>  </td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="tbx_Home_Phone" runat="server" CssClass="txtFormCell" Width="250px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="tableform" nowrap>Email<%--<span style="color: red">&nbsp;*</span>--%>  </td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="tbx_Email" runat="server" CssClass="txtFormCell" Width="250px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="tableform" style="vertical-align: top">Description</td>
                <td class="tableform">
                    <asp:TextBox ID="tbx_remark" runat="server" CssClass="txtFormCell" TextMode="MultiLine" Rows="3" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tableform" style="vertical-align: top">Tax ID<span style="color: red">&nbsp;*</span>  </td>
                <td class="tableform" style="width: 100%">
                    <asp:TextBox ID="tbx_taxid" runat="server" CssClass="txtFormCell" MaxLength="13" Width="200px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
