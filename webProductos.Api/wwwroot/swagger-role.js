(() => {
    const decodeJwt = (token) => {
        try {
            const payload = token.split(".")[1];
            return JSON.parse(atob(payload));
        } catch {
            return null;
        }
    };

    const hideEndpointsForUser = () => {
        const tokenInput = document.querySelector("input[placeholder='Bearer {token}']");
        if (!tokenInput) return;

        const token = tokenInput.value.replace("Bearer ", "").trim();
        const payload = decodeJwt(token);
        if (!payload) return;

        const roleClaim = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || payload["role"];

        // Si el rol no es Admin → ocultamos endpoints de admin
        if (roleClaim && roleClaim.toLowerCase() === "user") {
            document.querySelectorAll("section div.opblock-summary-description").forEach(desc => {
                if (desc.innerText.includes("[ADMIN_ONLY]")) {
                    const block = desc.closest(".opblock");
                    if (block) block.style.display = "none";
                }
            });
        }
    };

    // Escucha cambios en el botón Authorize
    setInterval(hideEndpointsForUser, 2000);
})();
