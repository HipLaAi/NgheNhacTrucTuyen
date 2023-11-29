var nut  = document.getElementById("button");
nut.onclick = function(){
    if(nut.style.backgroundColor == "white"){
        nut.style.backgroundColor = "rgb(92, 214, 92)"
    }
    else{
        nut.style.backgroundColor = "white"
    }
}

// ----------------------------------------------

var email = document.getElementById("txtEmail");
var errorEmail = document.getElementById("errorEmail");
email.addEventListener("input", function(){
    if(email.value == "")
    {
        errorEmail.style.display = "table-cell";
        email.style.border = "1px solid red"
    }
    else
    {
        errorEmail.style.display = "none";
        email.style.border = "2px solid gray"
    }
});


var password = document.getElementById("txtPassWord");
var errorPassword = document.getElementById("errorPassWord");
password.addEventListener("input", function() {
    if(password.value == "") {
        errorPassword.style.display = "table-cell";
        password.style.border = "1px solid red";
    } else {
        errorPassword.style.display = "none";
        password.style.border = "2px solid gray"
    }
});

var errorUser = document.getElementById('errorUser');
function checkUsser(){
    if(email.value == "" && password.value == ""){
        window.scrollTo({
            top: 0,
            behavior: "smooth"
        });
        errorUser.style.display = 'table-cell';
    }
}

function Check(input,error){
    if((input.value == ""))
    {
        window.scrollTo({
            top: 100,
            behavior: "smooth"
        });
        error.style.display = "table-cell";
        input.style.border = "1px solid red";
        errorUser.style.display = 'none';
    }
    else
    {
        error.style.display = "none";
        input.style.border = "1px solid grey";
    }
}


$(document).ready(function() {
    $('#icon').click(function() {
      var passwordField = $('#txtPassWord');
      var passwordFieldType = passwordField.attr('type');
      if (passwordFieldType == 'password') {
        passwordField.attr('type', 'text');
        $(this).removeClass('fa-eye-slash').addClass('fa-eye');
      } else {
        passwordField.attr('type', 'password');
        $(this).removeClass('fa-eye').addClass('fa-eye-slash');
      }
    });
  });
  

var sign = document.getElementById("sign");
sign.onclick = function(){
    if(email.value == "" && password.value == ""){
        checkUsser();
    }
    else{
        Check(email,errorEmail);
        Check(password,errorPassword);
    }

}
