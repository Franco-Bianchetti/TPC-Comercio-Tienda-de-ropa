<%@ Page Title="Tipo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioTipo.aspx.cs" Inherits="TPC_TiendaRopa.FormularioTipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        <asp:Label ID="lblTitulo" runat="server" Text="Nuevo tipo" />
    </h2>

    <div class="mb-3" style="max-width: 400px;">
        <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label" AssociatedControlID="txtNombre" />
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100" />
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    <a href="Tipos.aspx" class="btn btn-secondary">Cancelar</a>

</asp:Content>