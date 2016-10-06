<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="customerinfo.aspx.cs" Inherits="CreateInvoiceSystem.customerinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table class="searchCriteria">
            <tr>
                <td class="headlabel" colspan="5">Customer Profile</td>
            </tr>
            <tr>
                <td class="searchCriterialabel" nowrap>Customer Code</td>
                <td>
                    <asp:TextBox ID="TextBox_customer_code" runat="server" CssClass="txtFormCell" Width="120px"></asp:TextBox>
                </td>
                <td class="searchCriterialabel" nowrap>Customer Name</td>
                <td>
                    <asp:TextBox ID="TextBox_customer_name" runat="server" CssClass="txtFormCell" Width="250px"></asp:TextBox>
                </td>
                <td style="width: 100%">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" /><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" OnClick="ImageButton2_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <table style="width: 100%">
                        <tr>
                            <td style="vertical-align: top;">
                                <img id="addbtn" src="Images/add.png" width="20px" /></td>
                            <td>
                                <style>
                                    #MainContent_GridView1 th {
                                        text-align: center;
                                    }
                                </style>
                                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True"
                                    DataKeyNames="CustomerID" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                    ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px"
                                    AllowPaging="True" EnablePersistedSelection="True" Width="100%"
                                    OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CustomerID" HeaderText="Customer Code" HeaderStyle-Wrap="false" Visible="false" />
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <a href="javascript:GetCustomerByCode('<%# Eval("CustomerID") %>','edit');"><%# Eval("first_name") %></a>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Branch_No" HeaderText="Branch No">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Addr_1" HeaderText="Address">
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Home_Phone" HeaderText="Telephone">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Email" HeaderText="Email">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsActive" HeaderText="Status" Visible="false" />
                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" HeaderStyle-Width="30px" DeleteImageUrl="Images/meanicons_24-20.png">
                                            <ControlStyle BorderStyle="Solid" BorderWidth="0px" />
                                            <HeaderStyle Width="30px"></HeaderStyle>
                                        </asp:CommandField>
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
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">&nbsp;</td>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
        </table>
    </div>
    <div id="dialog-customer" style="display: none;">
        <input class="hidden" type="text" id="txt_FormState" />
        <table style="width: 100%">
            <tr>
                <td class="headlabel" colspan="2">Customer Profile</td>
            </tr>
            <tr style="display: none;">
                <td class="tableform" nowrap>Customer Code<%--<span style="color:red">&nbsp;*</span>--%></td>
                <td class="tableform" style="width: 70%">
                    <asp:TextBox ID="tbx_cust_code" runat="server" CssClass="txtFormCell" Width="120px" MaxLength="30"></asp:TextBox>

                </td>
            </tr>
            <tr style="display: none;">
                <td class="tableform" nowrap>NameTitle<%--<span style="color:red">&nbsp;*</span>--%>  </td>
                <td class="tableform" style="width: 80%">
                    <asp:TextBox ID="tbx_prfx_name" runat="server" CssClass="txtFormCell" Width="80%"></asp:TextBox>

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
                <td class="tableform" nowrap>LastName<%--<span style="color:red">&nbsp;*</span>--%></td>
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

    <script type="text/javascript">
        $(function () {
            InitialFrom = function () {
                $('#txt_FormState').val('');
                $('#MainContent_tbx_cust_code').val('00000000-0000-0000-0000-000000000000');
                $('#MainContent_tbx_prfx_name').val('');
                $('#MainContent_tbx_first_name').val(''); 
                $('#MainContent_txt_BranchNo').val('');
                $('#MainContent_tbx_last_name').val('');
                $('#MainContent_rbl_gndr_code').val('');
                $('#MainContent_tbx_Addr_1').val('');
                $('#MainContent_tbx_Addr_2').val('');
                $('#MainContent_tbx_Addr_3').val('');
                $('#MainContent_tbx_Addr_4').val('');
                $('#MainContent_tbx_Addr_5').val('');
                //$('#MainContent_province').val('');
                //$('#MainContent_country').val('');
                //$('#MainContent_tbx_ZipCode').val('');
                $('#MainContent_tbx_Home_Phone').val('');
                $('#MainContent_tbx_Email').val('');
                $('#MainContent_tbx_remark').val('');
                $('#MainContent_tbx_taxid').val('');
            };
            var dialog = $("#dialog-customer");
            $("#dialog-customer").hide();
            $("#addbtn").click(function () {
                InitialFrom();
                $('#txt_FormState').val('insert');
                var mode = $('#txt_FormState').val();
                if (mode == 'insert') {
                    //$('#MainContent_tbx_cust_code').addAttr('disable', 'disable');
                    document.getElementById("MainContent_tbx_cust_code").disabled = false;

                }
                $("#dialog-customer").dialog({
                    resizable: false,
                    height: window.innerHeight,
                    width: 900,
                    modal: true,
                    title: 'Create New Customer',
                    open: function (event, ui) {
                        //$('#MainContent_countrycode').val('');
                        //$('#MainContent_countryname').val('');
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    buttons: {
                        "Save": function () {
                            SaveCustomer();
                        },
                        Cancel: function () {
                            dialog.dialog("close");
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

            GetCustomerByCode = function (key, mode) {
                InitialFrom();
                $('#txt_FormState').val('edit');
                $.ajax({
                    url: 'servicepos.asmx/GetCustomerByCode',
                    type: "POST",
                    //dataType: "xml",
                    cache: false,
                    asyn: false,
                    //data: { 'key': obj.cust_code, 'mode': $('#txt_FormState').val() },
                    //data: JSON.stringify({ obj: obj, mode: $('#txt_FormState').val() }),
                    data: "{ 'key':'" + key + "','mode':'" + mode + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != undefined) {
                            if (mode == 'edit') {
                                //$('#MainContent_tbx_cust_code').addAttr('disable', 'disable');
                                document.getElementById("MainContent_tbx_cust_code").disabled = true;

                            }
                            $('#MainContent_tbx_cust_code').val(data.d.CustomerID);
                            $('#MainContent_tbx_prfx_name').val(data.d.prfx_name);
                            $('#MainContent_tbx_first_name').val(data.d.first_name);
                            $('#MainContent_txt_BranchNo').val(data.d.Branch_No);
                            $('#MainContent_tbx_last_name').val(data.d.last_name);
                            //$('#MainContent_rbl_gndr_code').val(data.d.gndr_code);
                            $('#MainContent_tbx_Addr_1').val(data.d.Addr_1);
                            $('#MainContent_tbx_Addr_2').val(data.d.Addr_2);
                            $('#MainContent_tbx_Addr_3').val(data.d.Addr_3);
                            $('#MainContent_tbx_Addr_4').val(data.d.Addr_4);
                            $('#MainContent_tbx_Addr_5').val(data.d.Addr_5);
                            //$('#MainContent_province').val(data.d.Prvn_Code);
                            //$('#MainContent_country').val(data.d.Cntr_Code);
                            //$('#MainContent_tbx_ZipCode').val(data.d.Zip_Code);
                            $('#MainContent_tbx_Home_Phone').val(data.d.Home_Phone);
                            $('#MainContent_tbx_Email').val(data.d.Email);
                            $('#MainContent_tbx_remark').val(data.d.remark);
                            $('#MainContent_tbx_taxid').val(data.d.TaxID);
                            //$('#RBLExperienceApplicable').val(SelectdValue);
                            RadionButtonSelectedValueSet('MainContent_rbl_gndr_code', data.d.gndr_code);

                            $("#dialog-customer").dialog({
                                resizable: false,
                                height: window.innerHeight,
                                width: 900,
                                modal: true,
                                title: 'Update Customer Info',
                                open: function (event, ui) {
                                    $('.ui-dialog').css('z-index', 103);
                                    $('.ui-widget-overlay').css('z-index', 102);
                                },
                                buttons: {
                                    "Save": function () {
                                        SaveCustomer();
                                    },
                                    "Cancel": function () {
                                        dialog.dialog("close");
                                    }
                                }
                            });
                            return false;
                        }

                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        console.log(xmlHttpRequest.responseText);
                        console.log(textStatus);
                        console.log(errorThrown);
                    }
                });

            };
            function RadionButtonSelectedValueSet(name, SelectdValue) {
                //$('input[name="' + name + '"][value="' + SelectdValue + '"]').attr('checked', true);
                //var evalue = radGrp[i].value 
                var radioList = "MainContent_rbl_gndr_code";
                var radioButtonList = document.getElementById(radioList);
                var listItems = radioButtonList.getElementsByTagName("input");
                for (var i = 0; i < listItems.length; i++) {
                    var evalue = listItems[i].value
                    if (evalue == SelectdValue) {
                        listItems[i].checked = true;
                    }
                }

            }
            function setFocus(id) {
                document.getElementById(id).focus();
            }
            SaveCustomer = function () {
                if (validateform()) {
                    var CustomerID = $("#MainContent_tbx_cust_code").val();
                    var prfx_name = $("#MainContent_tbx_prfx_name").val();
                    var first_name = $("#MainContent_tbx_first_name").val();
                    var Branch_No = $('#MainContent_txt_BranchNo').val();
                    var last_name = $("#MainContent_tbx_last_name").val();
                    var gndr_code = Validation();
                    var Addr_1 = $("#MainContent_tbx_Addr_1").val();
                    var Addr_2 = $("#MainContent_tbx_Addr_2").val();
                    var Addr_3 = $("#MainContent_tbx_Addr_3").val();
                    var Addr_4 = $("#MainContent_tbx_Addr_4").val();
                    var Addr_5 = $("#MainContent_tbx_Addr_5").val();
                    var prov = ""; //$('#MainContent_province').val();
                    var countr = ""; //$('#MainContent_country').val();
                    var Zip_Code = ""; //$("#MainContent_tbx_ZipCode").val();
                    var Home_Phone = $("#MainContent_tbx_Home_Phone").val();
                    var Email = $("#MainContent_tbx_Email").val();
                    var remark = $('#MainContent_tbx_remark').val();
                    var taxid = $("#MainContent_tbx_taxid").val();
                    var obj = {
                        CustomerID: CustomerID,
                        first_name: first_name,
                        last_name: last_name,
                        prfx_name: prfx_name,
                        Branch_No: Branch_No,
                        gndr_code: gndr_code,
                        Addr_1: Addr_1,
                        Addr_2: Addr_2,
                        Addr_3: Addr_3,
                        Addr_4: Addr_4,
                        Addr_5: Addr_5,
                        Prvn_Code: prov,
                        Cntr_Code: countr,
                        Zip_Code: Zip_Code,
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
                            cache: false,
                            //data: { 'key': obj.cust_code, 'mode': $('#txt_FormState').val() },
                            data: JSON.stringify({ obj: obj, mode: $('#txt_FormState').val() }),
                            //data: "{ 'cust_code':'" + obj.cust_code + "','first_name':'" + obj.first_name +
                            //    "','last_name':'" + obj.last_name + "','prfx_name':'" + obj.prfx_name +
                            //    "','mode':'" + $('#txt_FormState').val() + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.IsSuccessfull) {
                                    //alert('Save Success');
                                    dialog.dialog("close");
                                    alert(data.d.Message);
                                    //$('#MainContent_Button1').click();
                                    $('#MainContent_ImageButton2').click();

                                    //window.open('');
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
                }
            };
            validateform = function () {
                var bool = true;
                //var cust_code = $("#MainContent_tbx_cust_code").val();
                //if (cust_code == "") {
                //    alert('Please fill the customer code.');
                //    setFocus("MainContent_tbx_cust_code");
                //    return false;
                //}
                //var prfx_name = $("#MainContent_tbx_prfx_name").val();

                //if (prfx_name == "") {
                //    alert('Please fill prefix name');
                //    setFocus("MainContent_tbx_prfx_name");
                //    return false;
                //}
                var first_name = $("#MainContent_tbx_first_name").val();

                if (first_name == "") {
                    alert('Please fill the firstname.');
                    setFocus("MainContent_tbx_first_name");
                    return false;
                }
                //var last_name = $("#MainContent_tbx_last_name").val();

                //if (last_name == "") {
                //    alert('Please fill the lastname.');
                //    setFocus("MainContent_tbx_last_name");
                //    return false;
                //}
                //var gndr_code = Validation();

                //if (!gndr_code) {
                //    alert('Please choose gender type.');
                //    //setFocus("MainContent_tbx_cust_code");
                //    return false;
                //}
                var Addr_1 = $("#MainContent_tbx_Addr_1").val();

                if (Addr_1 == "") {
                    alert('Please fill in the address.');
                    setFocus("MainContent_tbx_Addr_1");
                    return false;
                }
                //var Zip_Code = $("#MainContent_tbx_ZipCode").val();

                //if (Zip_Code == "") {
                //    alert('Please fill in zip code.');
                //    setFocus("MainContent_Zip_Code");
                //    return false;
                //}
                //var Home_Phone = $("#MainContent_tbx_Home_Phone").val();

                //if (Home_Phone == "") {
                //    alert('Please fill in telephone number.');
                //    setFocus("MainContent_tbx_Home_Phone");
                //    return false;
                //}
                //var Email = $("#MainContent_tbx_Email").val();

                //if (Email == "") {
                //    alert('Please fill in email. ');
                //    setFocus("MainContent_tbx_Email");
                //    return false;
                //}

                //var prov = $("#MainContent_province").val();

                //if (prov == "") {
                //    alert('Please fill in province.');
                //    setFocus("MainContent_province");
                //    return false;
                //}
                //var countr = $("#MainContent_country").val();

                //if (countr == "") {
                //    alert('Please fill in country. ');
                //    setFocus("MainContent_country");
                //    return false;
                //}
                var taxid = $("#MainContent_tbx_taxid").val();
                if (taxid == "") {
                    alert('Please fill in Tax ID. ');
                    setFocus("MainContent_tbx_taxid");
                    return false;
                }
                return bool;
            };
        });
    </script>

</asp:Content>
