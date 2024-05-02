using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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
    /// Logique d'interaction pour Nouvelle_Commande.xaml
    /// </summary>
    public partial class Nouvelle_Commande : Window
    {
        private MySqlConnection connexion;
        int idbouquet;
        int prixProd;
        public Nouvelle_Commande()
        {
            InitializeComponent();
            try
            {
                string credentials = "SERVER=localhost;PORT=3306;DATABASE=Fleuriste;UID=root;PASSWORD=Nestor05!";
                connexion = new MySqlConnection(credentials);
                connexion.Open();
                MySqlCommand verification = new MySqlCommand("SELECT COUNT(*) FROM Bouquet", connexion);
                idbouquet = Convert.ToInt32(verification.ExecuteScalar())+1;
                verification.Parameters.Clear();
                verification.Dispose();
                MySqlCommand add = new MySqlCommand("INSERT INTO bouquet VALUES(@id,@d,@p)", connexion);
                add.Parameters.AddWithValue("@id", idbouquet);
                add.Parameters.AddWithValue("@d", "Perso");
                add.Parameters.AddWithValue("@p", 0);
                add.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.ToString());
            }
        }
        

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            string courriel = Courriel.Text;
            string Magasin = ChoixMagasin.Text;
            string message = Message.Text;
            string adresse = Adresse.Text;
            string date = Date.Text;
            int prix = 0;
            string description = Description.Text; //On récupère les informations depuis le formulaire
            string bouquetB = Bouquet.Text;
            MySqlCommand v = new MySqlCommand("SELECT COUNT(*) FROM Client WHERE courriel=@courriel", connexion);
            v.Parameters.AddWithValue("@courriel", courriel);
            int resultat = Convert.ToInt32(v.ExecuteScalar());
            v.Dispose();
            if (resultat <= 0)
            {
                MessageBox.Show("Courriel inexistant ! ");
                return;
            }
            MySqlCommand verification = new MySqlCommand("SELECT COUNT(*) FROM Bon_de_Commande", connexion);
            int idcommande = Convert.ToInt32(verification.ExecuteScalar())+1; 
            verification.Parameters.Clear();
            verification.Dispose();
            verification.CommandText = "SELECT COUNT(*) FROM Bouquet";
            int idBouquet = Convert.ToInt32(verification.ExecuteScalar());
            verification.Parameters.Clear();
            verification.Dispose();
            MySqlCommand findid = new MySqlCommand("SELECT idMagasin FROM magasin WHERE nom=@nom", connexion);
            findid.Parameters.AddWithValue("@nom", Magasin);
            int idM = Convert.ToInt32(findid.ExecuteScalar());
            findid.Parameters.Clear();
            MySqlCommand findidC = new MySqlCommand("SELECT NumeroC FROM client WHERE courriel=@courriel", connexion);
            findidC.Parameters.AddWithValue("@courriel", courriel);
            int idC = Convert.ToInt32(findidC.ExecuteScalar());
            findid.Parameters.Clear();
            if(PrixPerso.Text.Length>0) prix = Convert.ToInt32(PrixPerso.Text);
            verification.CommandText = "INSERT INTO Bouquet VALUES (@id, @desc, @prix)"; //Si bouquet sur demande, on l'insère dans la BDD
            verification.Parameters.AddWithValue("@id", idBouquet + 1);
            verification.Parameters.AddWithValue("@desc", description);
            verification.Parameters.AddWithValue("@prix",prix);
            verification.ExecuteNonQuery();
            if (bouquetB.Length> 0)
            {
                verification.CommandText = "SELECT NumeroBS FROM Bouquet_Standard WHERE nom=@nb"; 
                verification.Parameters.AddWithValue("@nb", bouquetB);
                int idBS = Convert.ToInt32(verification.ExecuteScalar());
                verification.Parameters.Clear();
                verification.Dispose();
                MySqlCommand ajout = new MySqlCommand("INSERT INTO Contient_S VALUES(@numeroBS,@idcomm,1)", connexion); //Ajout bouquet du magasin
                ajout.Parameters.AddWithValue("@numeroBS", idBS);
                ajout.Parameters.AddWithValue("@idcomm", idcommande);
                verification.CommandText = "SELECT prix FROM Bouquet_Standard WHERE nom=@name"; 
                verification.Parameters.AddWithValue("@name", bouquetB);
                prix += Convert.ToInt32(verification.ExecuteScalar());
                verification.Parameters.Clear();
                verification.Dispose();
            }
            verification.Parameters.Clear();
            verification.Dispose();
            verification.CommandText = "INSERT INTO Bon_de_commande VALUES (@id, @etat, @adresse,@message,@dateC,@DateL,@numC,@idM)"; //Finalement on ajoute la commande
            verification.Parameters.AddWithValue("@id",idcommande);
            verification.Parameters.AddWithValue("@etat","CC");
            verification.Parameters.AddWithValue("@adresse", adresse);
            verification.Parameters.AddWithValue("@message", message);
            verification.Parameters.AddWithValue("@dateC", DateTime.Now);
            verification.Parameters.AddWithValue("@dateL", date);
            verification.Parameters.AddWithValue("@numC",idC);
            verification.Parameters.AddWithValue("@idM", idM);
            MySqlCommand fid = new MySqlCommand("SELECT Fidelite FROM client WHERE NumeroC=@c", connexion); //Fidélité
            fid.Parameters.AddWithValue("@c", idC);
            string fidelite = Convert.ToString(fid.ExecuteScalar());
            fid.Parameters.Clear();
            try
            {
                verification.ExecuteNonQuery();
                if(fidelite=="or") MessageBox.Show("Commande passée !\nAvec le status Or,le prix est de :" + 0.85*prix);
                else if(fidelite=="bronze") MessageBox.Show("Commande passée !\nAvec le status Bronze,le prix est de :" + 0.95 * prix);
                else MessageBox.Show("Commande passée ! Le prix est de :" + prix);
                Close();
            }
            catch (MySqlException i)
            {
                Console.WriteLine(i.ToString());
            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            int qtt = Convert.ToInt32(Quantité.Text);
            string produit = Produits.Text;
            MySqlCommand verif = new MySqlCommand("SELECT NumeroP FROM produit WHERE description=@n", connexion); //Vérifie le numéro de Produit
            verif.Parameters.AddWithValue("@n", produit);
            int idP = Convert.ToInt32(verif.ExecuteScalar());
            MySqlCommand mag= new MySqlCommand("SELECT idMagasin FROM magasin WHERE nom=@nom", connexion); //Vérifie le numéro du magasin dans lequel on commande
            mag.Parameters.AddWithValue("@nom", ChoixMagasin.Text);
            int idM = Convert.ToInt32(mag.ExecuteScalar());
            MySqlCommand st = new MySqlCommand("SELECT quantite FROM possede WHERE NumeroP=@idP AND idMagasin=@idM",connexion); //vérifie les stocks restants
            st.Parameters.AddWithValue("@idP", idP);
            st.Parameters.AddWithValue("@idM", idM);
            int stockrestant = Convert.ToInt32(st.ExecuteScalar());
            if (stockrestant-qtt < 0)
            {
                MessageBox.Show("Stock négatif !");
                return;
            }
            st.Parameters.AddWithValue("@n", produit);
            int Stockrestant = Convert.ToInt32(verif.ExecuteScalar());
            MySqlCommand ajout = new MySqlCommand("INSERT INTO Est_compose VALUES(@numP,@numB,@qtt)", connexion); //Insère le tuple
            ajout.Parameters.AddWithValue("@numP", idP);
            ajout.Parameters.AddWithValue("@numB", idbouquet);
            ajout.Parameters.AddWithValue("@qtt", qtt);
            ajout.ExecuteNonQuery();
            ajout.Parameters.Clear();
            MySqlCommand prix = new MySqlCommand("SELECT prix FROM produit WHERE NumeroP=@p", connexion); //Ajoute le prix au panier
            prix.Parameters.AddWithValue("@p", idP);
            prixProd += Convert.ToInt32(prix.ExecuteScalar());
            prix.Dispose();
            MessageBox.Show("produit ajouté !");

        } //Ajoute les produits à l'éracn dans un bouquet, que l'on ajoutera à la commande

        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

