<%@ Page Title="Marca"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="FormularioMarca.aspx.cs"
    Inherits="TPC_TiendaRopa.FormularioMarca" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>
        <asp:Label
            ID="lblTitulo"
            runat="server"
            Text="Nueva marca" />
    </h2>

    <asp:Panel
        ID="pnlFormulario"
        runat="server"
        DefaultButton="btnGuardar">

        <div class="mb-3"
             style="max-width:400px;">

            <asp:Label
                ID="lblNombre"
                runat="server"
                Text="Nombre de la marca"
                CssClass="form-label"
                AssociatedControlID="txtNombre" />

            <asp:TextBox
                ID="txtNombre"
                runat="server"
                CssClass="form-control"
                MaxLength="100" />

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

        <asp:HyperLink
            ID="hlCancelar"
            runat="server"
            NavigateUrl="~/Marcas.aspx"
            CssClass="btn btn-secondary">
            Cancelar
        </asp:HyperLink>

    </asp:Panel>

</asp:Content>