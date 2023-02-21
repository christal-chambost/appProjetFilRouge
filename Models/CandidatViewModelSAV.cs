//using appprojetfilrouge.data.entities;
//using microsoft.entityframeworkcore.metadata.internal;
//using system.componentmodel.dataannotations.schema;
//using system.componentmodel.dataannotations;
//using system.componentmodel;


//namespace appprojetfilrouge.models
//{
//    public class candidateviewmodeltest
//    {
//        [key]
//        [column("candidat_id")]
//        public int id { get; set; }

//        [required]
//        [displayname("non d'utilisateur")]
//        //[column("name", typename = "varchar(256)")]
//        public string? username { get; set; }

//        [required]
//        [displayname("normalisation nom d'utilisateur")]
//        //[column("name", typename = "varchar(256)")]
//        public string? normalizedusername { get; set; }

//        [required]
//        [displayname("email")]
//        //[column("name", typename = "varchar(256)")]
//        public string? email { get; set; }

//        [required]
//        [displayname("normalisation email")]
//        //[column("name", typename = "varchar(256)")]
//        public string? normalizedemail { get; set; }

//        [required]
//        [displayname("email confirmé")]
//        public bool? emailconfirmed { get; set; }

//        [required]
//        [displayname("password hash")]
//        //[column("name", typename = "varchar(256)")]
//        public string? passwordhash { get; set; }

//        [required]
//        [displayname("security stamp")]
//        //[column("name", typename = "varchar(256)")]
//        public string? securitystamp { get; set; }

//        [required]
//        [displayname("concurrency stamp")]
//        //[column("name", typename = "varchar(256)")]
//        public string? concurrencystamp { get; set; }

//        [required]
//        [displayname("numero de téléphone")]
//        //[column("name", typename = "varchar(256)")]
//        public string? phonenumber { get; set; }

//        [required]
//        [displayname("confirmation du numero de téléphone")]
//        public bool? phonenumberconfirmed { get; set; }

//        [required]
//        [displayname("twofactorenabled")]
//        public bool? twofactorenabled { get; set; }

//        [required]
//        [displayname("lockoutend")]
//        public datetime? blockoutend { get; set; }

//        [required]
//        [displayname("lockoutenabled")]
//        public bool? lockoutenabled { get; set; }

//        [required]
//        [displayname("accessfailedcount")]
//        public int? accessfailedcount { get; set; }

//        [required]
//        [displayname("date de naissance")]
//        public datetime? abirthdate { get; set; }

//        [required]
//        [displayname("prénom")]
//        //[column("name", typename = "varchar(75)")]
//        public string? firstname { get; set; }

//        [required]
//        [displayname("handleby")]
//        //[column("name", typename = "varchar(450)")]
//        public string? handleby { get; set; }

//        [required]
//        [displayname("nom de famille")]
//        //[column("name", typename = "varchar(75)")]
//        public string? lastname { get; set; }

//        [required]
//        [displayname("discriminator")]
//        //[column("name", typename = "varchar(256)")]
//        public string? discriminator { get; set; }

//    }

//}


