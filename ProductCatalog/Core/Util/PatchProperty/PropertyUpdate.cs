using System.Linq;
using System.Reflection;

namespace Core.Util.PatchProperty
{

    public class PropertyUpdate
    {
        private string property;
        public string Property
        {
            get { return property; }
            set
            {
                var capitalisedValue = value.First().ToString().ToUpper() + value.Substring(1);
                property = capitalisedValue;
            }
        }
        public object Value { get; set; }


        public void  PatchProperty<T>(T objectToPatch) {
            PropertyInfo property = objectToPatch.GetType().GetProperty(Property);
            property.SetValue(objectToPatch, Value);
        }


    }
}
