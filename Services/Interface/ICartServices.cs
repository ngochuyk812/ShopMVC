﻿using ShopMVC.Database.Model;
using ShopMVC.DTO;
using ShopMVC.Extentions;
using ShopMVC.ViewModel;

namespace ShopMVC.Services.Interface
{
        public interface ICartServices
        {
        Task<CartDTO> CreateCart(int idUser, int idProduct, CartViewModel model);
        Task<IEnumerable<CartDTO>> GetCartByUser(int id);
        }

}