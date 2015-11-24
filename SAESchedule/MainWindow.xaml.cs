using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAESchedule
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

        private static string saveFile = "./persistent.bin";

        public List<User> MW_userList;
        

		public MainWindow()
		{
			InitializeComponent();

            AutoLoad();


            //Initalizirung
            MW_userList = new List<User>();
         

		}

        private void AutoLoad()
        {
            string fullPath = System.IO.Path.GetFullPath(saveFile);

            if (File.Exists(fullPath))
            {
                using (Stream stream = File.Open(fullPath, FileMode.Open))
                {
                    ShiftScheduler.Load(stream);
                }
            }
        }

       

        private bool AddUser()
        {

            

            return false;
        
        
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            UserAddWindow win = new UserAddWindow();
            win.Show();
            win.mainW = this; ;
            
            
        }

      
	}
}
