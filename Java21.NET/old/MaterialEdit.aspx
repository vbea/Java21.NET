<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialEdit.aspx.cs" Inherits="Java21.NET.old.MaterialEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>资料修改</title>
    <link href="/css/java.css" rel="stylesheet" />
    <link href="/css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form" runat="server">
    <div>
        标题：
        <asp:TextBox ID="txtTitle" runat="server" required="required" Width="82%"></asp:TextBox><br />
        地址：
        <asp:TextBox ID="txtUrl" runat="server" Width="82%"></asp:TextBox><br />
        密码：
        <asp:TextBox ID="txtPassword" runat="server" Width="82%"></asp:TextBox><br />
        介绍：<br />
        <asp:TextBox ID="txtRemark" runat="server" Height="100px" TextMode="MultiLine" Width="92%"></asp:TextBox><br />
        <br /><asp:Button ID="btnAdd" runat="server" Text="添 加" CssClass="btn btn-info" OnClick="btnAdd_Click" Visible="False"/>
        <asp:Button ID="btnEdit" runat="server" Text="修 改" CssClass="btn btn-info" OnClick="btnEdit_Click" Visible="False"/>
    </div>
    </form>
</body>
</html>
