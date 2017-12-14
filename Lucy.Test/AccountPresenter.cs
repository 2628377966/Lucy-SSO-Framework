using Lucy.Services.Dtos;
using Lucy.Services.Interfaces;
using Lucy.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Test
{
  public  class AccountPresenter
    {
        private IAccountSvc AccountSvc;
        public AccountPresenter(IAccountSvc _AccountSvc)
        {
            AccountSvc = _AccountSvc;
        }
        public ResultEx UpdatePassword(AccountModel.ModifyPwd model)
        {
            return AccountSvc.UpdatePassword(model);
        }
    }
}
