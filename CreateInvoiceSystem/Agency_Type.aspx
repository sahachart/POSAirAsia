<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="Agency_Type.aspx.cs" Inherits="CreateInvoiceSystem.Agency_Type" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="5" >Agency Type Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Agency Type Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Agency Type Name</td>
            <td style="width: 30%" ><asp:TextBox ID="tname" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td style="width: 100%; vertical-align:middle; padding-left: 5px; padding-top: 3px" ><asp:ImageButton ID="ImageButton1" 
                runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" 
                    OnClick="ImageButton2_Click" /></td>
        </tr>
        <tr>
            <td style=" text-align: center; padding: 10px;" colspan="5"  > 
    <table>
                <tr>
                    <td style="vertical-align:top;"><img id="addbtn" src="Images/add.png" width="20px" /></td>
                    <td style="text-align:center;">
                        <style>
                            #MainContent_GridView1 th{
                                text-align: center;
                            }
                        </style>
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="AgencyTypeId" 
                             AutoGenerateColumns="False" 
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
                                <asp:BoundField HeaderText="Agency Type Code" DataField="Agent_type_code" HeaderStyle-Width="200px" />

                                <asp:TemplateField HeaderText="Agency Name">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("AgencyTypeId") %>','<%# Eval("Agent_type_code") %>','<%# Eval("Agent_type_Name") %>');"><%# Eval("Agent_type_Name") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="400px" />
                                </asp:TemplateField>

                                     <asp:BoundField HeaderText="Inv (ABB)" DataField="Last_Inv_No" HeaderStyle-Width="120px" />
                                     <asp:BoundField HeaderText="Tax Inv" DataField="Last_TAX_No" HeaderStyle-Width="120px" Visible="false" />
                                     <asp:BoundField HeaderText="Refund" DataField="Last_Refund_Inv_No" HeaderStyle-Width="120px" Visible="false" />

                                  
                               
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:ImageButton runat="server" ID="btnDel" ImageUrl="Images/meanicons_24-20.png"
                                                 AlternateText='<%# Eval("AgencyTypeId").ToString() %>'
                                                  OnClientClick="return confirm('Do You want to Delete?');" 
                                                onclick="btndel_click"
                                                 />
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
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" style="display: none;" />
                    </td>
                </tr>
            </table>
    </td>
        </tr>
    </table>
    
    <div id="dialog-confirm" title="Create Agency Type" style="display: none;">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Agency Type Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="code" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="30"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Agency Type Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="name" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="150"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="checkIsactive" />
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
                    title: 'Create Agency Type',
                    open: function (event, ui) {
                        $('#MainContent_code').val('');
                        $('#MainContent_name').val('');
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
                            if ($('#MainContent_name').val() == '') {
                                alert('กรุณาระบุชื่อ');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveAgencyType',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'code': $('#MainContent_code').val(), 'name': $('#MainContent_name').val()
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

        function editmode(ID,Code, Sname) {
            $.ajax({
                url: 'servicepos.asmx/GetAgencyType',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'code': Code, 'name': Sname},
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    $('#MainContent_code').val(Code);
                    $('#MainContent_name').val(Sname);
                    $('#MainContent_code').attr("disabled", true);
                    //$('#MainContent_code').prop('disabled', false);
                    //$('#MainContent_code').attr('disabled', '');

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 300,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Agency Type',
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
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditAgencyType',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                       'ID': ID, 'code': $('#MainContent_code').val(), 'name': $('#MainContent_name').val()
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
