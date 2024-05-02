using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private MySqlConnection connexion;
        public Login()
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

        private void Compte_Click(object sender, RoutedEventArgs e)
        {
            Nouveau_Client Ajouter = new Nouveau_Client();
            Ajouter.ShowDialog();
        }

        private void Connecter_Click(object sender, RoutedEventArgs e)
        {
            string Courriel = courriel.Text;
            string motdepasse = mdp.Text;
            if(Courriel=="bellefleur@gmail.com" && motdepasse=="secret")
            {
                Close();
            }
            else
            {
                string requeteC = "SELECT COUNT(*) FROM Client WHERE Courriel=@c AND Mot_de_passe=@mdp";
                MySqlCommand requete = new MySqlCommand(requeteC, connexion);
                requete.Parameters.AddWithValue("@c", Courriel);
                requete.Parameters.AddWithValue("@mdp",motdepasse);
                int resultat = Convert.ToInt32(requete.ExecuteScalar());
                if (resultat <= 0)
                {
                    MessageBox.Show("Ce compte n'existe pas ! ");
                    return;
                }
                Client pageC = new Client(Courriel);
                pageC.ShowDialog();
                Environment.Exit(0);
            }
        }
    }
}
