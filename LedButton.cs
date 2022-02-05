using Godot;
using System;

public class LedButton : Godot.TouchScreenButton
{
    const string ui="UI";
    protected Color pressed_color;
    protected Color released_color;
    private Rect2 rect;

	public override void _Ready()
	{
        AddToGroup(ui);
        pressed_color=new Color(1,1,1,0.5f);
        released_color=new Color(1,1,1,1f);

        rect=new Rect2(GlobalPosition,new Vector2(Normal.GetSize()*Scale));

		this.Connect("released",this,nameof(_Released));
        this.Connect("pressed",this,nameof(_Pressed));

        SetProcess(true);

	}

    public override void _Process(float delta)
    {
        if(!Controller.hovered)
        {
            Controller.hovered=rect.HasPoint(GetGlobalMousePosition());
        }
    }


    public virtual void _Pressed()
    {
        this.SelfModulate=pressed_color;
    }

	public virtual void _Released()
	{

        this.SelfModulate=released_color;
        Controller.send_udp(Char.ToString((char)UDP_COMMAND.BUILDIN_LED_SWITCH));

	}

    public virtual void _Update_State(string[] data)
    {
        //set
    }


}
