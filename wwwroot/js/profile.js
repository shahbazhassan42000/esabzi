window.addEventListener("load", () => {
    const getCookie = (cname) => {
        const name = cname + "=";
        const decodedCookie = decodeURIComponent(document.cookie);
        const ca = decodedCookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
    
    const update_profile_btn = document.getElementById("update_profile_btn");
    const profile_file = document.getElementById("profile_file");
    const user_profile_picture = document.getElementById("user_profile_picture");
    const update_profile_loader = document.getElementById("update_profile_loader");
    const update_btn = document.getElementById("update_btn");
    const user_address = document.getElementById("user_address");
    const loader = document.getElementById("loader");
    const profile_image = document.getElementById("profile_image");
    const msg = document.getElementById("msg");
    const msg_text = msg.firstElementChild;
    const backendURL = "http://localhost:8080";
    const usersURL = "api/user";
    //get user from cookie
    const user = JSON.parse(getCookie("user"));


    //update user address
    update_btn.addEventListener("click", () => {
        console.log(user);
        loader.style.display = "flex";
        const address = user_address.value;
        axios.patch(`${backendURL}/${usersURL}/address/${user.Id}`, JSON.stringify(address), {
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(res => {
            msg.style.display = "flex";
            msg_text.style.backgroundColor = "#ffc700";
            msg_text.style.color = "#172B4D"
            user.Address = address;
            //update cookies
            document.cookie = `user=${encodeURIComponent(JSON.stringify(user))}; path=/`;
            msg_text.innerHTML = "Profile updated successfully";
            console.log(res);
        }).catch(err => {
            msg.style.display = "flex";
            msg_text.style.backgroundColor = "#FF0000";
            msg_text.style.color = "#FFFFFF"
            msg_text.innerHTML = "Error while updating profile";
            console.log(err);
        }).finally(() => {
            loader.style.display = "none";
            setTimeout(() => msg.style.display = "none", 3000);
        })
    });
        


    update_profile_btn.addEventListener("click", () => profile_file.click());

    //update the profile picture
    profile_file.addEventListener("change", () => {
        let payload = new FormData();
        let file = profile_file.files[0];
        payload.append("image", file);
        const imgBBURL = "https://api.imgbb.com/1/upload";
        const apiKey = "4d0eff80cd1cea3d5f1f524ac3a0808a";
        console.log(user);
        //show loader by removing hidden class and disable update_profile_btn
        update_profile_loader.classList.remove("hidden");
        update_profile_btn.disabled = true;
        
        axios.post(imgBBURL + "?key=" + apiKey, payload)
            .then(res => {
                console.log("Profile updated successfully!");
                console.log('response', res)
                const image = res.data.data.image.url;
                console.log('response URL: ', image);
                //update the current user profile picture
                user.Picture = image;
                //update the profile picture in the database
                axios.patch(`${backendURL}/${usersURL}/${user.Id}`, JSON.stringify(image), {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(res => {
                    console.log('response', res);
                    //update user_profile_picture
                    user_profile_picture.src = image;
                    profile_image.src = image;
                    //update cookies
                    document.cookie = `user=${encodeURIComponent(JSON.stringify(user))}; path=/`;
                    //hide loader by adding hidden class and enable update_profile_btn
                    update_profile_loader.classList.add("hidden");
                    update_profile_btn.disabled = false;
                }).catch(err => {
                    console.log("ERROR: ", err);
                });

            }).catch(err => {
                console.log("ERROR: ", err);
            });
    });
});