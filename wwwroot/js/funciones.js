// Función adicional (puedes expandir)
document.addEventListener('DOMContentLoaded', () => {
  console.log("Funciones JS cargadas correctamente.");
});
<script>
  // Sidebar toggle
  document.getElementById("menu-toggle").addEventListener("click", () => {
    document.getElementById("wrapper").classList.toggle("toggled");
  });

  // Modo oscuro persistente con íconos
  const body = document.body;
  const themeToggle = document.getElementById("toggle-theme");
  const themeIcon = document.getElementById("theme-icon");

  function updateThemeIcon(theme) {
    themeIcon.className = theme === "dark" ? "bi bi-sun-fill" : "bi bi-moon-fill";
  }

  // Inicializar modo
  let currentTheme = localStorage.getItem("theme") || "light";
  body.setAttribute("data-bs-theme", currentTheme);
  updateThemeIcon(currentTheme);

  // Al hacer clic
  themeToggle.addEventListener("click", () => {
    currentTheme = currentTheme === "dark" ? "light" : "dark";
    body.setAttribute("data-bs-theme", currentTheme);
    localStorage.setItem("theme", currentTheme);
    updateThemeIcon(currentTheme);
  });

  // Menús sociales
  function toggleDropdown(id) {
    document.querySelectorAll('.dropdown-social').forEach(el => {
      el.style.display = el.id === id && el.style.display !== 'block' ? 'block' : 'none';
    });
  }

  document.addEventListener("click", function (e) {
    if (!e.target.classList.contains('social-icon')) {
      document.querySelectorAll('.dropdown-social').forEach(el => el.style.display = 'none');
    }
  });
</script>


<script>
  // Asegurar que solo un submenú esté abierto a la vez
  document.querySelectorAll('#sidebar-menu [data-bs-toggle="collapse"]').forEach(trigger => {
    trigger.addEventListener('click', function () {
      const targetId = this.getAttribute('href');
      document.querySelectorAll('#sidebar-menu .collapse').forEach(collapse => {
        if ('#' + collapse.id !== targetId) {
          const bsCollapse = bootstrap.Collapse.getInstance(collapse);
          if (bsCollapse) {
            bsCollapse.hide();
          } else {
            new bootstrap.Collapse(collapse, { toggle: false }).hide();
          }
        }
      });
    });
  });
</script>
// Submenús: solo uno abierto
document.querySelectorAll('#sidebar-menu [data-bs-toggle="collapse"]').forEach(trigger => {
  trigger.addEventListener('click', function () {
    const targetId = this.getAttribute('href');
    document.querySelectorAll('#sidebar-menu .collapse').forEach(collapse => {
      if ('#' + collapse.id !== targetId) {
        const bsCollapse = bootstrap.Collapse.getInstance(collapse);
        if (bsCollapse) {
          bsCollapse.hide();
        } else {
          new bootstrap.Collapse(collapse, { toggle: false }).hide();
        }
      }
    });
  });
});
