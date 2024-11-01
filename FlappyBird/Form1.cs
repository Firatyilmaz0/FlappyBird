using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        //boruların,yerçekiminin,skorun değerlerini belirliyoruz
        int pipeSpeed = 8;
        int gravity = 10;
        int score = 0;
        public Form1()
        {
            InitializeComponent();
        }


        private void gameTimeEvent(object sender, EventArgs e)
        {
            //Kuşun,Boruların ve skorun nasıl artış göstereceğini veya nasıl hareket edeceğini gösterdik
            bird.Top += gravity;
            pipedown.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score;
            //iki boru arasından geçtiğinde skorumuz +2 artacak
            if (pipedown.Left<-150) {
                pipedown.Left = 700;
                score++;
            }
            if (pipeTop.Left<-180)
            {
                pipeTop.Left = 850;
                score++;
            }
            if (bird.Bounds.IntersectsWith(pipedown.Bounds) ||
                bird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                bird.Bounds.IntersectsWith(ground.Bounds)) 
            {
                endGame();
            }
            //skorumuz 8 olduktan sonra hız artışı olacak
            if(score>8)
            {
                pipeSpeed = 12;
            }
            if (bird.Top<-2) {
                endGame();
            }
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                gravity = -10;
            }

        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 10;
            }
           
        }
        private void endGame()
        {
            gameTimer.Stop();
            // Mesaj kutusu göster
            DialogResult result = MessageBox.Show($"Oyun Bitti! Skorunuz: {score}\nYeniden oynamak ister misiniz?",
                "Oyun Bitti", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                // Oyunu yeniden başlat
                RestartGame();
            }
            else if (result == DialogResult.No)
            {
                // Skoru göster
                MessageBox.Show($"Son skorunuz: {score}", "Skor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Uygulamadan çık
                Application.Exit();
            }
        }

        private void RestartGame()
        {
            // Oyun değişkenlerini sıfırla
            score = 0;
            pipeSpeed = 8;
            gravity = 10;

            // Kuşun, boruların vs. başlangıç pozisyonlarını sıfırla
            bird.Top = 100; // Başlangıç pozisyonunu ayarla
            pipedown.Left = 700; // Boru pozisyonunu sıfırla
            pipeTop.Left = 850; // Boru pozisyonunu sıfırla
            scoreText.Text = "Skor: " + score;

            gameTimer.Start(); // Oyun zamanlayıcısını yeniden başlat
        }

    }
}
