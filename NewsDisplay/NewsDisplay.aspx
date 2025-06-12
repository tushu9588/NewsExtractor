<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDisplay.aspx.cs" Inherits="NewsDisplay.NewsDisplay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Economic Times News</title>
    <!-- Bootstrap CSS CDN -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
            padding-top: 40px;
        }
        .form-inline .form-control {
            margin-right: 10px;
        }
        .table td a {
            color: #007bff;
            text-decoration: none;
        }
        .table td a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container">
        <h2 class="mb-4">📰 Economic Times News (Search by Date Range or Keyword)</h2>

        <div class="form-inline mb-4">
            <label class="mr-2 font-weight-bold">Start Date:</label>
            <asp:TextBox ID="StartDateTextBox" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

            <label class="ml-3 mr-2 font-weight-bold">End Date:</label>
            <asp:TextBox ID="EndDateTextBox" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

            <label class="ml-3 mr-2 font-weight-bold">Search:</label>
            <asp:TextBox ID="SearchTextBox" runat="server" CssClass="form-control" placeholder="Search title or URL"></asp:TextBox>

            <asp:Button ID="FetchNewsButton" runat="server" Text="📥 Fetch News" CssClass="btn btn-primary ml-3" OnClick="FetchNewsButton_Click" />
        </div>

        <asp:GridView ID="NewsGridView" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:TemplateField HeaderText="URL">
                    <ItemTemplate>
                        <a href='<%# Eval("Url") %>' target="_blank">Read Article</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PublicationDate" HeaderText="Published On" DataFormatString="{0:dd MMM yyyy HH:mm}" />
            </Columns>
        </asp:GridView>
    </form>

    <!-- Bootstrap JS CDN -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
