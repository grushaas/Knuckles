using System;
using System.Drawing;

namespace Knuckles
{
    class Dices
    {
        public Bitmap dice {  get; private set; }
        public int value { get; private set; }

        public Dices(Bitmap dice, int value) 
        {
            this.dice = dice;
            this.value = value;
        }
    }
}
