using Godot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Net.Sockets;
using System.Threading.Tasks;

public class Controller : Node2D
{
	public const string udp_ip = "192.168.43.100";
	public const string hndshk = "ESP32HNDSHK";
	public const int udp_port = 19740;
	public const int handshake_port=19680;
	public static LedConfig ledConfig;

	private TabContainer container;

	public override void _Ready()
	{
		ledConfig=new LedConfig();
		VisualServer.SetDefaultClearColor(new Color(0.388235f, 0.25098f, 0.313726f,1f));
		container=GetNode<TabContainer>("TabContainer");

		var options=new JsonSerializerOptions
		{
			IncludeFields=true,
		};

		List<LedConfig.RgbLed>rgbs=new List<LedConfig.RgbLed>();
		rgbs.Add(new LedConfig.RgbLed(1,2,3));
		rgbs.Add(new LedConfig.RgbLed(2,3,1));
		rgbs.Add(new LedConfig.RgbLed(3,1,2));
		rgbs.Add(new LedConfig.RgbLed(4,3,1));
		ledConfig.rgbs=rgbs;

		GD.Print(JsonSerializer.Serialize(ledConfig,options));

		InstallListener(this);
		send_udp(Char.ToString((char)UDP_COMMAND.HANDSHAKE));

	}

	public override void _Process(float delta)
	{

	}
	
	private static void InstallListener(Controller controller)
	{

		string[]data=new string[0];
		string answer="";

		Task.Factory.StartNew(async()=>
		{
			using(UdpClient client=new UdpClient(handshake_port))
			{
				while(true)
				{
					UdpReceiveResult result=await client.ReceiveAsync();
					answer=Encoding.ASCII.GetString(result.Buffer);
					if(answer.StartsWith(hndshk))
					{
						GD.Print(answer);
						answer=answer.Replace(hndshk+":","");
						GD.Print(answer);
						data=answer.Split(":");
						if(data.Length!=0&&data[0].Equals(hndshk))
						{
							controller.GetTree().CallGroup("UI","_Update_State",data.Clone());
						}
					}

				}
			}

		});
		
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
