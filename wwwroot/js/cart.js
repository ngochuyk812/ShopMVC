var dataCart = []
const closeCart = () => {
    document.querySelector(".bg-cart").style.maxWidth = "0px";
}
const openCart = () => {
    if (dataCart.length == 0)
    getDataCart();
    document.querySelector(".bg-cart").style.maxWidth = "100%";

}

const getDataCart = () => {
    $.ajax({
        url: '/api/cart',
        type: 'GET',
        success: function (response) {
            dataCart = response;
            initData()
        }
    })
}
const initData = () => {
    var elmListCart = document.querySelector(".list-cart");
    elmListCart.innerHTML = "";
    dataCart.map(tmp => {
        renderCart(tmp)
    })
}

const renderCart = (tmp) => {
    var elmListCart = document.querySelector(".list-cart");
    var discountAmount = tmp.product.price * (tmp.product.discount / 100);
    discountAmount = tmp.product.price - discountAmount;
    var formattedDiscountAmount = discountAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    var price = tmp.product.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    let colorOptions = tmp.product.imports.map(color => {
        return `<option value="${color.id}" ${tmp.import.id ==  color.id ? "selected" : ""}  style="color: ${color.color};">${color.color}</option>`;
    }).join('');

    let elm = `
            <div class="item-cart">
                <div class="left-cart">
                    <i onclick="removeCart(${tmp.id})" class="fa-solid fa-circle-xmark"></i>
                    <img width="90px" src="/${tmp.product.images[0]?.src}" />
                </div>
                <div class="center-cart">
                    <div class="top-center-cart mb-1">
                        <p class="name-product-cart">${tmp.product.title}</p>
                        <div class="price-product-cart">
                            <p class="pro-price">${price}</p>
                            <p class="discount-price">${formattedDiscountAmount}</p>
                        </div>
                    </div>
                    <div class="d-flex justify-content-between">
                        <div class="quantity-product">
                            <i class="fa-solid fa-circle-minus"></i>
                            <p class="display-quantity">Qty: <span>${tmp.quantity}</span></p>
                            <i class="fa-solid fa-circle-plus"></i>
                        </div>
                        <select class="colorSelect" data-previous-value=${tmp.import.id} onchange="showSelectedColor(event, ${tmp.id})">
                            ${colorOptions}
                        </select>
                        
                    </div>
                </div>
            </div>
        `
    elmListCart.innerHTML += elm;
}


const showSelectedColor = (e, CartId) => {
    const ImportId = e.target.value;
    const oldValue = e.target.getAttribute('data-previous-value');
    $.ajax({
            url: '/api/cart/color',
            type: 'PUT',
            data:JSON.stringify({
                CartId,
                ImportId
            }),
            contentType:'application/json',
            success: function (response) {
                e.target.setAttribute('data-previous-value', ImportId);
            },
            error: function (xhr, status, error) {
                toastr.error(xhr.responseJSON.mess, 'Thất bại!')
                e.target.value = oldValue;
            }
        });

}
const removeCart = (id)=>{
    $.ajax({
        url: '/api/cart?id=' + id,
        type: 'DELETE',
        success: function (response) {
            const index = dataCart.findIndex(f => f.id == id)
            dataCart.splice(index ,1);
            initData()
        },
        error: function (xhr, status, error) {
            toastr.error(xhr.responseJSON.mess, 'Thất bại!')
        }
    });
}
const addToCart = (e, id) => {
    e.stopPropagation();
    $.ajax({
        url: '/api/cart?ImportId=' + id,
        type: 'POST',
        success: function (response) {
            const index = dataCart.findIndex(f => f.id == response.id)
            if (index != -1) {
                dataCart[index] = response
            } else {
                dataCart = [response, ...dataCart]
            }

            initData()
            toastr.success('Thêm sản phẩm vào giỏ hàng', 'Thành công!')
            console.log('Review submitted successfully:', response);
        },
        error: function (xhr, status, error) {
            toastr.error(xhr.responseJSON.mess, 'Thất bại!')

        }
    });


}

const channgeColor = (e, CartId) => {
    e.stopPropagation();
    console.log("Change Color Cart" + id)
    

}
