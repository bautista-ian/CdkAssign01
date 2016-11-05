using CdkAssign01.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdkAssign01.BAL
{
    public interface ICustomersRepository
    {
        CustomerDTO GetCustomerById(int customerId);
    }
}
