namespace CovidTracker.CasesAndDeaths
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Covid19CasesAndDeaths
    {
        [JsonProperty("submission_date")]
        public DateTimeOffset SubmissionDate { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }

        [JsonProperty("tot_cases")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotCases { get; set; }

        [JsonProperty("conf_cases", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfCases { get; set; }

        [JsonProperty("prob_cases", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProbCases { get; set; }

        [JsonProperty("new_case")]
        public string NewCase { get; set; }

        [JsonProperty("pnew_case", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PnewCase { get; set; }

        [JsonProperty("tot_death")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotDeath { get; set; }

        [JsonProperty("conf_death", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfDeath { get; set; }

        [JsonProperty("prob_death", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProbDeath { get; set; }

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
    }

    public enum Consent { Agree, NA, NotAgree };

    public enum State { Al, Ca, Ct, De, Gu, Id, Il, In, Md, Me, Mi, Mo, Ms, Mt, Nc, Nd, Ne, Nh, Nv, Vi, Vt, Wa, Wi };

    public partial class Covid19CasesAndDeaths
    {
        public static Covid19CasesAndDeaths[] FromJson(string json) => JsonConvert.DeserializeObject<Covid19CasesAndDeaths[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Covid19CasesAndDeaths[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
                case "AL":
                    return State.Al;
                case "CA":
                    return State.Ca;
                case "CT":
                    return State.Ct;
                case "DE":
                    return State.De;
                case "GU":
                    return State.Gu;
                case "ID":
                    return State.Id;
                case "IL":
                    return State.Il;
                case "IN":
                    return State.In;
                case "MD":
                    return State.Md;
                case "ME":
                    return State.Me;
                case "MI":
                    return State.Mi;
                case "MO":
                    return State.Mo;
                case "MS":
                    return State.Ms;
                case "MT":
                    return State.Mt;
                case "NC":
                    return State.Nc;
                case "ND":
                    return State.Nd;
                case "NE":
                    return State.Ne;
                case "NH":
                    return State.Nh;
                case "NV":
                    return State.Nv;
                case "VI":
                    return State.Vi;
                case "VT":
                    return State.Vt;
                case "WA":
                    return State.Wa;
                case "WI":
                    return State.Wi;
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
                case State.Al:
                    serializer.Serialize(writer, "AL");
                    return;
                case State.Ca:
                    serializer.Serialize(writer, "CA");
                    return;
                case State.Ct:
                    serializer.Serialize(writer, "CT");
                    return;
                case State.De:
                    serializer.Serialize(writer, "DE");
                    return;
                case State.Gu:
                    serializer.Serialize(writer, "GU");
                    return;
                case State.Id:
                    serializer.Serialize(writer, "ID");
                    return;
                case State.Il:
                    serializer.Serialize(writer, "IL");
                    return;
                case State.In:
                    serializer.Serialize(writer, "IN");
                    return;
                case State.Md:
                    serializer.Serialize(writer, "MD");
                    return;
                case State.Me:
                    serializer.Serialize(writer, "ME");
                    return;
                case State.Mi:
                    serializer.Serialize(writer, "MI");
                    return;
                case State.Mo:
                    serializer.Serialize(writer, "MO");
                    return;
                case State.Ms:
                    serializer.Serialize(writer, "MS");
                    return;
                case State.Mt:
                    serializer.Serialize(writer, "MT");
                    return;
                case State.Nc:
                    serializer.Serialize(writer, "NC");
                    return;
                case State.Nd:
                    serializer.Serialize(writer, "ND");
                    return;
                case State.Ne:
                    serializer.Serialize(writer, "NE");
                    return;
                case State.Nh:
                    serializer.Serialize(writer, "NH");
                    return;
                case State.Nv:
                    serializer.Serialize(writer, "NV");
                    return;
                case State.Vi:
                    serializer.Serialize(writer, "VI");
                    return;
                case State.Vt:
                    serializer.Serialize(writer, "VT");
                    return;
                case State.Wa:
                    serializer.Serialize(writer, "WA");
                    return;
                case State.Wi:
                    serializer.Serialize(writer, "WI");
                    return;
            }
            throw new Exception("Cannot marshal type State");
        }

        public static readonly StateConverter Singleton = new StateConverter();
    }
}
