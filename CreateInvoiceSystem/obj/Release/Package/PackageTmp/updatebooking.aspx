<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="updatebooking.aspx.cs" Inherits="CreateInvoiceSystem.updatebooking" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="Content/jquery.ui.timepicker.css" rel="stylesheet" />
    <script src="Scripts/jquery.dataTables.min.js"></script>

    <style>
        .hil {
            background-color:#61BBF8;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
   
    <asp:HiddenField ID="hidfCurrentDate" runat="server" ClientIDMode="Static" Value="" />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
        <ProgressTemplate>
            <div style="width: 110px; top: 40%; left: 43%; position: fixed; z-index: 9999999;">
                <img alt="" style="width: 200px; height: 200px;" src="../images/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="container-fluid" style="padding-top: 20px; padding-left: 5px;">
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
                                                <asp:ImageButton ID="imgDatabaseLoad" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="imgDatabaseLoad_Click" ClientIDMode="Static" />
                                                <asp:ImageButton ID="imgRefresh" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" OnClick="imgRefresh_Click" ClientIDMode="Static" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="col-sm-5 text-right">
                                                <asp:ImageButton ID="imgSkyspeedLoad" runat="server" ImageUrl="~/Images/database.png" Height="24px" Width="24px" OnClick="imgSkyspeedLoad_Click" ClientIDMode="Static" />
                                            </div>
                                            <label for="ImageButton3" class="col-sm-5 text-left no-padding-left">Get Data Online</label>
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
                                    <div class="col-sm-6">
                                        <%--<div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                        </div>--%>
                                        
                                    </div>
                                </div>
                                
                            </div>
                            <hr />

                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:Button ID="btnCreateABB" runat="server" Text="Create ABB" Enabled="false" CssClass="btn btn-default" Width="120px" Style="margin-right:15px;" OnClick="btnCreateABB_Click" />
                                    <asp:Button ID="btn_CreateInvoice" runat="server" Text="Full Tax Invoice" Enabled="false" CssClass="btn btn-default" Width="120px"  OnClientClick="return false;" ClientIDMode="Static" />
                                    <%-- <label for="tbx_abb" style="margin-top: 5px;" class="control-label no-padding-right">ABB No : </label>--%>

                                    <%--  <%= GetLabelLinkList() %>--%>
                                    <a target="" href="#" onclick=""></a>
                                </div>
                            </div>


               
                                <div class="col-sm-12">
                                    <table id="table_ABB_Invoice" class="table  table-bordered" cellspacing="0" width="100%">
                                        <thead>
                                            <tr style="background-color: #007acc; color: white">
                                                <th width="5%" >Select</th>
                                                <th width="25%" nowrap>ABB No.</th>
                                                <th width="30%" nowrap>Payment Date</th>
                                                <th width="5%" nowrap>OR ABB</th>
                                                <th >Invoice No.</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                             <%= TableABB_Invoice()%>
                                        </tbody>
                                    </table>
                                </div>
                       



                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h5 class="header smaller lighter blue">Flight Detail</h5>
                                    <div class="col-sm-6">
                                        <asp:Image ID="imgAddFlight" runat="server" Style="width: 18px; cursor: pointer;" ImageUrl="Images/add.png" ClientIDMode="Static" />
                                        Add Flight
                                        <table id="table_flight" class="table  table-bordered" cellspacing="0" width="100%">
                                            <thead>
                                                <tr style="background-color: #007acc; color: white">
                                                    <th style="width: 20px;">Action</th>
                                                    <th>Flight No.</th>
                                                    <th>Flight Date</th>
                                                    <th>From</th>
                                                    <th>To</th>
                                                    <th>From Time</th>
                                                    <th>Arrive Time</th>
                                                    <th>ABB No</th>
                                                    <th style="width: 20px;">Del</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptFlightDetail" runat="server" OnItemDataBound="rptFlightDetail_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <tr id="trdata" runat="server">
                                                            <td style="text-align: center;">
                                                                <asp:Image ID="imgAction" runat="server" Style="width: 18px; cursor: pointer;" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFlightNo" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblFlightDate" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblFrom" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblTo" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblFromTime" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblArriveTime" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblFlightAbbNo" runat="server" Text="Label"></asp:Label></td>
                                                            <td>
                                                                <asp:Image ID="imgDel" runat="server" ImageUrl="Images/meanicons_24-20.png" Style="cursor: pointer; width: 18px;" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <%--         <%= TableFlight()%>--%>
                                            </tbody>
                                        </table>

                                    </div>
                                    <div class="col-sm-6">
                                    <asp:Image ID="Image4" runat="server" Style="width: 18px; cursor: pointer;visibility:hidden;" ImageUrl="Images/add.png" ClientIDMode="Static" />

                                        <table id="table_flightsky" class="table  table-bordered" cellspacing="0" width="100%">
                                            <thead>
                                                <tr style="background-color: #007acc; color: white">
                                                    <th>Choose</th>
                                                    <th>Flight No.</th>
                                                    <th>Flight Date</th>
                                                    <th>From</th>
                                                    <th>To</th>
                                                    <th>From Time</th>
                                                    <th>Arrive Time</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%= TableFlightSky()%>
                                            </tbody>
                                        </table>
                                        <asp:HiddenField ID="fare1" runat="server" Value="" />
                                        <asp:HiddenField ID="fare2" runat="server" Value="" />

                                        <asp:HiddenField ID="hidfDelFlight" runat="server" Value="" ClientIDMode="Static" />
                                        <asp:Button ID="btnFlightSkyUpdate" runat="server" Text="movepaybtn" Style="display: none" OnClick="btnFlightSkyUpdate_Click" ClientIDMode="Static" />
                                        <asp:Button ID="btnDelFlight" runat="server" Text="delpaybtn" Style="display: none" OnClick="btnDelFlight_Click" ClientIDMode="Static" />
                                        <asp:Button ID="btnSaveFlight" runat="server" Text="updatedata" Style="display: none" OnClick="btnSaveFlight_Click" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hidfFlightSaveVal" runat="server" Value="" ClientIDMode="Static" />

                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h5 class="header smaller lighter blue">Passenger</h5>

                                    <div class="col-sm-6">
                                     <asp:Image ID="imgAddPass" runat="server" Style="width: 18px; cursor: pointer;" ImageUrl="Images/add.png" ClientIDMode="Static" />
                                        Add Passenger
                                        <table id="table_passenger" class="table  table-bordered" cellspacing="0" width="100%">
                                            <thead>
                                                <tr style="background-color: #007acc; color: white">
                                                    <th style="width: 20px;">Action</th>
                                                    <th style="width: 20px">No.</th>
                                                    <th>Passenger Name</th>
                                                    <th>ABB No</th>
                                                    <th style="width: 20px;">Del</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptPassenger" runat="server" OnItemDataBound="rptPassenger_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <tr id="trdata" runat="server">
                                                            <td style="text-align: center;">
                                                                <asp:Image ID="imgAction" runat="server" Style="width: 18px; cursor: pointer;" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblNo" runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblPassName" runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblPassAbbNo" runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:Image ID="imgDel" runat="server" ImageUrl="Images/meanicons_24-20.png" Style="cursor: pointer; width: 18px;" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>


                                                <%--   <%= TablePassenger()%>--%>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-sm-6">
                                    <asp:Image ID="Image3" runat="server" Style="width: 18px; cursor: pointer;visibility:hidden;" ImageUrl="Images/add.png" ClientIDMode="Static" />

                                        <table id="table_passengersky" class="table  table-bordered" cellspacing="0" width="100%">
                                            <thead>
                                                <tr style="background-color: #007acc; color: white">
                                                    <th style="width: 20px">Choose</th>
                                                    <th style="width: 20px">No.</th>
                                                    <th style='display: none;'>Id</th>
                                                    <th>Passenger Name</th>
                                                    <th style='display: none;'>PaxType</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%= TablePassengersky()%>
                                            </tbody>
                                        </table>
                                        <asp:HiddenField ID="pass" runat="server" Value="" />
                                        <asp:HiddenField ID="hidfPassSaveVal" runat="server" Value="" ClientIDMode="Static" />

                                        <asp:HiddenField ID="hidfPassSkyUpdate" runat="server" Value="" ClientIDMode="Static" />
                                        <asp:Button ID="btnPassSkyUpdate" runat="server" Text="" Style="display: none" OnClick="btnPassSkyUpdate_Click" ClientIDMode="Static" />
                                        <asp:Button ID="btnSavePass" runat="server" Text="movebtn" Style="display: none" OnClick="btnSavePass_Click" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hidfDelPass" runat="server" Value="" ClientIDMode="Static" />
                                        <asp:Button ID="btnDelPass" runat="server" Text="delbtn" Style="display: none" OnClick="btnDelPass_Click" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h5 class="header smaller lighter blue">Fare</h5>
                                    <div id="tb_Fare" class="col-sm-6" style="overflow-x: auto;">
                                        <asp:Image ID="imgAddFare" runat="server" Style="width: 18px; cursor: pointer;" ImageUrl="Images/add.png" ClientIDMode="Static" />Add Fare
                                        
                                        <div style="overflow-x: auto; width: 1100px;">
                                            <table id="table_fare" class="table  table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr style="background-color: #007acc; color: white">
                                                        <th style="width: 20px;">Action</th>
                                                        <th style="text-align: center; width: 50px;">Pax Type</th>
                                                        <th style="text-align: center; width: 70px;">ChargeCode</th>
                                                        <th style="text-align: center; width: 100px;">Fare Date</th>
                                                        <th style="text-align: center; width: 70px;">Currency</th>
                                                        <th style="text-align: center; width: 10px;">Ex.</th>
                                                        <th style="text-align: center;">Amount</th>
                                                        <th style="text-align: center;">Amount (THB)</th>
                                                        <th style="text-align: center; width: 10px;">Qty</th>
                                                        <th style="text-align: center;">Discount (THB)</th>
                                                        <th style="text-align: center;">Total(THB)</th>
                                                        <th style="width: 50px;">ABB No</th>
                                                        <th style="width: 20px;">Del</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptFare" runat="server" OnItemDataBound="rptFare_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr id="trdata" runat="server">
                                                                <td style="text-align: center;">
                                                                    <asp:Image ID="FareImgAction" runat="server" Style="width: 18px; cursor: pointer;" />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_FarePaxType" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_FareChargeCode" runat="server"></asp:Label>
                                                                </td>
                                                                 <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_FareCreateDate" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_FareCurrency" runat="server"></asp:Label>
                                                                </td>
                                                                
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_FareExchangeRateTH" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_FareAmount" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_FareAmountTH" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_FareQuantity" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_FareDiscountTH" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_FareTotalTH" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_FeeABBNo" runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:Image ID="FareImgDel" runat="server" ImageUrl="Images/meanicons_24-20.png" Style="cursor: pointer; width: 18px;" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th colspan="6" style="text-align: right">Total:</th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lbl_FareTotalFareAmountOri" runat="server" Text="0.00"></asp:Label>
                                                        </th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lbl_FareTotalFareAmountTH" runat="server" Text="0.00"></asp:Label>
                                                        </th>
                                                        <th></th>
                                                        <th></th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lbl_FareTotalFareTH" runat="server" Text="0.00"></asp:Label>
                                                        </th>
                                                        <th></th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                        <asp:Button ID="btn_DeleteFare" runat="server" Text="" Style="display: none" OnClick="btn_DeleteFare_Click" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hidfDelFare" runat="server" Value="" ClientIDMode="Static" />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Image ID="Image5" runat="server" Style="width: 18px; cursor: pointer; visibility: hidden" ImageUrl="Images/add.png" />
                                        <div id="tb_SkyFare" style="margin-left: 7px; overflow-x: auto;">
                                            <table id="table_faresky" class="table  table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr style="background-color: #007acc; color: white">
                                                        <th style="width: 20px;">Choose</th>
                                                        <th style="text-align: center; width: 50px;">Pax Type</th>
                                                        <th style="text-align: center; width: 70px;">ChargeCode</th>
                                                        <th style="text-align: center; width: 70px;">Currency</th>
                                                        <th style="text-align: center; width: 10px;">Ex.</th>
                                                        <th style="text-align: center;">Amount</th>
                                                        <th style="text-align: center;">Amount (THB)</th>
                                                        <th style="text-align: center; width: 10px;">Qty</th>
                                                        <th style="text-align: center;">Discount (THB)</th>
                                                        <th style="text-align: center;">Total(THB)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpt_SkyFare" runat="server" OnItemDataBound="rpt_SkyFare_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr id="trdata" runat="server">
                                                                <td style="text-align: center;">
                                                                    <asp:Image ID="SkyFareImgAction" runat="server" ImageUrl="Images/back-20.png" Style="width: 18px; cursor: pointer;" />
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_SkyFarePaxType" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_SkyFareChargeCode" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_SkyFareCurrency" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_SkyFareExchangeRateTH" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_SkyFareAmount" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_SkyFareAmountTH" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_SkyFareQuantity" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_SkyFareDiscountTH" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lbl_SkyFareTotalTH" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th colspan="5" style="text-align: right">Total:</th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lbl_TotalFareSkyAmountOri" runat="server" Text="0.00"></asp:Label>
                                                        </th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lbl_TotalFareSkyAmountTH" runat="server" Text="0.00"></asp:Label>
                                                        </th>
                                                        <th></th>
                                                        <th></th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lbl_TotalFareSkyTH" runat="server" Text="0.00"></asp:Label>
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hidfFareSkyUpdate" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:Button ID="btn_FareSkyUpdate" runat="server" Text="movebtn" Style="display: none" OnClick="btn_FareSkyUpdate_Click" ClientIDMode="Static" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h5 class="header smaller lighter blue">Fee</h5>
                                    <div class="col-sm-6" style="overflow-x: auto;">
                                        
                                        <asp:Image ID="imgAddFee" runat="server" Style="width: 18px; cursor: pointer;" ImageUrl="Images/add.png" ClientIDMode="Static" />
                                        Add Fee
                                       
                                        <div style="overflow-x: auto; width: 900px;">
                                            <table id="table_fee" class="table  table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr style="background-color: #007acc; color: white">
                                                        <th style="width: 20px;">Action</th>
                                                        <th style="width: 50px;">Passenger</th>
                                                        <th>Detail</th>
                                                        <th style="text-align: center; width: 100px;">Fee Date</th>
                                                        <th style="width: 10px;">Qty</th>
                                                        <th>Price</th>
                                                        <th>Discount</th>
                                                        <th style="width: 10px;">Ex.</th>
                                                        <th >Amount(THB)</th>
                                                        <th style="width: 50px;">ABB No</th>
                                                        <th style="width: 20px;">Del</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptFee" runat="server" OnItemDataBound="rptFee_OnItemDataBound">
                                                        <ItemTemplate>
                                                            <tr id="trdata" runat="server">
                                                                <td style="text-align: center;">
                                                                    <asp:Image ID="imgAction" runat="server" Style="width: 18px; cursor: pointer;" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblFeePassName" runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblDetail" runat="server"></asp:Label></td>
                                                              
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_FeeCreateDate" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblQty" runat="server"></asp:Label></td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lblPrice" runat="server"></asp:Label></td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lblDiscount" runat="server"></asp:Label></td>
                                                                <td style="text-align: right;  ">
                                                                    <asp:Label ID="lblEx" runat="server"></asp:Label></td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label></td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lblFeeABB_No" runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:Image ID="imgDel" runat="server" ImageUrl="Images/meanicons_24-20.png" Style="cursor: pointer; width: 18px;" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                    <%--       <%= TableFee()%>--%>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th colspan="8" style="text-align: right">Total:</th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label></th>
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
                                    <div class="col-sm-6">
                                        <asp:Image ID="Image1" runat="server" Style="width: 18px; cursor: pointer; visibility: hidden" ImageUrl="Images/add.png" />
                                        <div style="margin-left: 7px; overflow-x: auto">
                                            <table id="table_feesky" class="table  table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr style="background-color: #007acc; color: white">
                                                        <th>Choose</th>
                                                        <th  style="width: 50px;">Passenger</th>
                                                        <th>Detail</th>
                                                        <th>Qty</th>
                                                        <th>Price</th>
                                                        <th>Discount(THB)</th>
                                                        <th>Amount(THB)</th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <asp:Repeater ID="rptFeeSky" runat="server" OnItemDataBound="rptFeeSky_OnItemDataBound">
                                                        <ItemTemplate>
                                                            <tr id="trdata" runat="server">
                                                                <td style="text-align: center;">
                                                                    <asp:Image ID="imgAction" runat="server" ImageUrl="Images/back-20.png" Style="width: 18px; cursor: pointer;" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPassenger" runat="server"></asp:Label></td>
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
                                                    <%--  <%= TableFeesky()%>--%>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th colspan="6" style="text-align: right">Total:</th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lblTotalFeeSky" runat="server" Text="0.00"></asp:Label>
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                            </table>

                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hidfFeeSkyUpdate" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:Button ID="btnFeeSkyUpdate" runat="server" Text="movebtn" Style="display: none" OnClick="btnFeeSkyUpdate_Click" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hidfFeeSaveVal" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:Button ID="btnSaveFee" runat="server" Text="" Style="display: none" OnClick="btnSaveFee_Click" ClientIDMode="Static" />
                                    <asp:Button ID="btnDelFee" runat="server" Text="" Style="display: none" OnClick="btnDelFee_Click" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hidfDelFee" runat="server" Value="" ClientIDMode="Static" />

                                    <asp:HiddenField ID="hidfFareSaveVal" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:Button ID="btnSaveFare" runat="server" Text="" Style="display: none" OnClick="btnSaveFare_Click" ClientIDMode="Static" />




                                    <asp:HiddenField ID="fee" runat="server" Value="" />
                                    <asp:Button ID="Button8" runat="server" Text="movebtn" Style="display: none" OnClick="Button8_Click" />
                                    <asp:HiddenField ID="delfee" runat="server" Value="" />
                                    <asp:Button ID="Button9" runat="server" Text="delbtn" Style="display: none" OnClick="Button9_Click" />
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h5 class="header smaller lighter blue">Payment</h5>
                                    <div class="col-sm-6" style="overflow-x: auto;">
                                    <asp:Image ID="imgAddPay" runat="server" Style="width: 18px; cursor: pointer;" ImageUrl="Images/add.png" ClientIDMode="Static" />
                                        Add Payment
                                        <div style="overflow-x: auto; width: 1000px;">
                                            <table id="table_payment" class="table  table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr style="background-color: #007acc; color: white">
                                                        <th style="width: 20px;">Action</th>
                                                        <th style='display: none;'>ABB No</th>
                                                        <th nowrap>Payment Agent</th>
                                                        <th nowrap>Payment Date</th>
                                                        <th nowrap>Payment Type</th>
                                                        <th nowrap>Currency</th>
                                                        <th nowrap>Payment Amt.</th>
                                                        <th nowrap>Payment Amt.(THB)</th>
                                                        <th nowrap>ABB No</th>
                                                        <th style="width: 20px;">Del</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptPayment" runat="server" OnItemDataBound="rptPayment_OnItemDataBound">
                                                        <ItemTemplate>
                                                            <tr id="trdata" runat="server">
                                                                <td style="text-align: center;">
                                                                    <asp:Image ID="imgAction" runat="server" Style="width: 18px;" />
                                                                </td>
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
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lblPayAbbNo" runat="server"></asp:Label></td>
                                                                <td>
                                                                    <asp:Image ID="imgDel" runat="server" ImageUrl="Images/meanicons_24-20.png" Style="cursor: pointer; width: 18px;" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>



                                                    <%--<%= TablePayment()%>--%>
                                                </tbody>

                                                <tfoot>
                                                    <tr>
                                                        <th colspan="6" style="text-align: right">Total:</th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lblTotalPay" runat="server" Text="Total"></asp:Label></th>
                                                        <th></th>
                                                        <th></th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                    <asp:Image ID="Image2" runat="server" Style="width: 18px; cursor: pointer; visibility:hidden;" ImageUrl="Images/add.png" ClientIDMode="Static" />

                                        <div style="margin-left: 7px; overflow-x: auto">
                                            <table id="table_paymentsky" class="table  table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr style="background-color: #007acc; color: white">
                                                        <th>Choose</th>
                                                        <th nowrap>Payment Agent</th>
                                                        <th nowrap>Payment Date</th>
                                                        <th nowrap>Payment Type</th>
                                                        <th nowrap>Currency</th>
                                                        <th nowrap>Payment Amt.</th>
                                                        <th nowrap>Payment Amt.(THB)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%= TablePaymentsky() %>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th colspan="5" style="text-align: right">Total:</th>
                                                        <th style="text-align: right;">
                                                            <asp:Label ID="lblTotalPaySky" runat="server" Text="Total"></asp:Label></th>
                                                        <th></th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hidfPaySaveVal" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:Button ID="btnSavePayment" runat="server" Text="btnSavePayment" Style="display: none" OnClick="btnSavePayment_Click" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hidfPaySkyUpdate" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:Button ID="btnPaySkyUpdate" runat="server" Text="" Style="display: none" OnClick="btnPaySkyUpdate_Click" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hidfDelPay" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:Button ID="btnDelPay" runat="server" Text="btnDelPay" Style="display: none" OnClick="btnDelPay_Click" ClientIDMode="Static" />

                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 20px;">
                        <div style="margin: 0 auto; width: 250px;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="120px" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnCancel" Width="120px" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="display:none;">
                <asp:Button ID="btnORABB" runat="server" Text="OR Abb" OnClick="btnORABB_Click" ClientIDMode="Static" />

           
             <asp:HiddenField ID="hidfORABB" runat="server" ClientIDMode="Static" Value="" />
            <asp:HiddenField ID="hidfBookingID" runat="server" ClientIDMode="Static" Value="" />
            <asp:HiddenField ID="hidfTranID_New" runat="server" Value="" />
            <asp:HiddenField ID="transactionid" runat="server" Value="" />
                 </div>
            <div id="dialog-formABB" style="display: none">
                <iframe id="pdfObject" src="" width="100%" height="100%"></iframe>
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
                    $("#table_flightsky").DataTable(
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
                    //$("#table_passenger").DataTable(
                    //    {
                    //        "lengthMenu": [[-1], ["All"]],
                    //        lengthChange: false,
                    //        select: true,
                    //        ordering: false,
                    //        searching: false,
                    //        paging: false,
                    //        "bInfo": false
                    //    }
                    // );
                    $("#table_passengersky").DataTable(
                        {
                            "lengthMenu": [[-1], ["All"]],
                            lengthChange: false,
                            select: true,
                            ordering: false,
                            searching: false,
                            paging: false,
                            "bInfo": false
                        }
                     );
                });


                function pageLoad() {
                    

                    $(function () {

                        $("#dateFlightFrom").datepicker({
                            //showOn: 'button',
                            //buttonImageOnly: true,
                            //buttonImage: 'Images/calender.png',
                            dateFormat: 'dd/mm/yy',
                            changeMonth: true,
                            changeYear: true,
                        });

                        $("#dateFlightTo").datepicker({
                            //showOn: 'button',
                            //buttonImageOnly: true,
                            //buttonImage: 'Images/calender.png',
                            dateFormat: 'dd/mm/yy',
                            changeMonth: true,
                            changeYear: true,
                        });

                        $("#datePaymentDate").datepicker({
                            showOn: 'button',
                            buttonImageOnly: true,
                            buttonImage: 'Images/calender.png',
                            dateFormat: 'dd/mm/yy',
                            changeMonth: true,
                            changeYear: true,
                        });

                        $("#dialog-confirm-flight").hide();
                        $("#dialog-confirm-passenger").hide();
                        $("#dialog-confirm-fee").hide();
                        $("#dialog-confirm-fare").hide();
                        $("#dialog-confirm-payment").hide();

                        $('#imgDatabaseLoad').click(function () {
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



                        $('#imgAddFlight').click(function () {
                            if ($("#MainContent_tbx_booking_no").val() != '') {
                                var date = $('#hidfCurrentDate').val();
                                editflight('NEW','', '');
                            }
                        });

                        $('#imgAddPass').click(function () {
                            if ($("#MainContent_tbx_booking_no").val() != '') {
                                editpassenger('NEW','', '', '', '', '', '', '');
                            }
                        });

                        $('#imgAddFee').click(function () {
                            if ($("#MainContent_tbx_booking_no").val() != '') {
                                editfee('NEW','','', '-1', '', '', '', '', '', '', '', 'THB', '0', 'THB', '0','');
                            }
                        });


                        $('#imgAddFare').click(function () {
                            if ($("#MainContent_tbx_booking_no").val() != '') {
                                editfare('NEW', '', '', -1, '', '', '', '', '', '', '', 'THB', '0', 'THB', '0', '');
                            }
                        });

                        $('#imgAddPay').click(function () {
                            if ($("#MainContent_tbx_booking_no").val() != '') {
                                var date = $('#hidfCurrentDate').val();
                                editpayment('NEW','', '', '', '', '', '', '', date, 'THB', 0, 'THB', 0)
                            }
                        });


                        $('#tb_Fare').on('scroll', function (e) {
                            $('#tb_SkyFare').scrollLeft($('#tb_Fare').scrollLeft() * 0.2);
                        });

                    });




                }


                function ConfirmOR(abb_no) {
                    var s = confirm('คุณต้องการ OR ABB '+abb_no +' ไหม?');
                    if (s) {
                        $('#hidfORABB').val(abb_no);
                        $('#btnORABB').click();
                    }
                }


                function editflight(mode, trid, str) {

                    if (str == '') {
                        var date = $('#hidfCurrentDate').val();
                        $('#flightcode').val("FD");
                        $('#ftime').val("00:00");
                        $('#fto').val("00:00");
                        $('#dateFlightFrom').val(date);
                        $('#dateFlightTo').val(date);
                    }
                    else {
                        str = str.replace(/&quot;/g, '\"');
                        var data = JSON.parse(str);
                        $("#dialog-confirm-flight").hide();
                        $('#flhid').val(data.index);
                        $('#flightcode').val(data.CarrierCode);
                        $('#flightnumber').val(data.FlightNumber);
                        $('#departure').val(data.DepartureStation);
                        $('#arrv').val(data.ArrivalStation);
                        $('#ftime').val(data.STD_Time);
                        $('#fto').val(data.STA_Time);
                        $('#dateFlightFrom').val(data.STD_Date);
                        $('#dateFlightTo').val(data.STA_Date);
                        $('#ddlFlightAbbNo').val(data.ABBNo);
                        AddHighlight(trid);
                    }
                    $("#dialog-confirm-flight").dialog({
                        resizable: false,
                        height: 450,
                        width: 800,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 2000);
                            $('.ui-widget-overlay').css('z-index', 1000);
                            $('.ui-dialog-buttonpane').find('button:contains("Save")').addClass('btn btn-primary');
                            $('.ui-dialog-buttonpane').find('button:contains("Cancel")').addClass('btn btn-default');
                        },
                        close: function (event, ui) { RemoveHighlight(trid); },
                        title: 'Edit Flight',
                        buttons: {
                            "Save": function () {
                                if ($('#flightcode').val() == '') {
                                    alert('กรุณาระบุรหัส');
                                    return false;
                                }
                                if ($('#flightnumber').val() == '') {
                                    alert('กรุณาระบุรหัส');
                                    return false;
                                }
                                if ($('#departure').val() == '') {
                                    alert('กรุณาระบุ DepartureStation');
                                    return false;
                                }
                                if ($('#arrv').val() == '') {
                                    alert('กรุณาระบุ ArrivalStation');
                                    return false;
                                }
                                if ($('#ftime').val() == '') {
                                    alert('กรุณาระบุ เวลาออก');
                                    return false;
                                }
                                if ($('#fto').val() == '') {
                                    alert('กรุณาระบุ เวลาถึง');
                                    return false;
                                }
                                if ($('#dateFlightFrom').val() == '') {
                                    alert('กรุณาระบุ วันที่ออก');
                                    return false;
                                }
                                if ($('#dateFlightTo').val() == '') {
                                    alert('กรุณาระบุ วันที่ถึง');
                                    return false;
                                }

                                var chkTime = formatTime($('#ftime').val());
                                if (chkTime == false) {
                                    alert('กรุณาระบุ เวลาให้ถูกต้อง');
                                    return false;
                                }
                                else {
                                    $('#ftime').val(chkTime);
                                }

                                var chkTime = formatTime($('#fto').val());
                                if (chkTime == false) {
                                    alert('กรุณาระบุ เวลาให้ถูกต้อง');
                                    return false;
                                }
                                else {
                                    $('#fto').val(chkTime);
                                }

                                var strJson = [{
                                    "Mode": mode,
                                    "SegmentTId": segId,
                                    "CarrierCode": $('#flightcode').val(),
                                    "FlightNumber": $('#flightnumber').val(),
                                    "DepartureStation": $('#departure').val(),
                                    "ArrivalStation": $('#arrv').val(),
                                    "time_STD": $('#ftime').val(),
                                    "time_STA": $('#fto').val(),
                                    "date_STD": $('#dateFlightFrom').val(),
                                    "date_STA": $('#dateFlightTo').val(),
                                    "ABBNo": $('#ddlFlightAbbNo').val()
                                }];
                                var str = JSON.stringify(strJson);
                                $('#hidfFlightSaveVal').val(str);
                                $('#btnSaveFlight').click();
                                $(this).dialog("close");

                            },
                            Cancel: function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                }



                function editpassenger(mode,trid, index, passTid, passid, title, fname, lname, paxtype, abb_no) {
                    var header = mode + ' Passenger';

                    $("#dialog-confirm-passenger").hide();
                    $('#hidfPassid').val(passTid);
                    //$('#txtPassID').val(passid);
                    $('#txtPassTitle').val(title);
                    $('#txtPassFirstName').val(fname);
                    $('#txtPassLastName').val(lname);
                    $('#ddlPaxType').val(paxtype);
                    $('#ddlPassABBNo').val(abb_no);
                    AddHighlight(trid);
                    $("#dialog-confirm-passenger").dialog({
                        resizable: false,
                        height: 400,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 2000);
                            $('.ui-widget-overlay').css('z-index', 1000);
                            $('.ui-dialog-buttonpane').find('button:contains("Save")').addClass('btn btn-primary');
                            $('.ui-dialog-buttonpane').find('button:contains("Cancel")').addClass('btn btn-default');
                        },
                        close: function (event, ui) { RemoveHighlight(trid); },
                        title: header,
                        buttons: {
                            "Save": function () {
                                //if ($('#txtPassID').val() == '') {
                                //    alert('กรุณาระบุรหัส');
                                //    return false;
                                //}
                                if ($('#txtPassTitle').val() == '') {
                                    alert('กรุณาระบุคำนำหน้า');
                                    return false;
                                }
                                if ($('#txtPassFirstName').val() == '') {
                                    alert('กรุณาระบุชื่อ');
                                    return false;
                                }
                                if ($('#txtPassLastName').val() == '') {
                                    alert('กรุณาระบุนามสกุล');
                                    return false;
                                }
                                if ($('#ddlPaxType').val() == '') {
                                    alert('กรุณาระบุ Pax Type');
                                    return false;
                                }



                                var strJson = [{
                                    "Mode": mode,
                                    "PassengerTId": $('#hidfPassid').val(),
                                    //"PassengerId": $('#txtPassID').val(),
                                    "Title": $('#txtPassTitle').val(),
                                    "FirstName": $('#txtPassFirstName').val(),
                                    "LastName": $('#txtPassLastName').val(),
                                    "PaxType": $('#ddlPaxType').val(),
                                    "ABBNo": $('#ddlPassABBNo').val()
                                }];
                                var str = JSON.stringify(strJson);
                                $('#MainContent_pass').val(index);
                                $('#hidfPassSaveVal').val(str);
                                $('#btnSavePass').click();
                                $(this).dialog("close");

                            },
                            Cancel: function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                }




                function editfee(mode,trid, index, ServiceChargeTId, SegmentTId, PassengerId, ChargeType, ChargeCode, ChargeDetail, TicketCode, CollectType, CurrencyCode, Amount, ForeignCurrencyCode, ForeignAmount, abb_no) {
                     $("#dialog-confirm-fee").hide();

                     $('#ddlFeePassName').val(PassengerId);
                     $('#txtFeeChargeType').val(ChargeType);
                     $('#ddlFeeChargeCode').val(ChargeCode);
                     $('#txtFeeChargeDetail').val(ChargeDetail);
                     $('#txtFeeTicCode').val(TicketCode);
                     $('#txtFeeCollType').val(CollectType);
                     $('#txtFeeCurr').val(CurrencyCode);
                     $('#txtFeeAmount').val(Amount);
                     $('#txtFeeForeignCurrencyCode').val(ForeignCurrencyCode);
                     $('#txtFeeForeignAmount').val(ForeignAmount);
                     $('#ddlFeeAbbNo').val(abb_no);

                     AddHighlight(trid);
                     $("#dialog-confirm-fee").dialog({
                         resizable: false,
                         height: 500,
                         width: 750,
                         modal: true,
                         open: function (event, ui) {
                             $('.ui-dialog').css('z-index', 2000);
                             $('.ui-widget-overlay').css('z-index', 1000);
                             $('.ui-dialog-buttonpane').find('button:contains("Save")').addClass('btn btn-primary');
                             $('.ui-dialog-buttonpane').find('button:contains("Cancel")').addClass('btn btn-default');
                         },
                         close: function (event, ui) { RemoveHighlight(trid); },
                         title: 'Edit Fee',
                         buttons: {
                             "Save": function () {

                                 if ($('#ddlFeePassName').val() == '') {
                                     alert('กรุณาระบุ Passenger');
                                     return false;
                                 }
                                 if ($('#txtFeeChargeType').val() == '') {
                                     alert('กรุณาระบุ Charge Type');
                                     return false;
                                 }
                                 if ($('#ddlFeeChargeCode').val() == '') {
                                     alert('กรุณาระบุ Charge Code ');
                                     return false;
                                 }
                                 //if ($('#txtFeeTicCode').val() == '') {
                                 //    alert('กรุณาระบุ Ticket Code  ');
                                 //    return false;
                                 //}
                                 //if ($('#txtFeeChargeDetail').val() == '') {
                                 //    alert('กรุณาระบุ Charge Detail ');
                                 //    return false;
                                 //}
                                 //if ($('#txtFeeCollType').val() == '') {
                                 //    alert('กรุณาระบุ Collection Type  ');
                                 //    return false;
                                 //}
                                 if ($('#txtFeeCurr').val() == '') {
                                     alert('กรุณาระบุ Currency');
                                     return false;
                                 }
                                 if ($('#txtFeeAmount').val() == '') {
                                     alert('กรุณาระบุ Amount 1');
                                     return false;
                                 }

                                 if ($('#txtFeeForeignCurrencyCode').val() == '') {
                                     alert('กรุณาระบุ Foreign Currency');
                                     return false;
                                 }
                                 if ($('#txtFeeForeignAmount').val() == '') {
                                     alert('กรุณาระบุ Amount 2');
                                     return false;
                                 }

                                 var strJson = [{
                                     "index": index,
                                     "Mode": mode,
                                     "PassengerFeeServiceChargeTId": ServiceChargeTId,
                                     "PassengerId": $('#ddlFeePassName').val(),
                                     "ChargeType": $('#txtFeeChargeType').val(),
                                     "ChargeCode": $('#ddlFeeChargeCode').val(),
                                     "ChargeDetail": $('#txtFeeChargeDetail').val(),
                                     "TicketCode": $('#txtFeeTicCode').val(),
                                     "CollectType": $('#txtFeeCollType').val(),
                                     "CurrencyCode": $('#txtFeeCurr').val(),
                                     "Amount": $('#txtFeeAmount').val(),
                                     "ForeignCurrencyCode": $('#txtFeeForeignCurrencyCode').val(),
                                     "ForeignAmount": $('#txtFeeForeignAmount').val(),
                                     "ABBNo": $('#ddlFeeAbbNo').val(),
                                 }];

                                 var str = JSON.stringify(strJson);
                                 $('#hidfFeeSaveVal').val(str);
                                 $('#btnSaveFee').click();
                                 $(this).dialog("close");

                             },
                             Cancel: function () {
                                 $(this).dialog("close");
                             }
                         }
                     });
                 }


                function editfare(mode, trid, index, ServiceChargeTId, PaxFareTId, PaxType, ChargeType, ChargeCode, ChargeDetail, TicketCode, CollectType, CurrencyCode, Amount, ForeignCurrencyCode, ForeignAmount, abb_no) {
                     $("#dialog-confirm-fare").hide();
                     $('#ddlSegment').val(PaxFareTId);
                     //$('#ddl_FarePaxType').val(PaxType);
                     $('#txtFareChargeType').val(ChargeType);
                     $('#ddlFareChargeCode').val(ChargeCode);
                     $('#txtFareChargeDetail').val(ChargeDetail);
                     $('#txtFareTicket').val(TicketCode);
                     $('#txtFareCollectionType').val(CollectType);
                     $('#txtFareCurerency').val(CurrencyCode);
                     $('#txtFareAmount').val(Amount);
                     $('#txtFareForeignCurrency').val(ForeignCurrencyCode);
                     $('#txtFareForeignAmount').val(ForeignAmount);

                     $('#ddlFareAbbNo').val(abb_no);

                     AddHighlight(trid);

                     $("#dialog-confirm-fare").dialog({
                         resizable: false,
                         height: 550,
                         width: 750,
                         modal: true,
                         open: function (event, ui) {
                             $('.ui-dialog').css('z-index', 2000);
                             $('.ui-widget-overlay').css('z-index', 1000);
                             $('.ui-dialog-buttonpane').find('button:contains("Save")').addClass('btn btn-primary');
                             $('.ui-dialog-buttonpane').find('button:contains("Cancel")').addClass('btn btn-default');
                         },
                         close: function (event, ui) { RemoveHighlight(trid); },
                         title: 'Edit Fare',
                         buttons: {
                             "Save": function () {

                                 if ($('#ddlSegment').val() == '') {
                                     alert('กรุณาระบุ Flight');
                                     return false;
                                 }
                                 //if ($('#ddl_FarePaxType').val() == '') {
                                 //    alert('กรุณาระบุ PaxType');
                                 //    return false;
                                 //}
                                 if ($('#txtFareChargeType').val() == '') {
                                     alert('กรุณาระบุ Charge Type');
                                     return false;
                                 }
                                 //if ($('#ddlFareChargeCode').val() == '') {
                                 //    alert('กรุณาระบุ Charge Code ');
                                 //    return false;
                                 //}
                                 //if ($('#txtFareTicket').val() == '') {
                                 //    alert('กรุณาระบุ Ticket Code  ');
                                 //    return false;
                                 //}
                                 //if ($('#txtFareChargeDetail').val() == '') {
                                 //    alert('กรุณาระบุ Charge Detail ');
                                 //    return false;
                                 //}
                                 //if ($('#txtFareCollectionType').val() == '') {
                                 //    alert('กรุณาระบุ Collection Type  ');
                                 //    return false;
                                 //}
                                 if ($('#txtFareCurerency').val() == '') {
                                     alert('กรุณาระบุ Currency');
                                     return false;
                                 }
                                 if ($('#txtFareAmount').val() == '') {
                                     alert('กรุณาระบุ Amount 1');
                                     return false;
                                 }

                                 if ($('#txtFareForeignCurrency').val() == '') {
                                     alert('กรุณาระบุ Foreign Currency');
                                     return false;
                                 }
                                 if ($('#txtFareAmount').val() == '') {
                                     alert('กรุณาระบุ Amount 2');
                                     return false;
                                 }

                                 var strJson = [{
                                     "Mode": mode,
                                     "index": index,
                                     "PaxFareTId": $('#ddlSegment').val(),
                                     "PassengerFeeServiceChargeTId": ServiceChargeTId,
                                     //"PaxType": $('#ddl_FarePaxType').val(),
                                     //"PassengerId": $('#ddlFarePassenger').val(),
                                     //"PassengerName": $('#ddlFarePassenger').text(),
                                     "ChargeType": $('#txtFareChargeType').val(),
                                     "ChargeCode": $('#ddlFareChargeCode').val(),
                                     "ChargeDetail": $('#txtFareChargeDetail').val(),
                                     "TicketCode": $('#txtFareTicket').val(),
                                     "CollectType": $('#txtFareCollectionType').val(),
                                     "CurrencyCode": $('#txtFareCurerency').val(),
                                     "Amount": $('#txtFareAmount').val(),
                                     "ForeignCurrencyCode": $('#txtFareForeignCurrency').val(),
                                     "ForeignAmount": $('#txtFareForeignAmount').val(),
                                     "ABBNo": $('#ddlFareAbbNo').val(),
                                     
                                 }];

                                 var str = JSON.stringify(strJson);
                                 $('#hidfFareSaveVal').val(str);
                                 $('#btnSaveFare').click();
                                 $(this).dialog("close");

                             },
                             Cancel: function () {
                                 $(this).dialog("close");
                             }
                         }
                     });
                 }




                 function editpayment(mode,trid, index, payTID, payID, paycode, paytype, agen, paydate, curr, amt, collcurr, collamt,abb_no) {
                     $("#dialog-confirm-payment").hide();

                     $('#hidfPayid').val(payTID);
                     //$('#txtPaymentID').val(payID);
                     $('#txtPaymentCode').val(paycode);
                     $('#txtPaymentMehtodType').val(paytype);
                     $('#txtPaymentAgency').val(agen);
                     $('#datePaymentDate').val(paydate);

                     $('#txtPayCurrency').val(curr);
                     $('#txtPaymentAmount').val(amt);
                     $('#txtCollectedCurrency').val(collcurr);
                     $('#txtPayCollectedAmount').val(collamt);


                     $('#ddlPayAbbNo').val(abb_no);

                     AddHighlight(trid);
                     $("#dialog-confirm-payment").dialog({
                         resizable: false,
                         height: 400,
                         width: 800,
                         modal: true,
                         open: function (event, ui) {
                             $('.ui-dialog').css('z-index', 2000);
                             $('.ui-widget-overlay').css('z-index', 1000);
                             $('.ui-dialog-buttonpane').find('button:contains("Save")').addClass('btn btn-primary');
                             $('.ui-dialog-buttonpane').find('button:contains("Cancel")').addClass('btn btn-default');
                         },
                         close: function (event, ui) { RemoveHighlight(trid); },
                         title: 'Edit Payment',
                         buttons: {
                             "Save": function () {
                                 if ($('#txtPaymentCode').val() == '') {
                                     alert('กรุณาระบุ Payment Method Code');
                                     return false;
                                 }
                                 if ($('#txtPaymentMehtodType').val() == '') {
                                     alert('กรุณาระบุ Payment Method Type');
                                     return false;
                                 }
                                 if ($('#txtPaymentAgency').val() == '') {
                                     alert('กรุณาระบุ AgentCode');
                                     return false;
                                 }
                                 if ($('#datePaymentDate').val() == '') {
                                     alert('กรุณาระบุ Approval Date');
                                     return false;
                                 }
                                 if ($('#txtPayCurrency').val() == '') {
                                     alert('กรุณาระบุ Currency Code');
                                     return false;
                                 }

                                 if ($('#txtPaymentAmount').val() == '') {
                                     alert('กรุณาระบุ Payment Amount');
                                     return false;
                                 }

                                 if ($('#txtCollectedCurrency').val() == '') {
                                     alert('กรุณาระบุ Collected Currency');
                                     return false;
                                 }


                                 if ($('#txtPayCollectedAmount').val() == '') {
                                     alert('กรุณาระบุ Collected Amount');
                                     return false;
                                 }



                                 var strJson = [{
                                     "Mode": mode,
                                     "PaymentTId": $('#hidfPayid').val(),
                                     //"PaymentID": $('#txtPaymentID').val(),
                                     "PaymentMethodCode": $('#txtPaymentCode').val(),
                                     "PaymentMethodType": $('#txtPaymentMehtodType').val(),
                                     "AgentCode": $('#txtPaymentAgency').val(),
                                     "ApprovalDate": $('#datePaymentDate').val(),
                                     "CurrencyCode": $('#txtPayCurrency').val(),
                                     "PaymentAmount": $('#txtPaymentAmount').val(),
                                     "CollectedCurrencyCode": $('#txtCollectedCurrency').val(),
                                     "CollectedAmount": $('#txtPayCollectedAmount').val(),
                                     "ABBNo": $('#ddlPayAbbNo').val(),
                                 }];
                                 var str = JSON.stringify(strJson);
                                 $('#hidfPaySaveVal').val(str);
                                 $('#btnSavePayment').click();
                                 $(this).dialog("close");

                             },
                             Cancel: function () {
                                 $(this).dialog("close");
                             }
                         }
                     });
                 }


                 function AddHighlight(id) {
                     if(id != '')
                         $("#"+id).addClass("hil");
                 }

                 function RemoveHighlight(id) {
                     if(id != '')
                         $("#" + id).removeClass("hil");
                 }

                function isNumberKey(evt, element) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if (charCode == 46 && evt.srcElement.value.split('.').length > 1) {
                        return false;
                    }
                    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                        return false;
                    return true;
                }
                function formatTime(time) {
                    var result = false, m;
                    var re = /^\s*([01]?\d|2[0-3]):?([0-5]\d)\s*$/;
                    if ((m = time.match(re))) {
                        result = (m[1].length === 2 ? "" : "0") + m[1] + ":" + m[2];
                    }
                    return result;
                }
            </script>
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>



    <div id="dialog-confirm-flight" title="Create Flight">
        <div class="container col-lg-12">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Flight No<span style="color:red">*</span>
                </div>
                <div class="col-lg-9">
                    <div class="form-inline">
                        <asp:TextBox ID="flightcode" runat="server" Width="60px" CssClass="form-control pull-left" ClientIDMode="Static"></asp:TextBox><asp:TextBox ID="flightnumber" runat="server" CssClass="form-control pull-left" ClientIDMode="Static" Style="margin-left: 10px;"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    From<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="departure" runat="server"  CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="col-lg-3">
                    To<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="arrv" runat="server"  CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>

            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Flight Date<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="dateFlightFrom" ReadOnly="True" BackColor="White" runat="server"  CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="col-lg-3">
                    To<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="dateFlightTo" ReadOnly="True" BackColor="White" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>

            </div>


            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Flight Time<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="ftime" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>

                <div class="col-lg-3">
                    To<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="fto" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div>

            </div>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-lg-3">
                            ABB No
                        </div>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="ddlFlightAbbNo" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:HiddenField ID="flhid" runat="server" Value="" ClientIDMode="Static" />
    </div>

    <div id="dialog-confirm-passenger" title="Passenger">
        <div class="container col-lg-12">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    Title<span style="color:red">*</span>
                </div>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtPassTitle" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    First Name<span style="color:red">*</span>
                </div>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtPassFirstName" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    Last Name<span style="color:red">*</span>
                </div>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtPassLastName" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    Pax Type<span style="color:red">*</span>
                </div>
                <div class="col-lg-8">
                    <asp:DropDownList ID="ddlPaxType" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%">
                        <asp:ListItem Text="ADT" Value="ADT" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="CHD" Value="CHD"></asp:ListItem>
                    </asp:DropDownList>

                    <%--<asp:TextBox ID="txtPaxType" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>--%>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-lg-4">
                            ABB No
                        </div>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlPassABBNo" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hidfPassid" runat="server" Value="" ClientIDMode="Static" />
    </div>

    <div id="dialog-confirm-fare" title="Fare">
        <div class="container col-lg-12 ">
            
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-lg-3">
                            Flight<span style="color:red">*</span>
                        </div>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                    <%--<div class="row" style="margin-top: 10px;">
                        <div class="col-lg-3">
                            Pax Type<span style="color:red">*</span>
                        </div>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="ddl_FarePaxType" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%">
                                <asp:ListItem Text="ADT" Value="ADT" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="CHD" Value="CHD"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="row" style="margin-top: 10px; display: none;">
                        <div class="col-lg-3">
                            Passenger<span style="color:red">*</span>
                        </div>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="ddlFarePassenger" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Charge Type<span style="color:red">*</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox ID="txtFareChargeType" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Charge Code<%--<span style="color:red">*</span>--%>
                </div>
                <div class="col-lg-3">
                    <asp:DropDownList ID="ddlFareChargeCode" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                </div>
                <div class="col-lg-3">
                    Ticket Code<%--<span style="color:red">*</span>--%>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFareTicket" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Charge Detail
                </div>
                <div class="col-lg-9">
                    <asp:TextBox ID="txtFareChargeDetail" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Collection Type
                </div>
                <div class="col-lg-9">
                    <asp:TextBox ID="txtFareCollectionType" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Currency<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFareCurerency" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    Amount<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">

                    <asp:TextBox ID="txtFareAmount" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    ForeignCurrency<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFareForeignCurrency" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    Amount<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFareForeignAmount" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-lg-3">
                            ABB No
                        </div>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="ddlFareAbbNo" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </div>
        <asp:HiddenField ID="hidfFareID" runat="server" Value="" ClientIDMode="Static" />
    </div>

    <div id="dialog-confirm-fee" title="Fee">
        <div class="container col-lg-12">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    Passenger<span style="color:red">*</span>
                </div>
                <div class="col-lg-8">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlFeePassName" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%--<asp:TextBox ID="txtFeePassName" runat="server" CssClass="txtcalendar" ClientIDMode="Static" Width="100%"></asp:TextBox>--%>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    Charge Type<span style="color:red">*</span>
                </div>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtFeeChargeType" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    Charge Code<span style="color:red">*</span>
                </div>
                <div class="col-lg-8">
                    <%--<asp:TextBox ID="txtFeeChargeCode" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlFeeChargeCode" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>

                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    Charge Detail
                </div>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtFeeChargeDetail" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Ticket Code
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFeeTicCode" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                    <%--<asp:TextBox ID="txtFeeTicCode" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>--%>
                </div>
                <div class="col-lg-3">
                    Collection Type
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFeeCollType" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    Currency<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFeeCurr" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    Amount<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFeeAmount" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                    ForeignCurrency<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFeeForeignCurrencyCode" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    Amount<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFeeForeignAmount" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                </div>

            </div>
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-lg-3">
                            ABB No
                        </div>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="ddlFeeAbbNo" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <asp:HiddenField ID="hidfFeeid" runat="server" Value="" ClientIDMode="Static" />
    </div>

    <div id="dialog-confirm-payment" title="Payment">
        <div class="container col-lg-12">
            <div class="row">
                <%--                <div class="col-lg-3">
                    Payment ID
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtPaymentID" runat="server" ClientIDMode="Static" Width="100%" CssClass="txtcalendar"></asp:TextBox>
                </div>--%>

                <div class="col-lg-3">
                    Payment Code<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtPaymentCode" runat="server" ClientIDMode="Static" Width="100%" CssClass="txtcalendar"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-3">
                    PaymentMehtodType<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtPaymentMehtodType" runat="server" ClientIDMode="Static" Width="100%" CssClass="txtcalendar"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    Payment Agency<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtPaymentAgency" runat="server" ClientIDMode="Static" Width="100%" CssClass="txtcalendar"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-3">
                    Payment Date<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="datePaymentDate" runat="server" CssClass="txtcalendar" ClientIDMode="Static" Width="90%"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-3">
                    Currency<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtPayCurrency" runat="server" ClientIDMode="Static" Width="100%" CssClass="txtcalendar"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    Amount<span style="color:red">*</span>
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="txtcalendar" ClientIDMode="Static" Width="100%" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3">
                    CollectedCurrency<span style="color:red">*</span>
                </div>
                <div class="col-lg-3 ">
                    <asp:TextBox ID="txtCollectedCurrency" runat="server" ClientIDMode="Static" Width="100%" CssClass="txtcalendar"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    CollectedAmount<span style="color:red">*</span>
                </div>

                <div class="col-lg-3 ">
                    <asp:TextBox ID="txtPayCollectedAmount" runat="server" CssClass="txtcalendar" ClientIDMode="Static" Width="100%" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                </div>
            </div>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
            <div class="row">
                <div class="col-lg-3">
                    ABB No
                </div>
                <div class="col-lg-9">
                    <asp:DropDownList ID="ddlPayAbbNo" runat="server" CssClass="form-control" ClientIDMode="Static" Width="100%"></asp:DropDownList>
                </div>
            </div>
                    </ContentTemplate>
                            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hidfPayid" runat="server" Value="" ClientIDMode="Static" />
    </div>



</asp:Content>
