<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="posmac.aspx.cs" Inherits="CreateInvoiceSystem.posmac" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="7" >POS Machine Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >POS Machine Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>POS Machine No</td>
            <td style="width: 30%" ><asp:TextBox ID="tname" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap >Staion</td>
            <td style="width: 30%" ><asp:DropDownList ID="dlstation" runat="server" CssClass="txtFormCell"></asp:DropDownList></td>
            <td style="width: 100%; vertical-align:middle; padding-left: 5px; padding-top: 3px" ><asp:ImageButton ID="ImageButton1" 
                runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" 
                    OnClick="ImageButton2_Click" /></td>
        </tr>
        <tr>
            <td style=" text-align: center; padding: 10px;" colspan="7"  > 
    <table>
                <tr>
                    <td style="vertical-align:top;"><img id="addbtn" src="Images/add.png" width="20px" /></td>
                    <td>
                        <style>
                            #MainContent_GridView1 th{
                                text-align: center;
                            }
                        </style>
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="POS_Id" 
                            OnRowDeleting="OnRowDeleting" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" 
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px" AllowPaging="True" 
                            EnablePersistedSelection="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="POS Code" DataField="POS_Code" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderText="PID">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("POS_Id") %>');"><%# Eval("POS_MacNo") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Station Code" DataField="Station_Code" HeaderStyle-Width="100px" />
                                <asp:BoundField HeaderText="Station Name" DataField="Station_Code" HeaderStyle-Width="100px" />
                                <asp:BoundField HeaderText="Inv(ABB)" DataField="Last_Inv_No" HeaderStyle-Width="100px" />
                                <asp:BoundField HeaderText="Tax Inv" DataField="Last_TAX_No" HeaderStyle-Width="100px" />
                                <asp:BoundField HeaderText="Refund" DataField="Last_Refund_Inv_No" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" HeaderStyle-Width="30px" 
                                    DeleteImageUrl="Images/meanicons_24-20.png">
                                    <ControlStyle BorderStyle="Solid" BorderWidth="0px" />
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
                        <asp:Label ID="Label1" runat="server" Text="No Result" CssClass="Nodata"></asp:Label>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" style="display: none;" />
                    </td>
                </tr>
            </table>
    </td>
        </tr>
    </table>
    
    <div id="dialog-confirm" title="Create POS Machine" style="display: none;">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>POS Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="code" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="3"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>POS Machine No<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="macno" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Station<span style="color:red ">*</span></td>
                <td style="width: 60%">
                    <asp:DropDownList ID="dlstationadd" runat="server" Width="210px" CssClass="txtcalendar">
                    </asp:DropDownList></td>
            </tr>
            <tr style="display: none;">
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Slip Message<span style="color:red ">*</span></td>
                <td style="width: 60%">
                    <asp:DropDownList ID="dlslipadd" runat="server" Width="210px" CssClass="txtcalendar">
                    </asp:DropDownList></td>
            </tr>
            <tr style="display: none">
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Set EJ</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox1" runat="server" /></td>
            </tr>
            <tr style="display: none">
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Customer Display</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox2" runat="server" /></td>
            </tr>
            <tr style="display: none">
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Cash Drawer</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox3" runat="server" /></td>
            </tr>
            <tr style="display: none">
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Format Printing</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox4" runat="server" /></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Active</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox5" runat="server" /></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $("#addbtn").click(function () {
                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 400,
                    width: 600,
                    modal: true,
                    title: 'Create POS Machine',
                    open: function (event, ui) {
                        $('#MainContent_code').val('');
                        $('#MainContent_macno').val('');
                        $('#MainContent_dlstationadd').val('');
                        $('#MainContent_dlslipadd').val('');
                        $('#MainContent_CheckBox1')[0].checked = false;
                        $('#MainContent_CheckBox2')[0].checked = false;
                        $('#MainContent_CheckBox3')[0].checked = false;
                        $('#MainContent_CheckBox4')[0].checked = false;
                        $('#MainContent_CheckBox5')[0].checked = false;
                        $('#MainContent_code').attr("disabled", false);

                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    buttons: {
                        "Save": function () {
                            if ($('#MainContent_code').val() == '') {
                                alert('กรุณาระบุรหัส');
                                return false;
                            }
                            if ($('#MainContent_macno').val() == '') {
                                alert('กรุณาระบุรหัสเครื่อง');
                                return false;
                            }
                            
                            if ($('#MainContent_dlstationadd').val() == '') {
                                alert('กรุณาระบุ Station');
                                return false;
                            }

                            //if ($('#MainContent_dlslipadd').val() == '') {
                            //    alert('กรุณาระบุ Slip Message');
                            //    return false;
                            //}
                            
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SavePOS',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'code': $('#MainContent_code').val(), 'POS_MacNo': $('#MainContent_macno').val()
                                    , 'Station_Code': $('#MainContent_dlstationadd').val(), 'SlipMessage_Code': $('#MainContent_dlslipadd').val()
                                , 'SetEJ': $('#MainContent_CheckBox1')[0].checked
                                    , 'CusDisplay': $('#MainContent_CheckBox2')[0].checked
                                    , 'CashDrawer': $('#MainContent_CheckBox3')[0].checked
                                    , 'FormatPrint': $('#MainContent_CheckBox4')[0].checked
                                    , 'Active': $('#MainContent_CheckBox5')[0].checked
                                },
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
            });
        });

        function editmode(id) {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $.ajax({
                url: 'servicepos.asmx/GetPOS',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_code').val(obj[0].POS_Code);
                    $('#MainContent_macno').val(obj[0].POS_MacNo);
                    $('#MainContent_dlstationadd').val(obj[0].Station_Code);
                    $('#MainContent_dlslipadd').val(obj[0].SlipMessage_Code);
                    $('#MainContent_code').attr("disabled", true);

                    $('#MainContent_CheckBox1')[0].checked = (obj[0].SetEJ == 'Y') ? true : false;
                    $('#MainContent_CheckBox2')[0].checked = (obj[0].CusDisplay == 'Y') ? true : false;
                    $('#MainContent_CheckBox3')[0].checked = (obj[0].CashDrawer == 'Y') ? true : false;
                    $('#MainContent_CheckBox4')[0].checked = (obj[0].FormatPrint == 'Y') ? true : false;
                    $('#MainContent_CheckBox5')[0].checked = (obj[0].Active == 'Y') ? true : false;
                    

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 400,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit POS Machine',
                        buttons: {
                            "Save": function () {
                                if ($('#MainContent_code').val() == '') {
                                    alert('กรุณาระบุรหัส');
                                    return false;
                                }
                                if ($('#MainContent_macno').val() == '') {
                                    alert('กรุณาระบุรหัสเครื่อง');
                                    return false;
                                }

                                if ($('#MainContent_dlstationadd').val() == '') {
                                    alert('กรุณาระบุ Station');
                                    return false;
                                }

                                //if ($('#MainContent_dlslipadd').val() == '') {
                                //    alert('กรุณาระบุ Slip Message');
                                //    return false;
                                //}

                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditPOS',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                        'code': $('#MainContent_code').val(), 'POS_MacNo': $('#MainContent_macno').val()
                                    , 'Station_Code': $('#MainContent_dlstationadd').val(), 'SlipMessage_Code': $('#MainContent_dlslipadd').val()
                                , 'SetEJ': $('#MainContent_CheckBox1')[0].checked
                                    , 'CusDisplay': $('#MainContent_CheckBox2')[0].checked
                                    , 'CashDrawer': $('#MainContent_CheckBox3')[0].checked
                                    , 'FormatPrint': $('#MainContent_CheckBox4')[0].checked
                                    , 'Active': $('#MainContent_CheckBox5')[0].checked, 'id': id
                                    },
                                    success: function (data) {
                                        if (data.firstChild.textContent == '') {
                                            alert('Save Success');
                                            $('#MainContent_Button1').click();
                                        }
                                        else {
                                            msg = data.firstChild.textContent;
                                            alert(msg);
                                        }
                                        if (msg == '') {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                                
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
