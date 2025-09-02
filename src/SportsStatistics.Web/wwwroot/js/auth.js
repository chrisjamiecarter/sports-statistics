// wwwroot/js/auth.js
export async function postSignin({ email, password, isPersistant, returnUrl }) {
    const response = await fetch("/api/authentication/signin", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            email,
            password,
            isPersistant,
            returnUrl
        })
    });

    const result = await response.json();
    return result;
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
