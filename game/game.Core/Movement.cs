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
        int x = 50, y = 50, width = 100, height = 100;
        SpriteBatch spriteBatch;
        Rectangle rectangle;
        Texture2D _texture;
        GraphicsDevice deviceGraphics;
        SpriteFont _font;
        Vector2 _position;
       

        public Movement(GraphicsDevice deviceGraphics) { 
            this.deviceGraphics = deviceGraphics;
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

            spriteBatch.Draw(_texture, rectangle, Color.Blue);
            spriteBatch.End();
        }

        public void Update(GameTime gametime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                rectangle.X += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                rectangle.X -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                rectangle.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
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
        public void Update(GameTime gameTime) {
            var teclado = Keyboard.GetState();

            if (teclado.IsKeyDown(Keys.W)) Posicion.Y -= 2;
            if (teclado.IsKeyDown(Keys.S)) Posicion.Y += 2;
            if (teclado.IsKeyDown(Keys.A)) Posicion.X -= 2;
            if (teclado.IsKeyDown(Keys.D)) Posicion.X += 2;
        }
    }
}
