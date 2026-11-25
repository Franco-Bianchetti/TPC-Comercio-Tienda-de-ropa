<%@ Page Title="Nueva Compra"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="FormularioCompra.aspx.cs"
    Inherits="TPC_TiendaRopa.FormularioCompra" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>
        <asp:Label
            ID="lblTitulo"
            runat="server"
            Text="Registro de Compra">
        </asp:Label>
    </h2>

    <asp:Label
        ID="lblError"
        runat="server"
        CssClass="text-danger"
        Visible="false">
    </asp:Label>

    <asp:Panel
        ID="pnlCompra"
        runat="server"
        DefaultButton="btnAgregar">

        <div class="card shadow-sm mb-4">

            <div class="card-header bg-secondary text-white">
                <h5 class="mb-0">
                    Datos de la Factura
                </h5>
            </div>

            <div class="card-body">

                <div class="row g-3 mb-4">

                    <div class="col-md-4">

                        <label>Proveedor</label>

                        <asp:DropDownList
                            ID="ddlProveedor"
                            runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-4">

                        <label>Comprobante</label>

                        <asp:DropDownList
                            ID="ddlTipoComprobante"
                            runat="server"
                            CssClass="form-select">

                            <asp:ListItem
                                Text="Seleccione comprobante"
                                Value="" />

                            <asp:ListItem
                                Text="Factura A"
                                Value="FACTURA A" />

                            <asp:ListItem
                                Text="Factura B"
                                Value="FACTURA B" />

                            <asp:ListItem
                                Text="Factura C"
                                Value="FACTURA C" />

                        </asp:DropDownList>

                    </div>

                    <div class="col-md-4">

                        <label>N° Comprobante</label>

                        <asp:TextBox
                            ID="txtNumeroComprobante"
                            runat="server"
                            CssClass="form-control"
                            placeholder="0001-00001234">
                        </asp:TextBox>

                    </div>

                </div>

                <hr />

                <h6 class="mb-3">
                    Detalle de la Compra
                </h6>

                <div class="row g-3 mb-4">

                    <div class="col-md-4">

                        <label>Producto</label>

                        <asp:DropDownList
                            ID="ddlProducto"
                            runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                        </asp:DropDownList>

                        <small class="text-muted d-block mt-1">
                            <asp:Label
                                ID="lblStockActual"
                                runat="server"
                                Text="Stock actual: -">
                            </asp:Label>
                        </small>

                    </div>

                    <div class="col-md-4">

                        <label>Cantidad</label>

                        <asp:TextBox
                            ID="txtCantidad"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Cantidad">
                        </asp:TextBox>

                    </div>

                    <div class="col-md-4">

                        <label>Precio Compra</label>

                        <asp:TextBox
                            ID="txtPrecioCompra"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Precio">
                        </asp:TextBox>

                    </div>

                </div>

                <div class="row mb-3 justify-content-center">

                    <div class="col-md-3">

                        <asp:Button
                            ID="btnAgregar"
                            runat="server"
                            Text="Agregar"
                            CssClass="btn btn-secondary w-100"
                            OnClick="btnAgregar_Click" />

                    </div>

                    <div class="col-md-3">

                        <asp:Button
                            ID="btnCancelarEdicion"
                            runat="server"
                            Text="Cancelar edición"
                            CssClass="btn btn-warning w-100"
                            Enabled="false"
                            OnClick="btnCancelarEdicion_Click" />

                    </div>

                </div>
            </div>

        </div>

    </asp:Panel>

    <asp:GridView
        ID="gvDetalle"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvDetalle_RowCommand">

        <Columns>

            <asp:BoundField
                DataField="NombreProducto"
                HeaderStyle-Width="45%"
                HeaderText="Producto" />

            <asp:BoundField
                DataField="Cantidad"
                HeaderStyle-Width="15%"
                HeaderText="Cantidad" />

            <asp:BoundField
                DataField="PrecioCompra"
                HeaderStyle-Width="20%"
                HeaderText="Precio Compra"
                DataFormatString="{0:N2}" />

            <asp:BoundField
                DataField="Subtotal"
                HeaderStyle-Width="20%"
                HeaderText="Subtotal"
                DataFormatString="{0:N2}" />

            <asp:TemplateField HeaderText="Acciones">

                <ItemTemplate>

                    <asp:Button
                        ID="btnEditar"
                        runat="server"
                        Text="Editar"
                        CssClass="btn btn-warning btn-sm"
                        CommandName="Editar"
                        CommandArgument='<%# Container.DataItemIndex %>' />

                    <asp:Button
                        ID="btnEliminar"
                        runat="server"
                        Text="Eliminar"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Eliminar"
                        CommandArgument='<%# Container.DataItemIndex %>'
                        OnClientClick="return confirm('¿Seguro que desea quitar este producto?');" />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <div class="card border-primary mt-3 mb-3">

        <div class="card-body">

            <h4 class="text-primary mb-0">

                Total:
                <asp:Label
                    ID="lblTotal"
                    runat="server"
                    Text="$0">
                </asp:Label>

            </h4>

        </div>

    </div>

    <div class="mt-3">

        <asp:Button
            ID="btnConfirmar"
            runat="server"
            Text="Confirmar compra"
            CssClass="btn btn-primary"
            OnClick="btnConfirmar_Click" />

        <asp:Button
            ID="btnVaciarCompra"
            runat="server"
            Text="Vaciar compra"
            CssClass="btn btn-danger"
            OnClick="btnVaciarCompra_Click"
            OnClientClick="return confirm('¿Desea eliminar todos los productos cargados?');" />

        <a href="Compras.aspx"
           class="btn btn-secondary">
            Volver
        </a>

    </div>

</asp:Content>