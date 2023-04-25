using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebroneConstant.Snake
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelScore;
        private int dirX, dirY;
        private int _width = 900;
        private int _height = 800;
        private int _sizeOfSides = 40;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Snake";
            this.Width = _width;
            this.Height = _height;
            dirX = 1;
            dirY = 0;
            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Location = new Point(810, 10);
            this.Controls.Add(labelScore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(_sizeOfSides - 1, _sizeOfSides - 1);
            snake[0].BackColor = Color.Red;
            this.Controls.Add(snake[0]);
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(_sizeOfSides, _sizeOfSides);
            _generateMap();
            _generateFruit();
            timer.Tick += new EventHandler(_update);
            timer.Interval = 200;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }

        private void _eatFruit()
        {
            if (snake[0].Location.X == rI &&
                snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;

                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + _sizeOfSides * dirX, snake[score - 1].Location.Y - _sizeOfSides * dirY);
                snake[score].Size = new Size(_sizeOfSides - 1, _sizeOfSides - 1);
                snake[score].BackColor = Color.Red;
                this.Controls.Add(snake[score]);
                _generateFruit();
            }
        }

        private void _generateMap()
        {
            for (int x = 0; x < _width / _sizeOfSides; x++)
            {                
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSides * x);
                pic.Size = new Size(_width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int y = 0; y <= _height / _sizeOfSides; y++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSides * y, 0);
                pic.Size = new Size(1, _height);
                this.Controls.Add(pic);
            }
        }

        private void _generateFruit()
        {
            Random r = new Random();
            rI = r.Next(0, _height - _sizeOfSides);
            int tempI = rI % _sizeOfSides;
            rI -= tempI;
            rJ = r.Next(0, _height - _sizeOfSides);
            int tempJ = rJ % _sizeOfSides;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }

        private void _checkBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                dirX = 1;
                labelScore.Text = "Score: " + score;
            }
            if (snake[0].Location.X > _height)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                dirX = -1;
                labelScore.Text = "Score: " + score;
            }
            if (snake[0].Location.Y < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                dirY = 1;
                labelScore.Text = "Score: " + score;
            }
            if (snake[0].Location.Y > _height)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                dirY = -1;
                labelScore.Text = "Score: " + score;
            }
        }
        private void _eatItSelf()
        {
            for (int _i = 1; _i < score; _i++)
            {
                if (snake[0].Location == snake[_i].Location)
                {
                    for (int _j = _i; _j <= score; _j++)
                    {
                        this.Controls.Remove(snake[_j]);
                    }

                    score = score - (score - _i + 1);
                    labelScore.Text = "Score: " + score;
                }
            }
        }

        private void _moveSnake()
        {
            for (int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }

            snake[0].Location = new Point(snake[0].Location.X + dirX * _sizeOfSides, snake[0].Location.Y + dirY * _sizeOfSides);
        }

        private void _update(Object MyObject, EventArgs eventArgs)
        {
            _checkBorders();
            _eatItSelf();
            _eatFruit();
            _moveSnake();            
        }

        private void OKP(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            switch (e.KeyCode.ToString())
            {
                case "D":
                case "Right":
                    //cube.Location = new Point(cube.Location.X + _sizeOfSides, cube.Location.Y);
                    dirX = 1;
                    dirY = 0;
                    break;
                case "A":
                case "Left":
                    //cube.Location = new Point(cube.Location.X - _sizeOfSides, cube.Location.Y);
                    dirX = -1;
                    dirY = 0;
                    break;
                case "W":
                case "Up":
                    //cube.Location = new Point(cube.Location.X, cube.Location.Y - _sizeOfSides);
                    dirX = 0;
                    dirY = -1;
                    break;
                case "S":
                case "Down":
                    //cube.Location = new Point(cube.Location.X, cube.Location.Y + _sizeOfSides);
                    dirX = 0;
                    dirY = 1;
                    break;
                default:
                    break;
            }
        }
    }
}
