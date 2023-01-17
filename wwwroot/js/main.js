window.addEventListener("load", () => {
    const backendURL = "http://localhost:3000";
    const loginURL = '/api/v0.1/auth/login';
    const signupURL = '/api/v0.1/auth/signup';

    const patterns = {
        email: /^([a-z\d]+)@([a-z\d-]+)\.([a-z]{2,8})(\.[a-z]{2,8})?$/,
        contact: /^(\+92)(3)([0-9]{9})$/,
    };
    let usernames = [];
    const pwd_toggle_btn = document.getElementById("pwd_toggle_btn");
    const name_inp = document.getElementById("name");
    const email_inp = document.getElementById("email");
    const contact_inp = document.getElementById("contact");
    const username_inp = document.getElementById("username");
    const password_inp = document.getElementById("password");
    const address_inp = document.getElementById("address");
    const login_btn = document.getElementById("login_btn");
    const signup_btn = document.getElementById("signup_btn");
    const login_form = document.getElementById("login_form");
    const signup_form = document.getElementById("signup_form");
    const msg = document.getElementById("msg");
    const loader = document.getElementById("loader");

    //login page functions
    if (login_form) {
        //disable login button if username or password is empty
        login_form.addEventListener("input", () => {
            login_btn.disabled = !(username_inp.value.trim() && password_inp.value.trim());
            username_inp.style.borderColor = username_inp.value.trim() === "" ? "#FF0000" : "#8bc543";
            password_inp.style.borderColor = password_inp.value.trim() === "" ? "#FF0000" : "#8bc543";

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
            loader.style.display = "flex";
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
                            //TODO: save AWT token in local storage
                            //display success message for 3 seconds
                            show_msg("Login successful", "success", 5000);
                            login_form.reset();
                            window.location.href = "/";
                        } else {
                            login_btn.disabled = false;
                            show_msg("Invalid Credentials", "error", 5000);
                        }
                        loader.style.display = "none";
                    }
                )
                .catch(err => {
                    login_btn.disabled = false;
                    show_msg("ERROR!!! While login, Please try again", "error", 5000);
                    console.log(err);
                    loader.style.display = "none";
                });
        });
    }

    if (signup_form) {
        //get all usernames
        (() => {
            //TODO: get all usernames from backend
            // fetch(backendURL + '/api/v0.1/all_usernames')
            //     .then(res => res.json())
            //     .then(data => {
            //         usernames = data;
            //         console.log(usernames);
            //     })
            //     .catch(err => console.log(err));
        })();

        //disable signup button if any of the input is empty
        signup_form.addEventListener("input", () => {
            signup_btn.disabled = !(name_inp.value.trim() && validate_email() && validate_contact() && validate_username() && validate_password() && address_inp.value.trim());
            name_inp.style.borderColor = name_inp.value.trim() === "" ? "#FF0000" : "#8bc543";
            email_inp.style.borderColor = validate_email() ? "#8bc543" : "#FF0000";
            contact_inp.style.borderColor = validate_contact() ? "#8bc543" : "#FF0000";
            username_inp.style.borderColor = validate_username() ? "#8bc543" : "#FF0000";
            password_inp.style.borderColor = validate_password() ? "#8bc543" : "#FF0000";
            address_inp.style.borderColor = address_inp.value.trim() === "" ? "#FF0000" : "#8bc543";
        });

        const validate_email = () => {
            if (patterns.email.test(email_inp.value.trim())) {
                email_inp.nextElementSibling.style.visibility = "hidden";
                return true;
            } else {
                email_inp.nextElementSibling.style.visibility = "visible";
                return false;
            }
        }

        const validate_contact = () => {
            if (patterns.contact.test(contact_inp.value.trim())) {
                contact_inp.nextElementSibling.style.visibility = "hidden";
                return true;
            } else {
                contact_inp.nextElementSibling.style.visibility = "visible";
                return false;
            }
        }

        const validate_username = () => {
            if (username_inp.value.trim() === "") return false;
            if (usernames.includes(username_inp.value.trim())) {
                username_inp.nextElementSibling.style.visibility = "visible";
                return false;
            } else {
                username_inp.nextElementSibling.style.visibility = "hidden";
                return true;
            }
        }

        const validate_password = () => {
            if (password_inp.value.trim() !== "" && password_inp.value.trim().length >= 8) {
                password_inp.nextElementSibling.style.visibility = "hidden";
                return true;
            } else {
                password_inp.nextElementSibling.style.visibility = "visible";
                return false;
            }
        }

        //signup form submit
        signup_form.addEventListener("submit", (e) => {
            e.preventDefault();
            const name = name_inp.value;
            const email = email_inp.value;
            const contact = contact_inp.value;
            const username = username_inp.value;
            const password = password_inp.value;
            const address = address_inp.value;
            const data = {
                name,
                email,
                contact,
                username,
                password,
                address
            };
            signup_btn.disabled = true;
            loader.style.display = "flex";
            console.log("request load: ", data);
            fetch(backendURL + signupURL, {
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
                            show_msg("Signup successful", "success", 5000);
                            signup_form.reset();
                            window.location.href = "/login";
                        } else {
                            signup_btn.disabled = false;
                            show_msg("Invalid Information", "error", 5000);
                        }
                        loader.style.display = "none";
                    }
                )
                .catch(err => {
                    signup_btn.disabled = false;
                    show_msg("ERROR!!! While signup, Please try again", "error", 5000);
                    console.log(err);
                    loader.style.display = "none";
                });


        });//end of signup form submit
    }


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

    //display message for given time
    const show_msg = (message, type, time) => {
        msg.innerHTML = message;
        msg.style.color = type === "success" ? "#8bc543" : "#FF0000";
        msg.style.visibility = "visible";
        setTimeout(() => msg.style.visibility = "hidden", time);
    };
});//END OF window.addEventListener