//*********************************** VARIABLES ******************************************
var root;
var image;
var cropper;
var result;

var filter;

var pathStorage = "";

var currentImg;

var img;
//*********************************** funcImg ******************************************

var funcImg = {
    //Set image
    loadRotar: function () {
        $('body').on('click', '.btn-rotar', function (e) {
            var degree = $(this).data('option');
            cropper.rotate(degree);
            funcImg.updateRotate(true);

        });
    },
    loadZoom: function () {
        $('body').on('click', '.btn-zoom', function (e) {
            var val = $(this).data('option');
            if (val == "1") {
                cropper.zoom(0.1);
            } else {
                cropper.zoom(-0.1)
            }
            console.log(e.type, e.detail.ratio);

        });
    },
    loadColor: function () {
        $('body').on('change', '.filter-color', function (e) {
            funcImg.updateFilter();
        });
    },
    loadRecortar: function () {
        $('body').on('click', '.btn-recortar', function (e) {
            funcImg.updateFilter();

            var canvas = cropper.getCroppedCanvas({
                maxWidth: 1280,
                maxHeight: 1280,
                fillColor: '#fff',
                imageSmoothingEnabled: false,
                imageSmoothingQuality: 'medium'
            });
            var croppedImageDataURL = canvas.toDataURL("image/jpg", 0.5);
            img.src = croppedImageDataURL;

            $('.btn-save').removeAttr('disabled');

        });
    },
    loadRotate: function () {
        $('body').on('change', '#angle', function (e) {
            var angle = $('#angle').val();
            cropper.rotateTo(angle);
            funcImg.updateRotate(false);
        });
    },
    loadReset: function () {
        $('body').on('click', '.btn-reset', function (e) {
            funcImg.cleanResult();
            funcImg.resetFilter();
            cropper.reset();
            funcImg.updateRotate(true);
            
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
            zoomable: true,
            zoomOnWheel: false,
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

            var canvas = document.querySelector("canvas");
            canvas.toBlob((blob) => {
                
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
                                funcGenerico.mostrarMensaje("Imagen actualizada correctamente", "success");
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
            }, "image/jpeg"); //"image/webp"
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
            funcImg.cleanResult();
            funcImg.resetFilter();
            funcImg.loadCropper();
            funcImg.updateRotate(true);
            $('#modalEditImage').modal('show');
        });
        $('#modalEditImage').on('hidden.bs.modal', function (e) {
            cropper.destroy();
            funcImg.changeStateBtns(false);
        })
    },
    //Load image canvas
    draw: function () {
        var canvas = document.querySelector("canvas"),
            ctx = canvas.getContext("2d");
        
        canvas.width = this.width;
        canvas.height = this.height;

        if (typeof ctx.filter !== "undefined") {
            ctx.filter = filter != "" ? filter : "none";
            ctx.drawImage(this, 0, 0);
        }
        else {
            ctx.drawImage(this, 0, 0);
        }
        var croppedImageDataURL = canvas.toDataURL("image/jpg");
        result.html($('<img>').attr('src', croppedImageDataURL).addClass('img-edit'));

    },
    //Extra
    updateFilter: function () {
        var brightness = $('#brightness').val();
        var contrast = $('#contrast').val();
        var saturate = $('#saturate').val();

        filter = "brightness(" + brightness + "%) " + "contrast(" + contrast + "%) " + " saturate(" + saturate + "%)";

        $(".cropper-container.cropper-bg").css("filter", filter);
        //$("#result img").css("filter", filter);

    },
    resetFilter: function () {
        $('#brightness').val(100);
        $('#contrast').val(100);
        $('#saturate').val(100);
        funcImg.updateFilter();
    },
    updateRotate: function (bar) {
        var rotate = cropper.getData().rotate;
        if (bar) {
            $('#angle').val(rotate)
        }
        $('#dataRotate').html(rotate + "°") ;
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
        funcImg.loadRotate();
        funcImg.loadZoom();
        funcImg.loadRecortar();
        funcImg.loadReset();
        funcImg.loadSave();
        funcImg.loadColor();
        funcImg.loadShowModal();

        //Load image
        img = new Image();
        img.crossOrigin = "";
        img.onload = funcImg.draw;
    }
};

//************************************** ON READY **********************************************
$(function () {
    funcImg.init();
});


/*
 * 

        var img = new Image();
        img.crossOrigin = "";
        img.onload = draw;
        img.src = "https://i.imgur.com/2jjLqzk.jpg";

        function draw() {
            var canvas = document.querySelector("canvas"),
                ctx = canvas.getContext("2d");

            //canvas.width = this.width;
            //canvas.height = this.height;

            // filter
            if (typeof ctx.filter !== "undefined") {
                ctx.filter = filter;
                ctx.drawImage(this, 0, 0);
            }
            else {
                ctx.drawImage(this, 0, 0);
                // TODO: manually apply filter here.
            }

            document.querySelector("imgResult").src = canvas.toDataURL();
        } */

