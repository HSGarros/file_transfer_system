using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.IO;

namespace Client
{
    public partial class Client : Form
    {
        string resourceDirectory = null;
        string Filename = null;
        public Client()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            GetIp();
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            resourceDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(rootPath)))));
            resourceDirectory = Path.Combine(resourceDirectory, "resource");
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
        Socket socketSend = null;


        private void Recive()//接收服务端不同的消息类型
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];
                    int r = socketSend.Receive(buffer);
                    if (r == 0) break;
                    /*ShowMes("收到消息");
                    string count = Encoding.UTF8.GetString(buffer, 1, r - 1);
                    ShowMes("收到消息长" + count.Length);
                    ShowMes("收到消息"+ count);*/
                    if (buffer[0] == 0)
                    {
                        string count = Encoding.UTF8.GetString(buffer, 1, r - 1);

                        string[] splitted = count.Split(':');
                        ShowMes("all file:   " + string.Join("   ", splitted) + "   时间：" + DateTime.Now.ToString());
                    }
                    else if (buffer[0] == 1)
                    {

                        string filePath = Path.Combine(resourceDirectory, Filename);

                        File.WriteAllBytes(filePath, buffer[1..r]);
                        Application.EnableVisualStyles();
                        Application.Run(new Show(filePath));
                        File.Delete(filePath);
                        ShowMes("show file");
                    }
                }
                catch { }
            }
        }

        private void ShowMes(string mes)//提示消息
        {
            txtMes.AppendText(mes + "\r\n");
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

                //开新线程接收服务端的消息
                Thread th = new Thread(Recive);
                th.IsBackground = true;
                th.Start();
            }
            catch { }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string str = txtSendMes.Text.Trim();
                Filename = str;
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                List<byte> list = new List<byte>();
                list.Add(1);
                list.AddRange(buffer);
                ShowMes(str);
                byte[] newBuffer = list.ToArray();
                socketSend.Send(newBuffer);
            }
            catch { }
        }

        private void txtClientPoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void btnls_Click(object sender, EventArgs e)
        {
            try
            {

                List<byte> list = new List<byte>();
                list.Add(0);
                ShowMes("show all file");
                byte[] newBuffer = list.ToArray();
                socketSend.Send(newBuffer);
            }
            catch { }
        }
    }
}