using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Projet_Modélisation
{
    public partial class Nouveau_Client : Window
    {
        private MySqlConnection connexion;

        public Nouveau_Client()
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
            string nom = Nom.Text;
            string prenom = Prenom.Text;
            string telephone = Telephone.Text;
            string courriel = Courriel.Text;
            string motdepasse = MotDePasse.Password;
            string adresse = Adresse.Text;
            string carte = Carte.Text;
            MySqlCommand verification = new MySqlCommand("SELECT COUNT(*) FROM Client WHERE courriel=@courriel", connexion);
            verification.Parameters.AddWithValue("@courriel", courriel);
            int resultat = Convert.ToInt32(verification.ExecuteScalar());
            if (resultat > 0)
            {
                Erreur.Text = "Un client existe déjà avec la même adresse mail !";
                Erreur.Visibility = Visibility.Visible;
                return;
            }
            MySqlCommand verifnumC = new MySqlCommand("SELECT COUNT(*) FROM Client", connexion);
            int ValeurnumC = Convert.ToInt32(verifnumC.ExecuteScalar())+1;
            string fidelité = "aucun";
            MySqlCommand ajout = new MySqlCommand("INSERT INTO Client VALUES (@numC, @telephone, @courriel, @nom, @prenom,  @motdepasse, @adresse, @carte, @fidelité)", connexion);
            ajout.Parameters.AddWithValue("@numC",ValeurnumC);
            ajout.Parameters.AddWithValue("@nom", nom);
            ajout.Parameters.AddWithValue("@prenom", prenom);
            ajout.Parameters.AddWithValue("@telephone", telephone);
            ajout.Parameters.AddWithValue("@courriel", courriel);
            ajout.Parameters.AddWithValue("@motdepasse", motdepasse);  //Pour éviter les injections SQL
            ajout.Parameters.AddWithValue("@adresse", adresse);
            ajout.Parameters.AddWithValue("@carte", carte);
            ajout.Parameters.AddWithValue("@fidelité", fidelité);
            try
            {
                int ajouter = ajout.ExecuteNonQuery();
                if (ajouter > 0)
                {
                    MessageBox.Show("compte crée !");
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
