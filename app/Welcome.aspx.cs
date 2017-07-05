using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Configuration;
using VoteWeb;

public partial class Welcome : System.Web.UI.Page
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
            throw new Exception("Vote Data Stratefy is not defined.");
    }

    protected Employee currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            currentUser = GetCurrentUser();

            if (currentUser.WelcomeNoShowCheck)
            {
                string account = Request.QueryString["Account"];
                Response.Redirect("~/Vote.aspx?Account=" + account);
            }

            SelectedLanguage.Value = currentUser.SelectedLanguage;

            if (!ClientScript.IsStartupScriptRegistered("RestoreLanguageScript"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "RestoreLanguageScript", "RestoreLanguage();", true);
            }

        }
    }

    protected void IContributorPanel_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://leadershipmodel-autopositionnement.safe.socgen/#contributor");
    }    

    protected void IManagerPanel_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://leadershipmodel-autopositionnement.safe.socgen/#manager");
    }


    protected void ButtonNEXT_Click(object sender, EventArgs e)
    {
        voteDataStrategy.SaveOptions(currentUser);
        string account = Request.QueryString["Account"];
        Response.Redirect("~/Vote.aspx?Account=" + account);
    }

    private Employee GetCurrentUser()
    {
        Employee employee = new Employee();
        employee.Account = ProgramClasses.GetCurrentAccount();
        employee.WelcomeNoShowCheck = voteDataStrategy.GetWelcomeNoShowCheck(employee.Account);
        employee.SelectedLanguage = voteDataStrategy.GetSelectedLanguage(employee.Account);
        return employee;
    }
}