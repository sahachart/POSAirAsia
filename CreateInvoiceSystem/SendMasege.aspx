<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="SendMasege.aspx.cs" Inherits="CreateInvoiceSystem.SendMasege" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
 <script>
               $(function () {
                   $("#MainContent_startDate").datepicker({
                       showOn: 'button',
                       buttonImage: '../Images/calender.png',
                       buttonImageOnly: true,
                       changeMonth: true,
                       changeYear: true,
                       showAnim: 'slideDown',
                       duration: 'fast',
                       dateFormat: 'dd/mm/yy',

                       onSelect: function () {
                           var i =  $("#startDate").val();
                           $("#lbStartDate").val(i);
                       }

                   });
               });
               $(function () {

                   $("#MainContent_ExpireDate").datepicker({
                       showOn: 'button',
                       buttonImage: '../Images/calender.png',
                       buttonImageOnly: true,
                       changeMonth: true,
                       changeYear: true,
                       showAnim: 'slideDown',
                       duration: 'fast',
                       dateFormat: 'dd/mm/yy',
                       onSelect: function () {
                           var i = $("#ExpireDate").val();
                           $("#lbExpireDate").val(i);
                       }
                   });
               });

               $(function () {
                   $("#MStartDate").datepicker({
                       showOn: 'button',
                       buttonImage: '../Images/calender.png',
                       buttonImageOnly: true,
                       changeMonth: true,
                       changeYear: true,
                       showAnim: 'slideDown',
                       duration: 'fast',
                       dateFormat: 'dd/mm/yy',

                       onSelect: function () {
                           var i = $("#MStartDate").val();
                           $("#lbMstartDate").val(i);
                       }
                   });
               });
               $(function () {
                   $("#MExpireDate").datepicker({
                       showOn: 'button',
                       buttonImage: '../Images/calender.png',
                       buttonImageOnly: true,
                       changeMonth: true,
                       changeYear: true,
                       showAnim: 'slideDown',
                       duration: 'fast',
                       dateFormat: 'dd/mm/yy',
                       onSelect: function () {
                           var i = $("#MExpireDate").val();
                           $("#lbMExpireDate").val(i);
                       }
                   });
               });

</script>
    <style type="text/css">
        #date-button {
    position: absolute;
    top: 0;
    left: 0;
    z-index: 2;
    height :10px;
}

#date-field {
    position: absolute;
    top: 0;
    left: 0;
    z-index: 1;
    width: 1px;
    height: 32px; 
    opacity: 0;
}

    </style>
   
 <div style="display : none;" >
     <asp:HiddenField runat="server" ID="lbStartDate" ClientIDMode="Static" />
     <asp:HiddenField runat="server" ID="lbExpireDate" ClientIDMode="Static" />
      <asp:TextBox ID="tname" runat="server" CssClass="txtFormCell"></asp:TextBox>
     </div> 
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="5" >Send Message Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Effective Date</td>
<td style="width: 30%" > <input type="text"  id="startDate" readonly="readonly" runat="server" />

</td>
            <td class="searchCriterialabel" nowrap>Ineffective Date</td>
            <td style="width: 30%" > <input type="text"  id="ExpireDate" readonly="readonly" runat="server" /></td>
            <td style="width: 30%; vertical-align:middle; padding-left: 5px; padding-top: 3px" ><asp:ImageButton ID="ImageButton3" 
                runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" 
                    OnClick="ImageButton2_Click" /></td>
            <td style="width: 100%"></td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Message</td>
<td colspan="5" > <asp:TextBox runat="server" ID="txtMessage" Width="50%"></asp:TextBox> </td>
            
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
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="ID" 
                            AutoGenerateColumns="False"
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
                                <asp:TemplateField HeaderText="Message">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("ID") %>','<%# Eval("DescMessage") %>','<%# Eval("StartDate") %>','<%# Eval("ExpireDate") %>','<%# Eval("IsActive") %>');"><%# Eval("DescMessage") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="400px" />
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Effective Date">   
                                    <ItemTemplate>
                                         <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StartDate","{0:dd/MM/yyyy}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Ineffective Date" >
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ExpireDate","{0:dd/MM/yyyy}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField> 
                                  <asp:TemplateField HeaderText="IsActive" Visible="false">   
                                    <ItemTemplate>
                                      <asp:CheckBox runat="server" ID="chk1" Checked='<%# CheckINV(Eval("IsActive").ToString()) %>' Enabled="false" /> 
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-Width="30px">   
                                    <ItemTemplate>
                                     <asp:ImageButton ImageUrl="Images/meanicons_24-20.png" runat="server" ID="lbdel" 
                                       OnClientClick='<%# CreateConfirmation("Do you want Delete ", Eval("DescMessage").ToString()) %>'
                                        OnClick="btndel_Click" AlternateText='<%# Eval("ID").ToString() %>'  />
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

      


    <div id="dialog-confirm" title="Create Message">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 15%;" nowrap>Effective Date<span style="color:red ">*</span></td>
                <td style="width: 15%"><input type="text"  style="width : 110px;" id="MStartDate" readonly="readonly" /></td>
            </tr>
            <tr>
                 <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>
                     Ineffective Date:<span style="color:red ">*</span>
                </td>
             <td style="width: 60%">
                <input type="text"  style="width : 110px;" id="MExpireDate" readonly="readonly" />
                </td>

            </tr>
            <tr>
                 <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>
                     Message:<span style="color:red ">*</span>
                </td>
             <td style="width: 60%">
                 <asp:TextBox runat="server" ID="txtMessageModal" TextMode="MultiLine" Width="100%" MaxLength="100" ></asp:TextBox>
                </td>

            </tr>
            <tr style="display: none">

                  <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>IsActive</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox1" runat="server" />
                <asp:Label runat="server" ID="lbCheckCode" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
       
        <div >

              <asp:HiddenField runat="server" ID="lbMstartDate" ClientIDMode="Static" />
     <asp:HiddenField runat="server" ID="lbMExpireDate" ClientIDMode="Static" />
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $("#addbtn").click(function () {
                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 350,
                    width: 750,
                    modal: true,
                    title: 'Create Message',
                    open: function (event, ui) {
                        $('#MainContent_txtMessageModal').val('');
                        $('#lbMstartDate').val('');
                        $('#lbMExpireDate').val('');
                        $('#MStartDate').val('');
                        $('#MExpireDate').val('');
                        $('#MainContent_CheckBox1')[0].checked = false;
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    buttons: {
                        "Save": function () {

                            
                            if ($('#lbMstartDate').val() == '') {
                                alert('กรุณาระบุ Effective Date');
                                return false;
                            }
                         
                            if ($('#lbMExpireDate').val() == '') {
                                alert('กรุณาระบุ Ineffective Date');
                                return false;
                            }

                            if ($('#MainContent_txtMessageModal').val() == '') {
                                alert('กรุณาระบุ Message');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveSendMessage',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'DescMessage': $('#MainContent_txtMessageModal').val(), 's': $('#lbMstartDate').val(),
                                    'e': $('#lbMExpireDate').val()
                                , 'stat': $('#MainContent_CheckBox1')[0].checked
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

        function editmode(id,des,start,expire,stat){ 
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
           
            $.ajax({
                url: 'servicepos.asmx/GetM_SendMessage',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'ID': id, 'name': des, 's': start, 'e': expire ,'stat': stat},
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);

                    $('#MainContent_txtMessageModal').val(obj[0].DescMessage);
                    $('#lbMstartDate').val(obj[0].Create_By);
                    $('#lbMExpireDate').val(obj[0].Update_By);
                    $('#MStartDate').val(obj[0].Create_By);
                    $('#MExpireDate').val(obj[0].Update_By);
                    $('#MainContent_CheckBox1')[0].checked = (obj[0].IsActive == 'Y') ? true : false;
                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 350,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Message',
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
                                    url: 'servicepos.asmx/EditM_SendMessage',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {
                                        'ID': id, 'name':   $('#MainContent_txtMessageModal').val(),
                                        's' :$('#lbMstartDate').val(),'e': $('#lbMExpireDate').val(), 'stat': $('#MainContent_CheckBox1')[0].checked
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
