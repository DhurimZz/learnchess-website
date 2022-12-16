[1mdiff --git a/learnchess/Configuration/ApplicationDbContext.cs b/learnchess/Configuration/ApplicationDbContext.cs[m
[1mindex faa099d..ff355be 100644[m
[1m--- a/learnchess/Configuration/ApplicationDbContext.cs[m
[1m+++ b/learnchess/Configuration/ApplicationDbContext.cs[m
[36m@@ -1,4 +1,5 @@[m
[31m-﻿using Microsoft.EntityFrameworkCore;[m
[32m+[m[32m﻿using learnchess.Models;[m
[32m+[m[32musing Microsoft.EntityFrameworkCore;[m
 [m
 namespace learnchess.Configuration[m
 {[m
[36m@@ -6,6 +7,7 @@[m [mnamespace learnchess.Configuration[m
     {[m
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)[m
         {}[m
[32m+[m[32m        public DbSet<User>Users { get; set; }[m[41m [m
 [m
     }[m
 }[m
[1mdiff --git a/learnchess/Program.cs b/learnchess/Program.cs[m
[1mindex 79a873e..ad59509 100644[m
[1m--- a/learnchess/Program.cs[m
[1m+++ b/learnchess/Program.cs[m
[36m@@ -1,4 +1,4 @@[m
[31m-using learnchess.Data;[m
[32m+[m[32musing learnchess.Configuration;[m
 using Microsoft.EntityFrameworkCore;[m
 [m
 var builder = WebApplication.CreateBuilder(args);[m
[1mdiff --git a/learnchess/appsettings.json b/learnchess/appsettings.json[m
[1mindex c84e282..9d28766 100644[m
[1m--- a/learnchess/appsettings.json[m
[1m+++ b/learnchess/appsettings.json[m
[36m@@ -7,7 +7,7 @@[m
   },[m
   "AllowedHosts": "*",[m
   "ConnectionStrings": {[m
[31m-    "DefaultConnection": "Server=DESKTOP-M5925HG;Database=LearnChess;Trusted_Connection=True;User Id=Dhurim;Password=Dhurim1234"[m
[32m+[m[32m    "DefaultConnection": "Server=DESKTOP-M5925HG;Database=LearnChess;Trusted_Connection=True;TrustServerCertificate=True;User Id=Dhurim;Password=Dhurim1234"[m
 [m
   }[m
 }[m
[1mdiff --git a/learnchess/learnchess.csproj b/learnchess/learnchess.csproj[m
[1mindex f105812..a8f5c87 100644[m
[1m--- a/learnchess/learnchess.csproj[m
[1m+++ b/learnchess/learnchess.csproj[m
[36m@@ -7,16 +7,18 @@[m
   </PropertyGroup>[m
 [m
   <ItemGroup>[m
[31m-    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="6.0.4" />[m
[31m-    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="6.0.4">[m
[32m+[m[32m    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.1" />[m
[32m+[m[32m    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.1">[m
       <PrivateAssets>all</PrivateAssets>[m
       <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>[m
     </PackageReference>[m
[31m-    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="6.0.4" />[m
[31m-    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="6.0.4">[m
[32m+[m[32m    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.1" />[m
[32m+[m[32m    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.1">[m
       <PrivateAssets>all</PrivateAssets>[m
       <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>[m
     </PackageReference>[m
[32m+[m[32m    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="6.0.11" />[m
[32m+[m[32m    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />[m
   </ItemGroup>[m
 [m
 </Project>[m
