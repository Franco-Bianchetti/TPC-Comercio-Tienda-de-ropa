<%@ Page Title="Clientes"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Clientes.aspx.cs"
    Inherits="TPC_TiendaRopa.Clientes" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Clientes</h2>

    <asp:GridView
        ID="gvClientes"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false">

        <Columns>

            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="Dni" HeaderText="DNI" />
            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
            <asp:BoundField DataField="Email" HeaderText="Email" />

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