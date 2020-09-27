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
using System.Numerics;

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
            BigInteger p, q;
            try
            {
                string[] pq = pAndQTextBox.Text.Split(' ');
                p = BigInteger.Parse(pq[0]);
                q = BigInteger.Parse(pq[1]);

                if(p < 0 || q < 0 || p == q || p*q <= 257 || !p.IsPrime() || !q.IsPrime())
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Некорректные значения!p и q указываются через пробел\np и q должны быть простыми числами, причём p ≠ q и p × q > 257.", "Ошибка!");
                return;
            }

            encryptor = new RSAEncryptor(p, q);
            publicKeyTextBox.Text = $"{encryptor.PublicKey.First} {encryptor.PublicKey.Second}";
            privateKeyTextBox.Text = $"{encryptor.PrivateKey.First} {encryptor.PrivateKey.Second}";
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = messageTextBox.Text;
                StringBuilder encryptedMessage = new StringBuilder();

                string[] key = keyTextBox.Text.Split(' ');
                Pair<BigInteger, BigInteger> k = new Pair<BigInteger, BigInteger>(BigInteger.Parse(key[0]), BigInteger.Parse(key[1]));

                foreach (var b in encryptor.Encrypt(k, message))
                {
                    encryptedMessage.Append(b.ToString()+" ");
                }
                encryptedMessage1TextBox.Text = encryptedMessage.ToString();
            }
            catch
            {
                MessageBox.Show("Что-то не так!", "Ошибка!");
                return;
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Эта шифравалка использует упрощённый вариант шифрования RSA. Сделана Неевиным Кириллом. Всё.", "Ты похоже хз что делать");
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] vals = encryptedTextBox.Text.Split(' ');
                BigInteger[] nums = new BigInteger[vals.Length];
                for(int i = 0; i < vals.Length; i++)
                {
                    if (vals[i] != "" && vals[i] != null)
                        nums[i] = BigInteger.Parse(vals[i]);
                }

                encryptedMessageTextBox.Text = encryptor.Decrypt(nums);

                vals[0] = "";
            }
            catch(Exception exc)
            {
                MessageBox.Show("Что-то не так!" + exc.Message, "Ошибка!");
                return;
            }
        }
    }
}
