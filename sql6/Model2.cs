namespace sql6
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<playlist> playlists { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<VideosPlaylist> VideosPlaylists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
    

        }
    }
}
