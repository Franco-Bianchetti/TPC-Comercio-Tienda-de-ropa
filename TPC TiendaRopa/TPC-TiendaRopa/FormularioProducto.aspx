<%@ Page Title="Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="TPC_TiendaRopa.FormularioProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        <asp:Label ID="lblTitulo" runat="server" Text="Nuevo producto" />
    </h2>

    <div class="row" style="max-width: 800px;">

        <div class="col-md-4 mb-3">
            <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="form-label" AssociatedControlID="txtCodigo" />
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" MaxLength="30" />
        </div>

        <div class="col-md-8 mb-3">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label" AssociatedControlID="txtNombre" />
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="150" />
        </div>

        <div class="col-md-4 mb-3">
            <asp:Label ID="lblMarca" runat="server" Text="Marca" CssClass="form-label" AssociatedControlID="ddlMarca" />
            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                <asp:ListItem Text="Seleccionar marca" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="col-md-4 mb-3">
            <asp:Label ID="lblTipo" runat="server" Text="Tipo" CssClass="form-label" AssociatedControlID="ddlTipo" />
            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                <asp:ListItem Text="Seleccionar tipo" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="col-md-2 mb-3">
            <asp:Label ID="lblTalle" runat="server" Text="Talle" CssClass="form-label" AssociatedControlID="txtTalle" />
            <asp:TextBox ID="txtTalle" runat="server" CssClass="form-control" MaxLength="20" />
        </div>

        <div class="col-md-2 mb-3">
            <asp:Label ID="lblColor" runat="server" Text="Color" CssClass="form-label" AssociatedControlID="txtColor" />
            <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" MaxLength="50" />
        </div>

        <div class="col-md-6 mb-3">
            <asp:Label ID="lblStockMinimo" runat="server" Text="Stock mínimo" CssClass="form-label" AssociatedControlID="txtStockMinimo" />
            <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" Text="0" />
        </div>

        <div class="col-md-6 mb-3">
            <asp:Label ID="lblPorcentaje" runat="server" Text="Porcentaje de ganancia (%)" CssClass="form-label" AssociatedControlID="txtPorcentaje" />
            <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="form-control" />
        </div>

        <div class="col-12 mb-3">
            <label class="form-label d-block">Proveedores</label>
            <asp:CheckBoxList ID="cblProveedores" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical" />
        </div>

    </div>

    <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    <a href="Productos.aspx" class="btn btn-secondary">Cancelar</a>

</asp:Content>