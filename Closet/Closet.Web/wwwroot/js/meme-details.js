$(document).ready(function () {
    $("#meme-upvote").click(function (event) {
        $.post("/Memes/AddOrUpdateVote",
            {
                MemeId: @Model.Id,
        Value: 1
            },
    function (data, status) {
        $("#meme-votes").text(data);
    });
        });

$("#meme-downvote").click(function (event) {
    $.post("/Memes/AddOrUpdateVote",
        {
            MemeId: @Model.Id,
    Value: -1
            },
    function (data, status) {
        $("#meme-votes").text(data);
    });
        });
    });