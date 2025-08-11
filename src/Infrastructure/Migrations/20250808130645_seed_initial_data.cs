using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_initial_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed Teams
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "dotnet" },
                    { 2, "Frontend" },
                    { 3, "Backend" },
                    { 4, "DevOps" },
                    { 5, "QA" }
                });

            // Seed VacationPackages
            migrationBuilder.InsertData(
                table: "VacationPackages",
                columns: new[] { "VacationPackageId", "VacationPackageName", "GrantedDays", "Year" },
                values: new object[,]
                {
                    // 2018
                    { 1, "Starter", 5, 2018 },
                    { 101, "Work-Life Basic", 10, 2018 },
                    { 201, "Work-Life Plus", 15, 2018 },
                    { 301, "Freedom Pro", 20, 2018 },

                    // 2019
                    { 2, "Starter", 5, 2019 },
                    { 102, "Work-Life Basic", 10, 2019 },
                    { 202, "Work-Life Plus", 15, 2019 },
                    { 302, "Freedom Pro", 20, 2019 },
                    { 402, "Unlimited Flex", 25, 2019 }, // New package

                    // 2020
                    { 3, "Starter", 5, 2020 },
                    { 103, "Work-Life Basic", 12, 2020 }, // Increased from 10 to 12
                    { 203, "Work-Life Plus", 15, 2020 },
                    { 303, "Freedom Pro", 22, 2020 }, // Increased from 20 to 22
                    { 403, "Unlimited Flex", 25, 2020 },
                    { 503, "Zen Contractor", 30, 2020 }, // New package

                    // 2021
                    { 4, "Starter", 5, 2021 },
                    { 104, "Work-Life Basic", 12, 2021 },
                    { 204, "Work-Life Plus", 17, 2021 }, // Increased from 15 to 17
                    { 304, "Freedom Pro", 22, 2021 },
                    { 404, "Unlimited Flex", 26, 2021 }, // Increased from 25 to 26
                    { 504, "Zen Contractor", 30, 2021 },

                    // 2022 - some packages discontinued
                    { 5, "Starter", 5, 2022 },
                    { 105, "Work-Life Basic", 12, 2022 },
                    { 205, "Work-Life Plus", 17, 2022 },
                    { 305, "Freedom Pro", 22, 2022 },
                    { 405, "Unlimited Flex", 26, 2022 },
                    { 505, "Zen Contractor", 32, 2022 }, // Increased from 30 to 32

                    // 2023
                    { 6, "Starter", 5, 2023 },
                    { 106, "Work-Life Basic", 12, 2023 },
                    { 206, "Work-Life Plus", 17, 2023 },
                    { 306, "Freedom Pro", 22, 2023 },
                    { 406, "Unlimited Flex", 26, 2023 },
                    { 506, "Zen Contractor", 32, 2023 },

                    // 2024
                    { 7, "Starter", 5, 2024 },
                    { 107, "Work-Life Basic", 12, 2024 },
                    { 207, "Work-Life Plus", 17, 2024 },
                    { 307, "Freedom Pro", 24, 2024 }, // Increased from 22 to 24
                    { 407, "Unlimited Flex", 28, 2024 }, // Increased from 26 to 28
                    { 507, "Zen Contractor", 32, 2024 },

                    // 2025
                    { 8, "Starter", 5, 2025 },
                    { 108, "Work-Life Basic", 12, 2025 },
                    { 208, "Work-Life Plus", 17, 2025 },
                    { 308, "Freedom Pro", 24, 2025 },
                    { 408, "Unlimited Flex", 28, 2025 },
                    { 508, "Zen Contractor", 32, 2025 },
                });

            // Seed Employees
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Name", "TeamId", "VacationPackageId", "SuperiorId" },
                values: new object[,]
                {
                    // .NET Team
                    { 1, "Jan Kowalski", 1, 2, null },       
                    { 2, "Anna Nowak", 1, 103, 1 },            
                    { 3, "Piotr Wiśniewski", 1, 204, 1 },      
                    { 4, "Maria Wójcik", 1, 305, 2 },          
                    { 5, "Kamil Zieliński", 1, 404, 2 },       
                    { 6, "Ewa Maj", 1, 508, 3 },               

                    // Frontend Team
                    { 7, "Tomasz Kowalczyk", 2, 105, null },   
                    { 8, "Katarzyna Kamińska", 2, 206, 7 },    
                    { 9, "Marek Nowak", 2, 307, 7 },           
                    { 10, "Paulina Szymańska", 2, 408, 8 },    
                    { 11, "Adam Król", 2, 508, 9 },            

                    // Backend Team
                    { 12, "Michał Lewandowski", 3, 206, null },  
                    { 13, "Agnieszka Zielińska", 3, 305, 12 },   
                    { 14, "Robert Wójcik", 3, 408, 12 },         
                    { 15, "Joanna Woźniak", 3, 508, 13 },        

                    // DevOps Team
                    { 16, "Paweł Szymański", 4, 305, null },     
                    { 17, "Joanna Kaczmarek", 4, 404, 16 },      
                    { 18, "Łukasz Dąbrowski", 4, 504, 17 },      

                    // QA Team
                    { 19, "Magdalena Kozłowska", 5, 107, null }, 
                    { 20, "Karol Pawlak", 5, 201, 19 },          
                    { 21, "Natalia Baran", 5, 301, 19 },         
                    { 22, "Patryk Lis", 5, 402, 20 },            
                    { 23, "Sylwia Górska", 5, 504, 21 },         
                    { 24, "Marta Konieczna", 5, 505, 2 },        
                    { 25, "Grzegorz Malinowski", 5, 303, 7 },    
                    { 26, "Zofia Piotrowska", 5, 204, 12 },      
                    { 27, "Wojciech Czarnecki", 5, 104, 16 },    
                    { 28, "Barbara Sławińska", 5, 5, 19 }      
                });

            // Seed Vacations
            migrationBuilder.InsertData(
                table: "Vacations",
                columns: new[] { "VacationId", "EmployeeId", "DateSince", "DateUntil", "NumberOfHours", "IsPartVacation" },
                values: new object[,]
                {
                    // 2018
                    { 1, 1, new DateTime(2018, 1, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 1, 19, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 2, 2, new DateTime(2018, 2, 5, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 2, 9, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 3, 3, new DateTime(2018, 3, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 3, 14, 0, 0, 0, DateTimeKind.Utc), 24, true },
                    { 4, 4, new DateTime(2018, 4, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 4, 27, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 5, 5, new DateTime(2018, 6, 4, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 6, 8, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 6, 6, new DateTime(2018, 7, 2, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 7, 6, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 7, 7, new DateTime(2018, 8, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 8, 24, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 8, 8, new DateTime(2018, 12, 24, 0, 0, 0, DateTimeKind.Utc), new DateTime(2019, 1, 2, 0, 0, 0, DateTimeKind.Utc), 72, false },

                    // 2019
                    { 9, 1, new DateTime(2019, 1, 7, 0, 0, 0, DateTimeKind.Utc), new DateTime(2019, 1, 11, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 10, 2, new DateTime(2019, 3, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2019, 3, 22, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 11, 3, new DateTime(2019, 5, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2019, 5, 17, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 12, 4, new DateTime(2019, 6, 24, 0, 0, 0, DateTimeKind.Utc), new DateTime(2019, 6, 28, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 13, 5, new DateTime(2019, 7, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2019, 7, 22, 0, 0, 0, DateTimeKind.Utc), 64, false },
                    { 14, 6, new DateTime(2019, 8, 5, 0, 0, 0, DateTimeKind.Utc), new DateTime(2019, 8, 9, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 15, 7, new DateTime(2019, 9, 2, 0, 0, 0, DateTimeKind.Utc), new DateTime(2019, 9, 9, 0, 0, 0, DateTimeKind.Utc), 64, false },
                    { 16, 8, new DateTime(2019, 12, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2020, 1, 3, 0, 0, 0, DateTimeKind.Utc), 88, false },

                    // 2020
                    { 17, 1, new DateTime(2020, 2, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2020, 2, 14, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 18, 2, new DateTime(2020, 3, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2020, 3, 20, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 19, 3, new DateTime(2020, 4, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2020, 4, 10, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 20, 4, new DateTime(2020, 6, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2020, 6, 5, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 21, 5, new DateTime(2020, 7, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2020, 7, 31, 0, 0, 0, DateTimeKind.Utc), 88, false },
                    { 22, 6, new DateTime(2020, 8, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2020, 8, 12, 0, 0, 0, DateTimeKind.Utc), 24, true },
                    { 23, 7, new DateTime(2020, 9, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2020, 9, 18, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 24, 8, new DateTime(2020, 12, 28, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 1, 4, 0, 0, 0, DateTimeKind.Utc), 56, false },

                    // 2021
                    { 25, 1, new DateTime(2021, 1, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 1, 15, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 26, 2, new DateTime(2021, 2, 8, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 2, 12, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 27, 3, new DateTime(2021, 3, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 3, 17, 0, 0, 0, DateTimeKind.Utc), 24, true },
                    { 28, 4, new DateTime(2021, 5, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 5, 14, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 29, 5, new DateTime(2021, 6, 21, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 6, 25, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 30, 6, new DateTime(2021, 7, 5, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 7, 9, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 31, 7, new DateTime(2021, 8, 30, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 9, 3, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 32, 8, new DateTime(2021, 12, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 3, 0, 0, 0, DateTimeKind.Utc), 72, false },

                    // 2022
                    { 33, 1, new DateTime(2022, 1, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 14, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 34, 2, new DateTime(2022, 2, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 2, 18, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 35, 3, new DateTime(2022, 3, 21, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 3, 25, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 36, 4, new DateTime(2022, 4, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 4, 15, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 37, 5, new DateTime(2022, 6, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 6, 10, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 38, 6, new DateTime(2022, 7, 4, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 7, 8, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 39, 7, new DateTime(2022, 8, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 8, 26, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 40, 8, new DateTime(2022, 12, 27, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 4, 0, 0, 0, DateTimeKind.Utc), 64, false },

                    // 2023
                    { 41, 1, new DateTime(2023, 1, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 13, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 42, 2, new DateTime(2023, 3, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 10, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 43, 3, new DateTime(2023, 4, 24, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 4, 28, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 44, 4, new DateTime(2023, 5, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 5, 19, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 45, 5, new DateTime(2023, 7, 3, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 7, 7, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 46, 6, new DateTime(2023, 8, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 8, 18, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 47, 7, new DateTime(2023, 9, 25, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 9, 29, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 48, 8, new DateTime(2023, 12, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc), 88, false },

                    // 2024
                    { 49, 1, new DateTime(2024, 2, 5, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 9, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 50, 2, new DateTime(2024, 3, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 51, 3, new DateTime(2024, 4, 8, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 12, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 52, 4, new DateTime(2024, 6, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 21, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 53, 5, new DateTime(2024, 7, 29, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 2, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 54, 6, new DateTime(2024, 9, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 9, 13, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 55, 7, new DateTime(2024, 10, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 18, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 56, 8, new DateTime(2024, 12, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc), 88, false },

                    // 2025
                    { 57, 1, new DateTime(2025, 1, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 17, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 58, 2, new DateTime(2025, 2, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 21, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 59, 3, new DateTime(2025, 3, 24, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 28, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 60, 4, new DateTime(2025, 5, 5, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 61, 5, new DateTime(2025, 6, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 20, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 62, 6, new DateTime(2025, 7, 28, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 1, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 63, 7, new DateTime(2025, 9, 8, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 12, 0, 0, 0, DateTimeKind.Utc), 40, false },
                    { 64, 8, new DateTime(2025, 12, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc), 80, false },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seed data in reverse order (due to foreign key constraints)
            migrationBuilder.DeleteData(
                table: "Vacations",
                keyColumn: "VacationId",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });

            migrationBuilder.DeleteData(
                table: "VacationPackages",
                keyColumn: "VacationPackageId",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });
        }
    }
}