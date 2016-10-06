<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoiceCN_Info.aspx.cs" Inherits="CreateInvoiceSystem.InvoiceCN_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script>
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">




    <asp:HiddenField ID="hidfOpenMode" runat="server" Value="NEW" ClientIDMode="Static" />
    <asp:HiddenField ID="hidfRowID" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="hidfInvoiceNo" runat="server" Value="" ClientIDMode="Static" />

    <asp:HiddenField ID="hidfTranID" runat="server" Value="" ClientIDMode="Static" />

    <asp:HiddenField ID="hidfCN_ID" runat="server" Value="" ClientIDMode="Static" />
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
                        <div style="display: none;"><asp:TextBox ID="txtCustCode" runat="server" Width="80px"  BackColor="#FFFF99" ForeColor="Black" ReadOnly="True" ClientIDMode="Static"></asp:TextBox></div>
                        <asp:TextBox ID="txtFullName" runat="server" Width="200px" ClientIDMode="Static" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                        <p style="display: none;">
                        &nbsp;<asp:ImageButton ID="imgSearchCus" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" ClientIDMode="Static" OnClientClick="ChooseCustClick(); return false;" />

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
                        <asp:TextBox ID="tbx_booking_address1" runat="server" MaxLength="255" Width="100%" Height="70" ClientIDMode="Static" TextMode="MultiLine"  BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 115px">Chashier No</td>
                    <td class="tableform" style="text-align: left; width: 58px;">
                        <asp:TextBox ID="tbx_cashier_no" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td class="tableform" style="width: 90px"></td>
                    <td class="tableform" style="text-align: left"></td>
                    <td style="width: 30px;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tableform" style="width: 141px"></td>
              <%--      <td class="tableform" style="width: 349px; text-align: left">
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
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>Status : <asp:Label ID="lblStatus" runat="server" ClientIDMode="Static"></asp:Label>
                    </td>
                    <td>
                        <%--<input id="btnCN" type="button" value="Request CN" class="btn btn-primary" runat="server"/>--%>
                        
                    </td>
                </tr>
            </table>





            <table class="searchCriteria">
                <tbody>
                    <tr>
                        <td class="tableform" colspan="7">
                            <table class="" style="width: 100%;">
                                <tr style="margin-bottom: 10px;">
                                    <td class="headlabel" colspan="7"><h5 class="header smaller lighter blue">Flight Detail</h5></td>
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
                        <td class="tableform" colspan="7">
                            <table class="" style="width: 100%;">
                                <tr style="margin-bottom: 10px;">
                                    <td class="headlabel" colspan="7"><h5 class="header smaller lighter blue">Passenger</h5> </td>

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
                </tbody>
            </table>

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
                            </table>
                        </div>
                </div>

            </div>

             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

          

            <div id="divCN_History" runat="server" visible="false" class="form-group" >
                <div class="col-sm-12">
                    <h5 class="header smaller lighter blue">CN History</h5>
                        <div style="overflow-x: auto; width:700px;">
                            <table  class="table table-striped table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr style="background-color: #007acc; color: white">
                                        <th>CN No.</th>
                                        <th>CN Date</th>
                                        <th>CN Reason Type</th>
                                        <th>Create By</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptCN" runat="server" OnItemDataBound="rptCN_OnItemDataBound">
                                        <ItemTemplate>
                                            <tr id="trdata" runat="server">
                                                <td>
                                                    <asp:Label ID="lblCN_No" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblCN_Date" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblReason" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblCreateBy" runat="server"></asp:Label></td>
                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                </div>
            </div>


                          </ContentTemplate>
            </asp:UpdatePanel>

            <h3 class="header smaller lighter blue"></h3>
            <div class="row" style="margin-bottom: 20px;">
                <div style="text-align: center">
                    <asp:HiddenField ID="hidfReasonID" runat="server" ClientIDMode="Static" />
                    <div style="display: none;"><asp:Button runat="server" ID="btnSave" Text="Save" Height="40px" Width="80px" OnClick="btnSave_Click" CssClass="btn btn-primary disabled" Enabled="false" /></div>
                    <asp:Button runat="server" ID="btnCancel"  Text="Cancel" Height="40px" Width="80px" OnClick="btnCancel_Click" CssClass="btn btn-default" Style="margin-left: 20px;" />
                    <asp:Button runat="server" ID="btnCN" Text="Request CN" Height="40px" Width="120px" ClientIDMode="Static" CssClass="btn btn-primary" OnClientClick="return false;" />
                </div>
                <div style="display:none;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <asp:Button ID="btnSaveNPrint" runat="server" Text="Save and Print" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="btnSaveNPrint_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSaveNPrint" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            

        </div>
    </div>


    <div id="dialog-CN" title="Set Full Tax to CN">
        <div class="container col-lg-12">
            <div class="row" style="margin-top: 20px;">
                <div class="col-lg-12">
                    CN Reason
                    <div style="float: right; width: 300px;">
                        <asp:DropDownList ID="ddlReason" runat="server" ClientIDMode="Static" CssClass="form-control "></asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="col-lg-12" style="margin-top: 20px;">
                <div style="margin: 0 auto; width: 240px;">
                    <button id="btnSavePopup" type="button" class="btn btn-primary">Save and Print</button>
                    <button id="btnClosePopup" type="button" class="btn btn-default" style="margin-left: 15px;">Cancel</button>
                </div>
            </div>

        </div>
    </div>


    <script src="Scripts/Bootstrap/bootstrap.min.js"></script>
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/Bootstrap/dataTables.bootstrap.min.js"></script>
    <script src="Scripts/WebForms/CIS-Customer/cis-master.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dialog-CN").hide();
            
            var val = urlParam('key', window.location.href);
            $('#MainContent_Button_print').click(function () {
                //var key = $('#MainContent_tbx_receipt_no').val();
                if (val != "") {
                    popup_preview(val);
                } else {
                    return;
                }
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

            $('#btnCN').click(function () {
                $("#dialog-CN").hide();

                $("#dialog-CN").dialog({
                    resizable: false,
                    height: 300,
                    width: 500,
                    modal: true,
                    open: function (event, ui) {
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                });

            });

            $('#btnClosePopup').click(function () {
                $('#dialog-CN').dialog('close');
            });
            $('#btnSavePopup').click(function () {
                var r = $('#ddlReason').val();
                $('#hidfReasonID').val(r)              
                $('#dialog-CN').dialog('close');
                $('#btnSaveNPrint').click();
            });

        });

        function SaveSuccess(inv_no) {
            $('#lblStatus').text('CN');
            alert('บันทึกสำเร็จ');
            var urltext = 'CreditNote_Form.aspx?inv_no=' + inv_no;
            //window.open('CreditNote_Form.aspx?inv_no=' + inv_no, '_blank');
            openPopupPreview(urltext, 'CN Preview', 900, 600);
            //window.location.href = window.location.href;
        }

        



    </script>

</asp:Content>
