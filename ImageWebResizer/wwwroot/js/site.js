function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }

    $.ajax(
        {
            url: "/",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {

                console.log(data);
                for (var i = 0; i < data.stored.length; i++) {

                    createList(data.stored[i]);
                    processImage(data.stored[i]);
                }
            }
        }
    );
}
function processImage(picture) {
    $.ajax(
        {
            url: "/Home/ResizeTo300/" + picture.id,
            //            data: id,
            //        processData: false,
            //        contentType: false,
            type: "GET",
            success: function (data) {
                console.log(data);
                $("#l-" + data.picture.id + " .l-sizes").html(
                    '<a href="/Home/GetImage/' + data.picture.id + '" target="_blank">Original</a> <a href="/Home/GetImage/' + data.picture.id + '?size=300" target="_blank">Resized300</a>'
                );

            }
        }
    );
}

function createList(picture) {
    $("#uploads #images").append(
        '<li id="l-' + picture.id + '"><span class="l-name">' + picture.originalName + '</span> <span class="l-sizes">Processing...</span></li>'
    );

    $("#uploads").fadeIn('slow');
}