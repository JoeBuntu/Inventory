<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inventory.WebUI.Models.MaterialsListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inventory: Materials
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
   
   
   <%: Html.ActionLink("Add New", "Create") %>
   <%: Html.Partial("Paging", Model.PagingInfo) %>
   <div class="table">
        <table>
            <thead>
                <tr>
                    <th><%: Html.ActionLink("ID", "List", new { page = 1, sort_col = "Id", sort_asc = true })  %></th>
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
                <% foreach (Material m in Model.Materials)
                   { %>
                   <tr>
                        <td><%: m.Id %></td>
                        <td><%: m.PartNumber %></td>
                        <td><%: m.Description %></td>
                        <td><%: m.Type.ToString() %></td>
                        <td><%: string.Format("{0:#,0}", m.PiecesPerCase)  %></td>
                        <td><%: string.Format("{0:#,0}", m.EachesPerPiece) %></td>
                        <td>
                            <a href="<%: Url.Action("Edit", new { material_id = m.Id }) %>">                           
                                <img alt="Edit" src="<%: Url.Content("~/Content/Images/edit.png") %>" title="Edit" />
                            </a>
                        </td>
                        <td>
                            <a href="<%: Url.Action("Delete", new { material_id = m.Id }) %>">                                
                                <img alt="Delete" src="<%: Url.Content("~/Content/Images/delete.gif") %>" title="Delete" />
                            </a>
                        </td>
                   </tr>
                <% } %>
            </tbody>
        </table>
   </div>

    <div class="pager">

    </div>
</asp:Content>
