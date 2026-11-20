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

    
<div class="row mb-3">

    <div class="col-md-4">

        <asp:DropDownList
            ID="ddlProveedor"
            runat="server"
            CssClass="form-select">
        </asp:DropDownList>

    </div>

    <div class="col-md-2">

        <asp:Button
            ID="btnBuscar"
            runat="server"
            Text="Buscar"
            CssClass="btn btn-secondary"
            OnClick="btnBuscar_Click" />

    </div>

    <div class="col-md-2">

        <asp:Button
            ID="btnLimpiar"
            runat="server"
            Text="Limpiar"
            CssClass="btn btn-outline-secondary"
            OnClick="btnLimpiar_Click" />

    </div>

</div>

    <asp:GridView
        ID="gvCompras"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false">

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

        </Columns>

    </asp:GridView>

    <br />

    <asp:Button
        ID="btnNuevaCompra"
        runat="server"
        Text="Nueva Compra"
        CssClass="btn btn-primary"
        OnClick="btnNuevaCompra_Click" />

</asp:Content>