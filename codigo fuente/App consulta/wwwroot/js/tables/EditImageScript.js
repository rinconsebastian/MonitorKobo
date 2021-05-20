//*********************************** VARIABLES ******************************************
var root;
var image;
var cropper;
var result;

var filter;

var pathStorage = "";

var currentImg;
//*********************************** funcImg ******************************************

var funcImg = {
    //Set image
    loadRotar: function () {
        $('body').on('click', '.btn-rotar', function (e) {
            var degree = $(this).data('option');
            cropper.rotate(degree);
        });
    },
    loadColor: function () {
        $('body').on('change', '.filter-color', function (e) {
            funcImg.updateFilter();
        });
    },
    loadRecortar: function () {
        $('body').on('click', '.btn-recortar', function (e) {

            var canvas = cropper.getCroppedCanvas();
            var  ctx = canvas.getContext('2d');
            ctx.filter = filter;

            var croppedImageDataURL = canvas.toDataURL("image/png");
            result.html($('<img>').attr('src', croppedImageDataURL).addClass('img-edit'));
            $('.btn-save').removeAttr('disabled');

        });
    },
    loadReset: function () {
        $('body').on('click', '.btn-reset', function (e) {
            funcImg.cleanResult();
            $('#brightness').val(100);
            $('#contrast').val(100);
            $('#saturate').val(100);
            funcImg.updateFilter();
            cropper.reset();
        });
    },
    //Save
    loadCropper: function () {
       image = document.getElementById('image');
        cropper = new Cropper(image, {
            minContainerWidth: 450,
            minContainerHeight: 450,
            autoCropArea: 1,
            movable: false,
            scalable: false,
            zoomable: false,
            zoomOnWheel: true,
           toggleDragModeOnDblclick: false,
           autoCrop: false,
           ready() {
               funcImg.changeStateBtns(true);
               this.cropper.crop();
           },
        });
    },
    loadSave: function () {
        $('body').on('submit', '#formUpload', function (e) {
            e.preventDefault();
            var formulario = $(this).closest('form');

            cropper.getCroppedCanvas({
                maxWidth: 4096,
                maxHeight: 4096,
                fillColor: '#fff',
                imageSmoothingEnabled: false,
                imageSmoothingQuality: 'high'
            });

            cropper.getCroppedCanvas().toBlob((blob) => {

                var formData = new FormData(formulario[0]);
                var filename = $('#inputFilename').val();

                $('#uploadingInfo').html('Cargando <img src="' + root + 'images/ajax-loader.gif">');
                $('.btn-save').attr('disabled', 'disabled');

                formData.append('file', blob);
                $.ajax({
                    type: "POST",
                    url: formulario.attr('action'),
                    data: formData,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data != null) {
                            if (data.success) {
                                var d = new Date();
                                $("#" + currentImg).attr("src", pathStorage + filename + "&time=" + d.getTime());
                                $('#modalEditImage').modal('hide');
                                cropper.destroy();
                            } else {
                                $('#uploadingInfo').html('<div class="text-danger">' + data.message + '</div>');
                                $('.btn-save').removeAttr('disabled');
                            }
                        } else {
                            $('#uploadingInfo').html('<div class="text-danger">Error en la respuesta del servidor.</div>');
                            $('.btn-save').removeAttr('disabled');
                        }
                    },
                    error: function (error) {
                        $('.btn-save').removeAttr('disabled');
                        $('#uploadingInfo').html('<div class="text-danger">Error al cargar el archivo.</div>');
                    }
                });
            });
        });
    },
    //Modal
    loadShowModal: function () {
        $('body').on('click', '.showEditModal', function (e) {
            var filename = $(this).data('filename');
            var d = new Date();

            currentImg = $(this).data('img');

            $('#image').attr('src', pathStorage + filename + "&time=" + d.getTime());
            $('#inputFilename').val(filename);
            funcImg.loadCropper();
            funcImg.cleanResult();
            $('#modalEditImage').modal('show');
        });
        $('#modalEditImage').on('hidden.bs.modal', function (e) {
            cropper.destroy();
            funcImg.changeStateBtns(false);
        })
    },
    //Extra
    updateFilter: function () {
        var brightness = $('#brightness').val();
        var contrast = $('#contrast').val();
        var saturate = $('#saturate').val();

        filter = "brightness(" + brightness + "%) " + "contrast(" + contrast + "%) " + " saturate(" + saturate + "%)";

        $(".cropper-container.cropper-bg").css("filter", filter);
        //$("#result").css("filter", filter);

    },
    changeStateBtns: function (state) {
        
        if (state) {
            $('.btn-img').removeAttr('disabled');
        } else {
            $('.btn-img').attr('disabled', 'disabled');
        }
    },
    cleanResult: function () {
        $('#uploadingInfo').html('');
        $('.btn-save').attr('disabled','disabled');
        $('#result').html('<div class="preview">VISTA PREVIA</div>');
    },
    init: function () {
        // Carga las variables de configuración.
        root = $('#Root').val();

        pathStorage = $('#PathStorage').val() + "?filename=";

        result = $('#result');

        //funcImg.loadCropper();
        funcImg.loadRotar();
        funcImg.loadRecortar();
        funcImg.loadReset();
        funcImg.loadSave();
        funcImg.loadColor();
        funcImg.loadShowModal();
    }
};

//************************************** ON READY **********************************************
$(function () {
    funcImg.init();
});




