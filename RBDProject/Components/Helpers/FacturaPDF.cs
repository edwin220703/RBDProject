using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using RBDProject.Models;

namespace RBDProject.Components.Helpers
{
    public class FacturaPDF
    {
        private string _rnc { get; set; }

        private RbdFactura _rbdFactura { get; set; }

        private List<DetalleFac> _listDetalle;

        public FacturaPDF(string _rnc, RbdFactura rbdFactura, List<DetalleFac> _listDetalle)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            this._rnc = _rnc;
            this._rbdFactura = rbdFactura;
            this._listDetalle = _listDetalle;
        }

        public void GenerarFactura()
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    //CONFIGURACION DE PAGINA        
                    pagina.ContinuousSize(80, Unit.Millimetre);

                    pagina.Margin(1, Unit.Millimetre);

                    pagina.Content().AlignTop().Column(col =>
                    {
                        col.Spacing(3);
                        //PRESENTACION
                        col.Item().Text("RBD SOFTWARE").AlignCenter().FontSize(16).Bold();
                        col.Item().Text("Ingenieria de Software").FontSize(14).AlignCenter();
                        col.Item().Text("Rep.Dom, Espaillat, Moca").FontSize(12).AlignCenter();
                        col.Item().Text("Plaza City Center, autop. Ramon Caceres").FontSize(12).AlignCenter();
                        col.Item().Text("(829)-000-000/(809)-000-0000").FontSize(12).AlignCenter();
                        col.Item().Text("").FontSize(8).AlignCenter();


                        //INFORMACIONES
                        col.Item().Text($"RNC:{_rnc}").FontSize(12);
                        col.Item().Text($"No. Factura: {_rbdFactura.NumFac}").FontSize(12);

                        string com = _rbdFactura.CodNCfNavigation == null ? string.Empty : _rbdFactura.CodNCfNavigation.SecCom;

                        col.Item().Text($"No. Comprobante: {com}").FontSize(12);
                        col.Item().Text($"Cliente: {_rbdFactura.CodCliNavigation.NomCli}").FontSize(12);
                        col.Item().Text($"Fecha: {_rbdFactura.FechaReg}").FontSize(12);
                        col.Item().Text($"Pago: {_rbdFactura.CodTipagoNavigation.NomPago}").FontSize(12);
                        col.Item().Text($"Empleado: {_rbdFactura.CodEmNavigation.NomEm}").FontSize(12);
                        col.Item().Text("").FontSize(12);


                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(col =>
                            {
                                col.ConstantColumn(15, Unit.Millimetre);
                                col.RelativeColumn();
                                col.ConstantColumn(15, Unit.Millimetre);
                                col.RelativeColumn();
                                col.ConstantColumn(15, Unit.Millimetre);
                            });

                            t.Header(h =>
                            {
                                h.Cell().BorderBottom(1).Padding(1).Text("#").FontSize(12).AlignCenter();
                                h.Cell().BorderBottom(1).Padding(1).Text("Desc.").FontSize(12).AlignCenter();
                                h.Cell().BorderBottom(1).Padding(1).Text("Cant.").FontSize(12).AlignCenter();
                                h.Cell().BorderBottom(1).Padding(1).Text("Precio").FontSize(12).AlignCenter();
                                h.Cell().BorderBottom(1).Padding(1).Text("Total").FontSize(12).AlignCenter();
                            });

                            foreach (var i in _listDetalle)
                            {
                                t.Cell().Padding(1).Text($"{i.Id}").FontSize(10);
                                t.Cell().Padding(1).Text($"{i.Name}").FontSize(10);
                                t.Cell().Padding(1).AlignCenter().Text($"{i.Cantidad}").FontSize(10);
                                t.Cell().Padding(1).AlignCenter().Text($"{i.Precio - i.DescuentoUnit}").FontSize(10);
                                t.Cell().Padding(1).AlignRight().Text($"{i.Sub_Total}").FontSize(10);
                            }

                            t.Footer(f =>
                            {
                                f.Cell().BorderTop(1).BorderBottom(1).Padding(1).Text("").FontSize(12);
                                f.Cell().BorderTop(1).BorderBottom(1).Padding(1).Text("").FontSize(12);
                                f.Cell().BorderTop(1).BorderBottom(1).Padding(1).Text($"{_listDetalle.Sum(x => x.Cantidad)}").FontSize(10).AlignCenter().Bold();
                                f.Cell().BorderTop(1).BorderBottom(1).Padding(1).Text($"{_listDetalle.Sum(x => x.Precio - x.DescuentoUnit)}").FontSize(10).AlignCenter().Bold();
                                f.Cell().BorderTop(1).BorderBottom(1).Padding(1).Text($"{_listDetalle.Sum(x => x.Sub_Total)}").FontSize(10).AlignRight().Bold();
                            });


                            var ITBIS = _rbdFactura.CodNCfNavigation != null ? _listDetalle.Sum(x => x.Sub_Total) * (_rbdFactura.CodNCfNavigation.ImpCom / 100) : 0;

                            double Descuento = 0;

                            foreach (var a in _listDetalle)
                            {
                                Descuento += a.Precio - a.DescuentoUnit;
                            }

                            var IMPCOM = _rbdFactura.CodNCfNavigation != null ? _rbdFactura.CodNCfNavigation.ImpCom : 0;

                            col.Item().Text($"SUB-TOTAL: {_listDetalle.Sum(x => x.Sub_Total)}").FontSize(10).AlignRight().Bold();
                            col.Item().Text($"Descuento: {Descuento}").FontSize(10).AlignRight().Bold();
                            col.Item().Text($"ITBIS ({IMPCOM}%): {ITBIS}").FontSize(10).AlignRight().Bold();
                            col.Item().Text($"TOTAL: {_listDetalle.Sum(x => x.Sub_Total) + ITBIS}").FontSize(10).AlignRight().Bold();

                        });

                        if (_rbdFactura.Miscelaneo != null)
                        {
                            //MISCELANEOS
                            col.Item().Text("").FontSize(10);
                            col.Item().Text($"Miscelaneos: {_rbdFactura.Miscelaneo}").FontSize(12);
                        }
                    });
                });
            }).GeneratePdfAndShow();

        }
    }
}
