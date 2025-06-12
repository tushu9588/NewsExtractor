<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDisplay.aspx.cs" Inherits="NewsDisplay.NewsDisplay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Economic Times News</title>
    <!-- ✅ Bootstrap 5 CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container py-5">
            <h2 class="text-center text-primary mb-4">📰 Economic Times News (Select Date Range)</h2>

            <!-- 🔽 Date Inputs Row -->
            <div class="row mb-4">
                <div class="col-md-4">
                    <label for="StartDateTextBox" class="form-label fw-bold">Start Date</label>
                    <asp:TextBox ID="StartDateTextBox" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label for="EndDateTextBox" class="form-label fw-bold">End Date</label>
                    <asp:TextBox ID="EndDateTextBox" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4 d-flex align-items-end">
                    <asp:Button ID="FetchNewsButton" runat="server" Text="📥 Fetch News" OnClick="FetchNewsButton_Click" CssClass="btn btn-primary w-100" />
                </div>
            </div>

            <!-- 🔽 News Table -->
            <asp:GridView ID="NewsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
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
        </div>
    </form>
</body>
</html>
