<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="UserGroup.aspx.cs" Inherits="CreateInvoiceSystem.UserGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="5" >UserGroup</td>
        </tr>
       
        <tr>
            <td class="searchCriterialabel" nowrap >UserGroup Code</td>
            <td style="width: 30%" ><asp:TextBox ID="tcode" runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>UserGroup Name</td>
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
                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="UserGroupId" 
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
                                <asp:BoundField HeaderText="UserGroup Code" DataField="UserGroupCode" HeaderStyle-Width="200px" />
                                <asp:TemplateField HeaderText="UserGroup Name">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("UserGroupId") %>','<%# IsVisible(Eval("Icount").ToString(),"1") %>');"><%# Eval("UserGroupName") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="400px" />
                                </asp:TemplateField>
                            
                                     <asp:TemplateField HeaderText="IsActive" Visible="false" >   
                                    <ItemTemplate>
                                      <asp:CheckBox runat="server" ID="chk1" Checked='<%# CheckINV(Eval("IsActive").ToString()) %>' Enabled="false" /> 
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-Width="30px">   
                                    <ItemTemplate>
                                     <asp:ImageButton ImageUrl="Images/meanicons_24-20.png" runat="server" ID="lbdel" 
                                          OnClientClick='<%# CreateConfirmation("Do you want Delete ", Eval("UserGroupCode").ToString()) %>'
                                        OnClick="btndel_Click" AlternateText='<%# Eval("UserGroupId").ToString() %>' ToolTip="ลบข้อมูล"  />
                                          </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">   
                                    <ItemTemplate>
                                        <a href="javascript:editRole('<%# Eval("UserGroupCode") %>');">Privilege</a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#56575B" />
                                <HeaderStyle BackColor="#56575B" Font-Bold="True" ForeColor="White" Height="30px" />
                                <PagerStyle BackColor="#56575B"  HorizontalAlign="Right" CssClass="GridPager" />
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
    
    <div id="dialog-confirm" title="Create UserGroup" style="display: none;">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>UserGroup Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="code" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>UserGroup Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="name" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr style="display: none">
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>IsActive</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox1" runat="server" />
                    <asp:Label runat="server" ID="lbCheckCode" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr style="">
                <td style="text-align: left; padding-right: 10px; width: 40%; border-bottom: solid; border-bottom-width: 1px; border-bottom-color: #e2e2e2;">Mapping User Group & AD</td>
                <td style="width: 60%"></td>
            </tr>
            <tr id="tr_AddAD">
                <td style="text-align: left; padding-right: 10px; width: 40%;" nowrap>
                    <img id="img_AddAD" src="Images/add.png" width="20px" />
                    <span class="AddDepartment">AD Department</span> 
                </td>
                <td style="width: 60%">
                    <asp:TextBox ID="txt_Department" runat="server" Width="200px" CssClass="txtcalendar AddDepartment" ClientIDMode="Static"></asp:TextBox>
                    <img id="img_AddItemAD" class="AddDepartment" src="Images/back-20.png" width="20px" />
                </td>
            </tr>
        </table>
        <table id="table_AD" class="table table-striped table-bordered" style="width: 100%">
            <thead>
                <tr style="background-color: #007acc; color: white">
                    <th style="width: 90%;">AD Department</th>
                    <%--<th>Edit</th>--%>
                    <th>Del</th>
                </tr>
            </thead>
            <tbody id="bodyAD">

            </tbody>
        </table>
    </div>

    <div id="dvresult" style="display: none">
        <table id="searchResult" style="width:100%;margin-left:auto;margin-right:auto; border: 1px solid; font-family: Tahoma; font-size: 12px;">
            <thead>
                <tr style="height:20px;text-align: center;background-color: gainsboro">
                    <th nowrap style="padding: 5px; font-size: 12px; width: 0%; ">Code</th>
                    <th nowrap style="padding: 5px; font-size: 12px; width: 95%; ">Name</th>
                    <th nowrap style="padding: 5px; font-size: 12px; width: 5%;text-align:center; ">View</th>
                    <%--<th nowrap style="padding: 5px; font-size: 12px; width: 5%;text-align:center;display:none ">Insert</th>
                    <th nowrap style="padding: 5px; font-size: 12px; width: 5%;text-align:center;display:none ">Delete</th>
                    <th nowrap style="padding: 5px; font-size: 12px; width: 5%;text-align:center;display:none ">Update</th>
                    <th nowrap style="padding: 5px; font-size: 12px; width: 5%;text-align:center;display:none ">Process</th>
                    <th nowrap style="padding: 5px; font-size: 12px; width: 5%;text-align:center;display:none ">Print</th>--%>
                </tr>
            </thead>
        </table>
    </div>
    <style>
        .GroupHeader {
            font-weight: bold;
            font-size: 1.5em;
            /*text-align: center;*/
            padding-left: 5px;
        } 
        .SubMenu {
            padding-left: 35px;
        }
        .center {
            text-align: center;
        }
    </style>
    <asp:HiddenField ID="jsonresult" runat="server" Value="" />
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-dvresult").hide();
            $("#dialog-confirm").hide();
            $("#addbtn").click(function () {
                $('.AddDepartment').hide();
                $('.tr_AD_Value').remove();
                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 350,
                    width: 600,
                    modal: true,
                    title: 'Create UserGroup',
                    open: function (event, ui) {
                        $('#MainContent_code').val('');
                        $('#MainContent_name').val('');
                        $('#MainContent_cmbList').val('0');
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
                            if ($('#MainContent_name').val() == '') {
                                alert('กรุณาระบุชื่อ');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveUserGroup',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                data: {
                                    'code': $('#MainContent_code').val(), 'name': $('#MainContent_name').val()
                                , 'Isactive': $('#MainContent_CheckBox1')[0].checked
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
            $('#img_AddAD').click(function () {
                //var col1 = '<td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>AD Department</td>';
                //var col2 = '<td style="width: 60%"><input class="txt_GroupAD txtcalendar" /></td>';
                //$('#tr_AddAD').after('<tr class="GroupAD">' + col1 + col2 + '</tr>');
                $('.AddDepartment').show();
            });
            $('#img_AddItemAD').click(function () {
                //"<tr><td style='text-align: center;'>" + ABBCheckBox + @"</td>
                //<td>" + ABBNo + @"</td>
                //<td>" + MasterDA.GetPaymentFromABBNo(ABB.ABBTid) + @"</td>
                //<td style='text-align: center;'>" + ORbtn + @"</td>
                //<td>" + INV_No + @"</td>
                //</tr>";

                var Value = $('#txt_Department').val().trim();
                if (Value == "") {
                    alert("กรุณากรอกข้อมูล");
                    return false;
                }

                for (var i = 0; i < $('.AD_Value').length; i++) {
                    if($('.AD_Value:eq('+i+')').text() == Value )
                    {
                        alert('พบข้อมูลซ้ำ');
                        return false;
                    }
                }

                var RowId = 'AD_' + Value;

                var col1 = '<td class="AD_Value">' + Value + '</td>';
                //var col2 = '<td style="text-align: center;"><img src="Images/Text-Edit-icon.png" width="20px" />' + '</td>';
                var col3 = '<td style="text-align: center;"><img src="Images/meanicons_24-20.png" width="20px" onclick="$(\'#' + RowId + '\').remove();" />' + '</td>';

                var RowHtml = '<tr id="' + RowId + '" class="tr_AD_Value">' + col1 + col3 + '</tr>';

                //var col1 = '<td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>AD Department</td>';
                //var col2 = '<td style="width: 60%"><input class="txt_GroupAD txtcalendar" /></td>';
                $('#bodyAD').append(RowHtml);
                $('#txt_Department').val('');
                $('.AddDepartment').hide();
            });
            
        });

        function editmode(id, viewstat) {
            $('.AddDepartment').hide();
            $('.tr_AD_Value').remove();
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-confirm").hide();
            $.ajax({
                url: 'servicepos.asmx/GetUserGroup',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $('#MainContent_code').val(obj[0].UserGroupCode);
                    $('#MainContent_name').val(obj[0].UserGroupName);
                    $('#MainContent_CheckBox1')[0].checked = (obj[0].IsActive == 'true') ? true : false;
                    var i = obj[0].Create_By;
                    if (i != "0") {
                        $('#MainContent_code').prop('disabled', false);
                        $('#MainContent_code').attr('disabled', '');
                    } else {
                        $("#MainContent_code").removeProp("disabled");
                        $("#MainContent_code").removeAttr("disabled");
                    }

                    for (var i = 0; i < obj[0].M_UserGroupMappingAD.length; i++) {

                        var Value = obj[0].M_UserGroupMappingAD[i].AD_Department;
                        var RowId = 'AD_' + Value;

                        var col1 = '<td class="AD_Value">' + Value + '</td>';
                        var col2 = '<td style="text-align: center;"><img src="Images/meanicons_24-20.png" width="20px" onclick="$(\'#' + RowId + '\').remove();" />' + '</td>';

                        var RowHtml = '<tr id="' + RowId + '" class="tr_AD_Value">' + col1 + col2 + '</tr>';

                        $('#bodyAD').append(RowHtml);
                    }
                    

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 350,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit UserGroup',
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

                                var UserGroupMappingADs = [];
                                for (var i = 0; i < $('.AD_Value').length; i++) {
                                    UserGroupMappingADs.push($('.AD_Value:eq(' + i + ')').text());
                                }

                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditUserGroup',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: {'id': id,
                                        'code': $('#MainContent_code').val(),
                                        'name': $('#MainContent_name').val(),
                                        'stat': $('#MainContent_CheckBox1')[0].checked,
                                        '_viewstat': viewstat,
                                        'oldcode': obj[0].UserGroupCode,
                                        'AD_Department': UserGroupMappingADs.length > 0 ? UserGroupMappingADs.join('|') : ""
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

        function setcanview(chk, i) {
            var strJson = $('#MainContent_jsonresult').val();
            var jsonObj = new Array();
            if (strJson.length > 0) {
                jsonObj = jQuery.parseJSON(strJson);
            }
            jsonObj.d[i].CanView = (chk.checked) ? '1' : '0';
            var json = JSON.stringify(jsonObj);
            $('#MainContent_jsonresult').val(json);
        }

        function editRole(UserGroupCode) {
            $("#dvresult").hide();
            $.ajax({
                url: 'servicepos.asmx/GetMenuPrivilege',
                type: "POST",
                dataType: "JSON",
                cache: false,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ UserGroupCode : UserGroupCode }),
                success: function (data) {
                    oTable.fnClearTable();
                    
                    for (var i = 0; i < data.d.length; i++) {
                        //oTable.fnAddData([data.d[i].Menu_Code, data.d[i].Menu_Name,
                        //    '<input onclick="setcancreate(this,' + i.toString() + ')" type="checkbox" ' + ((data.d[i].CanView == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanView == '2') ? 'disabled=true' : '') + ' />'
                        //    , '<input onclick="setcanedit(this,' + i.toString() + ')" type="checkbox" ' + ((data.d[i].CanInsert == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanInsert == '2') ? 'disabled=true' : '') + ' />'
                        //    , '<input onclick="setcanview(this,' + i.toString() + ')" type="checkbox" ' + ((data.d[i].CanDelete == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanDelete == '2') ? 'disabled=true' : '') + ' />'
                        //    , '<input onclick="setcanapprove(this,' + i.toString() + ')" type="checkbox" ' + ((data.d[i].CanUpdate == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanUpdate == '2') ? 'disabled=true' : '') + ' />'
                        //    , '<input onclick="setcanprint(this,' + i.toString() + ')" type="checkbox" ' + ((data.d[i].CanProcess == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanProcess == '2') ? 'disabled=true' : '') + ' />'
                        //    , '<input onclick="setcanreject(this,' + i.toString() + ')" type="checkbox" ' + ((data.d[i].CanPrint == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanPrint == '2') ? 'disabled=true' : '') + ' />']);

                        if (data.d[i].Menu_Name == "BookIng Information") {
                            oTable.fnAddData(['', '<div class="GroupHeader"><span>Booking</span></div>', '']);
                        }
                        else if (data.d[i].Menu_Name == "Invoice") {
                            oTable.fnAddData(['', '<div class="GroupHeader"><span>Invoice</span></div>', '']);
                        }
                        else if (data.d[i].Menu_Name == "รายงานยอดขาย ตามเครื่องจุดขาย") {
                            oTable.fnAddData(['', '<div class="GroupHeader"><span>Report</span></div>', '']);
                        }
                        else if (data.d[i].Menu_Name == "Currency Exchange") {
                            oTable.fnAddData(['', '<div class="GroupHeader"><span>Master Data</span></div>', '']);
                        }
                        else if (data.d[i].Menu_Name == "User Group") {
                            oTable.fnAddData(['', '<div class="GroupHeader"><span>User Management</span></div>', '']);
                        }
                        //oTable.fnAddData([data.d[i].Menu_Code, '<div><span class="SubMenu">' + data.d[i].Menu_Name + '</span></div>',
                        //   '<div class="center"><input onclick="setcanview(this,' + i.toString() + ')" type="checkbox" ' + ((data.d[i].CanView == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanView == '2') ? 'disabled=true' : '') + ' /></div>']);


                        var CheckBoxId = 'chk_Menu' + data.d[i].Menu_Code.replace(" ", "");
                        oTable.fnAddData([data.d[i].Menu_Code, '<div><span class="SubMenu">' + data.d[i].Menu_Name + '</span></div>',
                           '<div class="center"><input id="' + CheckBoxId + '" value="' + i.toString() + '" type="checkbox" ' + ((data.d[i].CanView == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanView == '2') ? 'disabled=true' : '') + ' /></div>']);

                        $('#' + CheckBoxId).on('click', function (e) {
                            var strJson = $('#MainContent_jsonresult').val();
                            var jsonObj = new Array();
                            if (strJson.length > 0) {
                                jsonObj = $.parseJSON(strJson);
                            }
                            jsonObj.d[e.target.value].CanView = (e.target.checked) ? '1' : '0';
                            var json = JSON.stringify(jsonObj);
                            $('#MainContent_jsonresult').val(json);
                        });

                    }

                    var json = JSON.stringify(data);

                    $('#MainContent_jsonresult').val(json);

                    $("#dvresult").dialog({
                        resizable: false,
                        height: 650,
                        width: 1000,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit Privilege UserGroup',
                        buttons: {
                            "Save": function () {
                                var strJson = $('#MainContent_jsonresult').val();
                                var jsonObj = new Array();
                                if (strJson.length > 0) {
                                    jsonObj = jQuery.parseJSON(strJson);
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/SaveUserGroupRole',
                                    type: "POST",
                                    dataType: "json",
                                    cache: false,
                                    data: JSON.stringify({ menuprivilege: jsonObj.d, code: UserGroupCode }),
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        if (data.d == '') {
                                            alert('Save Success');
                                        }
                                        else {
                                            msg = data.d;
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
        var oTable;
        $(document).ready(function () {

            oTable = $('#searchResult').dataTable({
                "bSort": false,
                "bFilter": false,
                "bLengthChange": false,
                "bPaginate": false,
                "ordering": false,
                "binfo": false,
                "sDom": 't',
                "columns": [
                    { "visible": false },
                    null,
                    null
                ]
            });

        });
        </script>
</asp:Content>
