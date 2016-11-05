using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdkAssign01.DAL
{
    public interface IOrdersEngine
    {
        DataSet GetOrdersForCustomer(int customerId);
    }
}
