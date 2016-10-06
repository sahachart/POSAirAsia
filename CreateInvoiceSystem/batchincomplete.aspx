<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="batchincomplete.aspx.cs" Inherits="CreateInvoiceSystem.batchincomplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="5">Batch Incomplete</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap>PNR No</td>
            <td>
                <asp:TextBox ID="tcode" Width="200px" runat="server" CssClass="txtFormCell"></asp:TextBox>
            </td>
            <td class="searchCriterialabel" nowrap style="width: 10%">Currency Date</td>
            <td style="width: 20%">
                <asp:TextBox ID="dtp_BatchDate" runat="server" CssClass="" Width="140px" ClientIDMode="Static"></asp:TextBox>
            </td>
            <td style="width: 100%; vertical-align: middle; padding-left: 5px; padding-top: 3px">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" OnClick="ImageButton2_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; padding: 10px;" colspan="5">
                <table>
                    <tr>
                        <td>
                            <style>
                                #MainContent_GridView1 th {
                                    text-align: center;
                                }
                            </style>
                            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px" AllowPaging="True"
                                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="PNR No" DataField="PNR_No" HeaderStyle-Width="200px" />
                                    <asp:BoundField HeaderText="Msg" DataField="Msg" HeaderStyle-Width="200px" />
                                    <asp:BoundField HeaderText="User" DataField="Create_By" HeaderStyle-Width="200px" />
                                    <asp:BoundField HeaderText="Date" DataField="Create_Date" HeaderStyle-Width="200px" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                </Columns>
                                <FooterStyle BackColor="#56575B" />
                                <HeaderStyle BackColor="#56575B" Font-Bold="True" ForeColor="White" Height="30px" />
                                <PagerStyle BackColor="#56575B" ForeColor="White" HorizontalAlign="Right" CssClass="GridPager" />
                                <RowStyle BackColor="#F0F0F0" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                            <asp:Label ID="Label1" runat="server" Text="No Result" CssClass="Nodata"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $("#dtp_BatchDate").datepicker({
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: 'Images/calender.png',
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true
        });
    </script>
</asp:Content>
