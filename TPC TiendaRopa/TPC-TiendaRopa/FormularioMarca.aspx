<%@ Page Title="Marca" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioMarca.aspx.cs" Inherits="TPC_TiendaRopa.FormularioMarca" %>
<%@ Page Title="Marca" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioMarca.aspx.cs" Inherits="TPC_TiendaRopa.FormularioMarca" %>
<%@ Page Title="Marca" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioMarca.aspx.cs" Inherits="TPC_TiendaRopa.FormularioMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        <asp:Label ID="lblTitulo" runat="server" Text="Nueva marca" />
    </h2>

    <div class="mb-3" style="max-width: 400px;">
        <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label" AssociatedControlID="txtNombre" />
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100" />
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
        <asp:RequiredFieldValidator 
            ID="rfvNombre" 
            runat="server" 
            ControlToValidate="txtNombre"
            ErrorMessage="El nombre de la marca es obligatorio." 
            CssClass="text-danger small d-block" 
            Display="Dynamic" />

        <asp:RegularExpressionValidator 
            ID="revNombre" 
            runat="server" 
            ControlToValidate="txtNombre"
            ValidationExpression=".*\S.*"
            ErrorMessage="El nombre no puede contener solo espacios en blanco." 
            CssClass="text-danger small d-block" 
            Display="Dynamic" />
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    <a href="Marcas.aspx" class="btn btn-secondary">Cancelar</a>

</asp:Content>