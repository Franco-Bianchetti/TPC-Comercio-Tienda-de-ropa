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
            Text="Nueva Compra">
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

        <div class="row g-2 mb-3">

            <div class="col-md-3">

                <label>Proveedor</label>

                <asp:DropDownList
                    ID="ddlProveedor"
                    runat="server"
                    CssClass="form-select">
                </asp:DropDownList>

            </div>

            <div class="col-md-3">

                <label>Producto</label>

                <asp:DropDownList
                    ID="ddlProducto"
                    runat="server"
                    CssClass="form-select">
                </asp:DropDownList>

            </div>

            <div class="col-md-2">

                <label>Cantidad</label>

                <asp:TextBox
                    ID="txtCantidad"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Cantidad">
                </asp:TextBox>

            </div>

            <div class="col-md-2">

                <label>Precio Compra</label>

                <asp:TextBox
                    ID="txtPrecioCompra"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Precio compra">
                </asp:TextBox>

            </div>

            <div class="col-md-2">

                <label>&nbsp;</label>

                <asp:Button
                    ID="btnAgregar"
                    runat="server"
                    Text="Agregar"
                    CssClass="btn btn-secondary w-100"
                    OnClick="btnAgregar_Click" />

                <asp:Button
                    ID="btnCancelarEdicion"
                    runat="server"
                    Text="Cancelar edición"
                    CssClass="btn btn-warning w-100 mt-2"
                    Visible="false"
                    OnClick="btnCancelarEdicion_Click" />

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
                DataField="PrecioCompra"
                HeaderText="Precio Compra"
                DataFormatString="{0:N2}" />

            <asp:BoundField
                DataField="Subtotal"
                HeaderText="Subtotal"
                DataFormatString="{0:N2}" />

            <asp:TemplateField
                HeaderText="Acciones">

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

    <div class="mt-3">

        <h5>
            Total:
            <asp:Label
                ID="lblTotal"
                runat="server"
                Text="$0">
            </asp:Label>
        </h5>

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