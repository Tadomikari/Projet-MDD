using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Reflection.PortableExecutable;

namespace Projet_Modelisation 
{
    internal class Program
    {


        static void Main(string[] args)
        {
            MySqlCommand demande; // On crée la demande que l'on va envoyer au server local MySQL
            MySqlDataReader reader; // Permet de lire la réponse du serveur

            MySqlConnection Connexion; // On crée une variable connexion Mysql
            try
            {
                string identifiants = "SERVER=localhost;PORT=3306;DATABASE=Projet;UID=root;PASSWORD=Nestor05!";
                Connexion = new MySqlConnection(identifiants); //On définit la connexion grâce aux identifiants
                Connexion.Open(); // On ouvre la connexion
                demande = Connexion.CreateCommand();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Qui êtes-vous ?\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                string rep = "";
                while (rep!="1" && rep !="2")
                {
                    Console.WriteLine("1.Je suis un client\n2.Je suis M.BelleFleur (administrateur)\n");
                    rep = Console.ReadLine();
                }
                if (rep == "1")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Que voulez-vous faire ?\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    string compte = "";
                    while (compte != "3")
                    {
                        Console.WriteLine("1.Me connecter\n2.Creer un compte\n3.Quitter");
                        compte = Console.ReadLine();
                        if (compte == "1")
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Connectez-vous avec vos identifiants :\n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Courriel :\n");
                            string courriel = Console.ReadLine();
                            Console.WriteLine("\nMot de passe :\n");
                            string motdepasse = Console.ReadLine();
                            while (Login(courriel, motdepasse) == false)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Connectez-vous avec vos identifiants :\n\n");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Courriel :\n");
                                courriel = Console.ReadLine();
                                Console.WriteLine("\nMot de passe :\n");
                                motdepasse = Console.ReadLine();
                            }
                            bool exit = false;
                            while (exit == false)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nQue voulez-vous faire ?\n");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("1.Passer une commande\n2.Voir mes commandes\n3.Afficher les magasins\n4.Quitter le logiciel\n");
                                int res = Convert.ToInt32(Console.ReadLine());
                                switch (res)
                                {
                                    case 1:
                                        Console.Clear();
                                        PasserCommande();
                                        break;
                                    case 2:
                                        Console.Clear();
                                        AfficherMesCommandes(courriel);
                                        break;
                                    case 3:
                                        Console.Clear();
                                        AfficherMagasins();
                                        break;
                                    case 4:
                                        exit = true; //Pour sortir du menu
                                        break;
                                    default:
                                        Console.WriteLine("Erreur dans la saisie !");
                                        break;

                                }
                            }


                        }
                        else if (compte == "2")
                        {
                            Console.Clear();
                            CreerClient();
                            Console.Clear();
                            Console.WriteLine("\nVotre compte à bien été crée, vous pouvez maintenant vous connecter");
                        }
                    }
                    
                }
                else if (rep == "2")
                {
                    bool sortie = false;
                    while (sortie == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Bonjour M.Bellefleur, que voulez-vous faire aujourd'hui ?\n\n");
                        Console.WriteLine("1.Gérer les clients\n2.Gérer les produits\n3.Gérer les commandes\n4.Voir les statistiques\n5.Vérifier les stocks\n6.Quitter le logiciel\n");
                        int res = Convert.ToInt32(Console.ReadLine());

                        switch (res)
                        {
                            case 1: //Gérer Client : 
                                Console.Clear();
                                Console.WriteLine("CLIENTS :\n\n");
                                bool client = false;
                                while (client == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Que voulez-vous faire ?\n");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("1.Afficher les clients\n2.Ajouter un client\n3.Historique des commandes d'un client\n4.Filtrer les clients par nom\n5.Retour au menu\n");
                                    int repClient = Convert.ToInt32(Console.ReadLine());
                                    switch (repClient)
                                    {
                                        case 1:
                                            Console.Clear();
                                            AfficherClients();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            CreerClient();
                                            break;
                                        case 3:
                                            Console.Clear();
                                            historiqueClient();
                                            break;
                                        case 4:
                                            Console.Clear();
                                            Console.WriteLine("\nQuel est le nom du client que vous cherchez ?");
                                            string reponse = Console.ReadLine();
                                            FiltreClientNom(reponse);
                                            break;
                                        case 5:
                                            client = true; //Pour sortir du menu Client
                                            break;
                                        default:
                                            Console.WriteLine("Erreur dans la saisie !");
                                            break;
                                    }
                                }
                                break;
                            case 2: //Gérer Produits :
                                Console.Clear();
                                Console.WriteLine("PRODUITS :\n\n");
                                bool produits = false;
                                while (produits == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Que voulez-vous faire ?\n");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("1.Afficher les produits\n2.Ajouter un produit\n3.Retirer un produit\n4.Retour au menu\n");
                                    int repClient = Convert.ToInt32(Console.ReadLine());
                                    switch (repClient)
                                    {
                                        case 1:
                                            Console.Clear();
                                            AfficherProduits();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            Ajouterproduit();
                                            break;
                                        case 3:
                                            Console.Clear();
                                            SupprimerProduit();
                                            break;
                                        case 4:
                                            produits = true; // Pour sortir du menu Produit
                                            break;
                                        default:
                                            Console.WriteLine("Erreur dans la saisie !");
                                            break;
                                    }
                                }
                                break;

                            case 3: //Gérer Commandes : 
                                Console.Clear();
                                Console.WriteLine("COMMANDES :\n\n");
                                bool commandes = false;
                                while (commandes == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Que voulez-vous faire ?\n");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("1.Afficher les commandes\n2.Ajouter une commande\n3.Changer l'état d'une commande\n4.Filtrer les commandes par date de livraison\n5.Retour au menu\n");
                                    int repCommande = Convert.ToInt32(Console.ReadLine());
                                    switch (repCommande)
                                    {
                                        case 1:
                                            Console.Clear();
                                            AfficherCommandes();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            PasserCommande(true); //Car on est administrateur donc valeur true pour différencier avec les commandes clients
                                            break;
                                        case 3:
                                            Console.Clear();
                                            ChangerEtat();
                                            break;
                                        case 4:
                                            Console.Clear();
                                            Console.WriteLine("\nEntrez une date : (AAAA-MM-JJ)\n");
                                            string reponse = Console.ReadLine();
                                            FiltreCommandesDate(reponse);
                                            break;
                                        case 5:
                                            commandes = true;
                                            break;
                                        default:
                                            Console.WriteLine("Erreur dans la saisie !");
                                            break;

                                    }
                                }
                                break;
                            case 4: //Statistiques :
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nVoici différentes statistiques en temps réel :\n\n");
                                Console.ForegroundColor = ConsoleColor.White;
                                AfficherStatistiques();
                                break;
                            case 5: //Gérer Stocks :
                                Console.Clear();
                                Console.WriteLine("STOCKS :\n\n");
                                bool stocks = false;
                                while (stocks == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Que voulez-vous faire ?\n");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("1.Afficher les stocks\n2.Ajouter des stocks\n3.Retirer des stocks\n4.Voir les alertes de stock\n5.Retour au menu\n");
                                    int repClient = Convert.ToInt32(Console.ReadLine());
                                    switch (repClient)
                                    {
                                        case 1:
                                            Console.Clear();
                                            AfficherStocks();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            AjouterStocks();
                                            break;
                                        case 3:
                                            Console.Clear();
                                            retirerStocks();
                                            break;
                                        case 4:
                                            Console.Clear();
                                            AlertesStocks();
                                            break;
                                        case 5:
                                            stocks = true; // Pour sortir du menu Stocks
                                            break;
                                        default:
                                            Console.WriteLine("Erreur dans la saisie !");
                                            break;
                                    }
                                }
                                break;
                            case 6:
                                sortie = true;
                                break;
                            default:
                                Console.WriteLine("Erreur dans la saisie !");
                                break;
                        }
                    }
                    Connexion.Close();
                }
               
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : "+e.ToString()); // Si erreur MySQL, on l'affiche
            }

            //Fonctions Clients :

            void AfficherClients()
            {
                Console.WriteLine("Voici la liste des clients :\n");
                demande.CommandText = "SELECT nom,prenom,courriel,adresse_facturation,type FROM Client"; // La demande mySQL
                reader = demande.ExecuteReader();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                    }
                    Console.WriteLine("\n");
                }
                demande.Parameters.Clear();
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard
            } //Affiche tous les clients
            void FiltreClientNom(string nom)
            {
                Console.WriteLine("Voici la liste des clients se nommant : "+nom+"\n");
                demande.CommandText = "SELECT nom,prenom,courriel,adresse_facturation,type FROM Client WHERE nom=@nom"; // La demande mySQL
                demande.Parameters.AddWithValue("@nom", nom);
                reader = demande.ExecuteReader();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                    }
                    Console.WriteLine("\n");
                }
                demande.Parameters.Clear();
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard
            } //Affiche tous les clients qui ont le nom "string nom"
            void CreerClient()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entrez les information du nouveau client :\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nNom : \n");
                string nom = Console.ReadLine();
                Console.WriteLine("\nPrénom : \n");
                string prenom = Console.ReadLine();
                Console.WriteLine("\nNuméro de Téléphone : \n");  //On demande toutes les informations du nouveau client
                string telephone = Console.ReadLine();
                Console.WriteLine("\nCourriel : \n");
                string courriel = Console.ReadLine();
                Console.WriteLine("\nMot de Passe : \n");
                string motdepasse = Console.ReadLine();
                Console.WriteLine("\nAdresse : \n");
                string adresse = Console.ReadLine();
                Console.WriteLine("\nNuméro de carte bancaire : \n");
                string numerocarte = Console.ReadLine();
                demande.CommandText = "SELECT COUNT(*) FROM Client WHERE courriel=@courriel"; //On regarde si le courriel est déja utilisé
                demande.Parameters.AddWithValue("@courriel", courriel); //permet d'utiliser une variable dans la requête mySQL
                int verification = Convert.ToInt32(demande.ExecuteScalar()); // Le nombre d'occurences trouvées par le Count(*)
                demande.Parameters.Clear();
                demande.Dispose();
                if (verification > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ce courriel est déja utilisé !!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    demande.CommandText = "INSERT INTO Client VALUES (@courriel2,  @motdepasse, @nom, @prenom, @telephone, @adresse, @carte, @fidelité)"; //requête pour insérer un tuple
                    demande.Parameters.AddWithValue("@courriel2", courriel);
                    demande.Parameters.AddWithValue("@motdepasse", motdepasse);
                    demande.Parameters.AddWithValue("@nom", nom);
                    demande.Parameters.AddWithValue("@prenom", prenom);
                    demande.Parameters.AddWithValue("@telephone", telephone);
                    demande.Parameters.AddWithValue("@adresse", adresse); //On remplace par les variables
                    demande.Parameters.AddWithValue("@carte", numerocarte);
                    demande.Parameters.AddWithValue("@fidelité", "pas de fidélité");
                    try
                    {
                        int verificationAjout = demande.ExecuteNonQuery(); //retourne le nombre de ligne affectée par la commande, donc si on ajoute un client, il y à 1 ligne qui change
                        if (verificationAjout == 1)
                        {
                            Console.WriteLine("Le client à été ajouté !\n");
                        }
                        demande.Parameters.Clear();
                        demande.Dispose();
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Erreur ajout client : " + e.ToString()); // Si erreur MySQL, on l'affiche
                    }
                }
            } //Crée un nouveau client, en vérifiant que le courriel n'est pas déja utilisé
            void historiqueClient()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Historique des commandes d'un client :\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Veuillez entrer le courriel du client dont vous voulez connaitre l'historique : \n"); //tester avec olivialopez@yahoo.com (2 commandes)
                string courrielclient = Console.ReadLine();
                demande.CommandText = "SELECT COUNT(*) FROM Client WHERE courriel=@courrielclient"; //On regarde si le courriel existe
                demande.Parameters.AddWithValue("@courrielclient", courrielclient);
                int verificationclient = Convert.ToInt32(demande.ExecuteScalar());
                demande.Parameters.Clear();
                demande.Dispose();
                if (verificationclient == 1)
                {
                    Console.WriteLine("\nVoici les commandes passées par le client : \n");
                    demande.CommandText = "SELECT etat,adresse_livraison,message,date_commande,date_livraison FROM commande WHERE courriel=@courriel";
                    demande.Parameters.AddWithValue("@courriel", courrielclient);
                    reader = demande.ExecuteReader();
                    while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetValue(i).ToString());
                            if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                        }
                        Console.WriteLine("\n");
                    }
                    reader.Close();    // On ferme
                    demande.Parameters.Clear();
                    demande.Dispose();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ce courriel est inexistant !!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } //Affiche l'historique des commandes d'un client choisi
            bool Login(string courriel,string mdp)
            {
                bool resultat = false;
                demande.CommandText = "SELECT COUNT(*) FROM client WHERE courriel = @courriel AND mot_de_passe = @mdp";
                demande.Parameters.AddWithValue("@courriel", courriel);
                demande.Parameters.AddWithValue("@mdp", mdp);
                try
                {
                    int verification = Convert.ToInt32(demande.ExecuteScalar()); // Le nombre d'occurences trouvées par le Count(*)
                    demande.Parameters.Clear();
                    demande.Dispose();
                    if (verification == 1)
                    {
                        demande.CommandText = "SELECT prenom FROM client WHERE courriel = @email AND mot_de_passe = @motdepasse";
                        demande.Parameters.AddWithValue("@email", courriel);
                        demande.Parameters.AddWithValue("@motdepasse", mdp);
                        string nom = demande.ExecuteScalar().ToString();
                        resultat = true;
                        Console.Clear();
                        Console.WriteLine("Bienvenue "+nom+" !\n");
                    }
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur login : " + e.ToString()); // Si erreur MySQL, on l'affiche
                }
                return resultat;
            } //vérifie la correspondance courriel/mot de passe

            //Fonctions Produits :

            void AfficherProduits()
            {
                Console.WriteLine("Voici la liste des produits :\n");
                demande.CommandText = "SELECT numero_produit,type,nom,prix FROM produit"; // La demande mySQL
                reader = demande.ExecuteReader();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                        else Console.Write(" euros "); //Pour le prix 
                    }
                    Console.WriteLine("\n");
                }
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Parameters.Clear();
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard
            } //Affiche tous les produits
            void Ajouterproduit()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entrez les information du nouveau produit :\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Nom : \n");
                string nomP = Console.ReadLine();
                string TypeP = "";
                while (TypeP != "fleur" && TypeP != "accessoire")
                {
                    Console.WriteLine("\nType : (fleur ou accessoire)\n"); //On demande toutes les informations du nouveau produit
                    TypeP = Console.ReadLine();
                }
                Console.WriteLine("\nprix : \n");
                double prixP = Convert.ToDouble(Console.ReadLine());
                demande.CommandText = "SELECT COUNT(*) FROM produit"; //On regarde combien de produits existent déja afin d'attribuer le numéro du produit
                int nbproduits = Convert.ToInt32(demande.ExecuteScalar()); // Le nombre d'occurences trouvées par le Count(*)
                demande.Parameters.Clear();
                demande.Dispose();
                demande.CommandText = "INSERT INTO produit VALUES (@numeroproduit, @nom, @type, @prix)"; //requête pour insérer un tuple    
                demande.Parameters.AddWithValue("@numeroproduit", nbproduits + 1);
                demande.Parameters.AddWithValue("@nom", nomP); //On remplace par les variables
                demande.Parameters.AddWithValue("@type", TypeP);
                demande.Parameters.AddWithValue("@prix", prixP);
                try
                {
                    int verificationAjout = demande.ExecuteNonQuery(); //retourne le nombre de ligne affectée par la commande, donc si on ajoute un client, il y à 1 ligne qui change
                    if (verificationAjout == 1)
                    {
                        Console.WriteLine("\nLe produit à été ajouté !\n");
                    }
                    demande.Parameters.Clear();
                    demande.Dispose();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur ajout produit : " + e.ToString()); // Si erreur MySQL, on l'affiche
                }
            } //Ajoute un produit au catalogue
            void SupprimerProduit()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Retirer un produit :\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Veuillez entrer le numéro du produit : \n");
                int numeroP = Convert.ToInt32(Console.ReadLine());
                demande.CommandText = "SELECT COUNT(*) FROM produit WHERE numero_produit=@numeroproduit"; //On regarde si le produit existe
                demande.Parameters.AddWithValue("@numeroproduit", numeroP);
                int verificationproduit = Convert.ToInt32(demande.ExecuteScalar()); // On regarde combien retourne le COUNT(*)
                demande.Parameters.Clear();
                demande.Dispose();
                if (verificationproduit == 1)
                {
                    try
                    {
                        demande.CommandText = "DELETE FROM produit WHERE numero_produit=@numeroproduit2"; //Instruction pour retirer le produit
                        demande.Parameters.AddWithValue("@numeroproduit2", numeroP);
                        int verificationAjout = demande.ExecuteNonQuery(); //retourne le nombre de lignes affectées par la commande
                        if (verificationAjout == 1)
                        {
                            Console.WriteLine("\nLe produit à été supprimé !\n");
                        }
                        demande.Parameters.Clear();
                        demande.Dispose();
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Erreur produit : " + e.ToString());
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ce numéro de produit n'existe pas !\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } //Supprime un produit du catalogue

            //Fonctions Commandes :

            void AfficherCommandes()
            {
                Console.WriteLine("Voici la liste des commandes :\n");
                demande.CommandText = "SELECT bon_commande,etat,adresse_livraison,date_commande,date_livraison,courriel FROM commande"; // La demande mySQL
                reader = demande.ExecuteReader();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                    }
                    Console.WriteLine("\n");
                }
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Parameters.Clear();
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard
            } //Affiche toutes les commandes en cours
            void AfficherMesCommandes(string courriel)
            {
                Console.WriteLine("Voici la liste de vos commandes :\n");
                demande.CommandText = "SELECT bon_commande,etat,adresse_livraison,date_commande,date_livraison FROM commande WHERE courriel=@courriel"; // La demande mySQL
                demande.Parameters.AddWithValue("@courriel", courriel);
                reader = demande.ExecuteReader();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                    }
                    Console.WriteLine("\n");
                }
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Parameters.Clear();
                demande.Dispose();
            }
            void FiltreCommandesDate(string Date)
            {
                Console.WriteLine("Voici la liste des commandes livrées le "+Date+" :\n");
                demande.CommandText = "SELECT bon_commande,etat,adresse_livraison,date_commande,date_livraison,courriel FROM commande WHERE date_livraison=@date"; // La demande mySQL
                demande.Parameters.AddWithValue("@date", Date);
                reader = demande.ExecuteReader();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                    }
                    Console.WriteLine("\n");
                }
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Parameters.Clear();
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard
            }  //Affiche toutes les commandes livrées un certain jour
            //Bool admin = true si on est administrateur, false par défaut (afin de pouvoir choisir le statut de la commande)
            void PasserCommande(bool admin = false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entrez les informations de la nouvelle commande :\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                double prixtotal = 0;
                AfficherMagasins();
                int verifMagasin = 0;
                string magasin = "";
                while(verifMagasin!=1)
                {
                    Console.WriteLine("\nDans quel magasin voulez-vous passer commande ?\n");
                    magasin = Console.ReadLine();
                    demande.CommandText = "SELECT COUNT(*) FROM magasin WHERE nom=@nommagasin"; //On regarde si le magasin existe
                    demande.Parameters.AddWithValue("@nommagasin", magasin); //permet d'utiliser une variable dans la requête mySQL
                    verifMagasin = Convert.ToInt32(demande.ExecuteScalar()); // Le nombre d'occurences trouvées par le Count(*)
                    demande.Parameters.Clear();
                    demande.Dispose();
                }
                Console.Clear();
                Console.WriteLine("Courriel : \n");
                string courrielC = Console.ReadLine();
                demande.CommandText = "SELECT COUNT(*) FROM Client WHERE courriel=@courriel"; //On regarde si le courriel est déja utilisé
                demande.Parameters.AddWithValue("@courriel", courrielC); //permet d'utiliser une variable dans la requête mySQL
                int verification = Convert.ToInt32(demande.ExecuteScalar()); // Le nombre d'occurences trouvées par le Count(*)
                demande.Parameters.Clear();
                demande.Dispose();
                if (verification == 1)
                {
                    Console.WriteLine("\nMessage : \n");
                    string Message = Console.ReadLine();
                    Console.WriteLine("\nAdresse de livraison : \n");
                    string Adresse = Console.ReadLine();
                    string Etat = "";
                    if (admin == true)
                    {
                        while (Etat != "VINV" && Etat != "CC" && Etat != "CPAV")
                        {
                            Console.WriteLine("\nEtat de la commande : VINV ou CC ou CPAV\n\nVINV = Commande standard pour laquelle un employé doit vérifier l’inventaire\nCC = Tous les items de la commande se trouvent en stock\nCPAV = Commande personnalisée à vérifier\n");
                            Etat = Console.ReadLine();
                        }
                    }
                    else Etat = "CC";
                    Console.WriteLine("\nDate de livraison : (AAAA-MM-JJ)\n");
                    string date = Console.ReadLine();
                    demande.CommandText = "SELECT COUNT(*) FROM commande"; //On regarde combien de commandes existent déja afin d'attribuer le numéro du bon de commande
                    int nbcommandes = Convert.ToInt32(demande.ExecuteScalar()); // Le nombre d'occurences trouvées par le Count(*)
                    demande.Parameters.Clear();
                    demande.Dispose();
                    string bouquetP = "";
                    while (bouquetP != "oui" && bouquetP != "non")
                    {
                        Console.WriteLine("\nVotre commande contient-elle un bouquet personnalisé ? (oui/non)\n");
                        bouquetP = Console.ReadLine();
                    }
                    if (bouquetP == "oui") //Création d'un bouquet personnalisé
                    {
                        Console.WriteLine("\nEntrez une description du bouquet que vous souhaitez : \n");
                        string description = Console.ReadLine();
                        Console.WriteLine("\nEntrez le budget donc vous disposez : \n");
                        double budget = Convert.ToInt32(Console.ReadLine());
                        prixtotal += budget;
                        demande.CommandText = "SELECT COUNT(*) FROM bouquet_personnalise";
                        int nbbouquetP = Convert.ToInt32(demande.ExecuteScalar());
                        demande.Dispose();
                        demande.Parameters.Clear();
                        demande.CommandText = "INSERT INTO bouquet_personnalise VALUES (@numerobouquet, @descriptionB, @budget)";
                        demande.Parameters.AddWithValue("@numerobouquet", nbbouquetP + 1);
                        demande.Parameters.AddWithValue("@descriptionB", description); //On remplace par les variables
                        demande.Parameters.AddWithValue("@budget", budget);
                        try
                        {
                            demande.ExecuteNonQuery();
                            demande.Parameters.Clear();
                            demande.Dispose();
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("Erreur bouquet Personnalisé : " + e.ToString()); // Si erreur MySQL, on l'affiche
                        }
                        demande.CommandText = "INSERT INTO commande VALUES (@numerocommande, @etat, @adresse, @message,@datecommande,@datelivraison,@courriel,@numerobouquet)";
                        demande.Parameters.AddWithValue("@numerobouquet", nbbouquetP + 1);
                    }
                    else
                    {
                        demande.CommandText = "INSERT INTO commande VALUES (@numerocommande, @etat, @adresse, @message,@datecommande,@datelivraison,@courriel,@numerobouquet)";
                        demande.Parameters.AddWithValue("@numerobouquet", null);
                    }
                    demande.Parameters.AddWithValue("@numerocommande", nbcommandes + 1);
                    demande.Parameters.AddWithValue("@etat", Etat);
                    demande.Parameters.AddWithValue("@adresse", Adresse);
                    demande.Parameters.AddWithValue("@message", Message);
                    demande.Parameters.AddWithValue("@datecommande", DateTime.Now);  //date actuelle --> Quand on passe la commande
                    demande.Parameters.AddWithValue("@datelivraison", date);
                    demande.Parameters.AddWithValue("@courriel", courrielC);
                    try
                    {
                        demande.ExecuteNonQuery(); // On execute l'instruction d'ajout
                        demande.Parameters.Clear();
                        demande.Dispose();
                        Console.Clear();
                        string choixbouquet = "";
                        while (choixbouquet != "oui" && choixbouquet != "non")
                        {
                            Console.WriteLine("\nVoulez-vous ajouter un bouquet préconçu à la commande ? (oui/non)\n");
                            choixbouquet = Console.ReadLine();
                        }
                        if (choixbouquet == "oui") //Ajout d'un bouquet préconçu
                        {
                            Console.WriteLine("\nVoici la liste de nos bouquets :\n");
                            AfficherBouquets();
                            demande.CommandText = "SELECT COUNT(*) FROM commande"; //On regarde combien de commandes existent déja afin d'attribuer le numéro du bon de commande
                            int nbcommande = Convert.ToInt32(demande.ExecuteScalar()); // Le nombre d'occurences trouvées par le Count(*)
                            demande.Parameters.Clear();
                            demande.Dispose();
                            Console.WriteLine("\nEntrez le numéro du bouquet que vous voulez ajouter à la commande : \n(entrez fin pour finaliser cette étape)\n");
                            string choixB = Console.ReadLine();
                            if (Int32.TryParse(choixB, out int numbouquet)) //Si on peut convertir la réponse en Int, il s'agit du numéro du bouquet
                            {
                                demande.CommandText = "INSERT INTO est_compose_standard VALUES (@numerobouquet, @numeroCommande)";
                                demande.Parameters.AddWithValue("@numerobouquet", choixB);
                                demande.Parameters.AddWithValue("@numeroCommande", nbcommande);
                                try
                                {
                                    demande.ExecuteNonQuery();
                                    Console.WriteLine("\nLe bouquet à bien été ajouté à la commande ! \n");
                                    demande.Parameters.Clear();
                                    demande.Dispose();
                                    demande.CommandText = "SELECT prix FROM bouquet_standard WHERE numero_bouquet=@numB"; //Pour ajouter le prix total
                                    demande.Parameters.AddWithValue("@numB", numbouquet);
                                    if (Int32.TryParse(choixB, out int testnotnull)) prixtotal += Convert.ToDouble(demande.ExecuteScalar());
                                    demande.Parameters.Clear();
                                    demande.Dispose();
                                    Thread.Sleep(1500);
                                }
                                catch (MySqlException e)
                                {
                                    Console.WriteLine("Erreur ajout bouquet préconçu : " + e.ToString()); // Si erreur MySQL, on l'affiche
                                }
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("Finalement, vous pouvez ajouter chaque article qui composera votre commmande : ");
                        AfficherProduits();
                        demande.CommandText = "SELECT COUNT(*) FROM bouquet_standard"; //On regarde combien de bouquets existent déja afin d'attribuer le numéro du bouquet
                        int nbbouquets = Convert.ToInt32(demande.ExecuteScalar()); // Le nombre d'occurences trouvées par le Count(*)
                        demande.Parameters.Clear();
                        demande.Dispose();
                        demande.CommandText = "INSERT INTO bouquet_standard VALUES (@numerobouquet, @description,@nom,@prix)"; //On crée un nouveau bouquet vide
                        demande.Parameters.AddWithValue("@numerobouquet", nbbouquets + 1);
                        demande.Parameters.AddWithValue("@description", "bouquet commande client");
                        demande.Parameters.AddWithValue("@nom", "commande");
                        demande.Parameters.AddWithValue("@prix", null); //On mettra le prix lorsque les articles composants le bouquets seront connus
                        try
                        {
                            demande.ExecuteNonQuery();
                            demande.Parameters.Clear();
                            demande.Dispose();
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("Erreur création bouquet vide : " + e.ToString()); // Si erreur MySQL, on l'affiche
                        }
                        string choix = "";
                        while (choix != "fin")        //On remplit le bouquet :
                        {
                            Console.WriteLine("\nEntrez le numéro du produit que vous voulez ajouter au bouquet : \n(entrez fin pour finaliser votre commande)\n");
                            choix = Console.ReadLine();
                            if (Int32.TryParse(choix, out int numproduit)) //Si on peut convertir la réponse en Int, il s'agit du numéro de l'article
                            {
                                Console.WriteLine("\nCombien voulez-vous en ajouter ?\n");
                                int quantite = Convert.ToInt32(Console.ReadLine());
                                demande.CommandText = "SELECT quantite FROM stock WHERE nom=@nomM && numero_produit=@numP"; //On vérifie les stocks
                                demande.Parameters.AddWithValue("@nomM",magasin);
                                demande.Parameters.AddWithValue("@numP", numproduit);
                                try
                                {
                                    int stock = Convert.ToInt32(demande.ExecuteScalar());
                                    if (stock - quantite >= 0) //Si on a assez de stock :
                                    {

                                        demande.CommandText = "INSERT INTO contient VALUES (@numeroproduit,@numerobouquet,@quantite)";
                                        demande.Parameters.AddWithValue("@numeroproduit", numproduit);
                                        demande.Parameters.AddWithValue("@numerobouquet", nbbouquets);
                                        demande.Parameters.AddWithValue("@quantite", quantite);
                                        try
                                        {
                                            demande.ExecuteNonQuery();
                                            Console.WriteLine("\nProduit ajouté !\n"); //On passe la commande
                                            demande.Parameters.Clear();
                                            demande.Dispose();
                                            demande.CommandText = "UPDATE stock SET quantite=@qtt WHERE nom=@nomM && numero_produit=@numP";
                                            demande.Parameters.AddWithValue("@qtt", stock - quantite); //Nouvelle valeur du stock
                                            demande.Parameters.AddWithValue("@nomM", magasin);
                                            demande.Parameters.AddWithValue("@numP", numproduit);
                                            demande.ExecuteNonQuery(); //On met les stocks à jour
                                            demande.Parameters.Clear();
                                            demande.Dispose();
                                        }
                                        catch (MySqlException e)
                                        {
                                            Console.WriteLine("Erreur ajout produits : " + e.ToString()); // Si erreur MySQL, on l'affiche
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nCe produit n'est plus en stock dans ce magasin !");
                                        demande.Parameters.Clear();
                                        demande.Dispose();
                                    }
                                }
                                catch (MySqlException e)
                                {
                                    Console.WriteLine("Erreur stock : " + e.ToString()); // Si erreur MySQL, on l'affiche
                                }
                            }
                        }
                        demande.CommandText = "SELECT sum(prix* quantite) FROM contient, produit WHERE contient.numero_produit = produit.numero_produit AND numero_bouquet = @numerobouquet;"; //requête jointure, avec une somme de toutes les lignes pour le prix total;
                        demande.Parameters.AddWithValue("@numerobouquet", nbbouquets);
                        if (Int32.TryParse(choix, out int test)) prixtotal += Convert.ToDouble(demande.ExecuteScalar()); //le prix final de la commande
                        demande.Parameters.Clear();
                        demande.Dispose();
                        Console.WriteLine("\nLa commande à été Finalisée !\nPrix total : " + prixtotal + " euros\n");
                        demande.CommandText = "SELECT type FROM client WHERE courriel=@courriel";
                        demande.Parameters.AddWithValue("@courriel", courrielC);
                        string fidelite = Convert.ToString(demande.ExecuteScalar());
                        demande.Parameters.Clear();
                        demande.Dispose();
                        if (fidelite == "bronze") Console.WriteLine("Grâce au statut de fidélité bronze, le prix total avec 5% de réduction est : " + 0.95 * prixtotal + " euros\n");
                        else if (fidelite == "or") Console.WriteLine("Grâce au le statut de fidélité or, le prix total avec 15% de réduction est : " + 0.85 * prixtotal + " euros\n");
                        MAJfidelite(); //Met à jour la fidélité de tous les clients après chaque commande
                        Thread.Sleep(1500);
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Erreur creation commande : " + e.ToString()); // Si erreur MySQL, on l'affiche
                    }
                    demande.Parameters.Clear();
                    demande.Dispose();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ce courriel n'existe pas !!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } //Passse une commande, en vérifiant le courriel, mettant à jour les stocks et la fidélité, et calulant le prix final
            void ChangerEtat()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chnager l'état d'une commande :\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                AfficherCommandes();
                Console.WriteLine("Veuillez entrer le numéro de la commande que vous voulez modifier: \n"); //tester avec olivialopez@yahoo.com (2 commandes)
                string numcommande = Console.ReadLine();
                string etat = "";
                while(etat!="VINV" && etat != "CC" && etat != "CPAV" && etat != "CAL" && etat != "CL")
                {
                    Console.WriteLine("\nQuel état voulez-vous attribuer à la commande n° " + numcommande + " ?\n");
                    Console.WriteLine("(VINV/CC/CPAV/CAL/CL)\n");
                    etat= Console.ReadLine();
                }
                demande.CommandText = "UPDATE commande SET etat=@etat WHERE  bon_commande=@numcommande";
                demande.Parameters.AddWithValue("@etat",etat);
                demande.Parameters.AddWithValue("@numcommande",numcommande);
                try
                {
                    demande.ExecuteNonQuery();
                    Console.WriteLine("\nL'état de la commande à bien été changé !");
                    Thread.Sleep(1500);
                    demande.Parameters.Clear();
                    demande.Dispose();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur changement etat : " + e.ToString()); // Si erreur MySQL, on l'affiche
                }
            } //Permet de changer l'état d'une commande selectionnée
            void MAJfidelite()
            {
                demande.CommandText = "SELECT courriel,MIN(date_commande) as premierAchat, MAX(date_commande) as dernierAchat,COUNT(*) as nbAchats FROM commande GROUP BY courriel;"; //On affiche le nombre de commandes, la date de première commande, et la date de dernière commande pour chaque client
                reader = demande.ExecuteReader();
                int compteur = 0;
                MySqlCommand changement = Connexion.CreateCommand();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    double nbmois =Convert.ToDouble(DateAndTime.DateDiff(DateInterval.Month, reader.GetDateTime(1), reader.GetDateTime(2)));
                    demande.Dispose();
                    demande.Parameters.Clear();
                    if (nbmois>=1 && (reader.GetDouble(3)/nbmois)>=5) //Si le client achète en moyenne plus de cinq bouquets par mois
                    {
                        changement.CommandText = "UPDATE client SET type=@type WHERE courriel=@courriel";
                        changement.Parameters.AddWithValue("@type","or");
                        changement.Parameters.AddWithValue("@courriel",reader.GetString(0));
                        reader.Close();
                        try
                        {
                            changement.ExecuteNonQuery();
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("Erreur statut or : " + e.ToString()); // Si erreur MySQL, on l'affiche
                        }
                        changement.Parameters.Clear();
                    }
                    else if(nbmois>=1 && (reader.GetDouble(3) / nbmois) >= 1) //Si le client achète en moyenne plus d'un bouquet par mois
                    {
                        changement.CommandText = "UPDATE client SET type=@type WHERE courriel=@courriel";
                        changement.Parameters.AddWithValue("@type", "bronze");
                        changement.Parameters.AddWithValue("@courriel", reader.GetString(0));
                        reader.Close();
                        try
                        {
                            changement.ExecuteNonQuery();
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("Erreur statut bronze : " + e.ToString()); // Si erreur MySQL, on l'affiche
                        }
                        changement.Parameters.Clear();
                    }
                    compteur++;
                    reader.Close();
                    reader = demande.ExecuteReader();
                    for (int i = 0; i < compteur; i++) reader.Read();
                }
                reader.Close();    // On ferme
                demande.Dispose();
            } //Permet de mettre à jour le statut de fidélité de tous les clients à chaque achat

            //Fonctions Stocks :
            void AfficherStocks()

            {
                Console.WriteLine("Voici les stocks :\n");
                demande.CommandText = "SELECT stock.nom,produit.nom,quantite FROM stock,produit WHERE stock.numero_produit = produit.numero_produit ORDER BY stock.nom;"; // La demande mySQL
                reader = demande.ExecuteReader();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                        else Console.Write(" unités ");
                    }
                    Console.WriteLine("\n");
                }
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Parameters.Clear();
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard
            } //Affiche les stocks de tous les produits dans chaque magasin
            void AjouterStocks()
            {
                AfficherProduits();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Quel est le numéro du produit que vous voulez ajouter ?\n");
                Console.ForegroundColor = ConsoleColor.White;
                int numP = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                AfficherMagasins();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nDans quel magasin voulez-vous l'ajouter ?\n");
                Console.ForegroundColor = ConsoleColor.White;
                string magasin = Console.ReadLine();
                Console.WriteLine("\nCombien voulez-vous en ajouter ?\n");
                int quantite = Convert.ToInt32(Console.ReadLine());
                demande.CommandText = "SELECT quantite FROM stock WHERE nom=@nomM && numero_produit=@numP"; //On vérifie les stocks
                demande.Parameters.AddWithValue("@nomM", magasin);
                demande.Parameters.AddWithValue("@numP", numP);
                try
                {
                    int stock = Convert.ToInt32(demande.ExecuteScalar());
                    demande.Parameters.Clear();
                    demande.Dispose();
                    demande.CommandText = "UPDATE stock SET quantite=@qtt WHERE nom=@nomM && numero_produit=@numP";
                    demande.Parameters.AddWithValue("@qtt", stock + quantite); //Nouvelle valeur du stock
                    demande.Parameters.AddWithValue("@nomM", magasin);
                    demande.Parameters.AddWithValue("@numP", numP);
                    demande.ExecuteNonQuery(); //On met les stocks à jour
                    Console.WriteLine("\nLes stocks ont bien étés mit à jour !\n");
                    demande.Parameters.Clear();
                    demande.Dispose();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur ajout stock : " + e.ToString()); // Si erreur MySQL, on l'affiche
                }
            } //Ajoute un produit au stock dans un magasin choisi
            void retirerStocks()
            {
                AfficherProduits();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Quel est le numéro du produit que vous voulez retirer ?\n");
                Console.ForegroundColor = ConsoleColor.White;
                int numP = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                AfficherMagasins();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nDans quel magasin voulez-vous le retirer ?\n");
                Console.ForegroundColor = ConsoleColor.White;
                string magasin = Console.ReadLine();
                Console.WriteLine("\nCombien voulez-vous en retirer ?\n");
                int quantite = Convert.ToInt32(Console.ReadLine());
                demande.CommandText = "SELECT quantite FROM stock WHERE nom=@nomM && numero_produit=@numP"; //On vérifie les stocks
                demande.Parameters.AddWithValue("@nomM", magasin);
                demande.Parameters.AddWithValue("@numP", numP);
                try
                {
                    int stock = Convert.ToInt32(demande.ExecuteScalar());
                    demande.Parameters.Clear();
                    demande.Dispose();
                    if (stock - quantite >= 0) //Si on a assez de stock :
                    {
                        demande.CommandText = "UPDATE stock SET quantite=@qtt WHERE nom=@nomM && numero_produit=@numP";
                        demande.Parameters.AddWithValue("@qtt", stock - quantite); //Nouvelle valeur du stock
                        demande.Parameters.AddWithValue("@nomM", magasin);
                        demande.Parameters.AddWithValue("@numP", numP);
                        demande.ExecuteNonQuery(); //On met les stocks à jour
                        Console.WriteLine("\nLes stocks ont bien étés mit à jour !\n");
                        demande.Parameters.Clear();
                        demande.Dispose();
                    }
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur suppression stocks : " + e.ToString()); // Si erreur MySQL, on l'affiche
                }
            } //Retire un produit du stock dans un magasin choisi
            void AlertesStocks()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nALERTES :\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Les produits suivants sont en faible quanités, pensez à réapprovisioner les stocks !\n");
                demande.CommandText = "SELECT stock.nom,produit.nom,quantite FROM stock,produit WHERE stock.numero_produit = produit.numero_produit AND quantite<20 ORDER BY stock.nom;"; // La demande mySQL
                reader = demande.ExecuteReader();
                while (reader.Read()) //reader.read() permet de lire la ligne suivante, on le fait donc pour toutes les lignes
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                        else Console.Write(" unités ");
                    }
                    Console.WriteLine("\n");
                }
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Parameters.Clear();
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard
            } //Affiche tous les produits dont la quantité est inférieure à 20 dans chaque magasin

            //Fonctions Stat :

            void AfficherStatistiques()
            {
                demande.CommandText = "SELECT AVG(prix) FROM commande NATURAL JOIN bouquet_personnalise;"; //Requête natural Join
                int prixMoyen = Convert.ToInt32(demande.ExecuteScalar());
                demande.Parameters.Clear();
                demande.Dispose();
                Console.WriteLine("prix moyen du bouquet acheté : "+prixMoyen+" euros");

                demande.CommandText = "SELECT SUM(produit.prix * stock.quantite) FROM stock, produit WHERE stock.numero_produit = produit.numero_produit;";
                int ChiffreAffaire = Convert.ToInt32(demande.ExecuteScalar());
                demande.Parameters.Clear();
                demande.Dispose();
                Console.WriteLine("\nLe chiffre d'affaire de tous les magasins est de : " + ChiffreAffaire + " euros");

                //Nombre de client total en additionant les clients qui ont passés une commande, et ceux qui n'en ont jamais passé :
                //Requête synchro + Union (double SELECT + utilisation de UNION)
                demande.CommandText = "SELECT COUNT(courriel) FROM Client WHERE courriel IN (SELECT courriel FROM commande UNION SELECT courriel FROM Client WHERE courriel NOT IN (SELECT courriel FROM commande));";
                int nbClients = Convert.ToInt32(demande.ExecuteScalar()); 
                demande.Parameters.Clear();
                demande.Dispose();
                Console.WriteLine("\nIl y a " + nbClients + " comptes crées");

                //Client ayant passé le plus de commandes : 
                //Requête synchro (double SELECT)
                demande.CommandText = "SELECT nom FROM Client WHERE courriel IN (SELECT courriel FROM commande UNION SELECT courriel FROM Client WHERE courriel NOT IN (SELECT courriel FROM commande));";
                string meilleur = Convert.ToString(demande.ExecuteScalar());
                demande.Parameters.Clear();
                demande.Dispose();
                Console.WriteLine("\nLe client avec le plus de commande est : "+meilleur+" !");

                //magasin avec le plus gros chiffre d'affaire : 
                demande.CommandText = "SELECT stock.nom,SUM(produit.prix * stock.quantite) FROM stock, produit WHERE stock.numero_produit = produit.numero_produit GROUP BY stock.nom;";
                string meilleurMagasin = Convert.ToString(demande.ExecuteScalar());
                demande.Parameters.Clear();
                demande.Dispose();
                Console.WriteLine("\nLe magasin avec le plus gros chiffre d'affaire est : " + meilleurMagasin + " !");

                Console.ReadLine();
            } //Affiche plusieurs statistiques intéréssantes sur l'ensemble du réseau de magasins

            //Fonctions bouquets :

            void AfficherBouquets()
            {
                demande.CommandText = "SELECT * FROM bouquet_standard"; // La demande mySQL
                reader = demande.ExecuteReader();
                int c = 0;
                while (c<8) //Pour affiche seulement les bouquets de la boutique, et non ceux des commandes clients qui sont après l'index 9
                {
                    reader.Read();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                        else Console.Write(" euros "); //Pour le prix 
                    }
                    Console.WriteLine("\n");
                    c++;
                }
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Parameters.Clear();
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard 
            } //Affiche tous les bouquets du catalogue

            //Fonctions Magasins :

            void AfficherMagasins()
            {
                demande.CommandText = "SELECT * FROM magasin"; // La demande mySQL
                reader = demande.ExecuteReader();
                while(reader.Read()){
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetValue(i).ToString());
                        if (i != reader.FieldCount - 1) Console.Write(" - "); //Affichage des valeurs
                    }
                    Console.WriteLine("\n");
                }
                reader.Close();    //fermeture afin de réutiliser un autre reader plus tard
                demande.Parameters.Clear();
                demande.Dispose(); //fermeture afin de réutiliser une autre demande plus tard 
            } //Affiche tous les magasins de la chaine de M.BelleFleur

        }     
    }
}