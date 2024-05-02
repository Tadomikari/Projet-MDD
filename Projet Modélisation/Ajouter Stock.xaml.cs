using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Utilities;
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
using System.Windows.Shapes;

namespace Projet_Modélisation
{
    /// <summary>
    /// Logique d'interaction pour Ajouter_Stock.xaml
    /// </summary>
    public partial class Ajouter_Stock : Window
    {
        private MySqlConnection connexion;

        public Ajouter_Stock()
        {
            InitializeComponent();
            try
            {
                string credentials = "SERVER=localhost;PORT=3306;DATABASE=Fleuriste;UID=root;PASSWORD=Nestor05!";
                connexion = new MySqlConnection(credentials);
                connexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.ToString());
            }
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            string magasin = ChoixMagasin.Text;
            string produit = Produits.Text;
            string quantite = Quantite.Text;
            MySqlCommand findid = new MySqlCommand("SELECT idMagasin FROM magasin WHERE nom=@nom", connexion);
            findid.Parameters.AddWithValue("@nom", magasin);
            int idM = Convert.ToInt32(findid.ExecuteScalar());
            findid.Parameters.Clear();
            findid.CommandText = "SELECT NumeroP FROM produit WHERE Description=@prod";
            findid.Parameters.AddWithValue("@prod", produit);
            int idP = Convert.ToInt32(findid.ExecuteScalar());
            findid.Parameters.Clear();
            findid.CommandText = "SELECT quantite FROM possede WHERE NumeroP=@idP AND idMagasin=@idM";
            findid.Parameters.AddWithValue("@idP", idP);
            findid.Parameters.AddWithValue("@idM", idM);
            int qtt = Convert.ToInt32(findid.ExecuteScalar());
            findid.Parameters.Clear();
            if(Convert.ToInt32(quantite) + qtt<0)
            {
                MessageBox.Show("Stock négatif !");
                return;
            }
            MySqlCommand ajout = new MySqlCommand("UPDATE possede SET quantite=@quantite WHERE NumeroP=@idPr AND idMagasin=@idMa", connexion);
            ajout.Parameters.AddWithValue("@idPr", idP);
            ajout.Parameters.AddWithValue("@idMa", idM);
            ajout.Parameters.AddWithValue("@quantite",Convert.ToInt32(quantite)+qtt);
            try
            {
                int ajouter = ajout.ExecuteNonQuery();
                if (ajouter > 0)
                {
                    MessageBox.Show("Stock modifié !");
                    Close();
                }
            }
            catch (MySqlException i)
            {
                MessageBox.Show(i.ToString());
            }
        }

        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
