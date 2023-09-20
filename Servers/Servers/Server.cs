using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Diagnostics;

namespace Servers
{
    public partial class Server : Form
    {
        List<Socket> cmbClientList = null;
        string resourceDirectory = null;
        List<string> fileName = new List<string>();
        Dictionary<string, List<string>> fragmentDict = new Dictionary<string, List<string>>();
        Dictionary<string, (int, int)> hashDict = new Dictionary<string, (int begin, int end)>();
        int count = 0;
        int FIX = 666;
        public Server()
        {
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            GetIp();//��ȡ����ip
            cmbClientList = new List<Socket>();

            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            resourceDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(rootPath)))));
            resourceDirectory = Path.Combine(resourceDirectory, "resource");
            string[] files = Directory.GetFiles(resourceDirectory);

            foreach (string file in files)
            {
                fileName.Add(Path.GetFileName(file));
            }
            foreach (string fn in fileName)
            {
                fragmentAndHash(fn);
            }


        }
        private void fragmentAndHash(string filename)
        {
            filename = Path.Combine(resourceDirectory, filename);
            int begin = 0;
            int end = 14;
            int length = 0;
            byte[] buffer = new byte[15];
            List<string> hashlist = new List<string>();
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int b;
                int mod;
                int sum = 0;
                int num = 0;
                byte[] frag = new byte[1024 * 32];
                // ���ļ�ָ���ƶ���ָ��λ��
                stream.Seek(0, SeekOrigin.Begin);
                // ��ȡָ���������ֽ�
                buffer = reader.ReadBytes(15);
                Array.Copy(buffer, frag, 15);
                length = 15;
                mod = ComputeMod2048(buffer);
                while ((b = stream.ReadByte()) != -1)
                {

                    mod = (mod + b) % 2048;
                    end++;

                    frag[length] = (byte)b;
                    length++;

                    if (mod == 198)
                    {
                        sum += end - begin;
                        num++;
                        //ShowMes(begin + " " + end);
                        if (!hashDict.ContainsKey(ComputeMD5Hash(frag))) hashDict.Add(ComputeMD5Hash(frag), (begin, end));
                        begin = end + 1;
                        Array.Clear(buffer, 0, buffer.Length);
                        hashlist.Add(ComputeMD5Hash(frag));

                        for (int i = 0; i < 15; i++)
                        {
                            if ((b = stream.ReadByte()) != -1)
                            {
                                buffer[i] = (byte)b;
                                end++;
                            }
                            else
                            {
                                break;
                            }

                        }
                        frag = new byte[1024 * 32];
                        Array.Copy(buffer, frag, 15);
                        length = 15;

                        mod = ComputeMod2048(buffer);
                    }
                    else
                    {
                        if (length > 10000)
                        {
                            //ShowMes("    " + length);
                        }
                        for (int i = 1; i < 15; i++) buffer[i - 1] = buffer[i];
                        buffer[14] = (byte)b;
                        mod = ComputeMod2048(buffer);
                    }

                }
                sum += end - begin;
                num++;
                ShowMes(begin + " " + end);
                hashlist.Add(ComputeMD5Hash(frag));
                if (!hashDict.ContainsKey(ComputeMD5Hash(frag))) hashDict.Add(ComputeMD5Hash(frag), (begin, end));
                if (fragmentDict.ContainsKey(filename))
                {
                    fragmentDict[filename] = hashlist;
                }
                else
                {
                    fragmentDict.Add(filename, hashlist);
                }
                //ShowMes("" + string.Join("  ", fragmentDict[filename]));

                ShowMes(" " + sum);
                ShowMes(" " + num);
                ShowMes(" " + sum / num);
                ShowMes(" " + hashDict.Count);

                using (var memoryStream = new MemoryStream())
                {
                    foreach (var hash in hashlist)
                    {
                        (int, int) posi = hashDict[hash];
                        stream.Seek(posi.Item1, SeekOrigin.Begin); // ��λ��ƫ������
                        byte[] needfragment = new byte[posi.Item2 - posi.Item1 + 1];
                        int bytesRead = stream.Read(needfragment, 0, posi.Item2 - posi.Item1 + 1);
                        memoryStream.Write(needfragment);
                    }

                    // �� MemoryStream ת��Ϊ byte[]
                    byte[] result = memoryStream.ToArray();
                    ShowMes("" + result.Length);
                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] hash = md5.ComputeHash(result);
                        ushort shortHash = BitConverter.ToUInt16(hash, 0);
                        ShowMes("" + shortHash);
                    }
                }

            }
        }
        static int ComputeMod2048(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(bytes);
                ushort shortHash = BitConverter.ToUInt16(hash, 0);
                return shortHash % 2048;
            }
        }
        static string ComputeMD5Hash(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
        private void GetIp()//��ȡ����ip
        {
            string hostName = Dns.GetHostName();//��������
            IPAddress[] ipList = Dns.GetHostAddresses(hostName);//����ip������ipv4��ipv6��
            foreach (IPAddress ip in ipList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    txtHostIp.Text = ip.ToString().Trim();
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                //��ʼ�������ڷ������˴���һ���������IP��ַ�Ͷ˿ںŵ�Socket
                Socket socketWacth = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                //�����˿�
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtHostPoint.Text.Trim()));
                //����c
                socketWacth.Bind(point);
                ShowMes("������Ѿ���������");
                socketWacth.Listen(10);//��������
                //��ͨ�Ŵ����µ��߳�ȥִ��
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.Listen), socketWacth);
            }
            catch { }
        }


        Socket socketSend = null;
        //��Զ�����ӻ���˵�IP��ַ��Socket���뼯����
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        private void Listen(object obj)
        {
            Socket skt = obj as Socket;//����ת��
            while (true)
            {
                try
                {
                    //�ȴ�����˵����ӣ����Ҵ���һ������ͨ�ŵ�Socket
                    socketSend = skt.Accept();
                    //��Զ�����ӻ���˵�IP��ַ��Socket���뼯����
                    dicSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend);
                    //�����IP��ַ�Ͷ˿ںŴ���������
                    cmbClientList.Add(socketSend);
                    ShowMes("����ˣ�" + socketSend.RemoteEndPoint.ToString() + ":���ӳɹ�");

                    //��ͨ�Ŵ����µ��߳�ȥִ��
                    Thread th = new Thread(Recive);
                    th.IsBackground = true;
                    th.Start(socketSend);
                }
                catch { }
            }
        }

        private void Recive(object obj)
        {
            Socket ssd = obj as Socket;
            while (true)
            {
                try
                {
                    //�ͻ������ӳɹ��󣬷�����Ӧ�ý��տͻ��˷�������Ϣ
                    byte[] buffer = new byte[1024 * 1024 * 5];
                    //ʵ�ʽ��յ�����Ч�ֽ���
                    int r = ssd.Receive(buffer);
                    if (r == 0) break;

                    if (buffer[0] == 1)
                    {

                        string str = string.Join(":", fileName);

                        ShowMes("�ͻ���" + socketSend.RemoteEndPoint + "��ȡ���п������ļ�    ʱ�䣺" + DateTime.Now.ToString());

                        SendToClient(2, Encoding.UTF8.GetBytes(str));


                    }
                    else if (buffer[0] == 2)
                    {
                        string filename = Encoding.UTF8.GetString(buffer, 1, r - 1);
                        ShowMes("1");
                        List<string> fnl = fragmentDict[Path.Combine(resourceDirectory, filename)];
                        ShowMes("2" + fnl.Count);
                        SendToClient(4, Encoding.UTF8.GetBytes(string.Join("", fnl.GetRange(0, fnl.Count))));
                        ShowMes("�ͻ���" + socketSend.RemoteEndPoint + "��ȡ�ļ�" + filename + "  ʱ�䣺" + DateTime.Now.ToString());
                    }
                    else if (buffer[0] == 3)
                    {
                        string hash = Encoding.UTF8.GetString(buffer, 1, 32);
                        string filename = Encoding.UTF8.GetString(buffer, 33, r - 1);
                        filename = filename.TrimEnd('\0');
                        (int, int) posi = hashDict[hash];
                        filename = Path.Combine(resourceDirectory, filename);
                        using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                        {
                            stream.Seek(posi.Item1, SeekOrigin.Begin); // ��λ��ƫ������
                            byte[] needfragment = new byte[posi.Item2 - posi.Item1 + 1];
                            int bytesRead = stream.Read(needfragment, 0, posi.Item2 - posi.Item1 + 1);
                            SendToClient(9, needfragment);
                        }

                    }
                }
                catch { }
            }
        }

        //�ļ�����


        private void ShowMes(string mes)//��ʾ��Ϣ
        {
            txtShowMes.AppendText(mes + "\r\n");
        }

        private void SendToClient(byte sort, byte[] buffer)
        {
            if (cmbClientList.Count < 1)
            {
                ShowMes("������" + DateTime.Now.ToString());
            }
            else
            {
                try
                {
                    List<byte> list = new List<byte>();
                    list.Add(sort);
                    list.AddRange(buffer);
                    //��list����ת�����µ�����
                    byte[] newBuffer = list.ToArray();
                    cmbClientList[0].Send(newBuffer);
                }
                catch { }
            }
        }



        /*        private void btnChose_Click(object sender, EventArgs e)
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.InitialDirectory = @"D\";
                    open.Title = "ѡ��Ҫ���͵��ļ�";
                    open.Filter = "�����ļ�|*.*";
                    open.ShowDialog();
                    txtFilePath.Text = open.FileName;
                }*/



        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = @"D\";
            open.Title = "ѡ��Ҫ���͵��ļ�";
            open.Filter = "�����ļ�|*.*";
            open.ShowDialog();
            string fullPath = open.FileName;
            string fileName = txtFilePath.Text;
            string destinationFile = Path.Combine(resourceDirectory, fileName);
            try
            {
                File.Copy(fullPath, destinationFile, true);
                fragmentAndHash(destinationFile);
            }
            catch { }
            
            
        }
    }
}