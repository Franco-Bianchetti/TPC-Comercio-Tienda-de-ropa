
<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="TPC_TiendaRopa.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .titulo-reportes {
            margin-bottom: 25px;
        }

        .titulo-reportes h2 {
            font-weight: 700;
            color: #202020;
            margin-bottom: 5px;
        }

        .subtitulo-reportes {
            color: #777;
            font-size: 14px;
        }

        .tarjeta-reporte {
            background: #fff;
            border: 1px solid #e5e2dc;
            border-radius: 12px;
            padding: 25px;
            margin-bottom: 25px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.04);
        }

        .titulo-seccion {
            font-size: 18px;
            font-weight: 700;
            color: #202020;
            margin-bottom: 20px;
        }

        .btn-exportar {
            background-color: #b49a78;
            color: #fff;
            border: none;
        }

        .btn-exportar:hover {
            background-color: #9e8463;
            color: #fff;
        }

        .mensaje-reporte {
            display: block;
            margin-top: 15px;
            font-size: 14px;
        }
    </style>

    <div class="titulo-reportes">
        <h2>Reportes</h2>
        <p class="subtitulo-reportes">
            Consultas y estadísticas de gestión del negocio.
        </p>
    </div>

    <div class="tarjeta-reporte">

        <div class="titulo-seccion">
            Ranking de vendedores
        </div>

        <p class="text-muted">
            Consultá el rendimiento de cada vendedor según las ventas realizadas
            durante un período determinado.
        </p>

        <div class="row align-items-end">

            <div class="col-md-3 mb-3">
                <label>Desde</label>
                <asp:TextBox
                    ID="txtDesde"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-control">
                </asp:TextBox>
            </div>

            <div class="col-md-3 mb-3">
                <label>Hasta</label>
                <asp:TextBox
                    ID="txtHasta"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-control">
                </asp:TextBox>
            </div>

            <div class="col-md-3 mb-3">
                <asp:Button
                    ID="btnBuscar"
                    runat="server"
                    Text="Consultar"
                    CssClass="btn btn-dark w-100"
                    OnClick="btnBuscar_Click" />
            </div>

            <div class="col-md-3 mb-3">
                <asp:Button
                    ID="btnExportar"
                    runat="server"
                    Text="Exportar CSV"
                    CssClass="btn btn-exportar w-100"
                    OnClick="btnExportar_Click" />
            </div>

        </div>

        <asp:Label
            ID="lblMensaje"
            runat="server"
            CssClass="mensaje-reporte"
            Visible="false">
        </asp:Label>

    </div>

    <div class="tarjeta-reporte">

        <div class="d-flex justify-content-between align-items-center flex-wrap mb-3">
            <div class="titulo-seccion mb-0">
                Resultados
            </div>

            <asp:Label
                ID="lblCantidadVendedores"
                runat="server"
                CssClass="text-muted">
            </asp:Label>
        </div>

        <div class="table-responsive">

            <asp:GridView
                ID="gvRanking"
                runat="server"
                AutoGenerateColumns="false"
                CssClass="table table-hover"
                GridLines="None"
                EmptyDataText="No se encontraron ventas para el período seleccionado.">

                <Columns>

                    <asp:BoundField
                        DataField="Codigo"
                        HeaderText="Código" />

                    <asp:BoundField
                        DataField="Nombre"
                        HeaderText="Nombre" />

                    <asp:BoundField
                        DataField="Apellido"
                        HeaderText="Apellido" />

                    <asp:BoundField
                        DataField="CantidadVentas"
                        HeaderText="Ventas"
                        DataFormatString="{0:N0}" />

                    <asp:BoundField
                        DataField="UnidadesVendidas"
                        HeaderText="Unidades"
                        DataFormatString="{0:N0}" />

                    <asp:BoundField
                        DataField="TotalVendido"
                        HeaderText="Total vendido"
                        DataFormatString="{0:C2}" />

                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>
