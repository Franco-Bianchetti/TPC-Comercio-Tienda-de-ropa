<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Marcas.aspx.cs"
    Inherits="TPC_TiendaRopa.Marcas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marcas</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">

        <div class="container">

            <h2>Marcas</h2>

            <asp:GridView
                ID="gvMarcas"
                runat="server"
                CssClass="table table-striped table-bordered"
                AutoGenerateColumns="false">

                <Columns>

                    <asp:BoundField
                        DataField="Id"
                        HeaderText="ID" />

                    <asp:BoundField
                        DataField="Nombre"
                        HeaderText="Nombre" />

                </Columns>

            </asp:GridView>

            <br />

            <asp:Button
                ID="btnNuevo"
                runat="server"
                Text="Nuevo"
                CssClass="btn btn-primary" />

            <asp:Button
                ID="btnEditar"
                runat="server"
                Text="Editar"
                CssClass="btn btn-warning" />

            <asp:Button
                ID="btnEliminar"
                runat="server"
                Text="Eliminar"
                CssClass="btn btn-danger" />

        </div>

    </form>

</body>
</html>