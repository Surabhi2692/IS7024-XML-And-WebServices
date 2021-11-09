
namespace CovidTracker.Vaccines
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Covid19Vaccine
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("mmwr_week")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MmwrWeek { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("administered_daily")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdministeredDaily { get; set; }

        [JsonProperty("administered_cumulative")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdministeredCumulative { get; set; }

        [JsonProperty("administered_7_day_rolling")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Administered7_DayRolling { get; set; }

        [JsonProperty("admin_dose_1_daily")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdminDose1_Daily { get; set; }

        [JsonProperty("admin_dose_1_cumulative")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdminDose1_Cumulative { get; set; }

        [JsonProperty("admin_dose_1_day_rolling")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdminDose1_DayRolling { get; set; }

        [JsonProperty("date_type")]
        public DateType DateType { get; set; }

        [JsonProperty("administered_daily_change")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdministeredDailyChange { get; set; }

        [JsonProperty("administered_daily_change_1")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdministeredDailyChange1 { get; set; }

        [JsonProperty("series_complete_daily")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SeriesCompleteDaily { get; set; }

        [JsonProperty("series_complete_cumulative")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SeriesCompleteCumulative { get; set; }

        [JsonProperty("series_complete_day_rolling")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SeriesCompleteDayRolling { get; set; }

        [JsonProperty("booster_daily")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long BoosterDaily { get; set; }

        [JsonProperty("booster_cumulative")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long BoosterCumulative { get; set; }

        [JsonProperty("booster_7_day_rolling_average")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Booster7_DayRollingAverage { get; set; }
    }

    public enum DateType { Admin, Report };

    public partial class Covid19Vaccine
    {
        public static Covid19Vaccine[] FromJson(string json) => JsonConvert.DeserializeObject<Covid19Vaccine[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Covid19Vaccine[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                DateTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
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

    internal class DateTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DateType) || t == typeof(DateType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Admin":
                    return DateType.Admin;
                case "Report":
                    return DateType.Report;
            }
            throw new Exception("Cannot unmarshal type DateType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DateType)untypedValue;
            switch (value)
            {
                case DateType.Admin:
                    serializer.Serialize(writer, "Admin");
                    return;
                case DateType.Report:
                    serializer.Serialize(writer, "Report");
                    return;
            }
            throw new Exception("Cannot marshal type DateType");
        }

        public static readonly DateTypeConverter Singleton = new DateTypeConverter();
    }
}
