$(document).ready(function () {
    $('.k-upload-button').addClass('btn-primary');
});

function validateDisable() {
    $("#Validate").data("kendoButton").enable(false);
    hideValidationMessage()
}

function uploadedIsCompleted(e) {
    hideValidationMessage();

    if (!!e && !!e.response) {
        if (e.response.isSuccess != undefined) {
            $('#IsUploadSuccessed').val(e.response.isSuccess);

            if (!!e.response.fileName) {
                $('#FileSavedName').val(e.response.fileName);
            }
            else {
                $('#FileSavedName').val("");
            }

            $("#Validate").data("kendoButton").enable(e.response.isSuccess);

            if (e.response.isSuccess) {
                $("#Validate").addClass("btn-primary");
            } else {
                $("#Validate").removeClass("btn-primary");
            }
        }
    }
}

function hideValidationMessage() {
    if (!$("#AlertDanger").hasClass("hidden")) {
        $("#AlertDanger").addClass("hidden");
    }
    if (!$("#AlertSuccess").hasClass("hidden")) {
        $("#AlertSuccess").addClass("hidden");
    }
}

function validateXML(e) {
    if ($('#IsUploadSuccessed').val()) {
        $.ajax({
            url: "Home/ValidateXmlByXsd",
            method: "POST",
            data: {
                "fileName": $('#FileSavedName').val()
            },
            success: function (data) {
                if (data) {
                    var result = JSON.parse(data);
                    if (result.isSuccess) {
                        if ($("#AlertSuccess").hasClass("hidden")) {
                            $("#AlertSuccess").removeClass("hidden");
                        }
                        if (!$("#AlertDanger").hasClass("hidden")) {
                            $("#AlertDanger").addClass("hidden");
                        }

                        $("#AlertSuccess .alert-success").text(result.message);
                    }
                    if (result.isSuccess != undefined && !result.isSuccess) {
                        if (!$("#AlertSuccess").hasClass("hidden")) {
                            $("#AlertSuccess").addClass("hidden");
                        }
                        if ($("#AlertDanger").hasClass("hidden")) {
                            $("#AlertDanger").removeClass("hidden");
                        }

                        $("#AlertDanger .alert-danger").text(result.message);
                    }
                }
            }
        });
    }
}