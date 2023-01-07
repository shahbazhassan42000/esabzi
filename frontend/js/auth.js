window.addEventListener("load", () => {
    const backendURL = "http://localhost:3000";
    const loginURL = '/api/v0.1/auth/login';
    const signupURL = '/api/v0.1/auth/signup';
    const pwd_toggle_btn = document.getElementById("pwd_toggle_btn");
    const username_inp = document.getElementById("username");
    const password_inp = document.getElementById("password");
    const login_btn = document.getElementById("login_btn");
    const login_form = document.getElementById("login_form");
    const msg= document.getElementById("msg");
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

    //disable login button if username or password is empty
    login_form.addEventListener("input", (event) => {
        login_btn.disabled = !(username_inp.value.trim() && password_inp.value.trim());
        username_inp.style.borderColor=username_inp.value.trim()===""?"#FF0000":"#8bc543";
        password_inp.style.borderColor=password_inp.value.trim()===""?"#FF0000":"#8bc543";

    });


    //login form submit
    login_form.addEventListener("submit", (e) => {
        e.preventDefault();
        const username = username_inp.value;
        const password = password_inp.value;
        const data = {
            username,
            password
        };
        login_btn.disabled = true;
        console.log("request load: ", data);
        fetch(backendURL + loginURL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        }).then(res => res.json())
            .then(data => {
                console.log("response: ", data);
                    if (data.success) {
                        //display success message for 3 seconds
                        show_msg("Login successful", "success",3000);
                        login_form.reset();
                        window.location.href = "/";
                    } else {
                        login_btn.disabled = false;
                        show_msg("Invalid Credentials", "error",3000);
                    }
                }
            )
            .catch(err => {
                login_btn.disabled = false;
                show_msg("ERROR!!! While login, Please try again","error",3000);
                console.log(err);
            });
    });

    const show_msg =(message,type,time) => {
        msg.innerHTML = message;
        msg.style.color=type==="success"?"#8bc543":"#FF0000";
        msg.style.display="block";
        setTimeout(() => {
                msg.style.display = "none";
            }
            , time);
    }

});//END OF window.addEventListener