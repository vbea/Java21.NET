﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JavaVideo.aspx.cs" Inherits="Java21.NET.JavaVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <title>音节码转换</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="javaVideo" runat="server" style="width:480px; height:20px; text-align:center; padding:0px; margin:0px auto;"></div><br />
        <asp:TextBox ID="txtPiano" runat="server" TextMode="MultiLine" Height="200px" Width="30%"></asp:TextBox>
        <asp:TextBox ID="txtValue" runat="server" TextMode="MultiLine" Height="200px" Width="30%"></asp:TextBox>
        <br />
        <asp:Button ID="btnEncode" runat="server" Text="转换为代码" OnClick="btnEncode_Click" />
        <asp:Button ID="btnEycode" runat="server" Text="转换为字母" OnClick="btnEycode_Click" />
        <asp:Button ID="btnBass" runat="server" OnClick="btnBass_Click" Text="降八度" />
        <asp:Button ID="btnUpper" runat="server" Text="转换为大写字母" OnClick="btnUpper_Click" />
        <asp:Button ID="btnFixComma" runat="server" OnClick="btnFixComma_Click" Text="修复双键逗号" />
    </form>
</body>
</html>
