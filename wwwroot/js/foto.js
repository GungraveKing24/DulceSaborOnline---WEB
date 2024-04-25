const fileInput = document.getElementById("photo");
const imagePreview = document.getElementById("previewImage");
const defaultText = document.querySelector(".image-preview__default-text");

fileInput.addEventListener("change", function () {
    const file = this.files[0];

    if (file) {
        const reader = new FileReader();

        reader.addEventListener("load", function () {
            imagePreview.setAttribute("src", this.result);
            defaultText.style.display = "none";
            imagePreview.style.display = "block";
        });

        reader.readAsDataURL(file);
    } else {
        imagePreview.setAttribute("src", "");
        imagePreview.style.display = "none";
        defaultText.style.display = "block";
    }
});