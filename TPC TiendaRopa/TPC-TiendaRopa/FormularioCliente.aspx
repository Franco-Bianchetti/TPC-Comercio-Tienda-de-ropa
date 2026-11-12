<%@ Page Title="Cliente"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="FormularioCliente.aspx.cs"
    Inherits="TPC_TiendaRopa.FormularioCliente" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>
        <asp:Label
            ID="lblTitulo"
            runat="server"
            Text="Nuevo cliente" />
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
                <label>Teléfono</label>

                <asp:TextBox
                    ID="txtTelefono"
                    runat="server"
                    CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label>Email</label>

                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="form-control" />
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

            <a href="Clientes.aspx"
               class="btn btn-secondary">
                Cancelar
            </a>

        </div>

    </div>

</asp:Content>