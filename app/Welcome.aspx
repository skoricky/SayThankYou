<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <title>Welcome</title>
    <script src="/js/Welcome.js"></script>
    <link rel="stylesheet" type="text/css" href="css/Vote.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
    <form id="form1" runat="server">
    <header>
        <div class="headPage">
            <div class="ImgLogo"><asp:Image runat="server" Height="100px" Width="442px" ImageUrl="~/img/logo.png"/></div>           
            <div id="ButtonsCheckedLang" class="ButtonsCheckedLang">
                <input type="button" id="ButtonRUS" value="RUS" onclick="CheckedRusLang()" />
                <input type="button" id="ButtonENG" value="ENG" onclick="CheckedEngLang()" />
                <input type="hidden" id="SelectedLanguage" value="" runat="server"/>            
            </div>
        </div>
    </header>
    <section>
        <div class="WelcomeHeaderContentArea">
            <h1 id="headerText">ДОБРО ПОЖАЛОВАТЬ!</h1>
        </div>
        <div class="Vote">
            <p id="Vote">ГОЛОСОВАНИЕ</p>
        </div>
        <div class="WelcomeContentArea">
            <p id="SayThanks" class="SayThanksParagText">В этом году акция "Скажи спасибо" проходит с фокусом на ценности Группы:</p>
            <p id="SGValues" class="SGValues">КОМАНДНЫЙ ДУХ, ИННОВАЦИИ, ВОВЛЕЧЕННОСТЬ и ОТВЕТСТВЕННОСТЬ</p>
            <p id="WelcomeParagText" class="WelcomeParagText">
                Для того, чтобы определить ценность, за которую Вы хотите поблагодарить, мы<br />
                предлагаем Вам вспомнить Лидерскую Модель и найти себя в ней<br /><br /><br />
                Для этого необходимо нажать кнопку ниже:<br />
            </p>

            <asp:LinkButton ID ="IContributorPanel" runat="server" onclick="IContributorPanel_Click">
            <div class="ButtonIContributor">
                <div id="IContributor" class="IContributor">
                    <p id="IContributorText" class="IContributorText">Я - </p>
                </div>
                <asp:Button id="ButtonIContributor" runat="server" Text="СОТРУДНИК" OnClick="IContributorPanel_Click"/>
            </div>
            </asp:LinkButton>

            <asp:LinkButton ID ="IManagerPanel" runat="server" onclick="IManagerPanel_Click">
            <div class="ButtonIManager">
                <div id="IManager" class="IManager">
                    <p id="IManagerText" class="IManagerText">Я - </p>
                </div>
                <asp:Button ID="ButtonIManager" runat="server" Text="РУКОВОДИТЕЛЬ" OnClick="IManagerPanel_Click"/>                
            </div>
            </asp:LinkButton>
                       
        </div>
        <div class="WelcomeFooterContentArea">
            <div class="ButtonOpenVote">
                <asp:Button ID="ButtonNEXT" runat="server" Text="ДАЛЕЕ      >" onclick = "ButtonNEXT_Click" />
            </div>
            <div class="CheckPoint" onclick="CheckedPoint()">
                <input type="hidden" id="CheckedPoint" value="" runat="server"/>
                <div id="CheckPointText" class="CheckPointText">Больше не показывать это сообщение</div>
                <div id="CheckPointValue" class="CheckPointValue" >X</div>
            </div>
        </div>
        
    </section>    
    </form>
</body>
</html>
