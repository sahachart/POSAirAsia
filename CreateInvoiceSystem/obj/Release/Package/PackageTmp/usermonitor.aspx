<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="usermonitor.aspx.cs" Inherits="CreateInvoiceSystem.usermonitor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="searchCriteria">
        <tr>
            <td class="headlabel" colspan="7" >Monitoring</td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap >User</td>
            <td ><asp:TextBox ID="tcode" Width="200px"  runat="server" CssClass="txtFormCell"></asp:TextBox></td>
            <td class="searchCriterialabel" nowrap>Station</td>
            <td ><asp:DropDownList ID="dlstation" runat="server" Width="200px"  CssClass="txtFormCell"></asp:DropDownList></td>
            <td class="searchCriterialabel" nowrap>Group</td>
            <td ><asp:DropDownList ID="dlgroup" runat="server" Width="200px"  CssClass="txtFormCell"></asp:DropDownList></td>
            <td style="width: 100%; vertical-align:middle; padding-left: 5px; padding-top: 3px" ><asp:ImageButton ID="ImageButton1" 
                runat="server" ImageUrl="~/Images/Search-icon.png" Height="24px" Width="24px" OnClick="ImageButton1_Click" />&nbsp;
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/reset.png" Height="24px" Width="24px" 
                    OnClick="ImageButton2_Click" /></td>
        </tr>
        <tr>
            <td class="searchCriterialabel" nowrap  >Lock System</td>
            <td colspan="6"><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" /></td>
        </tr>
        <tr>
            <td style=" text-align: center; padding: 10px;" colspan="7"  > 
                <table>
                    <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" 
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Vertical" Font-Names="Tahoma" Font-Size="12px" AllowPaging="True" 
                            OnPageIndexChanging="GridView1_PageIndexChanging">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="UserName" DataField="Username" HeaderStyle-Width="200px" />
                                <asp:BoundField HeaderText="Time" DataField="TimeStamp" HeaderStyle-Width="200px" />
                                    <asp:BoundField HeaderText="Station" DataField="Station_Name" HeaderStyle-Width="200px" />
                                    <asp:BoundField HeaderText="User Group" DataField="UserGroupName" HeaderStyle-Width="200px" />
                                    <asp:BoundField HeaderText="POS Machine" DataField="POS_MacNo" HeaderStyle-Width="200px" />
                                    <asp:BoundField HeaderText="On Process" DataField="Menu_Name" HeaderStyle-Width="200px" />
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
                    </td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
