// wwwroot/js/auth.js
export function postSignin({ email, password, isPersistant, returnUrl }) {
    const f = document.createElement("form");
    f.method = "POST";
    f.action = "/api/authentication/signin";
    f.style.display = "none";
    const add = (n, v) => {
        const i = document.createElement("input");
        i.type = "hidden";
        i.name = n;
        i.value = v ?? "";
        f.appendChild(i);
    };
    add("email", email);
    add("password", password);
    add("isPersistant", isPersistant);
    if (returnUrl) add("returnUrl", returnUrl);
    const xsrf = document.querySelector('meta[name="xsrf-token"]')?.content;
    if (xsrf) add("__RequestVerificationToken", xsrf);
    document.body.appendChild(f);
    f.submit();
}

export function postSignout(returnUrl) {
    const f = document.createElement("form");
    f.method = "POST";
    f.action = "/api/authentication/signout";
    f.style.display = "none";
    const add = (n, v) => {
        const i = document.createElement("input");
        i.type = "hidden";
        i.name = n;
        i.value = v ?? "";
        f.appendChild(i);
    };
    if (returnUrl) add("returnUrl", returnUrl);
    document.body.appendChild(f);
    f.submit();
}
