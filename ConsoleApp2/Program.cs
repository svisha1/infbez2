using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
class RSACSPSample
{
    static void Main()
    {
        ASCIIEncoding ByteConverter = new ASCIIEncoding();
        string dataString = "Data to Encrypt";
        byte[] dataToEncrypt = ByteConverter.GetBytes(dataString);
        string file_name = Console.ReadLine();
        string file_name2 = Console.ReadLine();

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        StreamWriter writer = new StreamWriter(file_name);

        string publicPrivateKeyXML = rsa.ToXmlString(true);
        writer.Write(publicPrivateKeyXML);
        writer.Close();

        writer = new StreamWriter(file_name2);
        string publicOnlyKeyXML = rsa.ToXmlString(false);
        writer.Write(publicOnlyKeyXML);
        writer.Close();

        byte[] cipherbytes = Encrypt(file_name2, dataToEncrypt);
        Decrypt(file_name, cipherbytes);

    }

    public static void Decrypt(String file_name, byte[] cipherbytes)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        StreamReader reader = new StreamReader(file_name);
        string publicPrivateKeyXML = reader.ReadToEnd();
        rsa.FromXmlString(publicPrivateKeyXML);
        reader.Close();
        byte[] plainbytes = rsa.Decrypt(cipherbytes, false); //fOAEP
        Console.WriteLine("==============================");
        Console.WriteLine(Encoding.UTF8.GetString(plainbytes));
    }

    public static byte[] Encrypt(String file_name, byte[] dataToEncrypt)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        StreamReader reader = new StreamReader(file_name);
        string publicOnlyKeyXML = reader.ReadToEnd();
        rsa.FromXmlString(publicOnlyKeyXML);
        reader.Close();
        byte[] cipherbytes = rsa.Encrypt(dataToEncrypt, false); //fOAEP
        Console.WriteLine("------------------------------");
        Console.WriteLine(Encoding.UTF8.GetString(dataToEncrypt));
        Console.WriteLine("------------------------------");
        Console.WriteLine(Encoding.UTF8.GetString(cipherbytes));
        return cipherbytes;
    }
}