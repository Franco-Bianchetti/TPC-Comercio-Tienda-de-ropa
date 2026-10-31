<%@ Page Title="Proveedores"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Proveedores.aspx.cs"
    Inherits="TPC_TiendaRopa.Proveedores" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Proveedores</h2>

    <asp:GridView
        ID="gvProveedores"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false">

        <Columns>

            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" />
            <asp:BoundField DataField="Cuit" HeaderText="CUIT" />
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