using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MySql.Data.MySqlClient;

namespace Projet_Modélisation
{
    public partial class MainWindow : Window
    {
        private MySqlConnection connexion;
        private MySqlDataAdapter adaptateur;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                string credentials = "SERVER=localhost;PORT=3306;DATABASE=Fleuriste;UID=root;PASSWORD=Nestor05!";
                connexion = new MySqlConnection(credentials);
                connexion.Open();
                string requete = "SELECT * FROM Client";
                adaptateur = new MySqlDataAdapter(requete, connexion);
                DataTable TableClient = new DataTable();
                adaptateur.Fill(TableClient);
                TabClients.ItemsSource = TableClient.DefaultView;
                Login accueil = new Login();
                accueil.ShowDialog();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.ToString());
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem selectedItem)
            {
                switch (selectedItem.Header.ToString())
                {
                    case "Clients":
                        string requeteC = "SELECT numeroC,numero_Telephone as Téléphone,courriel,NomC as Nom,prenom,adresse_de_facturation,fidelite FROM Client";
                        adaptateur.SelectCommand = new MySqlCommand(requeteC, connexion);
                        DataTable TableClient = new DataTable();
                        adaptateur.Fill(TableClient);
                        TabClients.ItemsSource = TableClient.DefaultView;
                        break;
                    case "Produits":
                        string requeteP = "SELECT * FROM Produit";
                        adaptateur.SelectCommand = new MySqlCommand(requeteP, connexion);
                        DataTable TableProduit = new DataTable();
                        adaptateur.Fill(TableProduit);
                        TabProduits.ItemsSource = TableProduit.DefaultView;
                        break;
                    case "Commandes":
                        //Requête Auto-Jointure :
                        string requeteCo = "SELECT idCommande,Etat_Commande,Adresse_Livraison,Message,Date_Commande,Date_Livraison,Nom as Magasin,nomC as Nom_Client FROM bon_de_commande NATURAL JOIN client,magasin WHERE bon_de_commande.idMagasin =magasin.idMagasin;";
                        adaptateur.SelectCommand = new MySqlCommand(requeteCo, connexion);
                        DataTable TableCommande = new DataTable();
                        adaptateur.Fill(TableCommande);
                        TabCommandes.ItemsSource = TableCommande.DefaultView;
                        break;
                    case "Stock":
                        //Requête Auto-Jointure :
                        string requeteS = "SELECT description,Nom,quantite FROM Possede NATURAL JOIN Produit,magasin WHERE magasin.idMagasin=Possede.idMagasin;";
                        adaptateur.SelectCommand = new MySqlCommand(requeteS, connexion);
                        DataTable TableStock = new DataTable();
                        adaptateur.Fill(TableStock);
                        TabStock.ItemsSource = TableStock.DefaultView;
                        break;
                    case "Statistiques":
                        break;
                }
            }
        }  //Choix du tableau

        private void NouveauClient_Click(object sender, RoutedEventArgs e)
        {
            Nouveau_Client Ajouter = new Nouveau_Client();
            Ajouter.ShowDialog();
            DataTable TableClient = new DataTable();
            adaptateur.Fill(TableClient);
            TabClients.ItemsSource = TableClient.DefaultView;
        } //Affiche la fenêtre pour ajouter un client
        private void NouveauProduit_Click(object sender, RoutedEventArgs e)
        {
            Nouveau_Produit Ajouter = new Nouveau_Produit();
            Ajouter.ShowDialog();
            DataTable Tableproduit = new DataTable();
            adaptateur.Fill(Tableproduit);
            TabProduits.ItemsSource = Tableproduit.DefaultView;
        } //Affiche la fenêtre pour ajouter un produit
        private void NouvelleCommande_Click(object sender, RoutedEventArgs e) //Affiche la fenêtre pour ajouter une commande
        {
            Nouvelle_Commande Ajouter = new Nouvelle_Commande();
            Ajouter.ShowDialog();
            DataTable TableCommande = new DataTable();
            adaptateur.Fill(TableCommande);
            TabCommandes.ItemsSource = TableCommande.DefaultView;
        }
        private void AjouterStock_Click(object sender, RoutedEventArgs e) //Affiche la fenêtre pour ajouter une commande
        {
            Ajouter_Stock Ajouter = new Ajouter_Stock();
            Ajouter.ShowDialog();
            DataTable TableStock = new DataTable();
            adaptateur.Fill(TableStock);
            TabStock.ItemsSource = TableStock.DefaultView;
        }
        void Chercher_Click(object sender, RoutedEventArgs e)
        {
                string requeteC = "SELECT numeroC,numero_Telephone as Téléphone,courriel,NomC as Nom,prenom,adresse_de_facturation,fidelite FROM Client WHERE NomC LIKE @nom";
                MySqlCommand requete = new MySqlCommand(requeteC, connexion);
                requete.Parameters.AddWithValue("@nom", "%"+Nom.Text+"%");
                adaptateur.SelectCommand = requete;
                DataTable TableClient = new DataTable();
                adaptateur.Fill(TableClient);
                TabClients.ItemsSource = TableClient.DefaultView;
        } //Affiche les clients en cherchant un nom de famille
        void Historique_Click(object sender, RoutedEventArgs e)
        {
            string requeteC = "SELECT numeroC FROM Client WHERE NomC=@nom";
            MySqlCommand requeteid = new MySqlCommand(requeteC, connexion);
            requeteid.Parameters.AddWithValue("@nom",Nom.Text);
            int id = Convert.ToInt32(requeteid.ExecuteScalar());
            MySqlCommand requete = new MySqlCommand("SELECT idCommande,Etat_Commande,Adresse_Livraison,Message,Date_Commande,Date_Livraison,Nom as Magasin,nomC as Nom_Client FROM bon_de_commande NATURAL JOIN client,magasin WHERE bon_de_commande.idMagasin = magasin.idMagasin AND NumeroC = @nC", connexion);
            requete.Parameters.AddWithValue("@nC", id);
            adaptateur.SelectCommand = requete;
            DataTable TableClient = new DataTable();
            adaptateur.Fill(TableClient);
            TabClients.ItemsSource = TableClient.DefaultView;
        } //Affiche l'historique d'un client avec son nom de famille
        void ChangerEtat_Click(object sender, RoutedEventArgs e)
        {
            int idC = Convert.ToInt32(idcommande.Text);
            string Etat = etat.Text;
            string requeteC = "UPDATE bon_de_commande SET Etat_Commande=@e WHERE idCommande=@id";
            if(Etat!="CC"&& Etat != "VINV" && Etat != "CPAV" && Etat != "CAL" && Etat != "CL")
            {
                MessageBox.Show("Etat invalide !");
                return;
            }
            MySqlCommand requeteid = new MySqlCommand(requeteC, connexion);
            requeteid.Parameters.AddWithValue("@e", Etat);
            requeteid.Parameters.AddWithValue("@id", idC);
            requeteid.ExecuteNonQuery();
            string requeteCo = "SELECT idCommande,Etat_Commande,Adresse_Livraison,Message,Date_Commande,Date_Livraison,Nom as Magasin,nomC as Nom_Client FROM bon_de_commande NATURAL JOIN client,magasin WHERE bon_de_commande.idMagasin =magasin.idMagasin;";
            adaptateur.SelectCommand = new MySqlCommand(requeteCo, connexion);
            DataTable TableCommande = new DataTable();
            adaptateur.Fill(TableCommande);
            TabCommandes.ItemsSource = TableCommande.DefaultView;
        } //Change l'état d'une commande
        void Alertes_Click(object sender, RoutedEventArgs e)
        {
            string requeteC = "SELECT description,Nom,quantite FROM Possede NATURAL JOIN Produit,magasin WHERE magasin.idMagasin=Possede.idMagasin AND quantite<5";
            MySqlCommand requete = new MySqlCommand(requeteC, connexion);
            adaptateur.SelectCommand = requete;
            DataTable Table = new DataTable();
            adaptateur.Fill(Table);
            TabStock.ItemsSource = Table.DefaultView;
        } //Affiche les alertes

        //Statistiques :

        void Meilleur_Click(object sender, RoutedEventArgs e)
        {
            string requeteC = "SELECT client.NomC, COUNT(com.NumeroC) AS nombre_de_commandes FROM Client LEFT JOIN Bon_de_Commande com ON client.NumeroC = com.NumeroC GROUP BY client.NumeroC ORDER BY COUNT(com.NumeroC) DESC";
            MySqlCommand requete = new MySqlCommand(requeteC, connexion);
            adaptateur.SelectCommand = requete;
            DataTable Table = new DataTable();
            adaptateur.Fill(Table);
            TabStatistiques.ItemsSource = Table.DefaultView;
        } //Meilleurs Clients

        void Vendus_Click(object sender, RoutedEventArgs e)
        {
            string requeteC = "SELECT Bouquet.NumeroB, Bouquet.Description, SUM(Contient.Quantite) AS Total_Vendu, COUNT(*) AS Nb_Commandes, SUM(Contient.Quantite* Bouquet.prix) AS Chiffre_d_affaire, Magasin.Nom AS Nom_Magasin, Magasin.Adresse AS Adresse_Magasin FROM Bouquet, Contient, Bon_de_Commande, Magasin WHERE Bouquet.NumeroB = Contient.NumeroB AND Contient.idCommande = Bon_de_Commande.idCommande AND Bon_de_Commande.idMagasin = Magasin.idMagasin GROUP BY Bouquet.NumeroB, Magasin.idMagasin ORDER BY Total_Vendu DESC;";
            MySqlCommand requete = new MySqlCommand(requeteC, connexion);
            adaptateur.SelectCommand = requete;
            DataTable Table = new DataTable();
            adaptateur.Fill(Table);
            TabStatistiques.ItemsSource = Table.DefaultView;
        } //Bouquets les plus vendus

        void Nombre_Click(object sender, RoutedEventArgs e)
        {
            string requeteC = "SELECT c.NumeroC, c.NomC, c.Prenom, (SELECT SUM(co.Quantite) FROM Contient co JOIN Bon_de_Commande bc ON co.idCommande = bc.idCommande WHERE bc.NumeroC = c.NumeroC) as Total_bouquets_vendus FROM Client c;";
            MySqlCommand requete = new MySqlCommand(requeteC, connexion);
            adaptateur.SelectCommand = requete;
            DataTable Table = new DataTable();
            adaptateur.Fill(Table);
            TabStatistiques.ItemsSource = Table.DefaultView;
        } //Nombre de bouquets vendus à chaque client

        void Filtre_Click(object sender, RoutedEventArgs e)
        {
            string requeteC = "SELECT nomC, courriel FROM client WHERE nomC LIKE 'P%' UNION SELECT nomC, courriel FROM client WHERE courriel LIKE '%@gmail.com'";
            MySqlCommand requete = new MySqlCommand(requeteC, connexion);
            adaptateur.SelectCommand = requete;
            DataTable Table = new DataTable();
            adaptateur.Fill(Table);
            TabStatistiques.ItemsSource = Table.DefaultView;
        } // @gmail + initiale est un "P"
    }
}
