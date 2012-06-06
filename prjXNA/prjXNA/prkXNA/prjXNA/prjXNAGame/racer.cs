using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prjXNAGame
{
    class racer : theGame
    {
        //create sprite variables
        sprite car;
        sprite road;
        sprite line1, line2, line3, line4;
        sprite flag;
        //create kid sprites
        sprite kid1, kid2;
        //random variables
        Random rX, rY;
        float timeElapsed;
        text timer;
        bool gameOver;
        text finalStatus;

         public override void gameIni(){
             gameOver = false;
             finalStatus = new text(this, "Arial", "");
             //create random variable objects
             rX = new Random();
             rY = new Random();
             this.setScreenSize(300, 400);
             //there should be no gravity
             this.setGravity(0);

             road = new sprite(this,  "road", 300f, 400f, 150f, 200f);
             road.enableCollision(false);

             flag = new sprite(this,  "flag", 40, 40, 200, -1000);
             flag.enableCollision(false);

          
             line1 = new sprite(this,  "line", 10, 80, 145, 40);
             line1.enableCollision(false);
             line2 = new sprite(this,  "line", 10, 80, 145, 140);
             line2.enableCollision(false);
             line3 = new sprite(this,  "line", 10, 80, 145, 240);
             line3.enableCollision(false);
             line4 = new sprite(this,  "line", 10, 80, 145, 340);
             line4.enableCollision(false);
            
             car = new sprite(this, "car", 30f, 60f, 135f, 200f, FarseerGames.FarseerPhysics.CollisionCategory.Cat3, FarseerGames.FarseerPhysics.CollisionCategory.Cat1);
             //car.enableCollision(false);


             //kids
             
             kid1 = new sprite(this,  "kid2", 30, 30, rX.Next (100,200), rY.Next(20,100),FarseerGames.FarseerPhysics.CollisionCategory.Cat1 ,FarseerGames.FarseerPhysics.CollisionCategory.Cat3 );
             kid2 = new sprite(this,  "kid3", 30, 30, rX.Next(100, 200), rY.Next(20, 100),FarseerGames.FarseerPhysics.CollisionCategory.Cat2 ,FarseerGames.FarseerPhysics.CollisionCategory.Cat4 );
             

             //create text object for timer
             timer = new text(this, "Arial", "0 seconds");

         }
         public override void gameLogic(){
             if (gameOver == false)
             {
                 //time tracker
                 timeElapsed = this.getElapsedTime();
                 timer.changeText(Convert.ToString(timeElapsed) + " seconds");
             }
             //accelerate the car
             if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up) == true)
             {
                 line1.applyForce(0, 25);
                 line2.applyForce(0, 25);
                 line3.applyForce(0, 25);
                 line4.applyForce(0, 25);
                 kid1.applyForce(0, 25);
                 kid2.applyForce(0, 25);
                 flag.applyForce(0, 25);
             }
             //reposition lines when they get off screen
             if (line1.getY() > 440)
             {
                 line1.setPosition(145, -40);
             }
             else if (line2.getY() > 440)
             {
                 line2.setPosition(145, -40);
             }
             else if (line3.getY() > 440)
             {
                 line3.setPosition(145, -40);
             }
             else if (line4.getY() > 440)
             {
                 line4.setPosition(145, -40);
             }
             if (kid1.getY() > 415)
             {
                 kid1.setPosition(rX.Next(100, 200), rY.Next(-200, -100));
             }
             if (kid2.getY() > 415)
             {
                 kid2.setPosition(rX.Next(100, 200), rY.Next(-200, -100));
             }
             
             //apply brakes
             if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down) == true && line1.getVelocityDY ()>0)
             {
                 line1.applyForce(0,-100);
                 line2.applyForce(0,-100);
                 line3.applyForce(0,-100);
                 line4.applyForce(0,-100);
             }
             //move car left and right
             if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left) == true)
             {
                 car.setVelocity(-55, 0);
             }
             else if (this.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right) == true)
             {
                 car.setVelocity(55, 0);
             }
             else
             {
                 car.setVelocity(0, 0);

             }


             //finish line
             if (car.getY() < flag.getY())
             {
                 finalStatus.changeText("Race Completed in " + Convert.ToString(timeElapsed) + " seconds");
                 gameOver = true;
             }
             //hitting pedestrians
             if (car.physicsCollide(kid1) )
             {
                 finalStatus.changeText("GAME OVER-Pedestrian Squashed!!!");
             
             }
             

         }
         public override void gameRender(){
             if (gameOver)
             {
                         }
             else
             {
                

                 road.draw();
                 finalStatus.draw(0, 50, Microsoft.Xna.Framework.Graphics.Color.Red);

                 line1.draw();
                 line2.draw();
                 line3.draw();
                 line4.draw();
                 kid1.draw();
                 kid2.draw();
                 car.draw();
                 timer.draw(10, 10, Microsoft.Xna.Framework.Graphics.Color.Red);
                 flag.draw();
             }

         }
      

    }
}
