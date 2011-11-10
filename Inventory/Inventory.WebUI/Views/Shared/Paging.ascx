<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inventory.WebUI.Models.PagingInfo>" %>
<div>
    <!-- Prev -->
    <% if (!Model.IsFirst) { %>
        <%: Html.ActionLink("Prev", "List", new {page = Model.CurrentPage - 1, sort_col = Model.SortColumn, sort_asc = Model.SortAsc} )%>
    <% } %>

    <!-- Left Ellipses -->
    <% if (Model.HasLeftGroupGap) { %>
        <%: Html.ActionLink("1", "List", new {page = 1, sort_col = Model.SortColumn, sort_asc = Model.SortAsc }) %>
        <label>...</label>
    <% } %>

    <!-- Page Numbers -->
    <%for (int i = Model.GroupStart; i < Model.GroupEnd + 1; i++)
      { %>
        <%: Html.ActionLink(i.ToString(), "List", new {page = i, sort_col = Model.SortColumn, sort_asc = Model.SortAsc }) %>
    <%} %>

    <!-- Right Ellipses -->
    <% if (Model.HasRightGroupGap) { %>
        <label>...</label>
        <%: Html.ActionLink(Model.TotalPages.ToString() , "List", new {page = Model.TotalPages, sort_col = Model.SortColumn, sort_asc = Model.SortAsc }) %>
    <% } %>
    
    <!-- Next -->
    <% if (!Model.IsLast) { %>
        <%: Html.ActionLink("Next", "List", new {page = Model.CurrentPage + 1, sort_col = Model.SortColumn, sort_asc = Model.SortAsc})%>
    <% } %>
</div>

