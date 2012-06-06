using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prjXNAGame
{
    class beachVolleyball:theGame 
    {
        sprite sand;
        sprite wallLeft, wallRight;

        sprite leftShell,rightShell;
        sprite ball;
        sprite bottomPost;
        text state;

        text leftScore, rightScore;
        int leftPoints=0, rightPoints=0;

        public override void gameIni()
        {
            //set the size of the screen
            this.setScreenSize(400, 400);
            //create the floor (use an image of sand)
            sand = new sprite(this,  "sand", 400, 100,200,450);
            //don't let the floor move
            sand.makeStatic(true);
            

            //create the left wall and don't let it move
            wallLeft = new sprite(this, "bottomPost", 5, 400, 2, 200);
            wallLeft.makeStatic(true);

            //create the right wall and on't let it move
            wallRight = new sprite(this, "bottomPost", 5, 400, 398, 200);
            wallRight.makeStatic(true);

            //create the left shell and don't allow it to be rotated
            leftShell = new sprite(this, "shell",  50, 50, 60, 310);
            leftShell.allowRotation(false);

            //create the right shell and don't allow it to be rotated
            rightShell = new sprite(this, "shell2", 50, 50, 340, 310);
            rightShell.allowRotation(false);

            //create the ball
            ball = new sprite(this, "volleyball",  40, 40, 60, 190);
            //give it a mass that is less than the shells (by default all sprites are given a mass of 1)
            ball.setMass(0.1f);
            //set the balls bounciness (ranges from 0 -no bounve to 1-perfect bounce)
            ball.setBounciness(1);


            //create the post and make it immovable
            bottomPost = new sprite(this, "bottomPost", 10, 200, 200, 350);
            bottomPost.makeStatic(true);


            //set the gravity-tis value affects the speed at which objects are pulled to the ground
            this.setGravity(190.81f);


            //create text objects
            state = new text(this, "testFont", "Beach Volleyball");
            leftScore = new text(this, "testFont", "0");
            rightScore = new text(this, "testFont", "0");
        }

        

        public override void gameLogic()
        {
            
            //control left shell
            if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.W) && Math.Round (leftShell.getVelocityDY ())==0)
            {
                leftShell.setVelocity(leftShell.getVelocityDX(), -191f);
            }
            if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
            {
                leftShell.setVelocity(-55, leftShell.getVelocityDY());
            }
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.D))
            {
                leftShell.setVelocity(55, leftShell.getVelocityDY());
            }
            else
            {
                leftShell.setVelocity(0, leftShell.getVelocityDY());
            }

           

            
            //control right shell
            if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up) && Math.Round(rightShell.getVelocityDY()) == 0)
            {
                rightShell.setVelocity(rightShell.getVelocityDX(), -191f);
            }
            if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                rightShell.setVelocity(-55, rightShell.getVelocityDY());
            }
            else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                rightShell.setVelocity(55, rightShell.getVelocityDY());
            }
            else
            {
                rightShell.setVelocity(0, rightShell.getVelocityDY());
            }

            //check ball state
            if (ball.physicsCollide(sand) && ball.getX() < 200)
            {
                //update score
                rightPoints++;
                //change score text to reflect this
                rightScore.changeText(Convert.ToString(rightPoints));
                //put ball above winner
                ball.setPosition(340, 100);
                //put both shells back at their start position
                rightShell.setPosition(340, 340);
                leftShell.setPosition(40, 340);
                //set ball's velocity to 0 so that it just falls down
                ball.setVelocity(0, 0);
            }
            if (ball.physicsCollide(sand) && ball.getX() > 200)
            {
                leftPoints++;
                leftScore.changeText(Convert.ToString(leftPoints));
                ball.setPosition(60, 100);
                rightShell.setPosition(340, 340);
                leftShell.setPosition(40, 340);
                ball.setVelocity(0, 0);
            }
            


        }
        public override void gameRender()
        {
            //draw everything
            sand.draw();
            wallLeft.draw();
            wallRight.draw();
            leftShell.draw();
            rightShell.draw();
            bottomPost.draw();
            ball.draw();
            leftScore.draw(10, 10, Microsoft.Xna.Framework.Graphics.Color.Red);
            rightScore.draw((int)(400-rightScore.getLength2 ()), 10, Microsoft.Xna.Framework.Graphics.Color.Red);
            state.draw((int)(200-state.getLength2 ()/2), 10, Microsoft.Xna.Framework.Graphics.Color.Red);
        }





    }
}
