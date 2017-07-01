var check = false;

function RestoreLanguage() {
    if (document.getElementById('SelectedLanguage').value === "ENG") {
        CheckedEngLang();
    }
    else {
        CheckedRusLang();
    }
}

function CheckedEngLang() {
    var form = document.forms[0];
    var textBoxComment = form.elements.TextBoxComment;
    document.getElementById('ButtonRUS').style.background = "none";
    document.getElementById('ButtonRUS').style.borderWidth = "2px";
    document.getElementById('ButtonRUS').style.color = "#444242";
    document.getElementById('ButtonENG').style.background = "#e60024";
    document.getElementById('ButtonENG').style.borderWidth = "2px";
    document.getElementById('ButtonENG').style.color = "#fff";
    document.getElementById('SelectedLanguage').value = "ENG";

    document.getElementById('headerText').innerHTML = "WELCOME!";
    document.getElementById('SayThanks').innerHTML = 'This year our internal activity "Say thank you" is focused on SG values';
    document.getElementById('SGValues').innerHTML = "TIME SPIRIT, INNOVATION, COMMITMENT, RESPONSIBILITY";
    document.getElementById('WelcomeParagText').innerHTML = "The clarify the value you want to vote, we are proposing to remember<br />SG Liadership Model and yourself there<br /><br /><br />To start the questinnaire please push the button below:";
    document.getElementById('CheckPointText').innerHTML = "Don't show this message again";
    document.getElementById('IContributorText').innerHTML = "I AM";
    document.getElementById('ButtonIContributor').value = "CONTRIBUTOR";
    document.getElementById('IManagerText').innerHTML = "I AM";
    document.getElementById('ButtonIManager').value = "MANAGER";
    document.getElementById('Vote').innerHTML = "VOTE";
    document.getElementById('ButtonNEXT').value = "NEXT      >";
}

function CheckedRusLang() {
    var form = document.forms[0];
    var textBoxComment = form.elements.TextBoxComment;
    document.getElementById('ButtonRUS').style.background = "#e60024";
    document.getElementById('ButtonRUS').style.borderWidth = "2px";
    document.getElementById('ButtonRUS').style.color = "#fff";
    document.getElementById('ButtonENG').style.background = "none";
    document.getElementById('ButtonENG').style.borderWidth = "2px";
    document.getElementById('ButtonENG').style.color = "#444242";
    document.getElementById('SelectedLanguage').value = "RUS";

    document.getElementById('headerText').innerHTML = "ДОБРО ПОЖАЛОВАТЬ!";
    document.getElementById('SayThanks').innerHTML = 'В этом году акция "Скажи спасибо" проходит с фокусом на ценности Группы:';
    document.getElementById('SGValues').innerHTML = "КОМАНДНЫЙ ДУХ, ИННОВАЦИИ, ВОВЛЕЧЕННОСТЬ и ОТВЕТСТВЕННОСТЬ";
    document.getElementById('WelcomeParagText').innerHTML = "Для того, чтобы определить ценность, за которую Вы хотите поблагодарить, мы<br />предлагаем Вам вспомнить Лидерскую Модель и найти себя в ней<br /><br /><br />Для этого необходимо нажать кнопку ниже:";
    document.getElementById('CheckPointText').innerHTML = "Больше не показывать это сообщение";
    document.getElementById('IContributorText').innerHTML = "Я -";
    document.getElementById('ButtonIContributor').value = "РАБОТНИК";
    document.getElementById('IManagerText').innerHTML = "Я - ";
    document.getElementById('ButtonIManager').value = "РУКОВОДИТЕЛЬ";
    document.getElementById('Vote').innerHTML = "ГОЛОСОВАНИЕ";
    document.getElementById('ButtonNEXT').value = "ДАЛЕЕ      >";
}

function CheckedIContributor() {
    document.getElementById('IContributor').style.background = "#e60024";
    document.getElementById('IContributor').style.color = "#fff";
    document.getElementById('ButtonIContributor').style.background = "black";
    document.getElementById('ButtonIContributor').style.color = "#fff";
    document.getElementById('IManager').style.background = "none";
    document.getElementById('IManager').style.color = "black";
    document.getElementById('ButtonIManager').style.background = "none";
    document.getElementById('ButtonIManager').style.color = "black";
}

function CheckedIManager() {
    document.getElementById('IManager').style.background = "#e60024";
    document.getElementById('IManager').style.color = "#fff";
    document.getElementById('ButtonIManager').style.background = "black";
    document.getElementById('ButtonIManager').style.color = "#fff";
    document.getElementById('IContributor').style.background = "none";
    document.getElementById('IContributor').style.color = "black";
    document.getElementById('ButtonIContributor').style.background = "none";
    document.getElementById('ButtonIContributor').style.color = "black";
}

function CheckedPoint() {
    if (check) {
        document.getElementById('CheckPointValue').style.color = "";
        document.getElementById('CheckPointText').style.color = "";
        document.getElementById('CheckedPoint').value = "0";
        check = false;
    } else {
        document.getElementById('CheckPointValue').style.color = "#e60024";
        document.getElementById('CheckPointText').style.color = "black";
        document.getElementById('CheckedPoint').value = "1";
        check = true;
    }
}
