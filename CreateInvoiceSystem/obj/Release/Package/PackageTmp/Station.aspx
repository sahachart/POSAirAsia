<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Station.aspx.cs" Inherits="CreateInvoiceSystem.Station" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="7" >Station Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Station Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Station Name</td>
            <td style="width: 30%" ><asp:TextBox ID="tname" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Location</td>
            <td style="width: 30%" ><asp:TextBox ID="tloc" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
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
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="Station_Id" 
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
                                <asp:BoundField HeaderText="Station Code" DataField="Station_Code" HeaderStyle-Width="200px" />
                                <asp:TemplateField HeaderText="Station Name">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("Station_Id") %>');"><%# Eval("Station_Name") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Location" DataField="Location_Name" HeaderStyle-Width="200px" />
                                    <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
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
    
    <div id="dialog-confirm" title="Create Station" style="display: none;">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Station Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="code" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="3"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Station Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="name" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Location<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="location" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>ABB Slip</td>
                <td style="width: 60%">
                    <asp:DropDownList ID="ddl_ABBSlip" runat="server" Width="200px" CssClass="txtcalendar"></asp:DropDownList>
                    <span style="padding-left: 10px;">On A4</span>            
                    <asp:CheckBox ID="chk_IsABBOnA4" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Invoice Slip</td>
                <td style="width: 60%">
                    <asp:DropDownList ID="ddl_InvoiceSlip" runat="server" Width="200px" CssClass="txtcalendar"></asp:DropDownList>
                    <span style="padding-left: 10px;">On A4</span>            
                    <asp:CheckBox ID="chk_IsInvoiceOnA4" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Credit Note Slip</td>
                <td style="width: 60%">
                    <asp:DropDownList ID="ddl_CreditNoteSlip" runat="server" Width="200px" CssClass="txtcalendar"></asp:DropDownList>
                    <span style="padding-left: 10px;">On A4</span>            
                    <asp:CheckBox ID="chk_IsCreditNoteOnA4" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Active</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox1" runat="server" /></td>
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
                    height: 420,
                    width: 600,
                    modal: true,
                    title: 'Create Station',
                    open: function (event, ui) {
                        $('#MainContent_code').attr("disabled", false);
                        $('#MainContent_code').val('');
                        $('#MainContent_name').val('');
                        $('#MainContent_location').val('');
                        $('#MainContent_CheckBox1')[0].checked = false;
                        $('#MainContent_chk_IsABBOnA4')[0].checked = false;
                        $('#MainContent_chk_IsInvoiceOnA4')[0].checked = false;
                        $('#MainContent_chk_IsCreditNoteOnA4')[0].checked = false;
                        document.getElementById('MainContent_ddl_ABBSlip').selectedIndex = 0;
                        document.getElementById('MainContent_ddl_InvoiceSlip').selectedIndex = 0;
                        document.getElementById('MainContent_ddl_CreditNoteSlip').selectedIndex = 0;
                        //$('#MainContent_ddl_ABBSlip').selectedIndex = -1;
                        //$('#MainContent_ddl_InvoiceSlip').selectedIndex = -1;
                        //$('#MainContent_ddl_CreditNoteSlip').selectedIndex = -1;
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
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
                            if ($('#MainContent_location').val() == '') {
                                alert('กรุณาระบุสถานที่');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveStation',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'code': $('#MainContent_code').val(),
                                    'name': $('#MainContent_name').val(),
                                    'location': $('#MainContent_location').val(),
                                    'active': $('#MainContent_CheckBox1')[0].checked,
                                    'ABB_Print_A4': $('#MainContent_chk_IsABBOnA4')[0].checked,
                                    'INV_Print_A4': $('#MainContent_chk_IsInvoiceOnA4')[0].checked,
                                    'CN_Print_A4': $('#MainContent_chk_IsCreditNoteOnA4')[0].checked,
                                    'ABB_SlipMessage_Id': $('#MainContent_ddl_ABBSlip').val(),
                                    'INV_SlipMessage_Id': $('#MainContent_ddl_InvoiceSlip').val(),
                                    'CN_SlipMessage_Id': $('#MainContent_ddl_CreditNoteSlip').val()
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
                url: 'servicepos.asmx/GetStation',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_code').val(obj[0].Station_Code);
                    $('#MainContent_name').val(obj[0].Station_Name);
                    $('#MainContent_location').val(obj[0].Location_Name);
                    $('#MainContent_chk_IsABBOnA4')[0].checked = (obj[0].ABB_Print_A4 == true) ? true : false;
                    $('#MainContent_chk_IsInvoiceOnA4')[0].checked = (obj[0].INV_Print_A4 == true) ? true : false;
                    $('#MainContent_chk_IsCreditNoteOnA4')[0].checked = (obj[0].CN_Print_A4 == true) ? true : false;
                    $('#MainContent_ddl_ABBSlip').val(obj[0].ABB_SlipMessage_Id);
                    $('#MainContent_ddl_InvoiceSlip').val(obj[0].INV_SlipMessage_Id);
                    $('#MainContent_ddl_CreditNoteSlip').val(obj[0].CN_SlipMessage_Id);
                    $('#MainContent_CheckBox1')[0].checked = (obj[0].Active == 'Y') ? true : false;
                    $('#MainContent_code').attr("disabled", true);
                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 420,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Station',
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
                                if ($('#MainContent_location').val() == '') {
                                    alert('กรุณาระบุสถานที่');
                                    return false;
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditStation',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                        'code': $('#MainContent_code').val(),
                                        'name': $('#MainContent_name').val(),
                                        'location': $('#MainContent_location').val(),
                                        'active': $('#MainContent_CheckBox1')[0].checked,
                                        'id': id,
                                        'ABB_Print_A4': $('#MainContent_chk_IsABBOnA4')[0].checked ,
                                        'INV_Print_A4': $('#MainContent_chk_IsInvoiceOnA4')[0].checked,
                                        'CN_Print_A4': $('#MainContent_chk_IsCreditNoteOnA4')[0].checked,
                                        'ABB_SlipMessage_Id': $('#MainContent_ddl_ABBSlip').val(),
                                        'INV_SlipMessage_Id': $('#MainContent_ddl_InvoiceSlip').val(),
                                        'CN_SlipMessage_Id': $('#MainContent_ddl_CreditNoteSlip').val()
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
                                            $("#dialog-confirm").dialog("close");
                                        }
                                    }
                                });
                                
                            },
                            Cancel: function () {
                                $("#dialog-confirm").dialog("close");
                            }
                        }
                    });
                }
            });
        }
    </script>
</asp:Content>
