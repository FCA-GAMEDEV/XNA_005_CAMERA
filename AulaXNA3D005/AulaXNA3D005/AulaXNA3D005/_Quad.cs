using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AulaXNA3D005
{
    public class _Quad
    {
        Game game;
        GraphicsDevice device;
        Matrix world;
        VertexPositionTexture[] verts;
        VertexBuffer buffer;
        BasicEffect effect;
        Texture2D texture;

        public _Quad(GraphicsDevice device, Game game, string textureName)
        {
            this.game = game;
            this.device = device;
            this.world = Matrix.Identity;

            this.verts = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(-1, 1,0),Vector2.Zero),  //v0
                new VertexPositionTexture(new Vector3( 1, 1,0),Vector2.UnitX), //v1
                new VertexPositionTexture(new Vector3(-1,-1,0),Vector2.UnitY), //v2

                new VertexPositionTexture(new Vector3( 1, 1,0),Vector2.UnitX), //v3
                new VertexPositionTexture(new Vector3( 1,-1,0),Vector2.One),   //v4
                new VertexPositionTexture(new Vector3(-1,-1,0),Vector2.UnitY), //v5
            };

            this.buffer = new VertexBuffer(this.device,
                                           typeof(VertexPositionTexture),
                                           this.verts.Length,
                                           BufferUsage.None);
            this.buffer.SetData<VertexPositionTexture>(this.verts);

            this.effect = new BasicEffect(this.device);

            this.texture = this.game.Content.Load<Texture2D>(textureName);
        }

        public void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(_Camera camera)
        {
            this.device.SetVertexBuffer(this.buffer);

            this.effect.World = this.world;
            this.effect.View = camera.GetView();
            this.effect.Projection = camera.GetProjection();
            this.effect.TextureEnabled = true;
            this.effect.Texture = this.texture;

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList,
                                                                    this.verts, 0, 2);
            }
        }
    }
}
