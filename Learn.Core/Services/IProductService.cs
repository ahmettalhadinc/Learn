﻿using Learn.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.Services
{
    public interface IProductService:IService<Product>
    {
        Task BuyProduct(Product product);
    }
}
