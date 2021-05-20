using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public static class SSCParser 
{
    // Start is called before the first frame update
   
    public static SongObject ParseFile(string data)
    {

        //objeto que crearemos  
        var songData = new SongObject();


        //limpiamos comentarios 
        Regex regex = new Regex("(\\s+//-([^;]+)\\s)|(//[\\s+]measure\\s[0-9]+\\s)");
        data = regex.Replace(data, "");

        // string text = "One car red car blue car";
        string pat = "#([^;]+);";

        // Instantiate the regular expression object.
        Regex r = new Regex(pat, RegexOptions.IgnoreCase);
        Match m = r.Match(data);

        int currentLevel = -1;

        while (m.Success)
        {
            //set key value 
            var rawValue = m.Groups[1].Value.Split(':');
            string key = rawValue[0].ToString();
            string value = rawValue[1].ToString();
            //se avanza en el contador de niveles
            if (key.Equals("NOTEDATA"))
            {
                currentLevel++;
                songData.Levels[currentLevel] = new LevelObject();
            }
            if (currentLevel > -1)//pa cuando es del nivel 
            {
                switch (key)
                {
                    case "NOTES":
                        songData.Levels[currentLevel].Notes = ProcessNotes(value);
                        break;
                    case "BPMS":
                    case "STOPS":
                    case "DELAYS":
                    case "WARPS":
                    case "TIMESIGNATURES":
                    case "TICKCOUNTS":
                    case "COMBOS":
                    case "SPEEDS":
                    case "SCROLLS":
                        songData.Levels[currentLevel].LevelModifiers[key] = GetModifiers(key, value);
                        break;
                    default:
                        songData.Levels[currentLevel].LevelTags[key] = value;
                        break;
                }
            }
            else// globales
            {
                switch (key)
                {
                    case "BPMS":
                    case "STOPS":
                    case "DELAYS":
                    case "WARPS":
                    case "TIMESIGNATURES":
                    case "TICKCOUNTS":
                    case "COMBOS":
                    case "SPEEDS":
                    case "SCROLLS":
                        songData.SongModifiers[key] = GetModifiers(key, value);
                        break;
                    default:
                        songData.SongTags[key] = value;
                        break;
                }
            }
            m = m.NextMatch();
        }
        return songData;

    }


    public static List<ModifierLevel> GetModifiers(string name, string modifieres)
    {
        var list = new List<ModifierLevel>();
        var mods = modifieres.Replace("\r", "").Replace("\n", "").Split(',');
        foreach (var mod in mods)
        {
            var values = mod.Split('=');
            try
            {
                list.Add(new ModifierLevel()
                {
                    Type = name,
                    Beat = Double.Parse(values[0]),
                    Value = Double.Parse(values[1]),
                    Value2 = values.Length > 2 ? Double.Parse(values[2]) : 0
                });
            }
            catch (Exception ex) {

              //  throw ex;
            }
        }
        return list;
    }
    public static List<RowArrows> ProcessNotes(string data)
    {
        var list = new List<RowArrows>();
        var blocks = data.Replace("\n\n", "\n").Split(',');
        double currentBeat = 0d;

        foreach (var block in blocks)
        {
            var rows = block.Split('\n');
            foreach (var row in rows)
            {
                if (!isEmptyRow(row.Replace("\n", "").Replace("\r", "")))
                {
                    list.Add(new RowArrows()
                    {
                        CurrentBeat = currentBeat,
                        Arrows = ProcessGameRow(row)
                    });
                }
                currentBeat += (4.0 / block.Length);
            }
        }
        return list;
    }

    public static List<Arrow> ProcessGameRow(string data)
    {
        List<Arrow> list = new List<Arrow>();
        var arrays = data.Replace("\n", "").Replace("\r", "").ToCharArray();
        foreach (var character in arrays)
        {
            list.Add(new Arrow()
            {
                Type = SSCUtils.CharToNote(character)
            });
        }
        return list;
    }

    public static bool isEmptyRow(string row)
    {
        return row.Replace("0", "").Equals("");
    }


    public class Arrow
    {
        public NoteTypes Type { get; set; }
    }

    public class RowArrows
    {
        public List<Arrow> Arrows { get; set; } = new List<Arrow>();
        public double CurrentBeat { get; set; }
        public List<ModifierLevel> Modifiers { get; set; }
    }

    public class ModifierLevel
    {
        public string Type { get; set; }
        public double Beat { get; set; }
        public double Value { get; set; }
        public double Value2 { get; set; }
    }

    public class SongObject
    {
        public Dictionary<string, dynamic> SongTags { get; set; } = new Dictionary<string, dynamic>();
        public Dictionary<string, List<ModifierLevel>> SongModifiers { get; set; } = new Dictionary<string, List<ModifierLevel>>();
        public Dictionary<int, LevelObject> Levels = new Dictionary<int, LevelObject>();

    }
    public class LevelObject
    {

        public List<RowArrows> Notes { get; set; }
        public Dictionary<string, dynamic> LevelTags { get; set; } = new Dictionary<string, dynamic>();
        public Dictionary<string, List<ModifierLevel>> LevelModifiers { get; set; } = new Dictionary<string, List<ModifierLevel>>();
    }

}




