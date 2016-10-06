<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="currencymaster.aspx.cs" Inherits="CreateInvoiceSystem.currencymaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" type="text/css"/>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <table class="searchCriteria">
            <tr>
                <td class="headlabel" colspan="5" >Currency Exchange</td>
            </tr>
            <tr>
                <td class="searchCriterialabel" nowrap style="width: 10%" >Currency Code</td>
                <td style="width: 10%" >
                    <asp:TextBox ID="TextBox_Currency_Code" runat="server" CssClass="txtFormCell" Width="120px"></asp:TextBox>
                </td>
                <td class="searchCriterialabel" nowrap style="width: 10%" >Currency Date</td>
                <td style="width: 15%" >
                    <asp:TextBox ID="dtp_CurrencyDate" runat="server" CssClass="" Width="140px" ClientIDMode="Static"></asp:TextBox>
                </td>
                <td style="width: 100%;" >
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" /><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" OnClick="ImageButton2_Click" />
                </td>           
            </tr>  
            <tr>
                <td colspan="5">
                    <Table>
                    <tr>
                        <td style="vertical-align:top;"><img id="addbtn" src="Images/add.png" width="20px" /></td>
                        <td>
                            <center>
                                <style>
                                    #MainContent_GridView1 th{
                                        text-align: center;
                                    }
                                </style>
                                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True"  
                                    DataKeyNames="Currency_Id" AutoGenerateColumns="False" BackColor="White" 
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px" 
                                    AllowPaging="True" EnablePersistedSelection="True" PageSize="20" Width="100%" 
                                    OnPageIndexChanging="GridView1_PageIndexChanging" 
                                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="No.">   
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>   
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>                               
                                        <asp:BoundField DataField="Currency_Code" HeaderText="Currency Code">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Description">   
                                            <ItemTemplate>
                                                <a href="javascript:GetExchangeRate('<%# Eval("Currency_Id") %>','edit');"><%# Eval("Currency_Name") %></a>       
                                            </ItemTemplate>
                                            <HeaderStyle Width="400px" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ExChangeRate" HeaderText="ExChangeRate" >                                
                                        <ItemStyle HorizontalAlign="Right" Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Update_Date" HeaderText="Last Update" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsActive" HeaderText="Status" Visible="false" />
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" HeaderStyle-Width="30px" Visible="false" DeleteImageUrl="Images/meanicons_24-20.png">
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
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;">&nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </Table>
                </td>
                
            </tr>   
        </table>
    <div id="dialog-exchangerate">        
        <input class="hidden" type="text" id="txt_FormState" />       
        <table class="">
            <tr>
                
                <td class="searchCriterialabel" nowrap >Currency Code<span style="color:red">&nbsp;*</span></td>
                <td style="width: 70%" >
                    <asp:TextBox ID="tbx_Currency_Code" runat="server" CssClass="txtFormCell" Width="120px" MaxLength="50"></asp:TextBox>
                    
                </td>           
            </tr>        
            <tr>
                <td class="searchCriterialabel" nowrap >Currency Name<span style="color:red">&nbsp;*</span></td>
                <td style="width: 70%" >
                    <asp:TextBox ID="tbx_Currency_Name" runat="server" CssClass="txtFormCell" Width="300px" MaxLength="50"></asp:TextBox>        
                    
                </td>           
            </tr>        
            <tr>
                <td class="searchCriterialabel" nowrap >Exchange Rate<span style="color:red">&nbsp;*</span></td>
                <td style="width: 70%" >
                    <asp:TextBox ID="tbx_ExChangeRate" runat="server" CssClass="txtFormCell" Width="150px"></asp:TextBox>
                    
                </td>           
            </tr>        
        </table>
    </div>
    <input id="CurrId" type="hidden" />
    <script src="Scripts/WebForms/CIS-Customer/cis-master.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#dtp_CurrencyDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'Images/calender.png',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            //$("#dtp_CurrencyDate").datepicker("setDate", new Date());
            InitialFrom = function () {
                $('#CurrId').val('');
                $('#MainContent_txt_FormState').val('');
                $('#MainContent_tbx_Currency_Code').val('');
                $('#MainContent_tbx_Currency_Name').val('');
                $('#MainContent_tbx_ExChangeRate').val('');
                document.getElementById("MainContent_tbx_Currency_Code").disabled = false;
            };
            var dialog = $("#dialog-exchangerate");
            $("#dialog-exchangerate").hide();
            $("#addbtn").click(function () {
                InitialFrom();
                $('#txt_FormState').val('add');
                PopupForm('Create');
            });
            PopupForm = function (header) {
                $("#dialog-exchangerate").dialog({
                    resizable: false,
                    height: 400,
                    width: 800,
                    modal: true,
                    title: header +' Exchange Rate Info',
                    open: function (event, ui) {
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    buttons: {
                        "Save": function () {
                            SaveCurrency();
                        },
                        "Cancel": function () {
                            dialog.dialog("close");
                        }
                    }
                });
                return false;
            };
            GetExchangeRate = function (key,mode) {
                InitialFrom();
                $('#txt_FormState').val(mode);
                $.ajax({
                    url: 'servicepos.asmx/GetMstCurrencyByCode',
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
                                document.getElementById("MainContent_tbx_Currency_Code").disabled = true;
                            }
                            $('#CurrId').val(data.d.Currency_Id);
                            $('#MainContent_tbx_Currency_Code').val(data.d.Currency_Code);
                            $('#MainContent_tbx_Currency_Name').val(data.d.Currency_Name);
                            $('#MainContent_tbx_ExChangeRate').val(data.d.ExChangeRate);
                            PopupForm('Update');
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
            SaveCurrency = function () {
                
                if (validateform()) {
                    var Currency_Id = ($("#CurrId").val() == '' ? "-1" : $("#CurrId").val());
                    var Currency_Code = $("#MainContent_tbx_Currency_Code").val();
                    var Currency_Name = $("#MainContent_tbx_Currency_Name").val();
                    var ExChangeRate = $("#MainContent_tbx_ExChangeRate").val();
                    var last_name = $("#MainContent_tbx_last_name").val();
                    //var gndr_code = Validation();
                    var obj = {
                        Currency_Id: Currency_Id,
                        Currency_Code: Currency_Code,
                        Currency_Name: Currency_Name,
                        ExChangeRate: ExChangeRate,
                    };
                    var msg = '';
                    try {
                        $.ajax({
                            url: 'servicepos.asmx/SaveMstCurrency',
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
                                    dialog.dialog("close");
                                    alert(data.d.Message);
                                    $('#MainContent_ImageButton2').click();
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
                var val1 = $('#MainContent_tbx_ExChangeRate').val();
                var num = new Number(val1.replace(',', ''));
                if (isNaN(num)) {
                    bool = false;
                    alert('Please fill the number.');
                    setFocus("MainContent_tbx_ExChangeRate");
                    return false;
                };
                if ($('#MainContent_tbx_ExChangeRate').val() == "") {
                    bool = false;
                    alert('Please fill the exchange rate.');
                    setFocus("MainContent_tbx_ExChangeRate");
                    return false;
                }
                if ( $('#MainContent_tbx_Currency_Code').val() == "") {
                    bool = false;
                    alert('Please fill the currency code.');
                    setFocus("MainContent_tbx_Currency_Code");
                    return false;
                }
                if ($('#MainContent_tbx_Currency_Name').val() == "") {
                    bool = false;
                    alert('Please fill the currency name.');
                    setFocus("MainContent_tbx_Currency_Name");
                    return false;
                }
                return bool;
            };
        });
    </script>
</asp:Content>
