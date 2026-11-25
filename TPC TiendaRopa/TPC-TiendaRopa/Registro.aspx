<%@ Page Title="Registrarse" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPC_TiendaRopa.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .tarjeta-formulario .form-control,
        .tarjeta-formulario .btn {
            max-width: 100%;
        }
    </style>

    <div class="d-flex justify-content-center mt-4">

        <div class="card shadow p-4 tarjeta-formulario" style="width: 100%; max-width: 400px;">

            <h4 class="text-center mb-4">Crear cuenta</h4>

            <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger py-2 d-block" Visible="false" />

            <div class="mb-3">
                <label>Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
            </div>

            <div class="mb-3">
                <label>Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" />
            </div>

            <div class="mb-3">
                <label>DNI</label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" MaxLength="10" />
            </div>

            <div class="mb-3">
                <label>Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" MaxLength="30" autocomplete="off" />
            </div>

            <div class="mb-3">
                <label>Contraseña</label>
                <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="form-control" autocomplete="new-password" />
                <div class="form-text">Mínimo 8 caracteres y al menos un número.</div>
            </div>

            <div class="mb-3">
                <label>Repetir contraseña</label>
                <asp:TextBox ID="txtRepetirClave" runat="server" TextMode="Password" CssClass="form-control" autocomplete="new-password" />
            </div>

            <div class="d-grid mt-3">
                <asp:Button ID="btnRegistrar" runat="server" Text="Crear cuenta" CssClass="btn btn-dark" OnClick="btnRegistrar_Click" />
            </div>

            <div class="text-center mt-3">
                <a href="Login.aspx">Volver al inicio de sesión</a>
            </div>

        </div>

    </div>

</asp:Content>