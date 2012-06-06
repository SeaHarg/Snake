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

namespace prjXNAGame
{
    public class theGame : Microsoft.Xna.Framework.Game
    {

        public GraphicsDeviceManager graphics;
        public SpriteBatch screen;
        public Color white;
        public ArrayList sprites;
        public AudioEngine engine;
        public SoundBank soundBank;
        public WaveBank waveBank;

       
       
        public theGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

             

        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            screen = new SpriteBatch(GraphicsDevice);
            sprites = new ArrayList();
           



            

            gameIni();
            // TODO: use this.Content to load your game content here
        }

        public virtual void gameIni()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
            // TODO: Add your update logic here
            gameLogic();
            base.Update(gameTime);
        }

        public virtual void gameLogic()
        {

        }
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



        public Boolean isKeyPressed(Keys whichKey){
                 
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(whichKey))
                return true;
            else
                return false;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            screen.Begin();
            gameRender();
            screen.End();
            base.Draw(gameTime);
        }

        public virtual void gameRender(){
            
        }
       
        


    }
}
