using System;

public class DepositTransaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed;
    private bool _success;
    private bool _reversed;
   
    public bool Executed
    {
        get { return _executed; }
    }
    public bool Success
    {
        get { return _success; }
    }
    public bool Reversed
    {
        get { return _reversed; }
    }

    public DepositTransaction( Account account, decimal amount)
    {
        this._account = account;
        this._amount = amount;
    }

    public void Execute()
    {
        if (this._executed)
        {
            throw new Exception ("Cannot execute this deposit. It has already been done");
        }
        this._executed = true;
        this._success = _account.Deposit(_amount);
    }
    public void Rollback()
    {
        if (!this._executed)
        {
            throw new Exception ("This account has not been executed");
        }
        if (this._reversed)
        {
            throw new Exception ("Cannot rollback this transaction. It has already been reversed.");
        }
        this._reversed = true;
        this._executed = false;
        this._success =! _account.Withdraw(_amount);
    }
     public void Print()
    {
        if (this._success)
        {
            Console.WriteLine("A deposit of "+_amount + " from " +_account.Name + "'s account was successfully completed.");
        }
        else
        {
            Console.WriteLine("The deposit was not successful.");
            if(this._reversed)
            {
                Console.WriteLine("The deposit was reversed.");
            }
        }
        this._account.Print();
    }
}