using Newtonsoft.Json;
using StarkBank.Utils;

namespace StarkBank
{
    public abstract class User : StarkCore.User
    {
        public User(string environment, string id, string privateKey) : base(environment, id, privateKey) { }
    }
}
