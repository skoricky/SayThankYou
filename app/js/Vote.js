var lengthTextLimit = 10;// минимальная длинна текста в комментарии
var TeamsVoteDifferens = 0;
var InnovVoteDifferens = 0;// лимит для инновации
var ComitVoteDifferens = 0;// лимит для вовлеченности
var ResponseVoteDifferens = 0;//лимит для ответственности
var summVotes;// общая переменная для определения остатка голосов по отдельным качествам
var changeVote = 0;// выбранная ценность
var TeamsVoteContent = "";
var InnovVoteContent = "";
var ComitVoteContent = "";
var ResponseVoteContent = "";
           
function SumCharsVotes() {
    var form = document.forms[0];
    var SummTeamsVotes = form.elements.TextBoxSummTeamsVotes;
    var SummInnovVotes = form.elements.TextBoxSummInnovVotes;
    var SummComitVotes = form.elements.TextBoxSummComitVotes;
    var SummResponseVotes = form.elements.TextBoxSummResponseVotes;
    var TeamsVoteLimit = form.elements.TextBoxTeamsVoteLimit;
    var InnovVoteLimit = form.elements.TextBoxInnovVoteLimit;
    var ComitVoteLimit = form.elements.TextBoxComitVoteLimit;
    var ResponseVoteLimit = form.elements.TextBoxResponseVoteLimit;
    
    if (TeamsVoteLimit.value > -1 && InnovVoteLimit.value > -1 && ComitVoteLimit.value > -1 && ResponseVoteLimit.value > -1) {
        TeamsVoteDifferens = Number(TeamsVoteLimit.value) - Number(SummTeamsVotes.value);
        TeamsVoteContent = document.getElementById('VoteOneText').innerHTML + " (" + TeamsVoteDifferens + ")";
        document.getElementById('VoteOneText').innerHTML = TeamsVoteContent;

        InnovVoteDifferens = Number(InnovVoteLimit.value) - Number(SummInnovVotes.value);
        InnovVoteContent = document.getElementById('VoteTwoText').innerHTML + " (" + InnovVoteDifferens + ")";
        document.getElementById('VoteTwoText').innerHTML = InnovVoteContent;

        ComitVoteDifferens = Number(ComitVoteLimit.value) - Number(SummComitVotes.value);
        ComitVoteContent = document.getElementById('VoteThreeText').innerHTML + " (" + ComitVoteDifferens + ")";
        document.getElementById('VoteThreeText').innerHTML = ComitVoteContent;

        ResponseVoteDifferens = Number(ResponseVoteLimit.value) - Number(SummResponseVotes.value);
        ResponseVoteContent = document.getElementById('VoteFourText').innerHTML + " (" + ResponseVoteDifferens + ")";
        document.getElementById('VoteFourText').innerHTML = ResponseVoteContent;

        summVotes = 0;
        summVotes += Number(TeamsVoteDifferens);
        summVotes += Number(InnovVoteDifferens);
        summVotes += Number(ComitVoteDifferens);
        summVotes += Number(ResponseVoteDifferens);
    } else {
        summVotes = -1;
    }
    
}

function LoadStatsPage() {
    //get it if you need: location.pathname = '/stats.aspx';
}

window.onload = function ()
{                
    var nothingChecked = true;
    var radioButtons = document.getElementsByName('RadioButtonListValues');
    for (var i = 0; i < radioButtons.length; i++) {
        if (radioButtons[i].checked)
        {
            nothingChecked = false;
            break;
        }
    }
    if (nothingChecked) {  
        
        if (summVotes === -1) {
            radioButtons[0].checked = true;            
        } else {
            radioButtons[0].checked = TeamsVoteDifferens > 0;
            radioButtons[1].checked = InnovVoteDifferens > 0 && TeamsVoteDifferens <= 0;
            radioButtons[2].checked = ComitVoteDifferens > 0 && TeamsVoteDifferens <= 0 && InnovVoteDifferens <= 0;
            radioButtons[3].checked = ResponseVoteDifferens > 0 && TeamsVoteDifferens <= 0 && InnovVoteDifferens <= 0 && ComitVoteDifferens <= 0;
        }                    
        getVoteCheckedRadio();
    }
}

function OpenErrModalWin() {
    document.getElementById('BlockScreen').style.display = "block";
    document.getElementById('ModalWindow').style.display = "block";
    document.getElementById('dVoteOne').style.display = "none";
    document.getElementById('dVoteOne').style.display = "none";
    document.getElementById('dVoteTwo').style.display = "none";
    document.getElementById('dVoteTwo').style.display = "none";
    document.getElementById('dVoteThree').style.display = "none";
    document.getElementById('dVoteThree').style.display = "none";
    document.getElementById('dVoteFour').style.display = "none";
    document.getElementById('dVoteFour').style.display = "none";
    document.getElementById('LabelVoteDescription').style.display = "none";
    document.getElementById('Text').style.margin = "12px auto";
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
    document.getElementById('headerText').innerText = "DEAR EMPLOYEE,";
    document.getElementById('paragText').innerHTML = "We invite you to choose <font color=red>one of the four</font> major characteristics of your colleagues!";
    document.getElementById('VoteOneText').innerHTML = "① TEAM SPIRIT";
    document.getElementById('VoteTwoText').innerHTML = "② INNOVATION";
    document.getElementById('VoteThreeText').innerHTML = "③ COMMITMENT";
    document.getElementById('VoteFourText').innerHTML = "④ RESPONSIBILITY";
    document.getElementById('LabelVoteDescription').innerHTML = "Your Сhoice!";
    textBoxComment.placeholder = "Submit your comment here <Required>";
    document.getElementById('SelectedLanguage').value = "ENG";
    document.getElementById('ButtonModal').value = "VOTE      >";
    getVoteCheckedRadio();
    SumCharsVotes();    
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
    document.getElementById('headerText').innerText = "УВАЖАЕМЫЙ СОТРУДНИК,";
    document.getElementById('paragText').innerHTML = "Приглашаем Вас выбрать <font color=red>одно</font> из <font color=red>четырех</font> качеств, присущее Вашему коллеге!";
    document.getElementById('VoteOneText').innerHTML = "① КОМАНДНЫЙ ДУХ";
    document.getElementById('VoteTwoText').innerHTML = "② ИННОВАЦИИ";
    document.getElementById('VoteThreeText').innerHTML = "③ ВОВЛЕЧЕННОСТЬ";
    document.getElementById('VoteFourText').innerHTML = "④ ОТВЕТСТВЕННОСТЬ";
    document.getElementById('LabelVoteDescription').innerHTML = "Выбери качество!";
    textBoxComment.placeholder = "Разместите здесь свой комментарий <Обязательное>";
    document.getElementById('SelectedLanguage').value = "RUS";
    document.getElementById('ButtonModal').value = "ГОЛОСОВАТЬ  >";
    getVoteCheckedRadio();
    SumCharsVotes();
}

function RestoreLanguage() {
    if (document.getElementById('SelectedLanguage').value === "ENG") {
        CheckedEngLang();
    }
    else {
        CheckedRusLang();
    }
}

function AnimationStop() {
    document.getElementById('dVoteOne').style.animation = "none";
    document.getElementById('dVoteOne').style.transition = "none";
    document.getElementById('dVoteTwo').style.animation = "none";
    document.getElementById('dVoteTwo').style.transition = "none";
    document.getElementById('dVoteThree').style.animation = "none";
    document.getElementById('dVoteThree').style.transition = "none";
    document.getElementById('dVoteFour').style.animation = "none";
    document.getElementById('dVoteFour').style.transition = "none";
    RestoreLanguage();
}

function OpenModalWindow() {
    RestoreLanguage();                              
    var fromDep = document.getElementById('LabelFromEmployeeDep').innerText;
    //var toDep = document.getElementById('LabelToEmployeeDep').innerText;
    var toDep = "";

    if (fromDep === toDep) {
        OpenErrModalWin();
        document.getElementById('Text').innerHTML = "<font color=red>Внимание! </font>Нельзя голосовать за сотрудника Вашего департамента<br /><font color=red>Attention!</font> It is not allowed to vote for colleague from your department";
    } else {                    
        if (summVotes === 0) {
            OpenErrModalWin();
            document.getElementById('Text').innerHTML = "У Вас не осталось голосов в этом месяце...<font color=red>очень жаль!</font><br /><font color=red>Sorry!</font> You have no more points for voting <font color=red>in this month!</font>";
        } else {
            var radioButtons = document.getElementsByName('RadioButtonListValues');
            for (var i = 0; i < radioButtons.length; i++) {
                if (radioButtons[i].checked) {
                    showFinalWindow();
                    break;
                }
            }
        }
    }
            
}

function showFinalWindow()
{
    RestoreLanguage();
    document.getElementById('BlockScreen').style.display = "block";               
    if (document.getElementById('SelectedLanguage').value === "RUS") {
        document.getElementById('Text').innerHTML = "<font color=red>Спасибо</font> за Ваш голос!</font>";
    } else {
        document.getElementById('Text').innerHTML = "<font color=red>Thank you</font> for your vote!</font>";
    }
}

function stoppedTyping() {
    var form = document.forms[0];
    var textBoxComment = form.elements.TextBoxComment;
    if (textBoxComment.value.length >= lengthTextLimit) {
        document.getElementById('ButtonModal').disabled = false;
        document.getElementById('ButtonModal').style.background = "#e60024";
        document.getElementById('ButtonModal').style.color = "#fff";
    } else {
        document.getElementById('ButtonModal').disabled = true;
        document.getElementById('ButtonModal').style.background = "#a2a0a0";
        document.getElementById('ButtonModal').style.color = "#fff";
    }
}

function clickOne() {
    var radioButtons = document.getElementsByName('RadioButtonListValues');
    radioButtons[0].checked = TeamsVoteDifferens > 0 || summVotes === -1;
    getVoteCheckedRadio();
}

function clickTwo() {
    var radioButtons = document.getElementsByName('RadioButtonListValues');
    radioButtons[1].checked = InnovVoteDifferens > 0 || summVotes === -1;
    getVoteCheckedRadio();
}

function clickThree() {
    var radioButtons = document.getElementsByName('RadioButtonListValues');
    radioButtons[2].checked = ComitVoteDifferens > 0 || summVotes === -1;
    getVoteCheckedRadio();
}

function clickFour() {
    var radioButtons = document.getElementsByName('RadioButtonListValues');
    radioButtons[3].checked = ResponseVoteDifferens > 0 || summVotes === -1;
    getVoteCheckedRadio();
}

function getVoteCheckedRadio() {
    var radioButtons = document.getElementsByName('RadioButtonListValues');
                
    for (var i = 0; i < radioButtons.length; i++) {
        if (radioButtons[i].checked) {
            stoppedTyping();
            document.getElementById('LabelVoteDescription').style.top = "-75px";
            document.getElementById('LabelVoteDescription').style.borderRadius = "10px 10px 10px 0px";
            document.getElementById('dVoteOne').style.color = "#fff";
            document.getElementById('dVoteOne').style.background = "#a2a0a0";
            document.getElementById('dVoteTwo').style.color = "#fff";
            document.getElementById('dVoteTwo').style.background = "#a2a0a0";
            document.getElementById('dVoteThree').style.color = "#fff";
            document.getElementById('dVoteThree').style.background = "#a2a0a0";
            document.getElementById('dVoteFour').style.color = "#fff";
            document.getElementById('dVoteFour').style.background = "#a2a0a0";
            switch(radioButtons[i].value) {
                case "TeamSpirit":
                    changeVote = 1;
                    document.getElementById('LabelVoteDescription').className = "new";
                    document.getElementById('LabelVoteDescription').style.left = "240px";                                
                    document.getElementById('dVoteOne').style.color = "#fff";
                    document.getElementById('dVoteOne').style.background = "#e60024";
                    if (document.getElementById('SelectedLanguage').value === "RUS") {
                        document.getElementById('LabelLastChose').innerText = "+ 1 Командный дух";
                        document.getElementById('LabelVoteDescription').innerHTML = "Концентрация энергии и таланта на достижение общего успеха";
                        document.getElementById('LabelVoteDescription').style.width = "500px";
                    } else {
                        document.getElementById('LabelLastChose').innerText = "+ 1 Team Spirit";
                        document.getElementById('LabelVoteDescription').innerHTML = "Concentration of energy and talent to achieve overall success";
                        document.getElementById('LabelVoteDescription').style.width = "460px";
                    }
                    break;
                case "Innovation":
                    document.getElementById('LabelVoteDescription').className = "new";
                    document.getElementById('LabelVoteDescription').style.left = "437px";                                
                    document.getElementById('dVoteTwo').style.color = "#fff";
                    document.getElementById('dVoteTwo').style.background = "#e60024";
                    if (document.getElementById('SelectedLanguage').value === "RUS") {
                        document.getElementById('LabelLastChose').innerText = "+ 1 Инновации";
                        document.getElementById('LabelVoteDescription').innerHTML = "Новые идеи и вклад в изменение процессов";
                        document.getElementById('LabelVoteDescription').style.width = "360px";
                    } else {
                        document.getElementById('LabelLastChose').innerText = "+ 1 Innovation";
                        document.getElementById('LabelVoteDescription').innerHTML = "New ideas and contribution to the change process";
                        document.getElementById('LabelVoteDescription').style.width = "380px";
                    }
                    break;
                case "Commitment":
                    document.getElementById('LabelVoteDescription').className = "new";
                    document.getElementById('LabelVoteDescription').style.left = "641px";                                
                    document.getElementById('dVoteThree').style.color = "#fff";
                    document.getElementById('dVoteThree').style.background = "#e60024";
                    if (document.getElementById('SelectedLanguage').value === "RUS") {
                        document.getElementById('LabelLastChose').innerText = "+ 1 Вовлеченность";
                        document.getElementById('LabelVoteDescription').innerHTML = "Вовлечение и проявление внимания к другим людям";
                        document.getElementById('LabelVoteDescription').style.width = "420px";
                    } else {
                        document.getElementById('LabelLastChose').innerText = "+ 1 Commitment";
                        document.getElementById('LabelVoteDescription').innerHTML = "Involvement and demonstration of attention to other people";
                        document.getElementById('LabelVoteDescription').style.width = "440px";
                             
                    }
                    break;
                case "Responsibility":
                    document.getElementById('LabelVoteDescription').className = "new";
                    document.getElementById('LabelVoteDescription').style.left = "845px";                                
                    document.getElementById('dVoteFour').style.color = "#fff";
                    document.getElementById('dVoteFour').style.background = "#e60024";
                    if (document.getElementById('SelectedLanguage').value === "RUS") {
                        document.getElementById('LabelLastChose').innerText = "+ 1 Ответственность";
                        document.getElementById('LabelVoteDescription').innerHTML = "Решительные действия и соблюдения этики";
                        document.getElementById('LabelVoteDescription').style.width = "350px";
                    } else {
                        document.getElementById('LabelLastChose').innerText = "+ 1 Responsibility";
                        document.getElementById('LabelVoteDescription').innerHTML = "Responsible actions and complying with Ethics";
                        document.getElementById('LabelVoteDescription').style.width = "350px";
                    }
                    break;
                default:
                    document.getElementById('LabelVoteDescription').innerHTML = "";
            }                         
        }
    }
}