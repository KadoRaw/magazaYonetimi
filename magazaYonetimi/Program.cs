var userList = new List<User>();
var productList = new List<Product>();
var authorizedUser = new User();
bool isLogedIn = false;
int selectedMenuItem = 0;




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
            Menu(out selectedMenuItem, in isLogedIn);
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
            ProductUpdate(productList, in authorizedUser, out selectedMenuItem);
            break;
        case 5:
            ProductAndCategoryReport(in productList, out selectedMenuItem);
            break;
        case 6:
      
            break;
        case 7:
      
            break;
        case 8:
            LogOut(out selectedMenuItem, authorizedUser, out isLogedIn);
            break ;
        case 9:
            Register(userList, out selectedMenuItem, authorizedUser, out isLogedIn);
            return;
        case 10:
            Login(in userList, out authorizedUser, out isLogedIn, out selectedMenuItem);
            break;
        default:
            break;
    }
}



static void Menu(out int selectedMenuItem, in bool ısLogedIn)
{
    if( ısLogedIn == false ) //// DÜZENLEEEEEEEEEEEEE
    {
        Console.WriteLine("\n------------------------------\n\nYapmak istediğiniz işlemi menüden seçiniz. \n---------------------\n");
        Console.WriteLine("9.Kayıt ol(9)\n10. Giriş Yap (10)\n");
        selectedMenuItem = Convert.ToInt32(Console.ReadLine());
        

    }
    else
    {
        Console.WriteLine("\n------------------------------\n\nYapmak istediğiniz işlemi menüden seçiniz. \n---------------------\n");
        Console.WriteLine("1. Ürün ekle :(1)\n2.Ürün Listesi :(2)\n3.Ürün sil :(3)\n4.Ürün Güncelle: (4)\n5.Kategori Bilgilerini al :(5)\n8.Çıkış Yap : (8)\n10. Farklı Kullanıcıya Geç (10)\n------------------------------\n");
        selectedMenuItem = Convert.ToInt32(Console.ReadLine());
    }
    
    
}
static void Login(in List<User> userLogin, out User authorizedUser,out bool IsLogedIn,out int selectedMenuItem)
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
                        authorizedUser = userLogin[i];
                        Console.WriteLine("Giriş Başarılı \n\n\n");
                        IsLogedIn = true;
                        var a = true;

                        selectedMenuItem = 0;

                        return;
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
        
            Console.WriteLine("Bu maile ait hesap bulunamadı.Lütfen Tekrar deneyiniz.");
            Console.WriteLine("------------------------------------------------------");
        
       
    }
}

static void AddProduct(List<Product> productList, in User authorizedUser,out int selectedMenuItem)
{
    selectedMenuItem = 0;


    Console.Write("Ürünün adı:\t");
    var urunAdı = Console.ReadLine();
    Console.Write("Ürünün kodu:\t");
    var urunKodu = Console.ReadLine();
    foreach (var item in productList)
    {
        if (urunAdı == item.productName || urunKodu == item.productCode)
        {
            Console.WriteLine("Aynı kodda ya da isimde ürün bulunmaktadır.");
            return;
        } 
    }

    Console.WriteLine("-----------------\nÜrünün kategorisi:\t\nsweat\ntshirt\nsapka\nhoodie\n-----------------");
    var urunCategory = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
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
static void ProductUpdate(List<Product> products, in User authorizedUser, out int selectedMenuItem)
{
    Console.Clear();
    if (authorizedUser.userRole == UserRole.admin || authorizedUser.userRole == UserRole.superuser)
    {
        GetProductList(in products, out selectedMenuItem);
        Console.WriteLine("==========================================================");
        Console.WriteLine("Güncellemek istediğiniz ürünü seçiniz.(örn: 1)");
        var secim1 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Değiştirmek istediğiniz özelliği seşiniz:\n1.Product Name:\n2.Product Code:\n3.Product Categort:\n4.Product Price:");
        var secim2 = Convert.ToInt32(Console.ReadLine());
        selectedMenuItem = 0;
        switch (secim2)
        {
            case 1:
                Console.WriteLine("Ürün Adını Giriniz:");
                var urunAdi = Console.ReadLine();
                foreach (var item in products)
                {
                    if (item.productName == urunAdi)
                    {
                        Console.WriteLine("Aynı isimde ürün bulunmaktadır tekrar deneyiniz.");
                        secim2 = 1;
                        return;
                    }
                }
                products[secim1 - 1].productName = urunAdi;
                Console.WriteLine("Ürün adı başarıyla değiştirildi.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"{products[secim1 - 1]}");
                break;
            case 2:
                Console.WriteLine("Ürün kodu giriniz: ");
                var urunKodu = Console.ReadLine();
                foreach (var item in products)
                {
                    if (item.productCode == urunKodu)
                    {
                        Console.WriteLine("Aynı isimde ürün bulunmaktadır tekrar deneyiniz.");
                        secim2 = 1;
                        return;
                    }
                }
                products[secim1 - 1].productCode = urunKodu;
                Console.WriteLine("Ürün kodu başarıyla değiştirildi.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"{products[secim1 - 1]}");
                break;
            case 3:
                Console.WriteLine("Ürün kategorisi giriniz: ");
                var urunCategory = (Category)Enum.Parse(typeof(Category), Console.ReadLine());

                products[secim1 - 1].category = urunCategory;
                Console.WriteLine("Ürün kategorisi başarıyla değiştirildi.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"{products[secim1 - 1]}");
                break;
            case 4:
                Console.WriteLine("Ürün fiyatı giriniz: ");

                var urunFiyati = Convert.ToDecimal(Console.ReadLine());

                products[secim1 - 1].productPrice = urunFiyati;
                Console.WriteLine("Ürün fiyatı başarıyla değiştirildi.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"{products[secim1 - 1]}");
                break;

            default:
                break;
        } 
    }
    else
    {
        Console.WriteLine("Kullanıcı rolünüz yeterli değildir. Sadece superuser ve admin değiştirebilir.");
        selectedMenuItem = 0;
    }
}
static void ProductAndCategoryReport(in List<Product> productList1, out int selectedMenuItem)
{
    selectedMenuItem = 0;
    int hoodie = 0;
    int sweat = 0;
    int sapka = 0;
    int tshirt = 0;
    int unCatorized = 0;
    foreach (Product product in productList1)
    {

        if (product.category == Category.hoodie)
        {
            hoodie++;

        }
        else if (product.category == Category.sweat)
        {
            sweat++;
        }
        else if (product.category == Category.sapka)
        {
            sapka++;
        }
        else if (product.category == Category.tshirt)
        {
            tshirt++;
        }
        else
        {
            unCatorized++;
        }


    }

    Console.WriteLine($"Hoodie Sayısı: {hoodie}\nSweat Sayısı: {sweat}\nSapka Sayısı:{sapka}\nTshirt Sayısı:{tshirt}\nKategorize edilmemiş: {unCatorized}");

}
static void Register(List<User> userList, out int selectedMenuItem, User authorizedUser ,out bool isLogedIn)
{
    selectedMenuItem = 0;
    isLogedIn = true;
    Console.WriteLine("Üye olmak için size sorulan bilgileri giriniz.");
    Console.WriteLine("Username giriniz");
    var username = Console.ReadLine();
    Console.WriteLine("E-mail giriniz:");
    var email = Console.ReadLine();
    Console.WriteLine("Şifre oluşturunuz: ");
    var password = Console.ReadLine();
    Console.WriteLine("User  role giriniz: admin / superuser / employee");
    var userrole = (UserRole)Enum.Parse(typeof(UserRole), Console.ReadLine());

    var user = new User(username, email, password, userrole);
    userList.Add(user);
    authorizedUser = user;
    Console.WriteLine("Hesap başarıyla oluşturuldu. Giriş Yapıldı.");
    

}
static void LogOut(out int selectedMenuItem,User authorizedUser,out bool isLogedIn)
{   isLogedIn = false;
    selectedMenuItem = 0;
    authorizedUser = null;
    Console.WriteLine("Çıkış başarılı. ");
}