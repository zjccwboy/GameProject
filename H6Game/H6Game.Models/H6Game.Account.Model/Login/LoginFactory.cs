using H6Game.Base;
using H6Game.Hotfix.Enums;
using System;
using System.Collections.Generic;

namespace H6Game.Account.Model
{
    public class LoginFactory
    {
        private static Dictionary<LoginType, ILogin> Logins { get; } = new Dictionary<LoginType, ILogin>();

        public static ILogin Create(LoginType type)
        {
            return Logins[type];
        }

        public static void Load()
        {
            var baseType = typeof(ALogin);
            var assemblies = ObjectPool.Assemblies;
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (!type.IsClass)
                        continue;

                    if (type.IsAbstract)
                        continue;

                    if (type.BaseType == baseType)
                    {
                        var login = Activator.CreateInstance(type) as ALogin;
                        Logins[login.LoginType] = login;
                    }
                }
            }
        }
    }
}
