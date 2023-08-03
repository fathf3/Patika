


Vote();

void Vote()
{
    User user = new User();
    Console.Write("Lütfen Kullanıcı Adınızı Giriniz: ");
    tryVote:
    Console.WriteLine("Oy Kullanmak icin Secim Yapınız");


    foreach (Category categoryItem in InMemoryDB.categories)
    {

        Console.WriteLine(categoryItem.Name + " icin " + categoryItem.Id);

    }
    Console.Write("Secim : ");
    int secim = int.Parse(Console.ReadLine());
    Console.Write("Kullanici Adi : ");
    string userName = Console.ReadLine();
    if (InMemoryDB.users.Any(u => u.UserName == userName))
    {
        user = InMemoryDB.users.First(u => u.UserName == userName);
        if(user.isVoted == false)
        {
            InMemoryDB.categories.Any(i => i.Id == secim);
            var category = InMemoryDB.categories.FirstOrDefault(c => c.Id == secim);
            category.VoteCount++;
            Console.WriteLine("Oylama Başarılı");
        }
        else
        {
            Console.WriteLine("Bu kullanıcı daha önce oy kullanmıs");
        }
    }
    else
    {
       
        Console.WriteLine("Boyle bir kullanici bulunamadi");
        Console.WriteLine("Lutfen kayit olun");
        tryNewUserName:
        Console.Write("Kullanici Adı : ");
        string newUserName = Console.ReadLine();
        if(InMemoryDB.users.Any(i => i.UserName == newUserName))
        {
            Console.WriteLine("Kullanıcı adı daha once alinmis, tekrar deneyiniz");
            goto tryNewUserName;
        }
        else
        {
            user.UserName = newUserName;
            InMemoryDB.users.Add(user);
            Console.WriteLine("Kayıt Yapılmıştır.");
            goto tryVote;
        }
    }
    Console.WriteLine("1- Devam Et / 2-Yeni Oy Kullan");
    Console.Write("Secim : ");
    switch (int.Parse(Console.ReadLine()))
    {
        case 1: break;
        case 2: goto tryVote;
    }
    Result();
}

void Result()
{
    Console.WriteLine("Oylama Sonuçları");
    int toplamOy = InMemoryDB.categories.Sum(c => c.VoteCount);
    Console.WriteLine("Toplam Oy : " + toplamOy);
    foreach (Category categoryItem in InMemoryDB.categories)
    {
        
        double yuzde = (100 *(categoryItem.VoteCount / toplamOy));
        Console.WriteLine(categoryItem.Name + " oy oranı : %" +yuzde);

    }
}


class User
{

    public string UserName { get; set; }
    public bool isVoted { get; set; } = false;
   
}

class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int VoteCount { get; set; }


}

class InMemoryDB
{
    private static List<Category> _categories;
    private static List<User> _users;
    static InMemoryDB()
    {
        _categories = new()
        {
            new Category{Id=1,Name="Spor",VoteCount=0},
            new Category{Id=2,Name="Komedi",VoteCount=0},
            new Category{Id=3,Name="Aksiyon",VoteCount=0},
          
        };

        _users = new()
        {
            new User{UserName="f"},
            new User{UserName="i"},
            new User{UserName="d"},
            new User{UserName="a"},
            new User{UserName="b"},
            new User{UserName="ahmet",isVoted=true},
            
        };
    }
    public static List<Category> categories => _categories;
    public static List<User> users => _users;
}














































