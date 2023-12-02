function pageClick(e) {
    let page = e.innerHTML;
    let searchBy = $('#searchBy').val()
    let searchByValue = $('#searchByValue').val();
    $.ajax({
        method: "get",
        url: "/?page=" + page + '&searchByValue=' + searchByValue + '&searchBy=' + searchBy,
        contentType: 'html',
        success: function (response) {
            history.pushState('', '', this.url),
                $('#result').html(jQuery(response).find('#result').html());
        },
        error: function () {
            console.error('error')
        },
    })
}

function findEmployee(e) {
    let searchBy = $('#searchBy').val()
    let searchByValue = $('#searchByValue').val()
    $.ajax({
        method: "get",
        url: "/?page=1" + '&searchByValue=' + searchByValue + '&searchBy=' + searchBy,
        contentType: 'html',
        success: function (response) {
            history.pushState('', '', this.url),
                $('#result').html(jQuery(response).find('#result').html());
        },
        error: function () {
            console.error('error')
        },
    })
}
setTimeout(function () {
    $('.alert').alert('close');
}, 5000);

const form = document.forms["form"];

form.addEventListener("input", inputHandler);

function inputHandler({ target }) {
    if (target.hasAttribute("data-reg")) {
        inputCheck(target);
    } 
}

function inputCheck(el) {
    const inputValue = el.value;
    const inputReg = el.getAttribute("data-reg");
    const reg = new RegExp(inputReg);
    if (reg.test(inputValue)) {
        el.style.border = "2px solid rgb(0, 196, 0)";
    } else {
        el.style.border = "2px solid rgb(255, 0, 0)"
    }
}

let phone = document.getElementById('phone');

let maskOptions = {
    mask: '+{7}(000)000-00-00',
    lazy: false
}
let mask = new IMask(phone, maskOptions);

// Get the container element
var btnContainer = document.getElementById("pagsBtns");

console.log(btnContainer)

// Get all buttons with class="btn" inside the container
var btns = btnContainer.getElementsByClassName("paginationItem");

console.log(btns)

// Loop through the buttons and add the active class to the current/clicked button
for (let i = 0; i < btns.length; i++) {
    btns[i].addEventListener("click", function () {
        let current = document.getElementsByClassName("active");
        current[0].className = current[0].className.replace(" active", "");
        this.className += " active";
    });
}


