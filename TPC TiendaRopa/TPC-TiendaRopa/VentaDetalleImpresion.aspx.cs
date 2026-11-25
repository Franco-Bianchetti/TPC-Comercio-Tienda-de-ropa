using System;
using System.Collections.Generic;
using System.IO;
using Dominio;
using Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TPC_TiendaRopa
{
    public partial class VentaDetalleImpresion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }

            generarPdf();
        }

        private void generarPdf()
        {
            int idVenta;
            if (!int.TryParse(Request.QueryString["id"], out idVenta))
                return;

            EmpresaNegocio empresaNegocio = new EmpresaNegocio();
            VentaNegocio ventaNegocio = new VentaNegocio();

            Empresa empresa = empresaNegocio.obtener();
            Venta venta = ventaNegocio.obtenerVenta(idVenta);

            if (empresa == null || venta == null)
                return;

            List<DetalleVenta> detalle = ventaNegocio.obtenerDetalle(idVenta);

            string tituloDoc = "FACTURA";
            string letraDoc = "B";
            string codigoDoc = "CÓD. 06";

            switch (venta.TipoFactura)
            {
                case "FACTURA A":
                    letraDoc = "A";
                    codigoDoc = "CÓD. 01";
                    break;
                case "FACTURA C":
                    letraDoc = "C";
                    codigoDoc = "CÓD. 11";
                    break;
                case "PRESUPUESTO":
                    tituloDoc = "PRESUPUESTO";
                    letraDoc = "X";
                    codigoDoc = "";
                    break;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document documento = new Document(PageSize.A4, 30, 30, 30, 30);
                PdfWriter.GetInstance(documento, ms);
                documento.Open();

                BaseColor bordeSuave = new BaseColor(210, 214, 220);
                BaseColor bordeGrilla = new BaseColor(222, 226, 230);
                BaseColor fondoTotal = new BaseColor(207, 226, 255);
                BaseColor fondoSuave = new BaseColor(248, 249, 250);
                BaseColor textoSubtitulo = new BaseColor(108, 117, 125);

                Font fOriginal = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13);
                Font fEmpresa = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                Font fTituloDoc = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13);
                Font fLetra = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
                Font fCodigo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7.5f);
                Font fCondIva = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9.5f);
                Font fSeccion = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7.5f, textoSubtitulo);
                Font fNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f);
                Font fNormal = FontFactory.GetFont(FontFactory.HELVETICA, 9f);

                // 1. BANNER ORIGINAL
                PdfPTable tablaOriginal = new PdfPTable(1);
                tablaOriginal.WidthPercentage = 100;

                PdfPCell celdaOriginal = new PdfPCell(new Phrase("ORIGINAL", fOriginal));
                celdaOriginal.HorizontalAlignment = Element.ALIGN_CENTER;
                celdaOriginal.VerticalAlignment = Element.ALIGN_MIDDLE;
                celdaOriginal.PaddingTop = 5f;
                celdaOriginal.PaddingBottom = 7f;
                celdaOriginal.BorderColor = bordeSuave;
                celdaOriginal.BorderWidth = 1f;
                tablaOriginal.AddCell(celdaOriginal);
                documento.Add(tablaOriginal);

                // 2. CABECERA (EMPRESA | LETRA | DATOS FACTURA)
                PdfPTable tablaCabecera = new PdfPTable(3);
                tablaCabecera.WidthPercentage = 100;
                tablaCabecera.SetWidths(new float[] { 44f, 12f, 44f });

                // Columna Izquierda: Empresa
                PdfPCell celdaEmpresa = new PdfPCell();
                celdaEmpresa.Padding = 9f;
                celdaEmpresa.BorderColor = bordeSuave;
                celdaEmpresa.BorderWidthTop = 0f;
                celdaEmpresa.BorderWidthLeft = 1f;
                celdaEmpresa.BorderWidthRight = 1f;
                celdaEmpresa.BorderWidthBottom = 1f;

                Paragraph pRazon = new Paragraph(empresa.RazonSocial, fEmpresa);
                pRazon.SpacingAfter = 5f;
                celdaEmpresa.AddElement(pRazon);

                celdaEmpresa.AddElement(new Paragraph(empresa.Direccion, fNormal));
                Paragraph pCiudad = new Paragraph(empresa.Ciudad, fNormal);
                pCiudad.SpacingAfter = 5f;
                celdaEmpresa.AddElement(pCiudad);

                celdaEmpresa.AddElement(crearLineaCampo("CUIT: ", empresa.CUIT, fNegrita, fNormal, 2.5f));
                celdaEmpresa.AddElement(crearLineaCampo("Ingresos Brutos: ", empresa.IngresosBrutos, fNegrita, fNormal, 2.5f));
                celdaEmpresa.AddElement(crearLineaCampo(
                    "Fecha Inicio Actividad: ",
                    empresa.FechaInicioActividad.ToString("dd/MM/yyyy"),
                    fNegrita,
                    fNormal,
                    6f));

                celdaEmpresa.AddElement(new Paragraph(empresa.CondicionIVA.ToUpper(), fCondIva));
                tablaCabecera.AddCell(celdaEmpresa);

                // Columna Central: Letra y Código
                PdfPCell celdaCentro = new PdfPCell();
                celdaCentro.PaddingTop = 8f;
                celdaCentro.PaddingBottom = 8f;
                celdaCentro.PaddingLeft = 6f;
                celdaCentro.PaddingRight = 6f;
                celdaCentro.HorizontalAlignment = Element.ALIGN_CENTER;
                celdaCentro.BorderColor = bordeSuave;
                celdaCentro.BorderWidthTop = 0f;
                celdaCentro.BorderWidthLeft = 0f;
                celdaCentro.BorderWidthRight = 0f;
                celdaCentro.BorderWidthBottom = 1f;

                PdfPTable cajaLetra = new PdfPTable(1);
                cajaLetra.WidthPercentage = 62f;
                PdfPCell celdaLetra = new PdfPCell(new Phrase(letraDoc, fLetra));
                celdaLetra.HorizontalAlignment = Element.ALIGN_CENTER;
                celdaLetra.VerticalAlignment = Element.ALIGN_MIDDLE;
                celdaLetra.PaddingTop = 3f;
                celdaLetra.PaddingBottom = 7f;
                celdaLetra.BorderColor = BaseColor.BLACK;
                celdaLetra.BorderWidth = 1.5f;
                cajaLetra.AddCell(celdaLetra);
                celdaCentro.AddElement(cajaLetra);

                if (!string.IsNullOrEmpty(codigoDoc))
                {
                    Paragraph pCod = new Paragraph(codigoDoc, fCodigo);
                    pCod.Alignment = Element.ALIGN_CENTER;
                    pCod.SpacingBefore = 3f;
                    celdaCentro.AddElement(pCod);
                }

                tablaCabecera.AddCell(celdaCentro);

                // Columna Derecha: Comprobante
                PdfPCell celdaFactura = new PdfPCell();
                celdaFactura.Padding = 9f;
                celdaFactura.BorderColor = bordeSuave;
                celdaFactura.BorderWidthTop = 0f;
                celdaFactura.BorderWidthLeft = 1f;
                celdaFactura.BorderWidthRight = 1f;
                celdaFactura.BorderWidthBottom = 1f;

                Paragraph pTituloDoc = new Paragraph(tituloDoc, fTituloDoc);
                pTituloDoc.SpacingAfter = 7f;
                celdaFactura.AddElement(pTituloDoc);

                celdaFactura.AddElement(crearLineaCampo("Punto de Venta: ", empresa.PuntoVenta, fNegrita, fNormal, 4f));
                celdaFactura.AddElement(crearLineaCampo("Comp. Nro: ", venta.NumeroComprobante, fNegrita, fNormal, 4f));
                celdaFactura.AddElement(crearLineaCampo(
                    "Fecha de Emisión: ",
                    venta.Fecha.ToString("dd/MM/yyyy"),
                    fNegrita,
                    fNormal,
                    0f));

                tablaCabecera.AddCell(celdaFactura);
                documento.Add(tablaCabecera);

                // 3. DATOS DEL CLIENTE
                PdfPTable tablaCliente = new PdfPTable(1);
                tablaCliente.WidthPercentage = 100;

                string nombreCliente = venta.Cliente != null
                    ? venta.Cliente.Nombre + " " + venta.Cliente.Apellido
                    : "-";
                string dniCliente = (venta.Cliente != null && !string.IsNullOrWhiteSpace(venta.Cliente.Dni))
                    ? venta.Cliente.Dni
                    : "-";
                string dirCliente = (venta.Cliente != null && !string.IsNullOrWhiteSpace(venta.Cliente.Direccion))
                    ? venta.Cliente.Direccion
                    : "-";
                string telCliente = (venta.Cliente != null && !string.IsNullOrWhiteSpace(venta.Cliente.Telefono))
                    ? venta.Cliente.Telefono
                    : "-";
                string emailCliente = (venta.Cliente != null && !string.IsNullOrWhiteSpace(venta.Cliente.Email))
                    ? venta.Cliente.Email
                    : "-";

                PdfPCell celdaCliente = new PdfPCell();
                celdaCliente.Padding = 8f;
                celdaCliente.BorderColor = bordeSuave;
                celdaCliente.BorderWidthTop = 0f;
                celdaCliente.BorderWidthLeft = 1f;
                celdaCliente.BorderWidthRight = 1f;
                celdaCliente.BorderWidthBottom = 1f;

                Paragraph pSecCliente = new Paragraph("DATOS DEL CLIENTE", fSeccion);
                pSecCliente.SpacingAfter = 4f;
                celdaCliente.AddElement(pSecCliente);

                celdaCliente.AddElement(crearLineaCampo("Apellido y Nombre: ", nombreCliente, fNegrita, fNormal, 3f));
                celdaCliente.AddElement(crearLineaCampo("DNI: ", dniCliente, fNegrita, fNormal, 3f));
                celdaCliente.AddElement(crearLineaCampo("Dirección: ", dirCliente, fNegrita, fNormal, 3f));
                celdaCliente.AddElement(crearLineaCampo("Teléfono: ", telCliente, fNegrita, fNormal, 3f));
                celdaCliente.AddElement(crearLineaCampo("Email: ", emailCliente, fNegrita, fNormal, 0f));

                tablaCliente.AddCell(celdaCliente);
                documento.Add(tablaCliente);

                // 4. DATOS DE LA OPERACIÓN (DEBAJO DE DATOS DEL CLIENTE)
                PdfPTable tablaOperacion = new PdfPTable(1);
                tablaOperacion.WidthPercentage = 100;

                string datosVendedor = "-";
                if (venta.Vendedor != null)
                {
                    string codigo = string.IsNullOrWhiteSpace(venta.Vendedor.Codigo)
                        ? ""
                        : venta.Vendedor.Codigo + " - ";
                    datosVendedor = codigo + venta.Vendedor.Nombre + " " + venta.Vendedor.Apellido;
                }

                PdfPCell celdaOperacion = new PdfPCell();
                celdaOperacion.Padding = 8f;
                celdaOperacion.BackgroundColor = fondoSuave;
                celdaOperacion.BorderColor = bordeSuave;
                celdaOperacion.BorderWidthTop = 0f;
                celdaOperacion.BorderWidthLeft = 1f;
                celdaOperacion.BorderWidthRight = 1f;
                celdaOperacion.BorderWidthBottom = 1f;

                Paragraph pSecOp = new Paragraph("DATOS DE LA OPERACIÓN", fSeccion);
                pSecOp.SpacingAfter = 4f;
                celdaOperacion.AddElement(pSecOp);
                celdaOperacion.AddElement(crearLineaCampo("Vendedor: ", datosVendedor, fNegrita, fNormal, 0f));

                tablaOperacion.AddCell(celdaOperacion);
                documento.Add(tablaOperacion);

                // 5. DETALLE DE PRODUCTOS
                PdfPTable tablaDetalle = new PdfPTable(4);
                tablaDetalle.WidthPercentage = 100;
                tablaDetalle.SpacingBefore = 11f;
                tablaDetalle.SetWidths(new float[] { 46f, 14f, 20f, 20f });

                string[] encabezados = { "Producto", "Cantidad", "Precio Unitario", "Subtotal" };
                foreach (string tituloCol in encabezados)
                {
                    PdfPCell celdaHeader = new PdfPCell(new Phrase(tituloCol, fNegrita));
                    celdaHeader.Padding = 5.5f;
                    celdaHeader.BorderColor = bordeGrilla;
                    celdaHeader.BorderWidth = 1f;
                    tablaDetalle.AddCell(celdaHeader);
                }

                foreach (DetalleVenta item in detalle)
                {
                    tablaDetalle.AddCell(crearCeldaDetalle(item.NombreProducto, fNormal, bordeGrilla));
                    tablaDetalle.AddCell(crearCeldaDetalle(item.Cantidad.ToString(), fNormal, bordeGrilla));
                    tablaDetalle.AddCell(crearCeldaDetalle("$ " + item.PrecioUnitario.ToString("N2"), fNormal, bordeGrilla));
                    tablaDetalle.AddCell(crearCeldaDetalle("$ " + item.Subtotal.ToString("N2"), fNormal, bordeGrilla));
                }

                documento.Add(tablaDetalle);

                // 6. FORMA DE PAGO (IZQUIERDA) Y TOTALES (DERECHA)
                decimal subtotal = (venta.FormaPago == "EFECTIVO")
                    ? venta.Total / 0.80m
                    : venta.Total;
                decimal descuento = subtotal - venta.Total;

                PdfPTable tablaInferior = new PdfPTable(3);
                tablaInferior.WidthPercentage = 100;
                tablaInferior.SpacingBefore = 12f;
                tablaInferior.SetWidths(new float[] { 50f, 8f, 42f });

                PdfPCell celdaPago = new PdfPCell(
                    crearLineaCampo("Forma de Pago: ", venta.FormaPago, fNegrita, fNormal, 0f));
                celdaPago.Padding = 8f;
                celdaPago.BackgroundColor = fondoSuave;
                celdaPago.BorderColor = bordeGrilla;
                celdaPago.BorderWidth = 1f;
                celdaPago.VerticalAlignment = Element.ALIGN_TOP;
                tablaInferior.AddCell(celdaPago);

                PdfPCell celdaEspacio = new PdfPCell();
                celdaEspacio.Border = iTextSharp.text.Rectangle.NO_BORDER;
                tablaInferior.AddCell(celdaEspacio);

                PdfPTable tablaTotales = new PdfPTable(2);
                tablaTotales.WidthPercentage = 100;
                tablaTotales.SetWidths(new float[] { 45f, 55f });

                agregarFilaTotal(tablaTotales, "Subtotal", "$ " + subtotal.ToString("N2"), fNegrita, fNormal, bordeGrilla, null);
                agregarFilaTotal(tablaTotales, "Descuento", "$ " + descuento.ToString("N2"), fNegrita, fNormal, bordeGrilla, null);
                agregarFilaTotal(tablaTotales, "TOTAL", "$ " + venta.Total.ToString("N2"), fNegrita, fNegrita, bordeGrilla, fondoTotal);

                PdfPCell celdaContenedorTotales = new PdfPCell(tablaTotales);
                celdaContenedorTotales.Border = iTextSharp.text.Rectangle.NO_BORDER;
                celdaContenedorTotales.Padding = 0f;
                tablaInferior.AddCell(celdaContenedorTotales);

                documento.Add(tablaInferior);

                // 7. PIE DE PÁGINA (QR + CAE)
                PdfPTable tablaPie = new PdfPTable(2);
                tablaPie.WidthPercentage = 100;
                tablaPie.SpacingBefore = 15f;
                tablaPie.SetWidths(new float[] { 45f, 55f });

                PdfPCell celdaQr = new PdfPCell();
                celdaQr.PaddingTop = 8f;
                celdaQr.BorderColor = bordeGrilla;
                celdaQr.BorderWidthTop = 1f;
                celdaQr.BorderWidthLeft = 0f;
                celdaQr.BorderWidthRight = 0f;
                celdaQr.BorderWidthBottom = 0f;

                string rutaQr = Server.MapPath("~/Imagenes/qr-demo.png");
                if (venta.TipoFactura != "PRESUPUESTO" && File.Exists(rutaQr))
                {
                    try
                    {
                        iTextSharp.text.Image imgQr = iTextSharp.text.Image.GetInstance(rutaQr);
                        imgQr.ScaleToFit(155f, 65f);
                        celdaQr.AddElement(imgQr);
                    }
                    catch
                    {
                    }
                }
                tablaPie.AddCell(celdaQr);

                PdfPCell celdaCae = new PdfPCell();
                celdaCae.PaddingTop = 10f;
                celdaCae.HorizontalAlignment = Element.ALIGN_RIGHT;
                celdaCae.VerticalAlignment = Element.ALIGN_MIDDLE;
                celdaCae.BorderColor = bordeGrilla;
                celdaCae.BorderWidthTop = 1f;
                celdaCae.BorderWidthLeft = 0f;
                celdaCae.BorderWidthRight = 0f;
                celdaCae.BorderWidthBottom = 0f;

                string textoCae = string.IsNullOrWhiteSpace(venta.CAE) ? "N/A" : venta.CAE;
                string textoVto = venta.VencimientoCAE.HasValue
                    ? venta.VencimientoCAE.Value.ToString("dd/MM/yyyy")
                    : "N/A";

                Paragraph pCae = crearLineaCampo("CAE: ", textoCae, fNegrita, fNormal, 5f);
                pCae.Alignment = Element.ALIGN_RIGHT;
                celdaCae.AddElement(pCae);

                Paragraph pVto = crearLineaCampo("Fecha de Vto. de CAE: ", textoVto, fNegrita, fNormal, 0f);
                pVto.Alignment = Element.ALIGN_RIGHT;
                celdaCae.AddElement(pVto);

                tablaPie.AddCell(celdaCae);
                documento.Add(tablaPie);

                documento.Close();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader(
                    "content-disposition",
                    "attachment; filename=Factura_" + venta.NumeroComprobante + ".pdf");
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }

        private Paragraph crearLineaCampo(
            string etiqueta,
            string valor,
            Font fEtiqueta,
            Font fValor,
            float espacioInferior)
        {
            Paragraph p = new Paragraph();
            p.Add(new Chunk(etiqueta, fEtiqueta));
            p.Add(new Chunk(valor, fValor));
            p.SpacingAfter = espacioInferior;
            return p;
        }

        private PdfPCell crearCeldaDetalle(string texto, Font fuente, BaseColor colorBorde)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, fuente));
            celda.Padding = 5.5f;
            celda.BorderColor = colorBorde;
            celda.BorderWidth = 1f;
            return celda;
        }

        private void agregarFilaTotal(
            PdfPTable tabla,
            string etiqueta,
            string valor,
            Font fEtiqueta,
            Font fValor,
            BaseColor colorBorde,
            BaseColor colorFondo)
        {
            PdfPCell cLabel = new PdfPCell(new Phrase(etiqueta, fEtiqueta));
            cLabel.Padding = 5.5f;
            cLabel.BorderColor = colorBorde;
            cLabel.BorderWidthTop = colorFondo != null ? 1f : 0f;
            cLabel.BorderWidthBottom = 1f;
            cLabel.BorderWidthLeft = 0f;
            cLabel.BorderWidthRight = 0f;
            if (colorFondo != null)
                cLabel.BackgroundColor = colorFondo;

            PdfPCell cValor = new PdfPCell(new Phrase(valor, fValor));
            cValor.Padding = 5.5f;
            cValor.HorizontalAlignment = Element.ALIGN_RIGHT;
            cValor.BorderColor = colorBorde;
            cValor.BorderWidthTop = colorFondo != null ? 1f : 0f;
            cValor.BorderWidthBottom = 1f;
            cValor.BorderWidthLeft = 0f;
            cValor.BorderWidthRight = 0f;
            if (colorFondo != null)
                cValor.BackgroundColor = colorFondo;

            tabla.AddCell(cLabel);
            tabla.AddCell(cValor);
        }
    }
}