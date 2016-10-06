<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master"  CodeBehind="frmSlipMessage.aspx.cs" Inherits="CreateInvoiceSystem.frmSlipMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />--%>
    <%--<link href="Content/dataTables.bootstrap.min.css" rel="stylesheet" />--%>
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="3" >Slip Message</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Slip Message Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td style="width: 100%; vertical-align:middle; padding-left: 5px; padding-top: 3px" >
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" OnClick="ImageButton2_Click" />
            </td>
        </tr>
        <tr>
            <td style=" text-align: center; padding: 10px;" colspan="3"  > 
                <table>
                    <tr>
                        <td style="vertical-align:top;"><img id="addbtn" src="Images/add.png" width="20px" /></td>
                        <td>
                            <style>
                                #MainContent_GridView1 th{
                                    text-align: center;
                                }
                            </style>
                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="SlipMessage_Id" 
                                AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px" AllowPaging="True" 
                                EnablePersistedSelection="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Slip Message Code">   
                                        <ItemTemplate>
                                              <a href="javascript:editmode('<%# Eval("SlipMessage_Id") %>');"><%# Eval("SlipMessage_Code") %></a>      
                                        </ItemTemplate>
                                        <HeaderStyle Width="600px" />
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-VerticalAlign="Middle">   
                                        <ItemTemplate>
                                         <asp:ImageButton ImageUrl="Images/meanicons_24-20.png" runat="server" ID="lbdel" 
                                              OnClientClick='<%# CreateConfirmation("Do you want Delete ", Eval("SlipMessage_Code").ToString()) %>'
                                            OnClick="btndel_Click" AlternateText='<%# Eval("SlipMessage_Id").ToString() %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#56575B" />
                                    <HeaderStyle BackColor="#56575B" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#56575B" HorizontalAlign="Right" CssClass="GridPager" />
                                    <RowStyle BackColor="#F0F0F0" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                            <asp:Label ID="Label1" runat="server" Text="No Result" CssClass="Nodata"></asp:Label>
                            <asp:Button ID="btnload" runat="server" OnClick="btnload_Click" Text="Button" style="display: none;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="dialog-data" style="display: none;">    
        <div class="row">
            <label class="hidden" id="FORM_STATE"></label>
            <label class="hidden" id="lb_SID"></label>
            <label class="hidden" id="lbl_rowindex" ></label>

                <table style="padding-top: 5%; padding-left:10%;" >
                    <tr>
                        <td style="text-align:right;">
                            Slip Message Code <span style="color:red ">*</span>
                        </td>
                        <td style="width:5%;">

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTCode" width="120px" CssClass="txtcalendar" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                             Type <span style="color:red ">*</span>

                            </td>
                            <td style="width:5%;">

                        </td>
                        <td>
                            <asp:RadioButton runat="server" ID="RHeader" Checked="true"  AutoPostBack="false" Text="Header"/>
                            <asp:RadioButton runat="server" ID="RFooter"  AutoPostBack="false" Text="Footer"/>
                           
                        </td>
                    </tr>
                     <tr>
                         <td style="text-align:right;">
                             Line Message<span style="color:red ">*</span>

                            </td>
                             <td style="width:5%;">

                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="txtcalendar" Width="200px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td>

                        </td>
                            <td style="width:5%;">

                        </td>
                        <td>

                            <asp:CheckBox runat="server" ID="chActive"  AutoPostBack="false" Text="Active"/>

                        </td>
                    </tr>
                   
                </table>
                <div>
                    <img id="addbtn1" src="Images/add.png" width="20px" />
                    <img id="editbtn" src="Images/Save-Edit-icon.png" width="18px" />
                    <table id="tb_detail1" cellspacing="0" width="100%" border="1" style="border: thin solid; font-size: 14px;"   >
                        <thead>                            
                            <tr>
                                <th style="width: 30px" class="text-center" >Edit</th>
                                <th class="text-center" >No</th>
                                <th style="width: 300px;">Line Message</th>
                                <th class="text-center" >Header/Footer</th>
                                <th class="text-center" >Active</th>
                                <th style="width: 30px;" class="text-center">Del</th>
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
    <div>
        <asp:HiddenField ID="lbIndex" runat="server" Value="0" />
    </div>
    <%--<script src="Scripts/Bootstrap/bootstrap.min.js"></script>--%>
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <%--<script src="Scripts/Bootstrap/dataTables.bootstrap.min.js"></script>--%>
    <script type="text/javascript">
        $('#MainContent_btnload').css('display', 'none');
        var CheckID = "";
        $('#lbl_rowindex').val('');// = '';
        //LoadData for Edit
        var lbIndex = document.getElementById("<%= lbIndex.ClientID %>");
        function editmode(id) {
           
          //  $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $('#FORM_STATE').val('add');
            $('#MainContent_txtTCode').attr("disabled", true);
            popup_form();
            $.ajax({
                url: 'servicepos.asmx/GetSlipMassege',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'ID': id },
                success: function (data) {
                var _code = document.getElementById("<%=txtTCode.ClientID %>");
                var Header = document.getElementById("<%= RHeader.ClientID %>");
                var Footer = document.getElementById("<%= RFooter.ClientID %>");
                var cName = document.getElementById("<%= txtName.ClientID %>");
                    var chActive = document.getElementById("<%= chActive.ClientID %>");
                    var str = data.firstChild.textContent;
                    var sp = str.split('\n');
                    for (i = 0; i < sp.length; i++) {
                        if (sp[i] != '') {
                var j = sp[i].split('|');
                    
                     if (j[0] != '') _code.value = j[0].trim();
                     if (j[1] != '') cName.value = j[1];
                     if (j[2] == 'True') Header.checked = true;
                     if (j[2] == 'False') Footer.checked = true;
                     if (j[3] == 'Y') chActive.checked = true;
                     AddData();
                     CheckID = id;

                        }
                    }
                
                }
            });
        }

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
                   
                    return;
                }
                update_rowindex();
         
            });
   
            $('#addbtn1').click(function () {
                //var stat = $('#FORM_STATE').val();
                //if (stat == 'add') {
                    AddData();

                //}
            });

            $('#editbtn').click(function () {
                //var stat = $('#FORM_STATE').val();
                //if (stat != 'add') {
                    updaterow();
                //}
            });

            $('#addbtn').click(function () {
                $('#FORM_STATE').val('add');
                $('#MainContent_txtTCode').attr("disabled", false);
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
                $("#MainContent_txtName").val('');
                document.getElementById("<%= RHeader.ClientID %>").checked = true;
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
           
                var cName = document.getElementById("<%= txtName.ClientID %>");

                if (cName.value == '') {
                    alert('กรุณากรอก header/footer');
                    return false;
                }
                var colName = "<td style='text-align:center;'>" + cName.value + "</td>";
                var Header = document.getElementById("<%= RHeader.ClientID %>");
                var Footer = document.getElementById("<%= RFooter.ClientID %>");
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
                var colDel = "<td><a href='' class='editor_remove'>Delete</a></td>";
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
            }   ;
            popup_form = function () {
                ClearData();
                var mode = $('#FORM_STATE').val();
                var SID = CheckID;
                var index = $('#lbl_rowindex').val();
                if (index != '') {
                    getDataRow(index);
                }
            
                $("#dialog-data").dialog({
                    resizable: false,
                    height: 600,
                    width: 600,
                    modal: true,
                    title: 'Slip Message',
                    open: function (event, ui) {
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    close: function (event, ui) {
                        $("#MainContent_txtTCode").val('');
                        initial = 0;
                        initialrow();
                        ClearData();
                        CheckID = "";
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
                            if (propertyArray == '') {
                                alert('กรุณากรอกรายการให้ครบ!');
                                return false;
                            }
                            var msg = '';
                            var jj = CheckID;
                            //debugger;
                            if (jj == '') {
                                $.ajax({
                                    url: 'servicepos.asmx/SaveSlipMassege',
                                    type: "POST",
                                    cache: false,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "JSON",
                                    data: JSON.stringify({ Code: document.getElementById("<%= txtTCode.ClientID %>").value, _Detail: propertyArray, _type : 'new' }),
                                    success: function (data) {
                                        if (data.d != '') {
                                            msg = data.d;
                                            alert(msg);
                                        }
                                        else {
                                            msg = data.d;
                                            alert('Save Success');
                                            $('#MainContent_btnload').click();
                                            
                                        }

                                    }
                                });

                            } else {
                                  $.ajax({
                                    url: 'servicepos.asmx/EditSlipMassege',
                                    type: "POST",
                                    cache: false,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "JSON",
                                    data: JSON.stringify({ ID : jj,Code: document.getElementById("<%= txtTCode.ClientID %>").value, _Detail: propertyArray }),
                                    success: function (data) {
                                        
                                        if (data.d != '') {
                                            msg = data.d;
                                            alert(msg);
                                        } 
                                        else {
                                            msg = data.d;
                                            alert('Save Success');
                                            $('#MainContent_btnload').click();
                                            
                                        }
                                    }
                                });
                            }
                            //initial = 0;
                            //initialrow();
                            //debugger
                            if (msg = '') {
                                $(this).dialog("close");
                            }

                        },
                        Cancel: function () {
                            $(this).dialog("close");
                            $("#MainContent_txtTCode").val('');
                            initial = 0;
                            initialrow();
                            ClearData();
                            CheckID = "";
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

                try {
                   
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
                }
                catch (err) {
                }





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

    </script>
</asp:Content>
