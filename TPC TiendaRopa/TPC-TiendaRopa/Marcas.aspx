<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Marcas.aspx.cs"
    Inherits="TPC_TiendaRopa.Marcas" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Marcas</h2>

    <asp:GridView ID="gvMarcas"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvMarcas_RowCommand">

        <Columns>

            <asp:BoundField
                DataField="Id"
                HeaderText="ID" />

            <asp:BoundField
                DataField="Nombre"
                HeaderText="Nombre" />

            <asp:TemplateField HeaderText="Acciones">

                <ItemTemplate>

                    <a href="FormularioMarca.aspx?id=<%# Eval("Id") %>"
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
                        OnClientClick="return confirm('¿Seguro que querés eliminar esta marca?');" />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <br />

    <asp:Button ID="btnNuevo"
        runat="server"
        Text="Nuevo"
        CssClass="btn btn-primary"
        OnClick="btnNuevo_Click" />

</asp:Content>