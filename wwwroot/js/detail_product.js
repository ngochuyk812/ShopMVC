const list_item_image = document.querySelectorAll(".item-image-detail")

list_item_image.forEach(tmp => {
    tmp.addEventListener('click', (e) => {
        const old_active = document.querySelector(".item-image-detail.active")
        const active = document.querySelector(".image-detail-active")
        if (active.src === tmp.src) return;
        old_active?.classList.remove("active")
        active.style.opacity = 0
        e.target.classList.add('active')
        setTimeout(() => {
            active.src = e.target.src
            active.style.opacity = 1

        },300)

    })
})
