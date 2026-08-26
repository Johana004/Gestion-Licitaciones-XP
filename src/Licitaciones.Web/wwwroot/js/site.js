// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", () => {
    // 1. Lógica de Modo Claro / Oscuro
    const btnTema = document.getElementById("btnToggleTema");
    const iconTema = document.getElementById("iconTema");
    
    const temaGuardado = localStorage.getItem("theme") || "light";
    document.documentElement.setAttribute("data-bs-theme", temaGuardado);
    actualizarIconoTema(temaGuardado);

    btnTema?.addEventListener("click", () => {
        const temaActual = document.documentElement.getAttribute("data-bs-theme");
        const nuevoTema = temaActual === "dark" ? "light" : "dark";
        document.documentElement.setAttribute("data-bs-theme", nuevoTema);
        localStorage.setItem("theme", nuevoTema);
        actualizarIconoTema(nuevoTema);
    });

    function actualizarIconoTema(tema) {
        if (!iconTema) return;
        iconTema.className = tema === "dark" ? "bi bi-sun" : "bi bi-moon-stars";
    }

    // 2. Lógica de Alternancia CRC / USD
    let monedaActual = "CRC";
    const tipoCambio = 510; // Valor de conversión o inyectado desde la API
    const btnMoneda = document.getElementById("btnToggleMoneda");
    const lblMoneda = document.getElementById("lblMoneda");

    btnMoneda?.addEventListener("click", () => {
        monedaActual = monedaActual === "CRC" ? "USD" : "CRC";
        if (lblMoneda) lblMoneda.innerText = monedaActual === "CRC" ? "USD" : "CRC";

        document.querySelectorAll(".monto-convertible").forEach(el => {
            const montoCRC = parseFloat(el.getAttribute("data-monto-crc") || "0");
            if (monedaActual === "USD") {
                const montoUSD = (montoCRC / tipoCambio).toFixed(2);
                el.innerText = `$${montoUSD} USD`;
            } else {
                el.innerText = `₡${montoCRC.toLocaleString('es-CR', { minimumFractionDigits: 2 })}`;
            }
        });
    });
});