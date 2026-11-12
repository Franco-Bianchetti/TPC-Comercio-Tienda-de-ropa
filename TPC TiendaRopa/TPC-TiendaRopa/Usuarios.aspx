<%@ Page Title="Usuarios"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Usuarios.aspx.cs"
    Inherits="TPC_TiendaRopa.Usuarios" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Usuarios</h2>

    <asp:GridView ID="gvUsuarios"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvUsuarios_RowCommand">

        <Columns>

            <asp:BoundField
                DataField="Id"
                HeaderText="ID" />

            <asp:BoundField
                DataField="Nombre"
                HeaderText="Nombre" />

            <asp:BoundField
                DataField="Apellido"
                HeaderText="Apellido" />

            <asp:BoundField
                DataField="NombreUsuario"
                HeaderText="Usuario" />

            <asp:BoundField
                DataField="Dni"
                HeaderText="DNI" />

            <asp:BoundField
                DataField="Perfil"
                HeaderText="Perfil" />

            <asp:TemplateField HeaderText="Acciones">

                <ItemTemplate>

                    <a href="FormularioUsuario.aspx?id=<%# Eval("Id") %>"
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
                        OnClientClick="return confirm('¿Seguro que querés eliminar este usuario?');" />

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