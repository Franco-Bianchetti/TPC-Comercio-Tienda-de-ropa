<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPC_TiendaRopa.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Productos</h2>

        <asp:Panel runat="server" DefaultButton="btnBuscar">
        <div class="row g-2 mb-3" style="max-width: 600px;">
            <div class="col">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar..." />
            </div>
            <div class="col-auto">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
            </div>
        </div>
    </asp:Panel>

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

        <asp:Label ID="lblResultado" runat="server" CssClass="d-block mb-3" Visible="false" />

    <br />

    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />

</asp:Content>