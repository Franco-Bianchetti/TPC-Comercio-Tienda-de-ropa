<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Productos.aspx.cs"
    Inherits="TPC_TiendaRopa.Productos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Productos</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">

        <div class="container mt-4">

            <h2>Productos</h2>

            <asp:GridView
                ID="gvProductos"
                runat="server"
                CssClass="table table-striped table-hover table-bordered"
                AutoGenerateColumns="false">

                <Columns>

                    <asp:BoundField
                        DataField="Id"
                        HeaderText="ID" />

                    <asp:BoundField
                        DataField="Nombre"
                        HeaderText="Nombre" />

                    <asp:BoundField
                        DataField="Talle"
                        HeaderText="Talle" />

                    <asp:BoundField
                        DataField="Color"
                        HeaderText="Color" />

                    <asp:BoundField
                        DataField="StockActual"
                        HeaderText="Stock" />

                    <asp:BoundField
                        DataField="StockMinimo"
                        HeaderText="Stock Mín." />

                    <asp:BoundField
                        DataField="PorcentajeGanancia"
                        HeaderText="% Ganancia" />

                </Columns>

            </asp:GridView>

            <br />

            <asp:Button
                ID="btnNuevo"
                runat="server"
                Text="Nuevo"
                CssClass="btn btn-primary me-2" />

            <asp:Button
                ID="btnEditar"
                runat="server"
                Text="Editar"
                CssClass="btn btn-warning me-2" />

            <asp:Button
                ID="btnEliminar"
                runat="server"
                Text="Eliminar"
                CssClass="btn btn-danger" />

        </div>

    </form>

</body>
</html>