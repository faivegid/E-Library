const signInButton = document.querySelector('.toggle-btn1');
const signUpButton = document.querySelector('.toggle-btn2');
const signUp = document.getElementById('sign-up');
const signIn = document.getElementById('sign-in');
const formContainer = document.querySelector('.form-container');

signUpButton.addEventListener('click', function () {
    signUp.classList.remove('hide');
    signIn.classList.add('hide');

});

signInButton.addEventListener('click', function () {
    signUp.classList.add('hide');
    signIn.classList.remove('hide');

});