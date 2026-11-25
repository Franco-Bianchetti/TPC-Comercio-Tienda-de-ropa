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
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
    ErrorMessage="El nombre es obligatorio." CssClass="text-danger small d-block" Display="Dynamic" />
    <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombre"
    ValidationExpression="^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]{2,50}$" ErrorMessage="El nombre solo debe contener letras (mínimo 2)."
    CssClass="text-danger small d-block" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label>Apellido</label>

                <asp:TextBox
                    ID="txtApellido"
                    runat="server"
                    CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido"
    ErrorMessage="El apellido es obligatorio." CssClass="text-danger small d-block" Display="Dynamic" />
    <asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtApellido"
    ValidationExpression="^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]{2,50}$" ErrorMessage="El apellido solo debe contener letras (mínimo 2)."
    CssClass="text-danger small d-block" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label>DNI</label>

                <asp:TextBox
                    ID="txtDni"
                    runat="server"
                    CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvDni" runat="server" ControlToValidate="txtDni"
    ErrorMessage="El DNI es obligatorio." CssClass="text-danger small d-block" Display="Dynamic" />
    <asp:RegularExpressionValidator ID="revDni" runat="server" ControlToValidate="txtDni"
    ValidationExpression="^\d{8,10}$" ErrorMessage="El DNI debe contener entre 8 y 10 dígitos numéricos."
    CssClass="text-danger small d-block" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label>Teléfono</label>

                <asp:TextBox
                    ID="txtTelefono"
                    runat="server"
                    CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
    ErrorMessage="El teléfono es obligatorio." CssClass="text-danger small d-block" Display="Dynamic" />
<asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtTelefono"
    ValidationExpression="^\d{8,10}$" ErrorMessage="El teléfono debe tener entre 8 y 10 dígitos numéricos."
    CssClass="text-danger small d-block" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label>Email</label>

                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
    ErrorMessage="El email es obligatorio." CssClass="text-danger small d-block" Display="Dynamic" />
    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Ingrese un formato de email válido."
    CssClass="text-danger small d-block" Display="Dynamic" />
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