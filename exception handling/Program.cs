using System;

class Money
{
    public int Hryvnias { get; private set; }
    public int Kopecks { get; private set; }

    public Money(int hryvnias, int kopecks)
    {
        if (hryvnias < 0 || kopecks < 0 || (hryvnias == 0 && kopecks < 0))
            throw new ArgumentException("The amount cannot be negative.");

        Hryvnias = hryvnias + kopecks / 100;
        Kopecks = kopecks % 100;
    }

    private void Normalize()
    {
        if (Kopecks >= 100)
        {
            Hryvnias += Kopecks / 100;
            Kopecks %= 100;
        }
        else if (Kopecks < 0)
        {
            Hryvnias -= 1 + Math.Abs(Kopecks) / 100;
            Kopecks = 100 - Math.Abs(Kopecks) % 100;
        }

        if (Hryvnias < 0 || (Hryvnias == 0 && Kopecks < 0))
            throw new InvalidOperationException("Bankrupt");
    }

    public static Money operator +(Money a, Money b)
    {
        return new Money(a.Hryvnias + b.Hryvnias, a.Kopecks + b.Kopecks);
    }

    public static Money operator -(Money a, Money b)
    {
        int hryvnias = a.Hryvnias - b.Hryvnias;
        int kopecks = a.Kopecks - b.Kopecks;
        return new Money(hryvnias, kopecks);
    }

    public static Money operator *(Money a, int multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("The multiplier cannot be negative.");

        int totalKopecks = (a.Hryvnias * 100 + a.Kopecks) * multiplier;
        return new Money(0, totalKopecks);
    }

    public static Money operator /(Money a, int divisor)
    {
        if (divisor <= 0)
            throw new ArgumentException("The divisor must be greater than zero.");

        int totalKopecks = (a.Hryvnias * 100 + a.Kopecks) / divisor;
        return new Money(0, totalKopecks);
    }

    public static Money operator ++(Money a)
    {
        return new Money(a.Hryvnias, a.Kopecks + 1);
    }

    public static Money operator --(Money a)
    {
        return new Money(a.Hryvnias, a.Kopecks - 1);
    }

    public static bool operator <(Money a, Money b)
    {
        return (a.Hryvnias * 100 + a.Kopecks) < (b.Hryvnias * 100 + b.Kopecks);
    }

    public static bool operator >(Money a, Money b)
    {
        return (a.Hryvnias * 100 + a.Kopecks) > (b.Hryvnias * 100 + b.Kopecks);
    }

    public static bool operator ==(Money a, Money b)
    {
        return (a.Hryvnias == b.Hryvnias && a.Kopecks == b.Kopecks);
    }

    public static bool operator !=(Money a, Money b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return $"{Hryvnias} грн {Kopecks:D2} коп";
    }


}

class Program
{
    static void Main()
    {
        try
        {
            Money money1 = new Money(3, 50);
            Money money2 = new Money(5, 75);

            Console.WriteLine("Transactions with monetary amounts:");
            Console.WriteLine($"money1: {money1}");
            Console.WriteLine($"money2: {money2}");

            Console.WriteLine("\nAddition:");
            Console.WriteLine(money1 + money2);

            Console.WriteLine("\nSubtraction:");
            Console.WriteLine(money1 - money2);

            Console.WriteLine("\nMultiplication:");
            Console.WriteLine(money1 * 2);

            Console.WriteLine("\nDivision:");
            Console.WriteLine(money1 / 2);

            Console.WriteLine("\nComparison:");
            Console.WriteLine($"money1 > money2: {money1 > money2}");
            Console.WriteLine($"money1 < money2: {money1 < money2}");
            Console.WriteLine($"money1 == money2: {money1 == money2}");

            Console.WriteLine("\nIncrement:");
            Console.WriteLine(++money1);

            Console.WriteLine("\nDecrement:");
            Console.WriteLine(--money1);

            Console.WriteLine("\nSubtract to negative:");
            Console.WriteLine(money2 - money1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
