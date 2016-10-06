<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="usermanage.aspx.cs" Inherits="CreateInvoiceSystem.usermanage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="5" >User Master</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Code</td>
            <td ><asp:TextBox ID="tcode" runat="server" Width="200px" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Name</td>
            <td ><asp:TextBox ID="tname" runat="server" Width="200px" CssClass="txtFormCell"></asp:TextBox></td>
            <td style="width: 100%" colspan="2" ></td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >Group</td>
            <td ><asp:DropDownList ID="dlgrp" runat="server" CssClass="txtFormCell"></asp:DropDownList></td>
            <td class="searchCriterialabel" nowrap >Staion</td>
            <td ><asp:DropDownList ID="dllStation" runat="server" CssClass="txtFormCell"></asp:DropDownList></td>
            <td style="width: 100%; vertical-align:middle; padding-left: 5px; padding-top: 3px" ><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" OnClick="ImageButton2_Click" /></td>
        </tr>
        <tr>
            <td style=" text-align: center; padding: 10px;" colspan="5"  >
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align:top;"><img id="addbtn" src="Images/add.png" width="20px" /></td>
                        <td >
                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="UserID" OnRowDeleting="OnRowDeleting" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px" AllowPaging="True" EnablePersistedSelection="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <asp:TemplateField HeaderText="No.">   
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>   
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Code" DataField="UserCode" HeaderStyle-Width="100px" />
                                <asp:BoundField HeaderText="Username" DataField="Username" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderText="Name">   
                                    <ItemTemplate>
                                        <a href="javascript:editmode('<%# Eval("UserID") %>');"><%# Eval("Name") %></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Group" DataField="UserGroupName" HeaderStyle-Width="100px" />
                                <asp:BoundField HeaderText="Station" DataField="Station_Name" HeaderStyle-Width="200px" />
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" HeaderStyle-Width="30px" DeleteImageUrl="Images/meanicons_24-20.png">
                                    <ControlStyle BorderStyle="Solid" BorderWidth="0px" />
                                </asp:CommandField>
                                <asp:TemplateField >   
                                    <ItemTemplate>
                                        <a href="javascript:editprivil('<%# Eval("UserCode") %>','<%# Eval("UserGroupCode") %>');"><img src="Images/Edituser.png"></img></a>       
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
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
    <div id="dialog-confirm" title="Create User">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>User Code<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="usercode" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>User Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="username" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Password<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="password1" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Confirm Password<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="password2" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>First Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="firstname" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Last Name<span style="color:red ">*</span></td>
                <td style="width: 60%"><asp:TextBox ID="lastname" runat="server" Width="200px" CssClass="txtcalendar"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Group<span style="color:red ">*</span></td>
                <td style="width: 60%">
                    <asp:DropDownList ID="dlUserGroup" runat="server" Width="210px" CssClass="txtcalendar">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Station<span style="color:red ">*</span></td>
                <td style="width: 60%">
                    <asp:DropDownList ID="dlstn" runat="server" Width="210px" CssClass="txtcalendar">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>POS</td>
                <td style="width: 60%">
                    <asp:DropDownList ID="dlposmac" runat="server" Width="210px" CssClass="txtcalendar">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Default Menu<span style="color:red ">*</span></td>
                <td style="width: 60%">
                    <asp:DropDownList ID="dldefmenu" runat="server" Width="210px" CssClass="txtcalendar">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%;" nowrap>Active</td>
                <td style="width: 60%"><asp:CheckBox ID="CheckBox1" runat="server" /></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; width: 40%; height: 35px;" nowrap>User AD</td>
                <td style="width: 60%"><asp:CheckBox ID="chk_UserAD" runat="server" ClientIDMode="Static" /></td>
            </tr>
        </table>
    </div>

    <div id="dvresult" style="display: none">
        <table id="searchResult" style="width:100%;margin-left:auto;margin-right:auto; border: 1px solid; font-family: Tahoma; font-size: 12px;">
            <thead>
                <tr style="height:20px;text-align: center;background-color: gainsboro">
                    <th nowrap style="padding: 5px; font-size: 12px; width: 0%; ">Code</th>
                    <th nowrap style="padding: 5px; font-size: 12px; width: 95%; ">Name</th>
                    <th nowrap style="padding: 5px; font-size: 12px; width: 5%;text-align:center; ">View</th>
                </tr>
            </thead>
        </table>
    </div>
    <asp:HiddenField ID="jsonresult" runat="server" Value="" />
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <style>
        input[type="text"]:disabled {
            background: #f3f3f3;
        }
        select:disabled.txtcalendar {
            background: #f3f3f3;
        }
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
    <script type="text/javascript">
        $(function () {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-dvresult").hide();
            $("#dialog-confirm").hide();
            $("#addbtn").click(function () {
                ClearDialog();
                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 660,
                    width: 600,
                    modal: true,
                    title: 'Create User',
                    open: function (event, ui) {
                        
                        $('.ui-dialog').css('z-index', 103);
                        $('.ui-widget-overlay').css('z-index', 102);
                    },
                    buttons: {
                        "Save": function () {
                            if ($('#MainContent_usercode').val() == '') {
                                alert('กรุณาระบุ usercode');
                                return false;
                            }
                            if ($('#MainContent_username').val() == '') {
                                alert('กรุณาระบุ username');
                                return false;
                            }
                            if ($('#MainContent_password1').val() == '') {
                                alert('กรุณาระบุ รหัสผ่าน');
                                return false;
                            }
                            if ($('#MainContent_password1').val() != $('#MainContent_password2').val()) {
                                alert('กรุณาระบุยืนยันรหัสผ่านให้ถูกต้อง');
                                return false;
                            }
                            if ($('#MainContent_firstname').val() == '') {
                                alert('กรุณาระบุ ชื่อ');
                                return false;
                            }
                            if ($('#MainContent_lastname').val() == '') {
                                alert('กรุณาระบุ นามสกุล');
                                return false;
                            }
                            if ($('#MainContent_dlUserGroup').val() == '') {
                                alert('กรุณาระบุ user group');
                                return false;
                            }
                            if ($('#MainContent_dlstn').val() == '') {
                                alert('กรุณาระบุ Station');
                                return false;
                            }

                            if ($('#MainContent_dldefmenu').val() == '') {
                                alert('กรุณาระบุ Menu');
                                return false;
                            }
                            var msg = '';
                            $.ajax({
                                url: 'servicepos.asmx/SaveUser',
                                type: "POST",
                                dataType: "xml",
                                cache: false,
                                async: false,
                                data: {
                                    'usercode': $('#MainContent_usercode').val()
                                    , 'username': $('#MainContent_username').val()
                                    , 'password': $('#MainContent_password1').val()
                                    , 'password1': $('#MainContent_password2').val()
                                    , 'firstname': $('#MainContent_firstname').val()
                                    , 'lastname': $('#MainContent_lastname').val()
                                    , 'group': $('#MainContent_dlUserGroup').val()
                                    , 'station': $('#MainContent_dlstn').val()
                                    , 'pos': $('#MainContent_dlposmac').val()
                                    , 'menu': $('#MainContent_dldefmenu').val()
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

        function ClearDialog() {
            $('#MainContent_Button1').css('display', 'none');
            $("#dialog-dvresult").hide();
            $("#dialog-confirm").hide();
            $('#MainContent_usercode').val('');
            $('#MainContent_username').val('');
            $('#MainContent_password1').val('');
            $('#MainContent_password2').val('');
            $('#MainContent_firstname').val('');
            $('#MainContent_lastname').val('');
            $('#MainContent_dlUserGroup').val('');
            $('#MainContent_dlstn').val('');
            $('#MainContent_dlposmac').val('');
            $('#MainContent_dldefmenu').val('');
            $('#MainContent_CheckBox1')[0].checked = false;
            $('#MainContent_usercode').attr("disabled", false);
            $('#MainContent_username').attr("disabled", false);
            $('#MainContent_firstname').attr("disabled", false);
            $('#MainContent_lastname').attr("disabled", false);
            $('#MainContent_dlUserGroup').attr("disabled", false);
            $('#MainContent_CheckBox1').attr("disabled", false);
            $('#chk_UserAD').attr("disabled", true);

            $('#chk_UserAD').parent().parent().hide();
            $('#MainContent_password1').parent().parent().show();
            $('#MainContent_password2').parent().parent().show();
        }

        function editmode(id) {
            ClearDialog();

            $('#MainContent_usercode').attr("disabled", true);
            $('#MainContent_username').attr("disabled", true);
            $('#chk_UserAD').parent().parent().show();
            $.ajax({
                url: 'servicepos.asmx/GetUser',
                type: "POST",
                dataType: "xml",
                cache: false,
                data: { 'id': id },
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);

                    $('#MainContent_usercode').val(obj[0].UserCode);
                    $('#MainContent_username').val(obj[0].Username);
                    $('#MainContent_password1').val(obj[0].Password);
                    $('#MainContent_password2').val(obj[0].Password);
                    $('#MainContent_firstname').val(obj[0].FirstName);
                    $('#MainContent_lastname').val(obj[0].LastName);
                    $('#MainContent_dlUserGroup').val(obj[0].UserGroup);
                    getmenu(obj[0].UserGroup);
                    $('#MainContent_dlstn').val(obj[0].Station_Code);
                    getpos(obj[0].Station_Code);
                    $('#MainContent_dlposmac').val(obj[0].PID);
                    $('#MainContent_dldefmenu').val(obj[0].DefaultMenu);
                    $('#MainContent_CheckBox1')[0].checked = (obj[0].Active == 'Y') ? true : false;

                    if (obj[0].IsUserAD) {
                        $('#MainContent_firstname').attr("disabled", true);
                        $('#MainContent_lastname').attr("disabled", true);
                        $('#MainContent_dlUserGroup').attr("disabled", true);
                        $('#MainContent_CheckBox1').attr("disabled", true);
                        $('#chk_UserAD')[0].checked = true;

                        $('#MainContent_password1').parent().parent().hide();
                        $('#MainContent_password2').parent().parent().hide();
                    }
                    else {
                        $('#chk_UserAD')[0].checked = false;
                    }

                    $("#dialog-confirm").dialog({
                        resizable: false,
                        height: 660,
                        width: 600,
                        modal: true,
                        open: function (event, ui) {
                            $('.ui-dialog').css('z-index', 103);
                            $('.ui-widget-overlay').css('z-index', 102);
                        },
                        title: 'Edit User',
                        buttons: {
                            "Save": function () {
                                if ($('#MainContent_usercode').val() == '') {
                                    alert('กรุณาระบุ usercode');
                                    return false;
                                }
                                if ($('#MainContent_username').val() == '') {
                                    alert('กรุณาระบุ username');
                                    return false;
                                }
                                if ($('#chk_UserAD')[0].checked == false) {
                                    if ($('#MainContent_password1').val() == '') {
                                        alert('กรุณาระบุ รหัสผ่าน');
                                        return false;
                                    }
                                    if ($('#MainContent_password1').val() != $('#MainContent_password2').val()) {
                                        alert('กรุณาระบุยืนยันรหัสผ่านให้ถูกต้อง');
                                        return false;
                                    }
                                }
                                if ($('#MainContent_firstname').val() == '') {
                                    alert('กรุณาระบุ ชื่อ');
                                    return false;
                                }
                                if ($('#MainContent_lastname').val() == '') {
                                    alert('กรุณาระบุ นามสกุล');
                                    return false;
                                }
                                if ($('#MainContent_dlUserGroup').val() == '') {
                                    alert('กรุณาระบุ user group');
                                    return false;
                                }
                                if ($('#MainContent_dlstn').val() == '') {
                                    alert('กรุณาระบุ Station');
                                    return false;
                                }

                                if ($('#MainContent_dldefmenu').val() == '') {
                                    alert('กรุณาระบุ Menu');
                                    return false;
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/EditUser',
                                    type: "POST",
                                    dataType: "xml",
                                    cache: false,
                                    data: { 
                                        'usercode': $('#MainContent_usercode').val()
                                        , 'username': $('#MainContent_username').val()
                                        , 'password': $('#MainContent_password1').val()
                                        , 'password1': $('#MainContent_password2').val()
                                        , 'firstname': $('#MainContent_firstname').val()
                                        , 'lastname': $('#MainContent_lastname').val()
                                        , 'group': $('#MainContent_dlUserGroup').val()
                                        , 'station': $('#MainContent_dlstn').val()
                                        , 'pos': $('#MainContent_dlposmac').val()
                                        , 'menu': $('#MainContent_dldefmenu').val()
                                        , 'active': $('#MainContent_CheckBox1')[0].checked
                                        , 'id': id
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

        $('#MainContent_dlstn').change(function () {
            var id = $('#MainContent_dlstn').val();
            if (id != '') {
                getpos(id);
            }
        });

        getpos = function (key) {
            $.ajax({
                type: 'POST',
                url: 'servicepos.asmx/GetPOSByStn',
                async: false,
                data: { 'stn': key },
                dataType: 'xml',
                cache: false,
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $("#MainContent_dlposmac").empty().append('<option selected="selected" value=""></option>');
                    $.each(obj, function (key, value) {
                        $("#MainContent_dlposmac").append($("<option></option>").val
                        (value.POS_Code).html(value.POS_MacNo));
                    });
                }
            });
        };

        $('#MainContent_dlUserGroup').change(function () {
            var id = $('#MainContent_dlUserGroup').val();
            if (id != '') {
                getmenu(id);
            }
        });

        getmenu = function (key) {
            $.ajax({
                type: 'POST',
                url: 'servicepos.asmx/GetMenuPrivilegeAddUser',
                data: { 'UserGroupCode': key },
                dataType: 'xml',
                cache: false,
                success: function (data) {
                    var content = data.childNodes['0'].childNodes['0'].nodeValue;
                    var obj = $.parseJSON(content);
                    $("#MainContent_dldefmenu").empty();
                    $.each(obj, function (key, value) {
                        $("#MainContent_dldefmenu").append($("<option></option>").val
                        (value.Menu_Code).html(value.Menu_Name));
                    });
                }
            });
        };

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

        function editprivil(usercode, UserGroupCode) {
            $("#dvresult").hide();
            $.ajax({
                url: 'servicepos.asmx/GetMenuPrivilegeUser',
                type: "POST",
                dataType: "JSON",
                cache: false,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ UserGroupCode: UserGroupCode, usercode: usercode }),
                success: function (data) {
                    oTable.fnClearTable();

                    for (var i = 0; i < data.d.length; i++) {
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
                        oTable.fnAddData([data.d[i].Menu_Code, '<div><span class="SubMenu">' + data.d[i].Menu_Name + '</span></div>',
                           '<div class="center"><input onclick="setcanview(this,' + i.toString() + ')" type="checkbox" ' + ((data.d[i].CanView == '1') ? 'checked=true' : '') + ' ' + ((data.d[i].CanView == '2') ? 'disabled=true' : '') + ' /></div>']);
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
                        title: 'Edit Privilege User',
                        buttons: {
                            "Save": function () {
                                var strJson = $('#MainContent_jsonresult').val();
                                var jsonObj = new Array();
                                if (strJson.length > 0) {
                                    jsonObj = jQuery.parseJSON(strJson);
                                }
                                var msg = '';
                                $.ajax({
                                    url: 'servicepos.asmx/SaveUserGroupRoleUser',
                                    type: "POST",
                                    dataType: "json",
                                    cache: false,
                                    data: JSON.stringify({ menuprivilege: jsonObj.d, code: UserGroupCode, usercode: usercode }),
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
