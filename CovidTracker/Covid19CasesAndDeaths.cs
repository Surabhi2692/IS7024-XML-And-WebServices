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

    public enum State { AL, AK, AR, AS, AZ, CA, CO, CT, DC, DE, FL, GA, GU, HI, IA, ID, IL, IN, KS, KY, LA, MA, MD, ME, MI, MN, MO, MP, MS, MT, NC, ND, NE, NH, NJ, NM, NV, NY, NYC, OH, OK, OR, PA, PR, PW, RI, SC, SD, TN, TX, UT, VA, VI, VT, WA, WI, WV, WY };

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
                case "AK":
                    return State.AK;
                case "AR":
                    return State.AR;
                case "AS":
                    return State.AS;
                case "AZ":
                    return State.AZ;
                case "CA":
                    return State.CA;
                case "CO":
                    return State.CO;
                case "CT":
                    return State.CT;
                case "DC":
                    return State.DC;
                case "DE":
                    return State.DE;
                case "FL":
                    return State.FL;
                case "GA":
                    return State.GA;
                case "GU":
                    return State.GU;
                case "HI":
                    return State.HI;
                case "IA":
                    return State.IA;
                case "ID":
                    return State.ID;
                case "IL":
                    return State.IL;
                case "IN":
                    return State.IN;
                case "KS":
                    return State.KS;
                case "KY":
                    return State.KY;
                case "LA":
                    return State.LA;
                case "MA":
                    return State.MA;
                case "MD":
                    return State.MD;
                case "ME":
                    return State.ME;
                case "MI":
                    return State.MI;
                case "MN":
                    return State.MN;
                case "MO":
                    return State.MO;
                case "MP":
                    return State.MP;
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
                case "NJ":
                    return State.NJ;
                case "NM":
                    return State.NM;
                case "NV":
                    return State.NV;
                case "NY":
                    return State.NY;
                case "NYC":
                    return State.NYC;
                case "OH":
                    return State.OH;
                case "OK":
                    return State.OK;
                case "OR":
                    return State.OR;
                case "PA":
                    return State.PA;
                case "PR":
                    return State.PR;
                case "PW":
                    return State.PW;
                case "RI":
                    return State.RI;
                case "SC":
                    return State.SC;
                case "SD":
                    return State.SD;
                case "TN":
                    return State.TN;
                case "TX":
                    return State.TX;
                case "UT":
                    return State.UT;
                case "VA":
                    return State.VA;
                case "VI":
                    return State.VI;
                case "VT":
                    return State.VT;
                case "WA":
                    return State.WA;
                case "WI":
                    return State.WI;
                case "WV":
                    return State.WV;
                case "WY":
                    return State.WY;
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
                case State.AK:
                    serializer.Serialize(writer, "AK");
                    return;
                case State.AR:
                    serializer.Serialize(writer, "AR");
                    return;
                case State.AS:
                    serializer.Serialize(writer, "AS");
                    return;
                case State.AZ:
                    serializer.Serialize(writer, "AZ");
                    return;
                case State.CA:
                    serializer.Serialize(writer, "CA");
                    return;
                case State.CO:
                    serializer.Serialize(writer, "CO");
                    return;
                case State.CT:
                    serializer.Serialize(writer, "CT");
                    return;
                case State.DC:
                    serializer.Serialize(writer, "DC");
                    return;
                case State.DE:
                    serializer.Serialize(writer, "DE");
                    return;
                case State.FL:
                    serializer.Serialize(writer, "FL");
                    return;
                case State.GA:
                    serializer.Serialize(writer, "GA");
                    return;
                case State.GU:
                    serializer.Serialize(writer, "GU");
                    return;
                case State.HI:
                    serializer.Serialize(writer, "HI");
                    return;
                case State.IA:
                    serializer.Serialize(writer, "IA");
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
                case State.KS:
                    serializer.Serialize(writer, "KS");
                    return;
                case State.KY:
                    serializer.Serialize(writer, "KY");
                    return;
                case State.LA:
                    serializer.Serialize(writer, "LA");
                    return;
                case State.MA:
                    serializer.Serialize(writer, "MA");
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
                case State.MN:
                    serializer.Serialize(writer, "MN");
                    return;
                case State.MO:
                    serializer.Serialize(writer, "MO");
                    return;
                case State.MP:
                    serializer.Serialize(writer, "MP");
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
                case State.NJ:
                    serializer.Serialize(writer, "NJ");
                    return;
                case State.NM:
                    serializer.Serialize(writer, "NM");
                    return;
                case State.NV:
                    serializer.Serialize(writer, "NV");
                    return;
                case State.NY:
                    serializer.Serialize(writer, "NY");
                    return;
                case State.NYC:
                    serializer.Serialize(writer, "NYC");
                    return;
                case State.OH:
                    serializer.Serialize(writer, "OH");
                    return;
                case State.OK:
                    serializer.Serialize(writer, "OK");
                    return;
                case State.OR:
                    serializer.Serialize(writer, "OR");
                    return;
                case State.PA:
                    serializer.Serialize(writer, "PA");
                    return;
                case State.PR:
                    serializer.Serialize(writer, "PR");
                    return;
                case State.PW:
                    serializer.Serialize(writer, "PW");
                    return;
                case State.RI:
                    serializer.Serialize(writer, "RI");
                    return;
                case State.SC:
                    serializer.Serialize(writer, "SC");
                    return;
                case State.SD:
                    serializer.Serialize(writer, "SD");
                    return;
                case State.TN:
                    serializer.Serialize(writer, "TN");
                    return;
                case State.TX:
                    serializer.Serialize(writer, "TX");
                    return;
                case State.UT:
                    serializer.Serialize(writer, "UT");
                    return;
                case State.VA:
                    serializer.Serialize(writer, "VA");
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
                case State.WV:
                    serializer.Serialize(writer, "WV");
                    return;
                case State.WY:
                    serializer.Serialize(writer, "WY");
                    return;
            }
            throw new Exception("Cannot marshal type State");
        }

        public static readonly StateConverter Singleton = new StateConverter();
    }
}
