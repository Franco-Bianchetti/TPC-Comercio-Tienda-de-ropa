<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPC_TiendaRopa.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Productos</h2>

    <asp:GridView ID="gvProductos" runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvProductos_RowCommand">

        <Columns>

            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Codigo" HeaderText="Código" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />

            <asp:TemplateField HeaderText="Marca">
                <ItemTemplate><%# Eval("Marca.Nombre") %></ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Tipo">
                <ItemTemplate><%# Eval("Tipo.Nombre") %></ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Talle" HeaderText="Talle" />
            <asp:BoundField DataField="Color" HeaderText="Color" />
            <asp:BoundField DataField="StockActual" HeaderText="Stock" />
            <asp:BoundField DataField="StockMinimo" HeaderText="Stock mínimo" />
            <asp:BoundField DataField="PorcentajeGanancia" HeaderText="% Ganancia" DataFormatString="{0:0.##}" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <a href="FormularioProducto.aspx?id=<%# Eval("Id") %>" class="btn btn-warning btn-sm">Editar</a>

                    <asp:Button ID="btnEliminarFila" runat="server"
                        Text="Eliminar"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClientClick="return confirm('¿Seguro que querés eliminar este producto?');" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <br />

    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />

</asp:Content>