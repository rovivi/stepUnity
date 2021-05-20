using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NoteTypes
{
    NOTE_EMPTY,
    NOTE_TAP,
    NOTE_LONG_START,
    NOTE_LONG_END,
    NOTE_FAKE,
    
}




public static class SSCUtils
{
    // Start is called before the first frame update
    public static NoteTypes CharToNote(char note)
    {

        switch (note)
        {
            case '1': return NoteTypes.NOTE_TAP;
            case '2': return NoteTypes.NOTE_LONG_START;
            case '3': return NoteTypes.NOTE_LONG_END;
            case '0':
            default: return NoteTypes.NOTE_EMPTY;
        }

    }
}
