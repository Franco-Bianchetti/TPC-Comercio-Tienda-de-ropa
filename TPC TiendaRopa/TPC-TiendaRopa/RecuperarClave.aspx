<%@ Page Title="Recuperar contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperarClave.aspx.cs" Inherits="TPC_TiendaRopa.RecuperarClave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .tarjeta-formulario .form-control,
        .tarjeta-formulario .btn {
            max-width: 100%;
        }
    </style>

    <div class="d-flex justify-content-center mt-4">

        <div class="card shadow p-4 tarjeta-formulario" style="width: 100%; max-width: 400px;">

            <h4 class="text-center mb-2">Recuperar contraseña</h4>

            <p class="text-center text-muted mb-4">
                Ingresá tu usuario y tu DNI para elegir una contraseña nueva.
            </p>

            <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger py-2 d-block" Visible="false" />

            <div class="mb-3">
                <label>Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" MaxLength="30" autocomplete="off" />
            </div>

            <div class="mb-3">
                <label>DNI</label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" />
            </div>

            <div class="mb-3">
                <label>Contraseña nueva</label>
                <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="form-control" autocomplete="new-password" />
                <div class="form-text">Mínimo 8 caracteres y al menos un número.</div>
            </div>

            <div class="mb-3">
                <label>Repetir contraseña nueva</label>
                <asp:TextBox ID="txtRepetirClave" runat="server" TextMode="Password" CssClass="form-control" autocomplete="new-password" />
            </div>

            <div class="d-grid mt-3">
                <asp:Button ID="btnCambiar" runat="server" Text="Cambiar contraseña" CssClass="btn btn-dark" OnClick="btnCambiar_Click" />
            </div>

            <div class="text-center mt-3">
                <a href="Login.aspx">Volver al inicio de sesión</a>
            </div>

        </div>

    </div>

</asp:Content>