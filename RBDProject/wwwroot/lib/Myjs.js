//TITULO
window.CambiarTitle = function (titlenew) {
    document.getElementById("TituloDePagina").innerHTML = titlenew;
}

//Alerta para enviar informacion
window.AlertaInfo = function (count, panel) {
    const alert = confirm("Se encontraron " + count + " registros similares en " + panel + " .¿Aun desea continuar?");

    return alert;
}

//ALERTA PARA ERRORES
window.Errorencontrado = function (error) {
    alert(error)
}

//ALERTA PARA NOTIFICACIONES
window.Notification = function (notificacion) {
    alert(notificacion)
}

window.BlazorFile = function (nombre, byteBase64) {

    var link = document.createElement('a');
    link.download = nombre;
    link.href = "data:application/vnd.ms-excel;base64," + byteBase64;
    document.body.appendChild(link); //FIREFOX
    link.click();
    document.body.removeChild(link);
}

window.RecargarPagina = function () {
    location.reload();
}
