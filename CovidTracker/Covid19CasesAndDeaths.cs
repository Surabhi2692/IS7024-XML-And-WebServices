﻿
// <auto-generated />


namespace CovidTracker
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    

    public partial class Covid19CasesAndDeaths
    {
        [JsonProperty("instructions")]
        public Instruction[] Instructions { get; set; }
    }

    public partial class Instruction
    {
        [JsonProperty("submission_date")]
        public DateTimeOffset SubmissionDate { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }

        [JsonProperty("tot_cases")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotCases { get; set; }

        [JsonProperty("new_case")]
        public string NewCase { get; set; }

        [JsonProperty("pnew_case", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PnewCase { get; set; }

        [JsonProperty("tot_death")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotDeath { get; set; }

        [JsonProperty("new_death")]
        public string NewDeath { get; set; }

        [JsonProperty("pnew_death", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PnewDeath { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("consent_cases", NullValueHandling = NullValueHandling.Ignore)]
        public Consent? ConsentCases { get; set; }

        [JsonProperty("consent_deaths", NullValueHandling = NullValueHandling.Ignore)]
        public Consent? ConsentDeaths { get; set; }

        [JsonProperty("conf_cases", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfCases { get; set; }

        [JsonProperty("prob_cases", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProbCases { get; set; }

        [JsonProperty("conf_death", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfDeath { get; set; }

        [JsonProperty("prob_death", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProbDeath { get; set; }
    }

    public enum Consent { Agree, NA, NotAgree };

    public enum State { Ak, Ar, As, Co, Fl, Ga, Hi, Ks, Ma, Mp, Nyc, Ok, Pr, Pw, Tx, Ut, Wv };

    public partial class Covid19CasesAndDeaths
    {
        public static List<Dictionary<string,string>> FromJson(string json) => JsonConvert.DeserializeObject<List<Dictionary<string,string>>>(json, CovidTracker.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Dictionary<string, string>> self) => JsonConvert.SerializeObject(self, CovidTracker.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ConsentConverter.Singleton,
                StateConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ConsentConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Consent) || t == typeof(Consent?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Agree":
                    return Consent.Agree;
                case "N/A":
                    return Consent.NA;
                case "Not agree":
                    return Consent.NotAgree;
            }
            throw new Exception("Cannot unmarshal type Consent");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Consent)untypedValue;
            switch (value)
            {
                case Consent.Agree:
                    serializer.Serialize(writer, "Agree");
                    return;
                case Consent.NA:
                    serializer.Serialize(writer, "N/A");
                    return;
                case Consent.NotAgree:
                    serializer.Serialize(writer, "Not agree");
                    return;
            }
            throw new Exception("Cannot marshal type Consent");
        }

        public static readonly ConsentConverter Singleton = new ConsentConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class StateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(State) || t == typeof(State?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "AK":
                    return State.Ak;
                case "AR":
                    return State.Ar;
                case "AS":
                    return State.As;
                case "CO":
                    return State.Co;
                case "FL":
                    return State.Fl;
                case "GA":
                    return State.Ga;
                case "HI":
                    return State.Hi;
                case "KS":
                    return State.Ks;
                case "MA":
                    return State.Ma;
                case "MP":
                    return State.Mp;
                case "NYC":
                    return State.Nyc;
                case "OK":
                    return State.Ok;
                case "PR":
                    return State.Pr;
                case "PW":
                    return State.Pw;
                case "TX":
                    return State.Tx;
                case "UT":
                    return State.Ut;
                case "WV":
                    return State.Wv;
            }
            throw new Exception("Cannot unmarshal type State");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (State)untypedValue;
            switch (value)
            {
                case State.Ak:
                    serializer.Serialize(writer, "AK");
                    return;
                case State.Ar:
                    serializer.Serialize(writer, "AR");
                    return;
                case State.As:
                    serializer.Serialize(writer, "AS");
                    return;
                case State.Co:
                    serializer.Serialize(writer, "CO");
                    return;
                case State.Fl:
                    serializer.Serialize(writer, "FL");
                    return;
                case State.Ga:
                    serializer.Serialize(writer, "GA");
                    return;
                case State.Hi:
                    serializer.Serialize(writer, "HI");
                    return;
                case State.Ks:
                    serializer.Serialize(writer, "KS");
                    return;
                case State.Ma:
                    serializer.Serialize(writer, "MA");
                    return;
                case State.Mp:
                    serializer.Serialize(writer, "MP");
                    return;
                case State.Nyc:
                    serializer.Serialize(writer, "NYC");
                    return;
                case State.Ok:
                    serializer.Serialize(writer, "OK");
                    return;
                case State.Pr:
                    serializer.Serialize(writer, "PR");
                    return;
                case State.Pw:
                    serializer.Serialize(writer, "PW");
                    return;
                case State.Tx:
                    serializer.Serialize(writer, "TX");
                    return;
                case State.Ut:
                    serializer.Serialize(writer, "UT");
                    return;
                case State.Wv:
                    serializer.Serialize(writer, "WV");
                    return;
            }
            throw new Exception("Cannot marshal type State");
        }

        public static readonly StateConverter Singleton = new StateConverter();
    }

}