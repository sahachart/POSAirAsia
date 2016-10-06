<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="countrymaster.aspx.cs" Inherits="CreateInvoiceSystem.countrymaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="5" >Country Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Country Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcountrycode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Country Name</td>
            <td style="width: 30%" ><asp:TextBox ID="tcountryname" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td style="width: 100%; vertical-align:middle; padding-left: 5px; padding-top: 3px" ><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" OnClick="ImageButton2_Click" /></td>
        </tr>
        <tr>
            <td style=" text-align: center; padding: 10px;" colspan="5"  >
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align:top;"><img id="addbtn" src="Images/add.png" width="20px" /></td>
                        <td >
                            <style>
                                #MainContent_GridView1 th{
                                    text-align: center;
                                }
                            </style>
                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" 
                                            DataKeyNames="CountryId" OnRowDeleting="OnRowDeleting" 
                                            AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" 
                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                                            GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px" 
                                            AllowPaging="True" EnablePersistedSelection="True" 
                                            OnPageIndexChanging="GridView1_PageIndexChanging"
                                            PageSize="20">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <asp:TemplateField HeaderText="No.">   
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>   
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Country Code" DataField="CountryCode" HeaderStyle-Width="200px" />
                                <asp:TemplateField HeaderText="Country Name">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("CountryId") %>');"><%# Eval("CountryName") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="400px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" HeaderStyle-Width="30px" DeleteImageUrl="Images/meanicons_24-20.png">
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
    <div id="dialog-confirm" title="Create Country" style="display: none;">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Country Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="countrycode" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Country Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="countryname" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="50"></asp:TextBox></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        $(function ()
        {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $("#addbtn").click(function () {
                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 300,
                    width: 600,
                    modal: true,
                    title: 'Create Country',
                    open: function (event, ui) {
                        document.getElementById("MainContent_countrycode").disabled = false;
                        $('#MainContent_countrycode').val('');
                        $('#MainContent_countryname').val('');
                        $('.ui-dialog').css('z-index',103);
                        $('.ui-widget-overlay').css('z-index', 102);

                    },
                    buttons: {
                        "Save": function () {
                            if ($('#MainContent_countrycode').val() == '') {
                                alert('กรุณาระบุรหัสประเทศ');
                                return false;
                            }
                            if ($('#MainContent_countryname').val() == '') {
                                alert('กรุณาระบุชื่อประเทศ');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveCountry',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                async: false,
                                data: { 'code': $('#MainContent_countrycode').val(), 'name': $('#MainContent_countryname').val() },
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
        $(document).ready(function () {
            //$("#dialog-confirm").hide();
        });
        function popupAlert(str) {
            //$("#dialog-confirm").hide();
            alert(str);
        }
        function editmode(id) {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $.ajax({
                url: 'servicepos.asmx/GetCountry',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_countrycode').val(obj[0].CountryCode);
                    $('#MainContent_countryname').val(obj[0].CountryName);
                      document.getElementById("MainContent_countrycode").disabled = true;

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 300,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index',103);
                            $('.ui-widget-overlay').css('z-index',102);
                        },
                        title:'Edit Country',
                        buttons: {
                            "Save": function () {
                                if ($('#MainContent_countrycode').val() == '') {
                                    alert('กรุณาระบุรหัสประเทศ');
                                    return false;
                                }
                                if ($('#MainContent_countryname').val() == '') {
                                    alert('กรุณาระบุชื่อประเทศ');
                                    return false;
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditCountry',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: { 'code': $('#MainContent_countrycode').val(), 'name': $('#MainContent_countryname').val(), 'id': id },
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
