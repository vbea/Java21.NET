<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JavaVideo.aspx.cs" Inherits="Java21.NET.JavaVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <title>21天学通Java音节码转换程序</title>
    <link href="/css/bootstrap_2.css" rel="stylesheet" />
    <link href="/css/home.css" rel="stylesheet" />
    <link href="/css/header.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div id="javaVideo" runat="server" style="height: 20px; text-align: center; padding: 0px; margin: 0px auto;">21天学通Java音节码转换程序</div>
            <div class="container">
                <asp:TextBox ID="txtPiano" runat="server" TextMode="MultiLine" Height="200px" CssClass="txtAudio" required="required"></asp:TextBox>
                <asp:TextBox ID="txtValue" runat="server" TextMode="MultiLine" Height="200px" CssClass="txtAudio"></asp:TextBox>
                <br />
                <asp:Button ID="btnEncode" runat="server" Text="转换为代码" OnClick="btnEncode_Click" Width="100px" CssClass="btn btn-info css_right" />
                <asp:Button ID="btnEycode" runat="server" Text="转换为字母" OnClick="btnEycode_Click" Width="100px" CssClass="btn btn-info css_right" />
                <input type="reset" value="重置" class="btn btn-info css_right" style="width: 60px;" />
                <asp:Button ID="btnNewAlphabet" runat="server" Text="转换为新版字母" OnClick="btnNewAlphabet_Click" Width="150px" CssClass="btn btn-info css_right" />
                <asp:Button ID="btnShowCode" runat="server" OnClick="btnShowCode_Click" Text="查看代码" Width="100px" CssClass="btn-info btn css_right" />
                <asp:Button ID="btnClear" runat="server" Text="清除内容" OnClick="btnClear_Click" Width="100px" CssClass="btn btn-info css_right" UseSubmitBehavior="False" />
            </div>
            <div class="container small">
                <p>转换说明：</p>
                新版字母转换支持扩展音节(如：A1、B10、C20等)，在转换之前需要用中括号“[]”括起来，遇到和弦音需要用小括号“()”括起来，遇到停顿就用减号“-”。老版音节码需要先进行逆转换为字母再转为新版字母码。<br />
                PS:当遇到快音节时，需要用小写字母表示，如a、b、c、c1、a11，停顿符号变为“_”。
            </div>
            <hr />
            <div runat="server" id="audioCode" class="container small"></div>
        </div>
    </form>
</body>
</html>
