#define Debug
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace bitlc_do1
{
    public partial class AssistSonja : Form
    {

        #region formloader
        // klassen können über univers aufeinander zugreifen
        Univers univers = new Univers();
        ModulRec modulRec;
        ModulSynt modulSynt;
        ModulMainSynt modulMainSynt;
        ModulAIMLBot modulAIMLBot;
        ModulSound modulSound;
        ModulAction modulSQL;

        

        public AssistSonja()
        {
            InitializeComponent();
            //starten von allen kassen 
            univers.form1 = this;
            modulRec = new ModulRec(univers);
            modulSynt = new ModulSynt(univers);
            modulMainSynt = new ModulMainSynt(univers);
            modulAIMLBot = new ModulAIMLBot(univers);
            modulSound = new ModulSound(univers);
            modulSQL = new ModulAction(univers);
            univers.modABot.Run();
            univers.modRec.Run();
            univers.modSynt.Run();
            univers.modMainSynt.Run();
            univers.modSound.Run();
            univers.modAct.Run();
            TirUpDate.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SelectScreen();

            FaceOpen();

            StartSock();
        }

        // programm auf dem zweiten bildschierm öffnen
        private void SelectScreen()
        {
            Screen creen = null;
            if (Screen.AllScreens.GetUpperBound(0) > 0)
            {
                creen = Screen.AllScreens[1];
            }

            if (creen != null)
            {
                this.Location = creen.Bounds.Location;
            }
        }
        #endregion

        #region Socket
        TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
        TcpClient client;
        Dictionary<string, TcpClient> clientList = new Dictionary<string, TcpClient>();
        CancellationTokenSource cancellation = new CancellationTokenSource();
        List<string> chat = new List<string>();
        public void updatUI(String m)
        {
            this.Invoke((MethodInvoker)delegate // To Write the Received data
            {
                txtSockBox.AppendText(">>" + m + Environment.NewLine);
            });
        }
        private void StartSock()
        {
            cancellation = new CancellationTokenSource(); //resets the token when the server restarts
            startServer();
        }
        public void updateUI(String m)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate // To Write the Received data
                {
                    txtSockBox.AppendText(">>" + m + Environment.NewLine);
                });
            }
            catch (Exception e)
            {


            }
          
        }
        public async void startServer()
        {
            listener.Start();
            updateUI("Server Started at " + listener.LocalEndpoint);
            updateUI("Waiting for Clients");
            try
            {
                int counter = 0;
                while (true)
                {
                    counter++;
                    //client = await listener.AcceptTcpClientAsync();
                    client = await Task.Run(() => listener.AcceptTcpClientAsync(), cancellation.Token);

                    /* get username */
                    byte[] name = new byte[50];
                    NetworkStream stre = client.GetStream(); //Gets The Stream of The Connection
                    stre.Read(name, 0, name.Length); //Receives Data 
                    String username = Encoding.ASCII.GetString(name); // Converts Bytes Received to String
                    username = username.Substring(0, username.IndexOf("$"));

                    /* add to dictionary, listbox and send userList  */
                    clientList.Add(username, client);
                    listSocketBox.Items.Add(username);
                    updateUI("Connected to user " + username + " - " + client.Client.RemoteEndPoint);
                    announce(username + " Joined ", username, false);

                    await Task.Delay(1000).ContinueWith(t => sendUsersList());


                    var c = new Thread(() => ServerReceive(client, username));
                    c.Start();

                }
            }
            catch (Exception)
            {
                listener.Stop();
            }

        }
        public void announce(string msg, string uName, bool flag)
        {
            try
            {
                foreach (var Item in clientList)
                {
                    TcpClient broadcastSocket;
                    broadcastSocket = (TcpClient)Item.Value;
                    NetworkStream broadcastStream = broadcastSocket.GetStream();
                    Byte[] broadcastBytes = null;

                    if (flag)
                    {
                        //broadcastBytes = Encoding.ASCII.GetBytes("gChat|*|" + uName + " says : " + msg);

                        chat.Add("gChat");
                        chat.Add(uName + " says : " + msg);
                        broadcastBytes = ObjectToByteArray(chat);
                    }
                    else
                    {
                        //broadcastBytes = Encoding.ASCII.GetBytes("gChat|*|" + msg);

                        chat.Add("gChat");
                        chat.Add(msg);
                        broadcastBytes = ObjectToByteArray(chat);

                    }

                    broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                    broadcastStream.Flush();
                    chat.Clear();
                }
            }
            catch (Exception er)
            {

            }
        }  //end broadcast function
        public Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
        public byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public void ServerReceive(TcpClient clientn, String username)
        {
            byte[] data = new byte[100000];
            while (true)
            {
                try
                {
                    NetworkStream stream = clientn.GetStream(); //Gets The Stream of The Connection
                    stream.Read(data, 0, data.Length); //Receives Data 
                    List<string> parts = (List<string>)ByteArrayToObject(data);

                    switch (parts[0])
                    {
                        case "gChat":
                            this.Invoke((MethodInvoker)delegate // To Write the Received data
                            {
                                txtSockBox.Text += username + ": " + parts[1] + Environment.NewLine;
                            });
                            announce(parts[1], username, true);
                            break;

                        case "pChat":
                            privateChat(parts);
                            break;
                    }

                    parts.Clear();
                }
                catch (Exception r)
                {
                    updateUI("Client Disconnected: " + username);
                    announce("Client Disconnected: " + username + "$", username, false);
                    clientList.Remove(username);

                    this.Invoke((MethodInvoker)delegate
                    {
                        listSocketBox.Items.Remove(username);
                    });
                    sendUsersList();
                    break;
                }
            }
        }
        public void btnServerStop_Click()
        {
            try
            {
                listener.Stop();
                updateUI("Server Stopped");
                foreach (var Item in clientList)
                {
                    TcpClient broadcastSocket;
                    broadcastSocket = (TcpClient)Item.Value;
                    broadcastSocket.Close();
                }
            }
            catch (SocketException er)
            {

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listSocketBox.SelectedIndex != -1)
            {
                String clientName = listSocketBox.GetItemText(listSocketBox.SelectedItem);

                chat.Clear();
                chat.Add("gChat");
                chat.Add("Admin : " + txtInputbox.Text);

                byte[] byData = ObjectToByteArray(chat);
                TcpClient workerSocket = null;
                workerSocket = (TcpClient)clientList.FirstOrDefault(x => x.Key == clientName).Value; //find the client by username in dictionary

                NetworkStream stm = workerSocket.GetStream();
                stm.Write(byData, 0, byData.Length);
                stm.Flush();
                chat.Clear();

            }
        }
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient workerSocket = null;

                String clientName = listSocketBox.GetItemText(listSocketBox.SelectedItem);
                workerSocket = (TcpClient)clientList.FirstOrDefault(x => x.Key == clientName).Value; //find the client by username in dictionary
                workerSocket.Close();

            }
            catch (SocketException se)
            {
            }
        }
        public void sendUsersList()
        {
            try
            {
                byte[] userList = new byte[1024];
                string[] clist = listSocketBox.Items.OfType<string>().ToArray();
                List<string> users = new List<string>();

                users.Add("userList");
                foreach (String name in clist)
                {
                    users.Add(name);
                }
                userList = ObjectToByteArray(users);

                foreach (var Item in clientList)
                {
                    TcpClient broadcastSocket;
                    broadcastSocket = (TcpClient)Item.Value;
                    NetworkStream broadcastStream = broadcastSocket.GetStream();
                    broadcastStream.Write(userList, 0, userList.Length);
                    broadcastStream.Flush();
                    users.Clear();
                }
            }
            catch (SocketException se)
            {
            }
        }
        private void privateChat(List<string> text)
        {
            try
            {

                byte[] byData = ObjectToByteArray(text);

                TcpClient workerSocket = null;
                workerSocket = (TcpClient)clientList.FirstOrDefault(x => x.Key == text[1]).Value; //find the client by username in dictionary

                NetworkStream stm = workerSocket.GetStream();
                stm.Write(byData, 0, byData.Length);
                stm.Flush();

            }
            catch (SocketException se)
            {
            }
        }
        private void txtSockBox_TextChanged(object sender, EventArgs e)
        {
            txtSockBox.SelectionStart = txtSockBox.TextLength;
            txtSockBox.ScrollToCaret();
        }
        #endregion

        #region FormLock
        public bool faceNext = false;
        private void TirUpDate_Tick(object sender, EventArgs e)
        {

            this.Invalidate();
            this.Refresh();
            if (faceNext == true)
            {
                FaceNext();
            }
         
        }
#if Debug
        int curserX = 0;
        int curserY = 0;
#endif
        //performens counter 
        PerformanceCounter perfCpu = new PerformanceCounter("Processor Information", "% processor Time", "_Total");
        PerformanceCounter perfmem = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        public string botin = "in";
        public string botout = "out";
        public bool sonja = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            int m = (int)perfmem.NextValue() / 2;
            int p = (int)perfCpu.NextValue();

            Graphics dc = e.Graphics;

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font fontt = new Font("Engravers MT", 7, FontStyle.Regular);
            TextRenderer.DrawText(dc,DateTime.Now.ToString("HH:mm"),
                fontt, new Rectangle(531, 294, 120, 20), SystemColors.ControlText, flags);

#if Debug
            TextRenderer.DrawText(dc, botin,
                fontt, new Rectangle(0, 40, 220, 10), SystemColors.ControlText, flags);

            TextRenderer.DrawText(dc, botout,
                fontt, new Rectangle(0, 80, 220, 10), SystemColors.ControlText, flags);
            
           

            Font font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc, "x=" + curserX.ToString() + ":" + "y=" + curserY.ToString(),
                font, new Rectangle(400, 38, 120, 20), SystemColors.ControlText, flags);
#endif

            if (sonja == true)
            {
                int x = 300;
                int y = 40;

                Rectangle srcRect = new Rectangle(30, 27, 76, 97);
                GraphicsUnit units = GraphicsUnit.Pixel;
                e.Graphics.DrawImage(imp, x, y, srcRect, units);
            }

            Font fon = new Font("Magneto", 8, FontStyle.Regular);
            TextRenderer.DrawText(dc, "CPU:" + " " + p + " " + "%",
                fon, new Rectangle(800, 336, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(dc, "Memory:" + " " + m + " " + "%",
                fon, new Rectangle(800, 400, 120, 20), SystemColors.ControlText, flags);

            Pen pen = new Pen(Color.Cyan, 1);
            e.Graphics.DrawLine(pen, 770, 330, 780, 330);
            e.Graphics.DrawLine(pen, 780, 330, 785, 330 + p);
            e.Graphics.DrawLine(pen, 785, 330 + p, 790 ,330);
            e.Graphics.DrawLine(pen, 790, 330 , 793, 330 - 5 - p);
            e.Graphics.DrawLine(pen, 793, 330 - 5 - p, 796, 330);
            e.Graphics.DrawLine(pen, 796, 330, 805, 330);

            e.Graphics.DrawLine(pen, 820, 390, 840, 390);
            e.Graphics.DrawLine(pen, 840, 390, 860, 390 - (m / 2));
            e.Graphics.DrawLine(pen, 860, 390 - (m / 2), 865, 390 - (m / 2));



            base.OnPaint(e);
        }
        private void AssistSonja_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.X < 30 && e.Y > 0 && e.Y < 200)
            {
                univers.modMainSynt.Sonjasay("bitte nicht kitzeln");

            }
        }
        private void AssistSonja_MouseMove(object sender, MouseEventArgs e)
        {
#if Debug
            curserX = e.X;
            curserY = e.Y;
#endif
        }
       
        Image imp;
        int ind = 0;
        public string anweisung;
        private Bitmap[] bmps = null;
        public void FaceOpen()
        {
            string dname = null;

            if (anweisung == "start")
            {
                dname = @"C:\bitlc_do1\bitlc_do1\bin\Debug\data_grafix\SonjaFace1\";
            }
            else
            {
                dname = @"C:\bitlc_do1\bitlc_do1\bin\Debug\data_grafix\Assets\";
               
            }
            string[] fnames = Directory.GetFiles(dname, "*.png");
            bmps = new Bitmap[fnames.Length];
            for (int i = 0; i < fnames.Length; i++)
            {
                bmps[i] = (Bitmap)Bitmap.FromFile(fnames[i]) as Bitmap;
            }
            imp = bmps[ind % bmps.Length];
            ind++;
        }
        public void FaceNext()
        {
            imp = bmps[ind % bmps.Length];
            ind++;
        }
        #endregion

        #region uperPannel
        // int bereit stellen
        new int Top;
        int MoveX, MoveY;
        private void PnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }
        private void PnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }
        private void PnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }
        private void btnFromOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnFromKlein_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

    }
}
