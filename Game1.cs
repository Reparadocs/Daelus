#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Mercado;
#endregion

namespace Daelus
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch SpriteBatch;
        MercadoEngine Mercado;

        Texture2D groundT;
        Texture2D playerT;

        PhysicsGameObject player;
        List<PhysicsGameObject> ground;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Mercado = new MercadoEngine();

            ServiceProvider.Initialize(Services);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Mercado.RegisterServices(SpriteBatch, new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
    
            groundT = Content.Load<Texture2D>("grassMid.png");
            playerT = Content.Load<Texture2D>("boxCoin.png");
            ground = new List<PhysicsGameObject>();
            for(int i = 0; i < 10; i++)
            {
                ground.Add(new PhysicsGameObject(this, groundT, new Vector2(i*64,500), true));
                ground.Add(new PhysicsGameObject(this, groundT, new Vector2(640 + i * 64, 564), true));
            }
            ground.Add(new PhysicsGameObject(this, groundT, new Vector2(384, 436), true));
            ground.Add(new PhysicsGameObject(this, groundT, new Vector2(256, 436), true));
            ground.Add(new PhysicsGameObject(this, groundT, new Vector2(384, 372), true));
            ground.Add(new PhysicsGameObject(this, groundT, new Vector2(128, 300), true));

            player = new PhysicsGameObject(this, playerT, Vector2.Zero, 7.0f);
            SetInput();
            RegisterComponents();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Mercado.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();
            base.Draw(gameTime);
            SpriteBatch.End();
        }

        private void RegisterComponents()
        {
            Components.Add(player);
            foreach(PhysicsGameObject g in ground)
            {
                Components.Add(g);
            }

        }

        private void SetInput()
        {
            InputManager.gameObject = player;
            InputManager.keyCommands.Add(Keys.D, new MoveCommand(new Vector2(3, 0)));
            InputManager.keyCommands.Add(Keys.A, new MoveCommand(new Vector2(-3, 0)));
            InputManager.keyCommands.Add(Keys.Space, new JumpCommand(-300.0f));
            InputManager.buttonCommands.Add(Buttons.A, new JumpCommand(-300.0f));
            InputManager.leftStick = new MoveCommand(new Vector2(3, 0));
        }
    }
}
