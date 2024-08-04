namespace AspireSmallFinance.Utilities
{
    public static class MathUtils
    {
        public static List<decimal> SplitAmount(decimal amount, int intervals)
        {
            List<decimal> result = new List<decimal>();
            decimal balanceAmount = amount;

            if (amount < 0)
            {
                throw new ArgumentException($"Amount should not be negative. Invalid amount : {amount} provided.");
            }
            if (intervals <= 0)
            {
                throw new ArgumentException($"Atleast 1 interval should be there, provided : {intervals} intervals.");
            }


            decimal periodicAmount = Math.Round(amount / intervals,2);

            while (balanceAmount > 0)
            {
                result.Add(periodicAmount);
                balanceAmount = balanceAmount - periodicAmount;

                if(balanceAmount == 0)
                {
                    return result;
                }

                if(balanceAmount < periodicAmount)
                {
                    result[intervals -1 ] += balanceAmount;
                    return result;
                }
            }

            return result;


        }
    }
}
