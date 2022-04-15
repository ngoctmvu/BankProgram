using System;

public class TransferTransaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private decimal _amount;

    private WithdrawTransaction _theWithdraw;
    private DepositTransaction _theDeposit;

    private bool _executed = false;
    private bool _reversed = false;
    //private bool _succeeded = false;

    public bool Executed
    {
        get { return this._executed; }
    }
     public bool Success
    {
        get 
        { 
            if (this._theDeposit.Success && this._theWithdraw.Success) 
                return true;
            else
                return false; 
        }
    }
    public bool Reversed
    {
        get { return this._reversed; }
    }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        this._fromAccount = fromAccount;
        this._toAccount = toAccount;
        this._amount = amount;
        this._theWithdraw = new WithdrawTransaction(this._fromAccount, this._amount);
        this._theDeposit = new DepositTransaction(this._toAccount, this._amount);
    }

    public void Execute()
    {
        if (this._executed)
        {
            throw new Exception("Cannot execute this action as it has already been executed!");
        }

        this._theWithdraw.Execute();
        if (this._theWithdraw.Success)
        {
            //Console.Write("WITHDRAW SUCCESS");
            this._executed = true;
            this._theDeposit.Execute();
            if (this._theDeposit.Success)
            {
                //Console.Write("DEPOSIT SUCCESS");
                this._executed = true;
            }
            else
            {
                //Console.Write("SOMETHING WENT WRONG");
            this._theWithdraw.Rollback();
            }
        }
        else
        {
            throw new Exception("Cannot execute transfer! Could not withdraw funds.");
        }
        
    } 
    public void Rollback()
    {
        if (!this._executed)
        {
            throw new Exception("Cannot rollback as the transaction doesn't exist!");
        }

        if (this._reversed)
        {
            throw new Exception("Cannot rollback this transaction as it has already been done.");
        }

        if (this._theWithdraw.Success)
        {
            this._theWithdraw.Rollback();
        }

        if (this._theDeposit.Success)
        {
            this._theDeposit.Rollback();
        }

        this._reversed = true;
        this._executed = false;

    }

    public void Print()
    {
        if (this._theWithdraw.Success && this._theDeposit.Success)
        {
            Console.WriteLine("A transfer of " + this._amount + " from " + this._fromAccount.Name + "'s account to " + this._toAccount.Name + "'s account was successful.");
            Console.Write("   ");
            this._theDeposit.Print();
            Console.Write("   ");
            this._theWithdraw.Print();
        }
        else
        {
            Console.WriteLine("The tranfer was NOT successful.");
            if (this._reversed)
            Console.WriteLine("Transfer was reversed");
        }
    }
}