<%@ Page Title="Inicio de sesión"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="TPC_TiendaRopa.Login" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="d-flex justify-content-center mt-5">

        <div class="card shadow p-4"
             style="width: 330px;">

            <h4 class="text-center mb-4">
                Iniciar sesión
            </h4>

            <asp:Label
                ID="lblExito"
                runat="server"
                CssClass="text-success text-center d-block mb-3 small"
                Text="¡Cuenta creada! Ya podés iniciar sesión."
                Visible="false" />

            <asp:Label
                ID="lblError"
                runat="server"
                CssClass="text-danger text-center d-block mb-3 small"
                Visible="false">
            </asp:Label>

            <div class="mb-3">

                <asp:Label
                    ID="lblUsuario"
                    runat="server"
                    Text="Usuario"
                    CssClass="form-label" />

                <asp:TextBox
                    ID="txtUsuario"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Ingrese su usuario" />

            </div>

            <div class="mb-3">

                <asp:Label
                    ID="lblClave"
                    runat="server"
                    Text="Contraseña"
                    CssClass="form-label" />

                <asp:TextBox
                    ID="txtClave"
                    runat="server"
                    TextMode="Password"
                    CssClass="form-control"
                    placeholder="Ingrese su contraseña" />

            </div>

            <div class="d-grid mt-4">

                <asp:Button
                    ID="btnIngresar"
                    runat="server"
                    Text="Iniciar sesión"
                    CssClass="btn btn-dark"
                    OnClick="btnIngresar_Click" />

            </div>

            <div class="text-center mt-3">

                <a href="Registro.aspx">¿No tenés cuenta? Registrate</a>

            </div>

            <div class="text-center mt-2">

                <a href="RecuperarClave.aspx">¿Olvidaste tu contraseña?</a>

            </div>

        </div>

    </div>

</asp:Content>