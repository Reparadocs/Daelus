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
        PhysicsService PhysicsService;
        Camera Camera;

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
            ServiceProvider.Initialize(Services);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            PhysicsService = new PhysicsService();
            Camera = new Camera(Vector2.Zero, new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            RegisterServices();
            groundT = Content.Load<Texture2D>("grassMid.png");
            playerT = Content.Load<Texture2D>("boxCoin.png");
            ground = new List<PhysicsGameObject>();
            for(int i = 0; i < 10; i++)
            {
                ground.Add(new PhysicsGameObject(this, groundT, new Vector2(i*64,500)));
            }
            player = new PhysicsGameObject(this, playerT, Vector2.Zero, 7.0f);

            RegisterComponents();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();
            base.Draw(gameTime);
            SpriteBatch.End();
        }

        private void RegisterServices()
        {
            ServiceProvider.AddService<SpriteBatch>(SpriteBatch);
            ServiceProvider.AddService<PhysicsService>(PhysicsService);
            ServiceProvider.AddService<Camera>(Camera);
        }

        private void RegisterComponents()
        {
            foreach(PhysicsGameObject g in ground)
            {
                Components.Add(g);
            }
            Components.Add(player);
        }
    }
}
