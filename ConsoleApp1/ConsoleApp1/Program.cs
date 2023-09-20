using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection.Emit;



class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: MyApp.exe <file_path>");
            return;
        }

        Application.EnableVisualStyles();
        Application.Run(new MainForm(args[0]));
    }
}
class Program
{
    static void Main()
    {
        byte[] content = new byte[] { 72, 101, 108, 108, 111, 32, 87, 111, 114, 108, 100 }; // Hello World in ASCII
        string filePath = @"C:\Users\HSGarros\Desktop\727A1\test1.bmp";

        File.WriteAllBytes(filePath, content);
        /*string filename = @"C:\Users\HSGarros\Desktop\727A1\A1\test1.bmp";
        int begin = 0;
        int end = 15;
        byte[] buffer = new byte[11];
        byte[] next = new byte[1];
        using (FileStream stream = new FileStream(filename, FileMode.Open))
        using (BinaryReader reader = new BinaryReader(stream))
        {
            int b;
            int mod;
            // 将文件指针移动到指定位置
            stream.Seek(0, SeekOrigin.Begin);
            // 读取指定数量的字节
            buffer = reader.ReadBytes(15);
            mod = ComputeMD5Mod2048(buffer);
            Array.Resize(ref buffer, buffer.Length + 1);
            while ((b = stream.ReadByte()) != -1)
            {
                mod = (mod + (int)b) % 2048;
                
                end++;

                if (mod == 666)
                {
                    Console.WriteLine(mod);
                    begin = end + 1;
                    end = begin + 11;
                    buffer = reader.ReadBytes(15);
                    mod = ComputeMD5Mod2048(buffer);
                }
                else
                {
                    
                    for (int i = 1; i < 15; i++) buffer[i - 1] = buffer[i];
                    buffer[14] = (byte)b;
                    end = end + 1;
                }

            }
            Console.WriteLine(mod);

*/
    }
    static void tes()
    {
        byte[] bytes = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A };

        int hash = MD5Hash(bytes);

        Console.WriteLine("MD5 hash of {0}", hash);
    }

    static int MD5Hash(byte[] bytes)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(bytes);
            ushort shortHash = BitConverter.ToUInt16(hash, 0);
            return shortHash % 2048;
        }
    }
    static byte[] GenerateRandomBytes(int length)
    {
        byte[] bytes = new byte[length];
        new Random().NextBytes(bytes);
        return bytes;
    }
    static string ComputeMD5Hash(byte[] bytes)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
    static int ComputeMD5Mod2048(byte[] bytes)
    {
        bytes.Sum(b => (int)b);
        return bytes.Sum(b => (int)b) % 2048;
    }
        static string ByteArrayToString(byte[] array)
    {
        return BitConverter.ToString(array).Replace("-", "");
    }
    private void btnSendFile_Click(object sender, EventArgs e)
    {
        try
        {
            /*using (FileStream fs = new FileStream(txtFilePath.Text.Trim(), FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int r = fs.Read(buffer, 0, buffer.Length);
                List<byte> list = new List<byte>();
                list.Add(1);
                list.AddRange(buffer);
                byte[] newBuffer = list.ToArray();
                cmbClientList[0].Send(newBuffer, 0, r + 1, SocketFlags.None);
            }*/
        }
        catch { }
    }
}