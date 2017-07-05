using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using VoteWeb;

public partial class Vote : System.Web.UI.Page
{
   
    IVoteDataStrategy voteDataStrategy = CreateVoteDataStrategy();

    static private IVoteDataStrategy CreateVoteDataStrategy()
    {
        string voteDataStrategyName = ConfigurationManager.AppSettings["VoteDataStrategy"].ToString();
        if (voteDataStrategyName == "Debug")
            return new VoteDataDebug();
        else if (voteDataStrategyName == "Sharepoint")
            return new VoteDataSharepoint();
        else
            throw new Exception("Vote Data Strategy is not defined.");
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            Employee currentUser = GetCurrentUser();
            ShowCurrentUserInfo(currentUser);

            SelectedLanguage.Value = currentUser.SelectedLanguage;

            Employee votedUser = GetVotedUser();
            ShowVotedUserInfo(votedUser);
            
            if (!ClientScript.IsStartupScriptRegistered("OpenModalWindowScript"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "OpenModalWindowScript", "OpenModalWindow();", true);
            }
        }
        else
        {
            if (!ClientScript.IsStartupScriptRegistered("AnimationStopScript"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "AnimationStopScript", "AnimationStop();", true);
            }
        }
    }

    protected void ButtonVote_Click(object sender, EventArgs e)
    {       
        EmployeeVote vote = GetVoteFromUI();
        SaveVote(vote);
        
        if (!ClientScript.IsStartupScriptRegistered("ShowFinalWindowScript"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ShowFinalWindowScript", "showFinalWindow();", true);
        }       
    }

    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        Response.Redirect(ProgramClasses.ORG_CHART_LINK);
    }

    /// <summary>
    /// Returns voted account name
    /// </summary>
    /// <returns>Vote account name</returns>
    public string GetVotedAccount()
    {
        //ToDo - Request must be studyed -
        string account = Request.QueryString["Account"];
        return account;
    }

    /// <summary>
    /// Returns current user info
    /// </summary>
    /// <returns>Current user info</returns>
    private Employee GetCurrentUser()
    {
        Employee employee = new Employee(); 
        employee.Account = ProgramClasses.GetCurrentAccount(); // ToDo: Добавить стратегию
        employee.EmployeeName = voteDataStrategy.GetEmployeeName(employee.Account);
        employee.Department = voteDataStrategy.GetDepartment(employee.Account);
        employee.ImageUrl = voteDataStrategy.GetImageUrl(employee.Account);
        // ToDo: Текущие и максимальные голоса
        employee.SelectedLanguage = voteDataStrategy.GetSelectedLanguage(employee.Account);
        return employee;
    }    

    /// <summary>
    /// Loads vote data from User Interface and returns it
    /// </summary>
    /// <returns>Vote</returns>
    private EmployeeVote GetVoteFromUI()
    {
        EmployeeVote vote = new EmployeeVote();
        
        vote.Date = DateTime.Now;
        vote.AccountFrom = ProgramClasses.GetCurrentAccount(); // ToDo: Добавить стратегию
        vote.AccountTo = GetVotedAccount(); // ToDo: Добавить стратегию
        switch (RadioButtonListValues.SelectedValue)
        {
            case "TeamSpirit":
                vote.CorporateValue = Value.TeamSpirit;
                break;
            case "Innovation":
                vote.CorporateValue = Value.Innovation;
                break;
            case "Commitment":
                vote.CorporateValue = Value.Commitment;
                break;
            case "Responsibility":
                vote.CorporateValue = Value.Responsibility;
                break;
            default:
                vote.CorporateValue = Value.Unknown;
                break;
        }

        vote.Comment = TextBoxComment.Text;

        return vote;
    }
 

    /// <summary>
    /// Returns voted user info
    /// </summary>
    /// <returns>Voted user info</returns>
    private Employee GetVotedUser()
    {
        Employee employee = new Employee();
        employee.Account = GetVotedAccount();
        employee.EmployeeName = voteDataStrategy.GetEmployeeName(employee.Account);
        employee.Department = voteDataStrategy.GetDepartment(employee.Account);
        employee.ImageUrl = voteDataStrategy.GetImageUrl(employee.Account);

        return employee;
    }

    /// <summary>
    /// Saves vote data
    /// </summary>
    /// <param name="vote">Vote structure</param>
    private void SaveVote(EmployeeVote vote)
    {
        voteDataStrategy.SaveVote(vote);        
    }  

/*
    /// <summary>
    /// Saves vote data to XML
    /// </summary>
    /// <param name="vote">Vote structure</param>
    private void SaveVoteToXML(ProgramClasses.EmployeeVote vote)
    {
        string xmlFileName = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\VoteResult.xml";

        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFileName);

        XmlNode rootNode = doc.SelectSingleNode("Votes");
        XmlElement voteNode = doc.CreateElement("Vote");

        XmlElement fromNode = doc.CreateElement("AccountFrom");
        fromNode.InnerText = vote.AccountFrom;
        voteNode.AppendChild(fromNode);

        XmlElement toNode = doc.CreateElement("AccountTo");
        toNode.InnerText = vote.AccountTo;
        voteNode.AppendChild(toNode);

        XmlElement valueNode = doc.CreateElement("Value");
        valueNode.InnerText = vote.CorporateValue.ToString();
        voteNode.AppendChild(valueNode);

        XmlElement commentNode = doc.CreateElement("Comment");
        commentNode.InnerText = vote.Comment;
        voteNode.AppendChild(commentNode);

        XmlElement dateNode = doc.CreateElement("Date");
        dateNode.InnerText = vote.Date.ToString();
        voteNode.AppendChild(dateNode);

        rootNode.AppendChild(voteNode);

        doc.Save(xmlFileName);
    }

    */

    /// <summary>
    /// Shows current user info on the interface
    /// </summary>
    /// <param name="currentUser">Current user info</param>
    private void ShowCurrentUserInfo(Employee currentUser)
    {
        LabelFromEmployeeName.Text = currentUser.EmployeeName;
        LabelFromEmployeeDep.Text = currentUser.Department;
        ImageFromEmployee.ImageUrl = currentUser.ImageUrl;
        TextBoxSummTeamsVotes.Text = voteDataStrategy.GetCurrentVoteCountByAccount(currentUser.Account, "TeamSpirit").ToString();
        TextBoxTeamsVoteLimit.Text = voteDataStrategy.GetMaxVoteCountByAccount(currentUser.Account, "TeamSpirit").ToString();
        TextBoxSummInnovVotes.Text = voteDataStrategy.GetCurrentVoteCountByAccount(currentUser.Account, "Innovation").ToString();
        TextBoxInnovVoteLimit.Text = voteDataStrategy.GetMaxVoteCountByAccount(currentUser.Account, "Innovation").ToString();
        TextBoxSummComitVotes.Text = voteDataStrategy.GetCurrentVoteCountByAccount(currentUser.Account, "Commitment").ToString();
        TextBoxComitVoteLimit.Text = voteDataStrategy.GetMaxVoteCountByAccount(currentUser.Account, "Commitment").ToString();
        TextBoxSummResponseVotes.Text = voteDataStrategy.GetCurrentVoteCountByAccount(currentUser.Account, "Responsibility").ToString();
        TextBoxResponseVoteLimit.Text = voteDataStrategy.GetMaxVoteCountByAccount(currentUser.Account, "Responsibility").ToString();        
    }

    /// <summary>
    /// Shows voted user info on the interface
    /// </summary>
    /// <param name="currentUser"></param>
    private void ShowVotedUserInfo(Employee votedUser)
    {
        LabelToEmployeeName.Text = votedUser.EmployeeName;
        LabelToEmployeeDep.Text = votedUser.Department;
        ImageToEmployee.ImageUrl = votedUser.ImageUrl;
    }
}