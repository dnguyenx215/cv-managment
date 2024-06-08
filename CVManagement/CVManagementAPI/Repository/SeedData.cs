using CVManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CVManagementAPI.Repository
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context =
                   new CVManagementDbContext(
                       serviceProvider.GetRequiredService<DbContextOptions<CVManagementDbContext>>()))
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                if (!await roleManager.Roles.AnyAsync())
                {
                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole { Name = "Admin" },
                        new IdentityRole { Name = "User" }
                    };

                    foreach (var role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role.Name))
                        {
                            await roleManager.CreateAsync(role);
                        }
                    }
                }


                if (!context.Source.Any())
                {
                    // Seed data for Source model
                    var sources = new List<Source>
                    {
                        new Source { NameSource = "Unknown" },
                        new Source { NameSource = "TopCV" },
                        new Source { NameSource = "Facebook" },
                        new Source { NameSource = "VietnamWork" },
                        new Source { NameSource = "ITViec" },
                        new Source { NameSource = "TopDev" },
                        new Source { NameSource = "SanDev" },
                    };
                    await context.Source.AddRangeAsync(sources);
                    await context.SaveChangesAsync();
                }


                if (!context.ColumnLayout.Any())
                {
                    // Seed data for Source model
                    var columnLayouts = new List<ColumnLayout>
                    {
                        new ColumnLayout {ColumnName = "Waiting review", IsEditableSetting = false, IsSendMail = true },
                        new ColumnLayout {ColumnName = "Review failed", IsEditableSetting = false, IsSendMail = true },
                        new ColumnLayout {ColumnName = "Review pass", IsEditableSetting = false, IsSendMail = true},
                        new ColumnLayout {ColumnName = "Interview fail", IsEditableSetting = false, IsSendMail = true },
                        new ColumnLayout {ColumnName = "Interview pass", IsEditableSetting = false, IsSendMail = true},
                        new ColumnLayout {ColumnName = "Interview backup", IsEditableSetting = false, IsSendMail = true},
                        new ColumnLayout {ColumnName = "Unknown", IsEditableSetting = false, IsSendMail = false},
                    };
                    await context.ColumnLayout.AddRangeAsync(columnLayouts);
                    await context.SaveChangesAsync();
                }


                if (!context.ColumnRelationship.Any())
                {
                    // Seed data for Source model
                    var columnLayouts = new List<ColumnRelationship>
                    {
                        //Waiting review: 1
                        new ColumnRelationship { ColumnLayoutId = 1, PullColumnId = 2 },
                        new ColumnRelationship { ColumnLayoutId = 1, PullColumnId = 3 },

                        // Review failed: 2
                        new ColumnRelationship {ColumnLayoutId = 2, PutColumnId = 1 },

                        // Review Pass: 3
                        new ColumnRelationship {ColumnLayoutId = 3, PutColumnId = 1 , PullColumnId = 4},
                        new ColumnRelationship {ColumnLayoutId = 3, PutColumnId = 1 , PullColumnId = 5},

                        //Interview res fail: 4
                        new ColumnRelationship {ColumnLayoutId = 4, PutColumnId = 3 },
                        new ColumnRelationship {ColumnLayoutId = 4, PutColumnId = 6 },

                        //Interview res pass: 5
                        new ColumnRelationship {ColumnLayoutId = 5, PutColumnId = 3, PullColumnId = 6},
                        new ColumnRelationship {ColumnLayoutId = 5, PutColumnId = 6, },

                        //Interview res pass: 6
                        new ColumnRelationship {ColumnLayoutId = 6, PutColumnId = 5, PullColumnId = 5},
                    };
                    await context.ColumnRelationship.AddRangeAsync(columnLayouts);
                    await context.SaveChangesAsync();
                }



                if (!context.Role.Any())
                {
                    // Seed data for Role model
                    var roles = new List<Role>
                    {
                        new Role { RoleName = "Admin" },
                        new Role { RoleName = "User" }
                    };
                    await context.Role.AddRangeAsync(roles);
                    await context.SaveChangesAsync();
                }

                if (!context.Position.Any())
                {
                    // Seed data for Position model
                    var positions = new List<Position>
                    {
                        new Position { PositionName = "Unknown" },
                        new Position { PositionName = "C#" },
                        new Position { PositionName = "PHP" },
                        new Position { PositionName = "Java" },
                        new Position { PositionName = "NodeJS" },
                        new Position { PositionName = "Unity" }
                    };
                    await context.Position.AddRangeAsync(positions);
                    await context.SaveChangesAsync();
                }

                //if (!context.ColumnLayout.Any())
                //{
                //    // Seed data for Position model
                //    var columns = new List<ColumnLayout>
                //    {
                //        new ColumnLayout { ColumnName = "Review Waiting", IsEditableSetting = false, IsSendMail = true, Id = 1   },

                //    };
                //    await context.ColumnLayout.AddRangeAsync(columns);
                //    await context.SaveChangesAsync();
                //}


                if (!userManager.Users.Any())
                {
                    // Create admin user
                    var adminUser = new AppUser
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        FullName = "Admin",
                        Phone = "0123456789",
                        RoleId = 1
                    };
                    await userManager.CreateAsync(adminUser, "AdminPassword123!");

                    // Create regular user
                    var regularUser = new AppUser
                    {
                        UserName = "user@example.com",
                        Email = "user@example.com",
                        FullName = "User",
                        Phone = "987654321",
                        RoleId = 2
                    };
                    await userManager.CreateAsync(regularUser, "UserPassword123!");
                    await context.SaveChangesAsync();
                }


                // Seed data for CV model
                //var CVs = new List<CV>
                //{
                //    new CV {
                //        NameCandidate = "Jane Smith",
                //        PhoneNumber = "987654321",
                //        Email = "janesmith@example.com",
                //        School = "XYZ University",
                //        YearOfBirth = "1995",
                //        SourceOfCV = "Source 2",
                //        DateReceiveCV = DateTime.Now,
                //        TimeOfInterview = "2:00 PM",
                //        DateOfInterview = DateTime.Now.AddDays(5),
                //        ReviewCV = "Average",
                //        Status = "Pending",
                //        CVEvaluate = "Good",
                //        CVNote = "Please follow up",
                //        Rate = "4",
                //        DateAcceptJob = null,
                //        PositionId = 2,
                //        SourceId = 2,
                //        AppUserId = null,
                //        IsSendMail = "not_send"
                //    },

                //};
                //await context.CV.AddRangeAsync(CVs);
                //await context.SaveChangesAsync();
            }
        }
    }
}
