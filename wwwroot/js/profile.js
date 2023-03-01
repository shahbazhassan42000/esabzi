window.addEventListener("load", () => {
    const update_profile_btn = document.getElementById("update_profile_btn");
    const profile_file = document.getElementById("profile_file");
    const user_profile_picture = document.getElementById("user_profile_picture");
    const backendURL = "http://localhost:8080";
    const usersURL = "api/user";
    //get user from cookie
    const user = JSON.parse(getCookie("user"));
    

    update_profile_btn.addEventListener("click", () => profile_file.click());

    //update the profile
    profile_file.addEventListener("change", () => {
        let payload = new FormData();
        let file = profile_file.files[0];
        payload.append("image", file);
        const imgBBURL = "https://api.imgbb.com/1/upload";
        const apiKey = "4d0eff80cd1cea3d5f1f524ac3a0808a";
        console.log(user);
        //axios.post(imgBBURL + "?key=" + apiKey, payload)
        //    .then(res => {
        //        console.log("Profile updated successfully!");
        //        console.log('response', res)
        //        const image = res.data.data.image.url;
        //        console.log('response URL: ', image);
        //        //update the current user profile picture
        //        user.Picture = image;
        //        //update the profile picture in the database
        //        axios.put(`${backendURL}/${usersURL}`, { image })

        //    }).catch(err => {
        //        console.log("ERROR: ", err);
        //    });
    });
});