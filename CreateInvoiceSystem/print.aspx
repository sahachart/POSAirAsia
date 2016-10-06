<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print.aspx.cs" Inherits="CreateInvoiceSystem.print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input id="PRINT" type="button" value="Print" onclick="javascript: location.reload(true); window.print();" />
    </div>
    </form>
</body>
</html>
