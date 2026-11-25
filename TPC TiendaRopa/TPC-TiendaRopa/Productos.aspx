<%@ Page Title="Productos"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Productos.aspx.cs"
    Inherits="TPC_TiendaRopa.Productos" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Productos</h2>

    <asp:Panel runat="server" DefaultButton="btnBuscar">

        <div class="card shadow-sm mb-3">

            <div class="card-header">
                <strong>Filtros de búsqueda</strong>
            </div>

            <div class="card-body">

                <div class="row g-3">

                    <div class="col-md-4">
                        <label class="form-label">Código o nombre</label>
                        <asp:TextBox
                            ID="txtBuscar"
                            runat="server"
                            CssClass="form-control"
                            placeholder="REM001, remera..." />
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Marca</label>
                        <asp:DropDownList
                            ID="ddlMarca"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Proveedor</label>
                        <asp:DropDownList
                            ID="ddlProveedor"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>
                    </div>

                </div>

                <style>
                    .filtro-stock-bajo {
                        gap: 0.55rem;
                        padding-top: 0.25rem;
                    }
                    .filtro-stock-bajo input[type="checkbox"] {
                        width: 1.1rem;
                        height: 1.1rem;
                        margin: 0;
                        cursor: pointer;
                    }
                    .filtro-stock-bajo label {
                        margin: 0;
                        font-size: 1rem;
                        line-height: 1.5;
                        cursor: pointer;
                    }
                </style>

                <div class="row g-3 mt-1 align-items-end">

                    <div class="col-md-2">
                        <label class="form-label">Talle</label>
                        <asp:TextBox
                            ID="txtTalle"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Ej. M, XL, 42" />
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">Color</label>
                        <asp:TextBox
                            ID="txtColor"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Ej. Negro, azul..." />
                    </div>

                    <div class="col-md-3 d-flex align-items-center ps-md-3" style="height: 38px;">
                        <asp:CheckBox
                            ID="chkStockBajo"
                            runat="server"
                            CssClass="d-inline-flex align-items-center filtro-stock-bajo"
                            Text="Productos con stock bajo" />
                    </div>

                    <div class="col-md-2">
                        <asp:Button
                            ID="btnBuscar"
                            runat="server"
                            Text="Buscar"
                            CssClass="btn btn-secondary w-100"
                            OnClick="btnBuscar_Click" />
                    </div>

                    <div class="col-md-2">
                        <asp:Button
                            ID="btnLimpiar"
                            runat="server"
                            Text="Limpiar"
                            CssClass="btn btn-outline-secondary w-100"
                            OnClick="btnLimpiar_Click" />
                    </div>

                </div>

            </div>

        </div>

    </asp:Panel>

    <asp:GridView
        ID="gvProductos"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvProductos_RowCommand"
        OnRowDataBound="gvProductos_RowDataBound">

        <Columns>

            <asp:BoundField
                DataField="Codigo"
                HeaderText="Código" />

            <asp:BoundField
                DataField="Nombre"
                HeaderText="Nombre" />

            <asp:TemplateField HeaderText="Marca">
                <ItemTemplate>
                    <%# Eval("Marca.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Proveedor">
                <ItemTemplate>
                    <%# Eval("Proveedores[0].RazonSocial") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Tipo">
                <ItemTemplate>
                    <%# Eval("Tipo.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField
                DataField="Talle"
                HeaderText="Talle" />

            <asp:BoundField
                DataField="Color"
                HeaderText="Color" />

            <asp:BoundField
                DataField="StockActual"
                HeaderText="Stock" />

            <asp:BoundField
                DataField="EstadoStock"
                HeaderText="Estado" />

            <asp:BoundField
                DataField="PrecioCompraMostrado"
                HeaderText="Precio Compra" />

            <asp:BoundField
                DataField="PrecioVentaMostrado"
                HeaderText="Precio Venta" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>

                    <asp:Panel
                        ID="pnlActiva"
                        runat="server">

                        <a href="FormularioProducto.aspx?id=<%# Eval("Id") %>"
                           class="btn btn-warning btn-sm">
                            Editar
                        </a>

                        <asp:Button
                            ID="btnEliminarFila"
                            runat="server"
                            Text="Eliminar"
                            CssClass="btn btn-danger btn-sm"
                            CommandName="Eliminar"
                            CommandArgument='<%# Eval("Id") %>'
                            OnClientClick="return confirm('¿Seguro que querés eliminar este producto?');" />

                    </asp:Panel>

                    <asp:Panel
                        ID="pnlInactiva"
                        runat="server"
                        Visible="false">

                        <asp:Button
                            ID="btnReactivarFila"
                            runat="server"
                            Text="Reactivar"
                            CssClass="btn btn-success btn-sm"
                            CommandName="Reactivar"
                            CommandArgument='<%# Eval("Id") %>'
                            OnClientClick="return confirm('¿Seguro que querés reactivar este producto?');" />

                    </asp:Panel>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <asp:Label
        ID="lblResultado"
        runat="server"
        CssClass="d-block mb-3"
        Visible="false" />

    <br />

    <asp:Button
        ID="btnNuevo"
        runat="server"
        Text="Nuevo"
        CssClass="btn btn-primary"
        OnClick="btnNuevo_Click" />

</asp:Content>