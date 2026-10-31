<%@ Page Title="Marcas"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Marcas.aspx.cs"
    Inherits="TPC_TiendaRopa.Marcas" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Marcas</h2>

    <asp:GridView
        ID="gvMarcas"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false">

        <Columns>

            <asp:BoundField
                DataField="Id"
                HeaderText="ID" />

            <asp:BoundField
                DataField="Nombre"
                HeaderText="Nombre" />

        </Columns>

    </asp:GridView>

    <br />

    <asp:Button
        ID="btnNuevo"
        runat="server"
        Text="Nuevo"
        CssClass="btn btn-primary" />

    <asp:Button
        ID="btnEditar"
        runat="server"
        Text="Editar"
        CssClass="btn btn-warning" />

    <asp:Button
        ID="btnEliminar"
        runat="server"
        Text="Eliminar"
        CssClass="btn btn-danger" />

</asp:Content>