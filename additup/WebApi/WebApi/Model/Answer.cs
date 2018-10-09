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
    /// Answer
    /// </summary>
    [DataContract]
    public partial class Answer :  IEquatable<Answer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Answer" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public Answer() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Answer" /> class.
        /// </summary>
        /// <param name="SessionId">SessionId (required).</param>
        /// <param name="ExerciseId">ExerciseId (required).</param>
        /// <param name="SubmittedAnswer">SubmittedAnswer (required).</param>
        public Answer(Guid? SessionId = default(Guid?), Guid? ExerciseId = default(Guid?), string SubmittedAnswer = default(string))
        {
            // to ensure "SessionId" is required (not null)
            if (SessionId == default(Guid?))
            {
                throw new InvalidDataException("SessionId is a required property for Answer and cannot be null");
            }
            else
            {
                this.SessionId = SessionId;
            }
            // to ensure "ExerciseId" is required (not null)
            if (ExerciseId == default(Guid?))
            {
                throw new InvalidDataException("ExerciseId is a required property for Answer and cannot be null");
            }
            else
            {
                this.ExerciseId = ExerciseId;
            }
            // to ensure "SubmittedAnswer" is required (not null)
            if (SubmittedAnswer == default(string))
            {
                throw new InvalidDataException("SubmittedAnswer is a required property for Answer and cannot be null");
            }
            else
            {
                this.SubmittedAnswer = SubmittedAnswer;
            }
        }
        
        /// <summary>
        /// Gets or Sets SessionId
        /// </summary>
        [DataMember(Name="sessionId", EmitDefaultValue=true)]
        public Guid? SessionId { get; set; }
        /// <summary>
        /// Gets or Sets ExerciseId
        /// </summary>
        [DataMember(Name="exerciseId", EmitDefaultValue=true)]
        public Guid? ExerciseId { get; set; }
        /// <summary>
        /// Gets or Sets SubmittedAnswer
        /// </summary>
        [DataMember(Name="submittedAnswer", EmitDefaultValue=true)]
        public string SubmittedAnswer { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Answer {\n");
            sb.Append("  SessionId: ").Append(SessionId).Append("\n");
            sb.Append("  ExerciseId: ").Append(ExerciseId).Append("\n");
            sb.Append("  SubmittedAnswer: ").Append(SubmittedAnswer).Append("\n");
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
        public Answer Clone()
        {
            return JsonConvert.DeserializeObject<Answer>(this.ToJson());
        }		

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as Answer);
        }

        /// <summary>
        /// Returns true if Answer instances are equal
        /// </summary>
        /// <param name="other">Instance of Answer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Answer other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.SessionId == other.SessionId ||
                    this.SessionId != default(Guid?) &&
                    this.SessionId.Equals(other.SessionId)
                ) && 
                (
                    this.ExerciseId == other.ExerciseId ||
                    this.ExerciseId != default(Guid?) &&
                    this.ExerciseId.Equals(other.ExerciseId)
                ) && 
                (
                    this.SubmittedAnswer == other.SubmittedAnswer ||
                    this.SubmittedAnswer != default(string) &&
                    this.SubmittedAnswer.Equals(other.SubmittedAnswer)
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
                if (this.SessionId != default(Guid?))
                    hash = hash * 59 + this.SessionId.GetHashCode();
                if (this.ExerciseId != default(Guid?))
                    hash = hash * 59 + this.ExerciseId.GetHashCode();
                if (this.SubmittedAnswer != default(string))
                    hash = hash * 59 + this.SubmittedAnswer.GetHashCode();
                return hash;
            }
        }

        private string ToJsonName(string param) {
            return Char.ToLowerInvariant(param[0]) + param.Substring(1);
        }
    }
}