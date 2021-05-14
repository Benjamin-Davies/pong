using System;
using System.Drawing;
using System.Windows.Forms;

class Pong {
  static void Main() {
    var rand = new Random();

    int ballx = 400;
    int bally = 300;
    int speedx = (2 * rand.Next(2) - 1) * 10;
    int speedy = rand.Next(-10, 10);

    int paddle1y = 250;
    int paddle2y = 250;

    int score1 = 0;
    int score2 = 0;

    var form = new Form {
      Width = 800,
      Height = 600,
      FormBorderStyle = FormBorderStyle.FixedSingle,
    };

    var timer = new Timer {
      Interval = 30,
      Enabled = true,
    };

    form.KeyDown += (s, e) => {
      switch (e.KeyCode) {
        case Keys.Q:
          paddle1y -= 20;
          if (paddle1y < 0) paddle1y = 0;
          break;
        case Keys.A:
          paddle1y += 20;
          if (paddle1y > 500) paddle1y = 500;
          break;
        case Keys.Up:
          paddle2y -= 20;
          if (paddle2y < 0) paddle2y = 0;
          break;
        case Keys.Down:
          paddle2y += 20;
          if (paddle2y > 500) paddle2y = 500;
          break;
      }
    };

    form.Paint += (s, e) => {
      var g = e.Graphics;

      g.FillRectangle(Brushes.Black, 0, 0, 800, 600);
      g.FillEllipse(Brushes.White, ballx - 5, bally - 5, 10, 10);
      g.FillRectangle(Brushes.White, 10, paddle1y, 5, 100);
      g.FillRectangle(Brushes.White, 780, paddle2y, 5, 100);

      g.DrawString($"Score: {score1} : {score2}", form.Font, Brushes.White, 20, 20);
    };

    timer.Tick += (s, e) => {
      ballx += speedx;
      bally += speedy;

      if (bally < 0 || bally > 570) {
        speedy *= -1;
      }

      if (ballx < 15 && bally > paddle1y && bally < paddle1y + 100) {
        ballx = 15;
        speedx *= -1;
      }
      if (ballx > 780 && bally > paddle2y && bally < paddle2y + 100) {
        ballx = 780;
        speedx *= -1;
      }

      if (ballx < 0 || ballx > 800) {
        if (ballx > 0)
          score1++;
        else
          score2++;

        ballx = 400;
        bally = 300;
        speedx = (2 * rand.Next(2) - 1) * 10;
        speedy = rand.Next(-10, 10);
      }

      form.Invalidate();
    };

    Application.Run(form);
  }
}
