<%@ Page Title="Registrar Venta"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="FormularioVenta.aspx.cs"
    Inherits="TPC_TiendaRopa.FormularioVenta" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Registro de Venta</h2>

    <asp:Label
        ID="lblError"
        runat="server"
        CssClass="text-danger"
        Visible="false">
    </asp:Label>

    <asp:Panel
        ID="pnlVenta"
        runat="server"
        DefaultButton="btnAgregar">

        <div class="card shadow-sm mb-4">

            <div class="card-header bg-secondary text-white">
                <h5 class="mb-0">
                    Datos de la Venta
                </h5>
            </div>

            <div class="card-body">

                <div class="row g-3 mb-4">

                    <div class="col-md-4">

                        <label>Cliente</label>

                        <asp:DropDownList
                            ID="ddlCliente"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-4">

                        <label>Tipo Factura</label>

                        <asp:DropDownList
                            ID="ddlTipoFactura"
                            runat="server"
                            CssClass="form-select">

                            <asp:ListItem
                                Text="Seleccione tipo de factura"
                                Value="" />

                            <asp:ListItem
                                Text="FACTURA A"
                                Value="FACTURA A" />

                            <asp:ListItem
                                Text="FACTURA B"
                                Value="FACTURA B" />

                            <asp:ListItem
                                Text="FACTURA C"
                                Value="FACTURA C" />

                            <asp:ListItem
                                Text="PRESUPUESTO"
                                Value="PRESUPUESTO" />

                        </asp:DropDownList>

                    </div>

                    <div class="col-md-4">

                        <label>Forma de Pago</label>

                            <asp:DropDownList
                                ID="ddlFormaPago"
                                runat="server"
                                CssClass="form-select"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">

                            <asp:ListItem
                                Text="Seleccione forma de pago"
                                Value="" />

                            <asp:ListItem
                                Text="EFECTIVO"
                                Value="EFECTIVO" />

                            <asp:ListItem
                                Text="TARJETA DÉBITO"
                                Value="TARJETA DÉBITO" />

                            <asp:ListItem
                                Text="TARJETA CRÉDITO"
                                Value="TARJETA CRÉDITO" />

                            <asp:ListItem
                                Text="TRANSFERENCIA"
                                Value="TRANSFERENCIA" />

                        </asp:DropDownList>

                    </div>

                </div>

                <hr />

                <h6 class="mb-3">
                    Detalle de la Venta
                </h6>

                <div class="row g-3">

                    <div class="col-md-4">

                        <label>Producto</label>

                        <asp:DropDownList
                            ID="ddlProducto"
                            runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-4">

                        <label>Precio Unitario</label>

                        <asp:TextBox
                            ID="txtPrecioUnitario"
                            runat="server"
                            CssClass="form-control"
                            ReadOnly="true"
                            Enabled="false">
                        </asp:TextBox>

                    </div>

                    <div class="col-md-4">

                        <label>Cantidad</label>

                        <asp:TextBox
                            ID="txtCantidad"
                            runat="server"
                            CssClass="form-control">
                        </asp:TextBox>

                    </div>

                </div>

                <div class="row g-3 mt-2 mb-4">

                    <div class="col-md-4">

                        <label>Stock Disponible</label>

                        <asp:TextBox
                            ID="txtStock"
                            runat="server"
                            CssClass="form-control"
                            ReadOnly="true"
                            Enabled="false">
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
                        CommandArgument='<%# Container.DataItemIndex %>' />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <div class="card border-primary mt-3 mb-3">

        <div class="card-body">
                <h5>

                    Subtotal Productos:

                    <asp:Label
                        ID="lblSubtotal"
                        runat="server"
                        Text="$0,00">
                    </asp:Label>

                </h5>


            <h5>

                Descuento:

                <asp:Label
                    ID="lblDescuento"
                    runat="server"
                    Text="$0,00">
                </asp:Label>

            </h5>

            <h4 class="text-primary mb-0">

                Total Final:

                <asp:Label
                    ID="lblTotal"
                    runat="server"
                    Text="$0,00">
                </asp:Label>

            </h4>

        </div>

    </div>

    <div class="mt-3">
        <asp:Button
            ID="btnConfirmar"
            runat="server"
            Text="Confirmar venta"
            CssClass="btn btn-primary"
            OnClick="btnConfirmar_Click" />

        <asp:Button
            ID="btnVaciarVenta"
            runat="server"
            Text="Vaciar venta"
            CssClass="btn btn-danger"
            OnClick="btnVaciarVenta_Click" />

        <a href="Ventas.aspx"
           class="btn btn-secondary">
            Volver
        </a>

    </div>

</asp:Content>