﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShareWithLikes.Data
{
    public  class ImageDataContextFactory : IDesignTimeDbContextFactory<ImageDBContext>
    {
        public ImageDBContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
               $"..{Path.DirectorySeparatorChar}ImageShareWithLikes.Web"))
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new ImageDBContext(config.GetConnectionString("ConStr"));
        }
    }
}
