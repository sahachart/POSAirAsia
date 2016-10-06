<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="billinvoice_form.aspx.cs" Inherits="CreateInvoiceSystem.billinvoice_form" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
*{-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box}*,:after,:before{color:#000!important;text-shadow:none!important;background:0 0!important;-webkit-box-shadow:none!important;box-shadow:none!important}.h2,h2{font-size:30px}.h1,.h2,.h3,h1,h2,h3{margin-top:20px;margin-bottom:10px}.h1,.h2,.h3,.h4,.h5,.h6,h1,h2,h3,h4,h5,h6{font-family:inherit;font-weight:500;line-height:1.1;color:inherit}h2,h3{page-break-after:avoid}h2,h3,p{orphans:3;widows:3}p{margin:0 0 10px}button,input,select,textarea{font-family:inherit;font-size:inherit;line-height:inherit}input{line-height:normal}button,input,optgroup,select,textarea{margin:0;font:inherit;color:inherit}button,select{text-transform:none}a{color:#337ab7;text-decoration:none}a,a:visited{text-decoration:underline}a{background-color:transparent}</style>
</head>
<body>
     <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%" ZoomMode="PageWidth" ShowBackButton="true" SizeToReportContent="True">
        </rsweb:ReportViewer>
       
    </form>
</body>
</html>
