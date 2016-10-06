<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoiceCN_M_Info.aspx.cs" Inherits="CreateInvoiceSystem.InvoiceCN_M_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/dataTables.bootstrap.min.css" rel="stylesheet" />
    <%--<link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        $(function () {
            $("[id^=MainContent_tbx_date]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'Images/calender.png',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidfTranID" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="hidfCN_No" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="hidfCN_ID" runat="server" Value="" ClientIDMode="Static" />
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="7" style="height: 22px">Basic Information</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px">&nbsp;</td>
            <td class="tableform" style="width: 35%; text-align: left">
                <asp:RadioButtonList ID="rbl_typ_receipt" runat="server" RepeatDirection="Horizontal"  ReadOnly="True">
                    <asp:ListItem Selected="True" Value="R">&nbsp;Receipt&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="RT">&nbsp;Receipt/Tax Invoice&nbsp;&nbsp;</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="tableform" style="width: 115px"></td>
            <td class="tableform" style="text-align: left; width: 58px;"></td>
            <td class="tableform" style="width: 90px"></td>
            <td class="tableform" style="text-align: left"></td>
            <td style="width: 30px;">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px">Branch</td>
            <td class="tableform" style="width: 349px; text-align: left">
                <asp:DropDownList ID="ddl_station" runat="server" Width="150px" BackColor="#FFFF99" ReadOnly="True"></asp:DropDownList>&nbsp;&nbsp; 
                Slip Message :&nbsp;<asp:DropDownList ID="ddl_slip" runat="server" Width="120px" BackColor="#FFFF99" ReadOnly="True"></asp:DropDownList>

            </td>
            <td class="tableform" style="width: 115px">&nbsp;</td>
            <td class="tableform" style="text-align: left; width: 58px;">&nbsp;</td>
            <td class="tableform" style="width: 90px"></td>
            <td class="tableform" style="text-align: left"></td>
            <td style="width: 30px;">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px; height: 27px;">Slip Header</td>
            <td class="tableform" rowspan="4">
                <asp:TextBox ID="txt_SlipHeader" runat="server" ClientIDMode="Static" TextMode="MultiLine" ReadOnly="True" BackColor="#FFFF99" Width="420px" Height="108px"></asp:TextBox>
            </td>
            <%--<td class="tableform" style="width: 349px; text-align: left; height: 27px;">
                <asp:TextBox ID="tbx_branch_name" runat="server" BackColor="#FFFF99" ReadOnly="True" ForeColor="Black" Width="100%"></asp:TextBox>
            </td>--%>
            <td class="tableform" style="width: 115px; height: 27px;">POS No</td>
            <td class="tableform" style="text-align: left; width: 58px; height: 27px;">
                <asp:TextBox ID="tbx_pos_no" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 90px; height: 27px;"></td>
            <td class="tableform" style="text-align: left; height: 27px;"></td>
            <td style="width: 30px; height: 27px;"></td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px"><%--Address1--%></td>
            <%--<td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_branch_address1" runat="server" Width="100%" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
            </td>--%>
            <td class="tableform" style="width: 115px">Chashier No</td>
            <td class="tableform" style="text-align: left; width: 58px;">
                <asp:TextBox ID="tbx_cashier_no" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 90px"></td>
            <td class="tableform" style="text-align: left"></td>
            <td style="width: 30px;">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px"><%--Address2--%></td>
            <%--<td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_branch_address2" runat="server" Width="100%" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
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
            <td class="tableform" style="width: 141px"><%--Address3--%></td>
            <%--<td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_branch_address3" runat="server" Width="100%" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
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
            <td class="tableform" style="width: 141px">Tax ID</td>
            <td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_branch_taxid" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True" MaxLength="13">1480900005000</asp:TextBox>
            </td>
            <td class="tableform" style="width: 115px">&nbsp;</td>
            <td class="tableform" style="text-align: left; width: 58px;">&nbsp;</td>
            <td class="tableform" style="width: 90px"></td>
            <td class="tableform" style="text-align: left"></td>
            <td style="width: 30px;">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px">Booking Name</td>
            <td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_booking_name" runat="server" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 115px">Receipt No.</td>
            <td class="tableform" style="text-align: left; width: 58px;">
                <asp:TextBox ID="tbx_receipt_no" runat="server" BackColor="#FFFF99" ForeColor="Black" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 90px">Receipt Date</td>
            <td class="tableform" style="text-align: left">
                <asp:TextBox ID="tbx_date_receipt" runat="server" BackColor="#FFFF99" ReadOnly="True" Width="120px"></asp:TextBox>
            </td>
            <td style="width: 30px;">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px">Address1</td>
            <td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_booking_address1" runat="server" MaxLength="255" Width="100%" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 115px">Booking No.</td>
            <td class="tableform" style="text-align: left; width: 58px;">
                <asp:TextBox ID="tbx_booking_no" runat="server" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 90px">Booking Date</td>
            <td class="tableform" style="text-align: left">
                <asp:TextBox ID="tbx_date_booking" runat="server" BackColor="#FFFF99" ReadOnly="True" Width="120px"></asp:TextBox>
            </td>
            <td style="width: 30px;">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px">Address2</td>
            <td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_booking_address2" runat="server" MaxLength="255" Width="100%" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 115px">Route</td>
            <td class="tableform" style="text-align: left;" colspan="3" rowspan="2">
                <asp:TextBox ID="tbx_route" runat="server" Width="100%" Rows="2" TextMode="MultiLine" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td style="width: 30px;">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px">Address3</td>
            <td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_booking_address3" runat="server" MaxLength="255" Width="100%" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 115px">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px">Tax ID</td>
            <td class="tableform" style="width: 349px; text-align: left">
                <asp:TextBox ID="tbx_booking_taxid" runat="server" MaxLength="13" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 115px">Remark</td>
            <td class="tableform" style="text-align: left" colspan="3" rowspan="2">
                <asp:TextBox ID="tbx_remark" runat="server" Width="100%" Rows="2" TextMode="MultiLine" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td style="width: 30px;"></td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px;">Passenger Name</td>
            <td class="tableform" style="width: 349px; text-align: left; height: 24px;" rowspan="2">
                <asp:TextBox ID="tbx_passenger_1" runat="server" Width="100%" Rows="2" TextMode="MultiLine" BackColor="#FFFF99" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="tableform" style="width: 115px;">&nbsp;</td>
            <td class="tableform" style="text-align: left;">&nbsp;</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px; height: 27px;">&nbsp;</td>
            <td class="tableform" style="width: 115px; text-align: left; height: 27px;">&nbsp;</td>
            <td class="tableform" style="width: 90px; height: 27px;"></td>
            <td class="tableform" style="text-align: left; height: 27px;" colspan="3"></td>
        </tr>
        <tr>
            <td class="tableform" style="width: 141px; height: 27px;"></td>
            <td class="tableform" style="width: 115px; text-align: left; height: 27px;">&nbsp;</td>
            <td class="tableform" style="width: 90px; height: 27px;">Status :
                <asp:Label ID="lblStatus" runat="server" Text="Full Tax" ClientIDMode="Static"></asp:Label></td>
            <td class="tableform" style="text-align: left; height: 27px;" colspan="3">
                
            </td>
        </tr>
        <tbody>
            <tr>
                <td class="headlabel" colspan="7">Payment Information</td>
            </tr>

            <tr>
                <td colspan="7">
                    <table id="tb_detail" class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <%--                                <th style="width: 30px" class="text-center">Edit</th>
                                <th style="width: 30px" class="text-center">Del</th>--%>
                                <th class="text-center">No</th>
                                <th class="text-center" style="width: 120px">Fee Type</th>
                                <th style="width: 500px">Fee Detail</th>
                                <th class="text-center">Qty</th>
                                <th class="text-center">Unit</th>
                                <th class="text-right">Amount(THB)</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <td class="text-right" colspan="5" style="font-weight: bold">Total</td>
                                <td class="text-right" style="font-weight: bold">
                                    <asp:TextBox runat="server" ID="lbl_amt_total" Style="text-align: right; font-size: small;" Text="0.00" BackColor="#FFFF99" ReadOnly="True" CssClass="ui-priority-primary"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-right" colspan="5" style="font-weight: bold">VAT&nbsp;<asp:DropDownList runat="server" ID="ddl_vat"></asp:DropDownList></td>
                                <td class="text-right" style="font-weight: bold">
                                    <asp:TextBox runat="server" ID="lbl_amt_vat" Style="text-align: right; font-size: small;" Text="0.00" BackColor="#FFFF99" ReadOnly="True" CssClass="ui-priority-primary"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-right" colspan="5" style="font-weight: bold">Grand Total</td>
                                <td class="text-right" style="font-weight: bold">
                                    <asp:TextBox runat="server" ID="lbl_amt_grandtotal" Style="text-align: right; font-size: small;" Text="0.00" BackColor="#FFFF99" ReadOnly="True" CssClass="ui-priority-primary"></asp:TextBox>
                                </td>
                            </tr>
                        </tfoot>
                        <tbody id="LogDetail">
                            <tr id="rowempty">
                                <td colspan="8" style="text-align: center;">No data found.</td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="4">
                                <center>
                                    <asp:RadioButtonList ID="rbl_pay" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Cash" Selected="True">&nbsp;Cash&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="Credit">&nbsp;Credit Card No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </center>
                            </td>
                            <td>&nbsp;<asp:TextBox ID="tbx_card_no" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td>&nbsp;Bank&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="tbx_bank" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divCN_History" runat="server" visible="false" class="form-group">
                <div class="col-sm-12">
                    <h5 class="header smaller lighter blue">CN History</h5>
                    <div style="overflow-x: auto; width: 700px;">
                        <table class="table table-striped table-bordered" cellspacing="0" width="100%">
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveNPrint" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <h3 class="header smaller lighter blue"></h3>
    <div class="row">
        <div style="text-align: center">
            <asp:Button runat="server" ID="Save" OnClientClick="return false;" Text="Save" Height="40px" Width="80px" Enabled="false" CssClass="btn btn-default disabled" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnCancel" OnClientClick="return false;" Text="Cancel" Height="40px" Width="80px" ClientIDMode="Static" CssClass="btn btn-default" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="Button_print" OnClientClick="return false;" Text="Print" Height="40px" Width="80px" Enabled="false" CssClass="btn btn-default disabled"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input id="btnCN" type="button" value="Request CN" class="btn btn-primary" />
        </div>

        <div style="display: none;">
            <asp:Button ID="btnSaveNPrint" runat="server" Text="Save and Print" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="btnSaveNPrint_Click" />
        </div>
    </div>
    <div id="dialog-data">
        <form class="form-horizontal">
            <div class="row">
                <label class="hidden" id="FORM_STATE"></label>
                <label class="hidden" id="lbl_rowindex"></label>
                <label class="hidden" id="FORM_MODE"></label>
                <div class="col-xs-12 col-sm-12">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <label class="col-sm-4">Fee type :</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_patment_type" runat="server" Width="180px"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <label class="col-sm-4">Description :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox TextMode="MultiLine" Columns="40" Rows="3" ID="tbx_desc" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <label class="col-sm-4">Quantity :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbx_qty" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <label class="col-sm-4">Prices/Unit :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbx_unit" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <div id="dialog-formInvoice" style="display: none">
        <iframe id="pdfObject" src="" width="100%" height="100%"></iframe>
    </div>

    <div id="dialog-CN" title="Set Full Tax to CN">
        <div class="container col-lg-12">
            <div class="row" style="margin-top: 20px;">
                <div class="col-lg-12">
                    CN Reason
                    <div style="float: right; width: 300px;">
                        <asp:DropDownList  ID="ddlReason" runat="server" ClientIDMode="Static" CssClass="form-control "></asp:DropDownList>
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
            $('#dialog-data').hide();
            $("#dialog-CN").hide();
            var mode = urlParam('mode', window.location.href);
            var val = urlParam('key', window.location.href);
            DataInfo = function (key) {
                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    url: 'servicepos.asmx/GetInvoiceBycode',
                    data: JSON.stringify({ key: key }),
                    dataType: 'json',
                    success: function (data) {
                        //alert(data.d);
                        var info = data.d.info;
                        var detail = data.d.detail;

                        if (info != null) {
                            //alert('found data.');
                            $('#hidfTranID').val(info.TransactionId);
                            $('#MainContent_tbx_receipt_no').val(info.Receipt_no);
                            //$('#MainContent_tbx_branch_name').val(info.Branch_name);
                            $('#MainContent_tbx_pos_no').val(info.POS_Code);
                            $('#txt_SlipHeader').val(info.SlipHeader);
                            //$('#MainContent_tbx_branch_address1').val(info.Branch_address1);
                            //$('#MainContent_tbx_branch_address2').val(info.Branch_address2);
                            //$('#MainContent_tbx_branch_address3').val(info.Branch_address3);
                            $('#MainContent_tbx_cashier_no').val(info.Cashier_no);
                            $('#MainContent_tbx_branch_taxid').val(info.Branch_Tax_ID);
                            $('#MainContent_tbx_create_by').val(info.Create_by);
                            $('#MainContent_tbx_create_date').val(formatDate(info.Create_date));
                            $('#MainContent_tbx_update_by').val(info.Update_by);
                            $('#MainContent_tbx_update_date').val(formatDate(info.Update_date));
                            $('#MainContent_tbx_booking_name').val(info.Booking_name);
                            $('#MainContent_tbx_booking_address1').val(info.Customer_address1);
                            $('#MainContent_tbx_booking_address2').val(info.Customer_address2);
                            $('#MainContent_tbx_booking_address3').val(info.Customer_address3);
                            $('#MainContent_tbx_booking_taxid').val(info.Customer_Tax_ID);
                            $('#MainContent_tbx_booking_no').val(info.Booking_no);
                            $('#MainContent_tbx_route').val(info.Route);
                            $('#MainContent_tbx_remark').val(info.Remark);
                            $('#MainContent_tbx_date_receipt').val(formatDate(info.Receipt_date));
                            $('#MainContent_tbx_date_booking').val(formatDate(info.Booking_date));
                            $('#MainContent_tbx_card_no').val(info.Card_no);
                            $('#MainContent_tbx_bank').val(info.Bank);
                            $('#MainContent_tbx_passenger_1').val(info.Passenger_name);

                            $('#MainContent_ddl_station').val(info.Station_Code);
                            $("#MainContent_ddl_station").trigger('update');

                            $('#MainContent_ddl_slip').val(info.SlipMessage_Code);
                            $("#MainContent_ddl_slip").trigger('update');

                            $('#MainContent_ddl_vat').val(info.VAT_CODE);
                            $("#MainContent_ddl_vat").trigger('update');
                            $("#MainContent_ddl_vat").disabled = true;

                            RadionButtonSelectedValueSet('', info.Receipt_type, 'MainContent_rbl_typ_receipt');
                            RadionButtonSelectedValueSet('', info.Payment_type, 'MainContent_rbl_pay');

                            $('#MainContent_lbl_amt_total').val(format_amount(info.Amount));
                            $('#MainContent_lbl_amt_vat').val(format_amount(info.Vat));
                            $('#MainContent_lbl_amt_grandtotal').val(format_amount(info.Net_balance));

                            
                            if (info.Status != null && info.Status != '')
                                $('#lblStatus').text(info.Status);


                            DataDetail(detail);
                            
                            if (info.CN_ID != '') {
                                $('#hidfCN_ID').val(info.CN_ID);
                                var cn_id = info.CN_ID;
                                $.ajax({
                                    url: 'servicepos.asmx/GetInvoiceCN',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: { 'cn_id': cn_id },
                                    success: function (data) {
                                        $('#hidfCN_No').val(data.childNodes['0'].textContent);
                                    },
                                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                                        console.log(xmlHttpRequest.responseText);
                                        console.log(textStatus);
                                        console.log(errorThrown);
                                    }

                                });
                            }





                        };
                    }
                });

            };
            formatDate = function (obj) {
                var milli = obj.replace(/\/Date\((-?\d+)\)\//, '$1');
                var d = new Date(parseInt(milli));
                var sd = d.getDate();
                var sm = d.getMonth() + 1;
                var sy = d.getFullYear();

                return sd.toString() + '/' + sm.toString() + '/' + sy.toString();//.getDate + '/' + d.getMonth + '/' + d.getFullYear;
            };
            DataDetail = function (obj) {
                initialrow();
                for (var i = 0; i < obj.length; i++) {
                    var index = $("#LogDetail").children("tr").length;
                    var colCode = "<td  style='text-align:center;'>" + obj[i].Flight_Fee_Code + "</td>";
                    var colName = "<td>" + obj[i].Flight_Fee_Name + "</td>";
                    var colAmt = "<td style='text-align:right;'>" + format_amount(parseFloat(obj[i].AmountTH).toLocaleString()) + "</td>";
                    var colIndex = "<td style='text-align:right;'>" + (i + 1) + "</td>";
                    var btn = "btnRemove_" + index;
                    var btn$ = "#btnRemove_" + index;
                    var colUnit = "<td style='text-align:right;'>" + format_amount(parseFloat(obj[i].UnitQTY).toLocaleString()) + "</td>";
                    var colQty = "<td style='text-align:center;'>" + obj[i].Qty + "</td>";
                    var colButton = ''; //= "<td><a href='' id='" + index + "' class='editor_edit'>Edit</a></td><td><a href='' class='editor_remove'>Delete</a></td>";
                    var item = "<tr>" + colButton + colIndex + colCode + colName + colQty + colUnit + colAmt + "</tr>";
                    $("#tb_detail").append(item);
                }
                return true;
            };
            InitialForm = function () {
                //$('#MainContent_tbx_update_date').val('xxx');
                //mode = urlParam('mode',window.location.href);
                document.getElementById("MainContent_tbx_card_no").disabled = true;
                document.getElementById("MainContent_tbx_bank").disabled = true;
                document.getElementById("MainContent_tbx_card_no").readOnly = true;
                document.getElementById("MainContent_tbx_bank").readOnly = true;
                $('#FORM_MODE').val(mode);

            };
            InitialForm();
            var initial = 0;
            // New record
            $('a.editor_create').on('click', function (e) {
                e.preventDefault();
                alert("new");
            });
            // Edit record
            $('#tb_detail').on('click', 'a.editor_edit', function (e) {
                e.preventDefault();
                //alert("edit");
                $('#FORM_STATE').val('edit');
                //FORM_STATE
                var id = this.id;
                $('#lbl_rowindex').val(id);
                popup_form();
            });
            // Delete a record
            $('#tb_detail').on('click', 'a.editor_remove', function (e) {
                e.preventDefault();
                $(this).parent().parent().remove();
                var count = $('#LogDetail').children('tr').length;
                var count = $('#LogDetail').children('tr').length;
                if (count == 0) {
                    initial = 0;
                    $("#tb_detail tbody").append(
			            "<tr>" +
			            "<td colspan='8' style='text-align:center;'>No data found.</td>" +
			            "</tr>");
                    $('#MainContent_lbl_amt_total').val("0.00");
                    $('#MainContent_lbl_amt_vat').val("0.00");
                    $('#MainContent_lbl_amt_grandtotal').val("0.00");
                    return;
                }
                update_rowindex();
                CalulateAmtTax();
            });
            $('#addbtn').click(function () {
                $('#FORM_STATE').val('add');
                popup_form();
            });
            initialrow = function () {
                if (initial == 0) {
                    initial = 1;
                    var count = $('#LogDetail').children('tr').length;
                    if (count > 0) {
                        $('#tb_detail tbody tr').remove();
                    }
                }
            };
            initial_data = function () {
                $("#MainContent_ddl_patment_type").val('');
                $("#MainContent_ddl_patment_type").trigger('update');
                $("#MainContent_tbx_qty").val('');
                $("#MainContent_tbx_desc").val('');
                $("#MainContent_tbx_unit").val('');
            };
            GetDistrictToList = function () {
                initialrow();
                var index = $("#LogDetail").children("tr").length;
                var str_type = $('#MainContent_ddl_patment_type').val();
                var str_desc = $('#MainContent_tbx_desc').val();
                var qty = $('#MainContent_tbx_qty').val();
                var unit = $('#MainContent_tbx_unit').val();
                var str_amt = (parseFloat(qty.replace(',', '')) * parseFloat(unit.replace(',', ''))).toLocaleString();
                var res_amtfm = format_amount(str_amt);

                var isduplicate = CheckDuplicate(str_type);
                //var colCode = "<td>" + str_type + "</td>";
                if (isduplicate) {
                    alert('Existing data in list.');
                    return false;
                }
                var colCode = "<td  style='text-align:center;'>" + str_type + "</td>";
                var colName = "<td>" + str_desc + "</td>";
                var colAmt = "<td style='text-align:right;'>" + res_amtfm + "</td>";
                var colIndex = "<td style='text-align:right;'>" + (index + 1) + "</td>";
                var btn = "btnRemove_" + index;
                var btn$ = "#btnRemove_" + index;
                var colUnit = "<td style='text-align:right;'>" + format_amount(parseFloat(unit).toLocaleString()) + "</td>";
                var colQty = "<td style='text-align:center;'>" + qty + "</td>";
                var colButton = "<td><a href='' id='" + index + "' class='editor_edit'>Edit</a></td><td><a href='' class='editor_remove'>Delete</a></td>";
                var item = "<tr>" + colButton + colIndex + colCode + colName + colQty + colUnit + colAmt + "</tr>";
                $("#tb_detail").append(item);
                return true;
            }
            getDataRow = function (index) {
                var table = document.getElementById('LogDetail'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;
                cells = rows[index].getElementsByTagName('td');
                var code = cells[3].innerHTML;
                var desc = cells[4].innerHTML;
                var qty = cells[5].innerHTML.replace(",", "");
                var price = cells[6].innerHTML.replace(",", "");
                var amount = cells[7].innerHTML.replace(",", "");

                $("#MainContent_ddl_patment_type").val(code);
                $("#MainContent_ddl_patment_type").trigger('update');
                $("#MainContent_tbx_qty").val(qty);
                $("#MainContent_tbx_desc").val(desc);
                $("#MainContent_tbx_unit").val(price);

            };
            updaterow = function () {
                var index = $('#lbl_rowindex').val();
                var table = document.getElementById('LogDetail'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;
                cells = rows[index].getElementsByTagName('td');
                var row_index = $("#LogDetail").children("tr").length;
                var str_type = $('#MainContent_ddl_patment_type').val();
                var str_desc = $('#MainContent_tbx_desc').val();
                var qty = $('#MainContent_tbx_qty').val().replace(",", "");
                var unit = $('#MainContent_tbx_unit').val().replace(",", "");
                var str_amt = (parseFloat(qty.replace(',', '')) * parseFloat(unit.replace(',', ''))).toLocaleString();
                var res_amtfm = format_amount(str_amt);

                var isduplicate = CheckDuplicate(str_type);
                if (isduplicate) {
                    alert('Existing data in list.');
                    return false;
                }
                cells[3].innerHTML = $("#MainContent_ddl_patment_type").val();
                cells[4].innerHTML = $("#MainContent_tbx_desc").val();
                cells[5].innerHTML = $("#MainContent_tbx_qty").val();
                cells[6].innerHTML = format_amount(parseFloat(unit).toLocaleString());
                cells[7].innerHTML = res_amtfm;
                update_rowindex();
                return true;
            };
            format_amount = function (amt) {
                var str_amt = amt.toLocaleString();
                var str_amtsl = str_amt.split('.');
                if (str_amtsl.length <= 1) {
                    str_amt = str_amtsl[0] + ".00";
                } else {
                    if (str_amtsl[1].length == 1) {
                        str_amt = str_amtsl[0] + "." + str_amtsl[1] + "0";
                    }
                }
                return str_amt;
            };
            CheckDuplicate = function (_code) {
                var _bool = false;
                var table = document.getElementById('LogDetail'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;
                var mode = $('#FORM_STATE').val();
                var r_index = $('#lbl_rowindex').val();;
                if (mode == 'edit') {
                    for (i = 0, j = rows.length; i < j; ++i) {
                        cells = rows[i].getElementsByTagName('td');
                        if (!cells.length) {
                            continue;
                        }
                        if (r_index != i) {
                            if (_code == cells[3].innerHTML) {
                                _bool = true;
                                break;
                            }
                        }
                    }
                } else if (mode == 'add') {
                    for (i = 0, j = rows.length; i < j; ++i) {
                        cells = rows[i].getElementsByTagName('td');
                        if (!cells.length) {
                            continue;
                        }

                        if (_code == cells[3].innerHTML) {
                            _bool = true;
                            break;
                        }
                    }
                }
                return _bool;
            }
            popup_form = function () {
                initial_data();
                //encodeURIComponent
                var mode = $('#FORM_STATE').val();
                if (mode == "edit") {
                    var index = $('#lbl_rowindex').val();
                    getDataRow(index);
                }
                $("#dialog-data").dialog({
                    resizable: false,
                    height: 400,
                    width: 800,
                    modal: true,
                    title: 'Payment Info',
                    open: function (event, ui) {
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    buttons: {
                        "Submit": function () {
                            var valid = validate_data();
                            if (valid) {
                                if (mode == "edit") {
                                    if (updaterow()) {
                                        $(this).dialog("close");
                                    }
                                } else if (mode == "add") {
                                    if (GetDistrictToList()) {
                                        $(this).dialog("close");
                                    }
                                }
                                CalulateAmtTax();
                            }
                        },
                        "Close": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            };
            update_rowindex = function () {
                var table = document.getElementById('LogDetail'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;

                for (i = 0, j = rows.length; i < j; ++i) {
                    cells = rows[i].getElementsByTagName('td');
                    if (!cells.length) {
                        continue;
                    }
                    cells[0].innerHTML = "<a href='' id='" + i + "' class='editor_edit'>Edit</a>";
                    cells[2].innerHTML = i + 1;
                }
            };
            CalulateAmtTax = function () {
                var index = $('#lbl_rowindex').val();
                var vat = parseFloat($('#MainContent_ddl_vat :selected').text());
                // $("#selDEPT_CODE :selected").text();
                var table = document.getElementById('LogDetail'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;
                var amt = '';
                var total = 0;
                for (i = 0, j = rows.length; i < j; ++i) {
                    cells = rows[i].getElementsByTagName('td');
                    if (!cells.length) {
                        continue;
                    }
                    amt = cells[7].innerHTML;
                    total += parseFloat(amt.replace(',', ''));
                }
                var res_total = format_amount(total);
                var res_vat = format_amount(total * (vat / 100));
                var res_net = format_amount(total + (total * (vat / 100)));

                $('#MainContent_lbl_amt_total').val(res_total);
                $('#MainContent_lbl_amt_vat').val(res_vat);
                $('#MainContent_lbl_amt_grandtotal').val(res_net);

            };
            validate_data = function () {
                var bool = true;
                var str_type = $('#MainContent_ddl_patment_type').val();
                var str_desc = $('#MainContent_tbx_desc').val();
                var qty = $('#MainContent_tbx_qty').val();
                var unit = $('#MainContent_tbx_unit').val();
                if (str_type == "") {
                    alert('Please selected data...');
                    return false;
                }
                if (qty == "") {
                    alert('Please fill data...');
                    return false;
                }
                if (unit == "") {
                    alert('Please fill data...');
                    return false;
                }
                return bool;
            };
            $('#MainContent_ddl_slip').change(function () {
                var id = $('#MainContent_ddl_slip').val();
                if (id > 0) {
                    getSlipMessage(id);
                }
            });
            $('#MainContent_ddl_vat').change(function () {
                var vat = $('#MainContent_ddl_vat :selected').text();
                if (vat != '' && vat != undefined) {
                    CalulateAmtTax();
                }
            });
            getSlipMessage = function (key) {

                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    url: 'servicepos.asmx/GetMstSlipMessageDetail',
                    data: JSON.stringify({ key: key }),
                    dataType: 'json',
                    success: function (data) {
                        //alert(data.d);
                        var dt = data.d;
                        if (dt.length > 0) {
                            var comp
                            if (dt[1].Descriptions != "") {
                                comp = dt[1].Descriptions;
                            }
                            var add1;
                            if (dt[2].Descriptions != "") {
                                add1 = dt[2].Descriptions;
                            }
                            var add2;
                            if (dt.length > 3) {

                                if (dt[3].Descriptions != "") {
                                    add2 = dt[3].Descriptions;
                                }
                            }
                            var add3;
                            if (dt.length > 4) {

                                if (dt[4].Descriptions != "") {
                                    add3 = dt[4].Descriptions;
                                }
                            }
                            //$('#MainContent_tbx_branch_name').val(comp);
                            //$('#MainContent_tbx_branch_address1').val(add1);
                            //$('#MainContent_tbx_branch_address2').val(add2);
                            //$('#MainContent_tbx_branch_address3').val(add3);
                        } else {
                            //$('#MainContent_tbx_branch_name').val("");
                            //$('#MainContent_tbx_branch_address1').val("");
                            //$('#MainContent_tbx_branch_address2').val("");
                            //$('#MainContent_tbx_branch_address3').val("");
                        }
                    }
                });
            };

            var model = new Array();
            var count = 0;
            getDataTable = function () {
                var table = document.getElementById('LogDetail'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;
                if (model != null) {
                    model = null;
                    model = new Array();
                    count = 0;
                }

                for (i = 0, j = rows.length; i < j; ++i) {
                    cells = rows[i].getElementsByTagName('td');
                    if (!cells.length) {
                        continue;
                    }
                    var code = cells[3].innerHTML;
                    var desc = cells[4].innerHTML;
                    var qty = cells[5].innerHTML.replace(",", "");
                    var price = cells[6].innerHTML.replace(",", "");
                    var amount = cells[7].innerHTML.replace(",", "");
                    try {
                        model[count] = {
                            Receipt_no: $('#MainContent_tbx_receipt_no').val(),
                            Flight_Fee_Code: code,
                            Flight_Fee_Name: desc,
                            Other: '',
                            Qty: qty,
                            UnitQTY: price,
                            AmountTH: amount
                        };
                    }
                    catch (err) {
                    }
                    count++;
                }
                return model;
            };
            var model_info = new Array();
            getDataInfo = function () {
                var inv_type = ValidationSelected('MainContent_rbl_typ_receipt');
                var pay_type = ValidationSelected('MainContent_rbl_pay');
                model_info = {
                    //TransactionId: null,
                    Receipt_no: $('#MainContent_tbx_receipt_no').val(),
                    Receipt_type: inv_type,
                    Station_Code: $('#MainContent_ddl_station').val(),
                    SlipMessage_Code: $('#MainContent_ddl_slip').val(),
                    //Branch_name: $('#MainContent_tbx_branch_name').val(),
                    //Branch_address1: $('#MainContent_tbx_branch_address1').val(),
                    //Branch_address2: $('#MainContent_tbx_branch_address2').val(),
                    //Branch_address3: $('#MainContent_tbx_branch_address3').val(),
                    Branch_Tax_ID: $('#MainContent_tbx_branch_taxid').val(),
                    POS_Code: $('#MainContent_tbx_pos_no').val(),
                    Cashier_no: $('#MainContent_tbx_cashier_no').val(),
                    Booking_name: $('#MainContent_tbx_booking_name').val(),
                    Booking_no: $('#MainContent_tbx_booking_no').val(),
                    Customer_address1: $('#MainContent_tbx_booking_address1').val(),
                    Customer_address2: $('#MainContent_tbx_booking_address2').val(),
                    Customer_address3: $('#MainContent_tbx_booking_address3').val(),
                    Customer_Tax_ID: $('#MainContent_tbx_booking_taxid').val(),
                    Passenger_name: $('#MainContent_tbx_passenger_1').val(),
                    Route: $('#MainContent_tbx_route').val(),
                    Remark: $('#MainContent_tbx_remark').val(),
                    Create_by: $('#MainContent_tbx_cashier_no').val(),
                    Update_by: $('#MainContent_tbx_cashier_no').val(),
                    VAT_CODE: $('#MainContent_ddl_vat').val(),
                    Amount: parseFloat($('#MainContent_lbl_amt_total').val().replace(',', '')),//$('#').val(),
                    Vat: parseFloat($('#MainContent_lbl_amt_vat').val().replace(',', '')),//$('#').val(),
                    Net_balance: parseFloat($('#MainContent_lbl_amt_grandtotal').val().replace(',', '')),// $('#').val(),
                    Payment_type: pay_type,
                    Card_no: (pay_type.trim() == 'Cash' ? '' : $('#MainContent_tbx_card_no').val()),
                    Bank: (pay_type.trim() == 'Cash' ? '' : $('#MainContent_tbx_bank').val()),
                    Document_State: '1',
                    IsPrint: false,
                    IsActive: true,

                    Receipt_date: setReturnDate($('#MainContent_tbx_date_receipt').val()),
                    //Create_date: setReturnDate($('#MainContent_tbx_create_date').val()),
                    //Update_date: setReturnDate($('#MainContent_tbx_update_date').val()),
                    Booking_date: setReturnDate($('#MainContent_tbx_date_booking').val()),

                };
                return model_info;
            };
            ValidationSelected = function (ctrl) {
                //var radioList = "MainContent_rbl_gndr_code";
                var radioList = ctrl;
                var radioButtonList = document.getElementById(radioList);
                var listItems = radioButtonList.getElementsByTagName("input");
                for (var i = 0; i < listItems.length; i++) {
                    if (listItems[i].checked) {
                        return listItems[i].value;
                        break;
                    }
                }
            };
            //$('#MainContent_Save').click(function () {
            //    //alert('test');
            //    var valid = validate();
            //    if (valid) {
            //        var info = getDataInfo();
            //        var list = getDataTable();
            //        if (list == null) {
            //            alert('No data found in Payment Information.');
            //            return false;
            //        }
            //        try {

            //            $.ajax({
            //                url: 'servicepos.asmx/SaveInvoice',
            //                type: "POST",
            //                cache: false,
            //                data: JSON.stringify({ oinfo: info, olist: list, mode: mode }),
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "json",
            //                success: function (data) {
            //                    if (data.d.IsSuccessfull) {
            //                        //dialog.dialog("close");
            //                        alert(data.d.Message);
            //                        if (mode == "add") {
            //                            $('#MainContent_tbx_receipt_no').val(data.d.Message);
            //                            window.location.href = "billinvoice.aspx?mode=edit&key=" + data.d.Message;
            //                        }
            //                        //$('#MainContent_ImageButton2').click();
            //                    }
            //                    else {
            //                        msg = data.d.Message;
            //                        alert(msg);
            //                    }

            //                },
            //                error: function (xmlHttpRequest, textStatus, errorThrown) {
            //                    console.log(xmlHttpRequest.responseText);
            //                    console.log(textStatus);
            //                    console.log(errorThrown);
            //                }
            //            });
            //        } catch (ex) {

            //        }
            //    }
            //});
            validate = function () {
                var bool = true;
                var station = $("#MainContent_ddl_station").val();
                if (station == "") {
                    alert('Please selected the station.');
                    setFocus("MainContent_ddl_station");
                    return false;
                }
                var slip = $("#MainContent_ddl_slip").val();
                if (slip == "") {
                    alert('Please selected the slip header.');
                    setFocus("MainContent_ddl_slip");
                    return false;
                }
                var pos_no = $("#MainContent_tbx_pos_no").val();
                if (pos_no == "") {
                    alert('POS Number not found.');
                    setFocus("MainContent_tbx_pos_no");
                    return false;
                }
                var cashier = $("#MainContent_tbx_cashier_no").val();
                if (cashier == "") {
                    alert('User operation not found.');
                    setFocus("MainContent_tbx_cashier_no");
                    return false;
                }
                var comp = $("#MainContent_tbx_branch_name").val();
                if (comp == "") {
                    alert('Branch not found data.');
                    setFocus("MainContent_tbx_branch_name");
                    return false;
                }
                var rcp_date = $("#MainContent_tbx_date_receipt").val();
                if (rcp_date == "") {
                    alert('Please fill data.');
                    setFocus("MainContent_tbx_date_receipt");
                    return false;
                }
                var bk_date = $("#MainContent_tbx_date_booking").val();
                if (rcp_date == "") {
                    alert('Please fill data.');
                    setFocus("MainContent_tbx_date_booking");
                    return false;
                }
                var bk_name = $("#MainContent_tbx_booking_name").val();
                if (bk_name == "") {
                    alert('Please fill data.');
                    setFocus("MainContent_tbx_booking_name");
                    return false;
                }
                var bk_no = $("#MainContent_tbx_booking_no").val();
                if (bk_no == "") {
                    alert('Please fill data.');
                    setFocus("MainContent_tbx_booking_no");
                    return false;
                }

                var bk_taxid = $("#MainContent_tbx_booking_taxid").val();
                if (bk_taxid == "") {
                    alert('Please fill data.');
                    setFocus("MainContent_tbx_booking_taxid");
                    return false;
                }
                var passenger = $("#MainContent_tbx_passenger_1").val();
                if (passenger == "") {
                    alert('Please fill data.');
                    setFocus("MainContent_tbx_passenger_1");
                    return false;
                }
                var inv_type = ValidationSelected('MainContent_rbl_typ_receipt');
                var pay_type = ValidationSelected('MainContent_rbl_pay');
                if (pay_type.trim() == 'Credit') {
                    var card_no = $("#MainContent_tbx_card_no").val();
                    if (card_no == "") {
                        alert('Please fill data.');
                        setFocus("MainContent_tbx_card_no");
                        return false;
                    }
                    var bank = $("#MainContent_tbx_bank").val();
                    if (bank == "") {
                        alert('Please fill data.');
                        setFocus("MainContent_tbx_bank");
                        return false;
                    }
                }
                if (mode == 'edit') {
                    var rcp_no = $("#MainContent_tbx_receipt_no").val();
                    if (rcp_no == "") {
                        alert('Please fill receipt number.');
                        setFocus("MainContent_tbx_receipt_no");
                        return false;
                    }
                }
                //MainContent_ddl_vat
                var vat = $("#MainContent_ddl_vat").val();
                if (vat == "") {
                    alert('Please selected the vat.');
                    setFocus("MainContent_ddl_vat");
                    return false;
                }
                return bool;
            };

            initial_mode = function () {
                if (mode == 'add') {
                    //disable_control('MainContent_Button_print', true);
                } else if (mode == 'view') {
                    //document.getElementById("MainContent_tbx_card_no").disabled = true;
                    //document.getElementById("MainContent_tbx_bank").readOnly = true;
                    disable_control('MainContent_tbx_card_no', true);
                    disable_control('MainContent_tbx_bank', true);
                    disable_control('MainContent_rbl_typ_receipt', true);
                    disable_control('MainContent_ddl_station', true);
                    disable_control('MainContent_ddl_slip', true);
                    disable_control('MainContent_tbx_booking_name', true);
                    disable_control('MainContent_tbx_booking_address1', true);
                    disable_control('MainContent_tbx_booking_address2', true);
                    disable_control('MainContent_tbx_booking_address3', true);
                    disable_control('MainContent_tbx_booking_taxid', true);
                    disable_control('MainContent_tbx_booking_no', true);
                    disable_control('MainContent_tbx_passenger_1', true);
                    disable_control('MainContent_rbl_pay', true);
                    disable_control('MainContent_Save', true);
                    disable_control('MainContent_Save', true);
                    disable_control('btnCancel', true);
                    disable_control('MainContent_Button2', true);
                    $('.ui-datepicker-trigger').addClass('hidden');
                    //MainContent_tbx_route,MainContent_tbx_remark,addbtn,MainContent_ddl_patment_type,MainContent_tbx_desc,MainContent_tbx_qty,MainContent_tbx_unit
                    disable_control('addbtn', true);
                    $('#addbtn').addClass('hidden');
                    disable_control('MainContent_tbx_route', true);
                    disable_control('MainContent_tbx_remark', true);
                    //disable_control('MainContent_Button_print', false);

                } else {
                    //disable_control('MainContent_Button_print', false);

                }
                if (mode != 'add') {
                    DataInfo(val);
                }
            };
            disable_control = function (ctrl, status) {
                document.getElementById(ctrl).disabled = status;
                document.getElementById(ctrl).readOnly = status;
            };
            initial_mode();
            function RadionButtonSelectedValueSet(name, SelectdValue, ctrl) {
                var radioList = ctrl;//"MainContent_rbl_pay";
                var radioButtonList = document.getElementById(radioList);
                var listItems = radioButtonList.getElementsByTagName("input");
                for (var i = 0; i < listItems.length; i++) {
                    var evalue = listItems[i].value
                    if (evalue.trim() == SelectdValue.trim()) {
                        listItems[i].checked = true;
                    }
                }
            }
            $('#btnCancel').click(function () {
                //history.back();
                window.location.href = 'InvoiceCN_M_List.aspx';

            });
            //$('#MainContent_Button_print').click(function () {
            //    var key = $('#MainContent_tbx_receipt_no').val();
            //    if (key != "") {
            //        popup_preview(key);
            //    } else {
            //        return;
            //    }
            //});

            popup_preview = function (key) {
                var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
                var urltext = 'billinvoice_form.aspx?id=' + key;
                if (is_chrome) {
                    window.open(urltext);
                    wcppDetectOnSuccess(urltext);
                } else {
                    var content = "<object id='pdfObject' style='overflow-x: hidden;' type='application/pdf' data='" + urltext + "' width='100%' height='100%' />";
                    $("#dialog-formInvoice").html(content);
                    $("#dialog-formInvoice").dialog({
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
                $('#dialog-CN').hide();

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
                $('#dialog-CN').dialog('close');
                var info;
                if ($('#hidfCN_ID').val() != '') {
                    info = {
                        CN_ID: $('#hidfCN_ID').val(),
                        TransactionId: $('#hidfTranID').val(),
                        INV_No: $('#MainContent_tbx_receipt_no').val(),
                        PNR_No: $('#MainContent_tbx_booking_no').val(),
                        CN_NO: $('#hidfCN_No').val(),
                        CNReason_Id: $('#ddlReason').val(),
                        mode  : 'edit'
                    }
                }
                else {
                    info = {
                        TransactionId: $('#hidfTranID').val(),
                        INV_No: $('#MainContent_tbx_receipt_no').val(),
                        PNR_No: $('#MainContent_tbx_booking_no').val(),
                        CN_NO: $('#hidfCN_No').val(),
                        CNReason_Id: $('#ddlReason').val(),
                        mode :'add'
                    }
                }
              

               
                try {
               
                    $.ajax({
                        url: 'servicepos.asmx/SaveInvoiceCN',
                        type: "POST",
                        data: JSON.stringify({oinfo: info }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.IsSuccessfull) {
                                //dialog.dialog("close");
                                alert("บันทึกสำเร็จ");
                                $('#dialog-CN').dialog('close');
                                //window.open('CreditNote_M_Form.aspx?inv_no=' + $('#MainContent_tbx_receipt_no').val(), '_blank');
                                //window.location.href = window.location.href;

                                $('#lblStatus').text('CN');
                                $('#btnSaveNPrint').click();
                                var urltext = 'CreditNote_M_Form.aspx?inv_no=' + $('#MainContent_tbx_receipt_no').val();
                                openPopupPreview(urltext, 'Credit Note Preview', 900, 500);

                                //if (mode == "add") {
                                //    $('#MainContent_tbx_receipt_no').val(data.d.Message);
                                //    window.location.href = "billinvoice.aspx?mode=edit&key=" + data.d.Message;
                                //}
                                //$('#MainContent_ImageButton2').click();
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
                    alert(ex.message);
                }

            });


        });
        //var t = $('#tb_detail').DataTable();
        //var counter = 1;
        //$("#tb_detail").DataTable(
        //    {
        //        //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
        //        lengthChange: false,
        //        select: true,
        //        ordering: false,
        //        searching: false,
        //        //scrollY: 300,
        //        paging: false,
        //        "bInfo": false,

        //    }
        // );
        $('#MainContent_tbx_qty').keydown(function (event) {
            var str = $('#MainContent_tbx_qty').val().split('.');
            if (event.key == '.' && str.length == 2) {
                event.preventDefault();
                return;
            };
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
                    || event.keyCode == 27 || event.keyCode == 13
                    || (event.keyCode == 65 && event.ctrlKey === true)
                    || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            }
            else if ((event.keyCode < 96 || event.keyCode > 105) && ((event.keyCode < 48 || event.keyCode > 57) ||
                (event.key != 0 && event.key != 1 && event.key != 2 && event.key != 3 && event.key != 4 && event.key != 5
                && event.key != 6 && event.key != 7 && event.key != 8 && event.key != 9))) {
                event.preventDefault();
                return;
            }
        });
        $('#MainContent_tbx_unit').keydown(function (event) {
            var str = $('#MainContent_tbx_unit').val().split('.');
            if (event.key == '.' && str.length == 2) {
                event.preventDefault();
                return;
            };
            if (event.keyCode == 190 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
                    || event.keyCode == 27 || event.keyCode == 13
                    || (event.keyCode == 65 && event.ctrlKey === true)
                    || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            }
            else if ((event.keyCode < 96 || event.keyCode > 105) && ((event.keyCode < 48 || event.keyCode > 57) ||
                (event.key != 0 && event.key != 1 && event.key != 2 && event.key != 3 && event.key != 4 && event.key != 5
                && event.key != 6 && event.key != 7 && event.key != 8 && event.key != 9)) && event.key != '.') {
                event.preventDefault();
                return;
            }
        });
        $('[id^=MainContent_rbl_pay]').on('change', function () {
            var clt = $(this);
            var id = clt.val().trim();
            if (id == 'Cash') {
                document.getElementById("MainContent_tbx_card_no").disabled = true;
                document.getElementById("MainContent_tbx_bank").disabled = true;
                document.getElementById("MainContent_tbx_card_no").readOnly = true;
                document.getElementById("MainContent_tbx_bank").readOnly = true;
            } else if (id == 'Credit') {
                document.getElementById("MainContent_tbx_card_no").disabled = false;
                document.getElementById("MainContent_tbx_bank").disabled = false;
                document.getElementById("MainContent_tbx_card_no").readOnly = false;
                document.getElementById("MainContent_tbx_bank").readOnly = false;
            }
        });



    </script>
</asp:Content>
