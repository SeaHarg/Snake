using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace prjXNAGame
{
    class exampleGame:theGame 
    {
        sprite ball;
        sprite ball2;
        sound s1;
        text t1;
        

        public exampleGame():base()
        {
            
        }
        public override void gameIni(){
            ball = new spriteCircle(this,"ball","ball1" ,50, 50, 25, 25);
            ball2 = new spriteCircle(this, "ball","ball2", 20, 20, 200, 200);
            sprites.Add(ball);
            sprites.Add(ball2);
            s1 = new sound(this, "ring");
            //this.ps.Gravity = new FarseerGames.FarseerPhysics.Mathematics.Vector2 (0f,9.84f);//PHYSICS
            t1 = new text(this, "testFont", "This is a test");

            this.ps.Gravity = new FarseerGames.FarseerPhysics.Mathematics.Vector2(0f, 9.81f);
            


        }
        public  override void gameLogic(){




            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.X))
            {
                ball2.applyImpulse(0, 0.01f);
            }

            //ball.move(1,1);
            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                ball2.applyForce(0, -17);
                //soundBank.PlayCue("ring");
            }
            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                ball2.applyForce(0, 17);
                //soundBank.PlayCue("ring");
            }
            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                ball2.applyForce(-17,0);
                //soundBank.PlayCue("ring");
            }
            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                ball2.applyForce(17,0);
                //soundBank.PlayCue("ring");
            }
            if(isKeyPressed (Microsoft.Xna.Framework.Input.Keys.D )){
                net.SignInGamer ();
            }
            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.C))
            {
                net.CreateSession();
            }
            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.G))
            {
                net.FindSession();
            }


            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.A ))
            {
                ball.getBody().IsStatic = true;

                //soundBank.PlayCue("ring");
            }


        if (getGamePadButtons(Microsoft.Xna.Framework.PlayerIndex.One, Microsoft.Xna.Framework.Input.Buttons.A))
            ball2.applyForce (-1,-1);

        if (ball.physicsCollide(ball2))
        {
            //ball.height = 10;
            //ball.width = 10;
        }
           
            if(ball.simpleCollidesWith (ball2 )){
           //     ball.height =100;
           //     s1.play();
            }
        }
        public  override void gameRender(){
            //screen.Draw(ball.asset , ball.position, white);
            ball.draw();
            ball2.draw();
            t1.draw(20, 20, Microsoft.Xna.Framework.Graphics.Color.Red);
            t1 = new text(this, "testFont", net.Message);
        }

    }
}
