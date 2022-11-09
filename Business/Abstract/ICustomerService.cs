using System;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        List<Customer> GetAll();

        Customer GetByCategoryId(string categoryId);
    }
}

