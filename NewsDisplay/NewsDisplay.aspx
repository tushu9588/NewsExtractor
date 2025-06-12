<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDisplay.aspx.cs" Inherits="NewsDisplay.NewsDisplay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Economic Times News</title>
    <style>
        body {
            font-family: Arial;
            padding: 30px;
            background-color: #f8f9fa;
        }

        h2 {
            color: #343a40;
        }

        .news-table {
            border-collapse: collapse;
            width: 100%;
        }

        .news-table th, .news-table td {
            border: 1px solid #ccc;
            padding: 10px;
        }

        .news-table th {
            background-color: #007bff;
            color: white;
        }

        .news-table tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .news-table a {
            color: #007bff;
            text-decoration: none;
        }

        .news-table a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>📰 Economic Times News (Today & Yesterday)</h2>
        <asp:GridView ID="NewsGridView" runat="server" CssClass="news-table" AutoGenerateColumns="False">
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
</body>
</html>
