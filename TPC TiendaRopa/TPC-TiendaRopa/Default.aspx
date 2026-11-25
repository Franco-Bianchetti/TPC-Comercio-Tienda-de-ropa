
<%@ Page Title="Panel de gestión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_TiendaRopa._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .portada {
            background: linear-gradient(135deg, #242424, #45413b);
            color: white;
            padding: 35px;
            border-radius: 14px;
            margin-bottom: 30px;
        }

        .portada h1 {
            font-size: 32px;
            font-weight: 700;
            margin-bottom: 8px;
        }

        .portada p {
            color: #dedbd5;
            font-size: 15px;
            margin-bottom: 0;
        }

        .portada-detalle {
            width: 55px;
            height: 4px;
            background-color: #b49a78;
            margin-bottom: 18px;
            border-radius: 5px;
        }

        .acceso-card:hover {
            transform: translateY(-3px);
            box-shadow: 0 7px 18px rgba(0,0,0,0.08);
            color: #9b805f;
            text-decoration: none;
        }

        .titulo-seccion {
            font-weight: 700;
            color: #292929;
            margin-bottom: 5px;
        }

        .subtitulo-seccion {
            color: #888;
            margin-bottom: 25px;
        }

        .producto-card {
            border: 1px solid #e8e5df;
            border-radius: 12px;
            overflow: hidden;
            height: 100%;
            background-color: white;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .producto-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 9px 22px rgba(0,0,0,0.09);
        }

        .producto-cabecera {
            background-color: #f0ede8;
            padding: 17px 20px;
            border-bottom: 1px solid #e8e5df;
        }

        .producto-cabecera h5 {
            font-weight: 700;
            color: #292929;
            margin: 0;
        }

        .producto-contenido {
            padding: 20px;
        }

        .producto-marca {
            color: #9b805f;
            font-size: 13px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            margin-bottom: 15px;
        }

        .producto-dato {
            color: #555;
            font-size: 14px;
            margin-bottom: 9px;
        }

        .producto-stock {
            display: inline-block;
            background-color: #f1eee8;
            color: #6d5a42;
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            margin-top: 5px;
        }

        .boton-vender {
            display: block;
            width: 100%;
            margin-top: 20px;
            background-color: #242424;
            border: 1px solid #242424;
            color: white;
            padding: 10px;
            border-radius: 7px;
            text-align: center;
            text-decoration: none;
            font-weight: 600;
            transition: all 0.2s ease;
        }

        .boton-vender:hover {
            background-color: #b49a78;
            border-color: #b49a78;
            color: #202020;
            text-decoration: none;
        }

        @media (max-width: 576px) {
            .portada {
                padding: 28px 22px;
            }

            .portada h1 {
                font-size: 27px;
            }
        }
    </style>

    <div class="portada">
        <div class="portada-detalle"></div>

        <h1>Panel de gestión</h1>

        <p>
            Administración y gestión de las operaciones comerciales.
        </p>
    </div>

    <div class="mb-4">
        <h3 class="titulo-seccion">Productos disponibles</h3>

        <p class="subtitulo-seccion">
            Seleccioná un producto para iniciar una venta.
        </p>
    </div>

    <div class="row">

        <asp:Repeater ID="repProductos" runat="server">

            <ItemTemplate>

                <div class="col-lg-4 col-md-6 mb-4">

                    <div class="producto-card">

                        <div class="producto-cabecera">
                            <h5>
                                <%#: Eval("Nombre") %>
                            </h5>
                        </div>

                        <div class="producto-contenido">

                            <div class="producto-marca">
                                <%#: Eval("Marca.Nombre") %>
                                ·
                                <%#: Eval("Tipo.Nombre") %>
                            </div>

                            <p class="producto-dato">
                                <strong>Talle:</strong>
                                <%#: Eval("Talle") %>
                            </p>

                            <p class="producto-dato">
                                <strong>Color:</strong>
                                <%#: Eval("Color") %>
                            </p>

                            <span class="producto-stock">
                                Stock disponible: <%#: Eval("StockActual") %>
                            </span>

                            <a href='<%# "FormularioVenta.aspx?idProducto=" + Eval("Id") %>'
                               class="boton-vender">
                                Vender producto
                            </a>

                        </div>

                    </div>

                </div>

            </ItemTemplate>

        </asp:Repeater>

    </div>

</asp:Content>
