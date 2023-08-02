Console.WriteLine("Ekrandan bir string bir de sayı alan (aralarında virgül ile), ilgili string ifade içerisinden verilen indexteki karakteri çıkartıp ekrana yazdıran console uygulasını yazınız.\r\n\r\nÖrnek: Input: Algoritma,3 Algoritma,5 Algoritma,22 Algoritma,0");
Console.Write("String : ");
string x = Console.ReadLine();

string[] y = x.Split(',',' ');

for(int i=0; i<y.Length; i+=2)
    Console.WriteLine(y[i]);