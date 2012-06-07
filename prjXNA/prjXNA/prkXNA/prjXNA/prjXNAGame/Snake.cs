using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prjXNAGame
{
    class Snake:theGame
    {
        sprite[] Border = new sprite[4];
        List<sprite> Body = new List<sprite>();
        sprite BodySprite;
        sprite Fruit;
        Random Fruity;
        
        int WhatFruit = 0;
        int Score = 0;

        public void FruitsIni()
        {
            Fruity = new Random(); 
            WhatFruit = Fruity.Next(1,50);
            Fruit = new sprite(this, "Pineapple", 15, 15, 150, 150);
        }

        public void EdgesIni ()
        {
            Border[0] = new sprite(this, "Side", 10, 800, 0, 0);
            Border[1] = new sprite(this, "Side", 800, 10, 0, 0);
            Border[2] = new sprite(this, "Side", 10, 800, 400, 0);
            Border[3] = new sprite(this, "side", 800, 50, 0, 400);
            Border[0].enableCollision(false);
            Border[1].enableCollision(false);
            Border[2].enableCollision(false);
            Border[3].enableCollision(false);

        }
        public void UserIni()
        {
            BodySprite = new sprite(this, "body", 10, 10, 200, 200);
            BodySprite.setVelocity(0, -10);
        }
        public void UserLogic()
        {
            if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
                BodySprite.setVelocity(0,-20);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
                BodySprite.setVelocity(0, 20);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
                BodySprite.setVelocity(-20, 0);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
                BodySprite.setVelocity(20, 0);

            if (BodySprite.pixelCollidesWith(Border[0]))
                BodySprite.setVelocity(0, 0);
            if (BodySprite.pixelCollidesWith(Border[1]))
                BodySprite.setVelocity(0, 0);
            if (BodySprite.pixelCollidesWith(Border[2]))
                BodySprite.setVelocity(0, 0);
            if (BodySprite.pixelCollidesWith(Border[3]))
                BodySprite.setVelocity(0, 0);

            if (BodySprite.pixelCollidesWith(Fruit))
            {
                Score++;
                Fruit.setPosition
            }

        }

        public override void gameIni()
        {
            base.gameIni();
            this.setScreenSize(400, 400);
            this.setScreenColour(Microsoft.Xna.Framework.Graphics.Color.Green);

            FruitsIni();
            EdgesIni();
            UserIni();
        }
        public override void gameLogic()
        {
            base.gameLogic();
            UserLogic();
        }
        public override void gameRender()
        {
            base.gameRender();
        
            for (int c = 0; c < Border.Length; c++)
            {
                Border[c].draw();
            }
            BodySprite.draw();
            Fruit.draw();
        }
    }
}
