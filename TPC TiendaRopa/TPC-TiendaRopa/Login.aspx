<%@ Page Title="Incio de sesion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_TiendaRopa.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-center mt-5">

        <div class="card shadow p-4" style="min-width: 350px;">

            <h4 class="text-center mb-4">Iniciar sesión</h4>

            <div class="mb-3">
                <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="form-label" />
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblClave" runat="server" Text="Contraseña" CssClass="form-label" />
                <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su contraseña" />
            </div>

            <div class="d-grid mt-4">
                <asp:Button ID="btnIngresar" runat="server" Text="Iniciar sesión" CssClass="btn btn-dark" />
            </div>

        </div>

    </div>

</asp:Content>