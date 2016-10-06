<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abbno.aspx.cs" Inherits="CreateInvoiceSystem.abbno" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.4.min.js"></script>
    <script src="Scripts/Bootstrap/bootstrap.min.js"></script>
    
</head>
<body>
    <%--<div id="MyDiv">
        <form id="form1" runat="server">
            <table style="width: 100px; border: 0px" id="table_detail">
                <tr>
                    <td colspan="2" style="text-align:center">
                        <img src="Images/airasiatop2.png" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center" colspan="2">
                        บริษัท ไทยแอร์เอเชีย จำกัด<br /><p></p>
                        THAI AIRASIA COMPANY LIMITED<br /><p></p>         
                        เลขที่ 222 หมู่ที่ 10 ท่าอากาศยานดอนเมือง<br /><p></p>            
                        ถนนวิภาวดีรังสิต แขวงสนามบิน<br/><p></p>            
                        เขตดอนเมือง กรุงเทพมหานคร 10210<br/><p></p>           
                        TAX INVOICE(ABB) TAX ID<br/><p></p>
                        PID:E010090D0200257<br/><p></p>
                        User: 14196 POS: 004<br/><p></p>
                        11/3/2016<br/><p></p>
                        BNO: <asp:Label ID="Label15" runat="server" Text=""></asp:Label><br/>
                        --------------------------------------------------------------------------------------------------------
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="เลขที่จอง/Booking No.:"></asp:Label></td>
                    <td style="text-align: right"><asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label3" runat="server" Text="จำนวนผู้โดยสาร/No. of Passenger:" ></asp:Label></td>
                    <td style="text-align: right"><asp:Label ID="Label4" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label5" runat="server" Text="ชื่อผู้โดยสาร/Passenger Name:"></asp:Label></td>
                    <td></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        --------------------------------------------------------------------------------------------------------
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                <tr>
                    <td colspan="2" style="text-align:center">
                        --------------------------------------------------------------------------------------------------------
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label8" runat="server" Text="ภาษีมูลค่าเพิ่ม/VAT" ></asp:Label></td>
                    <td style="text-align: right"><asp:Label ID="Label9" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label10" runat="server" Text="ค่าภาษีสนามบิน/Airport Tax" ></asp:Label></td>
                    <td style="text-align: right;  "><asp:Label ID="Label11" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label12" runat="server" Text="Total Baht (VAT Included)" ></asp:Label></td>
                    <td style="text-align: right"><asp:Label ID="Label13" runat="server" Text=""></asp:Label></td>
                </tr>
                <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                <tr>
                    <td colspan="2" style="text-align:center">
                        --------------------------------------------------------------------------------------------------------
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label14" runat="server" Text="Print-Date:" >Print-Date:14/03/2016</asp:Label></td>
                    <td style="text-align: right"><asp:Label ID="Label17" runat="server" Text="Time:">Time:12:12:12</asp:Label></td>
                </tr>
                 <tr>
                    <td><asp:Label ID="Label18" runat="server" Text="Print no:" >Print no:1</asp:Label></td>
                    <td style="text-align: right"><asp:Label ID="Label19" runat="server" Text="Pint reason:">Pint reason:</asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        --------------------------------------------------------------------------------------------------------
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center" colspan="2">
                        ติดต่อ ไทยแอร์เอเชีย โทร 02-515-9999<br /><p></p>
                        Website : http://www.airasia.com<br /><p></p>         
                        **ค่าโดยสารและค่าบริการไม่สามารถคืนได้**<br /><p></p>            
                       @@ ขอบคุณที่ใช้บริการ / Thank you @@<br/><p></p>            
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div style="text-align:center">
        <button id="btn_print" value="">Print</button>
    </div>
    <p></p>--%>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%" ZoomMode="PageWidth" ShowBackButton="true" SizeToReportContent="True">
        </rsweb:ReportViewer>
       
    </form>
       <%--<div class="row">
         <div class="col-md-12">  
            <form id="from_abbbill">
                <div class="col-md-12 col-sm-6">
                    <div class="form-group">
                    <div class="col-md-5">บริษัท ไทยแอร์เอเชีย จำกัด</div>                    
                </div>
                </div>
            </form>
         </div>
       
    </div>--%>
    
</body>
</html>
<%--<script type="text/javascript">
    $(document).ready(function () {
        $('#btn_print').click(function () {
            //window.print();
            //PrintElem('#MyDiv');
            printData();
        });
        function PrintElem(elem) {
            Popup($(elem).html());
        }
        function printData() {
            var divToPrint = document.getElementById("table_detail");
            newWin = window.open('', 'mydiv', 'height=400,width=600');
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }

        function Popup(data) {
            //var mywindow = window.open('', 'mydiv', 'height=400,width=600');
            //mywindow.document.write('<html><head><title>my div</title>');
            ///*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            //mywindow.document.write('</head><body >');
            //mywindow.document.write(data);
            //mywindow.document.write('</body></html>');

            //mywindow.document.close(); // necessary for IE >= 10
            //mywindow.focus(); // necessary for IE >= 10

            //mywindow.print();
            //mywindow.close();

            return true;
        }

    });
</script>--%>

<script type="text/javascript">
    function ShowButtonPrint() {
        if (true) {
            try {

                var ControlName = 'ReportViewer1_ctl05';
                var xxx1 = '<table cellpadding="0" cellspacing="0" toolbarspacer="true" style="display:inline-block;width:6px;"><tbody><tr><td></td></tr></tbody></table>';

                var xxx2 = '<div class=" " style="display:inline-block;font-family:Verdana;font-size:8pt;vertical-align:top;"><table cellpadding="0" cellspacing="0" style="display:inline;"><tbody><tr><td height="28px"><div id="ReportViewer1_ctl05_ctl06_ctl00"><div id="ReportViewer1_ctl05_ctl06_ctl00_ctl00" style="border: 1px solid transparent; cursor: default; background-color: transparent;"><table title="Print"><tbody><tr><td><input type="image" name="ctl00$MainContent$ReportViewer1$ctl05$ctl06$ctl00$ctl00$ctl00" title="Print" src="/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=11.0.3442.2&amp;Name=Microsoft.Reporting.WebForms.Icons.Print.gif" alt="Print" style="border-style:None;height:16px;width:16px;" onclick="PrintFunc(); return false;"></td></tr></tbody></table></div><div id="ReportViewer1_ctl05_ctl06_ctl00_ctl01" class="aspNetDisabled" disabled="disabled" style="border: 1px solid transparent; display: none;"><table title="Print"><tbody><tr><td><input type="image" name="ctl00$MainContent$ReportViewer1$ctl05$ctl06$ctl00$ctl01$ctl00" disabled="disabled" title="Print" src="/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=11.0.3442.2&amp;Name=Microsoft.Reporting.WebForms.Icons.PrintDisabled.gif" alt="Print" style="border-style:None;height:16px;width:16px;cursor:default;"></td></tr></tbody></table></div></div></td></tr></tbody></table></div>';

                $("#" + ControlName + " > div").append(xxx1 + xxx2);

            }
            catch (e) { alert(e); }
        }
    }
    //PrintFunc('ReportViewer1_ctl05')

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
        var docCnt = link + styles + $("#VisibleReportContentReportViewer1_ctl09").html();
        var docHead = '<head><title>...</title><style>body{margin:5;padding:0;}</style></head>';
        objDoc.open();
        objDoc.write(docType + '<html>' + docHead + '<body onload="window.print();">' + docCnt + '</body></html>');
        objDoc.close();

    }

    $(document).ready(function () {
        ShowButtonPrint();
    });
</script>
