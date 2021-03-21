using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SendIt_
{
  
    public partial class App : Application
    {
        private readonly ServiceProvider serviceprovider;

        public App() {

            ServiceCollection servcol = new ServiceCollection();
            ConfigureServices(servcol);
            serviceprovider = servcol.BuildServiceProvider();
             }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<iController>(new ChatController());
            services.AddSingleton<iChatConnector>(x=> new ChatConnector(x.GetRequiredService<iController>()));            
        }

        private void OnStartup(object sender, StartupEventArgs events)
        {
            MainWindow mainwindow = serviceprovider.GetService<MainWindow>();
            
            mainwindow.Show();
        }

    }
}
