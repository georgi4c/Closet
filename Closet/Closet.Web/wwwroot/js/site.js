$(document).ready(function () {
    $("#meme").memeGenerator();
});

function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#meme').attr('src', e.target.result).show();
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgInp").change(function () {
    readURL(this);
});