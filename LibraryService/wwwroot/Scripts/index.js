(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

window.onload = function () {
    $('.toast').toast({
        delay: 5000
    });

    $('#overdueBooksBtn').on('click', function () {
        $('#patron tbody tr').remove();
        $.getJSON('api')
            .done(function (data) {
                $.each(data, function (key, item) {
                    $('#patron tbody').append('<tr><td>' + item.firstName + '</td>' + '<td>' + item.lastName + '</td>' + '<td>' + item.checkoutDate + '</td>' + '<td>' + item.dueDate + '</td>' + '<td>' + item.areaCode + '</td>' + '<td>' + item.phone + '</td></tr>');
                });
            });

        $('#patron').show();
    });

    $('#addNewPatronBtn').on('click', function () {
        var firstName = $('#firstName').val();
        var lastName = $('#lastName').val();

        if (firstName.trim() === "" || lastName.trim() === "") {
            return;
        }

        var areaCode = $('#areaCode').val();
        var phone = $('#phone').val();
        var street = $('#street').val();
        var city = $('#city').val();
        var state = $('#state').val();
        var zip = $('#zip').val();

        var patron = {
            'FirstName': firstName,
            'LastName': lastName,
            'AreaCode': areaCode,
            'Phone': phone,
            'Street': street,
            'City': city,
            'State': state,
            'Zip': zip
        };

        $.ajax({
            url: 'api/RegisterPatron',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(patron),
            success: function (data) {
                $('#registerNewPatronModal').modal('toggle');
                $('.toast-body').text(data);
                $('.toast').toast('show');

                $(".needs-validation").removeClass("was-validated");
                $('#firstName').val("");
                $('#lastName').val("");
                $('#areaCode').val("");
                $('#phone').val("");
                $('#street').val("");
                $('#city').val("");
                $('#state').val("");
                $('#zip').val("");
            }
        });
    });
}