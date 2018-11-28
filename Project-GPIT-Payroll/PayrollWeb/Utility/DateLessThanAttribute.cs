﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PayrollWeb.Utility
{
    public class DateLessThanAttribute  : ValidationAttribute, IClientValidatable
    {
        private const string lessThanErrorMessage = "{0} must be less than {1}.";
        private const string lessThanOrEqualToErrorMessage = "{0} must be less than or equal to {1}.";                

        public string OtherProperty { get; private set; }

        private bool allowEquality;

        public bool AllowEquality
        {
            get { return this.allowEquality; }
            set
            {
                this.allowEquality = value;
                
                // Set the error message based on whether or not
                // equality is allowed
                this.ErrorMessage = (value ? lessThanOrEqualToErrorMessage : lessThanErrorMessage);
            }
        }

        public DateLessThanAttribute(string otherProperty): base(lessThanErrorMessage)
        {
            if (otherProperty == null) { throw new ArgumentNullException("otherProperty"); }

            this.OtherProperty = otherProperty;            
        }        

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, this.OtherProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);   
            
            if (otherPropertyInfo == null)
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "Could not find a property named {0}.", OtherProperty));
            }

            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            DateTime decValue;
            DateTime decOtherPropertyValue;

            if (value == null && otherPropertyValue == null)
            {
                return null;
            }


            // Check to ensure the validating property is numeric
            if (value == null)
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a date value.", validationContext.DisplayName));
            }
            if (!DateTime.TryParse(value.ToString(), out decValue))
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a date value.", validationContext.DisplayName));
            }

            // Check to ensure the other property is numeric
            if (otherPropertyValue == null)
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a date value.", OtherProperty));
            }
            // Check to ensure the other property is numeric
            if (!DateTime.TryParse(otherPropertyValue.ToString(), out decOtherPropertyValue))
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a date value.", OtherProperty));
            }

            // Check for equality
            if (AllowEquality && decValue == decOtherPropertyValue)
            {
                return null;
            }
            // Check to see if the value is greater than the other property value
            else if (decValue > decOtherPropertyValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }            

            return null;
        }

        public static string FormatPropertyForClientValidation(string property)
        {
            if (property == null)
            {
                throw new ArgumentException("Value cannot be null or empty.", "property");
            }
            return "*." + property;
        }       

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationDateLessThanRule(FormatErrorMessage(metadata.DisplayName), FormatPropertyForClientValidation(this.OtherProperty), this.AllowEquality);
        }
    }
}