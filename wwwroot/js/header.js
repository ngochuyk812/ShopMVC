document.querySelector(".menu_mobile").addEventListener('click', (e) => {
    e.preventDefault()
    const elmMenu = document.querySelector(".menu_item_mobile")
    if (elmMenu.style.display === "none") {
        elmMenu.style.display = "flex"
    } else {
        elmMenu.style.display = "none"
    }
})