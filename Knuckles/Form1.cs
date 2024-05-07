using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Knuckles.Properties;


namespace Knuckles
{
    public partial class Form1 : Form
    {
        private Dices dice1;
        private Dices dice2;
        private Dices dice3;
        private Dices dice4;
        private Dices dice5;
        private Dices dice6;
        private List<Dices> dices;


        private bool opponentMove = false;

        readonly Random rnd = new Random((int)DateTime.Now.Ticks);
        private GameModule gameModule = new GameModule();

        private List<PictureBox> cells;
        private List<Label> scorings;
        private List<PictureBox> cellsDestroy;
        private bool FirstStep = true;
        private User[] players;
        SoundPlayer player = new SoundPlayer();




        public Form1()
        {
            InitializeComponent();

            players = gameModule.AuthorizationPlayers();

            lb_nameMe.Text = players[0].name;
            lb_countMoney.Text = players[0].money.ToString();

            lb_nameEnemy.Text = players[1].name;
            lb_countMoneyEnemy.Text = players[1].money.ToString();

            picb_1_1.AllowDrop = true;
            picb_1_2.AllowDrop = true;
            picb_1_3.AllowDrop = true;
            picb_1_4.AllowDrop = true;
            picb_1_5.AllowDrop = true;
            picb_1_6.AllowDrop = true;
            picb_1_7.AllowDrop = true;
            picb_1_8.AllowDrop = true;
            picb_1_9.AllowDrop = true;

            picb_2_1.AllowDrop = true;
            picb_2_2.AllowDrop = true;
            picb_2_3.AllowDrop = true;
            picb_2_4.AllowDrop = true;
            picb_2_5.AllowDrop = true;
            picb_2_6.AllowDrop = true;
            picb_2_7.AllowDrop = true;
            picb_2_8.AllowDrop = true;
            picb_2_9.AllowDrop = true;

            dice1 = new Dices(Resources.dice_1, 1);
            dice2 = new Dices(Resources.dice_2, 2);
            dice3 = new Dices(Resources.dice_3, 3);
            dice4 = new Dices(Resources.dice_4, 4);
            dice5 = new Dices(Resources.dice_5, 5);
            dice6 = new Dices(Resources.dice_6, 6);

            dices = new List<Dices> { dice1, dice2, dice3, dice4, dice5, dice6 };
            cells = new List<PictureBox>
            {
                picb_1_1,
                picb_1_2,
                picb_1_3,
                picb_1_4,
                picb_1_5,
                picb_1_6,
                picb_1_7,
                picb_1_8,
                picb_1_9,
                picb_2_1,
                picb_2_2,
                picb_2_3,
                picb_2_4,
                picb_2_5,
                picb_2_6,
                picb_2_7,
                picb_2_8,
                picb_2_9,
            };
            scorings = new List<Label>
            {
                lb_point1,
                lb_point2,
                lb_point3,
                lb_point4,
                lb_point5,
                lb_point6,
            };
            

            GameModuleFunc(false);
            if(FirstStep)
            {
                opponentMove = gameModule.SetRandomSide(rnd, picb_stepUp, picb_stepBottom); // Установка стороны для первого хода
                FirstStep = false;
            }
        }

        private void GameModuleFunc(bool opponentMoves) // Игровой модуль; Тута логика
        {
            List<PictureBox> BottomCells = cells.GetRange(cells.Count / 2, cells.Count - cells.Count / 2);
            List<PictureBox> UpCells = cells.GetRange(0, cells.Count - cells.Count / 2);

            if(opponentMoves)
            {
                opponentMove = true;
                picb_stepBottom.Visible = false;
                picb_stepUp.Visible = true;
            }
            else
            {
                opponentMove = false;
                picb_stepBottom.Visible = true;
                picb_stepUp.Visible = false;
            }

            string full = gameModule.OneSideFull(BottomCells, UpCells);
            gameModule.SetRandomDice(dices, rnd, picb_pickDice); // Установка рандомной костяшки
            gameModule.ScoringModule(cells, scorings, dices); // Подсчет очков
            var result = gameModule.CalculationOfPoints(scorings, cells, players);
            
            if(full == "BottomFull")
            {
                opponentMove = false;
                picb_stepBottom.Visible = true;
                picb_stepUp.Visible = false;
            }
            else if(full == "UpFull")
            {
                opponentMove = true;
                picb_stepBottom.Visible = false;
                picb_stepUp.Visible = true;
            }

            if(result != null)
            {
                result.Location = new Point(104, 248);
                result.Width = result.Width + 25;
                Controls.Add(result);
                result.BringToFront();

                gameModule.SavePlayers(players); // Сохранение игроков
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawRectangle();
            DrawLines();
        }

        private void DrawRectangle() // Рисуем прямоугольник
        {
            Pen pen = new Pen(Color.Black);
            Graphics graphics = CreateGraphics();
            graphics.DrawRectangle(pen, new Rectangle(Width / 5, 20, 300, 570));
            pen.Dispose();
            graphics.Dispose();
        }

        private void DrawLines() // Рисуем линии
        {
            Pen pen = new Pen(Color.Black);
            Graphics graphics = CreateGraphics();       
            graphics.DrawRectangle(pen, new Rectangle(Width / 5, 117, 300, 1));
            graphics.DrawRectangle(pen, new Rectangle(Width / 5, 197, 300, 1));
            graphics.DrawRectangle(pen, new Rectangle(Width / 5, 277, 300, 1));

            graphics.DrawRectangle(pen, new Rectangle(Width / 5, 335, 300, 1));
            graphics.DrawRectangle(pen, new Rectangle(Width / 5, 418, 300, 1));
            graphics.DrawRectangle(pen, new Rectangle(Width / 5, 500, 300, 1));
            pen.Dispose();
            graphics.Dispose();
        }
        private void bt_leaderBoard_Click(object sender, EventArgs e)
        {
            player.SoundLocation = "../../Resources/Sounds/ClickButton.wav";
            player.Play();
            Leaderboard leaderboard = new Leaderboard();
            leaderboard.ShowDialog();
        }


        private void picb_pickDice_MouseDown(object sender, MouseEventArgs e)
        {
            //TODO Проверить на дефолтном курсоре
            player.SoundLocation = "../../Resources/Sounds/mouseDown.wav";
            player.Play();
            Bitmap bmp = new Bitmap(picb_pickDice.Width, picb_pickDice.Height);
            picb_pickDice.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));

            bmp.MakeTransparent(Color.White);
            IntPtr ptr = bmp.GetHicon();
            Cursor cur = new Cursor(ptr);
            Cursor.Current = cur;
            picb_pickDice.DoDragDrop(picb_pickDice.BackgroundImage, DragDropEffects.Move);
        }

        private void picb_pickDice_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }

        private void picb_1_1_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_1_1_DragDrop(object sender, DragEventArgs e)
        {
            if(picb_1_1.BackgroundImage == null)
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_1.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_1.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_1, picb_2_1, picb_2_2, picb_2_3 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
                
            }
        }

        private void picb_1_2_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_1_1.BackgroundImage == null)
            {
                picb_1_1_DragDrop(sender, e);
            }
            else
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_2.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_2.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_2, picb_2_1, picb_2_2, picb_2_3 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_1_2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_1_3_DragDrop(object sender, DragEventArgs e)
        {
            if(picb_1_2.BackgroundImage == null)
            {
                picb_1_2_DragDrop(sender, e);
            }
            else
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_3.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_3.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_3, picb_2_1, picb_2_2, picb_2_3 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);
                    
                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_1_3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_1_4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_1_4_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_1_4.BackgroundImage == null)
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_4.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_4.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_4, picb_2_4, picb_2_5, picb_2_6 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_1_5_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_1_4.BackgroundImage == null)
            {
                picb_1_4_DragDrop(sender, e);
            }
            else
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_5.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_5.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_5, picb_2_4, picb_2_5, picb_2_6 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_1_5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_1_6_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_1_5.BackgroundImage == null)
            {
                picb_1_5_DragDrop(sender, e);
            }
            else
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_6.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_6.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_6, picb_2_4, picb_2_5, picb_2_6 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_1_6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_1_7_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_1_7_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_1_7.BackgroundImage == null)
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_7.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_7.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_7, picb_2_7, picb_2_8, picb_2_9 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_1_8_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_1_7.BackgroundImage == null)
            {
                picb_1_7_DragDrop(sender, e);
            }
            else
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_8.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_8.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_8, picb_2_7, picb_2_8, picb_2_9 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_1_8_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_1_9_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_1_8.BackgroundImage == null)
            {
                picb_1_8_DragDrop(sender, e);
            }
            else
            {
                if(!opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_1_9.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_1_9.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_1_9, picb_2_7, picb_2_8, picb_2_9 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = false;
                    picb_stepUp.Visible = true;
                    opponentMove = true;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_1_9_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_1_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_1.BackgroundImage == null)
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_1.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_1.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_1, picb_1_1, picb_1_2, picb_1_3 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_2_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_1.BackgroundImage == null)
            {
                picb_2_1_DragDrop(sender, e);
            }
            else
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_2.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_2.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_2, picb_1_1, picb_1_2, picb_1_3 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_3_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_2.BackgroundImage == null)
            {
                picb_2_2_DragDrop(sender, e);
            }
            else
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_3.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_3.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_3, picb_1_1, picb_1_2, picb_1_3 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_4_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_4.BackgroundImage == null)
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_4.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_4.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_4, picb_1_4, picb_1_5, picb_1_6 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_5_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_4.BackgroundImage == null)
            {
                picb_2_4_DragDrop(sender, e);
            }
            else
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_5.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_5.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_5, picb_1_4, picb_1_5, picb_1_6 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_6_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_5.BackgroundImage == null)
            {
                picb_2_5_DragDrop(sender, e);
            }
            else
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_6.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_6.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_6, picb_1_4, picb_1_5, picb_1_6 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_7_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_7_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_7.BackgroundImage == null)
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_7.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_7.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_7, picb_1_7, picb_1_8, picb_1_9 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_8_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_7.BackgroundImage == null)
            {
                picb_2_7_DragDrop(sender, e);
            }
            else
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_8.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_8.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_8, picb_1_7, picb_1_8, picb_1_9 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_8_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void picb_2_9_DragDrop(object sender, DragEventArgs e)
        {
            if (picb_2_8.BackgroundImage == null)
            {
                picb_2_8_DragDrop(sender, e);
            }
            else
            {
                if(opponentMove)
                {
                    player.SoundLocation = "../../Resources/Sounds/chooseCell.wav";
                    player.Play();
                    picb_2_9.BackgroundImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    picb_2_9.BorderStyle = BorderStyle.None;

                    cellsDestroy = new List<PictureBox> { picb_2_9, picb_1_7, picb_1_8, picb_1_9 };
                    gameModule.DestroyTheSameOppositeCell(cellsDestroy);

                    picb_stepBottom.Visible = true;
                    picb_stepUp.Visible = false;
                    opponentMove = false;
                    GameModuleFunc(opponentMove);
                }
                else
                {
                    MessageBox.Show("Сейчас ход противника");
                }
            }
        }

        private void picb_2_9_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
        }
    }
}
