<%@ Page Title="" Language="C#" MasterPageFile="~/Java.Master" AutoEventWireup="true" CodeBehind="JavaAudio.aspx.cs" Inherits="Java21.NET.old.JavaAudio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>21天学通Java音节码转换程序</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div id="javaVideo" runat="server" class="container text-center">21天学通Java音节码转换程序</div>
        <div class="container">
            <asp:TextBox ID="txtPiano" runat="server" TextMode="MultiLine" Height="200px" required="required" Width="48%"></asp:TextBox>
            <asp:TextBox ID="txtValue" runat="server" TextMode="MultiLine" Height="200px" Width="48%"></asp:TextBox>
            </div>
        <div class="container">
            <asp:Button ID="btnEncode" runat="server" Text="转换为代码" OnClick="btnEncode_Click" Width="100px" CssClass="btn btn-info css_right" />
            <asp:Button ID="btnEycode" runat="server" Text="转换为字母" OnClick="btnEycode_Click" Width="100px" CssClass="btn btn-info css_right" />
            <asp:Button ID="btnNewAlphabet" runat="server" Text="转换为新版字母" OnClick="btnNewAlphabet_Click" Width="150px" CssClass="btn btn-info css_right" />
            <asp:Button ID="btnShowCode" runat="server" OnClick="btnShowCode_Click" Text="查看代码" Width="100px" CssClass="btn-info btn css_right" />
            <input type="reset" value="重置" class="btn btn-info css_right" style="width: 60px;" />
            <asp:Button ID="btnClear" runat="server" Text="清除内容" OnClick="btnClear_Click" Width="100px" CssClass="btn btn-info css_right" UseSubmitBehavior="False" />
            <asp:Button ID="btnMobile" runat="server" Text="手机版" CssClass="btn btn-info css_right" OnClick="btnMobile_Click" UseSubmitBehavior="False" Width="80px"/>
        </div>
        <div class="small container">
            <p>转换说明：</p>
            新版字母转换支持扩展音节(如：A1、B10、C20等)，在转换之前需要用中括号“[]”括起来，遇到和弦音需要用小括号“()”括起来，遇到停顿就用减号“-”。老版音节码需要先进行逆转换为字母再转为新版字母码。<br />
            PS:当遇到快音节时，需要用小写字母表示，如a、b、c、c1、a11，停顿符号变为“_”。
        </div>
        <hr />
        <div runat="server" id="audioCode" class="container-fluid small"></div>
    </div>
</asp:Content>
