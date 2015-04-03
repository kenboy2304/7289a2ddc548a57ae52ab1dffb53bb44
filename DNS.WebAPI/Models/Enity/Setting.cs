using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using DNS.WebAPI.Models.Enity;

// ReSharper disable once CheckNamespace
namespace DNS.WebAPI.Models
{
    public class Setting
    {
        public Setting()
        {
            
        }

        public Setting(object obj)
        {
            SetValue(obj);
        }
        public int Id { get; set; }
        public int Order { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }

        public void SetValue<T>(T obj)
        {
            Value = new JavaScriptSerializer().Serialize(obj);
            
        }
        public T GetValue<T>(T obj) where T : new()
        {
            return Value == null ? new T() : new JavaScriptSerializer().Deserialize<T>(Value);
        }
    }
}