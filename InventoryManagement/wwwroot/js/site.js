// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#Quantity").val(1);
$("#Price").val(1);
$("#inputDiscount").val(0);
$("#Vat").val(0);
$("#inputAfter").val(0);

$("#inputTotalAmount").val(1);

$(document).ready(function () {
       
    $(".form-group").on('input', '.input1', function () {
       // code logic here
        var getValue=$(this).val();
        console.log(getValue);

        var inputTotalAmount = $("#inputTotalAmount").val();
        
        $(".form-group .input1").each(function () {
            var get_textbox_value = $(this).val();
            if ($.isNumeric(get_textbox_value)) {
                inputTotalAmount *= parseFloat(get_textbox_value);
            }
            console.log(inputTotalAmount);
        });
        
        $("#inputTotalAmount").val(inputTotalAmount);

    });
    $(".form-group").on('input', '#inputVat', function () {
        // code logic here
        var getValue = $(this).val();
        console.log(getValue);
        var inputTotalAmount = parseFloat($("#Quantity").val()) * parseFloat($("#Price").val());
        console.log(inputTotalAmount);

        $(".form-group #inputVat").each(function () {
            var get_textbox_value = $(this).val();
            if ($.isNumeric(get_textbox_value)) {
                var vat = (parseFloat(get_textbox_value) / 100) * inputTotalAmount;
                inputTotalAmount -= vat;
            }
            console.log(get_textbox_value);
        });

        $("#inputTotalAmount").val(inputTotalAmount);


    });

    $(".form-group").on('input', '#inputDiscount', function () {
        // code logic here
        var getValue = $(this).val();
        console.log(getValue);
        if ($("#inputVat").val())
            var inputTotalAmount = parseFloat($("#Quantity").val()) * parseFloat($("#Price").val()) - parseFloat($("#inputVat").val()/100);
        else
            var inputTotalAmount = parseFloat($("#Quantity").val()) * parseFloat($("#Price").val());
        console.log(inputTotalAmount);
        var PriceAfterDiscount = 0;
        $(".form-group #inputDiscount").each(function () {
            var get_textbox_value = $(this).val();
            if ($.isNumeric(get_textbox_value)) {
                inputTotalAmount -= parseFloat(get_textbox_value);
                PriceAfterDiscount = inputTotalAmount;
            }
            console.log(PriceAfterDiscount);
            console.log(inputTotalAmount);
        });
        $("#inputAfter").val(PriceAfterDiscount);
       
        $("#inputTotalAmount").val(inputTotalAmount);

    });

   

});

$(document).ready(function () {

    $(".form-group").on('input', '.input1', function () {
        // code logic here
        var getValue = $(this).val();
        console.log(getValue);

        var inputTotalAmount = 1;

        $(".form-group .input1").each(function () {
            var get_textbox_value = $(this).val();
            if ($.isNumeric(get_textbox_value)) {
                inputTotalAmount *= parseFloat(get_textbox_value);
            }
            console.log(inputTotalAmount);
        });

        $("#inputTotalAmount1").val(inputTotalAmount);

    });
    $(".form-group").on('input', '#inputVat1', function () {
        // code logic here
        var getValue = $(this).val();
        console.log(getValue);
        var inputTotalAmount = parseFloat($("#Quantity1").val()) * parseFloat($("#Price1").val());
        console.log(inputTotalAmount);

        $(".form-group #inputVat1").each(function () {
            var get_textbox_value = $(this).val();
            if ($.isNumeric(get_textbox_value)) {
                var vat = (parseFloat(get_textbox_value) / 100) * inputTotalAmount;
                inputTotalAmount -= vat;
            }
            console.log(get_textbox_value);
        });

        $("#inputTotalAmount1").val(inputTotalAmount);


    });

    $(".form-group").on('input', '#inputDiscount1', function () {
        // code logic here
        var getValue = $(this).val();
        console.log(getValue);
        if ($("#inputVat1").val())
            var inputTotalAmount = parseFloat($("#Quantity1").val()) * parseFloat($("#Price1").val()) - parseFloat($("#inputVat1").val() / 100);
        else
            var inputTotalAmount = parseFloat($("#Quantity1").val()) * parseFloat($("#Price1").val());
        console.log(inputTotalAmount);
        var PriceAfterDiscount = 0;
        $(".form-group #inputDiscount").each(function () {
            var get_textbox_value = $(this).val();
            if ($.isNumeric(get_textbox_value)) {
                inputTotalAmount -= parseFloat(get_textbox_value);
                PriceAfterDiscount = inputTotalAmount;
            }
            console.log(PriceAfterDiscount);
            console.log(inputTotalAmount);
        });
        $("#inputAfter1").val(PriceAfterDiscount);

        $("#inputTotalAmount1").val(inputTotalAmount);

    });



});