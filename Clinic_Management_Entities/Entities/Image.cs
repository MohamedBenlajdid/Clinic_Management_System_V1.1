using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_Entities.Entities
{
    public sealed class Image
    {
        [DbColumn("ImageID")]
        public int ImageID { get; set; }

        [DbColumn("PersonID")]
        public int PersonID { get; set; }

        [DbColumn("ImagePath")]
        public string ImagePath { get; set; }
    }

}
