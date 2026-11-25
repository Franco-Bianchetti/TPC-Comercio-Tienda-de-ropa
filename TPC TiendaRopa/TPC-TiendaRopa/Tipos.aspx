<%@ Page Title="Tipos" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Tipos.aspx.cs"
    Inherits="TPC_TiendaRopa.Tipos" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Tipos</h2>

    <asp:GridView ID="gvTipos"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvTipos_RowCommand">

        <Columns>

            <asp:BoundField
                DataField="Id"
                HeaderText="ID" />

            <asp:BoundField
                DataField="Nombre"
                HeaderText="Nombre" />

            <asp:TemplateField HeaderText="Acciones">

                <ItemTemplate>

                    <a href="FormularioTipo.aspx?id=<%# Eval("Id") %>"
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
                        OnClientClick="return confirm('¿Seguro que querés eliminar este tipo?');" />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <br />

    <asp:Button
        ID="btnNuevo"
        runat="server"
        Text="Nuevo"
        CssClass="btn btn-primary"
        OnClick="btnNuevo_Click" />

</asp:Content>