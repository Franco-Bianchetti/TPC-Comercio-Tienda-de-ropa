<%@ Page Title="Proveedor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProveedor.aspx.cs" Inherits="TPC_TiendaRopa.FormularioProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        <asp:Label ID="lblTitulo" runat="server" Text="Nuevo proveedor" />
    </h2>

    <div class="row" style="max-width: 600px;">
        <div class="col-12 mb-3">
            <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social" CssClass="form-label" AssociatedControlID="txtRazonSocial" />
            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" MaxLength="150" />
        </div>

        <div class="col-md-6 mb-3">
            <asp:Label ID="lblCuit" runat="server" Text="CUIT" CssClass="form-label" AssociatedControlID="txtCuit" />
            <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" MaxLength="20" placeholder="Ej: 30-11223344-5" />
        </div>

        <div class="col-md-6 mb-3">
            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono" CssClass="form-label" AssociatedControlID="txtTelefono" />
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="30" />
        </div>

        <div class="col-12 mb-3">
            <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label" AssociatedControlID="txtEmail" />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" TextMode="Email" />
        </div>
    </div>

    <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    <a href="Proveedores.aspx" class="btn btn-secondary">Cancelar</a>

</asp:Content>