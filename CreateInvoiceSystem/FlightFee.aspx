<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FlightFee.aspx.cs" Inherits="CreateInvoiceSystem.FlightFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="7">Flight Fee Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap>Flight Fee Code</td>
            <td style="width: 30%">
                <asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Flight Fee Name</td>
            <td style="width: 30%">
                <asp:TextBox ID="tname" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Group</td>
            <td style="width: 30%">
                <asp:DropDownList ID="dllgrp" runat="server" CssClass="txtFormCell">
                    <asp:ListItem Selected="True"></asp:ListItem>
                    <asp:ListItem Value="ADMINFEE">ADMINFEE</asp:ListItem>
                    <asp:ListItem Value="FUEL">FUEL</asp:ListItem>
                    <asp:ListItem Value="SERVICE">SERVICE</asp:ListItem>
                    <asp:ListItem Value="VAT">VAT</asp:ListItem>
                    <asp:ListItem Value="AIRPORTTAX">AIRPORTTAX</asp:ListItem>
                    <asp:ListItem Value="INSURANCE">INSURANCE</asp:ListItem>
                    <asp:ListItem Value="REP">REP</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 100%; vertical-align: middle; padding-left: 5px; padding-top: 3px">
                <asp:ImageButton ID="ImageButton1"
                    runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px"
                    OnClick="ImageButton2_Click" /></td>
        </tr>
        <tr>
            <td style="text-align: center; padding: 10px;" colspan="7">
                <table>
                    <tr>
                        <td style="vertical-align: top;">
                            <img id="addbtn" src="Images/add.png" width="20px" /></td>
                        <td>
                            <style>
                                #MainContent_GridView1 th {
                                    text-align: center;
                                }
                            </style>
                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="Flight_Fee_Id"
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
                                    <asp:BoundField HeaderText="Flight Fee Code" DataField="Flight_Fee_Code" HeaderStyle-Width="200px" />
                                    <asp:TemplateField HeaderText="Flight Fee Name">
                                        <ItemTemplate>
                                            <a href="javascript:editmode('<%# Eval("Flight_Fee_Id") %>');"><%# Eval("Flight_Fee_Name") %></a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="400px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display On ABB" Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Group" DataField="AbbGroup" HeaderStyle-Width="100px" />
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" HeaderStyle-Width="30px"
                                        DeleteImageUrl="Images/meanicons_24-20.png" Visible="false">
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
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Style="display: none;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <div id="dialog-confirm" title="Create Flight Fee" style="display: none;">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Flight Fee Code<span style="color: red">*</span></td>
                <td style="width: 60%">
                    <asp:TextBox ID="code" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Flight Fee Name<span style="color: red">*</span></td>
                <td style="width: 60%">
                    <asp:TextBox ID="name" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr style="display: none">
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Display On ABB</td>
                <td style="width: 60%">
                    <asp:CheckBox ID="CheckBox1" runat="server" /></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Group</td>
                <td style="width: 60%">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="210px" CssClass="txtcalendar">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem Value="ADMINFEE">ADMINFEE</asp:ListItem>
                        <asp:ListItem Value="FUEL">FUEL</asp:ListItem>
                        <asp:ListItem Value="SERVICE">SERVICE</asp:ListItem>
                        <asp:ListItem Value="VAT">VAT</asp:ListItem>
                        <asp:ListItem Value="AIRPORTTAX">AIRPORTTAX</asp:ListItem>
                        <asp:ListItem Value="INSURANCE">INSURANCE</asp:ListItem>
                        <asp:ListItem Value="REP">REP</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Upgrade Group</td>
                <td style="width: 60%">
                    <asp:DropDownList ID="ddl_FlightFeeGroup" runat="server" Width="210px" ClientIDMode="Static" CssClass="txtcalendar"></asp:DropDownList>
                </td>
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
                    title: 'Create Flight Fee',
                    open: function (event, ui) {
                        $('#MainContent_code').val('');
                        $('#MainContent_name').val('');
                        $('#MainContent_CheckBox1')[0].checked = false;
                        $('#MainContent_code').attr("disabled", false);
                        $('#ddl_FlightFeeGroup').val('');

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
                            if ($('#MainContent_CheckBox1')[0].checked) {
                                if ($('#MainContent_DropDownList1').val() == '') {
                                    alert('กรุณาระบุ Group');
                                    return false;
                                }
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveFlightFee',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'code': $('#MainContent_code').val(),
                                    'name': $('#MainContent_name').val(),
                                    'displayabb': $('#MainContent_CheckBox1')[0].checked,
                                    'abbgrp': $('#MainContent_DropDownList1').val(),
                                    'strFlightFeeGroupID': $('#ddl_FlightFeeGroup').val()
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
                url: 'servicepos.asmx/GetFlightFee',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_code').val(obj[0].Flight_Fee_Code);
                    $('#MainContent_name').val(obj[0].Flight_Fee_Name);
                    $('#MainContent_code').attr("disabled", true);
                    $('#MainContent_CheckBox1')[0].checked = (obj[0].DisplayAbb == 'Y') ? true : false;
                    $('#MainContent_DropDownList1').val(obj[0].AbbGroup);
                    $('#ddl_FlightFeeGroup').val(obj[0].FlightFeeGroup_ID);

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 400,
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
                                if ($('#MainContent_CheckBox1')[0].checked) {
                                    if ($('#MainContent_DropDownList1').val() == '') {
                                        alert('กรุณาระบุ Group');
                                        return false;
                                    }
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditFlightFee',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                        'code': $('#MainContent_code').val(),
                                        'name': $('#MainContent_name').val(),
                                        'displayabb': $('#MainContent_CheckBox1')[0].checked,
                                        'abbgrp': $('#MainContent_DropDownList1').val(),
                                        'id': id,
                                        'strFlightFeeGroupID': $('#ddl_FlightFeeGroup').val()
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
                }
            });
        }
    </script>
</asp:Content>
