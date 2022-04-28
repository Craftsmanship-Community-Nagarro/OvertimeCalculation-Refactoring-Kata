namespace Overtime
{
    public class Overtime
    {
        public Overtime(decimal hoursRate1, decimal hoursRate2)
        {
            this.hoursRate1 = hoursRate1;
            this.hoursRate2 = hoursRate2;
        }

        public decimal hoursRate1 { get; set; }
        public decimal hoursRate2 { get; set; }

        public override string ToString()
        {
            return $"hoursRate1:{hoursRate1}_hoursRate2:{hoursRate2}";
        }
    }
}
