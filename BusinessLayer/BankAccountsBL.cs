﻿using DataLayer;
using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using CommonLayer.CustomExceptions;

namespace BusinessLayer
{
    public class BankAccountsBL
    {
        public IQueryable<AccountType> GetAccountTypes()
        {
            return new BankAccountsRepository().GetAccountTypes();
        }

        public IQueryable<BankAccount> GetBankAccounts()
        {
            return new BankAccountsRepository().GetBankAccounts();
        }

        public IQueryable<BankAccount> GetBankAccounts(string username, int accountTypeId)
        {
            //validation
            return new BankAccountsRepository().GetBankAccounts(username, accountTypeId);
        }

        public void OpenNewBankAccount(BankAccount b)
        {
             new BankAccountsRepository().OpenNewBankAccount(b);
        }

        public double GetCurrencyRate(string currencyFrom, string currencyTo)
        {
            //Eur > GBP : 0.8
            //http://api.fixer.io/latest?base=USD

            if (currencyFrom == currencyTo)
            {
                return 1;
            }
            else
            {
                WebClient myclient = new WebClient(); //System.Net

                string jsonDataWithRates = myclient.DownloadString("http://api.fixer.io/latest?base=" + currencyFrom);

                dynamic result = JsonConvert.DeserializeObject(jsonDataWithRates); //NuGet Package - Newtonsoft.json

                string rate = result.rates[currencyTo];

                double rateAsDouble = Convert.ToDouble(rate);

                return rateAsDouble;
            }
        }



        public void TransferFunds(string ibanFrom, string ibanTo, decimal amount)
        {
            //1 check for enough balance

            BankAccountsRepository bar = new BankAccountsRepository();

            if (bar.DoesAccountHaveEnoughFunds(ibanFrom, amount)) //1
            {
                bar.Withdraw(ibanFrom, amount); //2

                string currencyFrom = bar.GetBankAccount(ibanFrom).Currency_Fk; //3 getting the currencies

                string currencyTo = bar.GetBankAccount(ibanTo).Currency_Fk;

                double rate = GetCurrencyRate(currencyFrom, currencyTo); //4 getting the rate from a 3rd party

                decimal moneyToTransfer = Convert.ToDecimal(rate) * (amount);

                bar.Deposit(ibanTo, moneyToTransfer); //5 deposit
            }
            else
            {
                throw new NotEnoughBalanceException();
            }
        }

        public void UpdateAmounts()
        {
            decimal working1, working2, working3, ans;
            working1 = working2 = working3 = ans = 0;
            DateTime date = DateTime.Today;
            DateTime closeDate;

            foreach (BankAccount accounts in new BankAccountsBL().GetBankAccounts())
            {
                if(accounts.Duration != null)
                {
                    if(accounts.Duration == 1)
                    {
                        working1 = (Convert.ToDecimal(0.25 / 100)) * accounts.Balance;
                        working2 = working1 / 12;
                        working3 = working2 * (85 / 100);
                        ans = accounts.Balance + working3;
                        closeDate = accounts.DateOpened.AddMonths(accounts.Duration.Value);
                        //DateTime test = DateTime.Today.AddMonths(1);
                        //if(date == test)
                        if (closeDate == date)
                        {
                            new BankAccountsRepository().UpdateFixed(accounts.Iban, ans, accounts.Currency_Fk);
                        }
                    }
                    else if(accounts.Duration == 3)
                    {
                        working1 = (Convert.ToDecimal(1.65 / 100)) * accounts.Balance;
                        working2 = working1 / 12;
                        working3 = working2 * (85 / 100);
                        ans = accounts.Balance + working3;
                        closeDate = accounts.DateOpened.AddMonths(accounts.Duration.Value);
                        if (date == closeDate)
                        {
                            new BankAccountsRepository().UpdateFixed(accounts.Iban, ans, accounts.Currency_Fk);
                        }
                    }
                    else if (accounts.Duration == 6)
                    {
                        working1 = (Convert.ToDecimal(1.75 / 100)) * accounts.Balance;
                        working2 = working1 / 2;
                        working3 = working2 * (85 / 100);
                        ans = accounts.Balance + working3;
                        closeDate = accounts.DateOpened.AddMonths(accounts.Duration.Value);
                        if (date == closeDate)
                        {
                            new BankAccountsRepository().UpdateFixed(accounts.Iban, ans, accounts.Currency_Fk);
                        }
                    }
                    else if (accounts.Duration == 12)
                    {
                        working1 = (Convert.ToDecimal(2 / 100)) * accounts.Balance;
                        working2 = working1 / 1;
                        working3 = working2 * (85 / 100);
                        ans = accounts.Balance + working3;
                        closeDate = accounts.DateOpened.AddMonths(accounts.Duration.Value);
                        if (date == closeDate)
                        {
                            new BankAccountsRepository().UpdateFixed(accounts.Iban, ans, accounts.Currency_Fk);
                        }
                    }
                }
            }
        }
    }
}
