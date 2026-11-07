<%@ Page Title="Nueva venta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPC_TiendaRopa.Ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Nueva venta</h2>

    <div class="row g-2 mb-3">

        <div class="col-md-4">
            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select">
                <asp:ListItem Text="Seleccionar cliente" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="col-md-4">
            <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select">
                <asp:ListItem Text="Seleccionar producto" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="col-md-2">
            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" placeholder="Cantidad" />
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
                <th>Precio unitario</th>
                <th>Subtotal</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <h5>Total: $0</h5>

    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar venta" CssClass="btn btn-primary" />

</asp:Content>