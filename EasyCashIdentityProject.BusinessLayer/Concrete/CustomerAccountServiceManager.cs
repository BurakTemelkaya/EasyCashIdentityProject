using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.Concrete
{
    public class CustomerAccountServiceManager : ICustomerAccountService
    {
        private readonly ICustomerAccountService _customerAccountService;

        public CustomerAccountServiceManager(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }

        public void TDelete(CustomerAccount t)
        {
            _customerAccountService.TDelete(t);
        }

        public List<CustomerAccount> TGetAll()
        {
            return _customerAccountService.TGetAll();
        }

        public CustomerAccount TGetById(int id)
        {
            return _customerAccountService.TGetById(id);
        }

        public void TInsert(CustomerAccount t)
        {
            _customerAccountService.TInsert(t);
        }

        public void TUpdate(CustomerAccount t)
        {
            _customerAccountService.TUpdate(t);
        }
    }
}
