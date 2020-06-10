using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LifePlan.Models.DataModels
{
    public class DataModel
    {
    }

    public class Person
    {
        public int Id { get; set; }

        // Person Org props !
        [Display(Name = "Company")]
        public int? CompanyId { get; set; }
        [Display(Name = "Company")]
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        //Person Personalia !
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName { get { return string.Format("{0} {1} ", FirstName, LastName); } }

        //CName = Contact Name with Phonenumbers attached !
        public string CName { get { return string.Format("{0} {1} ", FullName, Ssn); } }

        [Display(Name = "Streetaddress")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postalcode")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Address")]
        public string Address { get { return string.Format("{0} {1} {2}", StreetAddress, ZipCode, City); } }

        [Display(Name = "SSN")]
        public string Ssn { get; set; }

        [Display(Name = "Telefonnummer1")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber1 { get; set; }

        [Display(Name = "Telefonnummer2")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber2 { get; set; }

        [Display(Name = "Phone #")]
        public string PhoneNumbers { get { return string.Format("{0} {1} ", PhoneNumber1, PhoneNumber2); } }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Person ID props !
        [Display(Name = "ID")]
        public string IdentityUserId { get; set; }
        [Display(Name = "ID")]
        [ForeignKey("IdentityUserId")]
        public IdentityUser IdentityUser { get; set; }

        // Person Group props !
        [Display(Name = "Category")]
        public int? PersonCategoryId { get; set; }
        [Display(Name = "Category")]
        [ForeignKey("PersonCategoryId")]
        public PersonCategory PersonCategory { get; set; }

    }

    public class PersonCategory
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string PersonCategoryName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class Company

    {
        public int Id { get; set; }
        // Company props !
        [Display(Name = "#")]
        public string CompanyNumber { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }


        [Display(Name = "Streetaddress")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postalcode")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Address")]
        public string Address { get { return string.Format("{0} {1} {2}", StreetAddress, ZipCode, City); } }

    }

}

