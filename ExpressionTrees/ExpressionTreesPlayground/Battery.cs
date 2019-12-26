namespace ExpressionTreesPlayground
{
    public class Battery
    {
        public int Energy { get; set; }

        public bool HasEnergy { get => Energy > 0; }

        public void Recharge(int energy)
        {
            Energy += energy;
        }
        
        public int UseEnergy(int energy)
        {
            Energy -= energy;
            return energy;
        }

        public string GetEnergyStatus(int energy, string status) => $"{energy}% - {status}";
    }
}
