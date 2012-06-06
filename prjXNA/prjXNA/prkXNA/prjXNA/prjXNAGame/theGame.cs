using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Collections;

using FarseerGames.FarseerPhysics;//PHYSICS
using FarseerGames.FarseerPhysics.Dynamics;//PHYSICS
using FarseerGames.FarseerPhysics.Factories;//PHYSICS


namespace prjXNAGame
{
    public class theGame : Microsoft.Xna.Framework.Game
    {

        public GraphicsDeviceManager graphics;
        public SpriteBatch screen;
        public Color white;
        public Color red;
        public ArrayList sprites;
        public AudioEngine engine;
        public SoundBank soundBank;
        public WaveBank waveBank;
        public PhysicsSimulator ps = new PhysicsSimulator();//PHYSICS
        
        public networker net;
        private Color screenColour = Color.CornflowerBlue;
        private GameTime gameTimeInfo;


        //current keyboard input string variables
        text test;
        String key;
        string theText = "";
        String previousKey;
        Boolean keysPressed;
       
        public theGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Components.Add(new GamerServicesComponent(this));
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            white = Color.White;
            //*******create and build XACT engine files in the project Content\Audio folder (you'll need to make this folder)
            //rename the .xgs file to sounds.xgs
            //make sure the audio and sound bank files have the default names
            //using the XACT engine means you don't need to add sounds to content pipeline
            
            engine = new AudioEngine("..\\..\\..\\Content\\Audio\\win\\sounds.xgs");
            soundBank = new SoundBank(engine, "..\\..\\..\\Content\\Audio\\win\\Sound Bank.xsb");
            waveBank = new WaveBank(engine, "..\\..\\..\\Content\\Audio\\win\\Wave Bank.xwb");
            net=  new networker();
            test = new text(this, "pongText", theText);
         }
        public void setScreenSize(int width, int height)
        {
            this.graphics.PreferredBackBufferWidth = width;
            this.graphics.PreferredBackBufferHeight =height;
            this.graphics.ApplyChanges();
        }
        public void setScreenColour(Color theColour){
            this.screenColour = theColour;
        }
        public void setGravity(float gravity)
        {
            ps.Gravity = new FarseerGames.FarseerPhysics.Mathematics.Vector2(0, gravity);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            screen = new SpriteBatch(GraphicsDevice);
            sprites = new ArrayList();
            gameIni();
            // TODO: use this.Content to load your game content here
        }


        //is to be overriden in users game class
        //all sound, text and sprite loadig to be done here
        //as well as variable initializations
        public virtual void gameIni()
        {

        }


        public void clearInputtedText()
        {
            theText = "";
        }


        public String getInputtedText()
        {
            keysPressed = false;
            foreach (Microsoft.Xna.Framework.Input.Keys value in Enum.GetValues(typeof(Microsoft.Xna.Framework.Input.Keys)))
            {
                if (this.isKeyPressed(value))
                {
                    keysPressed = true;
                    key = Convert.ToString(value);
                    if (key != previousKey)
                        theText += key;
                    previousKey = key;
                }
            }
            if (keysPressed == false)
            {
                previousKey = "";
            }
            return theText;
        }


        private String netData;
        protected override void Update(GameTime gameTime)
        {
            net.Update();
            if (net.SessionState == NetworkSessionState.Playing)
            {
                netData = "";
                netData += Convert.ToString(this.getMouse().X) + "|";
                netData += Convert.ToString(this.getMouse().Y) + "|";
                netData += Convert.ToString(this.getMouse().LeftButton) + "|";
                netData += Convert.ToString(this.getMouse().RightButton) + "|";

                // Send any key pressed to the remote player
                foreach (Keys key in Keyboard.GetState().GetPressedKeys())
                {
                    netData += key.ToString() + "|";
                }

                keysPressed = false;
                foreach (Microsoft.Xna.Framework.Input.Keys value in Enum.GetValues(typeof(Microsoft.Xna.Framework.Input.Keys)))
                {
                    if (this.isKeyPressed(value))
                    {
                        keysPressed = true;
                        key = Convert.ToString(value);
                        if (key != previousKey)
                            theText += key;
                        previousKey = key;
                    }
                }
                if (keysPressed == false)
                {
                    previousKey = "";
                }
                this.test.changeText(theText);



                net.SendMessage(netData);

                // Receive the keys from the remote player
                net.ReceiveMessage();
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            gameLogic();
            ps.Update(gameTime.ElapsedGameTime.Milliseconds * 0.001f);//PHYSICS

            this.gameTimeInfo = gameTime;
            base.Update(gameTime);
        }


        public  String currentKeyboardText()
        {
            return this.theText;
        }


        public float getElapsedTime()
        {
            try
            {
                return gameTimeInfo.TotalGameTime.Seconds;
            }
            catch (Exception e) { return 0; }
        }

        //is to be overriden in users game class
        //all decision making and caclulations to be done here
        public virtual void gameLogic()
        {
            
        }

        //get info about the state of the mouse
        public MouseState getMouse()
        {
            return Mouse.GetState();
        }


        //checks for gameoad button presses
        public Boolean getGamePadButtons(PlayerIndex player, Buttons theButton)
        {
            GamePadState gps = GamePad.GetState(PlayerIndex.One);
            if (gps.IsConnected)
            {
                if (gps.IsButtonDown(theButton))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        //checks for key presses
        public Boolean isKeyPressed(Keys whichKey){
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(whichKey))
                return true;
            else
                return false;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(screenColour);

            // TODO: Add your drawing code here
            screen.Begin();
            gameRender();
            screen.End();
            base.Draw(gameTime);
        }
        //is to be overriden in users game class
        //all sprite rendering to be done here
        public virtual void gameRender(){
            
        }

    }
}
