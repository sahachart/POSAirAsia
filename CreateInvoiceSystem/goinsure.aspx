<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="goinsure.aspx.cs" Inherits="CreateInvoiceSystem.goinsure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="7" >Service Type Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Service Type Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Service Type Name</td>
            <td style="width: 30%" ><asp:TextBox ID="tname" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>FeeType</td>
            <td style="width: 30%" ><asp:DropDownList ID="dllfee" runat="server" CssClass="txtFormCell"></asp:DropDownList></td>
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
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="Ginsure_Id" 
                            OnRowDeleting="OnRowDeleting" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" 
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px" AllowPaging="True" 
                            EnablePersistedSelection="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Service Type Code" DataField="Ginsure_Code" HeaderStyle-Width="300px" />
                                <asp:TemplateField HeaderText="Service Type Name">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("Ginsure_Id") %>');"><%# Eval("Ginsure_Name") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Display On FeeType" DataField="FlightFee" HeaderStyle-Width="300px" />
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" HeaderStyle-Width="30px" 
                                    DeleteImageUrl="Images/meanicons_24-20.png">
                                    <ControlStyle BorderStyle="Solid" BorderWidth="0px" />
                                    </asp:CommandField>
                                </Columns>
                                <FooterStyle BackColor="#56575B" />
                                <HeaderStyle BackColor="#56575B" Font-Bold="True" ForeColor="White" Height="30px" />
                                <PagerStyle BackColor="#56575B"  ForeColor="White" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F0F0F0" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        <asp:Label ID="Label1" runat="server" Text="No Result" CssClass="Nodata"></asp:Label>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                    </td>
                </tr>
            </table>
    </td>
        </tr>
    </table>
    
    <div id="dialog-confirm" title="Create Service Type">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Service Type Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="code" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Service Type Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="name" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Display On FeeType<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:DropDownList ID="DropDownList1" runat="server" Width="210px" CssClass="txtcalendar"></asp:DropDownList></td>
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
                    title: 'Create Service Type',
                    open: function (event, ui) {

                        $('#MainContent_code').val('');
                        $('#MainContent_name').val('');
                        $('#MainContent_DropDownList1').val('');
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
                            if ($('#MainContent_DropDownList1').val() == '') {
                                alert('กรุณาระบุ FeeType');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveGoInsure',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'code': $('#MainContent_code').val()
                                    , 'name': $('#MainContent_name').val()
                                    , 'flightfee': $('#MainContent_DropDownList1').val()

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
                url: 'servicepos.asmx/GetGoInsure',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_code').val(obj[0].Ginsure_Code);
                    $('#MainContent_name').val(obj[0].Ginsure_Name);
                    $('#MainContent_DropDownList1').val(obj[0].FlightFee);

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 300,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Service Type',
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
                                if ($('#MainContent_DropDownList1').val() == '') {
                                    alert('กรุณาระบุ FeeType');
                                    return false;
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditGoInsure',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                        'code': $('#MainContent_code').val()
                                    , 'name': $('#MainContent_name').val()
                                    , 'flightfee': $('#MainContent_DropDownList1').val(), 'id': id
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
