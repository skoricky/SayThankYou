<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vote.aspx.cs" Inherits="Vote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VoteWeb</title>
    <script src="/js/Vote.js"></script>
    <link rel="stylesheet" type="text/css" href="/css/Vote.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>    
<body>
     <form id="form1" runat="server">
        <div id="BlockScreen"></div>
        <div id="ModalWindow">
            <div id="Text" class="Text"></div>
                <asp:Button ID="ButtonOK" runat="server" Text="OK        >"  OnClick="ButtonOK_Click" />
        </div>
        <header>
           <div class="headPage">
                <div class="ImgFromEmployee"><asp:Image ID="ImageFromEmployee" ImageUrl="" runat="server" Height="91px" /></div>
                <div class="ImgLogo"><asp:Image runat="server" Height="100px" Width="442px" ImageUrl="~/img/logo.png"/></div>
                <div class="InfoFromEmployee"><asp:Label ID="LabelFromEmployeeName" runat="server"></asp:Label>, <br /><asp:Label ID="LabelFromEmployeeDep" runat="server" ></asp:Label> </div>
                <div id="ButtonsCheckedLang" class="ButtonsCheckedLang">
                    <input type="button" id="ButtonRUS" value="RUS" onclick="CheckedRusLang()" />
                    <input type="button" id="ButtonENG" value="ENG" onclick="CheckedEngLang()" />
                    <input type="hidden" id="SelectedLanguage" value="" runat="server"/>                   
                </div>
            </div>
        </header>
        <section>
            <div>
                <h1 id="headerText"></h1>
                <p id="paragText" class="paragText"></p>
            </div>
            <div class="FolderToVote">
                <div class="BookMarkVote"></div>
                <div class="ImgToEmployee">
                    <asp:Image ID="ImageToEmployee" ImageUrl="" runat="server" Height="182px" ImageAlign="Middle" />
                </div>
                    <p class="NamToEmployee">
                        <asp:Label ID="LabelToEmployeeName" runat="server"></asp:Label>
                    </p>
                    <p class="DepToEmployee">
                        <asp:Label ID="LabelToEmployeeDep" runat="server"></asp:Label>
                    </p>
                <div class="LastChose">
                        <asp:Label ID="LabelLastChose" runat="server"></asp:Label>
                </div>
                <div id="dVoteOne" onclick="clickOne()">
                    <p id="VoteOneText">① КОМАНДНЫЙ ДУХ</p>
                    <%--<asp:Label ID="LabelVoteOne" text="① КОМАНДНЫЙ ДУХ" runat="server"></asp:Label>--%>
                </div>
                <div id="dVoteTwo" onclick="clickTwo()">
                    <p id="VoteTwoText">② ИННОВАЦИИ</p>
                    <%--<asp:Label ID="LabelVoteTwo" text="② ИННОВАЦИИ" runat="server"></asp:Label>--%>
                </div>
                <div id="dVoteThree" onclick="clickThree()">
                    <p id="VoteThreeText">③ ВОВЛЕЧЕННОСТЬ</p>
                    <%--<asp:Label ID="LabelVoteThree" text="③ ВОВЛЕЧЕННОСТЬ" runat="server"></asp:Label>--%>
                </div>
                <div id="dVoteFour" onclick="clickFour()">
                    <p id="VoteFourText">④ ОТВЕТСТВЕННОСТЬ</p>
                    <%--<asp:Label ID="LabelVoteFour" text="④ ОТВЕТСТВЕННОСТЬ" runat="server"></asp:Label>--%>
                </div>
                <asp:Label ID="LabelVoteDescription" runat="server" Text="Выберите качество!"></asp:Label>
                <div class="RadioButtons">
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:RadioButtonList runat="server" Height="30px" RepeatColumns="4"  RepeatLayout="table" Width="600px" ID="RadioButtonListValues" RepeatDirection="Horizontal" Visible="True">
                            <asp:ListItem Value="TeamSpirit">Командный дух</asp:ListItem>
                            <asp:ListItem Value="Innovation">Инновации</asp:ListItem>
                            <asp:ListItem Value="Commitment">Вовлечённость</asp:ListItem>
                            <asp:ListItem Value="Responsibility">Ответственность</asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:Panel>
                </div>
                <div class="BoxComment">
                    <asp:TextBox ID="TextBoxComment" runat="server" MaxLength="1000" TextMode="MultiLine" Width="99%" Height="120" onkeyup="getVoteCheckedRadio()"></asp:TextBox>
                </div>
                <div class="ButtonOpenModal">
                    <asp:Button ID="ButtonModal" runat="server" Text="" OnClick="ButtonVote_Click"/>
                </div>                    
                <asp:TextBox runat="server" ID="TextBoxSummTeamsVotes" />
                <asp:TextBox runat="server" ID="TextBoxTeamsVoteLimit" />
                <asp:TextBox runat="server" ID="TextBoxSummInnovVotes" />
                <asp:TextBox runat="server" ID="TextBoxInnovVoteLimit" />
                <asp:TextBox runat="server" ID="TextBoxSummComitVotes" />
                <asp:TextBox runat="server" ID="TextBoxComitVoteLimit" />
                <asp:TextBox runat="server" ID="TextBoxSummResponseVotes" />
                <asp:TextBox runat="server" ID="TextBoxResponseVoteLimit" />                    
            </div>
        </section>
    </form>    
</body>
</html>
