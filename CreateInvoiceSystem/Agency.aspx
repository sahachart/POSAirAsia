<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Agency.aspx.cs" Inherits="CreateInvoiceSystem.Agency" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="5" >Agency Master</td>
        </tr>
          <tr>
            <td class="searchCriterialabel" nowrap >Agency Type Code</td>
            <td style="width: 30%" ><asp:DropDownList runat="server" ID="DropDownList2" CssClass="txtcalendar" width="200px" /></td>
            <td class="searchCriterialabel" nowrap></td>
            <td style="width: 30%" ></td>
            <td style="width: 100%; vertical-align:middle; padding-left: 5px; padding-top: 3px" ></td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Agency Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Agency Name</td>
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
                    <td>
                        <style>
                            #MainContent_GridView1 th{
                                text-align: center;
                            }
                        </style>
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="Agency_Id" 
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
                                <asp:BoundField HeaderText="Agency Code" DataField="Agency_Code" HeaderStyle-Width="200px" />
                                <asp:TemplateField HeaderText="Agency Name">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("Agency_Id") %>','<%# Eval("Agency_Type_Code") %>','<%# Eval("Agency_Code") %>');"><%# Eval("Agency_Name") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="400px" />
                                </asp:TemplateField>
                             <asp:BoundField HeaderText="AgentType Name" DataField="Agent_type_Name" HeaderStyle-Width="200px" />
                                     <asp:TemplateField HeaderText="Gen Invoice" HeaderStyle-Wrap="false">   
                                    <ItemTemplate>
                                      <asp:CheckBox runat="server" ID="chk1" Checked='<%# CheckINV(Eval("GenInvoice").ToString()) %>' Enabled="false" /> 
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-Width="30px">   
                                    <ItemTemplate>
                                     <asp:ImageButton ImageUrl="Images/meanicons_24-20.png" runat="server" ID="lbdel" 
                                          OnClientClick='<%# CreateConfirmation("Do you want Delete ", Eval("Agency_Code").ToString()) %>'
                                        OnClick="btndel_Click" AlternateText='<%# Eval("Agency_Id").ToString() %>'  />
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
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                    </td>
                </tr>
            </table>
    </td>
        </tr>
    </table>
    
    <div id="dialog-confirm" title="Create Agency">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Agency Type<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:DropDownList runat="server" ID="cmbList" CssClass="txtcalendar" width="200px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Agency Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="code" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Agency Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="name" runat="server" Width="200px" CssClass="txtcalendar" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Gen Invoice</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox1" runat="server" />
                    <asp:Label runat="server" ID="lbCheckCode" Visible="false"></asp:Label>


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
                    height: 350,
                    width: 600,
                    modal: true,
                    title: 'Create Agency',
                    open: function (event, ui) {
                        $('#MainContent_code').val('');
                        $('#MainContent_name').val('');
                        $('#MainContent_cmbList').val('0');
                        $('#MainContent_CheckBox1')[0].checked = false;
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                        $('#MainContent_code').attr("disabled", false);
                    },
                    buttons: {
                        "Save": function () {
                            if ($('#MainContent_cmbList').val() == '0') {
                                alert('กรุณาระบุ Agency Type');
                                return false;
                            }

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
                                url: 'servicepos.asmx/SaveAgency',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'code': $('#MainContent_code').val(), 'name': $('#MainContent_name').val()
                                , 'geninv': $('#MainContent_CheckBox1')[0].checked, 'AgencyType' :  $('#MainContent_cmbList').val()
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

        function editmode(id, Agency_Type_Code,Code) {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $.ajax({
                url: 'servicepos.asmx/GetAgency',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_code').val(obj[0].Agency_Code);
                    $('#MainContent_name').val(obj[0].Agency_Name);
                    $('#MainContent_CheckBox1')[0].checked = (obj[0].GenInvoice == 'Y') ? true : false;
                    $('#MainContent_cmbList').val(Agency_Type_Code);
                    $('#MainContent_code').attr("disabled", true);

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 350,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Agency',
                        buttons: {
                            "Save": function () {
                                if ($('#MainContent_cmbList').val() == '0') {
                                    alert('กรุณาระบุ Agency Type');
                                    return false;
                                }

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
                                    url: 'servicepos.asmx/EditAgency',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                        'code': $('#MainContent_code').val(), 'name': $('#MainContent_name').val()
                                        , 'geninv': $('#MainContent_CheckBox1')[0].checked, 'id': id,
                                        'AgenType': $('#MainContent_cmbList').val(), 'CheckCode': Code
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
