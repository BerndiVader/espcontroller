using Godot;
using System;

public class StripButton : LedButton
{

	public override void _Ready()
	{

		base._Ready();

	}

	public override void _Toggled(bool pressed)
	{

		string data=Char.ToString((char)UDP_COMMAND.RGBSTRIP_SWITCH)+":"+Convert.ToInt16(pressed);
		Controller.send_udp(Char.ToString((char)UDP_COMMAND.RGBSTRIP_SWITCH));

	}

    public override void _Update_State(string[] data)
    {
        Pressed=data[6].Equals("0")==false;
    }

}
