using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prueba.Models.Base;

namespace Prueba.Models
{
    public class User : BaseTransaction
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }

    public class Info
    {
        [JsonProperty("seed")]
        public string Seed { get; set; }

        [JsonProperty("results")]
        public long Results { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class Result
    {
        [JsonProperty("gender")]
        [JsonConverter(typeof(GenderConverter))]
        public Gender Gender { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("login")]
        public Login Login { get; set; }

        [JsonProperty("dob")]
        public Dob Dob { get; set; }

        [JsonProperty("registered")]
        public Dob Registered { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("cell")]
        public string Cell { get; set; }

        [JsonProperty("id")]
        public Id Id { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }

        [JsonProperty("nat")]
        public string Nat { get; set; }
    }

    public class Dob
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("age")]
        public long Age { get; set; }
    }

    public class Id
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Location
    {
        [JsonProperty("street")]
        public Street Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }


        [JsonProperty("coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("timezone")]
        public Timezone Timezone { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }

    public class Street
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Timezone
    {
        [JsonProperty("offset")]
        public string Offset { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Login
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("salt")]
        public string Salt { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }
    }

    public class Name
    {
        [JsonProperty("title")]
        [JsonConverter(typeof(TitleConverter))]
        public Title Title { get; set; }

        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }

    public class Picture
    {
        [JsonProperty("large")]
        public Uri Large { get; set; }

        [JsonProperty("medium")]
        public Uri Medium { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }

    public enum Gender { Female, Male, Other };

    public enum Title { Madame, Miss, Mr, Mrs, Ms, Citizen };

    public struct Postcode
    {
        public long? Integer;
        public string String;

        public static implicit operator Postcode(long Integer) => new Postcode { Integer = Integer };
        public static implicit operator Postcode(string String) => new Postcode { String = String };
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                GenderConverter.Singleton,
                TitleConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GenderConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Gender) || t == typeof(Gender?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "female":
                    return Gender.Female;
                case "male":
                    return Gender.Male;
                default:
                    return Gender.Other;
            }
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Gender)untypedValue;
            switch (value)
            {
                case Gender.Female:
                    serializer.Serialize(writer, "female");
                    return;
                case Gender.Male:
                    serializer.Serialize(writer, "male");
                    return;
            }
            throw new Exception("Cannot marshal type Gender");
        }

        public static readonly GenderConverter Singleton = new GenderConverter();
    }
    
    internal class TitleConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Title) || t == typeof(Title?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Madame":
                    return Title.Madame;
                case "Miss":
                    return Title.Miss;
                case "Mr":
                    return Title.Mr;
                case "Mrs":
                    return Title.Mrs;
                case "Ms":
                    return Title.Ms;
                default:
                    return Title.Citizen;
            }
            
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Title)untypedValue;
            switch (value)
            {
                case Title.Madame:
                    serializer.Serialize(writer, "Madame");
                    return;
                case Title.Miss:
                    serializer.Serialize(writer, "Miss");
                    return;
                case Title.Mr:
                    serializer.Serialize(writer, "Mr");
                    return;
                case Title.Mrs:
                    serializer.Serialize(writer, "Mrs");
                    return;
                case Title.Ms:
                    serializer.Serialize(writer, "Ms");
                    return;
                default:
                    serializer.Serialize(writer, "Other");
                    return;
            }
        }

        public static readonly TitleConverter Singleton = new TitleConverter();
    }
}
