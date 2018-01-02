<%@ Page Title="" Language="C#" MasterPageFile="~/Java.Master" AutoEventWireup="true" CodeBehind="Video.aspx.cs" Inherits="Java21.NET.Video" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>21天学通Java-视频管理</title>
    <link href="/Script/asyncbox/skins/ZCMS/asyncbox.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/cooke.js"></script>
    <script type="text/javascript" src="/Script/asyncbox/AsyncBox.v1.4.5.js"></script>
    <script type="text/javascript">
        function showDialog() {
            asyncbox.open({
                id: "edit_cate",
                url: 'VideoEdit.aspx',
                width: 300,
                height: 350,
                data: { "action": "add" },
                title: '添加视频',
            });
        }

        function showDialogEdit(id) {
            asyncbox.open({
                id: "edit_cate",
                url: 'VideoEdit.aspx',
                width: 300,
                height: 350,
                data: { "action": "edt", "id": id },
                title: '修改视频',
            });
        }

        function closeDialog() {
            asyncbox.close("edit_cate");
            __doPostBack('ctl00$ContentPlaceHolder1$linkData', '');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>在线视频后台管理</p>
    <asp:UpdatePanel ID="ajax" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvCategory_PageIndexChanging" CssClass="grid_b" GridLines="None" ShowFooter="True">
                <AlternatingRowStyle BackColor="#E7F7FF" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td style="width: 10%;">ID</td>
                                    <td style="width: 10%;">视频名称</td>
                                    <td style="width: 40%;">Flash地址</td>
                                    <td style="width: 30%;">Frame地址</td>
                                    <td style="width: 10%;">操作</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td style="width: 10%;"><%# Eval("id") %></td>
                                    <td style="width: 10%;"><%# Eval("name") %></td>
                                    <td style="width: 40%;"><%# Eval("url") %></td>
                                    <td style="width: 30%;"><%# Eval("url2") %></a></td>
                                    <td style="width: 10%;">
                                        <asp:LinkButton ID="lnkEdited" runat="server" Text="修改" OnClientClick='<%# "showDialogEdit(" + Eval("id") + ")" %>'></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="删除" CommandName='<%# Eval("id") %>' OnClientClick="return confirm('确定要删除吗？删除后不可恢复');" OnClick="lnkDelete_Click"></asp:LinkButton>
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
                <PagerStyle CssClass="pager_in" />
                <RowStyle BackColor="white" CssClass="gv_row" />
            </asp:GridView>
            <a href="javascript:void(0);" onclick="showDialog()">添加</a>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="linkData" runat="server" OnClick="linkData_Click"></asp:LinkButton>
</asp:Content>
