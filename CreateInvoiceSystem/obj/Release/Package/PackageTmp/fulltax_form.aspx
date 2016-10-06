<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fulltax_form.aspx.cs" Inherits="CreateInvoiceSystem.fulltax_form" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Height="1000" Style="margin: 0px auto;
                    width: auto; height: auto;"  ShowBackButton="true" >
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
