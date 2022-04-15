using System;

public class Account
{
    private decimal _balance;
    private string _name;

    public Account (string name, decimal startingBalance)
    {
        _name = name;
        _balance = startingBalance;
    }
    //Deposits money into the account
    public bool Deposit (decimal amountToAdd)
        {
            if (amountToAdd > 0)
            {
                _balance += amountToAdd;
                return true;
            }
            return false;
        }
    //Withdraws from the account balance
    public bool Withdraw (decimal amountToSubtract)
        {
            if ((amountToSubtract > 0 ) && (_balance > amountToSubtract))
            {
                _balance = _balance - amountToSubtract;
                return true;
            }
            return false;
        }
    //Gets the name under the account
    public string Name
    {
        get { return _name;}
    }

    public void Print ()
    {
        Console.WriteLine("Account Name: " + Name);
        Console.WriteLine ("Account Balance: " + _balance);
    }
}