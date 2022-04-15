using System;

enum MenuOption
{
   Withdraw,
   Deposit,
   Transfer,
   Print,
   Quit,     
}

public class Program
{
    //Read out Menu
    static MenuOption ReadUserOption()
    {
        int option;

        Console.WriteLine("    ");
        Console.WriteLine("Welcome to BankOz");
        Console.WriteLine("**********************");
        Console.WriteLine("1. Withdraw");
        Console.WriteLine("2. Deposit");
        Console.WriteLine("3. Transfer");
        Console.WriteLine("4. Print");
        Console.WriteLine("5. Quit");
        Console.WriteLine("*********************");
        do
        {
            Console.WriteLine("Choose an option [1-5]");
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please enter a valid choice" +  ex.Message);
                Console.WriteLine("There was a problem reading in your input.");
                option = -1;
            }
            if (option > 5|| option < 1)
            {
                Console.WriteLine("Please enter a number between 1 and 5");
            }
        }
        while (option < 1 || option > 5);
        return  (MenuOption)(option - 1);
    }
    // Perform Withdrawal
    private static void DoWithdraw(Account account)
    {
        decimal amount;

        Console.WriteLine("How much would you like to withdraw?");
        amount = Convert.ToDecimal(Console.ReadLine());
        WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, amount);

        try
        {
            withdrawTransaction.Execute();
            withdrawTransaction.Print();
            //account.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
    //Perform Deposit
     private static void DoDeposit(Account account)
    {
        decimal amount;

        Console.WriteLine("How much would you like to deposit?");
        amount = Convert.ToDecimal(Console.ReadLine());
        DepositTransaction depositTransaction = new DepositTransaction(account, amount);
        
          try
        {
            depositTransaction.Execute();
            depositTransaction.Print();
            //account.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
    //Perform account transfer
    private static void doTransfer(Account acc1, Account acc2)
    {
        Console.Write("How much would you like to transfer into " + acc2.Name + "'s account?");
        decimal amount = Convert.ToDecimal(Console.ReadLine());
        TransferTransaction transferTransaction = new TransferTransaction (acc1, acc2, amount);

        try
        {
            transferTransaction.Execute();
            transferTransaction.Print();
            //transferTransaction.Rollback();
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }
    // Perform recepit print
     private static void DoPrint(Account account)
    {
        account.Print();
    }
    //Menu Functionality
    public static void Main()
    {        
        Account account = new Account("Ngoc's Bank Account",25000);  
        Account account2 = new Account("Tom",4000);      
        MenuOption userSelection;

        do
        {
            userSelection = ReadUserOption();
            switch(userSelection)
            {
                case MenuOption.Withdraw:
                DoWithdraw(account);
                //Console.WriteLine("1. Withdrawal Initiated");
                break;

                case MenuOption.Deposit:
                DoDeposit(account);
                //Console.WriteLine("2. Deposit Initiated");
                break;

                case MenuOption.Transfer:
                doTransfer(account, account2);
                //Console.WriteLine("3.Transfer Initiated);
                break;

                case MenuOption.Print:
                DoPrint(account);
                //Console.WriteLine("4. Print ");
                break;
                
                case MenuOption.Quit:
                //Quit();
                Console.WriteLine("Goodbye!");
                break;
            }
        }while (userSelection != MenuOption.Quit);
    }
}
