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

const addToCart = (e, id) => {
    e.stopPropagation();
    /*toastr.success("Thêm sản phầm vào giỏ hàng "+id, "Thành công")*/
    console.log("Add Cart" +id)
}

const handleDetail = (id) => {
    window.location.href = '/product/detail?id=' + id
}