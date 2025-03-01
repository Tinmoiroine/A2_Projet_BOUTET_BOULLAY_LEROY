using Projet_PSI_Etape_1.Tests;
using System;
using System.IO;
using System.Windows.Forms;

namespace Projet_PSI_Etape_1
{
    internal class Program
    {
        /// <summary>
        /// Point d'initialisation du forms (fenêtre d'affichage du graphe)
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            Graphe monGraphe = new Graphe();
            string cheminFichier = "soc-karate.txt";

            /// Test unitaires  Lancement de tous les tests
            Tests_Unitaires.LancerTousLesTests();

            /// <summary>
            /// Vérifie si le fichier existe avant de tenter de le charger.
            /// </summary>
            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine($"ERREUR : Le fichier '{cheminFichier}' est introuvable !");
                return;
            }

            monGraphe.ChargerDepuisFichier(cheminFichier);
            monGraphe.AfficherListeAdjacence();
            monGraphe.AfficherMatriceAdjacence();

            int sommetDepart = 0;

            /// <summary>
            /// Vérifie si le sommet de départ existe avant d'exécuter DFS/BFS.
            /// </summary>
            if (!monGraphe.ListeAdjacence.ContainsKey(sommetDepart))
            {
                Console.WriteLine($"ERREUR : Le sommet de départ {sommetDepart} n'existe pas dans le graphe !");
                return;
            }

            monGraphe.DFS(sommetDepart);
            monGraphe.BFS(sommetDepart);

            Console.WriteLine("\nLe graphe est-il connexe ? " + monGraphe.EstConnexe());
            Console.WriteLine("\nLe graphe contient-il un cycle ? " + monGraphe.ContientCycle());

            /// <summary>
            /// Vérification et affichage des propriétés du graphe si non vide.
            /// </summary>
            if (monGraphe.ListeAdjacence.Count > 0)
            {
                monGraphe.AfficherProprietesGraphe();
            }
            else
            {
                Console.WriteLine("\nLe graphe est vide, impossible d'afficher ses propriétés.");
            }

            /// <summary>
            /// Affichage graphique du graphe.
            /// </summary>
            Visuel.AfficherGraphe(monGraphe);
        }
    }
}
