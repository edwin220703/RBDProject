using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RBDProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Reporte
    {
        //Muchos EMPLEADOS
        public void MultiEmpleados(List<RbdEmpleado> _listEmpleados)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Empleado").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Codigo").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre Completo").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("DNI").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Usuario").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Provincia").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Ciudad").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Estado").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (RbdEmpleado e in _listEmpleados)
                            {
                                table.Cell().Element(CellStyle).Text($"{e.CodEm}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{e.IdEm}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{e.NomEm}").AlignLeft();
                                table.Cell().Element(CellStyle).Text($"{e.DniEm}").AlignLeft();
                                table.Cell().Element(CellStyle).Text($"{e.NomUs}").AlignRight();

                                //PROVINCIA
                                if (e.IdProvinciaNavigation != null)
                                    table.Cell().Element(CellStyle).Text($"{e.IdProvinciaNavigation.NomProvincia}").AlignRight();
                                else
                                    table.Cell().Element(CellStyle).Text($"").AlignRight();

                                //CIUDAD
                                if (e.IdCiudadNavigation != null)
                                    table.Cell().Element(CellStyle).Text($"{e.IdCiudadNavigation.NomCiudad}").AlignRight();
                                else
                                    table.Cell().Element(CellStyle).Text($"").AlignRight();

                                table.Cell().Element(CellStyle).Text($"{e.CodEstNavigation.NomEst}").AlignRight();
                            }

                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        //MUCHOS CLIENTES
        public void MultiClientes(List<RbdCliente> _listClientes)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Clientes").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Codigo").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("RNC").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre Completo").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Correo").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Provincia").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Ciudad").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Fecha de Creacion").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (RbdCliente c in _listClientes)
                            {
                                table.Cell().Element(CellStyle).Text($"{c.CodCli}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.IdCli}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.TipRnc}");
                                table.Cell().Element(CellStyle).Text($"{c.NomCli}").AlignLeft();
                                table.Cell().Element(CellStyle).Text($"{c.CorrCli}").AlignLeft();

                                //PROVINCIA
                                if (c.IdProvinciaNavigation != null)
                                    table.Cell().Element(CellStyle).Text($"{c.IdProvinciaNavigation.NomProvincia}");
                                else
                                    table.Cell().Element(CellStyle).Text($"");

                                //CIUDAD
                                if (c.IdCiudadNavigation != null)
                                    table.Cell().Element(CellStyle).Text($"{c.IdCiudadNavigation.NomCiudad}");
                                else
                                    table.Cell().Element(CellStyle).Text($"");

                                table.Cell().Element(CellStyle).Text($"{c.FecEnt}");
                            }

                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        //EMPLEADOS INDIVIDUALES
        public void Empleados(RbdEmpleado _empleado)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4);

                    pagina.Margin((float)2.54, Unit.Centimetre);

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                                c.RelativeColumn();
                                c.RelativeColumn();
                                c.RelativeColumn();
                            });

                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("RBD SOFTWARE").Bold();
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("REPORTE DE EMPLEADOS").Bold(); ;

                            //DATOS PERSONALES
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS PERSONALES").Bold(); ;

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("Nombre Completo: ").Bold(); ;
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_empleado.NomEm}");

                            t.Cell().Element(CellStyle).Text("ID:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_empleado.CodEm}");

                            t.Cell().Element(CellStyle).Text("CODIGO:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_empleado.IdEm}");

                            t.Cell().Element(CellStyle).Text("DNI/CEDULA:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_empleado.DniEm}");

                            t.Cell().Element(CellStyle).Text("GENERO:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_empleado.CodGenNavigation.NomGen}");

                            //DIRECCION
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS DE LOCALICACION (DIRECCIONES)").Bold();

                            //PROVINCIA
                            t.Cell().Element(CellStyle).Text("PROVINCIA:").Bold();
                            if (_empleado.IdProvinciaNavigation != null)
                                t.Cell().Element(CellStyle).Text($"{_empleado.IdProvinciaNavigation.NomProvincia}");
                            else
                                t.Cell().Element(CellStyle).Text($"");

                            //CIUDAD
                            t.Cell().Element(CellStyle).Text("CIUDAD:").Bold();

                            if (_empleado.IdCiudadNavigation != null)
                                t.Cell().Element(CellStyle).Text($"{_empleado.IdCiudadNavigation.NomCiudad}");
                            else
                                t.Cell().Element(CellStyle).Text($"");

                            //CALLE
                            t.Cell().Element(CellStyle).Text("CALLE:").Bold();
                            if (_empleado.IdCalleNavigation != null)
                                t.Cell().Element(CellStyle).Text($"{_empleado.IdCalleNavigation.NomCalle}");
                            else
                                t.Cell().Element(CellStyle).Text($"");

                            t.Cell().ColumnSpan(2).Element(CellStyle).Text("");

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("DETALLES:").Bold();
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_empleado.DetallDirec}");

                            //CONTACTO
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS DE CONTACTO").Bold();

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("Contacto:").Bold();
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_empleado.RbdTelefonoEmpleados.First().TelEm}");

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("Correo electronico:").Bold();
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text("");

                            //DATOS EMPRESARIALES
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS EMPRESARIALES").Bold();

                            //CARGO
                            t.Cell().Element(CellStyle).Text("CARGO:").Bold();
                            if (_empleado.CodCarNavigation != null)
                                t.Cell().Element(CellStyle).Text($"{_empleado.CodCarNavigation.NomCar}");
                            else
                                t.Cell().Element(CellStyle).Text($"");

                            t.Cell().Element(CellStyle).Text("Sueldo:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_empleado.Suedms}");

                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS De Sistema").Bold();

                            t.Cell().Element(CellStyle).Text("USUARIO:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_empleado.NomUs}");

                            t.Cell().Element(CellStyle).Text("Estado:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_empleado.CodEstNavigation.NomEst}");

                            static IContainer CellStyle(IContainer container)
                            => container.Border(1).Padding(10);
                        });
                    });

                });
            }).GeneratePdfAndShow();
        }

        //CLIENTES INDIVIDUALES
        public void Clientes(RbdCliente _clientes)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4);

                    pagina.Margin((float)2.54, Unit.Centimetre);

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                                c.RelativeColumn();
                                c.RelativeColumn();
                                c.RelativeColumn();
                            });

                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("RBD SOFTWARE").Bold();
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("REPORTE DE CLIENTE").Bold(); ;

                            //DATOS PERSONALES
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS PERSONALES").Bold(); ;

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("Nombre Completo: ").Bold(); ;
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_clientes.NomCli}");

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("RNC:").Bold();
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_clientes.TipRnc}");

                            t.Cell().Element(CellStyle).Text("ID:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_clientes.CodCli}");

                            t.Cell().Element(CellStyle).Text("CODIGO:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_clientes.IdCli}");

                            t.Cell().Element(CellStyle).Text("DNI/CEDULA:").Bold();
                            t.Cell().Element(CellStyle).Text($"{_clientes.DniCli}");

                            //GENEROS
                            t.Cell().Element(CellStyle).Text("GENERO:").Bold();
                            if (_clientes.CodGenNavigation != null)
                                t.Cell().Element(CellStyle).Text($"{_clientes.CodGenNavigation.NomGen}");
                            else
                                t.Cell().Element(CellStyle).Text($"");

                            //DIRECCION
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS DE LOCALICACION (DIRECCIONES)").Bold();

                            //PROVINCIA
                            t.Cell().Element(CellStyle).Text("PROVINCIA:").Bold();
                            if (_clientes.IdProvinciaNavigation != null)
                                t.Cell().Element(CellStyle).Text($"{_clientes.IdProvinciaNavigation.NomProvincia}");
                            else
                                t.Cell().Element(CellStyle).Text($"");

                            //CIUDAD
                            t.Cell().Element(CellStyle).Text("CIUDAD:").Bold();
                            if (_clientes.IdCiudadNavigation != null)
                                t.Cell().Element(CellStyle).Text($"{_clientes.IdCiudadNavigation.NomCiudad}");
                            else
                                t.Cell().Element(CellStyle).Text($"");

                            //Calle
                            t.Cell().Element(CellStyle).Text("CALLE:").Bold();
                            if (_clientes.IdCalleNavigation != null)
                                t.Cell().Element(CellStyle).Text($"{_clientes.IdCalleNavigation.NomCalle}");
                            else
                                t.Cell().Element(CellStyle).Text($"");

                            t.Cell().ColumnSpan(2).Element(CellStyle).Text("");

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("DETALLES:").Bold();
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_clientes.DetallDirec}");

                            //CONTACTO
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS DE CONTACTO").Bold();

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("Contacto:").Bold();
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_clientes.RbdTelefonoClientes.First().TelCli}");

                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("Correo electronico:").Bold();
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_clientes.CorrCli}");


                            //DATOS DEL SISTEMA
                            t.Cell().ColumnSpan(4).Background(Colors.LightBlue.Accent1).Element(CellStyle).AlignCenter().Text("DATOS DE SISTEMA").Bold();


                            t.Cell().ColumnSpan(1).Element(CellStyle).Text("Fecha de Registro:").Bold();
                            t.Cell().ColumnSpan(3).Element(CellStyle).Text($"{_clientes.FecEnt}");

                            static IContainer CellStyle(IContainer container)
                            => container.Border(1).Padding(10);
                        });
                    });

                });
            }).GeneratePdfAndShow();
        }

        //DIRECCIONES
        public void Provincias(List<RbdProvincium> _listProvincias)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Provincias").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Provincia").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (RbdProvincium p in _listProvincias)
                            {
                                table.Cell().Element(CellStyle).Text($"{p.IdProvincia}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{p.NomProvincia}").AlignCenter();
                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        public void Ciudades(List<RbdCiudade> _listCiudades)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Ciudades").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Codigo Postal").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Provincia").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var c in _listCiudades)
                            {
                                table.Cell().Element(CellStyle).Text($"{c.IdCiudad}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.NomCiudad}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.CodPostal}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.IdProvinciaNavigation.NomProvincia}").AlignCenter();
                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        public void Calle(List<RbdCalle> _listCalle)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Calles").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Ciudad").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var c in _listCalle)
                            {
                                table.Cell().Element(CellStyle).Text($"{c.IdCalle}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.NomCalle}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.IdCiudadNavigation.NomCiudad}").AlignCenter();
                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        //CATEGORIAS
        public void Cargo(List<RbdCargo> _listCargo)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Cargos").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Descripcion").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Fecha de crecion").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Estado").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var c in _listCargo)
                            {
                                table.Cell().Element(CellStyle).Text($"{c.CodCar}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.NomCar}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.DesCar}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.FecCreacion}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.CodEstNavigation.NomEst}").AlignCenter();
                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        public void Grupos(List<RbdGrupo> _listGrupo)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Grupos").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Descripcion").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Fecha De Creacion").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Estado").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var g in _listGrupo)
                            {
                                table.Cell().Element(CellStyle).Text($"{g.CodGrup}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{g.NomGrup}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{g.DesGrup}");
                                table.Cell().Element(CellStyle).Text($"{g.FecCreacion}");

                                if (g.CodEstNavigation != null)
                                    table.Cell().Element(CellStyle).Text($"{g.CodEstNavigation.NomEst}");
                                else
                                    table.Cell().Element(CellStyle).Text($"");

                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        //ADMINISTRATIVOS
        public void TiposDePago(List<RbdTipoPago> _listTipoPago)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Tipos De Pagos").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Fecha de Creacion").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Estado").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var p in _listTipoPago)
                            {
                                table.Cell().Element(CellStyle).Text($"{p.CodTipago}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{p.NomPago}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{p.FecCreacion}");

                                if (p.CodEstNavigation != null)
                                    table.Cell().Element(CellStyle).Text($"{p.CodEstNavigation.NomEst}");
                                else
                                    table.Cell().Element(CellStyle).Text($"");
                            }

                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        public void Estados(List<RbdEstado> _listEstado)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Estados").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Descripcion").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Fecha De Creacion").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var e in _listEstado)
                            {
                                table.Cell().Element(CellStyle).Text($"{e.CodEst}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{e.NomEst}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{e.Descripcion}");
                                table.Cell().Element(CellStyle).Text($"{e.FecCreacion}");
                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        //FACTURACION
        public void Facturas(List<RbdFactura> _listFacturas)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Facturas").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Numero").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("NCF").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Cliente").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Tipo De Pago").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Balance").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Neto").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Empleado").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Fecha de registro ").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Estado").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var f in _listFacturas)
                            {
                                table.Cell().Element(CellStyle).Text($"{f.NumFac}").AlignCenter();

                                //NCF
                                if (f.CodNCfNavigation != null)
                                    table.Cell().Element(CellStyle).Text($"{f.CodNCfNavigation.SecCom}").AlignCenter();
                                else
                                    table.Cell().Element(CellStyle).Text($"").AlignCenter();

                                table.Cell().Element(CellStyle).Text($"{f.CodCliNavigation.NomCli}");
                                table.Cell().Element(CellStyle).Text($"{f.CodTipagoNavigation.NomPago}");
                                table.Cell().Element(CellStyle).Text($"{f.TotalBalance}");
                                table.Cell().Element(CellStyle).Text($"{f.TotalNeto}");
                                table.Cell().Element(CellStyle).Text($"{f.CodEmNavigation.NomEm}");
                                table.Cell().Element(CellStyle).Text($"{f.FechaReg}");
                                table.Cell().Element(CellStyle).Text($"{f.CodEstNavigation.NomEst}");
                            }

                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        public void CuentasPorCobrar(List<RbdCuentasPorCobrar> _listCuentasPorCobrar)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Cuentas Por Cobrar").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Factura").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Emision").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Vencimiento").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Balance").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Total").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Empleado").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var c in _listCuentasPorCobrar)
                            {
                                table.Cell().Element(CellStyle).Text($"{c.CodCcobro}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.NumFact}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{c.FechaEmi}");
                                table.Cell().Element(CellStyle).Text($"{c.VencPago}");
                                table.Cell().Element(CellStyle).Text($"{c.Balance}");
                                table.Cell().Element(CellStyle).Text($"{c.TotalFact}");

                                if (c.NumFactNavigation != null)
                                    table.Cell().Element(CellStyle).Text($"{c.NumFactNavigation.CodEmNavigation.NomEm}");
                                else
                                    table.Cell().Element(CellStyle).Text($"");

                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        //ARTICULO
        public void Articulos(List<RbdArticulo> _listArticulos)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Articulos").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Codigo").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Descripcion").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Existencias").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Grupo").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Fecha De Creacion").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Estado").FontColor(Colors.White).Bold().AlignCenter();
                            });


                            foreach (var a in _listArticulos)
                            {
                                table.Cell().Element(CellStyle).Text($"{a.CodArt}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{a.IdArt}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{a.NomArt}");
                                table.Cell().Element(CellStyle).Text($"{a.DesArt}");
                                table.Cell().Element(CellStyle).Text($"{a.ExistArt}");
                                table.Cell().Element(CellStyle).Text($"{a.CodGrupNavigation.NomGrup}");
                                table.Cell().Element(CellStyle).Text($"{a.FecArt}");
                                table.Cell().Element(CellStyle).Text($"{a.CodEstNavigation.NomEst}");
                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }

        //COMPROBANTES
        public void TiposDeComprobante(List<RbdTipoComprobante> _listTipoComprobante)
        {
            Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());

                    pagina.Margin((float)1.00, Unit.Centimetre);


                    pagina.Header().Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.Spacing(20);

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RBD SOFTWARE").AlignStart().FontSize(14).FontColor(Colors.Blue.Darken1);
                                col.Item().Text("Reporte De Tipos De Comprobantes").AlignStart().FontSize(30).Bold().FontColor(Colors.Blue.Darken1);

                            });

                            r.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"{DateTime.Now}").AlignStart().FontSize(10).AlignRight().FontColor(Colors.Blue.Darken1);

                            });
                        });
                    });

                    pagina.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(h =>
                            {
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("ID").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Nombre").FontColor(Colors.White).Bold().AlignCenter();
                                table.Cell().Background(Colors.Blue.Darken2).Element(CellStyle).Text("Descripcion").FontColor(Colors.White).Bold().AlignCenter();
                            });

                            foreach (var t in _listTipoComprobante)
                            {
                                table.Cell().Element(CellStyle).Text($"{t.CodTipocom}").AlignCenter();
                                table.Cell().Element(CellStyle).Text($"{t.NomTipocom}");
                                table.Cell().Element(CellStyle).Text($"{t.DesTipocom}");
                            }


                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(10);
                        });
                    });
                });
            }).GeneratePdfAndShow();
        }
    }

}
