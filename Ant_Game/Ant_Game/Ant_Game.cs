using System;
using System.Collections.Generic;
using System.Net.Mime;
using FarseerPhysics.Dynamics;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace Ant_Game;

public class Ant_Game : PhysicsGame
{
    Image StartAnt = LoadImage("Muurahainen.png");
    Image taustakuva = LoadImage("Maa.png");
    private Image Leaf = LoadImage("Lehti.png");
    private PhysicsObject startant;
    public override void Begin()
    {
        Kentta();

        Camera.Follow(startant);
        
        Keyboard.Listen(Key.A, ButtonState.Down, LiikutaPelaajaa, null, new Vector(-1000, 0));
        Keyboard.Listen(Key.D, ButtonState.Down, LiikutaPelaajaa, null, new Vector(1000, 0));
        Keyboard.Listen(Key.W, ButtonState.Down, LiikutaPelaajaa, null, new Vector(0, 1000));
        Keyboard.Listen(Key.S, ButtonState.Down, LiikutaPelaajaa, null, new Vector(0, -1000));
        
        Keyboard.Listen(Key.A, ButtonState.Released, LiikutaPelaajaa, null, Vector.Zero);
        Keyboard.Listen(Key.D, ButtonState.Released, LiikutaPelaajaa, null, Vector.Zero);
        Keyboard.Listen(Key.W, ButtonState.Released, LiikutaPelaajaa, null, Vector.Zero);
        Keyboard.Listen(Key.S, ButtonState.Released, LiikutaPelaajaa, null, Vector.Zero);

        
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    
    void LiikutaPelaajaa(Vector vektori)
    {
        
        
        startant.Velocity = vektori;
        startant.MaxVelocity = 200;

        if (vektori.Y < 0)
        {
            startant.Angle = Angle.StraightAngle;
        }
        else if (vektori.Y > 0)
        {
            startant.Angle = Angle.Zero;
        }
        else if (vektori.X < 0)
        {
            startant.Angle = Angle.RightAngle;
        }
        else if (vektori.X > 0)
        {
            startant.Angle = Angle.FromDegrees(-90);
        }
    }
    
    void Kentta()
    {
        Level.Background.Image = taustakuva;
        
        startant = new PhysicsObject(40, 40);
        startant.Shape = Shape.Rectangle;
        startant.Image = StartAnt;
        Add(startant);
        
        PhysicsObject leaf = new PhysicsObject(40, 40);
        leaf.Shape = Shape.Rectangle;
        leaf.Image = Leaf;
        Add(leaf);
    }
}