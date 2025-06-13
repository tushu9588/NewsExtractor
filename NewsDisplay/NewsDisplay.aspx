<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDisplay.aspx.cs" Inherits="NewsDisplay.NewsDisplay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>News Display</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container my-4">
        <h2 class="text-danger">🚨 Breaking News</h2>
        <asp:GridView ID="BreakingNewsGrid" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:HyperLinkField DataTextField="Link" HeaderText="Link" DataNavigateUrlFields="Link" Target="_blank" />
                <asp:BoundField DataField="InsertedAt" HeaderText="Published At" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
            </Columns>
        </asp:GridView>

        <hr />

        <h2 class="text-primary">📰 Regular News</h2>
        <asp:GridView ID="NewsGrid" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:HyperLinkField DataTextField="Url" HeaderText="Link" DataNavigateUrlFields="Url" Target="_blank" />
                <asp:BoundField DataField="PublicationDate" HeaderText="Published On" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
