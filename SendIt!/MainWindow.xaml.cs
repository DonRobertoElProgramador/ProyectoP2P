using SendIt_.Comandos;
using System.Windows;

namespace SendIt_
{
    public partial class MainWindow : Window
    {
        private iChatConnector chatconnector;
        public ComandoEnter PresionarEnter { get; private set; }


        public MainWindow(iController chatcontroller, iChatConnector chatconnector)
        {
            InitializeComponent();
            this.DataContext = this;
            this.chatconnector = chatconnector;
            listBox.ItemsSource = chatcontroller.getMessages();
            listBox.DisplayMemberPath = "message";

            chatconnector.connect();

            PresionarEnter = new ComandoEnter(enviarMensaje);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            enviarMensaje();
        }

        private void enviarMensaje()
        {
            Message mess = new Message();
            mess.message = textBox.Text;
            textBox.Clear();            
            chatconnector.sendMessage(mess.message);
        }        

        
    }
    }
