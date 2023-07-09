<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IKFCRUD.aspx.cs" Inherits="IKF.IKFCRUD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASP.NET Web Application</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.js"></script>

    <style type="text/css">
        a.class1 {
            color: Green;
        }

        .Mytable {
            border-collapse: collapse;
        }

            .Mytable td {
                padding: 6px;
            }

        body, html, div {
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            margin-left: 0px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            font-weight: normal;
            color: #000000;
            text-decoration: none;
            text-align: left;
        }

        form {
            margin: 0 auto;
            width: 700px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=ddlSkills]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="align-content: center">
        <div id="alertContainer" runat="server"></div>
        <table style="width: 100%; padding-left: 10px" class="Mytable" border="1">
            <tr>
                <td colspan="2">
                    <h1 style="text-align: center">IKF Technical Assignment</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnNew" runat="server" Text="New User" OnClick="btnNew_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnList" runat="server" Text="List User" OnClick="btnList_Click" CssClass="btn btn-secondary" />
                </td>
            </tr>
        </table>
        <table style="width: 100%; padding-left: 10px" class="Mytable" border="1" id="Maintable" runat="server">

            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" class="form-control" Width="52%"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblDob" runat="server" Text="Date Of Birth" ReadOnly="true"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDOB" runat="server" CssClass='datepickerDefault form-control' Width="52%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldesignation" runat="server" Text="Designation"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" Width="52%">
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSkills" runat="server" Text="Skills"></asp:Label>
                </td>
                <td>
                    <asp:ListBox ID="ddlSkills" runat="server" SelectionMode="Multiple"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add User" OnClick="btnSubmit_Click" CssClass="btn btn-success" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update User" OnClick="btnUpdate_Click" CssClass="btn btn-warning" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary" />
                </td>
            </tr>
        </table>

        <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(".datepickerDefault").datepicker({
                    dateFormat: 'dd-mm-yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "c-50:c"
                });

                setTimeout(function () {
                    var alertContainer = document.getElementById("alertContainer");
                    if (alertContainer.children.length > 0) {
                        alertContainer.removeChild(alertContainer.children[0]);
                    }
                }, 5000);
            });
        </script>
        <hr />
        <div id="divContainer" runat="server" style="height: 150PX; width: 700PX; overflow: auto; border: 1px solid #990000;">
            <asp:GridView ID="Grddata" runat="server" ForeColor="#333333" Width="100%" AutoGenerateColumns="false"
                OnRowDataBound="Grddata_RowDataBound" OnRowDeleting="Grddata_RowDeleting"
                OnRowCommand="Grddata_RowCommand" OnRowEditing="Grddata_RowEditing">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Of Birth">
                        <ItemTemplate>
                            <asp:Label ID="lbldob" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Skills">
                        <ItemTemplate>
                            <asp:Label ID="lblskills" runat="server" Text='<%# Eval("SKILLS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("DESIGNATION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkedit" CommandName="Edit" runat="server" CommandArgument='<%# Container.DataItemIndex + 1 %>'>Edit User</asp:LinkButton>
                            <asp:HiddenField ID="IKF" runat="server" Value='<%# Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="Delete User" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
        <hr />
    </form>
</body>
</html>
