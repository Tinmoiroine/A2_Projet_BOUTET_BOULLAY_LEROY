using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Projet_PSI_Etape_1
{
    internal class Visuel : Form
    {
        private Graphe graphe;
        private Dictionary<int, Point> positions;
        private Random random = new Random();
        private const int rayon = 30;

        /// <summary>
        /// Constructeur de la classe Visuel permettant d'afficher le graphe graphiquement.
        /// </summary>
        public Visuel(Graphe graphe)
        {
            this.graphe = graphe;
            this.Text = "Visualisation du Graphe";
            this.Size = new Size(800, 600);
            this.DoubleBuffered = true;

            positions = new Dictionary<int, Point>();
            GenererPositions();

            this.Paint += new PaintEventHandler(DessinerGraphe);
        }

        /// <summary>
        /// Génère des positions aléatoires pour chaque nœud du graphe.
        /// </summary>
        private void GenererPositions()
        {
            int largeur = this.ClientSize.Width - 100;
            int hauteur = this.ClientSize.Height - 100;

            foreach (var noeud in graphe.ListeAdjacence.Keys)
            {
                int x = random.Next(50, largeur);
                int y = random.Next(50, hauteur);
                positions[noeud] = new Point(x, y);
            }
        }

        /// <summary>
        /// Dessine le graphe en affichant ses arêtes et nœuds.
        /// </summary>
        private void DessinerGraphe(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen lignePen = new Pen(Color.Black, 2);
            System.Drawing.Font font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Blue);

            foreach (var noeud in graphe.ListeAdjacence)
            {
                foreach (var voisin in noeud.Value)
                {
                    Point p1 = positions[noeud.Key];
                    Point p2 = positions[voisin];
                    g.DrawLine(lignePen, p1, p2);
                }
            }

            foreach (var noeud in graphe.ListeAdjacence.Keys)
            {
                Point position = positions[noeud];
                Rectangle cercle = new Rectangle(position.X - rayon / 2, position.Y - rayon / 2, rayon, rayon);

                g.FillEllipse(Brushes.LightBlue, cercle);
                g.DrawEllipse(Pens.Black, cercle);
                g.DrawString(noeud.ToString(), font, Brushes.Black, position.X - 10, position.Y - 10);
            }
        }

        /// <summary>
        /// Affiche la fenêtre graphique contenant le graphe.
        /// </summary>
        public static void AfficherGraphe(Graphe graphe)
        {
            Application.Run(new Visuel(graphe));
        }
    }
}
