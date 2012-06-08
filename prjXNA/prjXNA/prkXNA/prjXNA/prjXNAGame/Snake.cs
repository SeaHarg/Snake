using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace prjXNAGame
{
    class Snake:theGame
    {
        #region Varibles 
        //*************************************************Arrays*************************************************
        sprite[] Border = new sprite[4];
        //*************************************************Sprites*************************************************
        sprite BodySprite;
        sprite Fruit;
        //*************************************************Text*************************************************
        text PointsDisplay;
        text WordScore;
        //*************************************************Objects*************************************************
        Random r;
        //*************************************************Integers*************************************************
        int WhatFruit = 0;
        int Score = 0;
        int speedx = 100;
        
        bool homeScreen, highScreen, gamePLayScreen,Gameover;
        #endregion

        public void TextIni()
        {
            WordScore = new text(this, "pongText", "Score:");
            PointsDisplay = new text(this, "pongText", Convert.ToString(Score));
        }

        public void FruitsIni()
        {
            r = new Random(); 
            WhatFruit = r.Next(1,50);
            Fruit = new sprite(this, "Pineapple", 15, 15, 150, 150);
            Fruit.enableCollision(false);
        }

        public void EdgesIni()
        {
            Border[0] = new sprite(this, "Side", 10, 800, 0, 0);
            Border[1] = new sprite(this, "Side", 800, 10, 0, 0);
            Border[2] = new sprite(this, "Side", 10, 800, 400, 0);
            Border[3] = new sprite(this, "side", 800, 60, 0, 400);
            Border[0].enableCollision(false);
            Border[1].enableCollision(false);
            Border[2].enableCollision(false);
            Border[3].enableCollision(false);

        }
        public void EdgesRender()
        {
            for (int c = 0; c < Border.Length; c++)
            {
                Border[c].draw();
            }
        }
        public void UserIni()
        {
            BodySprite = new sprite(this, "body", 10, 10, 200, 200);
            BodySprite.setVelocity(0, -20);
        }
        public void UserLogic()
        {
            if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
                BodySprite.setVelocity(0,-speedx);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
                BodySprite.setVelocity(0, speedx);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
                BodySprite.setVelocity(-speedx, 0);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
                BodySprite.setVelocity(speedx, 0);

            if (BodySprite.getX() < 10)
            {
                gamePLayScreen = false;
                Gameover = true;
            }
            if (BodySprite.getX() > 390)
            {
                gamePLayScreen = false;
                Gameover = true;
            }
            if (BodySprite.getY() < 10)
            {
                gamePLayScreen = false;
                Gameover = true;
            }
            if (BodySprite.getY() > 385-20)
            {
                gamePLayScreen = false;
                Gameover = true;
            }

            if (BodySprite.pixelCollidesWith(Fruit))
            {
                Score = Score + 10;
                TextIni();
                Fruit.setPosition(r.Next(10 + 8, 390 - 8), r.Next(10 + 8, 350 - 8));
                speedx = speedx+10;
            }

        }

        public override void gameIni()
        {
            base.gameIni();
            this.setScreenSize(400, 400);
            this.setScreenColour(Microsoft.Xna.Framework.Graphics.Color.Purple);

            FruitsIni();
            EdgesIni();
            UserIni();
            TextIni();

            gamePLayScreen = true;
            //homeScreen = true;
        }
        public override void gameLogic()
        {
            base.gameLogic();
            if (homeScreen)
            {


            }
            else if (gamePLayScreen)
            {
                UserLogic();
            }



            
        }
        public override void gameRender()
        {
            base.gameRender();

            if (homeScreen)
            {
                EdgesRender();
            }
            else if (gamePLayScreen)
            {
                EdgesRender();
                BodySprite.draw();
                Fruit.draw();
                PointsDisplay.draw(130, 367, Microsoft.Xna.Framework.Graphics.Color.Blue);
                WordScore.draw(30, 367, Microsoft.Xna.Framework.Graphics.Color.Blue);
            }
            else if (Gameover)
            {
                EdgesRender();
            }


           
           
        }
    }
}
