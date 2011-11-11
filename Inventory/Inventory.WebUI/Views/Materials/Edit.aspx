<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inventory.Core.Entities.Material>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inventory: Edit Material
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
   <div id="error">
       <p>
            <%: TempData["error"] %>
       </p>   
   </div>
    <h2>Edit: <%: Model.PartNumber %></h2>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) { %>

        <%: Html.HiddenFor(x => x.Id) %>
        <%: Html.HiddenFor(x => x.PartNumber) %>
        <%: Html.HiddenFor(x => x.Version) %>
        <%: Html.HiddenFor(x => x.Type) %>

        <table>
            <tr>
                <th>Part Number</th>
                <td><%: Model.PartNumber %></td>
            </tr>
            <tr>
                <th>Description</th>
                <td><%: Html.TextBoxFor(x => x.Description) %></td>
            </tr>
            <tr>
                <th>Type</th>
                <td><%: Model.Type %></td>
            </tr>
            <tr>
                <th>Pieces/Case</th>
                <td><%: Html.TextBoxFor(x => x.PiecesPerCase) %></td>
            </tr>
            <tr>
                <th>Eaches/Piece</th>
                <td><%: Html.TextBoxFor(x => x.EachesPerPiece) %></td>
            </tr>
        </table>

        <input type="submit" value="Save" />
        <%: Html.ActionLink("Cancel", "List", new { page = 1 })%>
    <% } %>
</asp:Content>
