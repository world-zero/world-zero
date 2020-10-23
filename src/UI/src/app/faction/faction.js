/*
function myFunction() {
  var popup = document.getElementById("myPopup");
  popup.classList.toggle("show");
}
*/
function myFunction(event) {
   event.target.children[0].classList.toggle("show");
}
