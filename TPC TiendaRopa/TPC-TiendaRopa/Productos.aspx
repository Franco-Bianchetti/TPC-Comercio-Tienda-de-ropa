<%@ Page Title="Productos"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Productos.aspx.cs"
    Inherits="TPC_TiendaRopa.Productos" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Productos</h2>

    <asp:GridView
        ID="gvProductos"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false">

        <Columns>

            <asp:BoundField DataField="Id" HeaderText="ID" />

            <asp:BoundField DataField="Nombre"
                HeaderText="Nombre" />

            <asp:BoundField DataField="Talle"
                HeaderText="Talle" />

            <asp:BoundField DataField="Color"
                HeaderText="Color" />

            <asp:BoundField DataField="StockActual"
                HeaderText="Stock" />

            <asp:BoundField DataField="StockMinimo"
                HeaderText="Stock Mínimo" />

            <asp:BoundField DataField="PorcentajeGanancia"
                HeaderText="% Ganancia" />

        </Columns>

    </asp:GridView>

    <br />

    <asp:Button ID="btnNuevo" runat="server"
        Text="Nuevo"
        CssClass="btn btn-primary" />

    <asp:Button ID="btnEditar" runat="server"
        Text="Editar"
        CssClass="btn btn-warning" />

    <asp:Button ID="btnEliminar" runat="server"
        Text="Eliminar"
        CssClass="btn btn-danger" />

</asp:Content>