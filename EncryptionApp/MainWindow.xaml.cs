using System;
using System.Collections.Generic;
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

namespace EncryptionApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RSAEncryptor encryptor { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Сalculate_Click(object sender, RoutedEventArgs e)
        {
            int p, q;
            try
            {
                p = int.Parse(pTextBox.Text);
                q = int.Parse(qTextBox.Text);

                if(p < 0 || q < 0 || p == q || p*q <= 257 || !p.IsPrime() || !q.IsPrime())
                {
                    throw new Exception();
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show("Некорректные значения!\np и q должны быть простыми числами, причём p ≠ q и p × q > 257.", "Ошибка!");
                return;
            }

            encryptor = new RSAEncryptor(p, q);
            publicKeyLabel.Content = $"Открытый ключ: {encryptor.PublicKey.First}, {encryptor.PublicKey.Second}";
            privateKeyLabel.Content = $"Закрытый ключ: {encryptor.PrivateKey.First}, {encryptor.PrivateKey.Second}";
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = messageTextBox.Text;
                StringBuilder encryptedMessage = new StringBuilder();
                foreach (var b in encryptor.Encrypt(encryptor.PublicKey, message))
                {
                    encryptedMessage.Append(b.ToString()+" ");
                }
                encrypdedMessageLabel.Text = encryptedMessage.ToString();

                dencrypdedMessageLabel.Content = encryptor.Decrypt(encryptor.Encrypt(encryptor.PublicKey, message));
            }
            catch(Exception exc)
            {
                MessageBox.Show("Что-то не так!", "Ошибка!");
                return;
            }
        }
    }
}
