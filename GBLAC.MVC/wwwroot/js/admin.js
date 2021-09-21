const dash1 = document.querySelector('.dash1')
const dash2 = document.querySelector('.dash2')
const dash3 = document.querySelector('.dash2')
const dash4 = document.querySelector('.dash4')
const adminDetails = document.querySelector('.admin-details')
const books = document.querySelector('.books')

dash1.addEventListener('click', function () {
    dash1.classList.remove('dash')
    dash2.classList.add('dash');
    dash3.classList.add('dash');
    adminDetails.classList.remove('hide')
    books.classList.add('hide')
})
dash2.addEventListener('click', function () {
    dash1.classList.add('dash');
    dash3.classList.add('dash');
    dash2.classList.remove('dash')
    adminDetails.classList.add('hide')
    books.classList.remove('hide')
})

const addButton = document.querySelector('.add-button')
const modal = document.querySelector('.modal')
const clostButton = document.querySelector('.close-button')
const gridContainer = document.querySelector('.grid-container')
addButton.addEventListener('click', function () {
    modal.classList.remove('hide')
    gridContainer.style.opacity = '.3'
})
clostButton.addEventListener('click', function () {
    modal.classList.add('hide')
    gridContainer.style.opacity = '1'
})
