﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Services
{
    public interface IpaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
    }
}
