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

        <div class="card-body p-3">

            <!-- ORIGINAL -->
            <div class="text-center border border-bottom-0 py-1">
                <strong style="font-size:20px; letter-spacing:1px;">
                    ORIGINAL
                </strong>
            </div>

            <!-- CABECERA -->
            <div class="row border m-0">

                <!-- EMPRESA -->
                <div class="col-md-5 p-3 border-end">

                    <h2 class="fw-bold mb-2" style="font-size:26px;">
                        <asp:Label
                            ID="lblRazonSocial"
                            runat="server" />
                    </h2>

                    <div class="mb-2 small">
                        <asp:Label
                            ID="lblDireccion"
                            runat="server" />
                        <br />
                        <asp:Label
                            ID="lblCiudad"
                            runat="server" />
                    </div>

                    <div class="mb-1 small">
                        <strong>CUIT:</strong>
                        <asp:Label
                            ID="lblCUIT"
                            runat="server" />
                    </div>

                    <div class="mb-1 small">
                        <strong>Ingresos Brutos:</strong>
                        <asp:Label
                            ID="lblIngresosBrutos"
                            runat="server" />
                    </div>

                    <div class="mb-2 small">
                        <strong>Fecha Inicio Actividad:</strong>
                        <asp:Label
                            ID="lblFechaInicioActividad"
                            runat="server" />
                    </div>

                    <div class="fw-bold small text-uppercase">
                        <asp:Label
                            ID="lblCondicionIVA"
                            runat="server" />
                    </div>

                </div>

                <!-- LETRA -->
                <div class="col-md-2 p-2 text-center border-end d-flex flex-column align-items-center justify-content-start pt-3">

                    <div style="border:3px solid black; font-size:38px; font-weight:bold; width:72px; height:72px; line-height:64px;">
                        <asp:Label
                            ID="lblLetra"
                            runat="server"
                            Text="B" />
                    </div>

                    <strong class="small mt-1">
                        <asp:Label
                            ID="lblCodigoComprobante"
                            runat="server"
                            Text="CÓD. 06" />
                    </strong>

                </div>

                <!-- FACTURA -->
                <div class="col-md-5 p-3">

                    <h4 class="fw-bold mb-3" style="font-size:19px;">
                        <asp:Label
                            ID="lblTituloComprobante"
                            runat="server"
                            Text="FACTURA" />
                    </h4>

                    <p class="mb-1 small">
                        <strong>Punto de Venta:</strong>
                        <asp:Label
                            ID="lblPuntoVenta"
                            runat="server" />
                    </p>

                    <p class="mb-1 small">
                        <strong>Comp. Nro:</strong>
                        <asp:Label
                            ID="lblComprobante"
                            runat="server" />
                    </p>

                    <p class="mb-0 small">
                        <strong>Fecha de Emisión:</strong>
                        <asp:Label
                            ID="lblFecha"
                            runat="server" />
                    </p>

                </div>

            </div>

            <!-- DATOS DEL CLIENTE -->
            <div class="border-start border-end border-bottom m-0 p-3 small">

                <div class="text-uppercase text-muted fw-bold mb-2" style="font-size:11px;">
                    Datos del Cliente
                </div>

                <div class="mb-1">
                    <strong>Apellido y Nombre:</strong>
                    <asp:Label
                        ID="lblCliente"
                        runat="server" />
                </div>

                <div class="mb-1">
                    <strong>DNI:</strong>
                    <asp:Label
                        ID="lblDniCliente"
                        runat="server" />
                </div>

                <div class="mb-1">
                    <strong>Dirección:</strong>
                    <asp:Label
                        ID="lblDireccionCliente"
                        runat="server" />
                </div>

                <div class="mb-1">
                    <strong>Teléfono:</strong>
                    <asp:Label
                        ID="lblTelefonoCliente"
                        runat="server" />
                </div>

                <div>
                    <strong>Email:</strong>
                    <asp:Label
                        ID="lblEmailCliente"
                        runat="server" />
                </div>

            </div>

            <!-- DATOS DE LA OPERACIÓN -->
            <div class="border-start border-end border-bottom m-0 p-3 bg-light small">

                <div class="text-uppercase text-muted fw-bold mb-1" style="font-size:11px;">
                    Datos de la Operación
                </div>

                <div>
                    <strong>Vendedor:</strong>
                    <asp:Label
                        ID="lblVendedor"
                        runat="server" />
                </div>

            </div>

            <!-- DETALLE -->
            <asp:GridView
                ID="gvDetalle"
                runat="server"
                CssClass="table table-bordered table-sm mt-3 small"
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

            <!-- FORMA DE PAGO Y TOTALES -->
            <div class="row mt-3 align-items-start small">

                <div class="col-md-6">
                    <div class="border rounded p-2 px-3 bg-light">
                        <strong>Forma de Pago:</strong>
                        <asp:Label
                            ID="lblFormaPago"
                            runat="server" />
                    </div>
                </div>

                <div class="col-md-5 offset-md-1">

                    <table class="table table-sm mb-0">

                        <tr>
                            <th>Subtotal</th>
                            <td class="text-end">
                                <asp:Label
                                    ID="lblSubtotal"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <th>Descuento</th>
                            <td class="text-end">
                                <asp:Label
                                    ID="lblDescuento"
                                    runat="server" />
                            </td>
                        </tr>

                        <tr class="table-primary">
                            <th>TOTAL</th>
                            <td class="text-end">
                                <strong>
                                    <asp:Label
                                        ID="lblTotal"
                                        runat="server" />
                                </strong>
                            </td>
                        </tr>

                    </table>

                </div>

            </div>

            <hr class="my-3" />

            <!-- PIE FACTURA -->
            <div class="row mt-2 small">

                <div class="col-md-3">
                    <asp:Image
                        ID="imgQR"
                        runat="server"
                        Width="220px"
                        Height="95px"
                        ImageUrl="~/Imagenes/qr-demo.png" />
                </div>

                <div class="col-md-9 text-end d-flex flex-column justify-content-center">

                    <p class="mb-1">
                        <strong>CAE:</strong>
                        <asp:Label
                            ID="lblCAE"
                            runat="server" />
                    </p>

                    <p class="mb-0">
                        <strong>Fecha de Vto. de CAE:</strong>
                        <asp:Label
                            ID="lblVencimientoCAE"
                            runat="server" />
                    </p>

                </div>

            </div>

            <div class="mt-3">

                <asp:HyperLink
                    ID="hlPdf"
                    runat="server"
                    CssClass="btn btn-success btn-sm">
                    Descargar PDF
                </asp:HyperLink>

                <asp:HyperLink
                    ID="hlVolver"
                    runat="server"
                    NavigateUrl="~/Ventas.aspx"
                    CssClass="btn btn-secondary btn-sm ms-2">
                    Volver
                </asp:HyperLink>

            </div>

        </div>

    </div>

</asp:Content>