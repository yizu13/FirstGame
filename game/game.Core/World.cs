using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace game.Core
{
    struct Tile {
        public Rectangle DrawRectangle;
    }
    internal class World
    {
        GraphicsDevice Device;
        SpriteBatch spriteBatch;
        Texture2D Grass;
        Tile[][] Grid;
        Tile[] FloorTiles;
        int tileSize = 16;
        int size;
        Random r = new Random();
        Camera2D camara;

        public World(int size, Texture2D Grass, GraphicsDevice Device)
        {
            this.size = size;
            this.Grass = Grass;
            this.Device = Device;
            spriteBatch = new SpriteBatch(Device);
            camara = new Camera2D();

            FloorTiles = new Tile[4];
            FloorTiles[0] = new Tile();
            FloorTiles[0].DrawRectangle = new Rectangle(240, 16, 16, 16);
            FloorTiles[1] = new Tile();
            FloorTiles[1].DrawRectangle = new Rectangle(240, 16, 16, 16);
            FloorTiles[2] = new Tile();
            FloorTiles[2].DrawRectangle = new Rectangle(224, 32, 16, 16);
            FloorTiles[3] = new Tile();
            FloorTiles[3].DrawRectangle = new Rectangle(240, 32, 16, 16);

            Grid = new Tile[size][];
            for (int i = 0; i < size; i++)
            {
                Grid[i] = new Tile[size];
                for (int j = 0; j < size; j++)
                {
                    Grid[i][j] = FloorTiles[r.Next(FloorTiles.Length)];
                }
            }

        }

        public void Draw() {
            spriteBatch.Begin(transformMatrix: camara.GetTransform());
            for (int i = 0; i < size; i++) {

                for (int j = 0; j < size; j++) {
                   
                    spriteBatch.Draw(Grass, new Rectangle(i * tileSize, j * tileSize, tileSize, tileSize), Grid[i][j].DrawRectangle, Color.White);
                    
                }
            }
            spriteBatch.End();
            
        }

        public void Update(GameTime gametime) { 
            camara.Update(gametime);
        }
    }
}
