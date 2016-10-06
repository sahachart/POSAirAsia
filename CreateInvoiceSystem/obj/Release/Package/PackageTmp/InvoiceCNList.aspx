<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoiceCNList.aspx.cs" Inherits="CreateInvoiceSystem.InvoiceCNList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
        <link href="Content/Custom/CIS-Custom.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/Bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id^=MainContent_tbx_date]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'Images/calender.png',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container" style="padding-top: 20px;">
        <div class="panel panel-default">
            <div class="panel-heading">CN Invoice Criteria Search</div>


            <table class="searchCriteria">

                <tr>
                    <td class="tableform" nowrap>Booking No.</td>
                    <td class="tableform" style="width: 30%; text-align: left;">
                        <asp:TextBox ID="tbx_booking_no" runat="server" CssClass=""></asp:TextBox></td>
                    <td class="tableform" nowrap style="width: 125px"></td>
                    <td style="width: 30%">&nbsp;</td>
                    <td style="width: 100%; vertical-align: middle; padding-left: 5px; padding-top: 3px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tableform" nowrap>Booking Date From</td>
                    <td class="tableform" style="width: 30%; text-align: left;">

                        <asp:TextBox ID="tbx_date_from" runat="server" CssClass="" Width="140px"></asp:TextBox>

                    </td>
                    <td class="tableform"  style="text-align: center; width: 125px">To</td>
                    <td class="tableform" style="width: 30%;" >
                        <asp:TextBox ID="tbx_date_to" runat="server"  CssClass="" Width="140px"></asp:TextBox></td>
                    <td class="tableform" style="width: 100%; vertical-align: middle; padding-left: 5px; padding-top: 3px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tableform" >Passenger Name</td>
                    <td class="tableform" style="width: 30%" >
                        <asp:TextBox ID="tbx_passenger" runat="server" CssClass="" Width="100%"></asp:TextBox></td>
                    <td class="tableform"  style="width: 125px">
                        Status

                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="200">
                            <asp:ListItem Value ="All" Text="All"/>
                            <asp:ListItem Value ="Full" Text="Full Tax"/>
                            <asp:ListItem Value ="CN" Text="CN"/>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 100%; vertical-align: middle; padding-top: 3px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center; padding: 10px;" colspan="5">
                        
                            <asp:Button ID="btn_Search" runat="server" Text="Search" OnClick="btn_Search_Click" ClientIDMode="Static" CssClass="btn btn-primary"  />
                            <asp:Button ID="btn_Clear" runat="server" Text="Clear" OnClick="btn_Clear_Click" ClientIDMode="Static" CssClass="btn btn-default" style="margin-left: 15px;" />
                        


                    </td>
                </tr>
            </table>




        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <style>
                        #MainContent_GridView1 {
                            width: 100%;
                        }
                        #MainContent_GridView1 th{
                            text-align: center;
                        }
                    </style>
                    <asp:GridView ID="GridView1" runat="server"
                        ShowHeaderWhenEmpty="True"
                        DataKeyNames="TaxInvoiceNo"
                        AutoGenerateColumns="False"
                        OnRowDataBound="GridView1_RowDataBound"
                        BackColor="White"
                        BorderColor="#DEDFDE"
                        BorderStyle="None"
                        BorderWidth="1px"
                        CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        Font-Names="Tahoma" Font-Size="12px" AllowPaging="True"
                        EnablePersistedSelection="True"
                        OnPageIndexChanging="GridView1_PageIndexChanging"
                        ViewStateMode="Enabled" HeaderStyle-HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="No." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                                        <asp:HyperLinkField DataTextField="INV_No" HeaderText="Invoice No" DataNavigateUrlFormatString="InvoiceCN_Info.aspx?INV_No={0}&Booking={1}" DataNavigateUrlFields="INV_No,PNR_No" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="90px" />
                            </asp:HyperLinkField>
                            <asp:BoundField HeaderText="Booking No" DataField="PNR_No" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Booking_Date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Booking Date" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TaxInvoiceNo" HeaderText="ABB No." ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Customer Name" DataField="CustName">
                                <%--<HeaderStyle Width="150px" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Branch" DataField="Branch" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Create_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Create Date" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Create_by" HeaderText="Create By" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="80px" />
                             </asp:BoundField>
                            <asp:BoundField DataField="Update_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Update Date" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="80px" />
                             </asp:BoundField>
                            <asp:BoundField DataField="Update_by" HeaderText="Update By" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="80px" />
                             </asp:BoundField>
                            <asp:CommandField HeaderStyle-HorizontalAlign="Center" HeaderText="Print" ShowCancelButton="False" ShowEditButton="True" ButtonType="Image" EditImageUrl="~/Images/Print-icon.png" EditText="">

                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />

                            </asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="#56575B" />
                        <HeaderStyle BackColor="#56575B" Font-Bold="True" ForeColor="White" Height="30px" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#56575B" ForeColor="White" HorizontalAlign="Right" CssClass="GridPager" />
                        <RowStyle BackColor="#F0F0F0" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>

                    <asp:Label ID="Label1" runat="server" Text="No Result" CssClass="Nodata"></asp:Label>
                </ContentTemplate>
<%--                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Search" EventName="Click" />
                </Triggers>--%>
            </asp:UpdatePanel>

        </div>
    </div>



    <script type="text/javascript">


        $(document).ready(function () {
            OpenPrint = function (key) {
                //window.open("billinvoice.aspx?mode=edit&key=" + key, '');
                var urltext = "CreditNote_Form.aspx?inv_no=" + key;
                openPopupPreview(urltext, 'Invoice', 900, 500);
            };
        });
    </script>
</asp:Content>
