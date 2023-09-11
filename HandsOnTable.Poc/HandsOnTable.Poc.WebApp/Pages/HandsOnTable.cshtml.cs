using Bogus;
using HandsOnTable.Poc.WebApp.Controllers;
using HandsOnTable.Poc.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HandsOnTable.Poc.WebApp.Pages
{
    public class HandsOnTableModel : PageModel
    {
        private readonly IManufacturersController _manufacturersController;
        private readonly IProductsController _productsController;
        private readonly ILogger<HandsOnTableModel> _logger;

        public HandsOnTableModel(IManufacturersController manufacturersController, IProductsController productsController, ILogger<HandsOnTableModel> logger)
        {
            _manufacturersController = manufacturersController;
            _productsController = productsController;
            _logger = logger;
        }

        public List<Product> Products { get; } = new List<Product>();
        public List<Manufacturer> Manufacturers { get; } = new List<Manufacturer>();

        public void OnGet()
        {
            Manufacturers.AddRange(_manufacturersController.GetAll());
            Products.AddRange(_productsController.GetAll());
        }

        public void OnPost()
        {
            var test = string.Empty;
            //var foo = argument.First();
        }
    }
}
