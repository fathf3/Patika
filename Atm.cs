/*
 
 * ATM uygulaması Uygulama ilk çalıştığında kullanıcıdan yapmak istediği işlemi öğrenmelidir. 
Bunlar ATM üzerinden yapılabilecek temel işlemledir. 
Para çekme, para yatırma, ödeme yapma. 
İsteğe bağlı olarak genişletilebilir. 
    Öncelikle ATM ye giriş yapan kullanıcın sistemde kayıtlı bir kullanıcı olduğundan emin olunmalıdır.

Uygulamada bir de gün sonu seçeneği olmalıdır. Gün sonu alınmak istendiğinde, gün içerisinde yapılan transaction ların logları ve fraud olabilecek yani hatalı giriş denemeleri log gösterilmelidir. Aynı client'ın bilgisayarında belirlenen bir lokasyona EOD_Tarih(DDMMYYY formatında).txt adında bir dosyaya yazılmalıdır.

Kullanılması gereken teknikler:

Dosyaya Yazma
Dosyadan Okuma
İşlem listesi pre-defined olarak kullanılabilir.
 
 * */


using System;
using System.IO;
Log log = new Log();
AtmApp();

void AtmApp()
{

    log.checkAndCreate();

giris:
    Console.WriteLine("Lütfen Kullanici adi sifre giriniz");
    Console.Write("Kullanıcı Adı : ");
    string userName = Console.ReadLine();
    Console.Write("Kullanıcı Sifre : ");
    string password = Console.ReadLine();
    User user = InMemoryDB.users.FirstOrDefault(x => x.UserName == userName);
    if (userNameAndPassword(userName, password))
    {
        log.succesFullLogin(user.UserName, user.Id);
        Console.WriteLine("Sayin " + userName + " Hoş Geldiniz");
    secimEkrani:
        Console.WriteLine("1-Para Çekme\n" + "2-Para Yatırma\n" + "3-Para gönderme\n" + "4-Bakiye görüntüle\n" + "0-Çıkış yap" + "");
        Console.Write("Secim : ");
        int secim = int.Parse(Console.ReadLine());
        switch (secim)
        {
            case 1: ParaCek(user); break;
            case 2: ParaYatir(user); break;
            case 3: ParaGonder(user); break;
            case 4: Bakiye(user); break;
            default: Console.WriteLine("Yanlis secim"); goto secimEkrani;
        }
        Console.WriteLine("1-Yeni Giris Yap\n" + "2-Ana Menu\n" + "0-Cikis Yap\n");
        Console.Write("Secim : ");
        int secim2 = int.Parse(Console.ReadLine());
        switch (secim2)
        {
            case 0: break;
            case 1: goto giris;
            case 2: goto secimEkrani;
        }
    }
    else
    {

        Console.WriteLine("Başarısız giris");

        log.failedLogin(user.UserName, user.Id);
        goto giris;
    }
}

bool userNameAndPassword(string userName, string password)
{
    if (InMemoryDB.users.Any(u => u.UserName == userName))
    {
        User user = InMemoryDB.users.FirstOrDefault(x => x.UserName == userName);
        if (user.Password == password)
            return true;
        else return false;
    }
    else return false;
}
void ParaCek(User user)
{
miktarGiris:
    Console.WriteLine("Bakiye : " + user.Balance);
    Console.WriteLine("Cekmek istediginiz miktarı giriniz ");
    Console.Write("Miktar : ");
    int para = int.Parse(Console.ReadLine());
    if (user.Balance < para)
    {
        Console.WriteLine("Bakiyenizden fazla miktar girdiniz");
        log.failedWithdraw(user.UserName, user.Id, para);
        goto miktarGiris;

    }
    else
    {
        user.Balance = user.Balance - para;
        Console.WriteLine("Para Cekme İslemi Gerceklesmistir");
        Console.WriteLine("Kalan Bakiye : " + user.Balance);
        log.succesfullWithdraw(user.UserName, user.Id, para);


    }
}
void ParaYatir(User user)
{
miktarGiris:
    Console.WriteLine("Bakiye : " + user.Balance);
    Console.WriteLine("Lutfen para yatırınız ");
    Console.Write("Miktar : ");
    int para = int.Parse(Console.ReadLine());

    user.Balance = user.Balance + para;
    Console.WriteLine("Para yatırma islemi gerceklesmistir");
    Console.WriteLine("Yeni Bakiye : " + user.Balance);
    log.succesfullDeposit(user.UserName, user.Id, para);
}
void ParaGonder(User user)
{
    Console.WriteLine("Bakiyeniz : " + user.Balance);
    Console.Write("Hesap İsmi :");
    string userName = Console.ReadLine();
    User recipientUser = InMemoryDB.users.FirstOrDefault(x => x.UserName == userName);
    if (recipientUser != null)
    {
        Console.Write("Gonderilicek Miktar : ");
        int miktar = int.Parse(Console.ReadLine());
        user.Balance -= miktar;
        recipientUser.Balance += miktar;
        Console.WriteLine("Gonderme islemi basarili olmustur");
        log.succesfullSendMoney(user.UserName, user.Id, miktar, recipientUser.UserName, recipientUser.Id);
    }
    else
    {
        Console.WriteLine("Girilen kullanıcı bulunamadı");
        log.FailedSendMoney(user.UserName, user.Id, 0, recipientUser.UserName, recipientUser.Id);
    }
}
void Bakiye(User user)
{
    Console.WriteLine("Guncel bakiye : " + user.Balance);
    log.viewBalance(user.UserName, user.Id);
}





class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Balance { get; set; }

}

public class Log
{
    //Başarılı giriş loglaması
    public void succesFullLogin(string name, int id)
    {
        string logMessage = $"{DateTime.Now} : {id} numaralı kullanıcı başarılı giris yapti ";
        writeLog(id, logMessage);
    }
    //Başarısız giriş loglaması
    public void failedLogin(string name, int id)
    {
        string logMessage = $"{DateTime.Now} : {id} numaralı kullanıcı hatalı giris yapti ";
        writeLog(id, logMessage);
    }
    //Başarılı para çekme loglaması
    public void succesfullWithdraw(string name, int id, int amount)
    {
        string logMessage = $"{DateTime.Now} : {id} numaralı kullanıcı - {amount} miktarda başarılı para çekme işlemı gerçekleştirmiştir";
        writeLog(id, logMessage);
    }

    //Başarısız para çekme loglaması
    public void failedWithdraw(string name, int id, int amount)
    {
        string logMessage = $"{DateTime.Now} : {id} numaralı kullanıcı - {amount} miktarda başarısız para çekme işlemi denemesi gerçekleştirmiştir";
        writeLog(id, logMessage);
    }

    //Başarılı para yatırma loglaması
    public void succesfullDeposit(string name, int id, int amount)
    {
        string logMessage = $"{DateTime.Now} : {id} numaralı kullanıcı - {amount} miktarda başarılı para yatırma işlemi denemesi gerçekleştirmiştir";
        writeLog(id, logMessage);
    }

    //Başarısız para yatırma loglaması
    public void failedDeposit(string name, int id, int amount)
    {
        string logMessage = $"{DateTime.Now} : {id} numaralı kullanıcı - {amount} miktarda başarısız para yatırma işlemi denemesi gerçekleştirmiştir";
        writeLog(id, logMessage);
    }

    //Başarılı para gönderme loglaması
    public void succesfullSendMoney(string senderName, int senderId, int amount, string recipientName, int recipientId)
    {
        string logMessage = $"{DateTime.Now} : {senderId} numaralı kullanıcı - {recipientName} numaralı kullanıcıya {amount} miktarda para göndermiştir ";
        writeLog(senderId, logMessage);
    }

    //Başarısız para göndermek loglaması
    public void FailedSendMoney(string senderName, int senderId, int amount, string recipientName, int recipientId)
    {
        string logMessage = $"{DateTime.Now} : {senderId} numaralı kullanıcı - {recipientName} numaralı kullanıcıya {amount} miktarda para gönderme işlemi gerçekleştirilemedi ";
        writeLog(senderId, logMessage);
    }

    //Başarılı para almma loglaması
    public void succesfullTakeMoney(string senderName, int senderId, int amount, string recipientName, int recipientId)
    {
        string logMessage = $"{recipientId} kimlik numaralı {recipientName} kişisi {DateTime.Now} tarihinde {senderId} kimlik numaralı {senderName} kişisinden {amount} miktarda başarılı para alma işlemi  gerçekleştirmiştir";
        writeLog(recipientId, logMessage);
    }

    //Başarılı bakiye görüntüleme loglaması
    public void viewBalance(string name, int id)
    {
        string logMessage = $"{DateTime.Now} : {id} numaralı kullanıcı - bakiyesini görüntülemiştir";
        writeLog(id, logMessage);
    }

    //Çıkış yapma loglaması
    public void LogOut(string name, int id)
    {
        string logMessage = $"{DateTime.Now} : {id} numaralı kullanıcı  hesabından çıkış yapmıştır";
        writeLog(id, logMessage);
    }



    public void checkAndCreate()
    {
        /* Bu metot var olan listedeki her eleman için bir log dosyası oluşturma amacıyla yazılmıştır
         * Amaç belirtilen dosya yoluna gidip o dosya yolunda belirtilen klasörü açmak yoksa ise
         * o klasörü oluşturup listedeki her kullanıcı için bir file.txt açmak
         */
        string filePath = @"C:\Users\fatih\Documents\Code\CSharp\Patika\Log";
        if (!Directory.Exists(filePath))//Logların bulunduğu dosya yoksa oluşturur
        {
            Directory.CreateDirectory(filePath);//Dosya yoksa oluşturur

            foreach (var user in InMemoryDB.users)//Her kullanıcıya bir file.txt açılır
            {
                //Aşağıdaki kod ise string ifadeleri birleştirerek bir dosya yolu oluşturur
                string logFilePath = Path.Combine(filePath, user.Id + ".txt");

                if (!File.Exists(logFilePath))
                    File.Create(logFilePath);
            }
        }
        else
        {
            /*Logların bulunduğu dosya varsa listedeki elemanların her birine ait log.txt varmı diye
             kontrol eder yoksa açar*/
            foreach (var user in InMemoryDB.users)
            {
                //Her kullanıcının id numarası ile bir log.txt açar
                string logFilePath = Path.Combine(filePath, user.Id + ".txt");
                if (!File.Exists(logFilePath))
                    File.Create(logFilePath);
            }
        }

    }

    //Log mesajını kullanıcının log dosyasına yazdıran metot
    public void writeLog(int id, string logMessage)
    {
        //Önce log dosyalarının bulunduğu ana dosyaya gideceğiz
        string folderPath = @"C:\Users\fatih\Documents\Code\CSharp\Patika\Log";
        //Sonra belirtilen dosya yolunu kapsayan log.txt dosya yollarını bir diziye aktaracağız
        string[] logFiles = Directory.GetFiles(folderPath, id + ".txt");

        if (logFiles.Length == 0)//Eğer dizi boyutu sıfırsa demekki öyle log.txt dosyaları yok
        {
            Console.WriteLine($"Belirtilen id numaradında ( {id}  ) bir kişi bulunamadı");
            return;
        }


        string logFilePath = logFiles[0];//Belirtilen koşulu sağlayan ilk log dosyasını alıyoruz

        //StreamWriter ile dosyaya yazdırma işlemi gerçekleştiriyoruz  
        using (StreamWriter logWriter = File.AppendText(logFilePath))
        {
            //AppenText() metodu var olan metin  dosyasına yeni bir metin dosyası eklemek için kullanılıır
            logWriter.WriteLine(logMessage);
        }
    }


}

class InMemoryDB
{

    private static List<User> _users;
    static InMemoryDB()
    {


        _users = new()
        {
            new User{Id=1001,UserName="fatih",Password="abc",Balance=1000},
            new User{Id=1002,UserName="ahmet",Password="abc",Balance=1000},
            new User{Id=1003,UserName="mehmet",Password="abc",Balance=1000},
            new User{Id=1004,UserName="ayse",Password="abc",Balance=1000},

        };
    }
    public static List<User> users => _users;
}














































