using System;
using System.Collections.Generic;


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

    public bool buildin_led{get;set;}
    public int buildin_led_brightness{get;set;}

    public int rgb_count{get;set;}
    public bool rgb_onoff{get;set;}
    public int rgb_brightness{get;set;}
    public List<RgbLed> rgbs{get;set;}

}