using System.ComponentModel.DataAnnotations;

namespace hello_app.Models
{
    //[System.ComponentModel.DataAnnotations.Schema.Table("sbx-1033-nextgen-f7c2001f.Test_NextGen_DS.Test_NextGen_Tb")]
    public class Project
    {
        public Project() { }
        public Project(string projectId){
            ProjectId = projectId;
        }
        [Key]
        public string ProjectId { get; set; }


        public string ProjectName { get; set; }
    }
}
