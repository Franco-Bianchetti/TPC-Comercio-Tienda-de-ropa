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

        .oculto-visual {
            visibility: hidden;
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
            <asp:Label ID="lblTituloReporte" runat="server" Text="Seleccione reporte" />
        </div>

        <p class="text-muted">
            <asp:Label ID="lblDescripcionReporte" runat="server" Text="Elegí un tipo de reporte para consultar información y exportar a CSV." />
        </p>

        <div class="row align-items-end">

            <div class="col-md-4 mb-3">
                <label>Tipo de reporte</label>
                <asp:DropDownList
                    ID="ddlReporte"
                    runat="server"
                    CssClass="form-select"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlReporte_SelectedIndexChanged">
                    <asp:ListItem Value="" Text="Seleccione reporte" Selected="True" />
                    <asp:ListItem Value="CLIENTES" Text="Ranking de clientes frecuentes" />
                    <asp:ListItem Value="VENDEDORES" Text="Ranking de vendedores" />
                    <asp:ListItem Value="PRODUCTOS" Text="Productos más vendidos" />
                    <asp:ListItem Value="STOCK_BAJO" Text="Productos con bajo stock" />
                    <asp:ListItem Value="MEDIO_PAGO" Text="Ventas por Medio de Pago" />
                    <asp:ListItem Value="BALANCE" Text="Balance de Ventas vs. Compras" />
                </asp:DropDownList>
            </div>

            <div class="col-md-2 mb-3" id="divDesde" runat="server">
                <label>Desde</label>
                <asp:TextBox
                    ID="txtDesde"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-control" />
            </div>

            <div class="col-md-2 mb-3" id="divHasta" runat="server">
                <label>Hasta</label>
                <asp:TextBox
                    ID="txtHasta"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-control" />
            </div>

            <div class="col-md-2 mb-3">
                <asp:Button
                    ID="btnBuscar"
                    runat="server"
                    Text="Consultar"
                    CssClass="btn btn-dark w-100"
                    OnClick="btnBuscar_Click" />
            </div>

            <div class="col-md-2 mb-3">
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
            Visible="false" />

    </div>

    <div class="tarjeta-reporte">

        <div class="d-flex justify-content-between align-items-center flex-wrap mb-3">
            <div class="titulo-seccion mb-0">
                Resultados
            </div>

            <asp:Label
                ID="lblCantidadRegistros"
                runat="server"
                CssClass="text-muted" />
        </div>

        <div class="table-responsive">

            <asp:GridView
                ID="gvResultados"
                runat="server"
                AutoGenerateColumns="true"
                ShowFooter="true"
                FooterStyle-Font-Bold="true"
                CssClass="table table-hover"
                GridLines="None"
                OnRowDataBound="gvResultados_RowDataBound"
                EmptyDataText="Seleccione un reporte y presione Consultar para ver resultados." />

        </div>

    </div>

</asp:Content>