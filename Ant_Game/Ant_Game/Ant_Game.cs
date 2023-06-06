using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace Ant_Game;

public class Ant_Game : PhysicsGame
{
    Image StartAnt = LoadImage("MuurahainenSiivilla.png");
    
    public override void Begin()
    {
        Kentta();
        
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    void Kentta()
    {
        PhysicsObject startant = new PhysicsObject(40, 40);
        startant.Shape = Shape.Rectangle;
        startant.Image = StartAnt;
        Add(startant);
    }
}