﻿using Newtonsoft.Json;

namespace SkyEditor.RomEditor.Domain.Automation
{
    public class ScriptUtilities
    {
        public string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
