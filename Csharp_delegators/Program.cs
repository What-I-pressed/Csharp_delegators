using System.Diagnostics;
using System.Runtime.InteropServices;

arrDel del;
Action actionDel;
Func<int, int, double> funcDel;


int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9};
int[] arr2;
while (true)
{
    Console.WriteLine("\nShow even numbers - [0]\n" +
        "Odd numbers - [1]\nPrime numbers - [2]\n" +
        "Fibonacci numbers - [3]\nExit - [4]");
    short option = short.Parse(Console.ReadLine());
    if (option == 4) break;
    del = option == 0 ? ActiOnArr.Even : option == 1 ?
        ActiOnArr.Odd : option == 2 ? ActiOnArr.Prime : ActiOnArr.Fibonacci;
    foreach (int i in del(arr)) Console.Write(i + " ");
}


while (true)
{
    Console.WriteLine("\nShow current time - [0]\n" +
        "Current date - [1]\nCurrent day of week - [2]\n" +
        "Calculate area of triangle - [3]\n" +
        "Calculate area of rectangle - [4]\nExit - [5]");
    short option = short.Parse(Console.ReadLine());
    if (option == 5) break;
    if(option == 0 || option == 1 || option == 2)
    {
        actionDel = option == 0 ? Time.CurrentTime : option == 1 ?
            Time.CurrentDate : Time.CurrentDayOfWeek;
        actionDel();
        continue;
    }
    int side, height;
    if (option == 3)
    {
        Console.Write("Enter triangle side length : ");
        side = int.Parse(Console.ReadLine());
        Console.Write("Enter triangle height length : ");
        height = int.Parse(Console.ReadLine());
        funcDel = Area.Triangle;
    }
    else {
        Console.Write("Enter rectangle width : ");
        side = int.Parse(Console.ReadLine());
        Console.Write("Enter rectangle heigth : ");
        height = int.Parse(Console.ReadLine());
        funcDel = Area.Rectangle;
    }
    Console.WriteLine(funcDel(side, height));
}

Console.WriteLine("\n");
Card card = new Card();
card.Balance = 10000;
card.CreditLimit = 5000;
card.OnBalanceRefill += Message.ShowMessage;
card.OnFundsSpending += Message.ShowMessage;
card.OnStartOfUsingCreditFunds += Message.ShowMessage;
card.OnLimitReach += Message.ShowMessage;
card.OnPinChange += Message.ShowMessage;
card.Refill(2000);
card.FundsSpending(14000);
card.FundsSpending(7000);
card.ChangePIN("1234");




class ActiOnArr
{
    public static int[] Even(int[] x)
    {
        List<int> list = new List<int>();
        foreach(int i in x) if(i % 2 == 0) list.Add(i);
        return list.ToArray();
    }

    public static int[] Odd(int[] x)
    {
        List<int> list = new List<int>();
        foreach (int i in x) if (i % 2 == 1) list.Add(i);
        return list.ToArray();
    }

    public static int[] Prime(int[] x)
    {
        List<int> list = new List<int>();
        foreach (int i in x) if (i > 1 && isPrime(i)) list.Add(i);
        return list.ToArray();
    }

    public static int[] Fibonacci(int[] x)
    {
        List<int> list = new List<int>();
        foreach (int i in x) if (isFibonacci(i)) list.Add(i);
        return list.ToArray();
    }

    public static bool isFibonacci(int x)
    {
        for (int i = 1, j = 0; j + i <= x; i += j, j = i -j) if (i + j == x) return true;
        return false;
    }

    public static bool isPrime(int x)
    {
        for (int i = 2; i * i <= x; i++) if (x % i == 0) return false;
        return true;
    }
}

class Time
{
    public static void CurrentTime()
    {
        Console.WriteLine($"{DateTime.Now.Hour}:{DateTime.Now.Minute}");
    }

    public static void CurrentDate()
    {
        Console.WriteLine($"{DateTime.Now.Day} of {DateTime.Now.Month}");
    }

    public static void CurrentDayOfWeek()
    {
        Console.WriteLine(DateTime.Now.DayOfWeek);
    }
}

class Area
{
    public static double Triangle(int side, int height)
    {
        return 0.5 * side * height;
    }

    public static double Rectangle(int a, int b)
    {
        return a * b;
    }
}

class Message
{

    static public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}

class Card
{
    public event eventType OnBalanceRefill;
    public event eventType OnFundsSpending;
    public event eventType OnStartOfUsingCreditFunds;
    public event eventType OnLimitReach;
    public event eventType OnPinChange;



    public string ID { get;set; } 
    public string FullName { get;set; }
    public string Validity { get;set; }
    public string PIN { get;set; }
    public int CreditLimit { get;set; }
    public int Balance;

    public void Refill(int amount)
    {
        Balance += amount;
        OnBalanceRefill.Invoke("Balance refilled successfully");
    }


    public void FundsSpending(int amount)
    {
        if(Balance + CreditLimit >= amount)
        {
            Balance -= amount;
            OnFundsSpending.Invoke($"{amount}$ was spent");
            if(Balance < 0 && Balance + amount >= 0)
            {
                OnStartOfUsingCreditFunds?.Invoke("You have started to use credit funds");
            }
        }
        else
        {
            OnLimitReach.Invoke("Limit of credit funds reached");
        }
    }

    public void ChangePIN(string newPin)
    {
        PIN = newPin;
        OnPinChange.Invoke("PIN code changed successfully");
    }
}




public delegate int[] arrDel(int[] x);
public delegate void eventType(string message);