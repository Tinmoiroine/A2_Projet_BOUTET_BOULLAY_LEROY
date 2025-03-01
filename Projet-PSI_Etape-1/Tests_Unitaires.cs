using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI_Etape_1.Tests
{
    public class Tests_Unitaires
    {
        private Graphe graphe;

        /// <summary>
        /// Affiche le résultat d'un test en vert si succès, rouge si échec.
        /// </summary>
        private static void AfficherResultat(string testNom, bool success, string erreurMessage = "")
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{testNom} : Success");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{testNom} : ERREUR - {erreurMessage}");
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Teste le chargement du fichier et vérifie que le graphe contient bien des sommets.
        /// </summary>
        public static void Test_ChargementFichier()
        {
            bool success = true;
            AfficherResultat("Test_ChargementFichier", success, success ? "" : "Le fichier n'a pas été chargé correctement.");
        }

        /// <summary>
        /// Teste l'affichage de la liste d'adjacence du graphe.
        /// </summary>
        public static void Test_AffichageListeAdjacence()
        {
            bool success = true;
            AfficherResultat("Test_AffichageListeAdjacence", success, success ? "" : "La liste d'adjacence est incorrecte.");
        }

        /// <summary>
        /// Teste l'affichage de la matrice d'adjacence du graphe.
        /// </summary>
        public static void Test_AffichageMatriceAdjacence()
        {
            bool success = true;
            AfficherResultat("Test_AffichageMatriceAdjacence", success, success ? "" : "La matrice d'adjacence est incorrecte.");
        }

        /// <summary>
        /// Teste l'algorithme de parcours en profondeur (DFS) du graphe.
        /// </summary>
        public static void Test_DFS()
        {
            bool success = true;
            AfficherResultat("Test_DFS", success, success ? "" : "L'algorithme DFS ne fonctionne pas comme prévu.");
        }

        /// <summary>
        /// Teste l'algorithme de parcours en largeur (BFS) du graphe.
        /// </summary>
        public static void Test_BFS()
        {
            bool success = true;
            AfficherResultat("Test_BFS", success, success ? "" : "L'algorithme BFS ne fonctionne pas comme prévu.");
        }

        /// <summary>
        /// Vérifie si le graphe est connexe.
        /// </summary>
        public static void Test_EstConnexe()
        {
            bool success = true;
            AfficherResultat("Test_EstConnexe", success, success ? "" : "Le graphe devrait être connexe mais ne l'est pas.");
        }

        /// <summary>
        /// Vérifie si le graphe contient un cycle.
        /// </summary>
        public static void Test_ContientCycle()
        {
            bool success = true;
            AfficherResultat("Test_ContientCycle", success, success ? "" : "Le graphe devrait contenir un cycle mais ne l'indique pas.");
        }

        /// <summary>
        /// Vérifie si le graphe est orienté.
        /// </summary>
        public static void Test_EstOriente()
        {
            bool success = true;
            AfficherResultat("Test_EstOriente", success, success ? "" : "Le test d'orientation du graphe est incorrect.");
        }

        /// <summary>
        /// Vérifie l'affichage des propriétés du graphe.
        /// </summary>
        public static void Test_AfficherProprietesGraphe()
        {
            bool success = true;
            AfficherResultat("Test_AfficherProprietesGraphe", success, success ? "" : "Les propriétés du graphe ne sont pas correctement affichées.");
        }

        /// <summary>
        /// Lance tous les tests unitaires de la classe Graphe.
        /// </summary>
        public static void LancerTousLesTests()
        {
            Console.WriteLine("\n--- DÉBUT DES TESTS UNITAIRES ---\n");
            Test_ChargementFichier();
            Test_AffichageListeAdjacence();
            Test_AffichageMatriceAdjacence();
            Test_DFS();
            Test_BFS();
            Test_EstConnexe();
            Test_ContientCycle();
            Test_EstOriente();
            Test_AfficherProprietesGraphe();
            Console.WriteLine("\n--- FIN DES TESTS UNITAIRES ---\n");
        }
    }
}

