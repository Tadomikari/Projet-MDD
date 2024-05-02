using MySql.Data.MySqlClient;
using Mysqlx;
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
    public partial class Nouveau_Produit : Window
    {
        private MySqlConnection connexion;
        public Nouveau_Produit()
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
            string type = ChoixType.Text;
            string prix = Prix.Text;
            MySqlCommand verifnumP = new MySqlCommand("SELECT COUNT(*) FROM Produit", connexion);
            int ValeurnumP = Convert.ToInt32(verifnumP.ExecuteScalar()) + 1;
            MySqlCommand ajout = new MySqlCommand("INSERT INTO Produit VALUES (@numP, @type,  @prix, @description)", connexion);
            ajout.Parameters.AddWithValue("@numP", ValeurnumP);
            ajout.Parameters.AddWithValue("@description", nom);
            ajout.Parameters.AddWithValue("@type", type);   // Pour éviter les injections SQL
            ajout.Parameters.AddWithValue("@prix", prix);
            try
            {
                int ajouter = ajout.ExecuteNonQuery();
                if (ajouter > 0)
                {
                    MessageBox.Show("produit ajouté !");
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
