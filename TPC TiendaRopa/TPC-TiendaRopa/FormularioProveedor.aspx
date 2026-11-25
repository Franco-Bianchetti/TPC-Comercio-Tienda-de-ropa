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

    <asp:RequiredFieldValidator ID="rfvRazonSocial" runat="server" ControlToValidate="txtRazonSocial"
    ErrorMessage="La razón social es obligatoria." CssClass="text-danger small d-block" Display="Dynamic" />
<asp:RegularExpressionValidator ID="revRazonSocial" runat="server" ControlToValidate="txtRazonSocial"
    ValidationExpression=".*\S.*" ErrorMessage="La razón social no puede contener solo espacios."
    CssClass="text-danger small d-block" Display="Dynamic" />

    <asp:RequiredFieldValidator ID="rfvCuit" runat="server" ControlToValidate="txtCuit"
    ErrorMessage="El CUIT es obligatorio." CssClass="text-danger small d-block" Display="Dynamic" />
<asp:RegularExpressionValidator ID="revCuit" runat="server" ControlToValidate="txtCuit"
    ValidationExpression="^\d{2}-?\d{8}-?\d{1}$" ErrorMessage="Formato inválido (Ej: 30-11223344-5 o 11 números)."
    CssClass="text-danger small d-block" Display="Dynamic" />

<asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
    ValidationExpression="^[0-9\s\-\+\(\)]{6,30}$" ErrorMessage="Ingrese un teléfono válido (solo números y guiones)."
    CssClass="text-danger small d-block" Display="Dynamic" />

    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Ingrese un formato de email válido."
    CssClass="text-danger small d-block" Display="Dynamic" />

</asp:Content>