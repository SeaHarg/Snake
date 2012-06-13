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
        sprite[] Body = new sprite[100];
        //*************************************************Sprites*************************************************
        sprite BodySprite;
        sprite Fruit;
        sprite GameoverSprite;
        //*************************************************Text*************************************************
        text PointsDisplay;
        text WordScore;
        //*************************************************Objects*************************************************
        Random r;
        //*************************************************Integers*************************************************
        int WhatFruit = 0;
        int Score = 0;
        int speedx = 100;
        
        bool homeScreen, highScreen, gamePLayScreen,Gameover,bordersareon;
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
        public void Borders(bool DrawMeMaybe)
        {
            if (DrawMeMaybe == true)
            {
                Border[0] = new sprite(this, "Side", 10, 800, 0, 0);
                Border[1] = new sprite(this, "Side", 800, 10, 0, 0);
                Border[2] = new sprite(this, "Side", 10, 800, 400, 0);
                Border[3] = new sprite(this, "side", 800, 60, 0, 400);
                Border[0].enableCollision(false);
                Border[1].enableCollision(false);
                Border[2].enableCollision(false);
                Border[3].enableCollision(false);
                bordersareon = true;
            }
            else
            {
                Border[3] = new sprite(this, "side", 800, 60, 0, 400);
                Border[3].enableCollision(false);
                bordersareon = false;
            }

        }
        public void BordersRender()
        {
            for (int c = 0; c < Border.Length; c++)
            {
                Border[c].draw();
            }
        }
        public void UserIni()
        {
            Body[0] = new sprite(this, "body", 10, 10, 200, 200);
            Body[0].setVelocity(0, speedx);
            for (int c = 0; c < Body.Length; c++)
            {
                Body[c] = new sprite(this, "body", 10, 10, 200, 180);
                Body[c].enableCollision(false);
            }
        }
        public void UserLogic()
        {
            #region FOllowing the head
            for (int c = Body.Length - 1; c >= 1; c--)
                Body[c].setPosition (Body[c - 1].getX(),Body [c-1].getY ());
            #endregion
            #region Moving the head
            if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
                Body[0].setVelocity(0, -speedx);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
                Body[0].setVelocity(0, speedx);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
                Body[0].setVelocity(-speedx,0);
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
                Body[0].setVelocity(speedx,0);
            #endregion
            #region Colliding with the walls
            if (bordersareon == true)
            {

                if (Body[0].getX() < 10)
                {
                    gamePLayScreen = false;
                    Gameover = true;
                }
                if (Body[0].getX() > 390)
                {
                    gamePLayScreen = false;
                    Gameover = true;
                }
                if (Body[0].getY() < 10)
                {
                    gamePLayScreen = false;
                    Gameover = true;
                }
                if (Body[0].getY() > 385 - 20)
                {
                    gamePLayScreen = false;
                    Gameover = true;
                }
            }
            else
            {
                if (Body[0].getX() < 10)
                {
                    Body[0].setPosition(400, Body[0].getY());
                }
                if (Body[0].getX() > 390)
                {
                    Body[0].setPosition(10, Body[0].getY());
                }
                if (Body[0].getY() < 10)
                {
                    Body[0].setPosition(Body[0].getX(), 400);
                }
                if (Body[0].getY() > 385 - 20)
                {
                    Body[0].setPosition(Body[0].getX(), 10);
                }
            }
            #endregion
            #region Fruit Collision
            if (Body[0].physicsCollide(Fruit))
            {
                Score = Score + 10;
                TextIni();
                Fruit.setPosition(r.Next(10 + 8, 390 - 8), r.Next(10 + 8, 350 - 8));
                speedx = speedx+10;
            }
            #endregion
        }
        public void UserRender()
        {
            for (int c = 0; c < Body.Length; c++)
            {
                Body[c].draw();
            }
        }
        public void GameoverIni()
        {
            GameoverSprite = new sprite(this, "gameover", 747 / 3, 123 / 3, 200, 100);
            GameoverSprite.enableCollision(false);
        }
        public void GameoverRender()
        {
            //GameoverSprite.draw();
        }
        public override void gameIni()
        {
            base.gameIni();
            this.setScreenSize(400, 400);
            this.setScreenColour(Microsoft.Xna.Framework.Graphics.Color.Purple);
            GameoverIni();
            FruitsIni();
            Borders(true);
            UserIni();
            TextIni();

            gamePLayScreen = true;
            //homeScreen = true;
        }
        //double timer = 0;
        public override void gameLogic()
        {
            base.gameLogic();
            if (homeScreen)
            {


            }
            else if (gamePLayScreen)
            {
                //timer = timer + 0.25;
                //if (timer > 3)
                
                    UserLogic();
                    //timer = 0;
                
            }



            
        }
        public override void gameRender()
        {
            base.gameRender();

            if (homeScreen)
            {
                BordersRender();
            }
            else if (gamePLayScreen)
            { 
                UserRender();
                BordersRender();
                Fruit.draw();
                PointsDisplay.draw(130, 367, Microsoft.Xna.Framework.Graphics.Color.Blue);
                WordScore.draw(30, 367, Microsoft.Xna.Framework.Graphics.Color.Blue);
            }
            else if (Gameover)
            {
                BordersRender();
                GameoverSprite.draw();
                //GameoverRender();

            }


           
           
        }
    }
}
