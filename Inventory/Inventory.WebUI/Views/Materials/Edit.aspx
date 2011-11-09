<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inventory.Core.Entities.Material>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <h2>Edit: <%: Model.PartNumber %></h2>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) { %>
        <p>
            <%: Html.HiddenFor(x => x.Id) %>
            <%: Html.HiddenFor(x => x.PartNumber) %>
            <%: Html.HiddenFor(x => x.Version) %>
            <%: Html.HiddenFor(x => x.Type) %>
            
            <label>Part Number</label>
            <label><%: Model.PartNumber %></label>
            <br />

            <label>Description</label>
            <%: Html.TextBoxFor(x => Model.Description) %>
            <br />

            <label>Type</label>
            <label><%: Model.Type %></label>
            <br />

            <label>Pieces/Case</label>
            <%: Html.TextBoxFor(x => x.PiecesPerCase)  %>
            <br />

            <label>Eaches/Piece</label>
            <%: Html.TextBoxFor(x => x.EachesPerPiece) %>
            <br />
        </p>

        <input type="submit" value="Save" />
        <%: Html.ActionLink("Cancel", "List") %>
    <% } %>

</asp:Content>
