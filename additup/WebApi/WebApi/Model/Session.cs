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
    /// Session
    /// </summary>
    [DataContract]
    public partial class Session :  IEquatable<Session>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Session" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public Session() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Session" /> class.
        /// </summary>
        /// <param name="Id">Id (required).</param>
        /// <param name="Rank">Rank (required).</param>
        /// <param name="Level">Level (required).</param>
        public Session(Guid? Id = default(Guid?), RankEnum Rank = default(RankEnum), int? Level = default(int?))
        {
            // to ensure "Id" is required (not null)
            if (Id == default(Guid?))
            {
                throw new InvalidDataException("Id is a required property for Session and cannot be null");
            }
            else
            {
                this.Id = Id;
            }
            // to ensure "Rank" is required (not null)
            if (Rank == default(RankEnum))
            {
                throw new InvalidDataException("Rank is a required property for Session and cannot be null");
            }
            else
            {
                this.Rank = Rank;
            }
            // to ensure "Level" is required (not null)
            if (Level == default(int?))
            {
                throw new InvalidDataException("Level is a required property for Session and cannot be null");
            }
            else
            {
                this.Level = Level;
            }
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=true)]
        public Guid? Id { get; set; }
        /// <summary>
        /// Gets or Sets Rank
        /// </summary>
        [DataMember(Name="rank", EmitDefaultValue=true)]
        public RankEnum Rank { get; set; }
        /// <summary>
        /// Gets or Sets Level
        /// </summary>
        [DataMember(Name="level", EmitDefaultValue=true)]
        public int? Level { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Session {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Rank: ").Append(Rank).Append("\n");
            sb.Append("  Level: ").Append(Level).Append("\n");
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
        public Session Clone()
        {
            return JsonConvert.DeserializeObject<Session>(this.ToJson());
        }		

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as Session);
        }

        /// <summary>
        /// Returns true if Session instances are equal
        /// </summary>
        /// <param name="other">Instance of Session to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Session other)
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
                    this.Rank == other.Rank ||
                    this.Rank != default(RankEnum) &&
                    this.Rank.Equals(other.Rank)
                ) && 
                (
                    this.Level == other.Level ||
                    this.Level != default(int?) &&
                    this.Level.Equals(other.Level)
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
                if (this.Rank != default(RankEnum))
                    hash = hash * 59 + this.Rank.GetHashCode();
                if (this.Level != default(int?))
                    hash = hash * 59 + this.Level.GetHashCode();
                return hash;
            }
        }

        private string ToJsonName(string param) {
            return Char.ToLowerInvariant(param[0]) + param.Substring(1);
        }
    }
}