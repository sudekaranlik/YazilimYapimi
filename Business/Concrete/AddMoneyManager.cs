﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidatonRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AddMoneyManager : IAddMoneyService
    {
        IAddMoneyDal _addMoneyDal;
        IUserWalletService _userWalletService;

        public AddMoneyManager(IAddMoneyDal addMoneyDal, IUserWalletService userWalletService)
        {
            _addMoneyDal = addMoneyDal;
            _userWalletService = userWalletService;
        }
        
        [SecuredOperation("user")]
        [ValidationAspect(typeof(AddMoneyValidator))]
        [CacheRemoveAspect("IAddMoneyService.Get")]
        public IResult Add(AddMoney money)
        {
            _addMoneyDal.Add(money);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAddMoneyService.Get")]
        public IResult Approve(int addMoneyId)
        {
            var result = _addMoneyDal.Get(a => a.Id == addMoneyId);
            if (result.Confirmation)
            {
                return new ErrorResult();
            }
            result.Confirmation = true;
            _userWalletService.AddMoney(new UserWallet { UserId = result.UserId, Money = result.Money });
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheAspect]
        public IDataResult<List<AddMoneyDetailDto>> GetApproved()
        {
            return new SuccessDataResult<List<AddMoneyDetailDto>>
                (_addMoneyDal.GetAddMoneyDetails(addmoney => addmoney.Confirmation == false));
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAddMoneyService.Get")]
        public IResult Refusal(int addMoneyId)
        {
            _addMoneyDal.Delete(new AddMoney { Id = addMoneyId });
            return new SuccessResult("para ekleme işlemi reddedildi");
        }
    }
}
