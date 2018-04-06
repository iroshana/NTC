using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class NTCEntities : DbContext
    {
        public NTCEntities()
            : base("name=NTCEntities")
        {
        }
        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Complain> Complains { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberType> MemberTypes { get; set; }
        public virtual DbSet<DeMerit> DeMerits { get; set; }
        public virtual DbSet<Evidence> Evidences { get; set; }
        public virtual DbSet<Merit> Merits { get; set; }
        public virtual DbSet<Notice> Notices { get; set; }
        public virtual DbSet<Officer> Officers { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MemberNotice> MemberNotices { get; set; }
        public virtual DbSet<ComplainCategory> ComplainCategories { get; set; }
        public virtual DbSet<LogMessage> LogMessages { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
    }
}
