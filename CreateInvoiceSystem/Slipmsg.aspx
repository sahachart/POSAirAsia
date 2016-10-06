<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Slipmsg.aspx.cs" Inherits="CreateInvoiceSystem.Slipmsg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="5" >Slip Message Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Slip Message Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap></td>
            <td style="width: 30%" ></td>
            <td style="width: 100%; vertical-align:middle; padding-left: 5px; padding-top: 3px" ></td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Head Line</td>
            <td style="width: 30%" ><asp:TextBox ID="theader" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Foot Line</td>
            <td style="width: 30%" ><asp:TextBox ID="tfooter" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
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
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="SlipMessage_Id" 
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
                                <asp:TemplateField HeaderText="Slip Message Code">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("SlipMessage_Id") %>');"><%# Eval("SlipMessage_Code") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head Line">   
                                    <ItemTemplate>
                                        <div style="text-align: center; vertical-align: middle;">
                                            <%# Eval("Header1") %><br />
                                            <%# Eval("Header2") %><br />
                                            <%# Eval("Header3") %><br />
                                            <%# Eval("Header4") %><br />
                                            <%# Eval("Header5") %><br />
                                            <%# Eval("Header6") %><br />
                                        </div>     
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Foot Line">   
                                    <ItemTemplate>
                                        <div style="text-align: center; vertical-align: middle;">
                                            <%# Eval("Footer1") %><br />
                                            <%# Eval("Footer2") %><br />
                                            <%# Eval("Footer3") %><br />
                                            <%# Eval("Footer4") %><br />
                                            <%# Eval("Footer5") %><br />
                                        </div>     
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                </asp:TemplateField>

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
    
    <div id="dialog-confirm" title="Create Slip Message">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Slip Message Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="code" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>HeadLine1<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="HeadLine1" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>FootLine1<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="FootLine1" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>HeadLine2<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="HeadLine2" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>FootLine2<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="FootLine2" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>HeadLine3<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="HeadLine3" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>FootLine3<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="FootLine3" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>HeadLine4<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="HeadLine4" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>FootLine4<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="FootLine4" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>HeadLine5<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="HeadLine5" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>FootLine5<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="FootLine5" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>HeadLine6<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="HeadLine6" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap></td>
                <td style="width: 60%"></td>
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
                    height: 600,
                    width: 800,
                    modal: true,
                    title: 'Create Slip Message',
                    open: function (event, ui) {

                        $('#MainContent_code').val('');
                        $('#MainContent_HeadLine1').val('');
                        $('#MainContent_FootLine1').val('');
                        $('#MainContent_HeadLine2').val('');
                        $('#MainContent_FootLine2').val('');
                        $('#MainContent_HeadLine3').val('');
                        $('#MainContent_FootLine3').val('');
                        $('#MainContent_HeadLine4').val('');
                        $('#MainContent_FootLine4').val('');
                        $('#MainContent_HeadLine5').val('');
                        $('#MainContent_FootLine5').val('');
                        $('#MainContent_HeadLine6').val('');
                        $('#MainContent_CheckBox1')[0].checked = false;
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    buttons: {
                        "Save": function () {
                            if ($('#MainContent_code').val() == '') {
                                alert('กรุณาระบุรหัส');
                                return false;
                            }
                            if ($('#MainContent_HeadLine1').val() == '') {
                                alert('กรุณาระบุ HeadLine1');
                                return false;
                            }
                            if ($('#MainContent_FootLine1').val() == '') {
                                alert('กรุณาระบุ FootLine1');
                                return false;
                            }
                            if ($('#MainContent_HeadLine2').val() == '') {
                                alert('กรุณาระบุ HeadLine2');
                                return false;
                            }
                            if ($('#MainContent_FootLine2').val() == '') {
                                alert('กรุณาระบุ FootLine2');
                                return false;
                            }
                            if ($('#MainContent_HeadLine3').val() == '') {
                                alert('กรุณาระบุ HeadLine3');
                                return false;
                            }
                            if ($('#MainContent_FootLine3').val() == '') {
                                alert('กรุณาระบุ FootLine3');
                                return false;
                            }
                            if ($('#MainContent_HeadLine4').val() == '') {
                                alert('กรุณาระบุ HeadLine4');
                                return false;
                            }
                            if ($('#MainContent_FootLine4').val() == '') {
                                alert('กรุณาระบุ FootLine4');
                                return false;
                            }
                            if ($('#MainContent_HeadLine5').val() == '') {
                                alert('กรุณาระบุ HeadLine5');
                                return false;
                            }
                            if ($('#MainContent_FootLine5').val() == '') {
                                alert('กรุณาระบุ FootLine5');
                                return false;
                            }
                            if ($('#MainContent_HeadLine6').val() == '') {
                                alert('กรุณาระบุ HeadLine6');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveSlipMsg',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'SlipMessage_Code': $('#MainContent_code').val()
                                    , 'Header1': $('#MainContent_HeadLine1').val()
                                    , 'Header2': $('#MainContent_HeadLine2').val()
                                    , 'Header3': $('#MainContent_HeadLine3').val()
                                    , 'Header4': $('#MainContent_HeadLine4').val()
                                    , 'Header5': $('#MainContent_HeadLine5').val()
                                    , 'Header6': $('#MainContent_HeadLine6').val()
                                    , 'Footer1': $('#MainContent_FootLine1').val()
                                    , 'Footer2': $('#MainContent_FootLine2').val()
                                    , 'Footer3': $('#MainContent_FootLine3').val()
                                    , 'Footer4': $('#MainContent_FootLine4').val()
                                    , 'Footer5': $('#MainContent_FootLine5').val()
                                    , 'active': $('#MainContent_CheckBox1')[0].checked

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
                url: 'servicepos.asmx/GetSlipMsg',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_code').val(obj[0].SlipMessage_Code);
                    $('#MainContent_HeadLine1').val(obj[0].Header1);
                    $('#MainContent_HeadLine2').val(obj[0].Header2);
                    $('#MainContent_HeadLine3').val(obj[0].Header3);
                    $('#MainContent_HeadLine4').val(obj[0].Header4);
                    $('#MainContent_HeadLine5').val(obj[0].Header5);
                    $('#MainContent_HeadLine6').val(obj[0].Header6);
                    $('#MainContent_FootLine1').val(obj[0].Footer1);
                    $('#MainContent_FootLine2').val(obj[0].Footer2);
                    $('#MainContent_FootLine3').val(obj[0].Footer3);
                    $('#MainContent_FootLine4').val(obj[0].Footer4);
                    $('#MainContent_FootLine5').val(obj[0].Footer5);
                    $('#MainContent_CheckBox1')[0].checked = (obj[0].Active == 'Y') ? true : false;

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 600,
                        width: 800,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Slip Message',
                        buttons: {
                            "Save": function () {
                                if ($('#MainContent_code').val() == '') {
                                    alert('กรุณาระบุรหัส');
                                    return false;
                                }
                                if ($('#MainContent_HeadLine1').val() == '') {
                                    alert('กรุณาระบุ HeadLine1');
                                    return false;
                                }
                                if ($('#MainContent_FootLine1').val() == '') {
                                    alert('กรุณาระบุ FootLine1');
                                    return false;
                                }
                                if ($('#MainContent_HeadLine2').val() == '') {
                                    alert('กรุณาระบุ HeadLine2');
                                    return false;
                                }
                                if ($('#MainContent_FootLine2').val() == '') {
                                    alert('กรุณาระบุ FootLine2');
                                    return false;
                                }
                                if ($('#MainContent_HeadLine3').val() == '') {
                                    alert('กรุณาระบุ HeadLine3');
                                    return false;
                                }
                                if ($('#MainContent_FootLine3').val() == '') {
                                    alert('กรุณาระบุ FootLine3');
                                    return false;
                                }
                                if ($('#MainContent_HeadLine4').val() == '') {
                                    alert('กรุณาระบุ HeadLine4');
                                    return false;
                                }
                                if ($('#MainContent_FootLine4').val() == '') {
                                    alert('กรุณาระบุ FootLine4');
                                    return false;
                                }
                                if ($('#MainContent_HeadLine5').val() == '') {
                                    alert('กรุณาระบุ HeadLine5');
                                    return false;
                                }
                                if ($('#MainContent_FootLine5').val() == '') {
                                    alert('กรุณาระบุ FootLine5');
                                    return false;
                                }
                                if ($('#MainContent_HeadLine6').val() == '') {
                                    alert('กรุณาระบุ HeadLine6');
                                    return false;
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditSlipMsg',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                        'SlipMessage_Code': $('#MainContent_code').val()
                                        , 'Header1': $('#MainContent_HeadLine1').val()
                                        , 'Header2': $('#MainContent_HeadLine2').val()
                                        , 'Header3': $('#MainContent_HeadLine3').val()
                                        , 'Header4': $('#MainContent_HeadLine4').val()
                                        , 'Header5': $('#MainContent_HeadLine5').val()
                                        , 'Header6': $('#MainContent_HeadLine6').val()
                                        , 'Footer1': $('#MainContent_FootLine1').val()
                                        , 'Footer2': $('#MainContent_FootLine2').val()
                                        , 'Footer3': $('#MainContent_FootLine3').val()
                                        , 'Footer4': $('#MainContent_FootLine4').val()
                                        , 'Footer5': $('#MainContent_FootLine5').val()
                                        , 'active': $('#MainContent_CheckBox1')[0].checked, 'id': id
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
