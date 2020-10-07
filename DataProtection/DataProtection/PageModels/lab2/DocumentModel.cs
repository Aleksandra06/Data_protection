using System;
using System.ComponentModel.DataAnnotations;

namespace DataProtection.PageModels
{
    public class DocumentModel
    {
        [MaxLength(50)]
        public string FileName { get; set; }
        [MaxLength(100)]
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
