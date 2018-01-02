﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Java.Master" AutoEventWireup="true" CodeBehind="Knowledges.aspx.cs" Inherits="Java21.NET.Knowledges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/umeditor.css" rel="stylesheet" />
    <script src="/Script/jquery.min.js"></script>
    <script src="/Script/umeditor.config.js"></script>
    <script src="/Script/umeditor.min.js"></script>
    <script src="/Script/lang/zh-cn/zh-cn.js"></script>
    <script src="/Script/mathquill/mathquill.min.js"></script>
    <link href="/Script/mathquill/mathquill.css" rel="stylesheet" />
    <title>编辑内容</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; padding-top: 10px; margin: 0px auto;">
        <a href="Knowledge.aspx">Java知识点</a> -
        <asp:Label ID="labTitle" runat="server"></asp:Label><br />
        <div style="width: 80%; vertical-align: middle; padding-top: 5px;">
            <span>分类：</span><asp:DropDownList ID="ddlCategory" runat="server" Width="20%"></asp:DropDownList>
            <span>标题：</span><asp:TextBox ID="txtTitle" runat="server" Width="60%" required="required"></asp:TextBox>
        </div>
        <script type="text/plain" id="javaEditor" style="width: 100%; height: 450px;"><%= content %></script>
        <table style="width: 100%; margin-top: 5px;">
            <tr>
                <td style="width: 60%; text-align: left;">
                    <asp:Label ID="labRemark" runat="server" style="line-height:10px; display:block;"></asp:Label></td>
                <td style="width: 20%; text-align: right;">
                    <asp:HyperLink ID="linkAdvance" runat="server">高级</asp:HyperLink>&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="btn btn-info" Width="100px" OnClick="btnSave_Click" Enabled="False" /></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        //实例化编辑器
        var um = UM.getEditor('javaEditor');
    </script>
</asp:Content>
