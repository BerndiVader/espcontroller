using Godot;
using System;

public class LedSlider : Godot.HSlider
{
    const string ui="UI";
    public override void _Ready()
    {
        AddToGroup(ui);
        Connect("value_changed",this,nameof(_Value_Changed));
        
    }

    public virtual void _Value_Changed(float value)
    {

        Controller.send_udp((char)UDP_COMMAND.BUILDIN_LED_DIMM+":"+(int)value);

    }

    public virtual void _Update_State()
    {
        Value=Controller.ledConfig.buildin_led_brightness;
    }

}
