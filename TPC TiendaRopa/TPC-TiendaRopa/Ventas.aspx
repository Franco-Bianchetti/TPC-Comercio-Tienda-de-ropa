<%@ Page Title="Ventas"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Ventas.aspx.cs"
    Inherits="TPC_TiendaRopa.Ventas" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Ventas</h2>

    <asp:Panel
        ID="pnlFiltros"
        runat="server"
        DefaultButton="btnBuscar">

        <div class="card shadow-sm mb-3">

            <div class="card-header">
                Filtros
            </div>

            <div class="card-body">

                <div class="row g-3">

                    <div class="col-md-3">

                        <label>Cliente</label>

                        <asp:DropDownList
                            ID="ddlCliente"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-3">

                        <label>Vendedor</label>

                        <asp:DropDownList
                            ID="ddlVendedor"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-2">

                        <label>Fecha Desde</label>

                        <asp:TextBox
                            ID="txtFechaDesde"
                            runat="server"
                            CssClass="form-control"
                            TextMode="Date">
                        </asp:TextBox>

                    </div>

                    <div class="col-md-2">

                        <label>Fecha Hasta</label>

                        <asp:TextBox
                            ID="txtFechaHasta"
                            runat="server"
                            CssClass="form-control"
                            TextMode="Date">
                        </asp:TextBox>

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

            </div>

        </div>

    </asp:Panel>

    <asp:Label
        ID="lblResultado"
        runat="server"
        Visible="false">
    </asp:Label>

    <asp:GridView
        ID="gvVentas"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvVentas_RowCommand">

        <Columns>

            <asp:BoundField
                DataField="Fecha"
                HeaderText="Fecha"
                DataFormatString="{0:dd/MM/yyyy}" />

            <asp:BoundField
                DataField="Fecha"
                HeaderText="Hora"
                DataFormatString="{0:HH:mm}" />

            <asp:TemplateField HeaderText="Cliente">

                <ItemTemplate>

                    <%# Eval("Cliente.Nombre") %>
                    <%# Eval("Cliente.Apellido") %>

                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="Vendedor">

                <ItemTemplate>

                    <%# Eval("Vendedor.Nombre") %>
                    <%# Eval("Vendedor.Apellido") %>

                </ItemTemplate>

            </asp:TemplateField>

            <asp:BoundField
                DataField="Total"
                HeaderText="Total"
                DataFormatString="{0:C}" />

            <asp:TemplateField HeaderText="Acciones">

                <ItemTemplate>

                    <asp:Button
                        ID="btnDetalle"
                        runat="server"
                        Text="Ver detalle"
                        CssClass="btn btn-info btn-sm"
                        CommandName="Detalle"
                        CommandArgument='<%# Eval("Id") %>' />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <br />

    <asp:Button
        ID="btnNuevaVenta"
        runat="server"
        Text="Registrar Venta"
        CssClass="btn btn-primary"
        OnClick="btnNuevaVenta_Click" />

</asp:Content>