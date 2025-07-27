// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Script de modo oscuro/claro
document.addEventListener("DOMContentLoaded", function () {
    const toggle = document.getElementById('themeToggle');
    const icon = document.getElementById('themeIcon');
    const root = document.documentElement;

    if (localStorage.getItem("theme") === "dark") {
        root.setAttribute("data-bs-theme", "dark");
        icon.classList.replace("bi-sun", "bi-moon");
    }

    toggle.addEventListener("click", () => {
        const isDark = root.getAttribute("data-bs-theme") === "dark";
        root.setAttribute("data-bs-theme", isDark ? "light" : "dark");
        localStorage.setItem("theme", isDark ? "light" : "dark");
        icon.classList.toggle("bi-sun");
        icon.classList.toggle("bi-moon");
    });
});
