<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SlipMassege.aspx.cs" Inherits="CreateInvoiceSystem.SlipMassege" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="7" style="height: 22px" >Basic Information</td>
        </tr>
   
        <tbody>
            <tr>
                <td class="headlabel" colspan="7" >Payment Information</td>
            </tr>
            <tr>
                <td colspan="7">
                    <table style="width:100%;">
                        <tr>
                            <td class="tableform" style="width: 48px">&nbsp;</td>
                            <td class="" style="width: 102px; text-align: left;">
                            </td>
                            <td class="tableform" style="width: 110px">&nbsp;</td>
                            <td class="tableform" style="text-align: left; width: 316px">
                                &nbsp;</td>
                            <td style="width:30px; text-align: right;"><img id="addbtn" src="Images/add.png" width="20px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <table id="tb_detail" class="table table-striped table-bordered">
                        <thead>                            
                            <tr>
                                <th style="width: 30px" class="text-center">Edit</th>
                                <th style="width: 30px" class="text-center">Del</th>
                                <th class="text-center">No</th>
                                <th class="text-center" style="width: 120px">Payment Type</th>
                                <th style="width: 500px">Payment Detail</th>
                                <th class="text-center">Qty</th>
                                <th class="text-center">Unit</th>
                                <th class="text-right">Amount(THB)</th>
                            </tr>
                        </thead>
                        
                        <tbody id="LogDetail">
                            <tr id="rowempty">
                                <td colspan="8" style="text-align:center;">No data found.</td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
                        
        </tbody>
    </table>
    <h3 class="header smaller lighter blue"></h3>
    <div class="row">
        <div style="text-align:center">
            <asp:Button runat="server" ID="Save" OnClientClick="return false;" Text="Save" Height="40px" Width="80px"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="Button1"  OnClientClick="return false;" Text="Cancel" Height="40px" Width="80px"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="Button2" OnClientClick="return false;" Text="Print" Height="40px" Width="80px"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </div>
    <div id="dialog-data">
        <form class="form-horizontal">            
            <div class="row">
                <label class="hidden" id="FORM_STATE"></label>
                <label class="hidden" id="lbl_rowindex"></label>

                <div class="col-xs-12 col-sm-12">
                     <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <label class="col-sm-4">Slip Message Code :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="txtTCode"></asp:TextBox>
                                  <!--  <asp:DropDownList ID="ddl_patment_type" runat="server" Width="180px"></asp:DropDownList> -->
                                </div>
                            </div>                    
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <label class="col-sm-4">Type :</label>
                                <div class="col-sm-8">
                                    <asp:RadioButton runat="server" ID="RHeader"  AutoPostBack="false" Text="Header"/>
               <asp:RadioButton runat="server" ID="RFooter"  AutoPostBack="false" Text="Footer"/>
                                </div>
                            </div>                    
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <label class="col-sm-4">Header/Footer :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="txtFormCell"></asp:TextBox>
                                </div>
                            </div>                    
                        </div>
                    </div>
                      <div class="form-group">
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <label class="col-sm-4">Header/Footer :</label>
                                <div class="col-sm-8">
                                    <asp:CheckBox runat="server" ID="chActive"  AutoPostBack="false" Text="Active"/>
                                </div>
                            </div>                    
                        </div>
                    </div>
                    <div class="form-group">
                        <img id="addbtn1" src="Images/add.png" width="20px" />
                        <div class="col-sm-12">
                            <div class="col-sm-12">
                                <div class="col-sm-8">
                         <table id="tb_detail1" class="table table-striped table-bordered">
                        <thead>                            
                            <tr>
                                <th style="width: 30px" class="text-center">Edit</th>
                                <th class="text-center">No</th>
                                <th style="width: 300px">Line Name</th>
                                <th class="text-center">Header/Footer</th>
                                <th class="text-center">Active</th>
                                                              <th style="width: 30px" class="text-center">Del</th>

                            </tr>
                        </thead>
                        
                        <tbody id="LogDetail1">
                            <tr id="rowempty">
                                <td colspan="8" style="text-align:center;">No data found.</td>
                            </tr>
                        </tbody>
                    </table>
                                </div>
                            </div>                    
                        </div>
                    </div>
                </div>           
            </div>
        </form>
    </div>
    <div>

        <asp:HiddenField ID="lbIndex" runat="server" Value="0" />
    </div>




    <script src="Scripts/Bootstrap/bootstrap.min.js"></script>
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/Bootstrap/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">
        var lbIndex = document.getElementById("<%= lbIndex.ClientID %>");

        $(document).ready(function () {
            $('#dialog-data').hide();
            InitialForm = function () {
            };
            InitialForm();
            var initial = 0;
            // New record
            $('a.editor_create').on('click', function (e) {
                e.preventDefault();
            });
            // Edit record
            $('#tb_detail1').on('click', 'a.editor_edit', function (e) {
                e.preventDefault();
                //alert("edit");
                $('#FORM_STATE').val('edit');
                //FORM_STATE
                var id = this.id;
                $('#lbl_rowindex').val(id);
                popup_form();
            });
            // Delete a record
            $('#tb_detail1').on('click', 'a.editor_remove', function (e) {
                e.preventDefault();
                $(this).parent().parent().remove();
                var count = $('#LogDetail1').children('tr').length;
                var count = $('#LogDetail1').children('tr').length;
                if (count == 0) {
                    initial = 0;
                    $("#tb_detail1 tbody").append(
			            "<tr>" +
			            "<td colspan='8' style='text-align:center;'>No data found.</td>" +
			            "</tr>");
                    //$('#MainContent_lbl_amt_total').val("0.00");
                    //$('#MainContent_lbl_amt_vat').val("0.00");
                    //$('#MainContent_lbl_amt_grandtotal').val("0.00");
                    return;
                }
                update_rowindex();
                CalulateAmtTax();
            });
   
            $('#addbtn1').click(function () {
                var stat = $('#FORM_STATE').val();
                if (stat == 'add') {
                    AddData();
                } else {
                    updaterow();

                
                }
            });

            $('#addbtn').click(function () {
                $('#FORM_STATE').val('add');
                popup_form();
            });

            initialrow = function () {
                if (initial == 0 ) {
                    initial = 1;
                    var count = $('#LogDetail1').children('tr').length;
                    if (count > 0) {
                        $('#tb_detail1 tbody tr').remove();
                    }
                }
            };
            //ลบข้อมูล
            ClearData = function () {
                
                $("#MainContent_txtTCode").val('');
                $("#MainContent_txtName").val('');
                document.getElementById("<%= RHeader.ClientID %>").checked = false;
                  document.getElementById("<%= RFooter.ClientID %>").checked = false;
                document.getElementById("<%= chActive.ClientID %>").checked = false;
            };


            AddData = function () {
                initialrow();
                var index = $("#LogDetail1").children("tr").length;
                //code
                var _code = document.getElementById("<%=txtTCode.ClientID %>");

            if (_code.value == "") {
                alert("กรุณาระบุ Code");
                return false;
            }
                var Header = document.getElementById("<%= RHeader.ClientID %>");
                var Footer = document.getElementById("<%= RFooter.ClientID %>");
                var cName = document.getElementById("<%= txtName.ClientID %>");

                if (cName.value == '') {
                    alert('กรุณากรอก header/footer');
                    return false;
                }
                var colName = "<td style='text-align:center;'>" + cName.value + "</td>";

          var Ccode = "";
                //active
                var chActive = document.getElementById("<%= chActive.ClientID %>");
                var chActive_list = "";

                if (chActive.checked == true) {
                    chActive_list = "<td style='text-align:center;'>Y</td>";
                } else {

                    chActive_list = "<td style='text-align:center;'>N</td>";
                }
                var h = "";
                if (Header.checked == true) {
                    h = "Header";
                    Ccode = "<td style='text-align:center;'>" + h + "</td>";

                }
                if (Footer.checked == true) {
                    h = "Footer";
                    Ccode = "<td style='text-align:center;'>" + h + "</td>";

                }
                var btn = "btnRemove_" + index;
                var btn$ = "#btnRemove_" + index;
                var coledit = "<td><a href='' id='" + index + "' class='editor_edit'>Edit</a></td>";
                var colDel = "<td><a href=''  class='editor_remove'>Delete</a></td>";
                var colIndex = "<td style='text-align:center;'>" + (index+1) +  "</td>";

                var item = "<tr>" + coledit + colIndex + colName + Ccode + chActive_list + colDel + "</tr>";
                $("#tb_detail1").append(item);
                ClearData();
                lbIndex.value += 1;
                return true;
            }
          
          
            CheckDuplicate = function(_code) {
                var _bool = false;
                var table = document.getElementById('LogDetail1'),
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
                ClearData();
                var mode = $('#FORM_STATE').val();
                if (mode == "edit") {
                    var index = $('#lbl_rowindex').val();
                    getDataRow(index);
                }
                $("#dialog-data").dialog({
                    resizable: false,
                    height: 750,
                    width: 850,
                    modal: true,
                    title: 'Slip Message',
                    open: function (event, ui) {
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },


                    buttons: {
                        "Save": function () {
                            var table = document.getElementById('LogDetail1'),
                                rows = table.getElementsByTagName('tr'),
                                          i, j, cells;
                            var txtName = rows.length;

                            var cells = null;

                            var propertyArray = new Array();

                            for (i = 0; i <= rows.length - 1; i++) {
                                 cells = rows[i].getElementsByTagName('td');
                                 propertyArray.push( cells[2].innerHTML + "|" + cells[3].innerHTML + "|" + cells[4].innerHTML);
                            }


                            //var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/SaveSlipMassege',
                                    type: "POST",
                                    cache: false,
                                    asyn: false,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "xml",
                                    data: JSON.stringify({ Code: document.getElementById("<%= txtTCode.ClientID %>").value, _Detail: propertyArray, _type : "new" }),
                                    success: function (data) {
                                        if (data.firstChild.textContent == '') {
                                            alert('Save Success');
                                        }
                                        else {
                                            msg = data.firstChild.textContent;
                                            alert(msg);
                                        }

                                    }
                                });
                            if (msg == '') {
                                $(this).dialog("close");
                            }
                        },
                        Cancel: function () {
                            $(this).dialog("close");
                            initialrow();
                            ClearData();

                        }

                    }

                  
                });
            };
            update_rowindex = function () {
                var table = document.getElementById('LogDetail1'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;

                for (i = 0, j = rows.length; i < j; ++i) {
                    cells = rows[i].getElementsByTagName('td');
                    if (!cells.length) {
                        continue;
                    }
                    cells[0].innerHTML = "<a href='' id='" + i + "' class='editor_edit'>Edit</a>";
                    cells[1].innerHTML = i + 1;
                }
                $('#FORM_STATE').val('add');
            };
            getDataRow = function (index) {
                var table = document.getElementById('LogDetail1'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;
                cells = rows[index].getElementsByTagName('td');

                var txtName = cells[2].innerHTML;
                document.getElementById("<%= txtName.ClientID %>").value = txtName;

                var hf = cells[3].innerHTML;


                if (hf == 'Header') {
                    document.getElementById("<%= RHeader.ClientID %>").checked = true;
                     document.getElementById("<%= RFooter.ClientID %>").checked = false;
                } else {
                     document.getElementById("<%= RHeader.ClientID %>").checked = false;
                     document.getElementById("<%= RFooter.ClientID %>").checked = true;
                }
                var act = cells[4].innerHTML;
                if (act == 'Y') {

                       document.getElementById("<%= chActive.ClientID %>").checked = true;
                }

                $('#FORM_STATE').val('edit');

            };
            updaterow = function () {
                var index = $('#lbl_rowindex').val();
                var table = document.getElementById('LogDetail1'),
                    rows = table.getElementsByTagName('tr'),
                    i, j, cells;
                cells = rows[index].getElementsByTagName('td');
               
            

                var  chActive = document.getElementById("<%= chActive.ClientID %>");
                 var Header = document.getElementById("<%= RHeader.ClientID %>");
                var Footer = document.getElementById("<%= RFooter.ClientID %>");
                var cName = document.getElementById("<%= txtName.ClientID %>");

                cells[2].innerHTML = cName.value;

                if (Header.checked == true) {
                    cells[3].innerHTML = 'Header';

                } else {
                    cells[3].innerHTML = 'Footer';

                }
                if (chActive.checked == true) {
                    cells[4].innerHTML = 'Y';

                } else {
                    cells[4].innerHTML = 'N';

                }
                update_rowindex();
                return true;
            };
        });
  
   

           function CheckRadio(datatype) {
            var _h = document.getElementById("<%=RHeader.ClientID%>");
            var _F = document.getElementById("<%=RFooter.ClientID%>");
            if (datatype == 1) {
                _F.checked = false;
            } else {
                _h.checked = false;
            }
            return true;
           }
        function editmode(id) {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $.ajax({
                url: 'servicepos.asmx/GetFlightType',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_code').val(obj[0].Flight_Type_Code);
                    $('#MainContent_name').val(obj[0].Flight_Type_Name);
                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 300,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Flight Fee',
                        buttons: {
                            "Save": function () {
                                if ($('#MainContent_code').val() == '') {
                                    alert('กรุณาระบุรหัส');
                                    return false;
                                }
                                if ($('#MainContent_name').val() == '') {
                                    alert('กรุณาระบุชื่อ');
                                    return false;
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditFlightType',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: { 'code': $('#MainContent_code').val(), 'name': $('#MainContent_name').val(), 'id': id },
                                    success: function (data) {
                                        if (data.firstChild.textContent == '') {
                                            alert('Save Success');
                                            $('#MainContent_Button1').click();
                                        }
                                        else {
                                            msg = data.firstChild.textContent;
                                            alert(msg);
                                        }

                                    }
                                });
                                if (msg == '') {
                                    $(this).dialog("close");
                                }
                            },
                            Cancel: function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                }
            });
        }
    </script>
</asp:Content>
