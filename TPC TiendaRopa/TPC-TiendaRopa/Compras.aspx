<%@ Page Title="Nueva compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="TPC_TiendaRopa.Compras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Nueva compra</h2>

    <div class="row g-2 mb-3">

        <div class="col-md-3">
            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select">
                <asp:ListItem Text="Seleccionar proveedor" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="col-md-3">
            <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select">
                <asp:ListItem Text="Seleccionar producto" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="col-md-2">
            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" placeholder="Cantidad" />
        </div>

        <div class="col-md-2">
            <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="form-control" placeholder="Precio de compra" />
        </div>

        <div class="col-md-2">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-secondary w-100" />
        </div>

    </div>

    <table class="table table-striped table-bordered">
        <thead>
            <tr>
                <th>Producto</th>
                <th>Cantidad</th>
                <th>Precio de compra</th>
                <th>Subtotal</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <h5>Total: $0</h5>

    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar compra" CssClass="btn btn-primary" />

</asp:Content>
