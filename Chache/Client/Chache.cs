using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client
{
    public partial class Chache : Form
    {
        List<Socket> cmbClientList = null;
        string FileName = null;

        Dictionary<string, byte[]> hashDict = new Dictionary<string, byte[]>();
        Socket socketSend = null;
        public Chache()
        {
            InitializeComponent();
            cmbClientList = new List<Socket>();
            Control.CheckForIllegalCrossThreadCalls = false;
            GetIp();

        }
        private void GetIp()//获取本机ip
        {
            string hostName = Dns.GetHostName();//本机名称
            IPAddress[] ipList = Dns.GetHostAddresses(hostName);//本机ip（包括ipv4和ipv6）
            foreach (IPAddress ip in ipList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    txtClientIp.Text = ip.ToString().Trim();
                }
            }
        }



        private void handle()//接收服务端不同的消息类型
        {

            try
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int r = socketSend.Receive(buffer);
                ShowMes("接收到消息");
                if (buffer[0] == 2)
                {
                    string str = Encoding.UTF8.GetString(buffer, 1, r - 1);
                    ShowMes("服务端" + socketSend.RemoteEndPoint + "发送:  " + str + "   时间：" + DateTime.Now.ToString());
                    SendToClient(0, buffer[1..r]);
                    ShowMes("1");
                }
                else if (buffer[0] == 4)
                {
                    List<string> fragmentHashList = new List<string>();
                    byte[] newfragment;

                    string str = Encoding.UTF8.GetString(buffer, 1, r - 1);
                    for (int i = 0; i < str.Length; i += 32)
                    {
                        string segment = str.Substring(i, 32);
                        fragmentHashList.Add(segment);
                    }
                    ShowMes("get fragment count" + fragmentHashList.Count);
                    double num = 0;
                    foreach (string fragmentHash in fragmentHashList)
                    {
                        if (!hashDict.ContainsKey(fragmentHash))
                        {
                            newfragment = new byte[1024 * 32];
                            SendToServers(3, Encoding.UTF8.GetBytes(fragmentHash + FileName));
                            num++;
                            int len = socketSend.Receive(newfragment);
                            hashDict.Add(fragmentHash, newfragment[1..(len)]);
                            ShowMes2(fragmentHash);
                        }
                    }
                    int sum = 0;
                    using (var memoryStream = new MemoryStream())
                    {
                        foreach (var hash in fragmentHashList)
                        {
                            sum = sum + hashDict[hash].Length;
                            // 将 buffer 写入 MemoryStream
                            memoryStream.Write(hashDict[hash], 0, hashDict[hash].Length);
                        }

                        // 将 MemoryStream 转换为 byte[]
                        byte[] result = memoryStream.ToArray();
                        SendToClient(1, result);
                        //result是组装好的文件块
                    }
                    ShowMes("response: " + Math.Round((1 - num / fragmentHashList.Count) * 100, 2) + "% of file " + FileName + " constructed with the cached data");
                    /*using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fs.Write(buffer, 1, r - 1);
                    }
                    MessageBox.Show("文件保存成功");*/
                }
            }
            catch { }

        }


        private void ShowMes(string mes)//提示消息
        {
            txtMes.AppendText(mes + "\r\n");
        }

        private void ShowMes2(string mes)//提示消息
        {
            txtMes2.AppendText(mes + "\r\n");
        }




        private void SendToServers(byte sort, byte[] buffer)
        {

            try
            {
                List<byte> list = new List<byte>();
                list.Add(sort);
                list.AddRange(buffer);
                //将list集合转换成新的数组
                byte[] newBuffer = list.ToArray();
                socketSend.Send(newBuffer);
            }
            catch { }

        }
        private void SendToClient(byte sort, byte[] buffer)
        {

            try
            {
                List<byte> list = new List<byte>();
                list.Add(sort);
                list.AddRange(buffer);
                //将list集合转换成新的数组
                byte[] newBuffer = list.ToArray();
                cmbClientList[0].Send(newBuffer);
            }
            catch { }

        }

        private void btnContent_Click(object sender, EventArgs e)
        {

            try
            {
                //创建负责通信的Socket
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(txtClientIp.Text.Trim());
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtClientPoint.Text.Trim()));
                //获得远程要连接的IP和端口号
                socketSend.Connect(point);

                ShowMes("连接服务端成功");

            }
            catch { }
        }
        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                //开始监听，在服务器端创建一个负责监听IP地址和端口号的Socket
                Socket socketWacth = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                //创建端口
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtChachePoint.Text.Trim()));
                //监听
                socketWacth.Bind(point);
                ShowMes("服务端已经启动监听");
                socketWacth.Listen(10);//连接数量

                //给通信创建新的线程去执行
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.Listen), socketWacth);
            }
            catch { }
        }

        Socket socketClientSend = null;
        //将远程连接客户端的IP地址和Socket存入集合中
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        private void Listen(object obj)
        {
            Socket skt = obj as Socket;//类型转换
            while (true)
            {
                try
                {
                    //等待客户端的连接，并且创建一个负责通信的Socket
                    socketClientSend = skt.Accept();
                    //将远程连接客户端的IP地址和Socket存入集合中
                    dicSocket.Add(socketClientSend.RemoteEndPoint.ToString(), socketClientSend);
                    //客户端IP地址和端口号存入下拉框
                    cmbClientList.Add(socketClientSend);
                    ShowMes("客户端：" + socketClientSend.RemoteEndPoint.ToString() + ":连接成功");

                    //给通信创建新的线程去执行
                    Thread th = new Thread(Recive2);
                    th.IsBackground = true;
                    th.Start(socketClientSend);
                }
                catch { }
            }
        }

        private void Recive2(object obj)
        {
            Socket ssd = obj as Socket;
            while (true)
            {
                try
                {
                    //客户端连接成功后，服务器应该接收客户端发来的消息
                    byte[] buffer = new byte[1024 * 1024 * 5];
                    //实际接收到的有效字节数
                    int r = ssd.Receive(buffer);
                    if (r == 0) break;
                    if (buffer[0] == 0)
                    {
                        SendToServers(1, buffer[0..r]);
                        ShowMes("get all filename");
                        handle();
                    }
                    else if (buffer[0] == 1)
                    {

                        FileName = Encoding.UTF8.GetString(buffer, 1, r - 1);
                        ShowMes("download file" + FileName);
                        SendToServers(2, buffer[1..r]);
                        handle();
                    }
                }
                catch { }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            hashDict.Clear();
            txtMes2.Clear();
            ShowMes("alreadly clean all fragment ");
        }
    }

}