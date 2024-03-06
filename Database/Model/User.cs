namespace ShopMVC.Database.Model
{
    public class User:BaseModel
    {
        public string Name { get; set; }
        public string? Avatar { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsVery { get; set; } = 0;
        public virtual IEnumerable<Address> Address { get; set; }
        public virtual IEnumerable<UserRole> Roles { get; set; }


    }
}
