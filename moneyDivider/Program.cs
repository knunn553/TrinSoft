namespace moneyDivider
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a dollar amount: ");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.Write("Please enter a valid positive dollar amount: ");
            }

            Console.Write("Enter an integer value: ");
            int divisions;
            while (!int.TryParse(Console.ReadLine(), out divisions) || divisions <= 0)
            {
                Console.Write("Please enter a valid positive integer: ");
            }

            decimal[] dividedAmounts = DivideAmount(amount, divisions);

            for (int i = 0; i < dividedAmounts.Length; i++)
            {
                Console.WriteLine($"Array[{i}] = {dividedAmounts[i]:C2}");
            }
        }

        static decimal[] DivideAmount(decimal amount, int divisions)
        {
            decimal[] result = new decimal[divisions];
            decimal baseValue = Math.Round(amount / divisions, 2, MidpointRounding.ToEven);

            for (int i = 0; i < divisions; i++)
            {
                result[i] = baseValue;
            }

            // Adjust the last element to ensure the sum matches the original amount exactly
            decimal total = baseValue * divisions;
            result[divisions - 1] = baseValue + (amount - total);

            return result;
        }
    }
}
