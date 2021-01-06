﻿using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Business.Banking
{
    public class Branch
    {
        public ResponseBase AddNewBranch(BranchRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@BranchName",request.DataContract.BranchName),
                new SqlParameter("@Adress",request.DataContract.Adress),
                new SqlParameter("@CityId",request.DataContract.CityId),
                new SqlParameter("@DateOfLaunch",request.DataContract.DateOfLaunch),
                new SqlParameter("@MailAdress",request.DataContract.MailAdress),
                new SqlParameter("@PhoneNumber",request.DataContract.PhoneNumber),                
            };

            try
            {
                var dbResponse =Convert.ToInt32(dbOperation.spExecuteScalar("COR.ins_AddNewBranch", parameters));
                return new ResponseBase() { DataContract = dbResponse, IsSuccess = true };
            }
            catch (Exception)
            {
                return new ResponseBase() { ErrorMessage = "AddNewBranch fonksiyonu başarısız", IsSuccess = false };
                //throw;
            }
        }

        public ResponseBase DeleteBranchById(BranchRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BranchId",request.DataContract.Id),
            };
            try
            {
                var dbResponse = dbOperation.spExecuteScalar("COR.del_BranchDeleteById", parameters);
                return new ResponseBase() { IsSuccess = true };
            }
            catch (Exception)
            {
                return new ResponseBase() { ErrorMessage = "DeleteBranchById işlemi başarısız oldu!", IsSuccess = false };
                //throw;
            }

        }
        public ResponseBase UpdateBranchDetailsById(BranchRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("Id",request.DataContract.Id),
                new SqlParameter("@BranchName",request.DataContract.BranchName),
                new SqlParameter("@Adress",request.DataContract.Adress),
                new SqlParameter("CityId",request.DataContract.CityId),
                new SqlParameter("MailAdress",request.DataContract.MailAdress),
                new SqlParameter("PhoneNumber",request.DataContract.PhoneNumber)
            };
            try
            {
                var dbResponse = dbOperation.spExecuteScalar("COR.upd_UpdateBranchDetailsById", parameters);
                return new ResponseBase() { IsSuccess = true, DataContract = request.DataContract };
            }
            catch (Exception)
            {

                return new ResponseBase() { ErrorMessage = "UpdateBranchDetailsById fonksiyonu başarısız", IsSuccess = false };
            }
        }

        public ResponseBase GetAllBranches(BranchRequest request)
        {
            DbOperation dbOperation = new DbOperation();
            List<BranchContract> branchContracts = new List<BranchContract>();
            try
            {
                SqlDataReader reader = dbOperation.GetData("COR.sel_GetAllBranches");
                while (reader.Read())
                {
                    branchContracts.Add(new BranchContract()
                    {
                        Id = (int)reader["Id"],
                        CityId = (int)reader["CityId"],
                        Adress = (string)reader["Adress"],
                        MailAdress = (string)reader["MailAdress"],
                        BranchName = (string)reader["BranchName"],
                        DateOfLaunch = (DateTime)reader["DateOfLaunch"],
                        PhoneNumber = (string)reader["PhoneNumber"]

                    });
                }
            }
            catch (Exception)
            {

                return new ResponseBase() { ErrorMessage = "GetAllBranches metodu başarısız.", IsSuccess = false };
            }

            if (branchContracts.Count > 0)
            {
                return new ResponseBase() { DataContract = branchContracts,IsSuccess = true };
            }
            else
            {
                return new ResponseBase() { IsSuccess = false, ErrorMessage = "Hiç şube getirilemedi." };
            }
        }

    }
}