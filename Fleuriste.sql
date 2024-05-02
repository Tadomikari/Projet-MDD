#------------------------------------------------------------
#        Script MySQL.
#------------------------------------------------------------

DROP DATABASE IF EXISTS Fleuriste;
CREATE DATABASE IF NOT EXISTS Fleuriste;
USE Fleuriste;

#------------------------------------------------------------
# Table: Client
#------------------------------------------------------------

CREATE TABLE Client(
        NumeroC                Int NOT NULL ,
        Numero_Telephone       Varchar (50),
        Courriel               Varchar (50),
        NomC                   Varchar (50),
        Prenom                 Varchar (50),
        Mot_de_passe           Varchar (50),
        Adresse_de_facturation Varchar (50),
        Carte_de_credit        Varchar (50),
        Fidelite               Varchar (50)
	,CONSTRAINT Client_PK PRIMARY KEY (NumeroC)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Bouquet
#------------------------------------------------------------

CREATE TABLE Bouquet(
        NumeroB     Int NOT NULL ,
        Description Varchar (50),
        prix        Int
	,CONSTRAINT Bouquet_PK PRIMARY KEY (NumeroB)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Produit
#------------------------------------------------------------

CREATE TABLE Produit(
        NumeroP       Int NOT NULL ,
        Type          Varchar (50),
        prix          Int,
        Description   Varchar (50),
	CONSTRAINT Produit_PK PRIMARY KEY (NumeroP)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Magasin
#------------------------------------------------------------

CREATE TABLE Magasin(
        idMagasin Int NOT NULL ,
        Nom       Varchar (50),
        Adresse   Varchar (50)
	,CONSTRAINT Magasin_PK PRIMARY KEY (idMagasin)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Bon de Commande
#------------------------------------------------------------

CREATE TABLE Bon_de_Commande(
        idCommande        Int NOT NULL ,
        Etat_Commande     enum ("VINV","CC","CPAV","CAL","CL"),
        Adresse_Livraison Varchar (50) NOT NULL ,
        Message           Varchar (200) NOT NULL ,
        Date_Commande     Date NOT NULL ,
        Date_Livraison    Date NOT NULL ,
        NumeroC           Int NOT NULL ,
        idMagasin         Int NOT NULL
	,CONSTRAINT Bon_de_Commande_PK PRIMARY KEY (idCommande)

	,CONSTRAINT Bon_de_Commande_Client_FK FOREIGN KEY (NumeroC) REFERENCES Client(NumeroC)
	,CONSTRAINT Bon_de_Commande_Magasin0_FK FOREIGN KEY (idMagasin) REFERENCES Magasin(idMagasin)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Bouquet Standard
#------------------------------------------------------------

CREATE TABLE Bouquet_Standard(
        NumeroBS    Int NOT NULL ,
        Description Varchar (200),
        prix        Int,
        Nom         Varchar (50)
	,CONSTRAINT Bouquet_Standard_PK PRIMARY KEY (NumeroBS)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Contient
#------------------------------------------------------------

CREATE TABLE Contient(
        NumeroB    Int,
        idCommande Int,
        Quantite   Int
	,CONSTRAINT Contient_PK PRIMARY KEY (NumeroB,idCommande)

	,CONSTRAINT Contient_Bouquet_FK FOREIGN KEY (NumeroB) REFERENCES Bouquet(NumeroB)
	,CONSTRAINT Contient_Bon_de_Commande0_FK FOREIGN KEY (idCommande) REFERENCES Bon_de_Commande(idCommande)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Est composé
#------------------------------------------------------------

CREATE TABLE Est_compose(
        NumeroP  Int,
        NumeroB  Int,
        Quantite Int
	,CONSTRAINT Est_compose_PK PRIMARY KEY (NumeroP,NumeroB)

	,CONSTRAINT Est_compose_Produit_FK FOREIGN KEY (NumeroP) REFERENCES Produit(NumeroP)
	,CONSTRAINT Est_compose_Bouquet0_FK FOREIGN KEY (NumeroB) REFERENCES Bouquet(NumeroB)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Possède
#------------------------------------------------------------

CREATE TABLE Possede(
        NumeroP   Int,
        idMagasin Int,
        Quantite  Int
	,CONSTRAINT Possede_PK PRIMARY KEY (NumeroP,idMagasin)

	,CONSTRAINT Possede_Produit_FK FOREIGN KEY (NumeroP) REFERENCES Produit(NumeroP)
	,CONSTRAINT Possede_Magasin0_FK FOREIGN KEY (idMagasin) REFERENCES Magasin(idMagasin)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Contient_S
#------------------------------------------------------------

CREATE TABLE Contient_S(
        NumeroBS   Int,
        idCommande Int,
        Quantite   Int
	,CONSTRAINT Contient_S_PK PRIMARY KEY (NumeroBS,idCommande)

	,CONSTRAINT Contient_S_Bouquet_Standard_FK FOREIGN KEY (NumeroBS) REFERENCES Bouquet_Standard(NumeroBS)
	,CONSTRAINT Contient_S_Bon_de_Commande0_FK FOREIGN KEY (idCommande) REFERENCES Bon_de_Commande(idCommande)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Insertion initiale :
#------------------------------------------------------------

INSERT INTO `Bouquet_Standard`(`NumeroBS`,`Description`,`prix`,`nom`) VALUES (1, 'Arrangement floral avec marguerites et verdure', 45, 'Gros Merci');
INSERT INTO `Bouquet_Standard` VALUES (2, 'Arrangement floral avec roses blanches et roses rouges', 65, "L'amoureux");
INSERT INTO `Bouquet_Standard` VALUES (3, 'Arrangement floral avec ginger, oiseaux du paradis, roses et genet', 40, "L'exotique");
INSERT INTO `Bouquet_Standard` VALUES (4, 'Arrangement floral avec gerbera, roses blanches, lys et alstoméria', 45, 'Fête des Mères');
INSERT INTO `Bouquet_Standard` VALUES (5, 'Arrangement floral avec lys et orchidées', 45, 'Mariage');

INSERT INTO `Produit` (`NumeroP`,`Type`,`Prix`,`Description`) VALUES (1, 'FLEUR', 5, 'Gerbera');
INSERT INTO `Produit` VALUES (2, 'FLEUR', 4, 'Ginger');
INSERT INTO `Produit` VALUES (3, 'FLEUR', 1, 'Glaïeul');
INSERT INTO `Produit` VALUES (4, 'FLEUR', 2.25, 'Marguerite');
INSERT INTO `Produit` VALUES (5, 'FLEUR', 2.50, 'Rose rouge');
INSERT INTO `Produit` VALUES (6, 'ACCESSOIRE', 1, 'Ruban blanc');
INSERT INTO `Produit` VALUES (7, 'ACCESSOIRE', 7, 'Coffret');

INSERT INTO `Client`(`NumeroC`,`Numero_Telephone`,`Courriel`,`nomC`,`prenom`,`Mot_de_passe`, `Adresse_de_facturation`,`Carte_de_credit`,`Fidelite`) VALUES (1,'0606060606', 'dimPayet@gmail.com','Payet', 'Dimitri','1987MA', 'impasse des lilas', '0000000000000000','or');
INSERT INTO `Client` VALUES (2,'0606060607', 'guendoudou@gmail.com','Guendouzi', 'Matteo','virAuteuil', 'rue Pasteur', '0000000000000001','bronze');
INSERT INTO `Client` VALUES (3,'0606060608', 'amineeee@gmail.com','Harit', 'Amine','goLions', 'rue Ziyech', '0000000000000002','aucun');
INSERT INTO `Client` VALUES (4,'0606060609', 'valou@gmail.com','Rongier', 'Valentin','jeSuisUnCamion', 'rue des acacias', '0000000000000003','aucun');
INSERT INTO `Client` VALUES (5,'0606060610', 'julio@gmail.com','Iglesias', 'Julio','zapatilla12', 'rue de la Soif', '0000000000000004','argent');
INSERT INTO `Client` VALUES (6,'0606060611', 'dudu@gmail.com','Dudu', 'Da Silva','azerty', 'rue du Paradis', '0000000000000005','aucun');
INSERT INTO `Client` VALUES (7,'0606060612', 'lucien@gmail.com','Laporte', 'Lucien','sacapuce', 'rue de la Gare', '0000000000000006','bronze');
INSERT INTO `Client` VALUES (8,'0606060613', 'johndoee@gmail.com','Doe', 'John','passw0rd', 'rue des lilas', '0000000000000007','aucun');
INSERT INTO `Client` VALUES (9,'0606060614', 'eleonore@gmail.com','Dupont', 'Eleonore','cat123', 'rue de la Liberté', '0000000000000008','argent');
INSERT INTO `Client` VALUES (10,'0606060615', 'pauline@yahoo.com','Sagne', 'Pauline','coucou123', 'rue de la Paix', '0000000000000009','or');
INSERT INTO `Client` VALUES (11,'0606060616', 'ahmed@gmail.com','Benzia', 'Ahmed','123soleil', 'rue de la Victoire', '0000000000000010','aucun');
INSERT INTO `Client` VALUES (12,'0606060617', 'amandine@gmail.com','Moreau', 'Amandine','motdepasse', 'rue de la Croix', '0000000000000011','aucun');
INSERT INTO `Client` VALUES (13,'0606060618', 'sylvain@gmail.com','Bertin', 'Sylvain','manger123', 'rue du Pain', '0000000000000012','argent');
INSERT INTO `Client` VALUES (14,'0606060619', 'lea@hotmail.com','Durand', 'Léa','123abcd', 'rue des Soucis', '0000000000000013','or');

INSERT INTO `Bouquet`(`NumeroB`, `Description`, `prix`) VALUES (1, 'Petit bouquet sympa avec des fleurs claires', 20);
INSERT INTO `Bouquet` VALUES (2, 'Grand bouquet très rouge', 40);
INSERT INTO `Bouquet` VALUES (3, 'Bouquet très divers et coloré', 50);
INSERT INTO `Bouquet` VALUES (4, 'Bouquet rose et blanc', 35);
INSERT INTO `Bouquet` VALUES (5, 'Bouquet de roses rouges', 25);
INSERT INTO `Bouquet` VALUES (6, 'Bouquet de lys blancs', 30);
INSERT INTO `Bouquet` VALUES (7, 'Bouquet de tulipes multicolores', 20);
INSERT INTO `Bouquet` VALUES (8, 'Bouquet de pivoines', 45);
INSERT INTO `Bouquet` VALUES (9, 'Bouquet de renoncules', 28);

INSERT INTO `Magasin`(`idMagasin`, `Nom`, `Adresse`) VALUES (1, 'XVIIIe', 'rue du clair de lune');
INSERT INTO `Magasin` VALUES (2, 'XXe', 'rue Voltaire');
INSERT INTO `Magasin` VALUES (3, 'Neuilly', 'avenue de la grande armee');

INSERT INTO `Bon_de_Commande`(`idCommande`,`Etat_Commande`,`Adresse_Livraison`,`Message`,`Date_Commande`,`Date_Livraison`, `NumeroC`,`idMagasin`) VALUES (1, 'CL', 'avenue des Champs-Elysees', 'gros bisous',"2023-03-13", "2023-03-17",1,1);
INSERT INTO `Bon_de_Commande` VALUES (2, 'CC', 'rue des vaches', 'salut', "2023-03-19", "2023-04-19",2, 1);
INSERT INTO `Bon_de_Commande` VALUES (3, 'CAL', 'rue Auber', 'bien joue', "2023-03-29", "2023-04-14",3, 3);
INSERT INTO `Bon_de_Commande` VALUES (4, 'VINV', 'avenue du Montparnasse', 'ah oui', "2023-04-12", "2023-04-20",4, 2);
INSERT INTO `Bon_de_Commande` VALUES (5, 'CL', 'rue de la Paix', 'Commande XYZ', "2023-04-16", "2023-04-18", 1, 2);
INSERT INTO `Bon_de_Commande` VALUES (6, 'VINV', 'avenue de la République', 'Commande ABC', "2023-04-17", "2023-04-19", 2, 3);
INSERT INTO `Bon_de_Commande` VALUES (7, 'CL', 'rue du Faubourg Saint-Honoré', 'Commande DEF', "2023-04-18", "2023-04-20", 3, 1);
INSERT INTO `Bon_de_Commande` VALUES (8, 'CPAV', 'boulevard Saint-Germain', 'Commande GHI', "2023-04-19", "2023-04-21", 4, 2);
INSERT INTO `Bon_de_Commande` VALUES (9, 'CAL', 'rue Saint-Antoine', 'Commande JKL', "2023-04-20", "2023-04-22", 5, 3);
INSERT INTO `Bon_de_Commande` VALUES (10, 'VINV', 'rue du Bac', 'Commande MNO', "2023-04-21", "2023-04-23", 1, 1);
INSERT INTO `Bon_de_Commande` VALUES (11, 'CPAV', 'boulevard Haussmann', 'Commande PQR', "2023-04-22", "2023-04-24", 2, 2);
INSERT INTO `Bon_de_Commande` VALUES (12, 'VINV', 'avenue des Ternes', 'Commande STU', "2023-04-23", "2023-04-25", 3, 1);
INSERT INTO `Bon_de_Commande` VALUES (13, 'CAL', 'rue de Rivoli', 'Commande VWX', "2023-04-24", "2023-04-26", 4, 2);
INSERT INTO `Bon_de_Commande` VALUES (14, 'CC', 'rue des Petits Champs', 'Commande YZA', "2023-04-25", "2023-04-27", 5, 3);
INSERT INTO `Bon_de_Commande` VALUES (15, 'CC', 'rue Saint-Honoré', 'Commande BCD', "2023-04-26", "2023-04-28", 1, 3);
INSERT INTO `Bon_de_Commande` VALUES (16, 'CAL', 'rue de la Pompe', 'Commande EFG', "2023-04-27", "2023-04-29", 2, 2);
INSERT INTO `Bon_de_Commande` VALUES (17, 'VINV', 'avenue Victor Hugo', 'Commande HIJ', "2023-04-28", "2023-04-30", 3, 1);
INSERT INTO `Bon_de_Commande` VALUES (18, 'CC', 'rue de la Boétie', 'Commande KLM', "2023-04-29", "2023-05-01", 4, 2);


INSERT INTO `Contient`(`numeroB`,`idCommande`, `Quantite`) VALUES (1, 1, 2);
INSERT INTO `Contient` VALUES (2, 1, 3);
INSERT INTO `Contient` VALUES (2, 3, 2);
INSERT INTO `Contient` VALUES (3, 2, 1);
INSERT INTO `Contient` VALUES (5, 3, 1);
INSERT INTO `Contient` VALUES (6, 4, 3);
INSERT INTO `Contient` VALUES (7, 1, 1);
INSERT INTO `Contient` VALUES (8, 4, 2);
INSERT INTO `Contient` VALUES (9, 2, 2);
INSERT INTO `Contient` VALUES (2, 9, 3);
INSERT INTO `Contient` VALUES (1, 4, 1);
INSERT INTO `Contient` VALUES (3, 1, 2);
INSERT INTO `Contient` VALUES (5, 2, 2);

INSERT INTO `Est_compose` (NumeroP, NumeroB, Quantite) VALUES (1, 1, 3);
INSERT INTO `Est_compose` VALUES (1, 2, 2);
INSERT INTO `Est_compose` VALUES (2, 1, 1);
INSERT INTO `Est_compose` VALUES (4, 3, 1);
INSERT INTO `Est_compose` VALUES (3,7, 4);
INSERT INTO `Est_compose` VALUES (5, 1, 3);
INSERT INTO `Est_compose` VALUES (6, 3, 1);
INSERT INTO `Est_compose` VALUES (4, 2, 2);
INSERT INTO `Est_compose` VALUES (2,5, 4);
INSERT INTO `Est_compose` VALUES (3, 8, 1);
INSERT INTO `Est_compose`VALUES (1, 3, 2);
INSERT INTO `Est_compose` VALUES (5, 9, 1);
INSERT INTO `Est_compose` VALUES (6, 2, 2);
INSERT INTO `Est_compose` VALUES (3,1, 3);

INSERT INTO `Possede` (NumeroP, idMagasin, Quantite) VALUES (1, 1, 10);
INSERT INTO `Possede` VALUES (2, 1, 5);
INSERT INTO `Possede` VALUES (3, 2, 8);
INSERT INTO `Possede` VALUES (4, 3, 12);
INSERT INTO `Possede` VALUES (4, 1, 6);
INSERT INTO `Possede` VALUES (5, 2, 3);
INSERT INTO `Possede` VALUES (6, 3, 2);
INSERT INTO `Possede` VALUES (7, 1, 1);
INSERT INTO `Possede` VALUES (2, 2, 4);
INSERT INTO `Possede` VALUES (5, 3, 5);
INSERT INTO `Possede` VALUES (6, 1, 7);

INSERT INTO `Contient_S` (NumeroBS, idCommande, Quantite) VALUES (1, 1, 2);
INSERT INTO `Contient_S` VALUES (2, 2, 1);
INSERT INTO `Contient_S` VALUES (1, 2, 3);
INSERT INTO `Contient_S` VALUES (3, 4, 2);
INSERT INTO `Contient_S` VALUES (2, 14, 1);
INSERT INTO `Contient_S` VALUES (4, 5, 5);
INSERT INTO `Contient_S` VALUES (1, 3, 2);
INSERT INTO `Contient_S` VALUES (2, 1, 1);
INSERT INTO `Contient_S` VALUES (3, 7, 3);
INSERT INTO `Contient_S` VALUES (4, 4, 4);

