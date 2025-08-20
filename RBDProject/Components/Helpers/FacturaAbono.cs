using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using RBDProject.Models;

namespace RBDProject.Components.Helpers
{
    public class FacturaAbono
    {
        public void GenerarFactura(RbdCuentasPorCobrar cxc, RbdDetalleCuentaPorCobrar detall)
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
                        col.Item().Text($"No. Factura: {cxc.NumFact}").FontSize(12);
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
                                h.Cell().BorderBottom(1).Padding(1).Text("Fecha").FontSize(12).AlignCenter();
                                h.Cell().BorderBottom(1).Padding(1).Text("Monto").FontSize(12).AlignCenter();
                            });


                            t.Cell().Padding(1).Text($"{detall.FechaPago}").FontSize(10);
                            t.Cell().Padding(1).Text($"{detall.Monto}").FontSize(10);


                            t.Footer(f =>
                            {
                                f.Cell().BorderTop(1).BorderBottom(1).Padding(1).Text($"{cxc.Balance}").FontSize(10).AlignCenter().Bold();
                                f.Cell().BorderTop(1).BorderBottom(1).Padding(1).Text($"{cxc.TotalFact}").FontSize(10).AlignRight().Bold();
                            });

                        });
                    });
                });
            }).GeneratePdfAndShow();
        }
    }
}
