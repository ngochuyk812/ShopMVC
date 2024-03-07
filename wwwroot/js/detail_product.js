$.ajax({
    url: '/api/review?Product='+idProduct,
    type: 'GET',
    dataType: 'json',
    success: function (reviews) {
        $.each(reviews, function (index, review) {
            addReview(review)
        });
    },
    error: function (xhr, status, error) {
        console.error('Failed to fetch data:', status, error);
    }
});

function addReview(review) {
    var listComment = document.querySelector("#list-comment");
    var dateString = new Date(review.createdDate);
    var formattedDate = moment(dateString).format('DD-MM-YYYY HH:mm:ss');
    var mediaElm = `
    
    `
    review.medias?.map(tmp => {
        if (tmp.type == 0) 
        mediaElm += `
        <img id="imagePreview" onclick="openModal(this)" src="/${tmp.src}" alt="">
        `
        else
        mediaElm += `
        <video id="videoPreview" controls>
          <source src="/${tmp.src}" type="video/mp4">
        </video>
        `
    })
    var itemReview = `
         <div class="item-review">
            <section class="reviews_reviewer">
                <div class="avatar_avatar">${review.user.name.charAt(0)}</div>
                <div class="reviews_info">
                    <p class="reviews_name">${review.user.name}</p>
                    <p class="reviews_verifyStatus">${review.user.email}</p>
                </div>
                <p class="reviews_verifyStatus">${formattedDate}</p>
            </section>
            <p class="content-review">
                ${review.content}
            </p>
            <div class="media-reviews">
            ${mediaElm}
            </div>
        </div>`
    listComment.innerHTML += itemReview
}
$('#create_review').submit(function (event) {
    event.preventDefault(); // Ngăn chặn hành vi mặc định của form

    var formData = new FormData($(this)[0]);

    $.ajax({
        url: '/api/review',
        type: 'POST',
        data: formData,
        processData: false, 
        contentType: false, 
        success: function (response) {
            addReview(response)
            toastr.success('Đánh giá của bạn đã được lưu lại', 'Đánh giá!')

            console.log('Review submitted successfully:', response);
        },
        error: function (xhr, status, error) {
            toastr.error(xhr.responseJSON.mess, 'Đánh giá!')
            
        }
    });
});


function openModal(img) {
    var modal = document.getElementById("imageModal");
    var modalImg = document.getElementById('img01');
    modal.style.display = "block"; 
    modalImg.src = img.src; 
}

function closeModal() {
    var modal = document.getElementById("imageModal");
    modal.style.display = "none"; 
}



