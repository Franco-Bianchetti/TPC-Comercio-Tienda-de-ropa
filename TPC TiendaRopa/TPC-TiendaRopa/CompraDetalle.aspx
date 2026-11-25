<%@ Page Title="Detalle Compra"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CompraDetalle.aspx.cs"
    Inherits="TPC_TiendaRopa.CompraDetalle" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="card shadow">

        <div class="card-body">

            <div class="d-flex justify-content-between align-items-center border-bottom pb-3 mb-3">
                <div>
                    <h2 class="fw-bold mb-0">Detalle de Compra</h2>
                    <span class="text-muted">
                        <asp:Label ID="lblTipoComprobante" runat="server" />
                        Nro: <asp:Label ID="lblNumeroComprobante" runat="server" />
                    </span>
                </div>
                <div class="text-end">
                    <strong>Fecha:</strong>
                    <asp:Label ID="lblFecha" runat="server" />
                </div>
            </div>

            <div class="row border rounded m-0 p-3 bg-light mb-3">

                <div class="col-md-6">
                    <div class="text-uppercase text-muted fw-bold small mb-1">
                        Proveedor
                    </div>
                    <div>
                        <strong>Razón Social:</strong>
                        <asp:Label ID="lblProveedor" runat="server" />
                    </div>
                </div>

                <div class="col-md-6 text-md-end">
                    <div class="text-uppercase text-muted fw-bold small mb-1">
                        Datos de la Operación
                    </div>
                    <div>
                        <strong>Registrado por:</strong>
                        <asp:Label ID="lblUsuario" runat="server" />
                    </div>
                </div>

            </div>

            <asp:GridView
                ID="gvDetalle"
                runat="server"
                CssClass="table table-bordered"
                AutoGenerateColumns="false">

                <Columns>

                    <asp:BoundField
                        DataField="NombreProducto"
                        HeaderText="Producto" />

                    <asp:BoundField
                        DataField="Cantidad"
                        HeaderText="Cantidad" />

                    <asp:BoundField
                        DataField="PrecioCompra"
                        HeaderText="Precio Compra"
                        DataFormatString="{0:C}" />

                    <asp:BoundField
                        DataField="Subtotal"
                        HeaderText="Subtotal"
                        DataFormatString="{0:C}" />

                </Columns>

            </asp:GridView>

            <div class="row mt-3">

                <div class="col-md-4 offset-md-8">

                    <table class="table mb-0">
                        <tr class="table-primary">
                            <th>TOTAL</th>
                            <td class="text-end">
                                <strong>
                                    <asp:Label ID="lblTotal" runat="server" />
                                </strong>
                            </td>
                        </tr>
                    </table>

                </div>

            </div>

            <div class="mt-3">
                <asp:HyperLink
                    ID="hlVolver"
                    runat="server"
                    NavigateUrl="~/Compras.aspx"
                    CssClass="btn btn-secondary">
                    Volver
                </asp:HyperLink>
            </div>

        </div>

    </div>

</asp:Content>