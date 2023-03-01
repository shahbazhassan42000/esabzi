window.addEventListener("load", () => {
    const backendURL = "http://localhost:8080";
    const usersURL = "api/user";
    const loginURL = 'login';
    const signupURL = 'users';
    const EXPIRY_TIME = "2592000"; //30 Days

    const patterns = {
        email: /^([a-z\d]+)@([a-z\d-]+)\.([a-z]{2,8})(\.[a-z]{2,8})?$/, contact: /^(\+92)(3)([0-9]{9})$/,
    };
    let users = [];
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
    const logout_form = document.getElementById("logout_form");

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
            const Username = username_inp.value;
            const Password = password_inp.value;
            const data = {
                Username, Password
            };
            login_btn.disabled = true;
            loader.style.display = "flex";
            console.log("request load: ", data);
            axios.post(`${backendURL}/${usersURL}/${loginURL}`, data)  //api/user/login
            .then(res => {
                    console.log("response: ", res);
                if (res.status === 200) {
                    //store in cookies
                    document.cookie = `token=${res.data.token}; max-age=${EXPIRY_TIME}; path=/;`;
                    show_msg("Login successful", "success", 5000);
                    login_form.reset();
                    window.location.href = "/";
                } else {
                    login_btn.disabled = false;
                    show_msg("Invalid Credentials", "error", 5000);
                }
                loader.style.display = "none";  
                })
                .catch(err => {
                    login_btn.disabled = false;
                    show_msg(err.response.status === 404 || err.response.status === 422 ? "Invalid Credentials" : "ERROR!!! While login, Please try again", "error", 5000);
                    console.log(err);
                    loader.style.display = "none";
                });
        });
    }

    if (signup_form) {
        //get all usernames
        (() => {
            axios.get(`${backendURL}/${usersURL}/usernames`)
                .then(res => {
                    if (res.status === 200) {
                        users = res.data; //arrays of users
                        console.log(users);
                    } else {
                        console.log("ERROR");
                    }

                })
                .catch(err => console.log(err.message));
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

        const validate_contact = () => {
            if (patterns.contact.test(contact_inp.value.trim())) {
                contact_inp.nextElementSibling.style.visibility = "hidden";
                return true;
            } else {
                contact_inp.nextElementSibling.style.visibility = "visible";
                return false;
            }
        }

        const validate_email = () => {
            //check if email is already taken
            if (patterns.email.test(email_inp.value.trim()) && !users.find(user => user.email === email_inp.value.trim())) {
                email_inp.nextElementSibling.style.visibility = "hidden";
                return true;
            } else {
                //if email is not valid then set invalid msg && if email already taken then set that message
                if (!patterns.email.test(email_inp.value.trim())) {
                    email_inp.nextElementSibling.innerText = "Email should be valid, e.g. me@mydomain.com";
                }
                if (users.find(user => user.email === email_inp.value.trim())) {
                    email_inp.nextElementSibling.innerText = "Email already taken";
                }
                email_inp.nextElementSibling.style.visibility = "visible";
                return false;
            }
        }

        const validate_username = () => {
            if (username_inp.value.trim() === "") return false;
            //check if username is already taken
            if (users.find(user => user.username === username_inp.value.trim())) {
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
            const Name = name_inp.value;
            const Email = email_inp.value;
            const ContactNo = contact_inp.value;
            const Username = username_inp.value;
            const Password = password_inp.value;
            const Address = address_inp.value;
            const data = {
                Name, Email, ContactNo, Username, Password, Address
            };
            signup_btn.disabled = true;
            loader.style.display = "flex";
            console.log("request load: ", data);
            axios.post(`${backendURL}/${usersURL}/${signupURL}`, data) //api/user/users
                .then(res => {
                    console.log("response: ", res);
                    if (res.status === 200) {
                        //display success message for 3 seconds
                        show_msg("Signup successful", "success", 5000);
                        signup_form.reset();
                        window.location.href = "/auth/login";
                    } else {
                        signup_btn.disabled = false;
                        show_msg("Please fill all the fields", "error", 5000);
                    }
                    loader.style.display = "none";
                })
                .catch(err => {
                    signup_btn.disabled = false;
                    show_msg(err.response.status === 422 ? "Please fill all the fields" :"ERROR!!! While signup, Please try again", "error", 5000);
                    console.log(err);
                    loader.style.display = "none";
                });


        });//end of signup form submit
    }


    if (login_form || signup_form) {
        //show-hide password button functionality
        pwd_toggle_btn.addEventListener("click", () => {
            if (password_inp.type === "password") {
                password_inp.type = "text";
                pwd_toggle_btn.classList.remove("fa-eye-slash");
                pwd_toggle_btn.classList.add("fa-eye");
            } else {
                password_inp.type = "password";
                pwd_toggle_btn.classList.remove("fa-eye");
                pwd_toggle_btn.classList.add("fa-eye-slash");

            }
        });
    }


    //logout button
    if (logout_form) {
        logout_form.addEventListener("submit", (event) => {
            event.preventDefault();
            //remove token and user from cookies
            document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
            document.cookie = "user=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

            //redirect to login page
            window.location.href = "/auth/login";



        });


    }

    //display message for given time
    const show_msg = (message, type, time) => {
        msg.innerHTML = message;
        msg.style.color = type === "success" ? "#8bc543" : "#FF0000";
        msg.style.visibility = "visible";
        setTimeout(() => msg.style.visibility = "hidden", time);
    };
});//END OF window.addEventListener