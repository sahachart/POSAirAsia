<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/ReportByMachine.aspx.cs" Inherits="CreateInvoiceSystem.Reports.Views.ReportByMachine" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            $("[id$=txb_date_from]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'Images/calender.png',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
            });
            $("[id$=txb_date_to]").datepicker({
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
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="4" style="height: 22px">
                <asp:Label runat="server" ID="Label_Menu"></asp:Label></td>
        </tr>
        <tr>
            <td class="headlabel" colspan="4" style="height: 22px" onclick="Shortcut();">Criteria Search...</td>
        </tr>
        <tr>
            <td class="tableform" style="width: 150px">
                <asp:Label runat="server" ID="lbl_st_from">From Station Code:</asp:Label>
            </td>
            <td class="tableform">
                <asp:DropDownList ID="ddl_station_from" Visible="false" runat="server"></asp:DropDownList>
            </td>
            <td class="tableform" style="width: 150px">
                <asp:Label runat="server" ID="lbl_st_to">To Station Code:</asp:Label>
            </td>
            <td class="tableform">
                <asp:DropDownList ID="ddl_station_to" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tableform">
                <asp:Label runat="server" ID="lbl_date_from" class="">From Date:</asp:Label>
            </td>
            <td class="tableform">
                <asp:TextBox ID="txb_date_from" runat="server"></asp:TextBox>
            </td>
            <td class="tableform">
                <asp:Label runat="server" ID="lbl_date_to" class="">To Date:</asp:Label>
            </td>
            <td class="tableform">
                <asp:TextBox ID="txb_date_to" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tbody>
            <tr>
                <td class="tableform">
                    <asp:Label runat="server" ID="lbl_flight_type" class="">Flight Type:</asp:Label>
                </td>
                <td class="tableform">
                    <asp:DropDownList ID="ddl_flight" runat="server"></asp:DropDownList>
                </td>
                <td class="tableform">&nbsp;</td>
                <td class="tableform">&nbsp;</td>
            </tr>
            <tr>
                <td class="tableform">&nbsp;</td>
                <td class="tableform">
                    <asp:Button runat="server" CssClass="" ID="btn_run" Text="Search" OnClick="btn_run_Click" />
                    <button id="btn_reset" class="">Reset</button>
                </td>
                <td class="tableform">&nbsp;</td>
                <td class="tableform">&nbsp;</td>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div id="dialog-Report">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" style="overflow-y:auto !important;" ClientIDMode="Static" ShowPrintButton="true" ></rsweb:ReportViewer>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_run" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <script src="Scripts/Bootstrap/bootstrap.min.js"></script>
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/Bootstrap/dataTables.bootstrap.min.js"></script>
    <script src="Scripts/WebForms/CIS-Customer/cis-master.js"></script>
    <script type="text/javascript">
        $(function () {
            var val = urlParam('id', window.location.href);
            initialform = function () {
                initialcontrol();
                if (val == '1') {

                }
                if (val == '2') {

                }
                if (val == '3') {

                }
                if (val == '4') {

                }
                if (val == '5') {

                }
                if (val == '6') {

                }
                if (val == '7') {

                }
                if (val == '8') {

                }
                if (val == '9') {

                }
            };
            discontrol = function (ctrl, flag) {
                if (flag == false) {
                    //$(ctrl).attr('Visible', flag);
                    document.getElementById(ctrl).setAttribute("Visible", 'false');
                } else {
                    $(ctrl).removeAttr('disable');

                }
            };
            initialcontrol = function () {
                $("#MainContent_ddl_station_from").val('ALL');
                $("#MainContent_ddl_station_from").trigger('update');
                $("#MainContent_ddl_station_to").val('');
                $("#MainContent_ddl_station_to").trigger('update');
                $("#MainContent_txb_date_from").val('');
                $("#MainContent_txb_date_to").val('');
            };
            $('#btn_reset').click(function () {
                initialcontrol();
            });
            $('#MainContent_btn_run').click(function () {
                validateform();
            });
            validateform = function () {
                //if (val == "1" || val == "2" || val =="3") {
                //if ($("#MainContent_ddl_station_from").val() == '') {
                //    alert('Please select station from.');
                //    return false;
                //}
                //if ($("#MainContent_ddl_station_to").val() == '') {
                //    alert('Please select station to.');
                //    return false;
                //}
                if ($("#MainContent_txb_date_from").val() == '') {
                    alert('Please select date from.');
                    return false;
                }
                if ($("#MainContent_txb_date_to").val() == '') {
                    alert('Please select date to.');
                    return false;
                }
                //}
                //else {

                //}

                return true;
            };
            initialform();
        });

        function Shortcut() {
            var fromDate = new Date();
            fromDate.setDate(fromDate.getDate() - 10);
            var toDate = new Date();

            var from_Date = fromDate.getDate() < 10 ? '0' + fromDate.getDate() : fromDate.getDate();
            var from_Month = (fromDate.getMonth() + 1) < 10 ? '0' + (fromDate.getMonth() + 1) : fromDate.getMonth() + 1;
            var from_Year = fromDate.getFullYear();

            var to_Date = toDate.getDate() < 10 ? '0' + toDate.getDate() : toDate.getDate();
            var to_Month = (toDate.getMonth() + 1) < 10 ? '0' + (toDate.getMonth() + 1) : toDate.getMonth() + 1;
            var to_Year = toDate.getFullYear();

            $("#MainContent_ddl_station_from").val('ALL');
            $("#MainContent_ddl_station_from").trigger('update');
            //$("#MainContent_ddl_station_to").val('System');
            //$("#MainContent_ddl_station_to").trigger('update');
            $("#MainContent_txb_date_from").val(from_Date + '/' + from_Month + '/' + from_Year);
            $("#MainContent_txb_date_to").val(to_Date + '/' + to_Month + '/' + to_Year);
        }

        function ShowButtonPrint() {
            if (true) {
                try {
                    
                    var ControlName = 'ctl00_MainContent_ReportViewer1_ctl05';
                    var xxx1 = '<table cellpadding="0" cellspacing="0" toolbarspacer="true" style="display:inline-block;width:6px;"><tbody><tr><td></td></tr></tbody></table>';

                    var xxx2 = '<div class=" " style="display:inline-block;font-family:Verdana;font-size:8pt;vertical-align:top;"><table cellpadding="0" cellspacing="0" style="display:inline;"><tbody><tr><td height="28px"><div id="ctl00_MainContent_ReportViewer1_ctl05_ctl06_ctl00"><div id="ctl00_MainContent_ReportViewer1_ctl05_ctl06_ctl00_ctl00" style="border: 1px solid transparent; cursor: default; background-color: transparent;"><table title="Print"><tbody><tr><td><input type="image" name="ctl00$MainContent$ReportViewer1$ctl05$ctl06$ctl00$ctl00$ctl00" title="Print" src="/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=11.0.3442.2&amp;Name=Microsoft.Reporting.WebForms.Icons.Print.gif" alt="Print" style="border-style:None;height:16px;width:16px;" onclick="PrintFunc(); return false;"></td></tr></tbody></table></div><div id="ctl00_MainContent_ReportViewer1_ctl05_ctl06_ctl00_ctl01" class="aspNetDisabled" disabled="disabled" style="border: 1px solid transparent; display: none;"><table title="Print"><tbody><tr><td><input type="image" name="ctl00$MainContent$ReportViewer1$ctl05$ctl06$ctl00$ctl01$ctl00" disabled="disabled" title="Print" src="/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=11.0.3442.2&amp;Name=Microsoft.Reporting.WebForms.Icons.PrintDisabled.gif" alt="Print" style="border-style:None;height:16px;width:16px;cursor:default;"></td></tr></tbody></table></div></div></td></tr></tbody></table></div>';

                    $("#" + ControlName + " > div").append(xxx1 + xxx2);

                }
                catch (e) { alert(e); }
            }
        }
        //PrintFunc('ctl00_MainContent_ReportViewer1_ctl05')

        function PrintFunc() {
            
            var link = '<link href="menu/menu_style.css" rel="stylesheet" />\
                        <link href="Content/styles.css" rel="stylesheet" />\
                        <link href="Content/themes/base/jquery-ui.css" rel="stylesheet" />\
                        <link href="Content/bootstrap.min.css" rel="stylesheet" />\
                        <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />';

            var strFrameName = ("printer-" + (new Date()).getTime());
            var jFrame = $("<iframe name='" + strFrameName + "'>");
            jFrame
            .css("width", "1px")
            .css("height", "1px")
            .css("left", "-2000px")
            .css("position", "absolute")
            .appendTo($("body:first"));
            var objFrame = window.frames[strFrameName];
            var objDoc = objFrame.document;
            var jStyleDiv = $("<div>").append($("style").clone());
            var styles = '<style type="text/css">' + jStyleDiv.html() + "</style>";
            var docType = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">';
            var docCnt = link + styles + $("#VisibleReportContentctl00_MainContent_ReportViewer1_ctl09").html();
            var docHead = '<head><title>...</title><style>body{margin:5;padding:0;}</style></head>';
            objDoc.open();
            objDoc.write(docType + '<html>' + docHead + '<body onload="window.print();">' + docCnt + '</body></html>');
            objDoc.close();

        }
    </script>
</asp:Content>
