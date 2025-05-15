using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using System.Security.Principal;

namespace game.Core
{
    internal class Movement 
    {
        int x = 50, y = 50, width = 40, height = 80;
        SpriteBatch spriteBatch;
        Rectangle rectangle;
        Texture2D _texture;
        GraphicsDevice deviceGraphics;
        SpriteFont _font;
        Vector2 _position;
        World world;
       

        public Movement(GraphicsDevice deviceGraphics, World world) { 
            this.deviceGraphics = deviceGraphics;
            this.world = world;
        }
        public void LoadContent(SpriteFont font) {

            spriteBatch = new SpriteBatch(deviceGraphics);

            rectangle = new Rectangle(x, y, width, height);

            _texture = new Texture2D(deviceGraphics, 1, 1);

            _texture.SetData(new Color[] { Color.DarkSlateGray });

            _position = new Vector2(10, 10);
            _font = font;
        }

        public void Draw(GameTime gametime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, $"X:{rectangle.Left} Y:{rectangle.Top}", _position, Color.Black);
            spriteBatch.DrawString(_font, $"{world.worldSize}", new Vector2(10, 30) ,Color.Black);

            spriteBatch.Draw(_texture, rectangle, Color.Blue);
            spriteBatch.End();
        }

        public void Update(GameTime gametime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.D) && (world.worldSize.X > rectangle.X))
            {
                rectangle.X += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && rectangle.X > 0)
            {
                rectangle.X -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && rectangle.Y > 0)
            {
                rectangle.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && (world.worldSize.Y > rectangle.Y))
            {
                rectangle.Y += 2;
            }

        }

    }

    public class Camera2D
    {
        public Vector2 Posicion = new Vector2(30, 40);
        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(new Vector3(-Posicion, 0));
        }
        public void Update(GameTime gameTime, Rectangle worldSize) {
            var teclado = Keyboard.GetState();

            if (teclado.IsKeyDown(Keys.W) && Posicion.Y > 0) Posicion.Y -= 2;
            if (teclado.IsKeyDown(Keys.S) && (worldSize.Y /2) > Posicion.Y) Posicion.Y += 2;
            if (teclado.IsKeyDown(Keys.A) && Posicion.X > 0) Posicion.X -= 2;
            if (teclado.IsKeyDown(Keys.D) && (worldSize.X /2) > Posicion.X) Posicion.X += 2;
        }
    }
}
