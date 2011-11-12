<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inventory.Core.Entities.Material>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inventory: Add Material
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <div id="error">
       <p>
            <%: TempData["error"] %>
       </p>   
   </div>
    <h2>Create New Material:</h2>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) { %>        
        <table>
            <tbody>
                <tr>
                    <th>Part Number</th>
                    <td><%: Html.TextBoxFor(x => Model.PartNumber) %></td>
                    <td><%: Html.ValidationMessageFor(x => Model.PartNumber) %></td>
                </tr>
                <tr>
                    <th>Description</th>
                    <td><%: Html.TextBoxFor(x => Model.Description) %></td>
                    <td><%: Html.ValidationMessageFor( x => Model.Description) %></td>
                </tr>
                <tr>
                    <th>Type</th>
                    <td>
                        <%: Html.DropDownListFor( x => Model.Type, new SelectListItem[] { 
                                new SelectListItem() { Text = MaterialType.Product.ToString(), Value = MaterialType.Product.ToString()},
                                new SelectListItem() { Text = MaterialType.Component.ToString(), Value = MaterialType.Component.ToString()}
                        })%>    
                    </td>
                    <td><%: Html.ValidationMessageFor(x => Model.Type) %></td>
                </tr>
                <tr>
                    <th>Pieces/Case</th>
                    <td><%: Html.TextBoxFor(x => x.PiecesPerCase) %></td>
                    <td><%: Html.ValidationMessageFor(x => x.PiecesPerCase) %></td>
                </tr>
                <tr>
                    <th>Eaches/Piece</th>
                    <td><%: Html.TextBoxFor(x => x.EachesPerPiece) %></td>
                    <td><%: Html.ValidationMessageFor(x => x.EachesPerPiece) %></td>
                </tr>
            </tbody>
        </table>

        <input type="submit" value="Save" />
         <%: Html.ActionLink("Cancel", "List", new { page = 1 })%>
    <% } %>
</asp:Content>
