<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inventory.Core.Entities.Material>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inventory: Delete Material
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <div id="error">
       <p>
            <%: TempData["error"] %>
       </p>   
   </div>
    <h2>Confirm Deletion: <%: Model.PartNumber %></h2>
    <table>
        <tbody>
            <tr>
                <th>Part Number</th>
                <td><%: Model.PartNumber %></td>
            </tr>
            <tr>
                <th>Description</th>
                <td><%: Model.Description %></td>
            </tr>
            <tr>
                <th>Type</th>
                <td><%: Model.Type %></td>
            </tr>
            <tr>
                <th>Pieces/Case</th>
                <td><%: string.Format("{0:#,0}", Model.PiecesPerCase) %></td>
            </tr>
            <tr>
                <th>Eaches/Piece</th>
                <td><%: string.Format("{0:#,0}", Model.EachesPerPiece) %></td>
            </tr>
        </tbody>
    </table>
    <% using (Html.BeginForm("ConfirmDelete", "Materials", new { material_id = Model.Id }))
       { %>

        <input type="submit" value="Confirm" />
        <%: Html.ActionLink("Cancel", "List", new { page = 1 })%>
    <% } %>
</asp:Content>
