namespace CovidTracker.CasesAndDeaths
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Covid19CasesAndDeaths
    {   //SubmissionDate = the date that the state submit their records
        [JsonProperty("submission_date")]
        public DateTimeOffset SubmissionDate { get; set; }
        //state
        [JsonProperty("state")]
        public State State { get; set; }
        //tot_cases --> Total Cases
        [JsonProperty("tot_cases")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotCases { get; set; }
        //Confimed cases
        [JsonProperty("conf_cases", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfCases { get; set; }
        //probably cases
        [JsonProperty("prob_cases", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProbCases { get; set; }
        //new cases
        [JsonProperty("new_case")]
        public string NewCase { get; set; }
        //number of new probable deaths
        [JsonProperty("pnew_case", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PnewCase { get; set; }
        //total death
        [JsonProperty("tot_death")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotDeath { get; set; }
        //confirmed death
        [JsonProperty("conf_death", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfDeath { get; set; }
        //probable death
        [JsonProperty("prob_death", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProbDeath { get; set; }
        //new death
        [JsonProperty("new_death")]
        public string NewDeath { get; set; }
        //probable new death
        [JsonProperty("pnew_death", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PnewDeath { get; set; }
        //date and time record was created
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        //if agree, then confirmed and probable cases are included, if not agree, then total cases will not be included
        [JsonProperty("consent_cases", NullValueHandling = NullValueHandling.Ignore)]
        public Consent? ConsentCases { get; set; }
        //if agree, then confirmed and death cases are included, if not agree, then total death cases will not be included
        [JsonProperty("consent_deaths", NullValueHandling = NullValueHandling.Ignore)]
        public Consent? ConsentDeaths { get; set; }
    }

    public enum Consent { Agree, NA, NotAgree };

    public enum State { AL, CA, CT, DE, GU, ID, IL, IN, MD, ME, MI, MO, MS, MT, NC, ND, NE, NH, NV, VI, VT, WA, WI };

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
                    return State.AL;
                case "CA":
                    return State.CA;
                case "CT":
                    return State.CT;
                case "DE":
                    return State.DE;
                case "GU":
                    return State.GU;
                case "ID":
                    return State.ID;
                case "IL":
                    return State.IL;
                case "IN":
                    return State.IN;
                case "MD":
                    return State.MD;
                case "ME":
                    return State.ME;
                case "MI":
                    return State.MI;
                case "MO":
                    return State.MO;
                case "MS":
                    return State.MS;
                case "MT":
                    return State.MT;
                case "NC":
                    return State.NC;
                case "ND":
                    return State.ND;
                case "NE":
                    return State.NE;
                case "NH":
                    return State.NH;
                case "NV":
                    return State.NV;
                case "VI":
                    return State.VI;
                case "VT":
                    return State.VT;
                case "WA":
                    return State.WA;
                case "WI":
                    return State.WI;
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
                case State.AL:
                    serializer.Serialize(writer, "AL");
                    return;
                case State.CA:
                    serializer.Serialize(writer, "CA");
                    return;
                case State.CT:
                    serializer.Serialize(writer, "CT");
                    return;
                case State.DE:
                    serializer.Serialize(writer, "DE");
                    return;
                case State.GU:
                    serializer.Serialize(writer, "GU");
                    return;
                case State.ID:
                    serializer.Serialize(writer, "ID");
                    return;
                case State.IL:
                    serializer.Serialize(writer, "IL");
                    return;
                case State.IN:
                    serializer.Serialize(writer, "IN");
                    return;
                case State.MD:
                    serializer.Serialize(writer, "MD");
                    return;
                case State.ME:
                    serializer.Serialize(writer, "ME");
                    return;
                case State.MI:
                    serializer.Serialize(writer, "MI");
                    return;
                case State.MO:
                    serializer.Serialize(writer, "MO");
                    return;
                case State.MS:
                    serializer.Serialize(writer, "MS");
                    return;
                case State.MT:
                    serializer.Serialize(writer, "MT");
                    return;
                case State.NC:
                    serializer.Serialize(writer, "NC");
                    return;
                case State.ND:
                    serializer.Serialize(writer, "ND");
                    return;
                case State.NE:
                    serializer.Serialize(writer, "NE");
                    return;
                case State.NH:
                    serializer.Serialize(writer, "NH");
                    return;
                case State.NV:
                    serializer.Serialize(writer, "NV");
                    return;
                case State.VI:
                    serializer.Serialize(writer, "VI");
                    return;
                case State.VT:
                    serializer.Serialize(writer, "VT");
                    return;
                case State.WA:
                    serializer.Serialize(writer, "WA");
                    return;
                case State.WI:
                    serializer.Serialize(writer, "WI");
                    return;
            }
            throw new Exception("Cannot marshal type State");
        }

        public static readonly StateConverter Singleton = new StateConverter();
    }
}
