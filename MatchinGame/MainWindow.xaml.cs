using System;
using System.Collections.Generic;
using System.Windows.Threading;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchinGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        int tempsEcoule = 0;
        int nbPairesTrouvees = 0;
        int index;
        DispatcherTimer timer = new DispatcherTimer();

        List<string> animalEmoji = new List<string>()
        {
        "🐈","🐈",
        "🐷","🐷",
        "🐐","🐐",
        "🦊","🦊",
        "🐴","🐴",
        "🦨","🦨",
        "🦉","🦉",
        "🐀","🐀",
        };

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            setupGame();
        }

        private void setupGame()
        {


            foreach (TextBlock textBlock in grdMain.Children.OfType<TextBlock>())
            {
                Random nbAlea = new Random();
                string nextEmoji;
                index = 0;
                if (textBlock.Name != "txtTemps" && animalEmoji.Count != 0)
                {
                    index = nbAlea.Next(animalEmoji.Count);
                    nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tempsEcoule++;
            txtTemps.Text = (tempsEcoule / 10F).ToString("0.0s");
            if (nbPairesTrouvees == 8)
            {
                timer.Stop();
                txtTemps.Text = txtTemps.Text + " - Rejouer ? ";
            }

        }

        TextBlock derniereTBClique; // on va l’utiliser pour faire une référence à  la TextBlock sur laquelle on vient de cliquer
        bool trouvePaire = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlockActif = sender as TextBlock;
            if (!trouvePaire)
            {
                textBlockActif.Visibility = Visibility.Hidden;
                derniereTBClique = textBlockActif;
                trouvePaire = true;
            }
            else if (textBlockActif.Text == derniereTBClique.Text)
            {
                nbPairesTrouvees++;
                textBlockActif.Visibility = Visibility.Hidden;
                trouvePaire = false;
            }
            else if (textBlockActif.Text == derniereTBClique.Text)
            {
                textBlockActif.Visibility = Visibility.Hidden;
                trouvePaire = false;
            }

            else
            {
                derniereTBClique.Visibility = Visibility.Visible;
                trouvePaire = false;
            }

        }

        private void txtTemps_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (nbPairesTrouvees == 8)
            {
                setupGame();
            }
        }
    }
}
