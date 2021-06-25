using AutoMapper;
using LojaQualquer.Web.Application.Interfaces;
using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaQualquer.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly IMapper _mapper;

        public ProductController(IProductApplication productApplication, IMapper mapper)
        {
            _productApplication = productApplication;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index(ProductViewModel request)
        {
            var products = await _productApplication.GetByFilterAsync(_mapper.Map<FilterProductRequest>(request.Filter));

            if (products.Any(x => x.StatusCode.HasValue))
            {
                var error = products.First();

                if (error.StatusCode == 401) return RedirectToAction("Logout", "Login");

                ModelState.AddModelError(string.Empty, error.ResponseError.Message);

                return View("Index");
            }

            return View(new ProductViewModel
            {
                Content = _mapper.Map<IList<ProductViewModel.ProductContent>>(products)
            });
        }
    }
}
