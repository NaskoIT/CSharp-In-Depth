namespace OptionalParameters.Library
{
    public class BankAccount
    {
        // Try changing the default value of the money parameter without pre-compiling the client
        // Try adding new optional parameter without pre-compiling the client
        public BankAccount(string accountHolder = default, decimal money = 1000)
        {
            this.AccountHolder = accountHolder;
            this.Money = money;
        }

        public string AccountHolder { get; set; }

        public decimal Money { get; set; }

        public override string ToString()
        {
            return $"Bank account of {this.AccountHolder} with {this.Money} in cash.";
        }
    }
}
