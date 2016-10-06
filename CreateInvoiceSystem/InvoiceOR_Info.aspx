<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoiceOR_Info.aspx.cs" Inherits="CreateInvoiceSystem.InvoiceOR_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script>

        $(document).ready(function () {
            //$('#MainContent_btnOR').click(function () {
            //    var s = confirm('คุณต้องการยกเลิก Invoice หรือไม่');
            //    if (s) {
            //        $('#btnSaveOR').click();
            //    }
            //});
        });
 
        function Req_OR() {
            var s = confirm('คุณต้องการยกเลิก Invoice หรือไม่');
            if (s) {
                $('#btnSaveOR').click();
            }
        }

        function SaveSuccess(inv_no) {
            alert('บันทึกสำเร็จ');
            window.location.href = window.location.href;
        }

        function SaveFail() {
            alert('บันทึกไม่สำเร็จ');
        }

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
                        <asp:TextBox ID="txtCustCode" runat="server" Width="80px" ClientIDMode="Static" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
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
                        <asp:TextBox ID="tbx_booking_address1" runat="server" MaxLength="255" Width="100%" Height="70" ClientIDMode="Static" TextMode="MultiLine" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
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
                    <td>Status :
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </td>
                    <td>
                        <input id="btnOR" type="button" value="Request OR" class="btn btn-primary" runat="server"  />
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
                                    <td class="headlabel" colspan="7"><h5 class="header smaller lighter blue">Passenger</h5></td>
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
                    <h5 class="header smaller lighter blue">Fee</h5>
                    <div style="overflow-x: auto; width: 900px;">
                        <table id="table_fee" class="table table-striped table-bordered" cellspacing="0">
                            <thead>
                                <tr style="background-color: #007acc; color: white">
                                    <th>Passenger</th>
                                    <th>Detail</th>
                                    <th>Qty</th>
                                    <th>Price</th>
                                    <th>Discount</th>
                                    <th>Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptFee" runat="server" OnItemDataBound="rptFee_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trdata" runat="server">
                                            <td>
                                                <asp:Label ID="lblFeePassName" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblDetail" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblQty" runat="server"></asp:Label></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblPrice" runat="server"></asp:Label></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblDiscount" runat="server"></asp:Label></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblAmount" runat="server"></asp:Label></td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan="5" style="text-align: right">Total:</th>
                                    <th style="text-align: right;">
                                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></th>

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

            <div id="divOR_History" runat="server" visible="false" class="form-group">
                <div class="col-sm-12">
                    <h5 class="header smaller lighter blue">OR History</h5>
                    <div style=" width: 700px; margin-left :30px;">
                       <b><asp:Label ID="Label2" runat="server" Text="OR Date : "/>
                        <asp:Label ID="lblOR_Date" runat="server" Text=""/></b> 
                    </div>
                </div>
            </div>


            <h3 class="header smaller lighter blue"></h3>
            <div class="row" style="margin-bottom: 20px;">
                <div style="text-align: center">

                    <asp:Button runat="server" ID="btnSave" Text="Save" Height="40px" Width="80px" OnClick="btnSave_Click" CssClass="btn btn-primary disabled" Enabled="false" />
                    <asp:Button runat="server" ID="btnCancel"  Text="Cancel" Height="40px" Width="80px" OnClick="btnCancel_Click" CssClass="btn btn-default" Style="margin-left: 20px;" />
                </div>
                <div style="display: none;">
                    <asp:Button ID="btnSaveOR" runat="server" Text="Save and Print" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="btnSaveOR_Click" />
                </div>
            </div>


        </div>
    </div>

</asp:Content>
