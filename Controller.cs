using Godot;
using System;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class Controller : Node2D
{
	public const string udp_ip = "192.168.43.100";
	public const string hndshk = "ESP32HNDSHK";
	public const int udp_port = 19740;
	public const int handshake_port=19680;
	public static bool hovered;

	public override void _Ready()
	{
		VisualServer.SetDefaultClearColor(new Color(0.388235f, 0.25098f, 0.313726f,1f));        

		hovered=false;
        SetProcess(true);
		
		handshake(this);

	}

	public override void _Process(float delta)
	{
		if(hovered)
		{
			Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
		}
		else
		{
			Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
		}
		hovered=false;
	}

	private static async void handshake(Controller controller)
	{

		string[]data=new string[0];
		string answer="";
		long ticks=DateTime.Now.Ticks;
		Task task=Task.Run(()=>
		{
			send_udp(Char.ToString((char)UDP_COMMAND.HANDSHAKE));

			using (UdpClient client=new UdpClient(handshake_port))
			{
				IPEndPoint point=new IPEndPoint(IPAddress.Any,handshake_port);
				var result=client.Receive(ref point);
				answer=Encoding.ASCII.GetString(result);
			}
			data=answer.Split(":");
		});
		if(await Task.WhenAny(task,Task.Delay(5000*5))==task)
		{
			GD.Print(new TimeSpan(DateTime.Now.Ticks-ticks));
			if(data[0].Equals(hndshk))
			{
				controller.GetTree().CallGroup("UI","_Update_State",data.Clone());
			}
		}
	}

	public static void send_udp(string data)
	{
		using(UdpClient client=new UdpClient(udp_port))
		{
			byte[]bytes=Encoding.ASCII.GetBytes(data);
			client.Send(bytes,bytes.Length,udp_ip,udp_port);
		}

	}


}
