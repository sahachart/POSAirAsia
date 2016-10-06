<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FlightFeeGroup.aspx.cs" Inherits="CreateInvoiceSystem.FlightFeeGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="7">Upgrade Group</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap>Upgrade Group Name</td>
            <td style="width: 30%">
                <asp:TextBox ID="txt_GroupNameSearch" runat="server" CssClass="txtFormCell"></asp:TextBox>
            </td>
            <td style="width: 50%; vertical-align: middle; padding-left: 5px; padding-top: 3px">
                <asp:ImageButton ID="btn_Search" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" ClientIDMode="Static" OnClick="btn_Search_Click" />&nbsp;
                <asp:ImageButton ID="btn_Clear" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" ClientIDMode="Static" OnClick="btn_Clear_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; padding: 10px;" colspan="7">
                <table>
                    <tr>
                        <td style="vertical-align: top;">
                            <img id="addbtn" src="Images/add.png" width="20px" />
                        </td>
                        <td style="width: 500px">
                            <style>
                                #MainContent_GridView1 th {
                                    text-align: center;
                                }
                            </style>
                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="ID"
                                OnRowDeleting="GridView1_RowDeleting" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="700px"
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
                                    <asp:TemplateField HeaderText="Upgrade Group Name">
                                        <ItemTemplate>
                                            <a href="javascript:editmode('<%# Eval("ID") %>');"><%# Eval("NameTH") %></a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="300px" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Description" DataField="DescriptionTH" HeaderStyle-Width="300px" />
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
                            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <div id="dialog-confirm" title="Create Flight Fee" style="display: none;">
        <table style="width: 100%">
            <tr>
                <td class="tableform"  style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Group Name(TH)<span style="color: red">*</span></td>
                <td style="width: 60%">
                    <asp:TextBox ID="txt_NameTH" runat="server" Width="200px" CssClass="txtcalendar" ClientIDMode="Static" MaxLength="255"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tableform"  style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Group Name(EN)</td>
                <td style="width: 60%">
                    <asp:TextBox ID="txt_NameEN" runat="server" Width="200px" CssClass="txtcalendar" ClientIDMode="Static" MaxLength="255"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tableform" style="vertical-align: top; text-align: right; padding-right: 10px; width: 40%;" nowrap>Description</td>
                <td style="width: 60%">
                    <asp:TextBox ID="txt_DescriptionTH" runat="server" Width="80%" CssClass="txtcalendar" ClientIDMode="Static" MaxLength="255" TextMode="MultiLine" Rows="3" ></asp:TextBox></td>
            </tr>


        </table>
    </div>
    <script type="text/javascript">
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
                    $('#txt_NameTH').val('');
                    $('#txt_NameEN').val('');
                    $('#txt_DescriptionTH').val('');

                    $('.ui-dialog').css('z-index', 103);
                    $('.ui-widget-overlay').css('z-index', 102);
                },
                buttons: {
                    "Save": function () {

                        if ($('#txt_NameTH').val() == '') {
                            alert('กรุณาระบุชื่อ');
                            return false;
                        }
                        var msg = '';
                        $.ajax({
                            url: 'servicepos.asmx/SaveFlightFeeGroup',
                            type: "POST",
                            dataType: "xml",
                            cache: false,
                            data: {
                                'NameTH': $('#txt_NameTH').val(),
                                'NameEN': $('#txt_NameEN').val(),
                                'DescriptionTH': $('#txt_DescriptionTH').val(),
                                'DescriptionEN': ""
                            },
                            success: function (data) {
                                if (data.firstChild.textContent == '') {
                                    alert('Save Success');
                                    $('#btn_Search').click();
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
                        $(this).dialog("close");
                    }
                }
            });
        });

        function editmode(ID) {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $.ajax({
                url: 'servicepos.asmx/GetFlightFeeGroupByID',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'strID': ID },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    var EditID = obj.ID;
                    $('#txt_NameTH').val(obj.NameTH);
                    $('#txt_NameEN').val(obj.NameEN);
                    $('#txt_DescriptionTH').val(obj.DescriptionTH);
                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 400,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Upgrade Group',
                        buttons: {
                            "Save": function () {
                                if ($('#txt_NameTH').val() == '') {
                                    alert('กรุณาระบุชื่อ');
                                    return false;
                                }

                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/UpdateFlightFeeGroup',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                        'strID': EditID,
                                        'NameTH': $('#txt_NameTH').val(),
                                        'NameEN': $('#txt_NameEN').val(),
                                        'DescriptionTH': $('#txt_DescriptionTH').val(),
                                        'DescriptionEN': ""
                                    },
                                    success: function (data) {
                                        if (data.firstChild.textContent == '') {
                                            alert('Save Success');
                                            $('#btn_Search').click();
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
                                $(this).dialog("close");
                            }
                        }
                    });
                }
            });
        }
    </script>
</asp:Content>
