using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Projet_PSI_Etape_1
{
    internal class Graphe
    {
        public Dictionary<int, List<int>> ListeAdjacence { get; private set; }
        public int[,] MatriceAdjacence { get; private set; }
        public int NombreNoeuds { get; private set; }
        private Dictionary<int, int> NoeudToIndex;

        public Graphe()
        {
            ListeAdjacence = new Dictionary<int, List<int>>();
            NoeudToIndex = new Dictionary<int, int>();
        }

        /// <summary>
        /// Charge un graphe depuis un fichier texte contenant des paires de sommets.
        /// </summary>
        /// <param name="chemin">Le chemin du fichier à charger.</param>
        public void ChargerDepuisFichier(string chemin)
        {
            try
            {
                var lignes = File.ReadAllLines(chemin);
                HashSet<int> noeuds = new HashSet<int>();
                List<Tuple<int, int>> liens = new List<Tuple<int, int>>();

                Regex regex = new Regex(@"\((\d+),\s*(\d+)\)");

                foreach (var ligne in lignes)
                {
                    Match match = regex.Match(ligne);
                    if (match.Success)
                    {
                        int noeud1 = int.Parse(match.Groups[1].Value);
                        int noeud2 = int.Parse(match.Groups[2].Value);

                        noeuds.Add(noeud1);
                        noeuds.Add(noeud2);
                        liens.Add(new Tuple<int, int>(noeud1, noeud2));
                    }
                }

                if (noeuds.Count == 0)
                {
                    Console.WriteLine("Aucune donnée valide trouvée dans le fichier !");
                    return;
                }

                int maxNoeud = noeuds.Max();
                NombreNoeuds = maxNoeud + 1;
                MatriceAdjacence = new int[NombreNoeuds, NombreNoeuds];

                foreach (var noeud in noeuds)
                {
                    if (!ListeAdjacence.ContainsKey(noeud))
                        ListeAdjacence[noeud] = new List<int>();
                }

                foreach (var lien in liens)
                {
                    ListeAdjacence[lien.Item1].Add(lien.Item2);
                    ListeAdjacence[lien.Item2].Add(lien.Item1);

                    MatriceAdjacence[lien.Item1, lien.Item2] = 1;
                    MatriceAdjacence[lien.Item2, lien.Item1] = 1;
                }

                Console.WriteLine("Le graphe a été chargé avec succès !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }
        }

        /// <summary>
        /// Affiche la liste d'adjacence du graphe.
        /// </summary>
        public void AfficherListeAdjacence()
        {
            Console.WriteLine("\nListe d'Adjacence :");
            foreach (var noeud in ListeAdjacence)
            {
                Console.Write($"{noeud.Key} -> ");
                Console.WriteLine(string.Join(", ", noeud.Value));
            }
        }

        /// <summary>
        /// Affiche la matrice d'adjacence du graphe.
        /// </summary>
        public void AfficherMatriceAdjacence()
        {
            Console.WriteLine("\nMatrice d'Adjacence :");
            for (int i = 0; i < NombreNoeuds; i++)
            {
                for (int j = 0; j < NombreNoeuds; j++)
                {
                    Console.Write(MatriceAdjacence[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Effectue un parcours DFS (profondeur) à partir d'un sommet donné.
        /// </summary>
        /// <param name="sommetDepart">Le sommet de départ.</param>
        public void DFS(int sommetDepart)
        {
            if (!ListeAdjacence.ContainsKey(sommetDepart))
            {
                Console.WriteLine($"\nLe sommet {sommetDepart} n'existe pas dans le graphe !");
                return;
            }

            HashSet<int> visites = new HashSet<int>();
            Console.WriteLine("\nParcours DFS depuis " + sommetDepart + " :");
            DFSRecursive(sommetDepart, visites);
            Console.WriteLine();
        }

        private void DFSRecursive(int sommet, HashSet<int> visites)
        {
            visites.Add(sommet);
            Console.Write(sommet + " ");

            foreach (var voisin in ListeAdjacence[sommet])
            {
                if (!visites.Contains(voisin))
                {
                    DFSRecursive(voisin, visites);
                }
            }
        }

        /// <summary>
        /// Effectue un parcours BFS (largeur) à partir d'un sommet donné.
        /// </summary>
        /// <param name="sommetDepart">Le sommet de départ.</param>
        public void BFS(int sommetDepart)
        {
            if (!ListeAdjacence.ContainsKey(sommetDepart))
            {
                Console.WriteLine($"\nLe sommet {sommetDepart} n'existe pas dans le graphe !");
                return;
            }

            HashSet<int> visites = new HashSet<int>();
            Queue<int> file = new Queue<int>();

            visites.Add(sommetDepart);
            file.Enqueue(sommetDepart);

            Console.WriteLine("\nParcours BFS depuis " + sommetDepart + " :");

            while (file.Count > 0)
            {
                int sommet = file.Dequeue();
                Console.Write(sommet + " ");

                foreach (var voisin in ListeAdjacence[sommet])
                {
                    if (!visites.Contains(voisin))
                    {
                        visites.Add(voisin);
                        file.Enqueue(voisin);
                    }
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Vérifie si le graphe est connexe.
        /// </summary>
        /// <returns>True si le graphe est connexe, sinon False.</returns>
        public bool EstConnexe()
        {
            if (ListeAdjacence.Count == 0) return false;

            HashSet<int> visites = new HashSet<int>();
            int premierSommet = ListeAdjacence.Keys.First();

            DFSRecursive(premierSommet, visites);

            return visites.Count == ListeAdjacence.Count;
        }

        /// <summary>
        /// Vérifie si le graphe contient un cycle.
        /// </summary>
        /// <returns>True si le graphe contient un cycle, sinon False.</returns>
        public bool ContientCycle()
        {
            HashSet<int> visites = new HashSet<int>();
            foreach (var noeud in ListeAdjacence.Keys)
            {
                if (!visites.Contains(noeud))
                {
                    if (DFSDetectCycle(noeud, -1, visites)) return true;
                }
            }
            return false;
        }

        private bool DFSDetectCycle(int noeud, int parent, HashSet<int> visites)
        {
            visites.Add(noeud);

            foreach (var voisin in ListeAdjacence[noeud])
            {
                if (!visites.Contains(voisin))
                {
                    if (DFSDetectCycle(voisin, noeud, visites)) return true;
                }
                else if (voisin != parent) return true;
            }
            return false;
        }

        /// <summary>
        /// Vérifie si le graphe est orienté.
        /// </summary>
        /// <returns>True si le graphe est orienté, sinon False.</returns>
        public bool EstOriente()
        {
            foreach (var noeud in ListeAdjacence.Keys)
            {
                foreach (var voisin in ListeAdjacence[noeud])
                {
                    if (!ListeAdjacence[voisin].Contains(noeud))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Affichage des propriétés du graphe
        /// </summary>
        public void AfficherProprietesGraphe()
        {
            int nombreNoeuds = ListeAdjacence.Count;
            int nombreLiens = ListeAdjacence.Sum(noeud => noeud.Value.Count) / 2; 

            Console.WriteLine("\nPropriétés du Graphe :");
            Console.WriteLine($"Nombre de nœuds (sommets) : {nombreNoeuds}");
            Console.WriteLine($"Nombre de liens (arêtes) : {nombreLiens}");
            Console.WriteLine($"Ordre du graphe : {nombreNoeuds}");
            Console.WriteLine($"Taille du graphe : {nombreLiens}");
            double densite = (double)nombreLiens / (nombreNoeuds * (nombreNoeuds - 1) / 2);
            Console.WriteLine($"Densité du graphe : {densite:F4}");
            Console.WriteLine($"Le graphe est orienté ? {EstOriente()}");
        }
    }
}
