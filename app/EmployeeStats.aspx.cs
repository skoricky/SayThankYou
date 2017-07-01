using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using NLog;
using VoteWeb;

public partial class Stats : System.Web.UI.Page
{
 /*
    //Класс для отдельного столбца таблицы
    private class StatTableColumn
    {
        public GridView gridViewColumn { get; set; }
        public Label labelNamePlaceOne { get; set; }
        public Label labelCountPlaceOne { get; set; }
        public Label labelNamePlaceTwo { get; set; }
        public Label labelCountPlaceTwo { get; set; }
        public Label labelNamePlaceThree { get; set; }
        public Label labelCountPlaceThree { get; set; }
    }

    //Класс, содержащий поля и методы для формированиия таблицы
    private class StatTable
    {        
        public StatTableColumn statTableColumn { get; set; }
        public List<ProgramClasses.StatValue> statValue { get; set; }
        public string value { get; set; }
        
        public void DataBingToColumn()
        {
            if (statValue.Count > 0)
            {
                statTableColumn.gridViewColumn.DataSource = statValue.OrderByDescending(a => a.ValueCount);
                statTableColumn.gridViewColumn.DataBind();
                SetCongratsPlace();
            }
                
        }

        private void SetCongratsPlace()
        {
            int sumRows;
            List<Label> Labels = new List<Label>() { statTableColumn.labelNamePlaceOne, statTableColumn.labelCountPlaceOne, statTableColumn.labelNamePlaceTwo, statTableColumn.labelCountPlaceTwo, statTableColumn.labelNamePlaceThree, statTableColumn.labelCountPlaceThree };
            

            if(statValue.Count >= 3)
            {
                sumRows = 3;
            } else
            {
                sumRows = statValue.Count;
            }

            for(int i = 0; i < sumRows; i++)
            {
                int j = i * 2;
                Labels[j].Text = statTableColumn.gridViewColumn.Rows[i].Cells[0].Text;
                Labels[j+1].Text = statTableColumn.gridViewColumn.Rows[i].Cells[1].Text;                
            }

            for (int i = 0; i < sumRows; i++)
            {
                int indexRow = statValue.FindIndex(x => x.EmployeeName == statTableColumn.gridViewColumn.Rows[i].Cells[0].Text);
                statValue.RemoveAt(indexRow);
            }
            statTableColumn.gridViewColumn.DataBind();
        }
    }

    //Класс-сборщик статистики по каждому качеству в общий лист statValues
    private class Stat
    {

        List<List<ProgramClasses.StatValue>> statValues = new List<List<ProgramClasses.StatValue>>();        

        public List<List<ProgramClasses.StatValue>> GetStatEmployee()
        {
            ProgramClasses.EmployeeVoteData getData = new ProgramClasses.EmployeeVoteData();

            for (int i = 0; i < 4; i++)
            {
                ProgramClasses.Value value = (ProgramClasses.Value)i;
                string valueName = value.ToString();
                List<ProgramClasses.StatValue> statValue = getData.GetStatByValue(valueName);
                statValues.Add(statValue);                
            }

            return statValues;

        }     

        public void ClearStat()
        {
 
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ProgramClasses.TEST_MODE)
        {
            
        }
        if (!IsPostBack)
        {
            SetStatTable();
        }
        else
        {
           
        }
    }

    //Метод, позволяющий получить требуемый объект класса StatTableColumn by value, содержащий все поля столбца таблицы статистики типов GridView и Label
    private StatTableColumn GetTableColumn(string value)
    {
        StatTableColumn StatTableColumn = new StatTableColumn();

        switch (value)
        {
            case "TeamSpirit":
                StatTableColumn.gridViewColumn = GridViewTeamSpiritStats;
                StatTableColumn.labelNamePlaceOne = TeamSpiritCongratsNamePlaseOne;
                StatTableColumn.labelCountPlaceOne = TeamSpiritCongratsCountPlaseOne;
                StatTableColumn.labelNamePlaceTwo = TeamSpiritCongratsNamePlaseTwo;
                StatTableColumn.labelCountPlaceTwo = TeamSpiritCongratsCountPlaseTwo;
                StatTableColumn.labelNamePlaceThree = TeamSpiritCongratsNamePlaseThree;
                StatTableColumn.labelCountPlaceThree = TeamSpiritCongratsCountPlaseThree;
                break;
            case "Innovation":
                StatTableColumn.gridViewColumn = GridViewInnovationStats;
                StatTableColumn.labelNamePlaceOne = InnovationCongratsNamePlaseOne;
                StatTableColumn.labelCountPlaceOne = InnovationCongratsCountPlaseOne;
                StatTableColumn.labelNamePlaceTwo = InnovationCongratsNamePlaseTwo;
                StatTableColumn.labelCountPlaceTwo = InnovationCongratsCountPlaseTwo;
                StatTableColumn.labelNamePlaceThree = InnovationCongratsNamePlaseThree;
                StatTableColumn.labelCountPlaceThree = InnovationCongratsCountPlaseThree;
                break;
            case "Commitment":
                StatTableColumn.gridViewColumn = GridViewCommitmentStats;
                StatTableColumn.labelNamePlaceOne = CommitmentCongratsNamePlaseOne;
                StatTableColumn.labelCountPlaceOne = CommitmentCongratsCountPlaseOne;
                StatTableColumn.labelNamePlaceTwo = CommitmentCongratsNamePlaseTwo;
                StatTableColumn.labelCountPlaceTwo = CommitmentCongratsCountPlaseTwo;
                StatTableColumn.labelNamePlaceThree = CommitmentCongratsNamePlaseThree;
                StatTableColumn.labelCountPlaceThree = CommitmentCongratsCountPlaseThree;
                break;
            case "Responsibility":
                StatTableColumn.gridViewColumn = GridViewResponsibilityStats;
                StatTableColumn.labelNamePlaceOne = ResponsibilityCongratsNamePlaseOne;
                StatTableColumn.labelCountPlaceOne = ResponsibilityCongratsCountPlaseOne;
                StatTableColumn.labelNamePlaceTwo = ResponsibilityCongratsNamePlaseTwo;
                StatTableColumn.labelCountPlaceTwo = ResponsibilityCongratsCountPlaseTwo;
                StatTableColumn.labelNamePlaceThree = ResponsibilityCongratsNamePlaseThree;
                StatTableColumn.labelCountPlaceThree = ResponsibilityCongratsCountPlaseThree;
                break;
            default:
                break;

        }
        return StatTableColumn;
    }

    protected void SetStatTable()
    {
        if (ProgramClasses.TEST_MODE)
        {
            //Тестовый код
            List<ProgramClasses.StatValue> valueData = new List<ProgramClasses.StatValue>();                     

            valueData.Add(new ProgramClasses.StatValue() { EmployeeName = "Max Pain", ValueCount = 365 });
            valueData.Add(new ProgramClasses.StatValue() { EmployeeName = "Harly Quinn", ValueCount = 815 });
            //valueData.Add(new ProgramClasses.StatValue() { EmployeeName = "Dead Pool", ValueCount = 118 });
            valueData.Add(new ProgramClasses.StatValue() { EmployeeName = "Spider Man", ValueCount = 400 });
            //valueData.Add(new ProgramClasses.StatValue() { EmployeeName = "Dead Shot", ValueCount = 19 });

            List<ProgramClasses.StatValue> valueData1 = new List<ProgramClasses.StatValue>();

            //valueData1.Add(new ProgramClasses.StatValue() { EmployeeName = "Max Pain", ValueCount = 100 });
            //valueData1.Add(new ProgramClasses.StatValue() { EmployeeName = "Harly Quinn", ValueCount = 8 });
            //valueData1.Add(new ProgramClasses.StatValue() { EmployeeName = "Dead Pool", ValueCount = 238 });
            //valueData1.Add(new ProgramClasses.StatValue() { EmployeeName = "Spider Man", ValueCount = 505 });
            //valueData1.Add(new ProgramClasses.StatValue() { EmployeeName = "Dead Shot", ValueCount = 3 });

            List<ProgramClasses.StatValue> valueData2 = new List<ProgramClasses.StatValue>();

            valueData2.Add(new ProgramClasses.StatValue() { EmployeeName = "Max Pain", ValueCount = 601 });
            valueData2.Add(new ProgramClasses.StatValue() { EmployeeName = "Harly Quinn",ValueCount = 889 });
            valueData2.Add(new ProgramClasses.StatValue() { EmployeeName = "Dead Pool", ValueCount = 15 });
            valueData2.Add(new ProgramClasses.StatValue() { EmployeeName = "Spider Man", ValueCount = 15 });
            valueData2.Add(new ProgramClasses.StatValue() { EmployeeName = "Dead Shot", ValueCount = 8 });

            List<ProgramClasses.StatValue> valueData3 = new List<ProgramClasses.StatValue>();

            valueData3.Add(new ProgramClasses.StatValue() { EmployeeName = "Max Pain", ValueCount = 10 });
            //valueData3.Add(new ProgramClasses.StatValue() { EmployeeName = "Harly Quinn", ValueCount = 235 });
            valueData3.Add(new ProgramClasses.StatValue() { EmployeeName = "Dead Pool", ValueCount = 8 });
            valueData3.Add(new ProgramClasses.StatValue() { EmployeeName = "Spider Man", ValueCount = 333 });
            valueData3.Add(new ProgramClasses.StatValue() { EmployeeName = "Dead Shot", ValueCount = 16 });

            List<List<ProgramClasses.StatValue>> statVotes = new List<List<ProgramClasses.StatValue>>() { valueData, valueData1, valueData2, valueData3 };

            for (int i = 0; i < 4; i++)
            {
                ProgramClasses.Value value = (ProgramClasses.Value)i;
                string valueName = value.ToString();
                StatTableColumn statTableColumn = GetTableColumn(valueName);
                StatTable StatTable = new StatTable() { statValue = statVotes[i], statTableColumn = statTableColumn, value = valueName };
                StatTable.DataBingToColumn();
            }
            
        }
        else
        {
            Stat stat = new Stat();
            List<List<ProgramClasses.StatValue>> statVotes = stat.GetStatEmployee();

            for (int i = 0; i < 4; i++)
            {
                ProgramClasses.Value value = (ProgramClasses.Value)i;
                string valueName = value.ToString();
                StatTableColumn statTableColumn = GetTableColumn(valueName);
                StatTable StatTable = new StatTable() { statValue = statVotes[i], statTableColumn = statTableColumn};
                StatTable.DataBingToColumn();
            }

        }
    }
    
    protected void ClearStatsTable()
    {
 
    }
    */
}