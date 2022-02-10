using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


public class LedConfig
{
    public class RgbLed
    {
        public RgbLed(int pos,int color,int brightness)
        {
            this.pos=pos;
            this.color=color;
            this.brightness=brightness;

        }
        public int pos{get;set;}
        public int color{get;set;}
        public int brightness{get;set;}
    }

    public LedConfig()
    {
        rgbs=new List<RgbLed>();
    }

    [JsonInclude]
    public bool buildin_led{get;set;}
    [JsonInclude]
    public int buildin_led_brightness{get;set;}

    [JsonInclude]
    public int rgb_count{get;set;}
    [JsonInclude]
    public bool rgb_onoff{get;set;}
    [JsonInclude]
    public int rgb_brightness{get;set;}
    [JsonInclude]
    public List<RgbLed> rgbs{get;set;}

}