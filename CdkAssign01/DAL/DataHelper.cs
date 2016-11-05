using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CdkAssign01.DAL
{
    public abstract class DataHelper
    {
        protected virtual string ConnectionString { private get; set; }

        protected virtual DataSet GetDataSet(string spName, CommandType cmdType, out SqlParameterCollection outDirectionParameters, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            var conn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand(spName, conn);

            cmd.Parameters.AddRange(parameters);

            var da = new SqlDataAdapter(cmd);
            cmd.CommandType = cmdType;
            da.Fill(ds);

            //filter in-direction parameters
            if (cmd.Parameters.Count > 0)
            {
                var inDirectionParameters = new List<SqlParameter>();

                for (int i = 0; i < cmd.Parameters.Count; i++)
                    if (cmd.Parameters[i].Direction == ParameterDirection.Input)
                        inDirectionParameters.Add(cmd.Parameters[i]);

                foreach (var inParam in inDirectionParameters)
                    cmd.Parameters.Remove(inParam);
            }

            //return out-direction parameters
            outDirectionParameters = cmd.Parameters;

            return ds;
        }
    }
}