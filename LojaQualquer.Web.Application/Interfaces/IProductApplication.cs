﻿using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.Application.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaQualquer.Web.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<IList<ProductResponse>> GetByFilterAsync(FilterProductRequest filter);
    }
}