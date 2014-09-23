using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOne
{
    class Camera
    {

        public Vector2 position { get; set; }
        public Vector2 origin { get; set; }
        public float zoom { get; set; }
        public float rotation { get; set; }

        public Camera(Viewport viewport)
        {
            origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
            zoom = 1.0f;
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-position * parallax, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(zoom, zoom, 1) *
                Matrix.CreateTranslation(new Vector3(origin, 0.0f));
        }
    }
}
