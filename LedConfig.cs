using System;
using System.Text.Json;
using System.Text.Json.Serialization;


public class LedConfig
{
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

}