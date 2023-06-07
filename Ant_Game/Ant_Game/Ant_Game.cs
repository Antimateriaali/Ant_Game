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
    Image Arrow = LoadImage("Arrow.png");
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

        Keyboard.Listen(Key.A, ButtonState.Pressed, Animatio, null);
        Keyboard.Listen(Key.D, ButtonState.Pressed, Animatio, null);
        Keyboard.Listen(Key.W, ButtonState.Pressed, Animatio, null);
        Keyboard.Listen(Key.S, ButtonState.Pressed, Animatio, null);
        
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    
    void LiikutaPelaajaa(Vector vektori)
    {
        // startant.Animation.Start();
        // startant.Animation.Stop();
        startant.Velocity = vektori;
        startant.MaxVelocity = 400;

        if (vektori.Y < 0)
        {
            
            startant.MaxVelocity = 400;
            startant.Angle = Angle.FromDegrees(-90);
            
        }
        else if (vektori.Y > 0)
        {
            
            startant.MaxVelocity = 400;
            startant.Angle = Angle.RightAngle;
        }
        else if (vektori.X < 0)
        {
            
            startant.MaxVelocity = 400;
            startant.Angle = Angle.StraightAngle;
            
            
        }
        else if (vektori.X > 0)
        {
            
            startant.MaxVelocity = 400;
            startant.Angle = Angle.Zero;
            
            
        }
        else
        {
            startant.Animation.Stop();
            startant.MaxVelocity = 0;
        }
    }

    void Animatio()
    {
        startant.Animation.Start();
    }
    
    void Kentta()
    {
        Level.Background.Image = taustakuva;
        int kentankoko = 40000;
        Level.Width = kentankoko;
        Level.Height = kentankoko;
        Level.Background.TileToLevel();
        Level.CreateBorders();
        Level.Background.FitToLevel();
        Camera.StayInLevel = true;
        
        startant = new PhysicsObject(40, 40);
        //Shape antmuoto = Shape.FromImage(StartAnt);
        startant.Shape = Shape.Hexagon;
        //startant.Image = StartAnt;
        startant.Animation = new Animation(Walk);
        
        startant.Y = RandomGen.NextDouble(-20000, 20000);
        startant.X = RandomGen.NextDouble(-20000, 20000);
        
        startant.Animation.FPS = 3;
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
            leaf.Y = RandomGen.NextDouble(-20000, 20000);
            leaf.X = RandomGen.NextDouble(-20000, 20000);
            
            Add(leaf, -1);
        }
        
        
        PhysicsObject pesa = new PhysicsObject(80, 80);
        pesa.Shape = Shape.Rectangle;
        pesa.Image = Nest;
        pesa.CanRotate = false;
        pesa.MaxVelocity = 0;
        pesa.IgnoresCollisionResponse = true;
        
        Add(pesa,-1);

        /*for (int i = 0; i < 100; i++)
        {
            PhysicsObject ant = new PhysicsObject(40, 40);
            
            ant.Shape = Shape.Hexagon;
            ant.Animation = new Animation(Walk);
            ant.Animation.Start();
            ant.Image = StartAnt;
            
            ant.CanRotate = false;
            ant.Angle = Angle.FromDegrees(-90);
            RandomMoverBrain satunnaisaivot = new RandomMoverBrain(200);
            satunnaisaivot.TurnWhileMoving = true;
            satunnaisaivot.WanderRadius = 500;
            
            
            FollowerBrain aivot = new FollowerBrain("leaf");
            aivot.Speed = 200;                 
            aivot.DistanceFar = 60000;           
            aivot.DistanceClose = 10;         
            aivot.StopWhenTargetClose = false;  
            //aivot.FarBrain = aivot;
            //aivot.CloseBrain = satunnaisaivot;
            
            ant.Brain = satunnaisaivot;
            ant.MaxVelocity = 200;
            Add(ant);
        }*/
        
        for (int u = 0; u < 300; u++)
        {
            PhysicsObject ant1 = new PhysicsObject(40, 40);
            
            ant1.Shape = Shape.Hexagon;
            ant1.Animation = new Animation(Walk);
            
            ant1.Animation.Start();
            
            ant1.Image = StartAnt;
            ant1.Color = new Color(255, 0, 0);
            ant1.CanRotate = false;

            RandomMoverBrain home = new RandomMoverBrain(200);
            home.TurnWhileMoving = true;
            home.WanderPosition = new Vector(0, 0);
            home.WanderRadius = 500;
            
            
            /*FollowerBrain aivothome = new FollowerBrain(pesa);
            aivothome.TurnWhileMoving = true;
            aivothome.Speed = 200;                 // Millä nopeudella kohdetta seurataan
            aivothome.DistanceFar = 600;           // Etäisyys jolla aletaan seurata kohdetta
            aivothome.DistanceClose = 200;         // Etäisyys jolloin ollaan lähellä kohdetta
            aivothome.CloseBrain = home;*/
            
            FollowerBrain aivot = new FollowerBrain(startant);
            aivot.TurnWhileMoving = true;
            aivot.Speed = 200;                 // Millä nopeudella kohdetta seurataan
            aivot.DistanceFar = 600;           // Etäisyys jolla aletaan seurata kohdetta
            aivot.DistanceClose = 20;         // Etäisyys jolloin ollaan lähellä kohdetta
            aivot.StopWhenTargetClose = true;  // Pysähdytään kun ollaan lähellä kohdetta
            aivot.FarBrain = home;
            
            ant1.Brain = aivot;
            ant1.MaxVelocity = 200;
            ant1.Y = RandomGen.NextDouble(-20000, 20000);
            ant1.X = RandomGen.NextDouble(-20000, 20000);
            Add(ant1);
            
            
        }
        
        GameObject Nuoli = new GameObject(20, 20);
        Nuoli.Shape = Shape.Rectangle;
        Nuoli.Image = Arrow;
        Vector suunta = (pesa.Position - startant.Position).Normalize();
        Nuoli.Y = startant.Y+50;
        Nuoli.X = startant.X;
        
        startant.Add(Nuoli); 
        
        
    }
}