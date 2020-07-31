/************* Event*****************/

$('.close').click(function () {
    ReleaseAll();
})

$('.release').click(function () {
    ReleaseAll();
})

function ReleaseAll() {
    $('#template_form').data('bootstrapValidator').resetForm();
}