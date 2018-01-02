<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoEdit.aspx.cs" Inherits="Java21.NET.old.VideoEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>视频修改</title>
    <link href="/css/java.css" rel="stylesheet" />
    <link href="/css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        标题：
        <asp:TextBox ID="txtTitle" runat="server" required="required" Width="82%"></asp:TextBox><br />
        <p>flash地址：</p>
        <asp:TextBox ID="txtUrl" runat="server" Width="92%"></asp:TextBox><br />
        <p>frame地址：</p>
        <asp:TextBox ID="txtUrl2" runat="server" Width="92%"></asp:TextBox><br />
        <br /><asp:Button ID="btnAdd" runat="server" Text="添 加" CssClass="btn btn-info" OnClick="btnAdd_Click" Visible="False"/>
        <asp:Button ID="btnEdit" runat="server" Text="修 改" CssClass="btn btn-info" OnClick="btnEdit_Click" Visible="False"/>
    </div>
    </form>
</body>
</html>
