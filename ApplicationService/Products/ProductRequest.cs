namespace ApplicationService.Products
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public string ManufactureEmail { get; set; }
        public string ManufacturePhone { get; set; }
        public DateTime ProduceDate { get; set; }
    }

}
