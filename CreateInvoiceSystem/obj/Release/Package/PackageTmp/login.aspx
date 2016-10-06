<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="CreateInvoiceSystem.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        
.bgimg {
    width: auto;
    height: auto;
    background-image: url('images/bg.png');
}
        .auto-style1 {
            width: 481px;
            height: 420px;
        }
        .auto-style2 {
            height: 420px;
            vertical-align: bottom;
        }
        .auto-style3 {
            height: 420px;
            vertical-align: bottom;
            width: 235px;
        }
        .auto-style4 {
            width: 235px;
            vertical-align: top;
            font-family: Tahoma; font-size: 14px; color: #808080;
            text-align: center;
        }
        .auto-style5 {
            width: 481px;
        }
        .auto-style6 {
            width: 481px;
            height: 124px;
        }
        .auto-style7 {
            width: 235px;
            vertical-align: top;
            height: 124px;
        }
        .auto-style8 {
            height: 124px;
        }
        .auto-style9 {
            width: 481px;
            height: 69px;
        }
        .auto-style10 {
            width: 235px;
            vertical-align: top;
            height: 69px;
            text-align: center;
        }
        .auto-style11 {
            height: 69px;
        }
        .form-control {
          position: relative;
          height: auto;
          -webkit-box-sizing: border-box;
             -moz-box-sizing: border-box;
                  box-sizing: border-box;
          padding: 10px;
          font-size: 16px;
          width: 100%
        }
        .validation-summary-errors {
    color: #e80c4d;
    font-weight: bold;
    font-family: Tahoma;
    font-size: 12px;flex-align: center;
}
    </style>
</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" >
    <form id="form1" runat="server">
        <table id="Table_01" width="1153" height="865" border="0" cellpadding="0" cellspacing="0" style="background-image: url('Images/bg.png'); background-repeat: no-repeat">
	        <tr>
                <td class="auto-style1"></td>
                <td class="auto-style3" style="font-family: Tahoma; font-size: 32px; color: #424041; font-weight: bold;padding-left:1px;">
                    LOGIN
	            </td>
                <td class="auto-style2"></td>
	        </tr>
            <tr>
                <td class="auto-style6"></td>
                <td class="auto-style7">
                    <table style="margin:0px; width:100%">
                        <tr >
                            <td style="padding-bottom: 8px;" colspan="2">
                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="Username" required="" autofocus=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="Password" runat="server" CssClass="form-control" placeholder="Password" required="" TextMode="Password" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tr1" runat="server" visible="false">
                            <td style="width:30%" nowrap><label style=" font-family: Tahoma; font-size: small;">POS Machine:</label></td><td><asp:DropDownList width="100%" AutoPostBack="true" runat="server" ID="cmbDropdawnList" OnSelectedIndexChanged="cmbDropdawnList_SelectedIndexChanged" ></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:label runat="server" ID="FailureText" CssClass="validation-summary-errors"></asp:label></td>
                        </tr>
                    </table>
	            </td>
                <td class="auto-style8"></td>
	        </tr>
            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style10">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/main_16.jpg" OnClick="ImageButton1_Click1" />&nbsp;&nbsp;
                    <img alt="" src="Images/main_18.jpg" style="cursor: pointer;" onclick="location.href = 'login.aspx';" />
                </td>
                <td class="auto-style11"></td>
	        </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4" >
	                <span style=" text-align: center; vertical-align: middle; font-family: Tahoma; font-size: small;">© 2016 AirAsia Version : <asp:Label ID="Label1" runat="server" Font-Names="Tahoma" Font-Size="Small"></asp:Label></span>
                </td>
                <td></td>
	        </tr>
        </table>
    </form>
</body>
</html>
