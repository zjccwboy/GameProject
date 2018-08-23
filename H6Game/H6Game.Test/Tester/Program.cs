using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace Tester
{
    // Define a custom attribute with one named parameter.
    [AttributeUsage(AttributeTargets.All)]
    public class MyAttribute : Attribute
    {
        private string myName;
        public MyAttribute(string name)
        {
            myName = name;
        }
        public string Name
        {
            get
            {
                return myName;
            }
        }
    }

    // Define a class that has the custom attribute associated with one of its members.
    [MyAttribute("This is an example attribute.")]
    public class MyClass1
    {
        public void MyMethod(int i)
        {
            return;
        }
    }

    public class MemberInfo_GetCustomAttributes
    {
        //public static void Main()
        //{
        //    try
        //    {
        //        // Get the type of MyClass1.
        //        Type myType = typeof(MyClass1);

        //        var customers = myType.CustomAttributes.Where(a=>a.AttributeType == typeof(MyAttribute)).Select(a=>a.ConstructorArguments);
        //        var first = customers.FirstOrDefault();

        //        var test1 = myType.GetCustomAttributes<MyAttribute>();

        //        // Get the members associated with MyClass1.
        //        MemberInfo[] myMembers = myType.GetMembers();

        //        // Display the attributes for each of the members of MyClass1.
        //        for (int i = 0; i < myMembers.Length; i++)
        //        {
        //            Object[] myAttributes = myMembers[i].GetCustomAttributes(true);
        //            if (myAttributes.Length > 0)
        //            {
        //                Console.WriteLine("\nThe attributes for the member {0} are: \n", myMembers[i]);
        //                for (int j = 0; j < myAttributes.Length; j++)
        //                    Console.WriteLine("The type of the attribute is {0}.", myAttributes[j]);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("An exception occurred: {0}", e.Message);
        //    }
        //}


    }
}

class Program
{
    static void Main(string[] args)
    {
        DebugInfo db = new DebugInfo();
        Console.WriteLine(db.Test1());
    }



}

class DebugInfo
{
    public String Test1()
    {
        string info = null;
        //设置为true，这样才能捕获到文件路径名和当前行数，当前行数为GetFrames代码的函数，也可以设置其他参数
        StackTrace st = new StackTrace(true);
        //得到当前的所以堆栈
        StackFrame[] sf = st.GetFrames();
        for (int i = 0; i < sf.Length; ++i)
        {
            info = info + "\r\n" + " FileName=" + sf[i].GetFileName() + " fullname=" + sf[i].GetMethod().DeclaringType.FullName + " function=" + sf[i].GetMethod().Name + " FileLineNumber=" + sf[i].GetFileLineNumber();
        }
        return info;
    }

}