using System;
using System.Collections.Generic;
using System.Linq;

namespace Bad.Code.BadSmells._02DuplicatedCode
{
    public class CustomerService
    {
        private readonly IList<Customer> _customers;

        public CustomerService(IList<Customer> customers)
            => _customers = customers;

        public Customer GetCustomer(int id)
        {
            return _customers.Where(c => c.Id == id
                                         && c.TenantId == UserContext.TenantId).FirstOrDefault();
        }

        public Customer GetCustomerByNationalCode(string nationalCode)
        {
            return _customers.Where(x =>
                     x.NationalCode == nationalCode
                    && x.TenantId == UserContext.TenantId).FirstOrDefault();
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers.Where(a => a.TenantId == UserContext.TenantId).ToList();
        }
        

    }

    public class UserContext
    {
        public static Guid TenantId { get; set; }
    }

    public class Customer
    {
        public Guid TenantId { get; set; }
        public int Id { get; set; }
        public string NationalCode { get; set; }
    }
}
