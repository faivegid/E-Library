const signInButton = document.querySelector('sign-in-btn');
const signUpButton = document.querySelector('sign-up-btn');
const signUpForm = document.getElementById('sign-up-form');
const signInForm = document.getElementById('sign-in-form');
const formContainer = document.querySelector('.form-container');

signUpButton.addEventListener('click', function () {
    signUpForm.classList.remove('hide');
    signInForm.classList.add('hide');
});

signInButton.addEventListener('click', function () {
    signUpForm.classList.add('hide');
    signInForm.classList.remove('hide');
});