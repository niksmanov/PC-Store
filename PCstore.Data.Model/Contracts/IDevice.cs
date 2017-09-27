namespace PCstore.Data.Model.Contracts
{
    public interface IDevice
    {
        decimal Price { get; set; }
        string SellerPhone { get; set; }
        string SellerEmail { get; set; }
        string Description { get; set; }
    }
}
