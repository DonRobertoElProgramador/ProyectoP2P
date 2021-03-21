using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SendIt_
{
    public class ChatConnector : iChatConnector
    {
        iController chatcontroller;

        public ChatConnector(iController chatcontroller) 
        {
            this.chatcontroller = chatcontroller;
        }

        public void sendMessage(String message)
        {
            try
            {                
                Int32 port = 6363;
                TcpClient client = new TcpClient("192.168.140.1", port);

                /*Traducimos el mensaje a una array de bytes*/
                Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);

                NetworkStream stream = client.GetStream();
                
                //Mandamos el mensaje
                stream.Write(data, 0, data.Length);

                //La respuesta
                data = new Byte[256];

                //El texto de la respuesta
                String responseData = String.Empty;

                //Leer los byes del stream
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);


                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void connect()
        {
            Thread thread = new Thread(new ThreadStart(serverStart));
            thread.Start();
    
        }

        private void serverStart()
        {

            TcpListener server = null;
            try
            {
                Debug.WriteLine("Iniciando conexión");

                //Puerto del TCP Server
                Int32 port = 6363;
                IPAddress address = IPAddress.Parse("192.168.140.1");
                server = new TcpListener(address, port);
                //Iniciamos el servidor, que escuchará a los posibles clientes
                server.Start();

                /*El buffer*/
                Byte[] informacionentrada = new Byte[256];
                String data = null;


                while (true)
                {
                    /*Bloqueante*/
                    TcpClient client = server.AcceptTcpClient();

                    /*Los datos que vamos a guardar*/
                    data = null;

                    //El stream en el que escribiremos/del que recibiremos los datos
                    NetworkStream stream = client.GetStream();

                    int i;

                    //Mientras haya datos en el buffer
                    while ((i = stream.Read(informacionentrada, 0, informacionentrada.Length)) != 0)
                    {
                        //Pasamos la información de entrada a string
                        data = System.Text.Encoding.UTF8.GetString(informacionentrada, 0, i);
                        
                        /*Preparamos una respuesta*/
                        byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);

                        //La escribimos en el stream y la mandamos
                        stream.Write(msg, 0, msg.Length);                      

                        
                        Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                         new Action(() => {
                             Message mess = new Message();
                             mess.message = data;
                             chatcontroller.addMessage(mess);
                         }));
                    }

                    //Cerramos la conexión
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                //Paramos el servidor
                server.Stop();
            }
        }


    }
}
