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

        [HttpGet("create")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create(ProductCreateUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            var response = await _productApplication.PostAsync(_mapper.Map<ProductCreateUpdateRequest>(model));

            if (response.ResponseError != null)
            {
                ModelState.AddModelError(string.Empty, response.ResponseError.Message);

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{productId}")]
        [Authorize]
        public async Task<IActionResult> Edit(int productId)
        {
            var response = await _productApplication.GetByIdAsync(productId);

            if (response.ResponseError != null)
            {
                ModelState.AddModelError(string.Empty, response.ResponseError.Message);

                return View();
            }

            return View(_mapper.Map<ProductCreateUpdateViewModel>(response));
        }

        [HttpPost("edit/{productId}")]
        [Authorize]
        public async Task<IActionResult> Edit(int productId, ProductCreateUpdateViewModel model)
        {
            var response = await _productApplication.PutAsync(productId, _mapper.Map<ProductCreateUpdateRequest>(model));

            if (response.ResponseError != null)
            {
                ModelState.AddModelError(string.Empty, response.ResponseError.Message);

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{productId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int productId)
        {
            var response = await _productApplication.GetByIdAsync(productId);

            if (response.ResponseError != null)
            {
                ModelState.AddModelError(string.Empty, response.ResponseError.Message);

                return View();
            }

            return View(_mapper.Map<ProductCreateUpdateViewModel>(response));
        }

        [HttpGet("confirmdelete/{productId}")]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int productId)
        {
            var response = await _productApplication.DeleteAsync(productId);

            if (response.ResponseError != null)
            {
                ModelState.AddModelError(string.Empty, response.ResponseError.Message);

                return View("Delete");
            }

            return RedirectToAction("Index");
        }
    }
}
