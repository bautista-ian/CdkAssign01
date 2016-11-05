using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using CdkAssign01.Models.DTO;

namespace CdkAssign01.DAL
{
    public class CustomersEngine : DataHelper, ICustomersEngine
    {
        public CustomersEngine()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["CDKConnection"].ConnectionString;
        }

        public DataSet GetCustomerById(int customerId)
        {
            SqlParameter customerIdParameter = new SqlParameter("@CustomerId", customerId);
            SqlParameter returnParameter = new SqlParameter("@ReturnVal", customerId);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            SqlParameterCollection outParameters;
            DataSet ds = GetDataSet("GetCustomerById", CommandType.StoredProcedure, out outParameters, customerIdParameter, returnParameter);

            var result = outParameters[returnParameter.ParameterName].Value;

            return ds;
        }
    }

}