using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmEntity
{
    public class Database
    {
        public string DatabaseName { get; set; }
        public List<BaseTable> TableList { get; set; }
    }

    public class BaseTable
    {
        public string TableName { get; set; }
        public List<BaseField> FieldList { get; set; }
    }

    public class BaseField
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
    }

    public class ServerConnect {
        public int Id { get; set; }

        [Required]
        public virtual string ServerType { get; set; } //Ms_SQL,Oracle,MySQL

        [Required]
        public virtual string ConnectionName { get; set; }

        [Required]
        public virtual string ServerHostNameIP { get; set; }

        [Required]
        public virtual string ServerUserName { get; set; }

        [Required]
        public virtual string ServerPassword { get; set; }

        [Required]
        public virtual string DatabaseName { get; set; }

        [Required]
        public virtual string TableName { get; set; }
            
        public virtual string EmailFieldName { get; set; }
    
    }

    public class Field : BaseField
    {
        public bool BoolienField { get; set; }
        public int NumericField { get; set; }
        public string VarCharField { get; set; }
        public DateTime DateTimeField { get; set; }

    }

    /// <summary>
    /// One Connection Has multiple Group depend rule condition
    /// </summary>
    public class Group
    {
        [NotMapped]
        private string _serverType;
        public Group() {
            this.Projects = new HashSet<Project>();
            _serverType = "";
        }

        public Group(string serverType) {
            _serverType = serverType;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string GroupName { 
            get{return _serverType;} 
            set{ _serverType = value + _serverType;} 
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Size { get; set; }

        [ForeignKey("ServerConnect")]
        public int ServerConnectId { get; set; }
        public virtual ServerConnect ServerConnect { get; set; }
        public virtual string ConditionQuery { get; set; }//if condition the execute condition 
        public virtual ICollection<Project> Projects { get; set; }
    }

    public class Project {
        public Project() {
            this.Groups = new HashSet<Group>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public bool Status { get; set; }               
        public DateTime StartTime { get; set; } //date time
        public Nullable<int> TimeInterval { get; set; }//1,2,3...
        public string IntervalOption { get; set; } //Day(s),Month,Year
        public DateTime ExpiredTime { get; set; }
        
        // user can assign any template from MailTemplate 
        //Or Can choose a template path saved in (ProjectTemplatePath) that has no text option {0}..{1}..{n}
        public string ProjectTemplatePath { get; set; }

        [ForeignKey("MailTemplate")]
        public int? MailTemplateId { get; set; }
        public virtual MailTemplate MailTemplate { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }


    public class MailTracker {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string MailTo { get; set; }
        public DateTime MailSendTime { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
     
    
    }

    //Attachment file should attach with project
    public class Attachment {
        [Key]
        public int Id { get; set; }
     
        [Required]
        public string AttachFileName { get; set; }

        [Required]
        public string AttachFilePath { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }


    public class MailTemplate {

        public MailTemplate() {
            this.MailTemplateTexts = new HashSet<MailTemplateText>();
            this.Projects = new HashSet<Project>();
        }
        [Key]
        [Index("Id", 1, IsUnique = true)]
        public int Id { get; set; }

       // [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        [Index("TemplateName", 2, IsUnique = true)]
        [Required]
        public virtual string TemplateName { get; set; }
        [Required]
        public string TemplateFilePath { get; set; }

        public string Description { get; set; }

        public virtual ICollection<MailTemplateText> MailTemplateTexts { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }

    public class MailTemplateText {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string TemplateDataOrder { get; set; } //{0},{1},.....

        [Required]
        public string TemplateData { get; set; }

        [ForeignKey("MailTemplate")]
        public int MailTemplateId { get; set; }
        public virtual MailTemplate MailTemplate { get; set; }
    }

 
    
}
