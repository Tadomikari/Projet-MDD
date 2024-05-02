using MySql.Data.MySqlClient;
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
    /// Logique d'interaction pour Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        private MySqlConnection connexion;
        private MySqlDataAdapter adaptateur;
        string CL;
        public Client(string courriel)
        {
            InitializeComponent();
            this.CL = courriel;
            try
            {
                string credentials = "SERVER=localhost;PORT=3306;DATABASE=Fleuriste;UID=root;PASSWORD=Nestor05!";
                connexion = new MySqlConnection(credentials);
                connexion.Open();
                string requete = "SELECT idCommande,Etat_Commande,Adresse_Livraison,Message,Date_Commande,Date_Livraison,Nom as Magasin,nomC as Nom_Client FROM bon_de_commande NATURAL JOIN client,magasin WHERE bon_de_commande.idMagasin = magasin.idMagasin AND Courriel = @c";
                adaptateur = new MySqlDataAdapter(requete, connexion);
                adaptateur.SelectCommand.Parameters.AddWithValue("@c", CL);
                DataTable Table = new DataTable();
                adaptateur.Fill(Table);
                Commandes.ItemsSource = Table.DefaultView;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.ToString());
            }
        }

        private void PasserCommande_Click(object sender, RoutedEventArgs e) //Affiche la fenêtre pour ajouter une commande
        {
            Nouvelle_Commande Ajouter = new Nouvelle_Commande();
            Ajouter.ShowDialog();
            DataTable Table = new DataTable();
            adaptateur.Fill(Table);
            Commandes.ItemsSource = Table.DefaultView;
        }
    }
}
