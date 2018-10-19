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
                for (var i = 0; i < data.stored.length; i++) {

                    createList(data.stored[i]);
                    processImage(data.stored[i]);
                }
                input.value = "";
            }
        }
    );
}
function processImage(picture) {
    var rowId = "#l-" + picture.id;
    $(rowId+ " .l-sizes").html("Processing...");
    $.ajax(
        {
            url: "/Home/ResizeTo300/" + picture.id,
            type: "GET",
            success: function (data) {
                $(rowId + " .l-original").html(getImageLink("Link (" + data.picture.lengthKB+ " KB)", data.picture));
                $(rowId + " .l-300").html(getImageLink("Link (" + data.picture.length300KB + " KB)", data.picture,300))
            }

        }
    );
}

function createList(picture) {
    var row = '<tr id="l-' + picture.id + '"><td class="l-name">' + picture.originalName + '</td> '
    row += '<td>' + picture.dateUploadFormated +'<td>'+picture.width+' x '+picture.height+'</td></td><td class="l-sizes l-original"></td><td class="l-sizes l-300"></td></tr>'; 
    $("#images").prepend(row);
    $("#uploads").fadeIn('slow');
}

function getImageLink(linkText,picture,size=null) {
    var link = $("<a />", {
        href: "/Home/GetImage/" + picture.id+(size===null?"":"?size="+size),
        text: linkText,
        target:"_blank"
    });
    return link;
}