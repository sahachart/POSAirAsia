<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListOfValue.aspx.cs" Inherits="CreateInvoiceSystem.ListOfValue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td style="vertical-align:top;"><img id="addbtn" src="Images/add.png" width="20px" /></td>
            <td>
                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="LovId" OnRowDeleting="OnRowDeleting" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                    <asp:TemplateField HeaderText="No.">   
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>   
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">   
                        <ItemTemplate>
                            <a href="javascript:editmode('<%# Eval("LovId") %>');"><%# Eval("LovDesc") %></a>       
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="LovType" HeaderText="Type" HeaderStyle-Width="200px" />
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" >
                        <ControlStyle BackColor="#6699FF" BorderStyle="Solid" BorderWidth="0px" />
                        </asp:CommandField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            </td>
        </tr>
    </table>
    <div id="dialog-confirm" title="Create List Of Value">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Desc<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="desc" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px;">Type<span style="color:red ">*</span></td>
                <td>
                    <asp:DropDownList ID="typelov" runat="server" CssClass="txtcalendar" Width="210px">
                        <asp:ListItem Value="1" Text="Country"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Province"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Currency Exchange"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Payment Type"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Station"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Slip Message"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Flight Type"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Flight Fee"></asp:ListItem>
                        <asp:ListItem Value="9" Text="POS / POS Machine"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Code Fee"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Agency"></asp:ListItem>
                        <asp:ListItem Value="12" Text="CN Reason"></asp:ListItem>
                        <asp:ListItem Value="13" Text="Service for Goinsure"></asp:ListItem>
                        <asp:ListItem Value="14" Text="User Group"></asp:ListItem>
                    </asp:DropDownList>
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
                    height: 300,
                    width: 600,
                    modal: true,
                    open: function (event, ui) {
                        $('#MainContent_desc').val('');
                        $('#MainContent_typelov').val('1');
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    buttons: {
                        "Save": function () {

                            if ($('#MainContent_desc').val() == '') {
                                alert('กรุณาระบุคำอธิบาย');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/Savelov',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: { 'name': $('#MainContent_desc').val(), 'typelov': $('#MainContent_typelov').val() },
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
                url: 'servicepos.asmx/GetLov',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_desc').val(obj[0].LovDesc);
                    $('#MainContent_typelov').val(obj[0].LovType);

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 300,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit List Of Value',
                        buttons: {
                            "Save": function () {

                                if ($('#MainContent_desc').val() == '') {
                                    alert('กรุณาระบุคำอธิบาย');
                                    return false;
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditLov',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: { 'name': $('#MainContent_desc').val(), 'typelov': $('#MainContent_typelov').val(), 'id': id },
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
