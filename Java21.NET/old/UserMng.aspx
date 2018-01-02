<%@ Page Title="" Language="C#" MasterPageFile="~/Java.Master" AutoEventWireup="true" CodeBehind="UserMng.aspx.cs" Inherits="Java21.NET.UserMng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>用户管理</title>
    <link href="/Script/asyncbox/skins/ZCMS/asyncbox.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/cooke.js"></script>
    <script type="text/javascript" src="/Script/asyncbox/AsyncBox.v1.4.5.js"></script>
    <script type="text/javascript">
        function showEditUser(id) {
            asyncbox.open({
                id: "edit_user",
                url: 'EditUser.aspx',
                width: 400,
                height: 400,
                data: { "id": id },
                title: '修改用户',
            });
        }
        function closeEditDialog() {
            asyncbox.close("edit_user");
            __doPostBack('ctl00$ContentPlaceHolder1$hidAjax','');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; height: 100%; padding: 0px; margin: 0 auto;">
        <p>用户列表</p><asp:LinkButton ID="hidAjax" runat="server" OnClick="hidAjax_Click" Text=""/>
        <asp:UpdatePanel ID="updddd" runat="server">
            <ContentTemplate>
                <div>搜索用户：
                    <asp:TextBox ID="txtSearch" runat="server" style="margin-bottom:0px;"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn btn-info" OnClick="btnSearch_Click"/>
                </div>
                <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvUser_PageIndexChanging" CssClass="grid_b" GridLines="None">
                    <AlternatingRowStyle BackColor="#E7F7FF" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table style="width: 100%; text-align: center;">
                                    <tr>
                                        <td style="width: 2.5%;">头像</td>
                                        <td style="width: 3%;">ID</td>
                                        <td style="width: 9%;">用户名</td>
                                        <td style="width: 8%;">角色</td>
                                        <td style="width: 8%;">昵称</td>
                                        <td style="width: 4%;">性别</td>
                                        <td style="width: 10%;">邮箱</td>
                                        <td style="width: 8%;">QQ</td>
                                        <td style="width: 8%;">手机</td>
                                        <td style="width: 7.5%;">生日</td>
                                        <td style="width: 12%;">地址</td>
                                        <td style="width: 4%;">活动</td>
                                        <td style="width: 8%;">注册日期</td>
                                        <td style="width: 8%;">操作</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table style="width: 100%; text-align: center;  word-break:break-all;">
                                    <tr>
                                        <td style="width:2.5%"><a href="<%# getHead(""+Eval("head")) %>"><img src='<%# getHead(""+Eval("head")) %>' alt="用户头像" style="width:100%;"/></a></td>
                                        <td style="width: 3%;"><%# Eval("id") %></td>
                                        <td style="width: 9%;"><%# Eval("name") %></td>
                                        <td style="width: 8%;"><%# getRoles(Convert.ToInt32(Eval("roles"))) %></td>
                                        <td style="width: 8%;"><%# Eval("nickname") %></td>
                                        <td style="width: 4%;"><%# getGender(Eval("gender")) %></td>
                                        <td style="width: 10%;"><%# Eval("email") %></td>
                                        <td style="width: 8%;"><%# Eval("qq") %></td>
                                        <td style="width: 8%;"><%# Eval("mobile") %></td>
                                        <td style="width: 7.5%;"><%# Convert.ToDateTime(Eval("birthday")).ToString("yyyy-MM-dd") %></td>
                                        <td style="width: 12%;"><%# Eval("address") %></td>
                                        <td style="width: 4%;"><%# Eval("valid") %></td>
                                        <td style="width: 8%;"><%# Convert.ToDateTime(Eval("cdate")).ToString("yyyy-MM-dd<br />HH:mm:ss") %></td>
                                        <td style="width: 8%;">
                                            <asp:LinkButton ID="linAmend" runat="server" Text="修改" OnClientClick='<%# "showEditUser(" + Eval("id") + ");" %>'></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="删除" CommandName='<%# Eval("id") %>' Visible='<%# getVisible(Eval("name")) %>' OnClientClick="return confirm('确定要删除吗？删除后不可恢复');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div style="width: 100%; text-align: center;">当前没有数据，请添加！</div>
                    </EmptyDataTemplate>
                    <FooterStyle CssClass="pager_in" />
                    <HeaderStyle BackColor="#4780AE" Font-Bold="True" ForeColor="White" CssClass="gv_hrader" />
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle CssClass="pager_in" />
                    <RowStyle BackColor="white" CssClass="gv_row"/>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
