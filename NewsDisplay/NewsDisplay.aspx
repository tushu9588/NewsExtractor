<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDisplay.aspx.cs" Inherits="NewsDisplay.NewsDisplay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>News Display</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .card-title { font-size: 1rem; font-weight: bold; }
        .card-link { font-size: 0.9rem; word-break: break-word; }
        .card-time { font-size: 0.8rem; color: #666; }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container-fluid px-3 py-4">

        <!-- Filter Section -->
        <div class="row mb-4">
            <div class="col-md-3">
                <asp:Label runat="server" Text="Start Date:" AssociatedControlID="StartDate" />
                <asp:TextBox ID="StartDate" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" Text="End Date:" AssociatedControlID="EndDate" />
                <asp:TextBox ID="EndDate" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" Text="Search Title:" AssociatedControlID="SearchText" />
                <asp:TextBox ID="SearchText" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2 d-grid mt-4 pt-1">
                <asp:Button ID="SearchBtn" runat="server" CssClass="btn btn-primary" Text="🔍 Filter" OnClick="SearchBtn_Click" />
            </div>
        </div>

        <!-- Count Label -->
        <div class="mb-3">
            <asp:Label ID="BreakingNewsCountLabel" runat="server" CssClass="badge bg-success fs-6" />
        </div>

        <!-- Breaking News Section -->
        <div class="news-section">
            <h2 class="text-danger">🚨 Breaking News</h2>

            <!-- Desktop Table -->
            <div class="table-responsive d-none d-sm-block">
                <asp:GridView ID="BreakingNewsGrid" runat="server" 
                    CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:HyperLinkField DataTextField="Link" HeaderText="Link" DataNavigateUrlFields="Link" Target="_blank" />
                        <asp:BoundField DataField="InsertedAt" HeaderText="Published At" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Mobile Cards -->
            <div class="d-block d-sm-none">
                <asp:Repeater ID="BreakingNewsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Title") %></h5>
                                <a href='<%# Eval("Link") %>' class="card-link text-primary" target="_blank">Read More</a>
                                <p class="card-time mt-2"><%# Eval("InsertedAt", "{0:dd-MMM-yyyy HH:mm}") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <hr />

        <!-- Regular News Section -->
        <div class="news-section">
            <h2 class="text-primary">📰 Regular News</h2>

            <!-- Desktop Table -->
            <div class="table-responsive d-none d-sm-block">
                <asp:GridView ID="NewsGrid" runat="server" 
                    CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:HyperLinkField DataTextField="Url" HeaderText="Link" DataNavigateUrlFields="Url" Target="_blank" />
                        <asp:BoundField DataField="PublicationDate" HeaderText="Published On" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Mobile Cards -->
            <div class="d-block d-sm-none">
                <asp:Repeater ID="NewsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Title") %></h5>
                                <a href='<%# Eval("Url") %>' class="card-link text-primary" target="_blank">Read More</a>
                                <p class="card-time mt-2"><%# Eval("PublicationDate", "{0:dd-MMM-yyyy HH:mm}") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
