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