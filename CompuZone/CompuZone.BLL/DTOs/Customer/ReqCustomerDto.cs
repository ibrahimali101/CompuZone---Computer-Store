using CompuZone.BLL.DTOs.Order;

namespace CompuZone.BLL.DTOs.Customer
{
    public class ReqCustomerDto
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
