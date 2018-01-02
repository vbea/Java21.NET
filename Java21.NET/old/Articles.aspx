<%@ Page Title="" Language="C#" MasterPageFile="~/Java.Master" AutoEventWireup="true" CodeBehind="Articles.aspx.cs" Inherits="Java21.NET.old.Articles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        <asp:TextBox ID="txtArticle" runat="server" style="height:400px;" Width="100%" TextMode="MultiLine"></asp:TextBox>
        <table style="width: 100%; margin-top: 5px;">
            <tr>
                <td style="width: 60%; text-align: left;">
                    <asp:Label ID="labRemark" runat="server" style="line-height:10px; display:block;"></asp:Label></td>
                <td style="width: 20%; text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="btn btn-info" Width="100px" OnClick="btnSave_Click" Enabled="False" /></td>
            </tr>
        </table>
    </div>
    
</asp:Content>
