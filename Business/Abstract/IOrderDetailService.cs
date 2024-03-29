﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderDetailService
    {
        IDataResult<List<OrderDetailDto>> GetOrderDetails(); //sipariş detayı
        IResult Add(OrderDetail orderDetail); 
    }
}
