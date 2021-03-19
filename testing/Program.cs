using System;
using ranksClient;
using System.Threading.Tasks;

namespace testing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GroupResolver group = new();
            GroupType gr = new()
            {
                about="asdasd",
                name="asd"
            };
            UserResolver user = new();
           //var a = await group.AddGroup(gr);
            UserInputTypeWithId newUser = new()
            {
                id="1",
                name = "Антонидас",
                secname = "Зубенко",
                groupId="1",
                rankId="2"
            };
            UserInputType newUs = new()
            {
                name = "Антонидасfromsharp",
                secname = "Зубенко",
                groupId = "1",
                rankId = "2",
                isAdmin = "2",
                password = "asasd",
                about= "dasdasas"
            };
            UserInputTypeWithId s = await user.AddUser(newUs);
            Console.WriteLine(s.id);
            var userList = await user.GetUsers();
            foreach (UserInputType u in userList)
            {
                Console.WriteLine(u.name);
                //Console.WriteLine(u.);
                Console.WriteLine(u.about);
                Console.WriteLine();
            }

        }
    }
}
