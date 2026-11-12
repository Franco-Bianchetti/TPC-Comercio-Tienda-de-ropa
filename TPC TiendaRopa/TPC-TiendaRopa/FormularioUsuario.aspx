<%@ Page Title="Usuario"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="FormularioUsuario.aspx.cs"
    Inherits="TPC_TiendaRopa.FormularioUsuario" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>
        <asp:Label
            ID="lblTitulo"
            runat="server"
            Text="Nuevo usuario" />
    </h2>

    <div class="row">

        <div class="col-md-6">

            <div class="mb-3">
                <label>Nombre</label>

                <asp:TextBox
                    ID="txtNombre"
                    runat="server"
                    CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label>Apellido</label>

                <asp:TextBox
                    ID="txtApellido"
                    runat="server"
                    CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label>DNI</label>

                <asp:TextBox
                    ID="txtDni"
                    runat="server"
                    CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label>Usuario</label>

                <asp:TextBox
                    ID="txtUsuario"
                    runat="server"
                    CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label>Contraseña</label>

                <asp:TextBox
                    ID="txtClave"
                    runat="server"
                    TextMode="Password"
                    CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label>Perfil</label>

                <asp:DropDownList
                    ID="ddlPerfil"
                    runat="server"
                    CssClass="form-select">

                    <asp:ListItem Text="Administrador"
                        Value="Administrador" />

                    <asp:ListItem Text="Vendedor"
                        Value="Vendedor" />

                </asp:DropDownList>
            </div>

            <div class="mb-3">

                <asp:Label
                    ID="lblError"
                    runat="server"
                    CssClass="text-danger"
                    Visible="false" />

            </div>

            <asp:Button
                ID="btnGuardar"
                runat="server"
                Text="Guardar"
                CssClass="btn btn-primary"
                OnClick="btnGuardar_Click" />

            <a href="Usuarios.aspx"
               class="btn btn-secondary">
                Cancelar
            </a>

        </div>

    </div>

</asp:Content>