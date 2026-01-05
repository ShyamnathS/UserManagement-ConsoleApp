using System;
using UserManagement.Lib.Services;
using UserManagement.Lib.Enums;

namespace UserManagement.ConsoleUI
{
    class Program
    {
        static UserManagementService service = new UserManagementService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== USER MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Manage Roles (CRUD)");
                Console.WriteLine("2. Manage Users (CRUD)");
                Console.WriteLine("3. Login Test");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1": RoleMenu(); break;
                    case "2": UserMenu(); break;
                    case "3": LoginTest(); break;
                    case "4": return;
                }
            }
        }

        static void RoleMenu()
        {
            Console.Clear();
            Console.WriteLine("--- ROLE MANAGEMENT ---");
            Console.WriteLine("1. Create Role");
            Console.WriteLine("2. List Roles");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Role Name: ");
                string name = Console.ReadLine();
                // For simplicity, assigning Admin perms to new roles here
                service.CreateRole(name, UserPermissionFlag.Read | UserPermissionFlag.Write, EntityStatus.Available);
                Console.WriteLine("Role Created!");
            }
            else
            {
                var roles = service.GetAllRoles();
                foreach (var r in roles) Console.WriteLine($"{r.Id}: {r.Name} [{r.Status}]");
            }
            Console.ReadKey();
        }

        static void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("--- USER MANAGEMENT ---");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. List Users");
            Console.WriteLine("3. Delete User (Deactivate)");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Username: ");
                string un = Console.ReadLine();
                Console.Write("Password: ");
                string pw = Console.ReadLine();
                Console.Write("Role ID: ");
                int rid = int.Parse(Console.ReadLine());
                service.CreateUser(un, pw, rid, EntityStatus.Available);
                Console.WriteLine("User Created!");
            }
            else if (choice == "2")
            {
                var users = service.GetAllUsers();
                foreach (var u in users) Console.WriteLine($"{u.Id}: {u.UserName} (Role: {u.UserRole?.Name}) - Status: {u.Status}");
            }
            else if (choice == "3")
            {
                Console.Write("Enter User ID to Deactivate: ");
                int id = int.Parse(Console.ReadLine());
                service.DeleteUser(id);
                Console.WriteLine("User set to Inactive.");
            }
            Console.ReadKey();
        }

        static void LoginTest()
        {
            Console.Clear();
            Console.Write("Username: ");
            string un = Console.ReadLine();
            Console.Write("Password: ");
            string pw = Console.ReadLine();

            var user = service.Login(un, pw);
            if (user != null) Console.WriteLine($"SUCCESS! Welcome {user.UserName}");
            else Console.WriteLine("FAILED: Invalid login or Inactive status.");
            Console.ReadKey();
        }
    }
}