<%@ Page Title="Detalle de Compra"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CompraDetalle.aspx.cs"
    Inherits="TPC_TiendaRopa.CompraDetalle" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Detalle de Compra</h2>

    <div class="card shadow-sm mb-4">

        <div class="card-header bg-secondary text-white">
            <h5 class="mb-0">
                Información de la Compra
            </h5>
        </div>

        <div class="card-body">

            <div class="row mb-3">

                <div class="col-md-3">
                    <strong>Fecha</strong><br />
                    <asp:Label
                        ID="lblFecha"
                        runat="server" />
                </div>

                <div class="col-md-3">
                    <strong>Usuario</strong><br />
                    <asp:Label
                        ID="lblUsuario"
                        runat="server" />
                </div>

                <div class="col-md-3">
                    <strong>Proveedor</strong><br />
                    <asp:Label
                        ID="lblProveedor"
                        runat="server" />
                </div>

                <div class="col-md-3">
                    <strong>Total</strong><br />
                    <asp:Label
                        ID="lblImporteTotal"
                        runat="server"
                        CssClass="fw-bold text-primary" />
                </div>

            </div>

            <div class="row">

                <div class="col-md-6">
                    <strong>Tipo Comprobante</strong><br />
                    <asp:Label
                        ID="lblTipoComprobante"
                        runat="server" />
                </div>

                <div class="col-md-6">
                    <strong>N° Comprobante</strong><br />
                    <asp:Label
                        ID="lblNumeroComprobante"
                        runat="server" />
                </div>

            </div>

        </div>

    </div>

    <div class="card shadow-sm">

        <div class="card-header bg-light">

            <h5 class="mb-0">
                Productos Comprados
            </h5>

        </div>

        <div class="card-body">

            <asp:GridView
                ID="gvDetalle"
                runat="server"
                CssClass="table table-striped table-bordered"
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

        </div>

    </div>

    <div class="mt-3">

        <a href="Compras.aspx"
           class="btn btn-secondary">
            Volver
        </a>

    </div>

</asp:Content>