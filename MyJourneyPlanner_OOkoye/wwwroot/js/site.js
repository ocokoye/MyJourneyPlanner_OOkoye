// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function setAction(form) {
    const url = 'http://localhost/api/show?';
    const andSign = '&';
    const startstation =  form.startstation.value;
    const destinationstation =  form.destinationstation.value;
    const viastation =  form.viastation.value;
    const excludingstation =  form.excludingstation.value;

   
    const newUrl = url + startstation + andSign + destinationstation + andSign + viastation + andSign + excludingstation ;

    fetch(
        newUrl,
        {
            headers: { "Content-Type": "application/json" },
            method: "GET",
            body: ""
        }
    )
        .then(data => data.json())
        .then((json) => {
            alert(JSON.stringify(json));
            document.getElementById("form").reset();
        });
    return false;
}
