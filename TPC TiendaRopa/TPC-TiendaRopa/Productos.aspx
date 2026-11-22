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
                Filtros
            </div>

            <div class="card-body">

                <div class="row">

                    <div class="col-md-3">

                        <label>Código o nombre</label>

                        <asp:TextBox
                            ID="txtBuscar"
                            runat="server"
                            CssClass="form-control"
                            placeholder="REM001, remera..." />

                    </div>

                    <div class="col-md-2">

                        <label>Marca</label>

                        <asp:DropDownList
                            ID="ddlMarca"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-2">

                        <label>Tipo</label>

                        <asp:DropDownList
                            ID="ddlTipo"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-1">

                        <label>Talle</label>

                        <asp:TextBox
                            ID="txtTalle"
                            runat="server"
                            CssClass="form-control" />

                    </div>

                    <div class="col-md-2">

                        <label>Color</label>

                        <asp:TextBox
                            ID="txtColor"
                            runat="server"
                            CssClass="form-control" />

                    </div>

                    <div class="col-md-1">

                        <label>&nbsp;</label>

                        <asp:Button
                            ID="btnBuscar"
                            runat="server"
                            Text="Buscar"
                            CssClass="btn btn-secondary w-100"
                            OnClick="btnBuscar_Click" />

                    </div>

                    <div class="col-md-1">

                        <label>&nbsp;</label>

                        <asp:Button
                            ID="btnLimpiar"
                            runat="server"
                            Text="Limpiar"
                            CssClass="btn btn-outline-secondary w-100"
                            OnClick="btnLimpiar_Click" />

                    </div>

                </div>

                <div class="mt-3">

                    <asp:CheckBox
                        ID="chkStockBajo"
                        runat="server"
                        Text=" Solo productos con stock bajo" />

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

    <asp:TemplateField
        HeaderText="Marca">

        <ItemTemplate>
            <%# Eval("Marca.Nombre") %>
        </ItemTemplate>

    </asp:TemplateField>

    <asp:TemplateField
        HeaderText="Proveedor">

        <ItemTemplate>
            <%# Eval("Proveedores[0].RazonSocial") %>
        </ItemTemplate>

    </asp:TemplateField>

    <asp:TemplateField
        HeaderText="Tipo">

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

    <asp:TemplateField
        HeaderText="Acciones">

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