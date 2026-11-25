<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPC_TiendaRopa.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="alert alert-danger mt-4" role="alert" style="max-width: 650px;">
        <h4 class="alert-heading">Hubo un problema</h4>
        <p>
            <asp:Label ID="lblMensajeError" runat="server" Text="Ha ocurrido un error inesperado en la aplicación." />
        </p>
        <hr />
        <a href="Default.aspx" class="btn btn-outline-danger btn-sm">Volver al inicio</a>
    </div>

</asp:Content>
