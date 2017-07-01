<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeStats.aspx.cs" Inherits="Stats" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<link rel="stylesheet" type="text/css" href="/css/Vote.css" />
    <title>VoteWebStats</title>
        <script type="text/javascript">
            function LoadVotePage() {
                //get it if you need: location.pathname = '/Vote.aspx';
            }
            window.onload = function () {

            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div class="headPage">
                <div class="ImgLogo"><asp:Image runat="server" Height="100px" Width="442px" ImageUrl="~/Images/logo.png"/></div>
                <div id="ButtonsCheckedLang" class="ButtonsCheckedLang">
                    <%--<input type="button" id="ButtonStats" value="VOTE" runat="server" onclick="LoadVotePage()" />--%>
                </div>
           </div>
        </header>
        <section>
            <div>
                <h1>EMPLOYEE STATISTICS</h1>
                <p class="paragText"></p>
            </div>        
            <div class="ColumnsVoteState">                
                <div id="TeamSpiritState">
                    <%--<div class="GoldenMedal"></div>
                    <div class="SilverMedal"></div>
                    <div class="BronzeMedal"></div>--%>
                    <div style="text-align:center; font-size: 22px; padding: 5px;">Team Spirit</div>
                    <div>
                        <asp:Label runat="server" Style="padding-left: 30px; font-size:20px;">Name</asp:Label>
                        <asp:Label runat="server" Style="position:absolute; right:15px; font-size:20px;">Count</asp:Label>
                    </div>
                    <div style ="background: white;">
                        <asp:Label runat="server" ID="TeamSpiritCongratsNamePlaseOne" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="TeamSpiritCongratsCountPlaseOne" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div style="background: #f6f3f3">
                        <asp:Label runat="server" ID="TeamSpiritCongratsNamePlaseTwo" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="TeamSpiritCongratsCountPlaseTwo" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server" ID="TeamSpiritCongratsNamePlaseThree" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="TeamSpiritCongratsCountPlaseThree" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div style="padding-right: 15px; padding-left:30px;">
                        <asp:GridView ID="GridViewTeamSpiritStats" runat="server"
                        AutoGenerateColumns="False" ShowHeader="false">
                        <Columns>
                            <asp:BoundField DataField ="EmployeeName" ItemStyle-Width ="110px" HeaderStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField ="ValueCount"  ItemStyle-Width ="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                        </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div id="InnovationState">
                    <%--<div class="GoldenMedal"></div>
                    <div class="SilverMedal"></div>
                    <div class="BronzeMedal"></div>--%>
                    <div style="text-align:center; font-size: 22px; padding: 5px;">Innovation</div>
                    <div>
                        <asp:Label runat="server" Style="padding-left: 30px; font-size:20px;">Name</asp:Label>
                        <asp:Label runat="server" Style="position:absolute; right:15px; font-size:20px;">Count</asp:Label>
                    </div>
                    <div style ="background: white;">
                        <asp:Label runat="server" ID="InnovationCongratsNamePlaseOne" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="InnovationCongratsCountPlaseOne" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div style="background:  #f6f3f3;">
                        <asp:Label runat="server" ID="InnovationCongratsNamePlaseTwo" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="InnovationCongratsCountPlaseTwo" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server" ID="InnovationCongratsNamePlaseThree" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="InnovationCongratsCountPlaseThree" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div style="padding-right: 15px; padding-left:30px;">
                        <asp:GridView ID="GridViewInnovationStats" runat="server"
                            AutoGenerateColumns="False" ShowHeader="false">
                            <Columns>
                                <asp:BoundField DataField ="EmployeeName" ItemStyle-Width ="110px" HeaderStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField ="ValueCount"  ItemStyle-Width ="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div id="CommitmentState" > 
                    <div style="text-align:center; font-size: 22px; padding: 5px;">Commitment</div>
                    <div>
                        <asp:Label runat="server" Style="padding-left: 30px; font-size:20px;">Name</asp:Label>
                        <asp:Label runat="server" Style="position:absolute; right:15px; font-size:20px;">Count</asp:Label>
                    </div>
                    <div style ="background: white;">
                        <asp:Label runat="server" ID="CommitmentCongratsNamePlaseOne" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="CommitmentCongratsCountPlaseOne" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div style="background: #f6f3f3">
                        <asp:Label runat="server" ID="CommitmentCongratsNamePlaseTwo" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="CommitmentCongratsCountPlaseTwo" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server" ID="CommitmentCongratsNamePlaseThree" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="CommitmentCongratsCountPlaseThree" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div style="padding-right: 15px; padding-left:30px;">               
                        <asp:GridView ID="GridViewCommitmentStats" runat="server"
                            AutoGenerateColumns="False" ShowHeader="false">
                            <Columns>
                                <asp:BoundField DataField ="EmployeeName" ItemStyle-Width ="110px" HeaderStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField ="ValueCount"  ItemStyle-Width ="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div id="ResponsibilityState" >
                    <div style="text-align:center; font-size: 22px; padding: 5px;">Responsibility</div>
                    <div>
                        <asp:Label runat="server" Style="padding-left: 30px; font-size:20px;">Name</asp:Label>
                        <asp:Label runat="server" Style="position:absolute; right:15px; font-size:20px;">Count</asp:Label>
                    </div>
                    <div style ="background: white;">
                        <asp:Label runat="server" ID="ResponsibilityCongratsNamePlaseOne" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="ResponsibilityCongratsCountPlaseOne" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div style="background: #f6f3f3">
                        <asp:Label runat="server" ID="ResponsibilityCongratsNamePlaseTwo" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="ResponsibilityCongratsCountPlaseTwo" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server" ID="ResponsibilityCongratsNamePlaseThree" Style="padding-left: 30px; font-size:20px;"></asp:Label>
                        <asp:Label runat="server" ID="ResponsibilityCongratsCountPlaseThree" Style="position:absolute; right:15px; font-size:20px;"></asp:Label>
                    </div>
                    <div style="padding-right: 15px; padding-left:30px;">
                        <asp:GridView ID="GridViewResponsibilityStats" runat="server"
                            AutoGenerateColumns="False" ShowHeader="false">
                            <Columns>
                                <asp:BoundField DataField ="EmployeeName" ItemStyle-Width ="110px" HeaderStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField ="ValueCount"  ItemStyle-Width ="110px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </section>      
    </form>
</body>
</html>
