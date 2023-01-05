window.addEventListener("load", () => {
    const pwd_toggle_btn = document.getElementById("pwd_toggle_btn");
    const username_inp = document.getElementById("username");
    const password_inp = document.getElementById("password");
    const login_btn = document.getElementById("login_btn");
    //show-hide password button functionality
    pwd_toggle_btn.addEventListener("click", () => {
        if (password_inp.type === "password") {
            password_inp.type = "text";
            pwd_toggle_btn.classList.remove("fa-eye");
            pwd_toggle_btn.classList.add("fa-eye-slash");
        } else {
            password_inp.type = "password";
            pwd_toggle_btn.classList.remove("fa-eye-slash");
            pwd_toggle_btn.classList.add("fa-eye");

        }
    });

});//END OF window.addEventListener