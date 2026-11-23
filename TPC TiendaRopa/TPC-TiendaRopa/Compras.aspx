<%@ Page Title="Compras"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Compras.aspx.cs"
    Inherits="TPC_TiendaRopa.Compras" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Compras</h2>

<asp:Panel
    ID="pnlFiltros"
    runat="server"
    DefaultButton="btnBuscar">


<div class="card shadow-sm mb-3">

    <div class="card-header">
        Filtros
    </div>

    <div class="card-body">

        <div class="row">

            <div class="col-md-3">

                <label>Proveedor</label>

                <asp:DropDownList
                    ID="ddlProveedor"
                    runat="server"
                    CssClass="form-select">
                </asp:DropDownList>

            </div>

            <div class="col-md-3">

                <label>Comprobante</label>

                <asp:TextBox
                    ID="txtComprobante"
                    runat="server"
                    CssClass="form-control"
                    placeholder="0001-00001234">
                </asp:TextBox>

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

    <asp:GridView
        ID="gvCompras"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvCompras_RowCommand">

        <Columns>

            <asp:BoundField
                DataField="Fecha"
                HeaderText="Fecha"
                DataFormatString="{0:dd/MM/yyyy}" />

            <asp:BoundField
                DataField="Fecha"
                HeaderText="Hora"
                DataFormatString="{0:HH:mm}" />

            <asp:TemplateField
                HeaderText="Usuario">

                <ItemTemplate>
                    <%# Eval("Usuario.Nombre") %>
                    <%# Eval("Usuario.Apellido") %>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField
                HeaderText="Proveedor">

                <ItemTemplate>
                    <%# Eval("Proveedor.RazonSocial") %>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:BoundField
                DataField="Total"
                HeaderText="Total"
                DataFormatString="{0:C}" />

            <asp:BoundField
                DataField="TipoComprobante"
                HeaderText="Tipo" />

            <asp:BoundField
                DataField="NumeroComprobante"
                HeaderText="Comprobante" />

            <asp:TemplateField HeaderText="Acciones">

                <ItemTemplate>

                    <asp:Button
                        ID="btnVerDetalle"
                        runat="server"
                        Text="Ver detalle"
                        CssClass="btn btn-info btn-sm"
                        CommandName="VerDetalle"
                        CommandArgument='<%# Eval("Id") %>' />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>

        <asp:Label ID="lblResultado" runat="server" CssClass="d-block mb-3" Visible="false" />

    <br />

    <asp:Button
        ID="btnNuevaCompra"
        runat="server"
        Text="Registrar Compra"
        CssClass="btn btn-primary"
        OnClick="btnNuevaCompra_Click" />

</asp:Content>