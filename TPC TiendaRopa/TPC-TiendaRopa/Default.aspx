<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_TiendaRopa._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tienda de Ropa</h2>
    <p class="lead">Productos disponibles</p>

    <div class="row">
        <asp:Repeater ID="repProductos" runat="server">
            <ItemTemplate>

                <div class="col-md-4 mb-3">
                    <div class="card h-100">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <p class="card-text">
                                <%#Eval("Marca.Nombre")%> - <%#Eval("Tipo.Nombre")%><br />
                                Talle: <%#Eval("Talle")%> | Color: <%#Eval("Color")%><br />
                                Stock: <%#Eval("StockActual")%>
                            </p>
                            <a href="Ventas.aspx?id=<%#Eval("Id")%>" class="btn btn-primary">Vender</a>
                        </div>
                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>