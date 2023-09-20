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
        private void GetIp()//��ȡ����ip
        {
            string hostName = Dns.GetHostName();//��������
            IPAddress[] ipList = Dns.GetHostAddresses(hostName);//����ip������ipv4��ipv6��
            foreach (IPAddress ip in ipList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    txtClientIp.Text = ip.ToString().Trim();
                }
            }
        }



        private void handle()//���շ���˲�ͬ����Ϣ����
        {

            try
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int r = socketSend.Receive(buffer);
                ShowMes("���յ���Ϣ");
                if (buffer[0] == 2)
                {
                    string str = Encoding.UTF8.GetString(buffer, 1, r - 1);
                    ShowMes("�����" + socketSend.RemoteEndPoint + "����:  " + str + "   ʱ�䣺" + DateTime.Now.ToString());
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
                            // �� buffer д�� MemoryStream
                            memoryStream.Write(hashDict[hash], 0, hashDict[hash].Length);
                        }

                        // �� MemoryStream ת��Ϊ byte[]
                        byte[] result = memoryStream.ToArray();
                        SendToClient(1, result);
                        //result����װ�õ��ļ���
                    }
                    ShowMes("response: " + Math.Round((1 - num / fragmentHashList.Count) * 100, 2) + "% of file " + FileName + " constructed with the cached data");
                    /*using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fs.Write(buffer, 1, r - 1);
                    }
                    MessageBox.Show("�ļ�����ɹ�");*/
                }
            }
            catch { }

        }


        private void ShowMes(string mes)//��ʾ��Ϣ
        {
            txtMes.AppendText(mes + "\r\n");
        }

        private void ShowMes2(string mes)//��ʾ��Ϣ
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
                //��list����ת�����µ�����
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
                //��list����ת�����µ�����
                byte[] newBuffer = list.ToArray();
                cmbClientList[0].Send(newBuffer);
            }
            catch { }

        }

        private void btnContent_Click(object sender, EventArgs e)
        {

            try
            {
                //��������ͨ�ŵ�Socket
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(txtClientIp.Text.Trim());
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtClientPoint.Text.Trim()));
                //���Զ��Ҫ���ӵ�IP�Ͷ˿ں�
                socketSend.Connect(point);

                ShowMes("���ӷ���˳ɹ�");

            }
            catch { }
        }
        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                //��ʼ�������ڷ������˴���һ���������IP��ַ�Ͷ˿ںŵ�Socket
                Socket socketWacth = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                //�����˿�
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtChachePoint.Text.Trim()));
                //����
                socketWacth.Bind(point);
                ShowMes("������Ѿ���������");
                socketWacth.Listen(10);//��������

                //��ͨ�Ŵ����µ��߳�ȥִ��
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.Listen), socketWacth);
            }
            catch { }
        }

        Socket socketClientSend = null;
        //��Զ�����ӿͻ��˵�IP��ַ��Socket���뼯����
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        private void Listen(object obj)
        {
            Socket skt = obj as Socket;//����ת��
            while (true)
            {
                try
                {
                    //�ȴ��ͻ��˵����ӣ����Ҵ���һ������ͨ�ŵ�Socket
                    socketClientSend = skt.Accept();
                    //��Զ�����ӿͻ��˵�IP��ַ��Socket���뼯����
                    dicSocket.Add(socketClientSend.RemoteEndPoint.ToString(), socketClientSend);
                    //�ͻ���IP��ַ�Ͷ˿ںŴ���������
                    cmbClientList.Add(socketClientSend);
                    ShowMes("�ͻ��ˣ�" + socketClientSend.RemoteEndPoint.ToString() + ":���ӳɹ�");

                    //��ͨ�Ŵ����µ��߳�ȥִ��
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
                    //�ͻ������ӳɹ��󣬷�����Ӧ�ý��տͻ��˷�������Ϣ
                    byte[] buffer = new byte[1024 * 1024 * 5];
                    //ʵ�ʽ��յ�����Ч�ֽ���
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