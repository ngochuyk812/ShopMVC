using Microsoft.AspNetCore.Mvc.Formatters;
using ShopMVC.Database.Model;

namespace ShopMVC.DTO
{
    [Serializable]
    public class Media
    {
        public enum TypeEnum
        {
            IMAGE, VIDEO
        }
        public string Src { get; set; }
        public TypeEnum Type { get; set; }
    }
}
