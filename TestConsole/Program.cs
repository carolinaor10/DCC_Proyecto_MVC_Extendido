using CoreApp;
using DataAccess.CRUD;
using DataAccess.DAOs;
using DTOs;
using System;

public class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("** Menu **");
            Console.WriteLine("1. User Operations");
            Console.WriteLine("2. Asset Operations");
            Console.WriteLine("3. Maintenance History Operations");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid option. Try again.");
                continue;
            }

            switch (option)
            {
                case 1:
                    UserMenu();
                    break;
                case 2:
                    AssetMenu();
                    break;
                case 3:
                    MaintenanceHistoryMenu();
                    break;
                case 4:
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    //User Menu
    static void UserMenu()
    {
        while (true)
        {
            Console.WriteLine("** User Operations **");
            Console.WriteLine("1. Create user");
            Console.WriteLine("2. Update user");
            Console.WriteLine("3. Delete user");
            Console.WriteLine("4. Retreive user by email");
            Console.WriteLine("5. Retreive all");
            Console.WriteLine("6. Back to main menu");
            Console.Write("Select an option: ");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid option. Try again.");
                continue;
            }

            switch (option)
            {
                case 1:
                    CreateUser();
                    break;
                case 2:
                    UpdateUser();
                    break;
                case 3:
                    DeleteUser();
                    break;
                case 4:
                    RetrieveUserByEmail();
                    break;
                case 5:
                    RetrieveAll();
                    break;
                    
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    //Asset Menu
    static void AssetMenu()
    {
        while (true)
        {
            Console.WriteLine("** Asset Operations **");
            Console.WriteLine("1. Create asset");
            Console.WriteLine("2. Update asset");
            Console.WriteLine("3. Delete asset");
            Console.WriteLine("4. Retreive asset by Id");
            Console.WriteLine("5. Retreive all");
            Console.WriteLine("6. Back to main menu");
            Console.Write("Select an option: ");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid option. Try again.");
                continue;
            }

            switch (option)
            {
                case 1:
                    CreateAsset();
                    break;
                case 2:
                    UpdateAsset();
                    break;
                case 3:
                    DeleteAsset();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }


    //Maintenance History Menu
    static void MaintenanceHistoryMenu()
    {
        while (true)
        {
            Console.WriteLine("** Maintenance History Operations **");
            Console.WriteLine("1. Create Maintenance History");
            Console.WriteLine("2. Update Maintenance History");
            Console.WriteLine("3. Delete Maintenance History");
            Console.WriteLine("4. Retreive Maintenance History by Id");
            Console.WriteLine("5. Retreive all");
            Console.WriteLine("6. Back to main menu");
            Console.Write("Select an option: ");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid option. Try again.");
                continue;
            }

            switch (option)
            {
                case 1:
                    CreateMaintenanceHistory();
                    break;
                case 2:
                    UpdateMaintenanceHistory();
                    break;
                case 3:
                    DeleteMaintenanceHistory();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    // User CRUD operations

    static void CreateUser()
    {
        Console.WriteLine("** Creating user **");

        Console.WriteLine("Enter the user's name:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter the user's email:");
        var email = Console.ReadLine();

        Console.WriteLine("Enter the user's department:");
        var department = Console.ReadLine();

        Console.WriteLine("Enter the user's role:");
        var role = Console.ReadLine();

        Console.WriteLine("Enter the user's birthday (yyyy-MM-dd HH:mm:ss):");
        var birthDate = DateTime.Parse(Console.ReadLine());

        var newUser = new User()
        {
            Name = name,
            Email = email,
            Department = department,
            Role = role,
            BirthDate = birthDate
        };

        try
        {
            //creamos user en bd
            var um = new UserManager();
            um.Create(newUser);
            Console.WriteLine("** User created successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user: {ex.Message}");
        }
    }

    static void UpdateUser()
    {
        Console.WriteLine("** Updating user **");
        Console.WriteLine("Enter the ID of the user you want to update:");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid ID. Try again.");
            return;
        }

        Console.WriteLine("Enter the new name of the user:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter the new email of the user:");
        var email = Console.ReadLine();

        Console.WriteLine("Enter the new department of the user:");
        var department = Console.ReadLine();

        Console.WriteLine("Enter the new role of the user:");
        var role = Console.ReadLine();

        Console.WriteLine("Enter the user's birthday (yyyy-MM-dd HH:mm:ss):");
        var birthDate = DateTime.Parse(Console.ReadLine());

        var user = new User()
        {
            Id = userId,
            Name = name,
            Email = email,
            Department = department,
            Role = role,
            BirthDate = birthDate
        };

        try
        {
            var uc = new UserCrudFactory();
            uc.Update(user);
            Console.WriteLine("** User updated successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user: {ex.Message}");
        }
    }

    //Retrieve user by emial

    static void RetrieveUserByEmail()
    {
        Console.WriteLine("** Retrieving user by Email **");
        Console.WriteLine("Enter the Email of the user you want to retrieve:");
        string email = Console.ReadLine();

        var user = new User { Email = email };

        try
        {
            var uc = new UserCrudFactory();
            var retrievedUser = uc.RetrieveByEmail(user);

            if (retrievedUser != null)
            {
                Console.WriteLine($"User retrieved successfully:\nID: {retrievedUser.Id}\nName: {retrievedUser.Name}\nEmail: {retrievedUser.Email}\nDepartment: {retrievedUser.Department}\nRole: {retrievedUser.Role}\nBirthDate: {retrievedUser.BirthDate}");
            }
            else
            {
                Console.WriteLine($"User with email {email} not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving user: {ex.Message}");
        }
    }

    static void RetrieveAll()
    {
        Console.WriteLine("** Retrieving all users **");

        try
        {
            var uc = new UserCrudFactory();
            var userList = uc.RetrieveAll<User>();

            if (userList.Count > 0)
            {
                Console.WriteLine("Users:");
                foreach (var user in userList)
                {
                    Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Department: {user.Department}, Role: {user.Role}, BirthDate: {user.BirthDate}");
                }
            }
            else
            {
                Console.WriteLine("No users found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving users: {ex.Message}");
        }
    }

    static void DeleteUser()
    {
        Console.WriteLine("** Deleting user **");

        Console.WriteLine("Enter the ID of the user you want to delete:");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid ID. Try again.");
            return;
        }

        try
        {
            var uc = new UserCrudFactory();
            uc.Delete(new User() { Id = userId });
            Console.WriteLine("** User deleted successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user: {ex.Message}");
        }
    
    }

    // Asset CRUD operations

    static void CreateAsset()
    {
        Console.WriteLine("** Creating asset **");

        Console.WriteLine("Enter the asset's name:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter the asset's category:");
        var category = Console.ReadLine();

        Console.WriteLine("Enter the asset's status:");
        var status = Console.ReadLine();

        //Console.WriteLine("Enter the asset's userId:");
       // var userId = int.Parse(Console.ReadLine());

        var newAsset = new Asset()
        {
            Name = name,
            Category = category,
            Status = status,
            //UserId = userId
        };

        try
        {
            var ac = new AssetCrudFactory();
            ac.Create(newAsset);
            Console.WriteLine("** Asset created successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating asset: {ex.Message}");
        }
    }

    static void UpdateAsset()
    {
        Console.WriteLine("** Updating asset **");

        Console.WriteLine("Enter the ID of the asset you want to update:");
        if(!int.TryParse(Console.ReadLine(), out int assetId))
        {
            Console.WriteLine("Invalid ID. Try again.");
            return;
        }

        Console.WriteLine("Enter the asset's name:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter the asset's category:");
        var category = Console.ReadLine();

        Console.WriteLine("Enter the asset's status:");
        var status = Console.ReadLine();

        var asset = new Asset()
        {
            Id = assetId,
            Name = name,
            Category = category,
            Status = status
        };

        try
        {
            var ac = new AssetCrudFactory();
            ac.Update(asset);
            Console.WriteLine("** Asset updated successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating asset: {ex.Message}");
        }
    }

    static void DeleteAsset()
    {
        Console.WriteLine("** Deleting asset **");
        Console.WriteLine("Enter the ID of the asset you want to delete:");
        if (!int.TryParse(Console.ReadLine(), out int assetId))
        {
            Console.WriteLine("Invalid ID. Try again.");
            return;
        }

        try
        {
            var ac = new AssetCrudFactory();
            ac.Delete(new Asset() { Id = assetId });
            Console.WriteLine("** Asset deleted successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting asset: {ex.Message}");
        }
    }

    // Maintenance History CRUD operations

    static void CreateMaintenanceHistory()
    {
        Console.WriteLine("** Creating Maintenance History **");

        Console.WriteLine("Enter the Maintenance History's Description:");
        var description = Console.ReadLine();

        Console.WriteLine("Enter the Maintenance History's Cost:");
        var cost = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter the Maintenance History's AssetId:");
        var assetId = int.Parse(Console.ReadLine());

        var newMaintenanceHistory = new MaintenanceHistory()
        {
            Description = description,
            Cost = cost,
            AssetId = assetId
        };

        try
        {
            var mc = new MaintenanceHistoryCrudFactory();
            mc.Create(newMaintenanceHistory);
            Console.WriteLine("** Maintenance History created successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating Maintenance History: {ex.Message}");
        }
    }

    static void UpdateMaintenanceHistory()
    {
        Console.WriteLine("** Updating Maintenance History **");
        Console.WriteLine("Enter the ID of the Maintenance History you want to update:");
        if (!int.TryParse(Console.ReadLine(), out int maintenanceId))
        {
            Console.WriteLine("Invalid ID. Try again.");
            return;
        }



        Console.WriteLine("Enter the Maintenance History's Description:");
        var description = Console.ReadLine();

        Console.WriteLine("Enter the Maintenance History's Cost:");
        var cost = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter the Maintenance History's AssetId:");
        var assetId = int.Parse(Console.ReadLine());

        var maintenanceHistory = new MaintenanceHistory()
        {
            Id = maintenanceId,
            Description = description,
            Cost = cost,
            AssetId = assetId
        };

        try
        {
            var mc = new MaintenanceHistoryCrudFactory();
            mc.Update(maintenanceHistory);
            Console.WriteLine("** Maintenance History updated successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating MaintenanceHistory: {ex.Message}");
        }
    }


    static void DeleteMaintenanceHistory()
    {
        Console.WriteLine("** Deleting Maintenance History **");

        Console.WriteLine("Enter the ID of the Maintenance History you want to delete:");
        if (!int.TryParse(Console.ReadLine(), out int MaintenanceHistoryId))
        {
            Console.WriteLine("Invalid ID. Try again.");
            return;
        }

        try
        {
            var uc = new MaintenanceHistoryCrudFactory();
            uc.Delete(new MaintenanceHistory() { Id = MaintenanceHistoryId });
            Console.WriteLine("** Maintenance History deleted successfully **");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting Maintenance History: {ex.Message}");
        }

    }


}
