<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inventory.WebUI.Models.MaterialsListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inventory - Materials
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
   
   <div class="table">
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Part Number</th>
                    <th>Description</th>
                    <th>Type</th>
                    <th>Pieces/Case</th>
                    <th>Eaches/Piece</th>
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
                        <td><%: m.PiecesPerCase.ToString("n") %></td>
                        <td><%: m.EachesPerPiece.ToString("n") %></td>
                        <td>
                            <a href="<%: Url.Action("Edit", new { material_id = m.Id }) %>">
                                <img src="../../Content/Images/edit.png" title="Edit" />
                            </a>
                        </td>
                        <td>
                            <a href="<%: Url.Action("Delete", new { material_id = m.Id }) %>">
                                <img src="../../Content/Images/delete.gif" title="Delete" />
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
