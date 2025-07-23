//FACTURA
//FACTURA
//FACTURA
//FACTURA
//FACTURA
class Detallefact {
    cant;
    descripcion;
    precio;
    total;
}

function(_noFac, _noCom, _fecFac, _client, _pago,Detallefact objeto, ) {
    var text = document.getElementById("textInput").value;
    // Create a new jsPDF instance
    const { jsPDF } = window.jspdf;

    var doc = new jsPDF({
        unit: "pt",
        format: [226.77, 600]
    });

    let y = 20;

    // Encabezado
    doc.setFontSize(12);
    doc.text("RDB SOFTWARE S.R.L.", 10, y);
    y += 20;
    doc.setFontSize(10);
    doc.text("RNC: 00-000000-0", 10, y);
    y += 15;
    doc.text("Tel: 809-000-0000", 10, y);
    y += 15;
    doc.text("Dirección: Rep. Dom. Espaillat, Moca,", 10, y);
    y += 10;
    doc.text("Plaza City centre, auto. Ramon Caceres", 10, y);
    y += 20;

    doc.setFontSize(10);
    doc.text("No. Factura: F0001", 10, y);
    y += 15;

    doc.text("No. Comprobante: F0001", 10, y);
    y += 15;

    doc.text("Fecha: 2025-07-23", 10, y);
    y += 20;

    doc.text("Cliente: Federico", 10, y);
    y += 20;

    doc.text("Pago: Contado", 10, y);
    y += 20;

    doc.text("---------------------------------------------", 10, y);
    y += 15;

    // Tabla productos
    doc.text("Cant  Descripción        Precio Unit.      Total", 10, y);
    y += 15;

    const productos = [
        { cantidad: 2, descripcion: "Producto A", precio: 100 },
        { cantidad: 1, descripcion: "Producto B", precio: 50 },
        { cantidad: 5, descripcion: "Producto c", precio: 5 },
    ];

    objeto.cant

    productos.forEach(prod => {
        const linea = `${prod.cantidad}     ${prod.descripcion.padEnd(15).slice(0, 15)}     ${prod.precio.toFixed(2)}`;
        doc.text(linea, 10, y);
        y += 15;
    });

    y += 10;
    doc.text("----------------------------------------------", 10, y);
    y += 15;

    const total = productos.reduce((sum, p) => sum + p.precio * p.cantidad, 0);
    doc.setFontSize(10);
    doc.text(`SubTotal: RD$ ${total.toFixed(2)}`, 10, y);
    y += 10;
    doc.text(`ITBIS(18%): RD$ ${total * 0.18}`, 10, y);
    y += 10;
    doc.text(`Total: RD$ ${total + (total * 0.18)}`, 10, y);
    y += 20;
    doc.text(`Miscelaneos:`, 10, y);

    // Download the PDF file
    doc.autoPrint();

    doc.output('dataurlnewwindow', { filename: 'comprobante.pdf' });
});

function Reporte() {
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF()

    // It can parse html:
    // <table id="my-table"><!-- ... --></table>
    autoTable(doc, { html: '#my-table' })

    // Or use javascript directly:
    autoTable(doc, {
        head: [['Name', 'Email', 'Ciudad', 'Provincia', 'Pais']],
        body: [
            ['David', 'david@example.com', 'Sweden'],
            ['Castille', 'castille@example.com', 'Spain'],
            // ...
        ],
    })

    doc.autoPrint();

    doc.output('dataurlnewwindow', { filename: 'comprobante.pdf' });
});

}