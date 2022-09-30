using Godot;
using System;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

public class Controller : Node2D
{
	public const string UDP_IP = "192.168.43.100";
	public const string HNDSHK = "ESP32HNDSHK";
	public const int UDP_PORT = 19740;
	public const int HANDSHAKE_PORT=19680;

	private TabContainer container;
	public static LedConfig ledConfig;

	public override void _Ready()
	{
		VisualServer.SetDefaultClearColor(new Color(0.388235f, 0.25098f, 0.313726f,1f));
		container=GetNode<TabContainer>("TabContainer");
		ledConfig=new LedConfig();

		InstallListener(this);
		send_udp(Char.ToString((char)UDP_COMMAND.HANDSHAKE));

	}

	public override void _Process(float delta)
	{

	}
	
	private static void InstallListener(Controller controller)
	{

		Task.Factory.StartNew(async()=>
		{
			using(UdpClient client=new UdpClient(HANDSHAKE_PORT))
			{
				string[] data=new string[0];
				string answer=string.Empty;

				while(true)
				{
					UdpReceiveResult result=await client.ReceiveAsync();
					answer=Encoding.ASCII.GetString(result.Buffer);
					data=answer.Split(":");

					if(data.Length>0&&data[0].ToInt()==(int)UDP_COMMAND.HANDSHAKE)
					{
						update_config(data);
						controller.GetTree().CallGroup("UI","_Update_State");
					}

				}
			}

		});
		
	}

	public static void update_config(string[] data)
	{
		for(int i1=1;i1<data.Length;i1+=2)
		{
			switch(data[i1].ToInt())
			{
				case (int)UDP_COMMAND.BUILDIN_LED_SWITCH:
					ledConfig.buildin_led=data[i1+1].ToInt()==1;
				break;
				case (int)UDP_COMMAND.BUILDIN_LED_DIMM:
					ledConfig.buildin_led_brightness=data[i1+1].ToInt();
				break;
			
			}
		}

	}

	public static void send_udp(string data)
	{
		using(UdpClient client=new UdpClient(UDP_PORT))
		{
			byte[]bytes=Encoding.ASCII.GetBytes(data);
			client.Send(bytes,bytes.Length,UDP_IP,UDP_PORT);
		}

	}

}