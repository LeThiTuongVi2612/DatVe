
$(document).ready(function () {

    $("#frmKhach").click(function () {
        $("#cbbKhach").toggleClass("active");
    })
    $("#frmLoaiGhe").click(function () {
        $("#cbbLoaiGhe").toggleClass("active");
    })

    //Xu li chon ngay
    flatpickr("#timeCheckIn", {
        minDate: "today",
/*        dateFormat: "d-m-Y",*/
        defaultDate: "today",
        onChange: function (selectedDates, dateStr, instance) {
            flatpickr("#timeCheckOut", {
                minDate: dateStr,
/*                dateFormat: "d-m-Y",*/
            });
        },

    });

    flatpickr("input[name='txtNgaySinh']", {})

    $('#btnLogin').click(function () {
        $('.sign-in').addClass('show')
    })

    $('#btnclose').click(function () {
        $('.sign-in').removeClass('show')
    })

    $('#btnSaved').click(function () {
        if ($('#session').val() == 0) {
            $('.sign-in').addClass('show')
        } else {
            window.location = '/User/Saved'
        }
    })

    $('#btnHistory').click(function () {
        if ($('#session').val() == 0) {
            $('.sign-in').addClass('show')
        } else {
            window.location = '/User/MyBooking'
        }
    })

    //Login
    $('#btnSignin').click(function () {
        if ($('#txtEmail').val() == "" || $('#txtPwd').val() == "") {
            $("#txtMsg").empty();
            $("#txtMsg").append("Vui lòng nhập đầy đủ thông tin!");
        } else {
            $.ajax({
                type: 'get',
                url: '/User/CheckAccount',
                data: {
                    email: $('#txtEmail').val(),
                    password: $('#txtPwd').val()
                },
                success: function (data) {
                    console.log(data);
                    if (data.code == 200) {
                        console.log(data);
                        if (data.thanhcong == true) {
                            alert("Đăng nhập thành công!");
                            location.reload();                           
                        } else {
                            $("#txtMsg").empty();
                            $("#txtMsg").append("Email hoặc mật khẩu không chính xác!");
                        }
                    }
                }
            })
        }
    })


    //window.onclick = function (e) {
    //    if (!e.target.matches(".form-group")) {
    //        var dropdowns = $(".combobox");
    //        var i;
    //        for (i = 0; i < dropdowns.length; i++) {
    //            var openDropdown = dropdowns[i];
    //            if (openDropdown.classList.contains('active')) {
    //                openDropdown.classList.remove('active');
    //            }
    //        }
    //    }
    //}

    //$('.main').click(function () {
    //    var dropdowns = $('.combobox')
    //    console.log(dropdowns)
    //    for (var i = 0; i < dropdowns.length; i++) {
    //        if (dropdowns[i].classList.contains('active')) {
    //            dropdowns[i].classList.remove('active');
    //        }
    //    }
    //})



})

