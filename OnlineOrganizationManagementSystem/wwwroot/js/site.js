// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

window.onload = function () {
	var fadeInElements = document.querySelectorAll('.fade-in');
	for (var i = 0; i < fadeInElements.length; i++) {
		fadeInElements[i].classList.add('show');
	}
}
// Write your JavaScript code.

	const greetings = ['Hello!', 'Aloha!', 'Bonjour!', 'Hola!', 'Ciao!'];

	function changeGreeting() {
		const index = Math.floor(Math.random() * greetings.length);
	const greeting = greetings[index];
	const greetingElement = document.getElementById('greeting');
	greetingElement.innerText = greeting;
	greetingElement.classList.add('animate__animated', 'animate__fadeIn');
		setTimeout(() => {
		greetingElement.classList.remove('animate__animated', 'animate__fadeIn');
		}, 1000);
	}

	changeGreeting();
	setInterval(changeGreeting, 5000);



