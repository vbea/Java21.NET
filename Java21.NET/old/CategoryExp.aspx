<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryExp.aspx.cs" Inherits="Java21.NET.old.CategoryExp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>添加分类</title>
    <link href="/css/java.css" rel="stylesheet" />
    <link href="/css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        分类名称：
        <asp:TextBox ID="txtName" runat="server" required="required" Width="82%"></asp:TextBox><br />
        分类介绍：<br />
        <asp:TextBox ID="txtValues" runat="server" Height="100px" TextMode="MultiLine" Width="92%"></asp:TextBox><br />
        <br /><asp:Button ID="btnAdd" runat="server" Text="添 加" CssClass="btn btn-info" OnClick="btnAdd_Click" Visible="False"/>
        <asp:Button ID="btnEdit" runat="server" Text="修 改" CssClass="btn btn-info" OnClick="btnEdit_Click" Visible="False"/>
    </div>
    </form>
</body>
</html>
