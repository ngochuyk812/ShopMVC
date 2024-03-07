toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}



const handleDetail = (id) => {
    window.location.href = '/product/detail?id=' + id
}

const goToLogin = (e = null) => {
    e?.stopPropagation();
    var path = window.location.href.replace(window.location.origin, "")
    window.location.href = "/Account/Login?RequestPath=" + path
}