namespace SGBank.Models.Response
{
    public class AccountDepositResponse:Response
    {
        public Account Account { get; set; }
        public decimal Amount { get; set; }
        public decimal OldBalance { get; set; }
    }
}