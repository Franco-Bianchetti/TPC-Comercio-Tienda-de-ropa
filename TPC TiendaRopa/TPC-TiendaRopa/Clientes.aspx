<%@ Page Title="Clientes"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Clientes.aspx.cs"
    Inherits="TPC_TiendaRopa.Clientes" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Clientes</h2>

    <asp:Panel runat="server" DefaultButton="btnBuscar">

        <div class="card shadow-sm mb-3">

            <div class="card-header">
                Filtros
            </div>

            <div class="card-body">

                <div class="row">

                    <div class="col-md-8">

                        <label>Buscar</label>

                        <asp:TextBox
                            ID="txtBuscar"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Buscar por nombre, apellido, DNI, dirección o email..." />

                    </div>

                    <div class="col-md-2">

                        <label>&nbsp;</label>

                        <asp:Button
                            ID="btnBuscar"
                            runat="server"
                            Text="Buscar"
                            CssClass="btn btn-secondary w-100"
                            OnClick="btnBuscar_Click" />

                    </div>

                    <div class="col-md-2">

                        <label>&nbsp;</label>

                        <asp:Button
                            ID="btnLimpiar"
                            runat="server"
                            Text="Limpiar"
                            CssClass="btn btn-outline-secondary w-100"
                            OnClick="btnLimpiar_Click" />

                    </div>

                </div>

            </div>

        </div>

    </asp:Panel>

    <asp:GridView
        ID="gvClientes"
        runat="server"
        CssClass="table table-striped table-bordered"
        AutoGenerateColumns="false"
        OnRowCommand="gvClientes_RowCommand"
        OnRowDataBound="gvClientes_RowDataBound">

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
                DataField="Dni"
                HeaderText="DNI" />

            <asp:BoundField
                DataField="Direccion"
                HeaderText="Dirección" />

            <asp:BoundField
                DataField="Telefono"
                HeaderText="Teléfono" />

            <asp:BoundField
                DataField="Email"
                HeaderText="Email" />

            <asp:TemplateField
                HeaderText="Acciones">

                <ItemTemplate>

                    <asp:Panel ID="pnlActiva" runat="server" CssClass="d-inline">

                        <a href="FormularioCliente.aspx?id=<%# Eval("Id") %>"
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
                            OnClientClick="return confirm('¿Seguro que querés eliminar este cliente?');" />

                    </asp:Panel>

                    <asp:Panel ID="pnlInactiva" runat="server" CssClass="d-inline" Visible="false">

                        <asp:Button
                            ID="btnReactivarFila"
                            runat="server"
                            Text="Reactivar"
                            CssClass="btn btn-success btn-sm"
                            CommandName="Reactivar"
                            CommandArgument='<%# Eval("Id") %>'
                            OnClientClick="return confirm('¿Reactivar este cliente?');" />

                    </asp:Panel>

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <asp:Label ID="lblResultado" runat="server" CssClass="d-block mb-3" Visible="false" />

    <br />

    <asp:Button
        ID="btnNuevo"
        runat="server"
        Text="Nuevo"
        CssClass="btn btn-primary"
        OnClick="btnNuevo_Click" />

</asp:Content>