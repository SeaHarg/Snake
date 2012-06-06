using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prjXNAGame
{
    class pong:theGame 
    {
        sprite ball;
        sprite leftPaddle;
        sprite rightPaddle;
        float paddleM = 0;
        text title;
        text playerScore, computerScore;
        int playerPoints=0, computerPoints = 0;
        sprite splash;
        int stages = 0;
        sprite menu;


        public override void gameIni()
        {
            //set screen size
            this.setScreenSize(400, 400);
            //set screen colour
            this.setScreenColour(Microsoft.Xna.Framework.Graphics.Color.Black);
            //there should be no gravity
            this.setGravity(0);

            //create ball sprite
            ball = new sprite(this, "pong",  10, 10, 240, 120);
            leftPaddle = new sprite(this, "pong",  10, 80, 10, 160);
            rightPaddle = new sprite(this, "pong", 10, 80, 380, 160);
            //disable physics collisions
            //otherwise physics engine takes control of paddles response to being hit
            leftPaddle.enableCollision(false);
            rightPaddle.enableCollision(false);
            ball.enableCollision(false);

            //text objects to be used in the game
            title = new text(this, "pongText", "PONG");
            playerScore = new text(this, "pongText", "0");
            computerScore = new text(this, "pongText", "0");

            //sets ball initial velocity
            ball.setVelocity(150, 150);
        }
        public override void gameLogic()
        {

                //ball bounces off walls and paddles
                if (ball.getX() > 390)
                {
                    ball.setVelocity(-ball.getVelocityDX(), ball.getVelocityDY());

                    //update score
                    playerPoints++;
                    //reassign text in playersScore text object to reflect this change
                    playerScore.changeText(Convert.ToString(playerPoints));
                }
                if (ball.getX() < 0)
                {
                    ball.setVelocity(-ball.getVelocityDX(), ball.getVelocityDY());
                    //update score
                    computerPoints++;
                    //reassign text in computerScore text object to reflect this change
                    computerScore.changeText(Convert.ToString(computerPoints));
                }
                if (ball.getY() > 390)
                {
                    ball.setVelocity(ball.getVelocityDX(), -ball.getVelocityDY());
                }
                if (ball.getY() < 0)
                {
                    ball.setVelocity(ball.getVelocityDX(), -ball.getVelocityDY());
                }
                if (ball.pixelCollidesWith(leftPaddle))
                {
                    ball.setVelocity(-ball.getVelocityDX(), ball.getVelocityDY());
                }


                //left player moves using Q and A
                if (ball.pixelCollidesWith(rightPaddle))
                {
                    ball.setVelocity(-ball.getVelocityDX(), ball.getVelocityDY());
                }
                if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Q))
                {
                    leftPaddle.setVelocity(0, -100);
                }
                else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
                {
                    leftPaddle.setVelocity(0, 100);
                }
                else
                {
                    leftPaddle.setVelocity(0, 0);
                }



                //autonomouse computer paddle on right
                paddleM = rightPaddle.getY() + rightPaddle.getHeight() / 2;
                if (ball.getY() < paddleM && ball.getX() > 150)
                {
                    rightPaddle.setVelocity(0, -170);
                }
                else if (ball.getY() > paddleM && ball.getX() > 150)
                {
                    rightPaddle.setVelocity(0, 170);
                }
                else
                {
                    rightPaddle.setVelocity(0, 0);
                }

            


            
        }
        public override void gameRender()
        {

                ball.draw();
                leftPaddle.draw();
                rightPaddle.draw();
                title.draw((int)(200 - title.getLength() / 2), 10, Microsoft.Xna.Framework.Graphics.Color.White);
                playerScore.draw(0, 10, Microsoft.Xna.Framework.Graphics.Color.White);
                computerScore.draw((int)(400 - computerScore.getLength2()), 10, Microsoft.Xna.Framework.Graphics.Color.White);
            
        }

    }
}
