using System;


class Program
{

    /*1. Zadatak
    Napisati program koji upisuje dva cjelobrojna broja i ispisuje rezultat dijeljenja ta dva
    broja. Rezultat treba ispisati u sljedećim formatima (Currency, Integer, Scientific,
    Fixed-point, General, Number, Hexadecimal).
    Prilikom upisa nekog podatka sa tipkovnice, podatak se učitava kao tip string, a ako
    nam treba tip int moramo ga pretvoriti uz pomoć ugrađenih metoda.
    Pripaziti da se obrade sve iznimke */
    private static void Zadatak1()
    {
        Console.WriteLine("Enter first number:");
        int num1 = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter second number:");
        int num2 = int.Parse(Console.ReadLine());
        if (Convert.ToInt32(num2) == 0)
            throw new Exception("you cant divide with zero");
        float result = (float)num1 / num2;
        Console.WriteLine("currency---------> ");
        Console.WriteLine("{0:C}", result);
        Console.WriteLine("scientific---------> ");
        Console.WriteLine("{0:#.##E+00}", result);
        Console.WriteLine("fixed-point---------> ");
        Console.WriteLine("{0:F}", result);
        Console.WriteLine("general---------> ");
        Console.WriteLine("{0:G}", result);
        //Console.WriteLine("hexadecimal---------> ");
        //Console.WriteLine((BitConverter.DoubleToInt64Bits(Convert.ToDouble(result))).ToString("X"));
        Console.WriteLine("Numeric---------> ");
        Console.WriteLine("{0:N}", result);

    }

    /*2. Zadatak
    Napisati program koji sadrži dvije varijable, jednu tipa int, a drugu tipa long u koju će
    biti zapisana najveća moguća vrijednost za tip long. Varijablu tipa long treba
    pridružiti varijabli tipa int, s tim da se obradi iznimka ako dođe do preljeva
    (overflow).
    Pomoć: vidjeti “checked” u MSDN */
    private static void Zadatak2()
    {
        int x = 1;
        long y = Int64.MaxValue;
        try
        {
            checked
            {
                Console.WriteLine(y + x);
            }
        }
        catch (OverflowException e)
        {
            Console.WriteLine(e.Message); 
        } 
    }

    

    /*3. Zadatak
    Napisati program za bankare koji ima deklariran pobrojani tip podataka u kojem se
    nalaze vrste računa(Štednja , Tekući račun, Žiro račun). Unutar programa deklarirati
    strukturu BankAccount koja će sadržavati tri varijable, broj računa, iznos na računu i
    vrstu računa.
    Program treba deklarirati polje struktura BankAccount od 5 elemenata, te napraviti
    izbornik koji će imati opcije, upisa novog računa, i ispis svih računa. Za ispis svih
    računa koristiti “foreach” iteraciju.*/

    enum AccountType
    {
        STEDNJA,
        TEKUCI,
        ZIRO
    }

    struct BankAccount
    {
        public int accountNumber;
        public string accountAmount;
        public AccountType accountType;

        public void SetBankAccount(int accountNumber, string accountAmount, AccountType accountType)
        {
            this.accountNumber = accountNumber;
            this.accountAmount = accountAmount;
            this.accountType = accountType;
        }

        public void DisplayAccount()
        {
            Console.WriteLine("BankAccount:");
            Console.WriteLine("\taccountNumber    : " + accountNumber);
            Console.WriteLine("\taccountAmount   : " + accountAmount);
            Console.WriteLine("\taccountType   : " + accountType);
            Console.WriteLine("\n");
        }

    };

    private static void Zadatak3()
    {
        BankAccount[] bankAccounts = new BankAccount[5];
        for (int i = 0; i < bankAccounts.Length; i++)
        {
 
            string accountType;
            do
            {
                Console.WriteLine("Enter accountType: STEDNJA, TEKUCI OR ZIRO");
                accountType = Console.ReadLine();
            } while (accountType != "STEDNJA" && accountType != "TEKUCI" && accountType != "ZIRO");
               
            AccountType a = (AccountType)Enum.Parse(typeof(AccountType), accountType);

            Console.WriteLine("Enter accountAmount:");
            string accountAmount = Console.ReadLine();


            bankAccounts[i] = new BankAccount();
            bankAccounts[i].SetBankAccount(i, accountAmount, a);
           
        }
        foreach( BankAccount a in bankAccounts)
        {
            a.DisplayAccount();
        }
    }

    static void Main(string[] args)
    {
        //Zadatak1();
        //Zadatak2();
        Zadatak3();
        
    }
}