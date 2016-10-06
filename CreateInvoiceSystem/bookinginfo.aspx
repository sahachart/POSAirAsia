<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="bookinginfo.aspx.cs" Inherits="CreateInvoiceSystem.bookinginfo" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txb_date_from]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'Images/x_office_calendar.png',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
            });            
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="padding-top: 20px; padding-right:15px;">
        <div class="panel panel-default">
        <div class="panel-heading">Criteria Search</div>
            <div class="row">
                <div class="panel-body col-xs-12">
                    <form class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                    <label for="tbx_booking_no" class="col-sm-4 control-label no-padding-right">Booking No </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="tbx_booking_no" placeholder="Booking Number..." runat="server" Width="150px"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        <asp:ImageButton ID="imgGetDataDB" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="imgGetDataDB_Click" />
                                        <asp:ImageButton ID="imgClearData" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" OnClick="imgClearData_Click" />
                                    </div>                                   
                                </div>
                                <div class="col-sm-6">
                                    <div class="col-sm-5 text-right">
                                        <asp:ImageButton ID="imgGetDataOnline" runat="server" ImageUrl="~/Images/database.png" Height="24px" Width="24px" OnClick="imgGetDataOnline_Click" />
                                    </div>
                                    <label for="imgGetDataOnline" class="col-sm-5 text-left no-padding-left">Get Data Online</label>
                                </div>
                            </div>                
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6"></div>  
                            <div class="col-sm-6">
                                <div class="col-sm-9 text-center">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Blue"></asp:Label><br />
                                    <asp:Label ID="errortext" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>  
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="row">
                <div class="panel-body col-xs-12">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                <label for="tbx_booking_date" class="col-sm-4 control-label no-padding-right">Booking Date </label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="tbx_booking_date" placeholder="dd/mm/yyyy" runat="server" Width="100px"></asp:TextBox>
                                </div>
                            </div>
                        </div>              
                    </div>
                    <hr />

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5 class="header smaller lighter blue">ABB & Invoice Detail</h5>
                        </div>
                        <div class="col-sm-12">
                            <div style="display: none;"><asp:TextBox ID="txt_CreateABBMode" runat="server" ClientIDMode="Static"></asp:TextBox></div>
                            <div class="col-sm-12" style="margin-bottom: 5px;">                           
                                <asp:Button ID="btn_CreateAbb" runat="server" Text="Create ABB" Enabled="false" CssClass="btn btn-default" Width="100px" Height="40px" Font-Size="12px" ClientIDMode="Static" OnClick="btn_CreateAbb_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_CreateInvoice" runat="server" Text="Full Tax Invoice" Enabled="false" CssClass="btn btn-default" Width="100px" Height="40px" Font-Size="12px" OnClientClick="return false;" ClientIDMode="Static" />
                            </div>
                                <div class="col-sm-12">
                                <table id="table_ABB_Invoice" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                    <thead>
                                        <tr style="background-color: #007acc; color: white">
                                            <th width="5%" nowrap>Select</th>
                                            <th width="25%" nowrap>ABB No.</th>
                                            <th width="30%" nowrap>Payment Date</th>
                                            <th width="5%" nowrap>OR ABB</th>
                                            <th nowrap>Invoice No.</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%= TableABB_Invoice()%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div> 

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5 class="header smaller lighter blue">Flight Detail</h5>
                            <div class="col-sm-12">
                                <table id="table_flight" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                    <thead>
                                        <tr style="background-color: #007acc; color: white">
                                            <th nowrap>Flight No.</th>
                                            <th nowrap>Flight Date</th>
                                            <th>From</th>
                                            <th>To</th>
                                            <th nowrap>From Time</th>
                                            <th nowrap>Arrive Time</th>
                                            <th nowrap>ABB No</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%= TableFlight()%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>  
                                  
                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5 class="header smaller lighter blue">Passenger</h5>
                            <div class="col-sm-12">
                                <table id="table_passenger" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                    <thead>
                                        <tr style="background-color: #007acc; color: white">
                                            <th>No.</th>
                                            <th nowrap>Passenger Name</th>
                                            <th>PaxType</th>
                                            <th nowrap>ABB No</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%= TablePassenger()%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div> 

                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5 class="header smaller lighter blue">Fare</h5>

                            <div class="col-sm-12" style="overflow-x: auto;">
                                <div style="overflow-x: auto;">
                                    <table id="table_fare" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                        <thead>
                                            <tr style="background-color: #007acc; color: white">
                                                <th style="text-align: center;">Pax Type</th>
                                                <th style="text-align: center;">ChargeCode</th>
                                                <th style="text-align: center; min-width:50px;">Fare Date</th>
                                                <th style="text-align: center;">Quantity</th>
                                                <th style="text-align: center;">Currency</th>
                                                <th style="text-align: center;">Amount</th>
                                                <th style="text-align: center;">Ex.</th>
                                                <th style="text-align: center;">Discount (THB)</th>
                                                <th style="text-align: center;">Amount (THB)</th>
                                                <th style="text-align: center;">Total(THB)</th>
                                                <th style="text-align: center;">Group</th>
                                                <th style="text-align: center; min-width:80px;"">ABB No</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%= TableFare()%>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th colspan="9" style="text-align:right">Total:</th>
                                                <th style="text-align: right;">
                                                    <asp:Label ID="lblTatalFare" runat="server" Text="0.00"></asp:Label>
                                                </th>
                                                <th></th>
                                                <th></th>
                                         
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>  
                                     
                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5 class="header smaller lighter blue">Fee</h5>

                            <div class="col-sm-12" style="overflow-x: auto;">
                                <div style="overflow-x: auto;">
                                    <table id="table_fee" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                        <thead>
                                            <tr style="background-color: #007acc; color: white">
                                                <th style="text-align: center; width: 300px;">Passenger</th>
                                                <th style="text-align: center;">ChargeCode</th>
                                                <th style="text-align: center; min-width:50px;">Fee Date</th>
                                                <th style="text-align: center;">Quantity</th>
                                                <th style="text-align: center;">Currency</th>
                                                 <th style="text-align: center;">Ex.</th>
                                                <th style="text-align: center;">Amount</th>
                                               
                                                <th style="text-align: center;">Discount (THB)</th>
                                                <th style="text-align: center;">Amount (THB)</th>
                                                <th style="text-align: center;">Group</th>
                                                <th style="text-align: center; min-width: 80px;"">ABB No</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%= TableFee()%>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th colspan="6" style="text-align:right">Total:</th>
                                                <th style="text-align: right;">
                                                    <asp:Label ID="lblOriFeeTotal" runat="server" Text="Total"></asp:Label>
                                                </th>
                                                <th></th>
                                                <th style="text-align: right;"> <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label> </th>
                                                <th></th>
                                           
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <th colspan="8" style="text-align:right">Total Fare + Fee:</th>
                                                <th style="text-align: right;">
                                                    <asp:Label ID="lblTotalFareFee" runat="server" Text="0.00"></asp:Label>
                                                </th>
                                              <th></th>
                                              <th></th>
                                              
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>  
                    
                    <div class="form-group">
                        <div class="col-sm-12">
                            <h5 class="header smaller lighter blue">Payment</h5>
                            <div class="col-sm-12">
                                <table id="table_payment" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                    <thead>
                                        <tr style="background-color: #007acc; color: white">
                                            <th nowrap>Payment Agent</th>
                                            <th nowrap>Payment Date</th>
                                            <th nowrap>Payment Type</th>
                                            <th>Currency</th>
                                            <th nowrap>Payment Amt.</th>
                                            <th nowrap>Payment Amt.(THB)</th>
                                            <th>ABB No</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%= TablePayment()%>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="4" style="text-align:right">Total:</th>
                                            <th style="text-align: right;">
                                                <asp:Label ID="lbl_PaymentAll" runat="server" Text="Total"></asp:Label>
                                            </th>
                                            <th style="text-align: right;">
                                                <asp:Label ID="lbl_PaymentAllTHB" runat="server" Text="Total"></asp:Label>
                                            </th>
                                            <th></th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>  
                </div>       
            </div>
        </div>
    </div>
    
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/Bootstrap/bootstrap.min.js"></script>
    <script src="Scripts/Bootstrap/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            convertDateTime = function (dt) {
                var jsDate = new Date(dt * 1000);
                return jsDate.getDay.toLocaleString() + "/" + jsDate.getMonth.toLocaleString() + "/" + jsDate.getYear.toLocaleString();
            };
            GetBooking = function (key) {
                $.ajax({
                    url: 'servicepos.asmx/GetBooking',
                    type: "POST",
                    cache: false,
                    asyn: false,
                    data: "{ 'key':'" + key + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != undefined) {
                            var jsDate = new Date(parseInt(data.d.Booking_Date));
                            var output = jsDate.getDate() + "/" + (jsDate.getMonth() + 1) + "/" + (jsDate.getFullYear() + 543);
                            $('#MainContent_tbx_booking_date').val(data.d.Booking_Date);
                        }
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        console.log(xmlHttpRequest.responseText);
                        console.log(textStatus);
                        console.log(errorThrown);
                    }
                });
            };
            //$('#table_fee').DataTable(
            //    {
            //        //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
            //        lengthChange: false,
            //        select: true,
            //        ordering: false,
            //        searching: false,
            //        scrollY: 200,
            //        paging: false,
            //        "bInfo": false,
            //        "footerCallback": function (row, data, start, end, display) {
            //            var api = this.api(), data;

            //            // Remove the formatting to get integer data for summation
            //            var intVal = function (i) {
            //                return typeof i === 'string' ?
            //                    i.replace(/[\$,]/g, '') * 1 :
            //                    typeof i === 'number' ?
            //                    i : 0;
            //            };

            //            if (api.column(1).data() == "Base Fare" || api.column(9).data() != "") {
            //                // Total over all pages
            //                total = api
            //                    .column(9)
            //                    .data()
            //                    .reduce(function (a, b) {
            //                        return intVal(a) + intVal(b);
            //                    }, 0);
            //            }

            //            if (api.column(1, { page: 'current' }).data() == "Base Fare" || api.column(9, { page: 'current' }).data() != "") {
            //                // Total over this page
            //                pageTotal = api
            //                    .column(9, { page: 'current' })
            //                    .data()
            //                    .reduce(function (a, b) {
            //                        return intVal(a) + intVal(b);
            //                    }, 0);
                            
            //            }

            //            // Update footer
            //            var ss ="";
            //            var tt = ss.number ;
            //            $(api.column(9).footer()).html(
            //                pageTotal.toLocaleString().toString("#,##0.00")
            //                //'$' + pageTotal + ' ( $' + total + ' total)'
            //            );
            //        }
            //    }
            // );
            $("#table_payment").DataTable(
                {
                    //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
                    lengthChange: false,
                    select: true,
                    ordering: false,
                    searching: false,
                    //scrollY: 300,
                    paging: false,
                    "bInfo": false
                }
             );
            $("#table_flight").DataTable(
                {
                    //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
                    lengthChange: false,
                    select: true,
                    ordering: false,
                    searching: false,
                    paging: false,
                    "bInfo": false
                    //scrollY: 300
                }
             );
            $("#table_passenger").DataTable(
                {
                    "lengthMenu": [[-1], ["All"]],
                    lengthChange: false,
                    select: true,
                    ordering: false,
                    searching: false,
                    paging: false,
                    "bInfo": false
                    //scrollY: 300
                }
             );
        });
        $(function () {
            $('#MainContent_imgGetDataDB').click(function () {
                if ($("#MainContent_tbx_booking_no").val() == "") {
                    alert('Please fill booking number.');
                    return false;
                }
            });

            popup = function (key) {

                var urltext = 'abbno.aspx?id=' + key;

                openPopupPreview(urltext, 'ABB', 900, 500);
                
                return false;
            };

            print_Invoice = function (key) {

                var urltext = "fulltax_form.aspx?INV_No=" + key + "&Mode=1";

                openPopupPreview(urltext, 'Invoice', 900, 500);

                return false;
            };

            $('#btn_CreateInvoice').click(function () {
                var ABB = "";
                var CheckItemLength = $('input[name="chk_ABB"]:checked').length;
                if (CheckItemLength == 0) {
                    alert("Please Select ABB !!!");
                    return false;
                }
                for (var i = 0; i < CheckItemLength; i++) {
                    ABB += $('input[name="chk_ABB"]:checked')[i].value + ",";
                }
                ABB = ABB.slice(0, -1);
                urltext = 'invoice_info.aspx?PNR_NO=' + $('#MainContent_tbx_booking_no').val() + '&key=' + ABB;
                window.location = urltext;
            });
        });

        function ORCreateABB(ABBs)
        {
            if (confirm('Found ABB No ' + ABBs + ' not print \nPlease select \n"Yes" if you want to OR All ABB and create new ABB\n"No" is create ABB for excess amount.')) {
                $('#txt_CreateABBMode').val('OR');
            }
            else {
                $('#txt_CreateABBMode').val('NEW');
            }
            $('#btn_CreateAbb').click();
        }

        function ORABB(ABBNo) {
            $.ajax({
                url: 'servicepos.asmx/ORABB',
                type: "POST",
                cache: false,
                asyn: false,
                data: { 'ABBNo': ABBNo },
                //contentType: "application/json; charset=utf-8",
                dataType: "xml",
                success: function (data) {
                    alert(data.firstChild.textContent);
                    $('#MainContent_imgGetDataDB').click();
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log(xmlHttpRequest.responseText);
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });
        }
    </script>
</asp:Content>
