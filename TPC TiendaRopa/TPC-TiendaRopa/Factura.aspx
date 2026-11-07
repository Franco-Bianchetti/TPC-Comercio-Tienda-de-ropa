<%@ Page Title="Factura" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="TPC_TiendaRopa.Factura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Factura</h2>

    <div class="card mb-3">
        <div class="card-body">
            <p class="mb-1"><strong>Número:</strong> 0001-00000001</p>
            <p class="mb-1"><strong>Fecha:</strong> --/--/----</p>
            <p class="mb-0"><strong>Cliente:</strong> (sin cliente)</p>
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

    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="btn btn-primary"
        OnClientClick="window.print(); return false;" />

</asp:Content>
