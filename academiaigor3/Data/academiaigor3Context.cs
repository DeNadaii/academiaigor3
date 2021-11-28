using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcademiaIgor.Models;

namespace academiaigor3.Data
{
    public class academiaigor3Context : DbContext
    {
        public academiaigor3Context (DbContextOptions<academiaigor3Context> options)
            : base(options)
        {
        }

        public DbSet<AcademiaIgor.Models.EmpresaModels> EmpresaModels { get; set; }

        public DbSet<AcademiaIgor.Models.PessoaModel> PessoaModel { get; set; }
    }
}
