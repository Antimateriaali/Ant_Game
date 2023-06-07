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
    private Image Nest = LoadImage("Nest.png");
    private Image[] Walk = LoadImages("Muurahainen.png", "Muurahainen1.png", "Muurahainen.png","Muurahainen2.png");
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
            startant.Animation.Start();
            startant.MaxVelocity = 200;
            startant.Angle = Angle.StraightAngle;
        }
        else if (vektori.Y > 0)
        {
            startant.Animation.Start();
            startant.MaxVelocity = 200;
            startant.Angle = Angle.Zero;
        }
        else if (vektori.X < 0)
        {
            startant.Animation.Start();
            startant.MaxVelocity = 200;
            startant.Angle = Angle.RightAngle;
        }
        else if (vektori.X > 0)
        {
            startant.Animation.Start();
            startant.MaxVelocity = 200;
            startant.Angle = Angle.FromDegrees(-90);
        }
        else
        {
            startant.Animation.Stop();
            startant.MaxVelocity = 0;
        }
    }
    
    void Kentta()
    {
        Level.Background.Image = taustakuva;
        int kentankoko = 4000;
        Level.Width = kentankoko;
        Level.Height = kentankoko;
        Level.CreateBorders();
        Level.Background.FitToLevel();
        Camera.StayInLevel = true;
        
        startant = new PhysicsObject(40, 40);
        startant.Shape = Shape.Rectangle;
        startant.Image = StartAnt;
        startant.Animation = new Animation(Walk);
        startant.CanRotate = false;
        Add(startant);

        for (int o = 0; o < 5; o++)
        {
            PhysicsObject leaf = new PhysicsObject(200, 200);
            
            leaf.Shape = Shape.Rectangle;
            leaf.Image = Leaf;
            leaf.CanRotate = false;
            leaf.MaxVelocity = 0;
            leaf.IgnoresCollisionResponse = true;
            leaf.Y = RandomGen.NextDouble(-2000, 2000);
            leaf.X = RandomGen.NextDouble(-2000, 2000);
            
            Add(leaf, -1);
        }
        
        
        PhysicsObject pesa = new PhysicsObject(80, 80);
        pesa.Shape = Shape.Rectangle;
        pesa.Image = Nest;
        pesa.CanRotate = false;
        pesa.MaxVelocity = 0;
        pesa.IgnoresCollisionResponse = true;
        
        Add(pesa,-1);

        for (int i = 0; i < 100; i++)
        {
            PhysicsObject ant = new PhysicsObject(40, 40);
            ant.Shape = Shape.Rectangle;
            ant.Animation = new Animation(Walk);
            ant.Image = StartAnt;
            ant.CanRotate = false;
            RandomMoverBrain satunnaisaivot = new RandomMoverBrain(200);
            FollowerBrain aivot = new FollowerBrain("leaf");
            satunnaisaivot.WanderRadius = 500;
            aivot.Speed = 200;                 
            aivot.DistanceFar = 600;           
            aivot.DistanceClose = 10;         
            aivot.StopWhenTargetClose = false;  
            aivot.FarBrain = aivot;
            aivot.CloseBrain = satunnaisaivot;
            ant.Brain = aivot;
            ant.MaxVelocity = 200;
            Add(ant);
        }
        
        
    }
}