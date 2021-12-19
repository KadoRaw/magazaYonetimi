public class Product
{
    public Product()
    {

    }
    public Product(string productName, string productCode, Category Category, User createdBy, DateTime dateTime, decimal productPrice) //const.
    {
        this.productName = productName;
        this.productCode = productCode;
        this.category = Category;
        this.createdBy = createdBy;
        this.createdAt = dateTime;
        this.productPrice = productPrice;
    }

    public string productName;
    public string productCode;
    public Category category;
    public User createdBy;
    public DateTime createdAt;
    public decimal productPrice;

    public override string ToString() => $"{productName}\\-----------\\{productCode}\\-----------\\{category}\\-----------\\{createdBy.userName}\\-----------\\{productPrice}";
}
