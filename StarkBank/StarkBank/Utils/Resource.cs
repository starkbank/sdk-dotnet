using System.Linq;
using System.Reflection;


namespace StarkBank.Utils
{
    public abstract class Resource : SubResource
    {
        public string ID { get; }

        public Resource(string id)
        {
            ID = id;
        }
    }
}
