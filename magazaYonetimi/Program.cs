var userList = new List<User>();
var productList = new List<Product>();
var authorizedUser = new User();
bool IsLogedIn = false;
int selectedMenuItem = 10;




var u1 = new User("kadir", "kado@gmail.com", "1234", UserRole.admin);
var u2 = new User("osman", "osman@gmail.com", "4321", UserRole.employee);
var u3 = new User("şifam", "sifa@gmail.com", "kado", UserRole.superuser);

userList.Add(u1);
userList.Add(u2);
userList.Add(u3);



var p1 = new Product("Clover", "clover1923", Category.tshirt, u1, DateTime.Now, 99.99m);
var p2 = new Product("Red Devils Hoodie", "reddevilsHoodie", Category.hoodie, u1, DateTime.Now, 159.99m);
var p3 = new Product("Classics Sweatshirt", "classicSweatShirt", Category.sweat, u1, DateTime.Now, 139.99m);

productList.Add(p1);
productList.Add(p2);
productList.Add(p3);



while (true)
{
    switch (selectedMenuItem)
    {
        case 0:
            Menu(out selectedMenuItem);
            break;

        case 1:
            AddProduct(productList, in authorizedUser,out selectedMenuItem);
            break;
        case 2:
            GetProductList(productList, out selectedMenuItem);
            
            break;

        case 3:
            RemoveProduct(productList, out selectedMenuItem, in authorizedUser);
            break;
        case 4:
            break;
        case 5:
            break;
        case 10:
            Login(in userList, out authorizedUser, IsLogedIn, out selectedMenuItem);
            break;
        default:
            break;
    }
}



static void Menu(out int selectedMenuItem)
{
       
    Console.WriteLine("\n------------------------------\n\nYapmak istediğiniz işlemi menüden seçiniz. \n---------------------\n");
    Console.WriteLine("1. Ürün ekle (1)\n2.Ürün Listesi (2)\n3.Ürün sil (3)\n10. Farklı Kullanıcıya Geç (10)\n------------------------------\n");
    selectedMenuItem = Convert.ToInt32(Console.ReadLine());
    
}
static void Login(in List<User> userLogin, out User authorizedUser,bool IsLogedIn,out int selectedMenuItem)
{   selectedMenuItem = 0;
    while (true)
    {
        authorizedUser = new User();

        Console.WriteLine("E-mail giriniz: ");
        var email = Console.ReadLine();

        for (int i = 0; i < userLogin.Count; i++)
        {
            if (userLogin[i].email == email)
            {
                int loginPassCount = 4;
                do
                {
                    Console.WriteLine("Şifre giriniz: ");
                    var password = Console.ReadLine();

                    if (userLogin[i].password == password)
                    {
                        authorizedUser.email = userLogin[i].email;
                        authorizedUser.userName = userLogin[i].userName;
                        authorizedUser.password = userLogin[i].password;
                        Console.WriteLine("Giriş Başarılı \n\n\n");
                        IsLogedIn = true;

                        selectedMenuItem = 0;

                        break;
                    }
                    else
                    {
                        loginPassCount--;
                        if (loginPassCount == 0)
                        {
                            Console.WriteLine($"{email} hesabına ait şifreyi yenilemek için teknik desteğe başvurunuz.");
                        }
                        else
                        {
                            Console.WriteLine($"Kalan giriş hakkınız.{loginPassCount}");
                        }
                    }
                } while (loginPassCount > 0);
            }
        }
        if (IsLogedIn)
        {
            break;
        }
        else
        {
            Console.WriteLine("Bu maile ait hesap bulunamadı.Lütfen Tekrar deneyiniz.");
            Console.WriteLine("------------------------------------------------------");
        }
       
    }
}

static void AddProduct(List<Product> productList, in User authorizedUser,out int selectedMenuItem)
{
    selectedMenuItem = 0;


    Console.Write("Ürünün adı:\t");
    var urunAdı = Console.ReadLine();
    Console.Write("Ürünün kodu:\t");
    var urunKodu = Console.ReadLine();
    Console.WriteLine("-----------------\nÜrünün kategorisi:\t\nsweat\ntshirt\nsapka\nhoodie\n-----------------");
    var urunCategory = (Category)Enum.Parse(typeof(Category),Console.ReadLine());
    var urunCreator = authorizedUser;
    var urunOlusturmaTarihi = DateTime.Now;
    Console.WriteLine("Ürün fiyatı:\t");
    var urunFiyati = Convert.ToDecimal(Console.ReadLine());

    var urun = new Product(urunAdı, urunKodu, urunCategory, urunCreator, urunOlusturmaTarihi, urunFiyati);

    productList.Add(urun);
    Console.WriteLine($"{urunAdı} başarıyla eklenmiştir.");

    

}

static void GetProductList(in List<Product>productList1, out int selectedMenuItem)
{
    selectedMenuItem = 0;

    var a = "\n";
    
    Console.WriteLine("Product Name \t\t Product Code \t Category \t Created By\tCreated At \t Product Price");

    for (int i = 0; i < productList1.Count; i++)
    {

        Console.WriteLine(productList1[i]);
    }
    
    
}

static void RemoveProduct(List<Product> productList1, out int selectedMenuItem, in User authorizedUser)
{
    
    selectedMenuItem = 0;
    Console.WriteLine("---------------------------------------------------------------------------");
    for (int i = 0; i < productList1.Count; i++)
    {

        Console.WriteLine(productList1[i]);
    }
    Console.WriteLine("\n\n---------------------------------------------------------------------------\n\n");
    Console.Write("Silmek istediğiniz ürünün sırasını giriniz: ");
    var secilen = Convert.ToInt32(Console.ReadLine()) - 1;
    if (authorizedUser.userRole == productList1[secilen].createdBy.userRole)
    {
        Console.WriteLine($"Seçilen {productList1[secilen].productName} başarıyla listeden silindi.");
        productList1.RemoveAt(secilen);
    }
    else
    {
        
        Console.WriteLine($"Seçilen {productList1[secilen].productName} silinemedi.\n \n Admin yetkiniz bulunmamaktadır.");
    }

 

}