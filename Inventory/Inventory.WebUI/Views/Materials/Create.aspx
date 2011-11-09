<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inventory.Core.Entities.Material>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create New Material:</h2>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) { %>
        <p>           
            <label>Part Number</label>
            <%: Html.TextBoxFor(x => Model.PartNumber) %>
            <br />

            <label>Description</label>
            <%: Html.TextBoxFor(x => Model.Description) %>
            <br />

            <label>Type</label>
            <%: Html.DropDownListFor( x => Model.Type, new SelectListItem[] { 
                        new SelectListItem() { Text = MaterialType.Product.ToString(), Value = MaterialType.Product.ToString()},
                        new SelectListItem() { Text = MaterialType.Component.ToString(), Value = MaterialType.Component.ToString()}
            })%>
            <br />

            <label>Pieces/Case</label>
            <%: Html.TextBoxFor(x => x.PiecesPerCase)  %>
            <br />

            <label>Eaches/Piece</label>
            <%: Html.TextBoxFor(x => x.EachesPerPiece) %>
            <br />
        </p>

        <input type="submit" value="Save" />
         <%: Html.ActionLink("Cancel", "List", new { page = 1 })%>
    <% } %>
</asp:Content>
