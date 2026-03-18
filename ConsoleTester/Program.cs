using Clinic_Management_BLL.ResultWraper;
using Clinic_Management_BLL.Service;
using Clinic_Management_Entities;
using System;

using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== UserService Tester ===");

        var service = new UserService();

        // =========================
        // Hash password correctly
        // =========================
        //var (hash, salt) =
        //    Clinic_Management_BLL.LoginProcess.PasswordHasher
        //        .HashPassword("1111111");

        //// =========================
        //// Build test user
        //// =========================
        //var user = new User
        //{
        //    Username = "test111",
        //    Email = $"test{DateTime.Now.Ticks}@mail.com",
        //    PersonId = 1,                // MUST exist & must not already have a User
        //    IsActive = true,
        //    MustChangePassword = false,
        //    PasswordHash = hash,
        //    PasswordSalt = salt,
        //    CreatedAt = DateTime.Now
        //};

        //// =========================
        //// CREATE
        //// =========================
        //Result<int> createResult = service.Create(user);

        //if (!createResult.IsSuccess)
        //{
        //    Console.WriteLine("CREATE FAILED:");
        //    Console.WriteLine(createResult.Errors);
        //    Pause();
        //    return;
        //}

        //int newUserId = createResult.Value;
        //Console.WriteLine($"CREATE OK: New UserId = {newUserId}");

        // =========================
        // GET BY ID
        // =========================
        Result<User> getResult = service.GetById(1003);

        if (!getResult.IsSuccess || getResult.Value == null)
        {
            Console.WriteLine("GET FAILED:");
            Console.WriteLine(getResult.Errors);
            Pause();
            return;
        }

        var created = getResult.Value;

        Console.WriteLine(
            $"GET OK: {created.Username} | {created.Email} | PersonId={created.PersonId}");

        // =========================
        // VERIFY PASSWORD (bonus)
        // =========================
        bool verified =
            Clinic_Management_BLL.LoginProcess.PasswordHasher.Verify(
                "1111111",
                created.PasswordHash,
                created.PasswordSalt);

        Console.WriteLine($"PASSWORD VERIFY: {verified}");

        Pause();
    }

    static void Pause()
    {
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }
}
