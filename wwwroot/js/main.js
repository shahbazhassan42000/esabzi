window.addEventListener("load", function () {
    const profile_image = document.getElementById("profile_image");
    const profile_dropdown = document.getElementById("profile_dropdown");
    const close_profile_dropdown = document.getElementById("close_profile_dropdown");

    //ading event listener to profile image to toggole profile dropdown
    profile_image.addEventListener("click", () => profile_dropdown.style.display = profile_dropdown.style.display=="none"?"block":"none" );

    //adding event listener to close profile dropdown to close profile dropdown
    close_profile_dropdown.addEventListener("click", () => profile_dropdown.style.display = "none");
    


    
});//END of Window Load