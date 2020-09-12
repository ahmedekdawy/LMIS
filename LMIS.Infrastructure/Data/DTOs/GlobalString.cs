using LMIS.Infrastructure.Enums;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.DTOs
{
    public struct LocalString
    {
        public readonly int L;
        public readonly string T;

        public LocalString(int lang, string text)
        {
            L = lang;
            T = text;
        }

        public LocalString(Language lang, string text)
        {
            L = (int)lang;
            T = text;
        }
    }

    public struct GlobalString
    {
        public readonly string English, French, Arabic, Id;

        public GlobalString(string en, string fr, string ar, string id = "")
        {
            English = en;
            French = fr;
            Arabic = ar;
            Id = id;
        }
        public GlobalString(List<LocalString> values, string id = "")
        {
            string en = null, fr = null, ar = null;

            foreach (var v in values)
            {
                switch (v.L)
                {
                    case (int)Language.English:
                        en = v.T;
                        break;
                    case (int)Language.French:
                        fr = v.T;
                        break;
                    case (int)Language.Arabic:
                        ar = v.T;
                        break;
                }
            }

            English = en;
            French = fr;
            Arabic = ar;
            Id = id;
        }
        public static implicit operator GlobalString(List<LocalString> values)
        {
            return new GlobalString(values);
        }
        public bool IsNullOrWhiteSpace()
        {
            return string.IsNullOrWhiteSpace(English) && string.IsNullOrWhiteSpace(French) &&
                   string.IsNullOrWhiteSpace(Arabic);
        }
        public bool Contains(string value)
        {
            value = value.Trim().ToLower();
            return value == English.Trim().ToLower() || value == French.Trim().ToLower() || value == Arabic.Trim().ToLower();
        }
        public List<string> ToLowerTrimmedStrings()
        {
            var ret = new List<string>();

            if (English != null) ret.Add(English.Trim().ToLower());
            if (French != null) ret.Add(French.Trim().ToLower());
            if (Arabic != null) ret.Add(Arabic.Trim().ToLower());

            return ret;
        }
        public GlobalString ToReducedCopy(Language lang, bool fallbackIfEmpty = false)
        {
            return new GlobalString(new List<LocalString> { ToLocalString(lang, fallbackIfEmpty) });
        }
        public LocalString ToLocalString(Language lang, bool fallbackIfEmpty = false)
        {
            if (fallbackIfEmpty) return FallBack(lang);

            switch (lang)
            {
                case Language.Arabic:
                    return new LocalString(lang, Arabic ?? "");
                case Language.French:
                    return new LocalString(lang, French ?? "");
                default:
                    return new LocalString(lang, English ?? "");
            }
        }
        public List<LocalString> ToLocalStrings()
        {
            var ret = new List<LocalString>();

            if (English != null) ret.Add(new LocalString(Language.English, English));
            if (French != null) ret.Add(new LocalString(Language.French, French));
            if (Arabic != null) ret.Add(new LocalString(Language.Arabic, Arabic));

            return ret;
        }
        private LocalString FallBack(Language lang)
        {
            switch (lang)
            {
                case Language.Arabic:
                    if (!string.IsNullOrWhiteSpace(Arabic)) return new LocalString(Language.Arabic, Arabic);
                    if (!string.IsNullOrWhiteSpace(English)) return new LocalString(Language.English, English);
                    if (!string.IsNullOrWhiteSpace(French)) return new LocalString(Language.French, French);
                    break;
                case Language.French:
                    if (!string.IsNullOrWhiteSpace(French)) return new LocalString(Language.French, French);
                    if (!string.IsNullOrWhiteSpace(English)) return new LocalString(Language.English, English);
                    if (!string.IsNullOrWhiteSpace(Arabic)) return new LocalString(Language.Arabic, Arabic);
                    break;
                default:
                    if (!string.IsNullOrWhiteSpace(English)) return new LocalString(Language.English, English);
                    if (!string.IsNullOrWhiteSpace(French)) return new LocalString(Language.French, French);
                    if (!string.IsNullOrWhiteSpace(Arabic)) return new LocalString(Language.Arabic, Arabic);
                   break;
            }

            return new LocalString(lang, "");
        }
    }
}