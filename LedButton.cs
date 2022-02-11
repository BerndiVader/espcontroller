using Godot;
using System;

public class LedButton : Godot.CheckButton
{
    const string ui="UI";

	public override void _Ready()
	{
        AddToGroup(ui);

	}

	public override void _Toggled(bool pressed)
	{
        Controller.send_udp(Char.ToString((char)UDP_COMMAND.BUILDIN_LED_SWITCH)+":"+Convert.ToInt16(pressed));
	}

    public virtual void _Update_State()
    {
    
        Pressed=Controller.ledConfig.buildin_led;
    }


}
