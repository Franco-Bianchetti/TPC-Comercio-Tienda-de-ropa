<%@ Page Title="Detalle Venta"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="VentaDetalle.aspx.cs"
    Inherits="TPC_TiendaRopa.VentaDetalle" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="card shadow">

        <div class="card-body">

            <!-- ORIGINAL -->

            <div class="text-center border border-bottom-0 p-2">

                <strong style="font-size:28px;">
                    ORIGINAL
                </strong>

            </div>

            <!-- CABECERA -->

            <div class="row border m-0">

                <!-- EMPRESA -->

                <div class="col-md-5 p-4 border-end">

                    <h1 class="fw-bold mb-3">

                        <asp:Label
                            ID="lblRazonSocial"
                            runat="server">
                        </asp:Label>

                    </h1>

                    <div class="mb-3">

                        <asp:Label
                            ID="lblDireccion"
                            runat="server">
                        </asp:Label>

                        <br />

                        <asp:Label
                            ID="lblCiudad"
                            runat="server">
                        </asp:Label>

                    </div>

                    <div class="mb-2">

                        <strong>CUIT:</strong>

                        <asp:Label
                            ID="lblCUIT"
                            runat="server">
                        </asp:Label>

                    </div>

                    <div class="mb-2">

                        <strong>Ingresos Brutos:</strong>

                        <asp:Label
                            ID="lblIngresosBrutos"
                            runat="server">
                        </asp:Label>

                    </div>

                    <div class="mb-3">

                        <strong>Fecha Inicio Actividad:</strong>

                        <asp:Label
                            ID="lblFechaInicioActividad"
                            runat="server">
                        </asp:Label>

                    </div>

                    <div style="
                        font-weight:bold;
                        font-size:18px;
                        text-transform:uppercase;">

                        <asp:Label
                            ID="lblCondicionIVA"
                            runat="server">
                        </asp:Label>

                    </div>

                </div>

                <!-- LETRA -->

                <div class="col-md-2 p-3 text-center border-end">

                    <div style="
                        border:4px solid black;
                        font-size:60px;
                        font-weight:bold;
                        height:120px;
                        line-height:100px;">

                        B

                    </div>

                    <strong>
                        CÓD. 06
                    </strong>

                </div>

                <!-- FACTURA -->

                <div class="col-md-5 p-4">

                    <h1 class="fw-bold mb-4">
                        FACTURA
                    </h1>

                    <p>

                        <strong>Punto de Venta:</strong>

                        <asp:Label
                            ID="lblPuntoVenta"
                            runat="server">
                        </asp:Label>

                    </p>

                    <p>

                        <strong>Comp. Nro:</strong>

                        <asp:Label
                            ID="lblComprobante"
                            runat="server">
                        </asp:Label>

                    </p>

                    <p>

                        <strong>Fecha de Emisión:</strong>

                        <asp:Label
                            ID="lblFecha"
                            runat="server">
                        </asp:Label>

                    </p>

                </div>

            </div>

            <!-- CLIENTE -->

            <div class="row border-start border-end border-bottom p-3">

                <div class="col-md-9 text-end d-flex flex-column justify-content-center">

                    <strong>Apellido y Nombre:</strong>

                    <asp:Label
                        ID="lblCliente"
                        runat="server">
                    </asp:Label>

                </div>

                <div class="col-md-5">

                    <strong>Forma de Pago:</strong>

                    <asp:Label
                        ID="lblFormaPago"
                        runat="server">
                    </asp:Label>

                </div>

            </div>

            <!-- VENDEDOR -->

            <div class="row border-start border-end border-bottom p-3">

                <div class="col-md-12">

                    <strong>Vendedor:</strong>

                    <asp:Label
                        ID="lblVendedor"
                        runat="server">
                    </asp:Label>

                </div>

            </div>

            <!-- DETALLE -->

            <asp:GridView
                ID="gvDetalle"
                runat="server"
                CssClass="table table-bordered mt-3"
                AutoGenerateColumns="false">

                <Columns>

                    <asp:BoundField
                        DataField="NombreProducto"
                        HeaderText="Producto" />

                    <asp:BoundField
                        DataField="Cantidad"
                        HeaderText="Cantidad" />

                    <asp:BoundField
                        DataField="PrecioUnitario"
                        HeaderText="Precio Unitario"
                        DataFormatString="{0:C}" />

                    <asp:BoundField
                        DataField="Subtotal"
                        HeaderText="Subtotal"
                        DataFormatString="{0:C}" />

                </Columns>

            </asp:GridView>

            <!-- TOTALES -->

            <div class="row">

                <div class="col-md-5 offset-md-7">

                    <table class="table">

                        <tr>

                            <th>
                                Subtotal
                            </th>

                            <td class="text-end">

                                <asp:Label
                                    ID="lblSubtotal"
                                    runat="server">
                                </asp:Label>

                            </td>

                        </tr>

                        <tr>

                            <th>
                                Descuento
                            </th>

                            <td class="text-end">

                                <asp:Label
                                    ID="lblDescuento"
                                    runat="server">
                                </asp:Label>

                            </td>

                        </tr>

                        <tr class="table-primary">

                            <th>
                                TOTAL
                            </th>

                            <td class="text-end">

                                <strong>

                                    <asp:Label
                                        ID="lblTotal"
                                        runat="server">
                                    </asp:Label>

                                </strong>

                            </td>

                        </tr>

                    </table>

                </div>

            </div>

            <hr />

            <!-- PIE FACTURA -->

            <div class="row mt-4">

                <div class="col-md-3">

                    <asp:Image
                    ID="imgQR"
                    runat="server"
                    Width="280px"
                    Height="120px"
                    ImageUrl="~/Imagenes/qr-demo.png" />

                </div>

                <div class="col-md-9 text-end d-flex flex-column justify-content-center">

                    <p>

                        <strong>CAE:</strong>

                        <asp:Label
                            ID="lblCAE"
                            runat="server">
                        </asp:Label>

                    </p>

                    <p>

                        <strong>Fecha de Vto. de CAE:</strong>

                        <asp:Label
                            ID="lblVencimientoCAE"
                            runat="server">
                        </asp:Label>

                    </p>

                </div>

            </div>

            <br />

            <asp:HyperLink
                ID="hlVolver"
                runat="server"
                NavigateUrl="~/Ventas.aspx"
                CssClass="btn btn-secondary">

                Volver

            </asp:HyperLink>

        </div>

    </div>

</asp:Content>