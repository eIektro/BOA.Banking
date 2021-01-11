﻿using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class Account
    {
        public ResponseBase GetAllAccounts(AccountRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            List<AccountContract> accountContracts = new List<AccountContract>();
            SqlDataReader reader = dbOperation.GetData("CUS.sel_GetAllAccounts");
            try
            {
                while (reader.Read())
                {
                    accountContracts.Add(new AccountContract()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        BranchId = (int)reader["BranchId"],
                        CustomerId = (int)reader["CustomerId"],
                        AdditionNo = (int)reader["AdditionNo"],
                        CurrencyId = (int)reader["CurrencyId"],
                        Balance = (decimal)reader["Balance"],
                        DateOfFormation = (DateTime)reader["DateOfFormation"],
                        IBAN = reader["IBAN"].ToString(),
                        IsActive = (bool)reader["IsActive"],
                        /*DateOfDeactivation = (DateTime)reader["DateOfDeactivation"]*/
                        FormedUserId = (int)reader["FormedUserId"],
                        BranchName = reader["BranchName"].ToString(),
                        CurrencyCode = reader["code"].ToString(),
                        FormedUserName = reader["UserName"].ToString()
                        /*DateOfLastTrasaction = (DateTime)reader["DateOfLastTransaction"]*/
                    });
                }

                return new ResponseBase() { DataContract = accountContracts, IsSuccess = true };
            }
            catch (Exception e)
            {
                return new ResponseBase { IsSuccess = false, ErrorMessage = "GetAllAccounts operasyonu başarısız" };
            }

                
        }

        public ResponseBase FilterAccountsByProperties(AccountRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            List<AccountContract> accountContracts = new List<AccountContract>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", request.DataContract.Id),
                new SqlParameter("@BranchId",request.DataContract.BranchId),
                new SqlParameter("@CustomerId",request.DataContract.CustomerId),
                new SqlParameter("@AdditionNo",request.DataContract.AdditionNo),
                new SqlParameter("@CurrencyId",request.DataContract.CurrencyId),
                new SqlParameter("@Balance",request.DataContract.Balance),
                new SqlParameter("@IBAN",request.DataContract.IBAN),
                new SqlParameter("@IsActive",request.DataContract.IsActive),
                new SqlParameter("@FormedUserId",request.DataContract.FormedUserId),
                new SqlParameter("@DateOfFormation",request.DataContract.DateOfFormation),
                //new SqlParameter("@DateOfDeactivation",request.DataContract.DateOfDeactivation),
                //new SqlParameter("@DateOfLastTransaction",request.DataContract.DateOfLastTrasaction)
            };
            
            try
            {
                List<AccountContract> accounts = new List<AccountContract>();
                SqlDataReader reader = dbOperation.GetData("CUS.sel_FilterAccountsByProperties", parameters);
                while (reader.Read())
                {
                    accounts.Add(new AccountContract() {

                        Id = Convert.ToInt32(reader["Id"]),
                        BranchId = (int)reader["BranchId"],
                        CustomerId = (int)reader["CustomerId"],
                        AdditionNo = (int)reader["AdditionNo"],
                        CurrencyId = (int)reader["CurrencyId"],
                        Balance = (decimal)reader["Balance"],
                        DateOfFormation = (DateTime)reader["DateOfFormation"],
                        IBAN = reader["IBAN"].ToString(),
                        IsActive = (bool)reader["IsActive"],
                        //DateOfDeactivation = (DateTime)reader["DateOfDeactivation"],
                        FormedUserId = (int)reader["FormedUserId"],
                        BranchName = reader["BranchName"].ToString(),
                        CurrencyCode = reader["code"].ToString(),
                        FormedUserName = reader["UserName"].ToString()
                        //DateOfLastTrasaction = (DateTime)reader["DateOfLastTransaction"]

                    });
                }
                return new ResponseBase() { DataContract = accounts, IsSuccess = true };
            }
            catch (Exception e)
            {
                return new ResponseBase() { ErrorMessage = "FilterAccountsByProperties operasyonu başarısız.", IsSuccess = false };
                throw;
            }
        }


        public ResponseBase AddNewAccount(AccountRequest request)
        {
            request.DataContract.DateOfFormation = DateTime.Now;
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] sqlParameters = new SqlParameter[] { 
                new SqlParameter("@BranchId",request.DataContract.BranchId),
                new SqlParameter("@CustomerId",request.DataContract.CustomerId),
                new SqlParameter("@AdditionNo",request.DataContract.AdditionNo),
                new SqlParameter("@CurrencyId",request.DataContract.CurrencyId),
                new SqlParameter("@Balance",request.DataContract.Balance),
                new SqlParameter("@IBAN",request.DataContract.IBAN),
                new SqlParameter("@IsActive",request.DataContract.IsActive),
                new SqlParameter("@FormedUserId",request.DataContract.FormedUserId),
                new SqlParameter("@DateOfFormation",request.DataContract.DateOfFormation)
            
            };

            try
            {
                var response = dbOperation.spExecuteScalar("CUS.ins_AddNewAccount", sqlParameters);
                return new ResponseBase() { IsSuccess = true };
            }
            catch (Exception)
            {
                return new ResponseBase() { IsSuccess = false, ErrorMessage = "AddNewAccount operasyonu başarısız oldu." };
                throw;
            }



        }

        public ResponseBase UpdateAccountDetailsById(AccountRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("@Id",request.DataContract.Id),
                new SqlParameter("@CustomerId",request.DataContract.CustomerId),
                new SqlParameter("@AdditionNo",request.DataContract.AdditionNo),
                new SqlParameter("@CurrencyId", request.DataContract.CurrencyId),
                new SqlParameter("@Balance",request.DataContract.Balance),
                new SqlParameter("@BranchId",request.DataContract.BranchId),
                new SqlParameter("@IBAN",request.DataContract.IBAN),
                new SqlParameter("@IsActive",request.DataContract.IsActive),
                new SqlParameter("@DateOfDeactivation",request.DataContract.DateOfDeactivation) //nullable in db side    
            };

            try
            {
                var response = dbOperation.spExecuteScalar("CUS.upd_UpdateAccountDetailsById", sqlParameters);
                return new ResponseBase() { IsSuccess = true };
            }
            catch (Exception e)
            {
                return new ResponseBase() { IsSuccess = false, ErrorMessage = "UpdateAccountDetailsById operasyonu başarısız." };
                throw;
            }
        }
    }
}
