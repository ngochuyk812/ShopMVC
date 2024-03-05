﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopMVC.Database.Model;
using ShopMVC.Extentions;
using ShopMVC.Repositories.Interface;
using ShopMVC.Services.Interface;
using ShopMVC.Settings;
using ShopMVC.ViewModel;

namespace ShopMVC.Services
{
    public class ProductServices : IProductServices
    {
        public PaginationSetting paginationSetting { get; set; }
        public UnitOfWork unitOfWork { get; set; }

        public ProductServices(UnitOfWork unitOfWork, IOptions<PaginationSetting> options)
        {
            this.unitOfWork = unitOfWork;
            this.paginationSetting = options.Value;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var clock = await unitOfWork.Product.PagiAsync(1, 4, f => f.Category.Name.Contains("Đồng hồ"), f => f.Include(f => f.Images), o => o.OrderByDescending(f => f.CreatedDate));
            var jewelry = await unitOfWork.Product.PagiAsync(1, 4, f => f.Category.Name.Contains("Phụ kiện"), f => f.Include(f => f.Images), o => o.OrderByDescending(f => f.CreatedDate));
            var model = new HomeViewModel
            {
                NewClock = clock.Data,
                NewJewelry = jewelry.Data,
            };
            return model;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await unitOfWork.Product.FindAsync(f => f.Id == id, f => f.Include(f => f.Images));
            return product;
        }

        public async Task<Pagination<Product>> PageProduct(ProductViewModel viewModel)
        {
            var rs = await unitOfWork.Product.PagiAsync(viewModel.Page, paginationSetting.PageSize,
                f => f.Title.Contains(viewModel.Title)
                    && (viewModel.MaxPrice == -1 || f.Price <= viewModel.MaxPrice)
                    && f.Price >= viewModel.MinPrice
                    && (viewModel.Category == -1 || f.CategoryId == viewModel.Category),
                i => i.Include(f => f.Images));

            return rs;

        }
    }
}
