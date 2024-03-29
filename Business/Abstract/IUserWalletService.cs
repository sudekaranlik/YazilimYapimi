﻿using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserWalletService
    {
        IResult CreateWallet(User user);
        IResult AddMoney(UserWallet userWallet);
        IDataResult<UserWallet> GetById(int userId);
        IResult Update(UserWallet userWallet);
        IResult UpdateList(List<UserWallet> userWallets);
    }
}
