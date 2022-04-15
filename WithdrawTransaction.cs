using System;



public class WithdrawTransaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed;
    private bool _success;
    private bool _reversed;
    
    public bool Executed
    {
        get
        {
            return _executed;
        }
    }
    public bool Success
    {
        get
        {
            return _success;
        }
    }
    public bool Reversed
    {
        get 
        {
            return _reversed;
        }
    }
    public WithdrawTransaction (Account account, decimal amount)
    {
        this._account = account;
        this._amount = amount;
    }
    public void Execute()
    {
        if (this._executed)
        {
            throw new Exception ("Cannot execute this transaction");
        }
        this._executed = true;
        this._success = _account.Withdraw(_amount);
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
        this._success =! _account.Deposit(_amount);
    }
    public void Print()
    {
        if (this._success)
        {
            Console.WriteLine("A withdrawal of "+_amount + " from " +_account.Name + "'s account was successfully completed.");
        }
        else
        {
            Console.WriteLine("The withdrawal was not successful.");
            if(this._reversed)
            {
                Console.WriteLine("The withdrawal was reversed.");
            }
        }
        this._account.Print();
    }
}