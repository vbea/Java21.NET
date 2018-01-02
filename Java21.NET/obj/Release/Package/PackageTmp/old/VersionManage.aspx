<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VersionManage.aspx.cs" Inherits="Java21.NET.old.VersionManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>版本下载管理</title>
    <link href="/css/java.css" rel="stylesheet" />
    <link href="/css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    版本名称：<asp:TextBox ID="txtVername" runat="server" required="required"></asp:TextBox><br />
    下载链接：<asp:TextBox ID="txtVerurl" runat="server"></asp:TextBox><br />
        发布时间：<asp:TextBox ID="txtVerdate" runat="server" required="required"></asp:TextBox><br />
        时间格式：
        (yyyy-MM-dd)<br />
        <br /><asp:Button ID="btnAdd" runat="server" Text="添 加" CssClass="btn btn-info" OnClick="btnAdd_Click" Visible="False"/>
        <asp:Button ID="btnEdit" runat="server" Text="修 改" CssClass="btn btn-info" OnClick="btnEdit_Click" Visible="False"/>
    </div>
    </form>
</body>
</html>
