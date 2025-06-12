<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDisplay.aspx.cs" Inherits="NewsDisplay.NewsDisplay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Economic Times News</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    
    <!-- Bootstrap CSS -->
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
        .breaking-news {
            margin-top: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container">
        <h2 class="mb-4">📰 Economic Times News (Search by Date Range or Keyword)</h2>

        <!-- 🔍 Filter Controls -->
        <div class="form-inline mb-4">
            <label class="mr-2 font-weight-bold">Start Date:</label>
            <asp:TextBox ID="StartDateTextBox" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

            <label class="ml-3 mr-2 font-weight-bold">End Date:</label>
            <asp:TextBox ID="EndDateTextBox" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

            <label class="ml-3 mr-2 font-weight-bold">Search:</label>
            <asp:TextBox ID="SearchTextBox" runat="server" CssClass="form-control" placeholder="Search title or URL"></asp:TextBox>

            <asp:Button ID="FetchNewsButton" runat="server" Text="📥 Fetch News" CssClass="btn btn-primary ml-3" OnClick="FetchNewsButton_Click" />
        </div>

        <!-- 🔥 Server-Side Breaking News Section -->
        <div class="card mb-4 shadow-sm">
            <div class="card-header bg-danger text-white d-flex justify-content-between align-items-center">
                <h5 class="mb-0">🔥 Breaking News (Server)</h5>
                <asp:Label ID="BreakingNewsCount" runat="server" CssClass="font-weight-bold" />
            </div>
            <div class="card-body p-2">
                <asp:Repeater ID="BreakingNewsRepeater" runat="server">
                    <HeaderTemplate>
                        <ul class="list-group list-group-flush">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="list-group-item px-2 py-1">
                            <strong><%# Container.ItemIndex + 1 %>.</strong>
                            <%# Eval("title") %>
                            <asp:HyperLink NavigateUrl='<%# Eval("link") %>' runat="server" Target="_blank" CssClass="ml-2 text-primary">Read</asp:HyperLink>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- 📋 News Grid from MySQL -->
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

        <!-- 🌐 JS-Based Breaking News -->
        <div class="breaking-news">
            <h4>🌐 Breaking News (via JS) <span class="badge badge-danger" id="breakingCount">0</span></h4>
            <table class="table table-hover table-bordered" id="breakingTable">
                <thead class="thead-dark">
                    <tr>
                        <th>#</th>
                        <th>Title</th>
                        <th>Link</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </form>

    <!-- Scripts -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        $(document).ready(function () {
            fetchBreakingNews();
        });

        function fetchBreakingNews() {
            $.ajax({
                url: "https://economictimes.indiatimes.com/etstatic/breakingnews/etjson_bnews.html",
                dataType: "script",
                success: function () {
                    let newsItems = window.breakingnews || [];
                    let tbody = $('#breakingTable tbody');
                    tbody.empty();
                    let count = 0;

                    newsItems.forEach((item) => {
                        if (item.title && item.link) {
                            count++;
                            tbody.append(`
                                <tr>
                                    <td>${count}</td>
                                    <td>${item.title.split('###')[1]}</td>
                                    <td><a href="${item.link}" target="_blank">View</a></td>
                                </tr>
                            `);
                        }
                    });

                    $('#breakingCount').text(count);
                }
            });
        }
    </script>
</body>
</html>
