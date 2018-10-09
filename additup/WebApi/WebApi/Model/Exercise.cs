/* 
 * AddItUp
 *
 * Add It Up API Definitions
 *
 * OpenAPI spec version: 1.0.0
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    /// <summary>
    /// Exercise
    /// </summary>
    [DataContract]
    public partial class Exercise :  IEquatable<Exercise>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Exercise" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public Exercise() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Exercise" /> class.
        /// </summary>
        /// <param name="Id">Id (required).</param>
        /// <param name="Expression1">Expression1 (required).</param>
        /// <param name="Expression2">Expression2 (required).</param>
        /// <param name="TimeLimit">TimeLimit (required).</param>
        /// <param name="CreatedDateTime">CreatedDateTime (required).</param>
        /// <param name="AssignedSession">AssignedSession (required).</param>
        public Exercise(Guid? Id = default(Guid?), int? Expression1 = default(int?), int? Expression2 = default(int?), int? TimeLimit = default(int?), DateTime? CreatedDateTime = default(DateTime?), Guid? AssignedSession = default(Guid?))
        {
            // to ensure "Id" is required (not null)
            if (Id == default(Guid?))
            {
                throw new InvalidDataException("Id is a required property for Exercise and cannot be null");
            }
            else
            {
                this.Id = Id;
            }
            // to ensure "Expression1" is required (not null)
            if (Expression1 == default(int?))
            {
                throw new InvalidDataException("Expression1 is a required property for Exercise and cannot be null");
            }
            else
            {
                this.Expression1 = Expression1;
            }
            // to ensure "Expression2" is required (not null)
            if (Expression2 == default(int?))
            {
                throw new InvalidDataException("Expression2 is a required property for Exercise and cannot be null");
            }
            else
            {
                this.Expression2 = Expression2;
            }
            // to ensure "TimeLimit" is required (not null)
            if (TimeLimit == default(int?))
            {
                throw new InvalidDataException("TimeLimit is a required property for Exercise and cannot be null");
            }
            else
            {
                this.TimeLimit = TimeLimit;
            }
            // to ensure "CreatedDateTime" is required (not null)
            if (CreatedDateTime == default(DateTime?))
            {
                throw new InvalidDataException("CreatedDateTime is a required property for Exercise and cannot be null");
            }
            else
            {
                this.CreatedDateTime = CreatedDateTime;
            }
            // to ensure "AssignedSession" is required (not null)
            if (AssignedSession == default(Guid?))
            {
                throw new InvalidDataException("AssignedSession is a required property for Exercise and cannot be null");
            }
            else
            {
                this.AssignedSession = AssignedSession;
            }
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=true)]
        public Guid? Id { get; set; }
        /// <summary>
        /// Gets or Sets Expression1
        /// </summary>
        [DataMember(Name="expression1", EmitDefaultValue=true)]
        public int? Expression1 { get; set; }
        /// <summary>
        /// Gets or Sets Expression2
        /// </summary>
        [DataMember(Name="expression2", EmitDefaultValue=true)]
        public int? Expression2 { get; set; }
        /// <summary>
        /// Gets or Sets TimeLimit
        /// </summary>
        [DataMember(Name="timeLimit", EmitDefaultValue=true)]
        public int? TimeLimit { get; set; }
        /// <summary>
        /// Gets or Sets CreatedDateTime
        /// </summary>
        [DataMember(Name="createdDateTime", EmitDefaultValue=true)]
        public DateTime? CreatedDateTime { get; set; }
        /// <summary>
        /// Gets or Sets AssignedSession
        /// </summary>
        [DataMember(Name="assignedSession", EmitDefaultValue=true)]
        public Guid? AssignedSession { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Exercise {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Expression1: ").Append(Expression1).Append("\n");
            sb.Append("  Expression2: ").Append(Expression2).Append("\n");
            sb.Append("  TimeLimit: ").Append(TimeLimit).Append("\n");
            sb.Append("  CreatedDateTime: ").Append(CreatedDateTime).Append("\n");
            sb.Append("  AssignedSession: ").Append(AssignedSession).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
		
        /// <summary>
        /// Returns clone of the object
        /// </summary>
        /// <returns>Clone of the object</returns>
        public Exercise Clone()
        {
            return JsonConvert.DeserializeObject<Exercise>(this.ToJson());
        }		

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as Exercise);
        }

        /// <summary>
        /// Returns true if Exercise instances are equal
        /// </summary>
        /// <param name="other">Instance of Exercise to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Exercise other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Id == other.Id ||
                    this.Id != default(Guid?) &&
                    this.Id.Equals(other.Id)
                ) && 
                (
                    this.Expression1 == other.Expression1 ||
                    this.Expression1 != default(int?) &&
                    this.Expression1.Equals(other.Expression1)
                ) && 
                (
                    this.Expression2 == other.Expression2 ||
                    this.Expression2 != default(int?) &&
                    this.Expression2.Equals(other.Expression2)
                ) && 
                (
                    this.TimeLimit == other.TimeLimit ||
                    this.TimeLimit != default(int?) &&
                    this.TimeLimit.Equals(other.TimeLimit)
                ) && 
                (
                    this.CreatedDateTime == other.CreatedDateTime ||
                    this.CreatedDateTime != default(DateTime?) &&
                    this.CreatedDateTime.Equals(other.CreatedDateTime)
                ) && 
                (
                    this.AssignedSession == other.AssignedSession ||
                    this.AssignedSession != default(Guid?) &&
                    this.AssignedSession.Equals(other.AssignedSession)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.Id != default(Guid?))
                    hash = hash * 59 + this.Id.GetHashCode();
                if (this.Expression1 != default(int?))
                    hash = hash * 59 + this.Expression1.GetHashCode();
                if (this.Expression2 != default(int?))
                    hash = hash * 59 + this.Expression2.GetHashCode();
                if (this.TimeLimit != default(int?))
                    hash = hash * 59 + this.TimeLimit.GetHashCode();
                if (this.CreatedDateTime != default(DateTime?))
                    hash = hash * 59 + this.CreatedDateTime.GetHashCode();
                if (this.AssignedSession != default(Guid?))
                    hash = hash * 59 + this.AssignedSession.GetHashCode();
                return hash;
            }
        }
        
        private string ToJsonName(string param) {
            return Char.ToLowerInvariant(param[0]) + param.Substring(1);
        }
    }
}