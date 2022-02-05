using Godot;
using System;

public class StripSlider : LedSlider
{
    public override void _Ready()
    {
        base._Ready();
        
    }

    public override void _Value_Changed(float value)
    {
        Controller.send_udp((char)UDP_COMMAND.RGBSTRIP_DIMM+":"+(int)value);
    }

}
