using System.Text;

namespace Testes;
public class Encrypted
{
    public Encrypted()
    {
        var encrypted = Encoding.UTF8.GetBytes("Eclepsis-Dragon_Ruler_of_Woes");
        Console.WriteLine(encrypted.Length);
    }
};

