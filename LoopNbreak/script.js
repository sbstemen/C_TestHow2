alert('hello');

var age = 30;
var counter = 0;
var startAt = 0;

/* THIS function runs 3 times,  0, 1, 2 */
function secondAlert(){
  for(startAt = 0; counter < 3; counter++){
    alert('Pop up that happens 3 times count == ' + counter);
  }
}

function newAlert(){
    alert('A New Message');
}


if (age <= 21){
    alert('You are young');
}
else if (age <= 22){
    alert('you got a ways yet');
}

function myFunction(){
    var popup = document.getElementById("mypop");
    popup.classList.toggle("show");
}

// Get all elements with class="closebtn"
var close = document.getElementsByClassName("closebtn");
var i;

// Loop through all close buttons
for (i = 0; i < close.length; i++) {
    // When someone clicks on a close button
    close[i].onclick = function(){

        // Get the parent of <span class="closebtn"> (<div class="alert">)
        var div = this.parentElement;

        // Set the opacity of div to 0 (transparent)
        div.style.opacity = "0";

        // Hide the div after 600ms (the same amount of milliseconds it takes to fade out)
        setTimeout(function(){ div.style.display = "none"; }, 600);
    }
}
method
function numberCalculation(number1, number2){
  
numberOut = number1 * number2;

 
 if(numberOut >= 3500){
 alert("Bigger than 3,500"); 
}
 else
{
alert("Smaller than 3,500"); 
}
 return numberOut;
}
