using Godot;
using System;

public class StripButton : LedButton
{

    public override void _Ready()
    {

        base._Ready();

    }

    public override void _Released()
    {

        this.SelfModulate=released_color;
        Controller.send_udp(Char.ToString((char)UDP_COMMAND.RGBSTRIP_SWITCH));

    }

}
