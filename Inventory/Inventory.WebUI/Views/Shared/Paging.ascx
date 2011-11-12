<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inventory.WebUI.Models.PagingInfoViewModel>" %>
<%@ Import Namespace="Inventory.WebUI.Models" %>
<div>
    <!-- Prev -->
    <% if (!Model.IsFirst) { %>
        <%: Html.ActionLink("Prev", "List",  RouteValueCombiner.Combine(Model.RouteValues, new {page = Model.CurrentPage - 1}))%>
    <% } %>

    <!-- Left Ellipses -->
    <% if (Model.HasLeftGroupGap) { %>
        <%: Html.ActionLink("1", "List", RouteValueCombiner.Combine(Model.RouteValues, new {page = 1 })) %>
        <label>...</label>
    <% } %>

    <!-- Page Numbers -->
    <%for (int i = Model.BlockStart; i < Model.BlockEnd + 1; i++)
      { %>
        <%: Html.ActionLink(i.ToString(), "List", RouteValueCombiner.Combine(Model.RouteValues, new {page = i })) %>
    <%} %>

    <!-- Right Ellipses -->
    <% if (Model.HasRightGroupGap) { %>
        <label>...</label>
        <%: Html.ActionLink(Model.TotalPages.ToString() , "List", RouteValueCombiner.Combine(Model.RouteValues, new {page = Model.TotalPages })) %>
    <% } %>
    
    <!-- Next -->
    <% if (!Model.IsLast) { %>
        <%: Html.ActionLink("Next", "List", RouteValueCombiner.Combine(Model.RouteValues, new {page = Model.CurrentPage + 1 })) %>
    <% } %>
</div>

