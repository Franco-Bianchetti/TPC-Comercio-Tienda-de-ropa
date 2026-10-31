<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Proveedores.aspx.cs"
    Inherits="TPC_TiendaRopa.Proveedores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Proveedores</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>

<form id="form1" runat="server">

    <div class="container">

        <h2>Proveedores</h2>

        <asp:GridView
            ID="gvProveedores"
            runat="server"
            CssClass="table table-striped table-bordered"
            AutoGenerateColumns="false">

            <Columns>

                <asp:BoundField
                    DataField="Id"
                    HeaderText="ID" />

                <asp:BoundField
                    DataField="RazonSocial"
                    HeaderText="Razón Social" />

                <asp:BoundField
                    DataField="Cuit"
                    HeaderText="CUIT" />

                <asp:BoundField
                    DataField="Telefono"
                    HeaderText="Teléfono" />

                <asp:BoundField
                    DataField="Email"
                    HeaderText="Email" />

            </Columns>

        </asp:GridView>

        <br />

       <br />

<asp:Button
    ID="btnNuevo"
    runat="server"
    Text="Nuevo"
    CssClass="btn btn-primary" />

&nbsp;

<asp:Button
    ID="btnEditar"
    runat="server"
    Text="Editar"
    CssClass="btn btn-warning" />

&nbsp;

<asp:Button
    ID="btnEliminar"
    runat="server"
    Text="Eliminar"
    CssClass="btn btn-danger" />

    </div>

</form>

</body>
</html>