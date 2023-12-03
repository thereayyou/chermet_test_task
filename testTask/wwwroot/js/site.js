// Функция для передачи текущей страницы, посредством ajax-запроса.

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

// Функция для передачи полученного значения из select(ФИО/Номер телефона) и input (в котором происходит поиск сотрудника).

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

// Функция для сокрытия ошибка, появляющиейся в случае незаполненного поля формы.

setTimeout(function () {
    $('.alert').alert('close');
}, 5000);

// Валидация полец формы.

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

// Маска для поля "Телефон".

let phone = document.getElementById('phone');

let maskOptions = {
    mask: '+{7}(000)000-00-00',
    lazy: false
}
let mask = new IMask(phone, maskOptions);


