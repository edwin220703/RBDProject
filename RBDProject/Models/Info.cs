namespace RBDProject.Models
{
    public class Info
    {
        public int CountArt { get; set; } = 0;

        public int CountCxc { get; set; } = 0;

        public int CountFact { get; set; } = 0;

        public int CountCli { get; set; } = 0;

        public WeekSell[] ShellWeek { get; set; } = new WeekSell[0];

        public MonthShell[] MonthSell { get; set; } = new MonthShell[0];
    }

    public class WeekSell
    {
        public string Day { get; set; } = string.Empty;

        public double Cant { get; set; } = 0;
    }

    public class MonthShell
    {
        public string MONTH { get; set; } = string.Empty;

        public double Cant { get; set; } = 0;
    }
}
