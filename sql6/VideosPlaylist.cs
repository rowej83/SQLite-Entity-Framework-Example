namespace sql6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VideosPlaylist
    {
        public long ID { get; set; }

        public long VideosID { get; set; }

        public long PlaylistsID { get; set; }
    }
}
