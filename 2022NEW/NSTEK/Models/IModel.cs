using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace _2022_Test.NSTEK.Models
{
    public abstract class Model
    {
        public EventHandler<EventArgs> ValueChanged;
        public string GetModelName()
        {
            return this.GetType().Name;
        }
        public string[] GetNames()
        {
            List<string> list = new List<string>();
            // Get the Type object for the instance
            Type type = this.GetType();

            // Get all public instance properties
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);


            list.AddRange(properties.Select(x => x.Name).ToArray());
            // Iterate through the properties and get their names and values
            //foreach (PropertyInfo property in properties)
            //{
            //    // Get the property name
            //    string propertyName = property.Name;


            //    // Get the property value from the object instance
            //    // For non-indexed properties, the second argument is null
            //    object propertyValue = property.GetValue(this, null);

            //    Console.WriteLine($"Property Name: {propertyName}, Value: {propertyValue ?? "null"}");
            //}

            return list.ToArray();
        }

        public Type GetTypeOf(string propName)
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            PropertyInfo p = properties.Where(x => x.Name == propName).First();

            return p.PropertyType;
        }
        public void SetValue(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            PropertyInfo p = properties.Where(x => x.Name == name).First();

            Type pt = p.PropertyType;
            try
            {
                if (pt.IsEnum)
                {
                    object v = Enum.Parse(pt, value, true);

                    p.SetValue(this, v);
                }
                else
                {
                    object v = Convert.ChangeType(value, pt);
                    p.SetValue(this, v);

                    if (ValueChanged != null)
                    {
                        ValueChanged.Invoke(this, new EventArgs());
                    }
                }
            }
            catch
            {
                return;
            }
            
        }
        public void SetValue(string name, object value)
        {
            if (value == null)
            {
                return;
            }

            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            properties.Where(x => x.Name == name).First().SetValue(this, value);

            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this, new EventArgs());
            }
        }
        public object GetValue(string name)
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            return properties.Where(x => x.Name == name).First().GetValue(this);
        }
        public Type GetType(string name)
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            return properties.Where(x => x.Name == name).First().GetType();
        }
        public string GetUnit(string name)
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            var attr = properties.Where(x => x.Name == name).First().GetCustomAttribute<MeasuredUnitAttribute>();
            
            return attr != null ? attr.Unit : "";
        }
        public override string ToString()
        {
            string ret = $"[{GetModelName()} Properties]\r\n";
            List<string> list = new List<string>();
            // Get the Type object for the instance
            Type type = this.GetType();

            // Get all public instance properties
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            foreach (PropertyInfo property in properties)
            {
                ret += $"{property.Name}: {property.GetValue(this).ToString()}\r\n";
            }

            return ret;
        }
    }
}
