<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inventory.WebUI.Models.MaterialsListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inventory: Materials
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
   
   <div id="confirmation">
       <p>
            <%: TempData["confirmation_message"] %>
       </p>   
   </div>

   <%: Html.ActionLink("Add New", "Create") %>
   <%: Html.Partial("Paging", Model.PagingInfo) %>

   <div class="table">
        <table>
            <thead>
                <tr>
                    <th />
                    <th><%: Html.ActionLink("PartNumber", "List", new { page = 1, sort_col = "PartNumber", sort_asc = true })  %></th>
                    <th><%: Html.ActionLink("Description", "List", new { page = 1, sort_col = "Description", sort_asc = true })  %></th>
                    <th><%: Html.ActionLink("Type", "List", new { page = 1, sort_col = "Type", sort_asc = true })  %></th>
                    <th><%: Html.ActionLink("Pieces/Case", "List", new { page = 1, sort_col = "PiecesPerCase", sort_asc = true })  %></th>
                    <th><%: Html.ActionLink("Eaches/Piece", "List", new { page = 1, sort_col = "EachesPerPiece", sort_asc = true })  %></th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <% int row = Model.PagingInfo.StartRow; %>
                <% foreach (Material m in Model.Materials)
                   { %>
                   <tr>
                        <td><%: row %></td>
                        <td><%: m.PartNumber %></td>
                        <td><%: m.Description %></td>
                        <td><%: m.Type.ToString() %></td>
                        <td><%: string.Format("{0:#,0}", m.PiecesPerCase)  %></td>
                        <td><%: string.Format("{0:#,0}", m.EachesPerPiece) %></td>
                        <td>
                            <a href="<%: Url.Action("Edit", new { material_id = m.Id, return_url = Request.Url.PathAndQuery  }) %>">                           
                                <img alt="Edit" src="<%: Url.Content("~/Content/Images/edit.png") %>" title="Edit" />
                            </a>
                        </td>
                        <td>
                            <a href="<%: Url.Action("Delete", new { material_id = m.Id, return_url = Request.Url.PathAndQuery }) %>">                                
                                <img alt="Delete" src="<%: Url.Content("~/Content/Images/delete.gif") %>" title="Delete" />
                            </a>
                        </td>
                   </tr>
                   <% row++; %>
                <% } %>
            </tbody>
        </table>
   </div>
</asp:Content>
