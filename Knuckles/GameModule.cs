using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Media;

namespace Knuckles
{
    class GameModule
    {
        private Dices dice1;
        private Dices dice2;
        private Dices dice3;
        private Dices dice4;
        private Dices dice5;
        private Dices dice6;

        public void SavePlayers(User[] players) // Сохранение пользователей
        {
            var json = File.ReadAllText("../../Resources/Users.json");
            var data = JsonConvert.DeserializeObject<List<User>>(json);

            if (data != null)
            {
                foreach (var item in players)
                {
                    data.Add(item);
                }

                json = JsonConvert.SerializeObject(data);
                File.WriteAllText("../../Resources/Users.json", json);
            }

            json = JsonConvert.SerializeObject(players);
            File.WriteAllText("../../Resources/Users.json", json);
        }

        public User[] AuthorizationPlayers() // Авторизация игроков
        {
            User[] players = new User[2];

            var player1 = AuthPlayer();
            var player2 = AuthPlayer();

            players[0] = player1;
            players[1] = player2;

            return players;
        }

        private User AuthPlayer()
        {
            using (Authorization authorization = new Authorization())
            {
                authorization.ShowDialog();

                return authorization.user;
            }
        }

        public string OneSideFull(List<PictureBox> BottomCells, List<PictureBox> UpCells) // Проверяем заполнился ли какая-нибудь сторона
        {
            if(CheckingСells(BottomCells))
            {
                return "BottomFull";
            }
            else if(CheckingСells(UpCells))
            {
                return "UpFull";
            }

            return string.Empty;
        }

        private bool CheckingСells(List<PictureBox> cells) // Проверяем заполнены ли все ячейки
        {
            foreach(PictureBox cell in cells)
            {
                if(cell.BackgroundImage == null)
                {
                    return false;
                }
            }

            return true;
        }

        public ResultOfTheGame CalculationOfPoints(List<Label> scorings, List<PictureBox> cells, User[] players) // Подсчет очков, для выбора победителя
        {
            if(CheckingСells(cells))
            {
                int sumPlayer1 = 0;
                int sumPlayer2 = 0;

                sumPlayer1 = int.Parse(scorings[0].Text) + int.Parse(scorings[1].Text) + int.Parse(scorings[2].Text);
                sumPlayer2 = int.Parse(scorings[3].Text) + int.Parse(scorings[4].Text) + int.Parse(scorings[5].Text);

                if (sumPlayer1 > sumPlayer2)
                {
                    players[0].money += sumPlayer1;
                    return new ResultOfTheGame(true, sumPlayer1, sumPlayer2);
                }
                players[1].money += sumPlayer2;
                return new ResultOfTheGame(false, sumPlayer1, sumPlayer2);
            }
            else
            {
                return null;
            }
        }

        public void DestroyTheSameOppositeCell(List<PictureBox> cells) // Уничтожаем противоположную такую же ячейку
        {
            for(int i = cells.Count - 1; i >= 1; i--)
            {
                if (cells[0].BackgroundImage == cells[i].BackgroundImage)
                {
                    cells[i].BackgroundImage = null;
                    cells[i].BorderStyle = BorderStyle.FixedSingle;
                    break;
                }
            }
        }

        public bool SetRandomSide(Random rnd, PictureBox stepUp, PictureBox stepBottom) // Установка стороны для первого хода
        {
            int chooseSide = rnd.Next(0, 2);

            if (chooseSide == 1)
            {
                stepUp.Visible = true;
                stepBottom.Visible = false;
                return true;
            }
            stepBottom.Visible = true;
            stepUp.Visible = false;
            return false;
        }

        public void SetRandomDice(List<Dices> dices, Random rnd, PictureBox pickDice) // Установка рандомной костяшки
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "../../Resources/Sounds/Dice.wav";
            player.Play();
            int chooseDice = rnd.Next(0, 6);
            pickDice.BackgroundImage = dices[chooseDice].dice;
        }

        public void ScoringModule(List<PictureBox> cells, List<Label> scorings, List<Dices> dices) // Подсчет очков
        {
            int scoreLeftColumnI = LeftColumnI(cells, dices); // Подсчет очков левого нижнего столбца
            int scoreCenterColumnI = CenterColumnI(cells, dices); // Подсчет очков среднего нижнего столбца
            int scoreRightColumnI = RightColumnI(cells, dices); // Подсчет очков правого нижнего столбца

            int scoreLeftColumnE = LeftColumnE(cells, dices); // Подсчет очков левого верхнего столбца
            int scoreCenterColumnE = CenterColumnE(cells, dices); // Подсчет очков среднего верхнего столбца
            int scoreRightColumnE = RightColumnE(cells, dices); // Подсчет очков среднего верхнего столбца


            scorings[0].Text = scoreLeftColumnE.ToString();
            scorings[1].Text = scoreCenterColumnE.ToString();
            scorings[2].Text = scoreRightColumnE.ToString();

            scorings[3].Text = scoreLeftColumnI.ToString();
            scorings[4].Text = scoreCenterColumnI.ToString();
            scorings[5].Text = scoreRightColumnI.ToString();     
        }

        private int CalculateTheCellValue(List<PictureBox> cells, List<Dices> dices, int min, int max)
        {
            int score = 0;

            for (int i = min; i < max; i++)
            {
                foreach (Dices dice in dices)
                {
                    if (cells[i].BackgroundImage != null)
                    {
                        if (cells[i].BackgroundImage == dice.dice)
                        {
                            int additionScore = dice.value;

                            if (i != min)
                            {
                                if (i == max - 2)
                                {
                                    if (cells[i].BackgroundImage == cells[i - 1].BackgroundImage)
                                    {
                                        score -= dice.value;
                                        additionScore *= dice.value;
                                    }
                                }
                                else if (i == max - 1)
                                {
                                    if (cells[i].BackgroundImage == cells[i - 1].BackgroundImage && cells[i].BackgroundImage == cells[i - 2].BackgroundImage)
                                    {
                                        score -= dice.value;
                                        additionScore *= dice.value * dice.value;
                                    }
                                    else if (cells[i].BackgroundImage == cells[i - 2].BackgroundImage || cells[i].BackgroundImage == cells[i - 1].BackgroundImage)
                                    {
                                        score -= dice.value;
                                        additionScore *= dice.value;
                                    }
                                }
                            }
                            score += additionScore;
                            break;
                        }
                    }
                }
            }

            return score;
        }

        // Подсчет очков для нижнего игрока
        private int LeftColumnI(List<PictureBox> cells, List<Dices> dices)
        {
            return CalculateTheCellValue(cells, dices, 0, 3);
        }
        private int CenterColumnI(List<PictureBox> cells, List<Dices> dices)
        {
            return CalculateTheCellValue(cells, dices, 3, 6);
        }
        private int RightColumnI(List<PictureBox> cells, List<Dices> dices)
        {
            return CalculateTheCellValue(cells, dices, 6, 9);
        }
        
        // Подсчет очков для верхнего игрока
        private int LeftColumnE(List<PictureBox> cells, List<Dices> dices)
        {
            return CalculateTheCellValue(cells, dices, 9, 12);
        }
        private int CenterColumnE(List<PictureBox> cells, List<Dices> dices)
        {
            return CalculateTheCellValue(cells, dices, 12, 15);
        }
        private int RightColumnE(List<PictureBox> cells, List<Dices> dices)
        {
            return CalculateTheCellValue(cells, dices, 15, 18);
        }
    }
}
